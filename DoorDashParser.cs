using System;
using System.IO;
using System.Collections.Generic;

using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.License;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

namespace GmailQuickstart {

    public class DoorDashParser {

        public static DoorDashMenu menu;

        //Constructor
        public DoorDashParser() {
            
            //Load the itext license
            try {
                LicenseKey.LoadLicenseFile(Program.iTextLicensePath);
            } catch (LicenseKeyException e) {
                Console.WriteLine(e);
            }
        }

        /* Extracts the text from the pdf and returns it as a List of strings */
        public List<string> ExtractTextFromPDF(string pathToPdf, string messageId) {

            PdfReader reader = new PdfReader(pathToPdf);
            PdfDocument doc = new PdfDocument(reader);

            List<string> lines = new List<string>();

            int lineCount = 0;
            for (int i = 1; i <= doc.GetNumberOfPages(); i++) {

                PdfPage page = doc.GetPage(i);

                string text = PdfTextExtractor.GetTextFromPage(page);

                string[] pageLines = text.Split('\n'); //Split the string into lines delimited by '\n';

                //Add each line into the List of lines to prepare for parsing 
                for (int j = 0; j < pageLines.Length; j++, lineCount++) {

                    string line = pageLines[j];

                    //This checks if the 1st char of the line is an unprintable symbol such as '•', and replaces it with '-'
                    //to make parsing easier later
                    char firstCharOfLine = pageLines[j][0];
                    int valOfFirstChar = Convert.ToInt32(firstCharOfLine);
                    if (valOfFirstChar == 127) {
                        string tmp = line.Substring(1);
                        line = tmp.Insert(0, "-");
                    }

                    lines.Add(line);

                    //Console.WriteLine(("LINE " + lineCount.ToString().PadLeft(3) + "  " + line));
                }
            }
            if (Program.DebugBuild) PrintToFile(lines, messageId);

            return lines;
        }

        /* Parses the information from the lines and returns the information in a Order object */
        public Order ParseOrder(List<string> lines, DateTime timeReceived) {

            Order order = new Order();
            order.Service = "DoorDash";
            order.TimeReceived = timeReceived;

            ParseCustomerName(lines[0], order);

            int start = 9;

            //Uncommon case
            if (lines[3].StartsWith("Tea for you")) {        
                ParsePickUpTime(lines[1], order);
                ParseOrderNumber(lines[4], order);
                ParseContactNumber(lines[8], order);
            //Rare case - not sure if still used
            }else if (lines[1].StartsWith("Tea for you")) {
                //Need to parse Pickup time 
                ParseOrderNumber(lines[2], order);
                ParseContactNumber(lines[6], order);
                start = 7;
            //Common case
            }else {
                ParsePickUpTime(lines[6], order);
                ParseOrderNumber(lines[2], order);
                ParseContactNumber(lines[6], order);
            }

            Item item = null;
            string labelName = null;


            for (int i=start; i<lines.Count; i++) {

                //Once we encounter this line, we add the last item, break, and return the order
                if (lines[i] == "~ End of Order ~") {
                    //Items are only inserted to the order's itemlist if we detect a new item
                    //So at the end of the order add the last item
                    if (item != null) order.ItemList.Add(item);
                    break;
                }

                //If the line starts with "Please Label", we set the labelName for use in future items
                if (lines[i].StartsWith("Please label")) {  
                    
                    labelName = ParseLabelName(lines[i]);
                    continue;
                }

                //If the line starts with "1x" for example, then we need to parse the itemName and quantity
                Regex regex = new Regex(@"\d+x.", RegexOptions.ECMAScript); // the option makes it match only english chars
                if (regex.IsMatch(lines[i])) {

                    //Since we've detected a new item, we need to add the old item before initializing a new one
                    if (item != null) order.ItemList.Add(item);

                    item = new Item();

                    //Set the label name if it exists
                    if (labelName != null) {
                        item.LabelName = labelName;
                    }

                    ParseItemName(lines[i], item);
                    ParseQuantity(lines[i], item);
                    continue;
                }

                //If the line starts with '-', then this is a Addon or Special Instruction
                //Handle special instructions here because i needs to be incremented after
                if (lines[i].StartsWith("-")) {
                    if (lines[i].StartsWith("-Special")) {
                        ParseSpecialInstructions(lines[i + 1], item);
                        i++;
                    }else {
                        ParseAddOn(lines[i], item);
                    }
                    continue;
                }
                
                Debug.WriteLine("Unmatched: " + lines[i]);
            }

            //Set these once we know how many items are in the order
            SetItemCounts(order.ItemList);
            SetOrderSize(order);
            SetUniqueItemCount(order);

            return order;
        }

        /* Gets the special instructions, removing unprintable quotes */
        private void ParseSpecialInstructions(string line, Item item) {
            item.SpecialInstructions = line.Replace('“', '\"').Replace('”', '\"');
        }

        private void ParseAddOn(string line, Item item) {
            string[] words = line.Split(' ');

            switch (words[0]) {
                case "-Additional":
                    ParseToppings(words, item);
                    break;
                case "-Size":
                    ParseSize(words, item);
                    break;
                case "-Sugar":
                    ParseSugar(words, item);
                    break;
                case "-Ice":
                    ParseIce(words, item);
                    break;
                case "-Style":
                    ParseStyle(words, item);
                    break;
                case "-Flavor":
                    if (words[1] == "Choice") {
                        ParseFlavorChoice(words, item);
                    }else if (words[1] == "Addition") {
                        ParseFlavorAddition(words, item);
                    }
                    break;
                default:
                    Debug.WriteLine("Unidentified addon starter word: " + "-" + words[0] + "-");
                    break;
            }
        }

        /* Parses the topping from string[] in form:
         * "Additional", "Toppings", {Topping}, {Additional Price}
         * Change to use string builder
         */
        private void ParseToppings(string[] words, Item item) {
            string topping = "";
            int startInd = 2; //Skips "Additional" and "Toppings"
            for (int i=startInd; i<words.Length; i++) {

                //Stop creating the addon string if we've reached the price
                if (words[i].StartsWith("(")) break;

                topping = topping + words[i] + ' ';
            }
            topping = topping.Trim(); //get rid of extra space character at the end
            
            if (item.AddOnList == null) {
                item.AddOnList = new List<string>();
            }

            item.AddOnList.Add(topping);
        }

        /* Parses the size from string[] in form:
         * "Size", "Choice", {Size}
         */
        private void ParseSize(string[] words, Item item) {
            item.Size = words[2].Trim();
        }

        /* Parses the sugar level from string[] in form:
         * "Sugar", "Level", ({Num} '%')
         */
        private void ParseSugar(string[] words, Item item) {
            if (words.Length == 3) {
                item.SugarLevel = (words[2] == "Standard") ? words[2] : words[2] + " S";

            } else if (words.Length == 4) { //to account for old DD format - remove later
                item.SugarLevel = (words[3] == "Standard") ? words[3] : words[3] + " S";

            }
            //Debug.WriteLine(item.SugarLevel);
        }

        /* Parses the ice level from string[] in form:
         * "Ice", "Level", ({Num} '%')
         */
        private void ParseIce(string[] words, Item item) {
            if (words.Length == 3) {
                item.IceLevel = (words[2] == "Standard") ? words[2] : words[2] + " I";

            } else if (words.Length == 4) { //to acount for old DD format - remove later
                item.IceLevel = (words[3] == "Standard") ? words[3] : words[3] + " I";

            }else if (words.Length == 5) {
                item.IceLevel = words[2] + " I";   //new DD format - only 0% ice has 5 words
            }
            //Debug.WriteLine(item.IceLevel);
        }

        /* Parses the style choice from string[] in form
         * "Style", "Choice", {Cold|Hot|Garlic|Honey}
         */
        private void ParseStyle(string[] words, Item item) {

            switch(words[2]) {
                case "Hot":
                    item.Temperature = "Hot";
                    break;
                case "Cold":
                    //This handles the case that Hot drinks that select "Cold", do not print "Cold"
                    //because default options such as "Cold" are not printed
                    bool needToAddTemp = ( item.ItemName.Contains("Ginger") || item.ItemName.Contains("Hot") );
                    if (needToAddTemp) {
                        item.ItemName = item.ItemName + " (Cold)";
                    }
                    break;
                case "Garlic":
                    item.ItemName = "Garlic " + item.ItemName;
                    break;
                case "Honey":
                    item.ItemName = "Honey " + item.ItemName;
                    break;
                default:
                    Debug.WriteLine("Unidentified Style Choice: " + "-"+ words[2] + "-");
                    break;
            }
        }

        /* Parses the tea flavor choice from string[] in form:
         * "Flavor", "Choice", {Tea}
         * Used for flavored teas that choose tea base
         */
        private void ParseFlavorChoice(string[] words, Item item) {
            item.ItemName = item.ItemName.Replace("Tea", "");
            item.ItemName = item.ItemName + words[2] + " Tea";
        }

        /* Parses the the flavor addition from string[] in form:
         * "Flavor", "Addition", {Flavor}
         * Used for flavored Eggpuffs that choose flavor
         */
        private void ParseFlavorAddition(string[] words, Item item) {
            item.ItemName = item.ItemName + " -" + words[2];
        }

        /* Parses Customer Name from line in format: 
         * "Customer: {Name} Page {int} of {int}"
         */
        private void ParseCustomerName(string line, Order order) {
            int indOfWordPage = line.IndexOf("Page");
            int lenOfWordCust = "Customer: ".Length;
            int custNameLen = indOfWordPage - lenOfWordCust;

            string custName = line.Substring(lenOfWordCust, custNameLen);
            //Console.WriteLine("Customer Name: " + custName);
            order.CustomerName = custName;
        }

        /* Parses the pickup time from line:
         * Sample Format 1: "Today (Fri Aug 3), at 3:24 PM!" 
         * sample Format 2: "Friday Aug 3, at 7:47 PM!"
         */
        private void ParsePickUpTime(string line, Order order) {
            try {
                char[] delim = { ' ', ':' };
                string[] tokens = line.Split(delim);

                int startInd = 4;
                if (line.Contains("(")) {
                    startInd = 5;
                }

                int year  = DateTime.Now.Year;
                int month = Program.GetMonthNum(tokens[startInd]);
                int day   = Int32.Parse(tokens[startInd + 1].Replace(",", "").Replace(")", "")); //replaces ',' and ')'
                int hour  = Int32.Parse(tokens[startInd + 3]);
                int min   = Int32.Parse(tokens[startInd + 4]);

                //Convert hours to military
                if (tokens[startInd + 5] == "PM!") {
                    if (hour != 12)
                        hour += 12;
                }

                DateTime pickUpDate = new DateTime(year, month, day, hour, min, 0);
                //Debug.WriteLine("Parsed DateTime: " + pickUpDate.ToString());
                order.PickUpTime = pickUpDate;
            } catch(Exception e) {
                Debug.WriteLine("ParsePickUpTime: " + e.ToString());
            }
        }

        private void ParseOrderNumber(string line, Order order) {
            string orderNumber = line.Replace("Delivery #", "");

            order.OrderNumber = orderNumber;
        }

        /* Parses Contact Number from line in form:
         * "{Contact Number} at {time} {AM|PM}"
         */
        private void ParseContactNumber(string line, Order order) {
            int indOfWordAt = line.IndexOf(" at");

            string contactNumber = line.Remove(indOfWordAt);
            //Console.WriteLine("Contact Number: " + contactNumber);
            order.ContactNumber = contactNumber;
        }

        /* Returns the label name from line in form:
         * "Please label: {name} ({int} item)" 
         */
        private string ParseLabelName(string line) {
            string[] words = line.Split(' ');
            return words[2];
        }

        /* Parses item name from line in form:
         * "{int}x {Item Name} (in {Category}) ${Sub Price} ${Total}"
         */
        private void ParseItemName(string line, Item item) {
            Regex regex = new Regex(@"\d+x", RegexOptions.ECMAScript); 
            string itemName = regex.Replace(line, "", 1);
            int parenInd = itemName.IndexOf('(');
            itemName = itemName.Remove(parenInd).Trim();

            item.ItemName = itemName;
        }

        /* Parses quantity from line in form:
         * "{int}x {Item Name} (in {Category}) ${Sub Price} ${Total}"
         */
        private void ParseQuantity(string line, Item item) {
            Regex regex = new Regex(@"\d+x", RegexOptions.ECMAScript);
            Match match = regex.Match(line);
            string quantity = match.Value.Replace("x", "");
            item.Quantity = Int32.Parse(quantity);
        }

        /* Sets the itemCount for each item in itemList - does not account for quantity */
        private void SetItemCounts(List<Item> itemList) {
            for (int i=0; i<itemList.Count; i++) {
                itemList[i].ItemCount = (i + 1) + "/" + itemList.Count;
            }
        }

        /* Sets the Order size that accounts for quantity of duplicated items */
        public static void SetOrderSize(Order order) {
            int orderSize = 0;
            foreach (Item item in order.ItemList) {
                orderSize += item.Quantity;
            }
            order.OrderSize = orderSize;
        }

        /* Set the Unique Item Count of the order */
        public static void SetUniqueItemCount(Order order) {
            foreach (Item item in order.ItemList) {
                order.UniqueItemCount++;
            }
        }

        /* Saves the extracted pdf line by line to a file if it doesn't exist */
        private void PrintToFile(List<string> lines, string messageId) {

            string fileName = messageId + ".txt";
            string path = Path.Combine(Program.DoorDashDebugDir, fileName);

            if (File.Exists(path)) {
                return;
            }

            StreamWriter file = new StreamWriter(path);

            for (int i=0; i<lines.Count; i++) { 

                file.WriteLine( ("LINE " + i.ToString().PadLeft(3) + "  " + lines[i]) );
            }

            file.Close();
        }

    }

}

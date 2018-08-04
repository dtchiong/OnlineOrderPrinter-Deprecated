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

        const string PathToItextKeyLicense = @"C:\Users\Derek\Documents\dev\MICROSOFT_VISUAL_STUDIO\Order Parser\Order Parser\itextKeyLicense\itextkeylicense.xml";
        const string PathToDebugFolder = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\DoorDash Orders\debug-files";

        public static DoorDashMenu menu;

        //Constructor
        public DoorDashParser() {
            
            //Load the itext license
            try {
                LicenseKey.LoadLicenseFile(PathToItextKeyLicense);
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
            PrintToFile(lines, messageId);

            return lines;
        }

        /* Parses the information from the lines and returns the information in a Order object */
        public Order ParseOrder(List<string> lines, DateTime timeReceived) {

            Order order = new Order();
            order.Service = "DoorDash";
            order.TimeReceived = timeReceived;

            ParseCustomerName(lines[0], order);

            if (lines[3].StartsWith("Tea for you")) {
                
                ParsePickUpTime1(lines[1], order);
                ParseOrderNumber(lines[4], order);
                ParseContactNumber(lines[8], order);
            }else {
                ParsePickUpTime2(lines[6], order);
                ParseOrderNumber(lines[2], order);
                ParseContactNumber(lines[6], order);
            }

            Item item = null;
            string labelName = null;

            int start = 9;
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

                //If the line starts with "1x" for example, then we need to parse the item
                Regex regex = new Regex(@"\d+x.", RegexOptions.ECMAScript); // the option makes it match only english chars
                if (regex.IsMatch(lines[i])) {

                    Console.WriteLine("Matched: " + lines[i]);

                    //Console.WriteLine("starts with Please label: " + lines[i]);
                    //Since we've detected a new item, we need to add the old item before initializing a new one
                    if (item != null) order.ItemList.Add(item);

                    item = new Item();

                    //Set the label name if it exists
                    if (labelName != null) {
                        item.LabelName = labelName;
                    }

                    ParseItemName(lines[i], item);
                    continue;
                }

                //If the line starts with '-', then this is a Addon or Special Instruction
                if (lines[i].StartsWith("-")) {
                    //Console.WriteLine("Starts with -: " + lines[i]);
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
            return order;
        }

        private void ParseSpecialInstructions(string line, Item item) {
            item.SpecialInstructions = line;
        }

        private void ParseAddOn(string line, Item item) {
            string[] words = line.Split(' ');

            switch (words[0]) {
                case "-Topping":
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
                default:
                    Debug.WriteLine("Unidentified addon starter word: " + words[0]);
                    break;
            }
        }

        /* Parses the topping from string[] in form:
         * "Topping", "Additions", {Topping}, {Additional Price}
         * Change to use string builder
         */
        private void ParseToppings(string[] words, Item item) {
            string topping = "";
            int startInd = 2; //Skip "Toppings" and "Additions
            for (int i=startInd; i<words.Length; i++) {

                //Stop creating the addon string if we've reached the price
                if ( !words[i].StartsWith("(") ) {
                    topping = topping + words[i] + ' ';
                    break;
                }
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
            item.Size = words[2];
        }

        /* Parses the sugar level from string[] in form:
         * "Sugar", "Level", "Choice", ({Num} '%')
         */
        private void ParseSugar(string[] words, Item item) {
            item.IceLevel = words[3];
        }

        /* Parses the ice level from string[] in form:
         * "Ice", "Level", "Choice", ({Num} '%')
         */
        private void ParseIce(string[] words, Item item) {
            item.IceLevel = words[3];
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

        private void ParsePickUpTime1(string line, Order order) {
            string pickUpTime = line.Replace("Order scheduled for ", "");
            pickUpTime = pickUpTime.Remove(pickUpTime.Length - 1);

            Console.WriteLine("Pickup Time: " + pickUpTime);

        }

        private void ParsePickUpTime2(string line, Order order) {
            int indOfWordAt = line.IndexOf("at");

            string pickUpTime = line.Substring(indOfWordAt);
            Console.WriteLine("Pickup Time: " + pickUpTime);          
        }

        private void ParseOrderNumber(string line, Order order) {
            string orderNumber = line.Replace("Delivery #", "");

            //Console.WriteLine("Order Number: " + orderNumber);
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

        /* Saves the extracted pdf line by line to a file if it doesn't exist */
        private void PrintToFile(List<string> lines, string messageId) {

            string fileName = messageId + ".txt";
            string path = Path.Combine(PathToDebugFolder, fileName);

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

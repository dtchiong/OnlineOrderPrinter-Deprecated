using System;
using System.IO;
using System.Collections.Generic;

using HtmlAgilityPack;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.License;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

//I think this is how you cite the itext license

/* Copyright (c) 1998-2018 iText Group NV
    Authors: iText Software.
 * 
 * This program is free software; you can redistribute it and/or modify it under the 
terms of the GNU Affero General Public License version 3 as published by the Free 
Software Foundation with the addition of the following permission added to Section 15 
as permitted in Section 7(a): FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT 
IS OWNED BY ITEXT GROUP NV, ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT OF 
THIRD PARTY RIGHTS This program is distributed in the hope that it will be useful, but
 WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more details.
 You should have received a copy of the GNU Affero General Public License along with 
this program; if not, see http://www.gnu.org/licenses/ or write to the Free Software
 Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA, 02110-1301 USA, or 
download the license from the following URL: http://itextpdf.com/terms-of-use/
The interactive user interfaces in modified source and object code versions of this
 program must display Appropriate Legal Notices, as required under Section 5 of the 
GNU Affero General Public License. In accordance with Section 7(b) of the GNU Affero 
General Public License, you must retain the producer line in every PDF that is created
or manipulated using iText. You can be released from the requirements of the license 
by purchasing a commercial license. Buying such a license is mandatory as soon as you 
develop commercial activities involving the iText software without disclosing the 
source code of your own applications. These activities include: offering paid 
services to customers as an ASP, serving PDFs on the fly in a web application, shipping
iText with a closed source product. */

namespace OnlineOrderPrinter {

    public class DoorDashParser {

        public static DoorDashMenu menu;

        //Constructor
        public DoorDashParser() {

            menu = new DoorDashMenu();

            /*
            //Load the itext license (license is expired)
            try {
                LicenseKey.LoadLicenseFile(Program.iTextLicensePath);
            } catch (LicenseKeyException e) {
                Debug.WriteLine(e);
            }
            */
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

                    if (Program.DebugPrint) Debug.WriteLine(("LINE " + lineCount.ToString().PadLeft(3) + "  " + line));
                }
            }
            if (Program.DebugBuild) PrintToFile(lines, messageId);

            return lines;
        }

        /* Parses the confirmURL for DoorDash orders which is
         * located inside the href of the given <a> node. If parsing fails,
         * then we set the confirmURL to an error message
         * 
         * The parsedURL doesn't seem to be the actual URL to confirm the order
         * because when going to it, we get http code 301, which means that the url
         * is used for redirection. So we add "www." after "https://" which seems
         * to be the actual confirm URL.
         */
        public void ParseConfirmURL(Order order, string html) {
            try {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                HtmlNode aNode = htmlDoc.DocumentNode.SelectSingleNode("a");
                string parsedConfirmURL = aNode.Attributes["href"].Value;

                order.ConfirmURL = GetCorrectConfirmURL(parsedConfirmURL);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                order.ConfirmURL = "Failed to parse";
            }

        }

        /* Given the parsedURL found in the DoorDash email, we set the actual URL 
         * to confirm the order by adding "www." between https:// and the next part
         */
        private string GetCorrectConfirmURL(string parsedUrl) {
            int startIdx = "https://".Length;
            string apiURL = string.Concat("https://www.", parsedUrl.Substring(startIdx));
            return apiURL;
        }

        /* Parses the information from the lines and returns the information in a Order object */
        public Order ParseOrder(List<string> lines, DateTime timeReceived, string messageId) {

            Order order = new Order();
            order.Service = "DoorDash";
            order.TimeReceived = timeReceived;
            order.MessageId = messageId;

            ParseOrderNumber(lines[0], order);
            ParseCustomerName(lines[2], order);
            ParsePickUpTime(lines[2], order);
            ParseContactNumber(lines[3], order);
            
            int startOfOrderIndex = 5;

            Item item = null;
            string labelName = null;

            for (int i=startOfOrderIndex; i<lines.Count; i++) {

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
            SetDrinkAndSnackCount(order);

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
                case "-Ramen":
                    ParseRamenAddons(words, item);
                    break;
                case "-Rice":
                    ParseRiceDishAddons(words, item);
                    break;
                default:
                    Debug.WriteLine("Unidentified addon starter word: " + words[0] + "-");
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
            }else if (words.Length == 5) {
                item.IceLevel = words[2] + " I";   //new DD format - only 0% ice has 5 words
            }else if (words[2] == "More" && words[3] == "Ice") {
                item.IceLevel = "More Ice";
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
            item.ItemName = item.ItemName.Replace("Tea", (words[2] + " Tea"));
        }

        /* Parses the the flavor addition from string[] in form:
         * "Flavor", "Addition", {Flavor}
         * Used for flavored Eggpuffs that choose flavor
         */
        private void ParseFlavorAddition(string[] words, Item item) {
            item.ItemName = words[2] + " " + item.ItemName.Replace("(Flavored)", "");
            item.ItemName = item.ItemName.Trim();
        }

        /* Parses rice addons from string[] in form:
         * "Rice", "Dish", "Addons", {Addon} {Price}
         */
        private void ParseRiceDishAddons(string[] words, Item item) {
            int startIdx = 3;
            string addon = "";
            for (int i=startIdx; i<words.Length; i++) {
                if (words[i].StartsWith("(")) break;
                addon = addon + words[i] + " ";
            }
            if (item.AddOnList == null) item.AddOnList = new List<string>();
            item.AddOnList.Add(addon);
        }

        /* Parses ramen addons from string[] in form:
         * "Ramen", "Addons", {Addon} {Price}
         */
        private void ParseRamenAddons(string[] words, Item item) {
            int startIdx = 2;
            string addon = "";
            for (int i=startIdx; i<words.Length; i++) {
                if (words[i].StartsWith("(")) break;
                addon = addon + words[i] + " ";
            }
            if (item.AddOnList == null) item.AddOnList = new List<string>();
            item.AddOnList.Add(addon);
        }

        /* Parses Customer Name from line in format: 
         * "{FullName} {Date} PREPAID"
         */
        private void ParseCustomerName(string line, Order order) {
            string[] words = line.Split(' ');

            StringBuilder sb = new StringBuilder();
            sb.Append(words[0]);
            sb.Append(' ');
            sb.Append(words[1]);
            string custName = sb.ToString();
            //Debug.WriteLine("Customer Name: " + custName);
            order.CustomerName = custName;
        }

        /* Parses the pickup time from line:
         * Samples:
         * Rita Crum 2 items Wed Sep 12 at 5:26 PM
         */
        private void ParsePickUpTime(string line, Order order) {
            try {
                char[] delim = { ' ', ':' };

                string[] words = line.Split(delim);
               
                int year  = DateTime.Now.Year;
                int month = Program.GetMonthNum( words[words.Length - 6]);
                int day   = Int32.Parse(         words[words.Length - 5]);
                int hour  = Int32.Parse(         words[words.Length - 3]);
                int min   = Int32.Parse(         words[words.Length - 2]);

                //Convert hours to military
                bool isPM = words[words.Length - 1] == "PM";
                if (isPM) {
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

        /* Parses the Order Number from line in form:
         * "Order Number: {Order Number}"
         */
        private void ParseOrderNumber(string line, Order order) {
            string orderNumber = line.Split(' ')[2];

            order.OrderNumber = orderNumber;
        }

        /* Parses Contact Number from line in form:
         * Sample: "(415) 994-4429 at 10:05 PM"
         */
        private void ParseContactNumber(string line, Order order) {

            try {
                string contactNumber = Regex.Replace(line, @"\s+", " ");
                //Debug.WriteLine("Contact Number: " + contactNumber);
                order.ContactNumber = contactNumber;
            } catch (Exception e) {
                order.ContactNumber = "Error";
            }

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
         * Also sets the item type using the menu table.
         */
        private void ParseItemName(string line, Item item) {
            Regex regex = new Regex(@"\d+x", RegexOptions.ECMAScript); 
            string itemName = regex.Replace(line, "", 1);
            int parenInd = itemName.IndexOf('(');
            itemName = itemName.Remove(parenInd).Trim();

            item.ItemName = itemName;
            //Set item type
            string itemType = menu.GetItemType(itemName);
            if (itemType != null) {
                item.ItemType = itemType;
            }
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

        /* Sets the drink and snack count of the order */
        public static void SetDrinkAndSnackCount(Order order) {
            foreach (Item item in order.ItemList) {
                if (item.ItemType == "Drink") {
                    order.NumOfDrinks+= item.Quantity;
                }else if (item.ItemType == "Snack") {
                    order.NumOfSnacks+= item.Quantity;
                }
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

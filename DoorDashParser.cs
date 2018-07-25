using System;
using System.IO;
using System.Collections.Generic;

using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.License;
using System.Text.RegularExpressions;
using System.Text;

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

                    Console.WriteLine(("LINE " + lineCount.ToString().PadLeft(3) + "  " + line));
                }
            }
            PrintToFile(lines, messageId);

            return lines;
        }

        /* Parses the information from the lines and saves it to the order */
        public void ParseOrder(List<string> lines, DoorDashOrder order) {

            ParseCustomerName (lines[0], order);
            ParsePickUpTime   (lines[1], order);
            ParseOrderNumber  (lines[4], order);
            ParseContactNumber(lines[8], order);

            Item item = null;

            int start = 9;
            for (int i=start; i<lines.Count; i++) {


                //If the line starts with "Please Label", we need to parse the label name,
                //and the item name in the line that immediately follows the 1st line 
                if (lines[i].StartsWith("Please label")) {

                    Console.WriteLine("starts with Please label: " + lines[i]);

                    item = new Item();
                    
                    ParseLabelName(lines[i], item);
                    ParseItem(lines[i + 1], item);

                    order.ItemList.Add(item);
                    i++;
                    continue;
                }

                //If the line starts with "1x" for example, then we need to parse the item
                Regex regex = new Regex(@"\d+x.", RegexOptions.ECMAScript); // the option makes it match only english chars
                if (regex.IsMatch(lines[i])) {

                    Console.WriteLine("Matched: " + lines[i]);

                    item = new Item();

                    ParseItem(lines[i], item);
                    continue;
                }

                //If the line starts with '-', then this is a Topping or Special Instruction
                if (lines[i].StartsWith("-")) {
                    Console.WriteLine("Starts with -: " + lines[i]);
                    continue;
                }
                

                Console.WriteLine("Unmatched: " + lines[i]);

            }

        }

        private void ParseCustomerName(string line, DoorDashOrder order) {
            int indOfWordPage = line.IndexOf("Page");
            int lenOfWordCust = "Customer: ".Length;
            int custNameLen = indOfWordPage - lenOfWordCust;

            string custName = line.Substring(lenOfWordCust, custNameLen);
            Console.WriteLine("Customer Name: " + custName);
        }

        private void ParsePickUpTime(string line, DoorDashOrder order) {
            string pickUpTime = line.Replace("Order scheduled for ", "");
            pickUpTime = pickUpTime.Remove(pickUpTime.Length - 1);

            Console.WriteLine("Pickup Time: " + pickUpTime);

        }

        private void ParseOrderNumber(string line, DoorDashOrder order) {
            string orderNumber = line.Replace("Delivery #", "");

            Console.WriteLine("Order Number: " + orderNumber);
        }

        private void ParseContactNumber(string line, DoorDashOrder order) {
            int indOfWordAt = line.IndexOf(" at");

            string contactNumber = line.Remove(indOfWordAt);
            Console.WriteLine("Contact Number: " + contactNumber);
        }

        private void ParseLabelName(string line, Item item) {

        }

        private void ParseItem(string line, Item item) {

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

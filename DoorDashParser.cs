using System;
using System.IO;
using System.Collections.Generic;

using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.License;

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
                string[] pageLines = text.Split('\n');

                for (int j = 0; j < pageLines.Length; j++, lineCount++) {
                    lines.Add(pageLines[j]);

                    Console.OutputEncoding = System.Text.Encoding.UTF8; //still prints �
                    Console.WriteLine(("LINE " + lineCount.ToString().PadLeft(3) + "  " + pageLines[j]));
                }
            }
            PrintToFile(lines, messageId);

            return lines;
        }

        /* Parses the information from the lines and saves it to the order */
        public void ParseOrder(List<string> lines, DoorDashOrder order) {

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

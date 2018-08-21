using System;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Printer;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace GmailQuickstart {

    public class PrinterUtility {

        const string PrintTemplatePath = "E:T4FORM4.ZPL"; //E dir is the internal flash
        const int ArrayFieldOffset = 9; //the number of elements in the array before the 1st field of the template is represented
        const int TemplateFieldCount = 13; //the numnber of fields in the template

        public static Connection printerConn = null;
     
        /* Constrcutor - intializes the printer connection and sets language to ZPL */
        public PrinterUtility() {
            Debug.WriteLine("In Constructor");
            try {
                printerConn = FindConnection();
                printerConn.Open();
                SetPrintLangauge(printerConn);
                printerConn.Close();
                Debug.WriteLine("Construtor methods worked");
            } catch(Exception e) {
                Debug.WriteLine(e.ToString());
            }
        }

        /* Returns the connection to the zebra printer after finding it through the Usb Discoverer */
        public static Connection FindConnection() {
            Debug.WriteLine("START: FindConnection()");
            //Finds all the USB connected Zebra printer drivers
            List<DiscoveredPrinterDriver> discoveredPrinterDrivers = UsbDiscoverer.GetZebraDriverPrinters();
            if (discoveredPrinterDrivers == null) {
                Debug.WriteLine("Error: No USB printers detected");
                return null;
            }

            //Gets the instance to the Zebra Printer driver
            DiscoveredPrinterDriver printerDriver = discoveredPrinterDrivers[0];
            Debug.WriteLine(printerDriver);

            Debug.WriteLine("END: FindConnection()");
            //Get the connection to the printer driver
            return printerDriver.GetConnection();
        }

        /*        "E:T4FORM4.ZPL" Item Template  
         * --------------------------------------------       
         * Index|CharLimit|FieldName           Example
         *   1  |    8    |"Item Count"      |"100/100"
         *   2  |   10    |"Service"         |"GrubHub"
         *   3  |   20    |"Customer Name"   |
         *   4  |   15    |"Label Name"      |
         *   5  |   40    |"Item Name"       | 
         *   6  |    5    |"Size"            |"Large"
         *   7  |    3    |"Temperature"     |"Hot"
         *   8  |    6    |"Ice Level"       |"80% I"
         *   9  |    6    |"Sugar Level"     |"50% S"
         *   10 |   14    |"Milk Subsitution"|"Whole Milk Sub"
         *   11 |   40    |"Toppings"        |"Pearls, Pudding,"
         *   12 |   48    |"Special Instructions1"  
         *   13 |   48    |"Special Instructions2" 
         */
        public static bool PrintOrder(string[][] items) {

            bool printStatus = true;
            Debug.WriteLine("START: PrintOrder()");
            try {
                
                if (printerConn == null) {
                    //Get the connection to the printer driver
                    printerConn = FindConnection();
                }

                //We open the connection to printer, then get the instance of the printer
                try {
                    Debug.WriteLine("PrinterOrder() - before opening connection");
                    printerConn.Open();
                    ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, printerConn);
                    
                    Debug.WriteLine("PrinterOrder() - got printer instance");

                    for (int i=items.Length-1; i > -1; i--) {
                        printer.PrintStoredFormat(PrintTemplatePath, items[i]);
                    }

                }catch(ConnectionException e) {
                    Debug.WriteLine("PrinterOrder()-103- something messed up");
                    Debug.WriteLine(e.ToString());
                    printStatus = false;
                }finally {
                    printerConn.Close();
                }
              
            } catch (ConnectionException e) {
                Debug.WriteLine($"Error discovering local printers: {e.Message}");
                printStatus = false;
            }

            return printStatus;
        }

        /* Returns a 2 dimensional array, where each value of the array is another 
         * array that represents the fields of the label that will be printed out.
         * The length of this 2-dim array is the number of items in the given order.
         * Used for supplying fields to fill PrintStoredFormat's template
         */
        public static string[][] OrderToArray(Order order) {

            int itemArrSize = ArrayFieldOffset + TemplateFieldCount;
            string[][] orderArr = new string[order.OrderSize][];

            int count = 0; //used to set the count in the printed label

            //loop for only unique items in the order
            foreach (Item item in order.ItemList) { 

                //Create the string array that represents the current item. Item count isn't set yet
                string[] itemArr = FillItemArray(order, item, itemArrSize, count);
                
                //Then make enough duplicates to account for item.Quantity, set the item count, and add it to the orderArr
                for (int i=0; i<item.Quantity; i++) {

                    string[] tmp = new string[itemArrSize];
                    itemArr.CopyTo(tmp, 0);
                    tmp[ArrayFieldOffset] = (count + 1) + "/" + order.OrderSize;

                    orderArr[count] = tmp;
                    count++;
                }
            }
            return orderArr;
        }

        /* Fills the array with the fields of the item. The offset is required 
         * because that's where the values start when read by the PrintStoredFormat()
         */
        private static string[] FillItemArray(Order order, Item item, int length, int count) {

            string[] fields = new string[length];

            const int instructionsCharLim = 48;
            //Fill values within the offeset with empty strings since they aren't going to be used
            for (int i = 0; i < ArrayFieldOffset; i++) {
                fields[i] = "";
            }

            //Construct actual values for the indices corresponding to fields
            fields[ArrayFieldOffset]     = ""; //the item count will be set outside of this func
            fields[ArrayFieldOffset + 1] = order.Service;
            fields[ArrayFieldOffset + 2] = (order.CustomerName.Length > 20) ? order.CustomerName.Substring(0, 20) : order.CustomerName;
            fields[ArrayFieldOffset + 3] = (item.LabelName != null) ? item.LabelName : "";
            fields[ArrayFieldOffset + 4] = item.ItemName;
            fields[ArrayFieldOffset + 5] = (item.Size != "Regular") ? item.Size : "";
            fields[ArrayFieldOffset + 6] = (item.Temperature == "Hot") ? "Hot" : "";
            fields[ArrayFieldOffset + 7] = (item.IceLevel != "Standard") ? item.IceLevel : "";
            fields[ArrayFieldOffset + 8] = (item.SugarLevel != "Standard") ? item.SugarLevel : "";
            fields[ArrayFieldOffset + 9] = (item.MilkSubsitution != null) ? item.MilkSubsitution : "";

            string addOns = "";
            if (item.AddOnList != null) {
                foreach (string addOn in item.AddOnList) {
                    addOns = addOns + addOn + ", ";
                }
            }
            fields[ArrayFieldOffset + 10] = addOns;

            fields[ArrayFieldOffset + 11] = "";
            fields[ArrayFieldOffset + 12] = "";

            string instruction = item.SpecialInstructions;
            if (instruction != null) {
                if (instruction.Length > instructionsCharLim) {
                    fields[ArrayFieldOffset + 11] = instruction.Substring(0, instructionsCharLim);
                    fields[ArrayFieldOffset + 12] = instruction.Substring(instructionsCharLim);
                }else {
                    fields[ArrayFieldOffset + 11] = instruction;
                }
            } 

            return fields;
        }

        /* Sets the page description langauge to ZPL */
        private bool SetPrintLangauge(Connection conn) {
            Debug.WriteLine("START: Set PrintLanguage()");
            string pageDescriptionLanguage = "zpl";

            //Set the print langaugein the printer
            SGD.SET("device.languages", pageDescriptionLanguage, conn);

            //Get the print language in the printer to verify the printe was able to switch
            string s = SGD.GET("device.languages", conn);
            if (!s.Contains(pageDescriptionLanguage)) {
                Debug.WriteLine("Error: " + pageDescriptionLanguage + " not found- Not a ZPL Printer");
                return false;
            }
            Debug.WriteLine("END: PrintLanguage() - language set");
            return true;
        }

        /* Returns true if the printer is ready to print, else false 
         * Called before printing 
         */
        private bool CheckPrinterStatus(Connection conn) {
            ZebraPrinter printer = ZebraPrinterFactory.GetLinkOsPrinter(conn);
            if (printer == null) {
                printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, conn);
            }

            PrinterStatus printerStatus = printer.GetCurrentStatus();
            if (printerStatus.isReadyToPrint) {
                Debug.WriteLine("Printer Status: Ready to Print");
                return true;
            }else if (printerStatus.isPaused) {
                Debug.WriteLine("Printer Status: Cannot printer - printer is paused");
            }else if (printerStatus.isHeadOpen) {
                Debug.WriteLine("Printer Status: Cannot print - head is open");
            }else if (printerStatus.isPaperOut) {
                Debug.WriteLine("Printer Status: Cannot print - paper is out");
            }
            return false;
        }

        /* Called during and after printing to check printer status */
        private bool PostPrintCheckPrinterStatus(Connection conn) {
            ZebraPrinter printer = ZebraPrinterFactory.GetLinkOsPrinter(conn, PrinterLanguage.ZPL);
            if (printer == null) {
                printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, conn);
            }
            PrinterStatus printerStatus = printer.GetCurrentStatus();

            // loop while printing until print is complete or there is an error
            while ((printerStatus.numberOfFormatsInReceiveBuffer > 0) && (printerStatus.isReadyToPrint)) {
                Thread.Sleep(500);
                printerStatus = printer.GetCurrentStatus();
            }
            if (printerStatus.isReadyToPrint) {
                Debug.WriteLine("Printer Status: Ready to Print");
                return true;

            } else if (printerStatus.isPaused) {
                Debug.WriteLine("Printer Status: Cannot printer - printer is paused");
            } else if (printerStatus.isHeadOpen) {
                Debug.WriteLine("Printer Status: Cannot print - head is open");
            } else if (printerStatus.isPaperOut) {
                Debug.WriteLine("Printer Status: Cannot print - paper is out");
            } else {
                Debug.WriteLine("Printer Status: Cannot print");
            }
            return false;
        }
    }
}
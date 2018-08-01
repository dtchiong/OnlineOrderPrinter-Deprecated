using System;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Printer;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GmailQuickstart {

    public class PrinterUtility {

        const string PrintTemplatePath = "E:T4FORM3.ZPL";
        const int ArrayFieldOffset = 9; //the number of elements in the array before the 1st field of the template is represented
        const int TemplateFieldCount = 12; //the numnber of fields in the template

        public static Connection printerConn = null;

        public Queue<Order> printQ = new Queue<Order>();
     
        /* Constrcutor - intializes the printer connection and sets language to ZPL */
        public PrinterUtility() {
            Console.WriteLine("In Constructor");
            try {
                printerConn = FindConnection();
                printerConn.Open();
                SetPrintLangauge(printerConn);
                printerConn.Close();
                Console.WriteLine("Construtor methods worked");
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        /* Returns the connection to the zebra printer after finding it through the Usb Discoverer */
        public static Connection FindConnection() {
            Console.WriteLine("START: FindConnection()");
            //Finds all the USB connected Zebra printer drivers
            List<DiscoveredPrinterDriver> discoveredPrinterDrivers = UsbDiscoverer.GetZebraDriverPrinters();
            if (discoveredPrinterDrivers == null) {
                Console.WriteLine("Error: No USB printers detected");
                return null;
            }

            //Gets the instance to the Zebra Printer driver
            DiscoveredPrinterDriver printerDriver = discoveredPrinterDrivers[0];
            Console.WriteLine(printerDriver);

            Console.WriteLine("END: FindConnection()");
            //Get the connection to the printer driver
            return printerDriver.GetConnection();
        }

        /*        "E:T4FORM3.ZPL" Item Template  
         * --------------------------------------------       
         * Count|CharLimit|FieldName           Example
         *   1  |    8    |"Item Count"      |"100/100"
         *   2  |   10    |"Service"         |"GrubHub"
         *   3  |   20    |"Customer Name"   |  
         *   4  |   40    |"Item Name"       | 
         *   5  |    5    |"Size"            |"Large"
         *   6  |    3    |"Temperature"     |"Hot"
         *   7  |    6    |"Ice Level"       |"80% I"
         *   8  |    6    |"Sugar Level"     |"50% S"
         *   9  |   14    |"Milk Subsitution"|"Whole Milk Sub"
         *   10 |   40    |"Toppings"         "Pearls, Pudding,"
         *   11 |   48    |"Special Instructions1"  
         *   12 |   48    |"Special Instructions2" 
         */
        public static bool PrintOrder(string[][] items) {

            bool printStatus = true;
            Console.WriteLine("START: PrintOrder()");
            try {
                
                if (printerConn == null) {
                    //Get the connection to the printer driver
                    printerConn = FindConnection();
                }

                //We open the connection to printer, then get the instance of the printer
                try {
                    Console.WriteLine("PrinterOrder() - before opening connection");
                    printerConn.Open();
                    ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, printerConn);
                    
                    Console.WriteLine("PrinterOrder() - got printer instance");
                    for (int i=0; i<items.Length; i++) {
                        printer.PrintStoredFormat(PrintTemplatePath, items[i]);
                    }

                }catch(ConnectionException e) {
                    Console.WriteLine("PrinterOrder()-103- something messed up");
                    Console.WriteLine(e.ToString());
                    printStatus = false;
                }finally {
                    printerConn.Close();
                }
              
            } catch (ConnectionException e) {
                Console.WriteLine($"Error discovering local printers: {e.Message}");
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
            string[][] orderArr = new string[order.TotalItemCount][];

            List<Item> itemsList = order.ItemList;
            for (int i=0; i< orderArr.Length; i++) {

                //Initialize array for each item
                orderArr[i] = new string[itemArrSize]; //the array representing 1 item
                Item item   = itemsList[i];            //the item used to fill the array

                FillItemArray(orderArr[i], order, item);
            }
            return orderArr;
        }

        /* Fills the array with the fields of the item. The offset is required 
         * because that's where the values start when read by the PrintStoredFormat()
         */
        private static void FillItemArray(string[] fields, Order order, Item item) {
            const int instructionsCharLim = 48;
            //Fill values within the offeset with empty strings since they aren't going to be used
            for (int i = 0; i < ArrayFieldOffset; i++) {
                fields[i] = "";
            }

            //Construct actual values for the indices corresponding to fields
            fields[ArrayFieldOffset    ] = item.ItemCount;
            fields[ArrayFieldOffset + 1] = order.Service;
            fields[ArrayFieldOffset + 2] = order.CustomerName;
            fields[ArrayFieldOffset + 3] = item.ItemName;
            fields[ArrayFieldOffset + 4] = (item.Size == "Large") ? "Large" : "";
            fields[ArrayFieldOffset + 5] = (item.Temperature == "Hot") ? "Hot" : "";
            fields[ArrayFieldOffset + 6] = (item.IceLevel != "Standard") ? item.IceLevel : "";
            fields[ArrayFieldOffset + 7] = (item.SugarLevel != "Standard") ? item.SugarLevel : "";
            fields[ArrayFieldOffset + 8] = (item.MilkSubsitution != null) ? item.MilkSubsitution : "";

            string addOns = "";
            if (item.AddOnList != null) {
                foreach (string addOn in item.AddOnList) {
                    addOns = addOns + addOn + ", ";
                }
            }
            fields[ArrayFieldOffset + 9] = addOns;

            fields[ArrayFieldOffset + 10] = "";
            fields[ArrayFieldOffset + 11] = "";

            string instruction = item.SpecialInstructions;
            if (instruction != null && instruction.Length > instructionsCharLim) {
                fields[ArrayFieldOffset + 10] = instruction.Substring(0, instructionsCharLim);
                fields[ArrayFieldOffset + 11] = instruction.Substring(instructionsCharLim);
            }
        }

        /* Sets the page description langauge to ZPL */
        private bool SetPrintLangauge(Connection conn) {
            Console.WriteLine("START: Set PrintLanguage()");
            string pageDescriptionLanguage = "zpl";

            //Set the print langaugein the printer
            SGD.SET("device.languages", pageDescriptionLanguage, conn);

            //Get the print language in the printer to verify the printe was able to switch
            string s = SGD.GET("device.languages", conn);
            if (!s.Contains(pageDescriptionLanguage)) {
                Console.WriteLine("Error: " + pageDescriptionLanguage + " not found- Not a ZPL Printer");
                return false;
            }
            Console.WriteLine("END: PrintLanguage() - language set");
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
                Console.WriteLine("Printer Status: Ready to Print");
                return true;
            }else if (printerStatus.isPaused) {
                Console.WriteLine("Printer Status: Cannot printer - printer is paused");
            }else if (printerStatus.isHeadOpen) {
                Console.WriteLine("Printer Status: Cannot print - head is open");
            }else if (printerStatus.isPaperOut) {
                Console.WriteLine("Printer Status: Cannot print - paper is out");
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
                Console.WriteLine("Printer Status: Ready to Print");
                return true;

            } else if (printerStatus.isPaused) {
                Console.WriteLine("Printer Status: Cannot printer - printer is paused");
            } else if (printerStatus.isHeadOpen) {
                Console.WriteLine("Printer Status: Cannot print - head is open");
            } else if (printerStatus.isPaperOut) {
                Console.WriteLine("Printer Status: Cannot print - paper is out");
            } else {
                Console.WriteLine("Printer Status: Cannot print");
            }
            return false;
        }
    }
}
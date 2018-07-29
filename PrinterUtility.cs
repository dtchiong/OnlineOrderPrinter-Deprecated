using System;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Printer;
using System.Collections.Generic;
using System.Text;

namespace GmailQuickstart {

    public class PrinterUtility {

        const string printTemplatePath = "E:T4FORM3.ZPL";

        public PrinterUtility() {

        }

        /* Printing Item Template
         * Field1: "Item Count"       e.g "100/100"
         * Field2: "Service"          e.g "GrubHub"
         * Field3: "Customer Name"     
         * Field4: "Item Name"
         * Field5: "Size"             e.g "Large"
         * Field6: "Temperature"      e.g "Hot"
         * Field7: "Ice Level"        e.g "80% I"
         * Field8: "Sugar Level"      e.g "50% S"
         * Field9: "Milk Subsitution" e.g "Whole Milk Sub"
         * Field10: "Toppings"        e.g "Pearls, Pudding,"
         * Field11: "Special Instructions1" 
         * Field12: "Special Instructions2"
         */
        public void TestPrint() {
            try {

                //Finds all the USB connected Zebra printer drivers
                List<DiscoveredPrinterDriver> discoveredPrinterDrivers = UsbDiscoverer.GetZebraDriverPrinters();
                if (discoveredPrinterDrivers == null) {
                    Console.WriteLine("Error: No USB printers detected");
                    return;
                }

                //Gets the instance to the Zebra Printer driver
                DiscoveredPrinterDriver printerDriver = discoveredPrinterDrivers[0];
                Console.WriteLine(printerDriver);

                //Get the connection to the printer driver
                Connection printerConn = printerDriver.GetConnection();

                //We open the connection to printer, then get the instance of the printer
                try {
                    printerConn.Open();

                    ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, printerConn);
                    
                    printer.

                    //Printing works, but for some reason, the indexing needs to start at the 10th field
                    string[] fields = { "", "", "", "", "", "", "", "", "", "99/99", "GrubHub", "Dingus Honk", @"Fresh Mango Smoothie w/ Pearl", "Large", "", "", "50% S", "", "Fig Jelly, Pudding", "testing special instructions1", "testing special instructions2" };

                    printer.PrintStoredFormat(printTemplatePath, fields);

                }catch(ConnectionException e) {
                    Console.WriteLine(e.ToString());
                }finally {
                    printerConn.Close();
                }
              
            } catch (ConnectionException e) {
                Console.WriteLine($"Error discovering local printers: {e.Message}");
            }

            Console.WriteLine("Done discovering local printers.");
        }

        /* Sets the page description langauge to ZPL */
        private bool setPrintLangauge(Connection conn) {
            string pageDescriptionLanguage = "zpl";

            //Set the print langaugein the printer
            SGD.SET("device.languages", pageDescriptionLanguage, conn);

            //Get the print language in the printer to verify the printe was able to switch
            string s = SGD.GET("device.languages", conn);
            if (!s.Contains(pageDescriptionLanguage)) {
                Console.WriteLine("Error: " + pageDescriptionLanguage + " not found- Not a ZPL Printer");
                return false;
            }

            return true;
        }

        /* Returns true if the printer is ready to print, else false */
        private bool checkPrinterStatus(Connection conn) {
            ZebraPrinter printer = ZebraPrinterFactory.GetLinkOsPrinter(conn);
            if (printer == null) {
                printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, conn);
            }

            PrinterStatus printerStatus = printer.GetCurrentStatus();
            if (printerStatus.isReadyToPrint) {
                Console.WriteLine("Printer Status: Ready to Print");
                return true;
            }else if (printerStatus.isPaused) {
                Console.WriteLine("Printer Status: Cannot printe - printer is paused");
            }else if (printerStatus.isHeadOpen) {
                Console.WriteLine("Printer Status: Cannot print - head is open");
            }else if (printerStatus.isPaperOut) {
                Console.WriteLine("Printer Status: Cannot print - paper is out");
            }
            return false;
        }

        private bool postPrintCheckPrinterStatus(Connection conn) {
            ZebraPrinter printer = ZebraPrinterFactory.getLinkOsPrinter(conn, PrinterLanguage.ZPL);
            if (null == printer) {
                printer = ZebraPrinterFactory.getInstance(PrinterLanguage.ZPL, conn);
            }
            PrinterStatus printerStatus = printer.getCurrentStatus();

            // loop while printing until print is complete or there is an error
            while ((printerStatus.numberOfFormatsInReceiveBuffer > 0) && (printerStatus.isReadyToPrint)) {
                Thread.sleep(500);
                printerStatus = printer.getCurrentStatus();
            }
            if (printerStatus.isReadyToPrint) {
                System.out.println("Ready To Print");
                return true;
            } else if (printerStatus.isPaused) {
                System.out.println("Cannot Print because the printer is paused.");
            } else if (printerStatus.isHeadOpen) {
                System.out.println("Cannot Print because the printer head is open.");
            } else if (printerStatus.isPaperOut) {
                System.out.println("Cannot Print because the paper is out.");
            } else {
                System.out.println("Cannot Print.");
            }
            return false;
        }
    }
}
 
 
 
 
 
 
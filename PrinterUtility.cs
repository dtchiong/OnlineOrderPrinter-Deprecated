﻿using System;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Printer;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GmailQuickstart {

    public class PrinterUtility {

        const string PrintTemplatePath = "E:T4FORM3.ZPL";
        const int ArrayFieldOffset = 9; //the number of elements in the array before the 1st field of the template is represented
        const int TemplateFieldCount = 12; //the numnber of fields in the template

        public Connection printerConn = null;

        public Queue<Order> printQ = new Queue<Order>();

        /* Constrcutor - intializes the printer connection and sets language to ZPL */
        public PrinterUtility() {
            Console.WriteLine("In Constructor");
            try {
                printerConn = FindConnection();
                SetPrintLangauge(printerConn);
                Console.WriteLine("Construtor methods worked");
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        /* Returns the connection to the zebra printer after finding it through the Usb Discoverer */
        public Connection FindConnection() {
            //Finds all the USB connected Zebra printer drivers
            List<DiscoveredPrinterDriver> discoveredPrinterDrivers = UsbDiscoverer.GetZebraDriverPrinters();
            if (discoveredPrinterDrivers == null) {
                Console.WriteLine("Error: No USB printers detected");
                return null;
            }

            //Gets the instance to the Zebra Printer driver
            DiscoveredPrinterDriver printerDriver = discoveredPrinterDrivers[0];
            Console.WriteLine(printerDriver);

            //Get the connection to the printer driver
            return printerDriver.GetConnection();
        }

        public void AddToPrintQueue(Queue<Order> orderQ) {
            while(orderQ.Count > 0) {
                Order order = orderQ.Dequeue();
                printQ.Enqueue(order);
            }
        }

        public void PrintOrders() {
            while(printQ.Count > 0) {
                string[][] items = OrderToArray(printQ.Dequeue());
                PrintOrder(items);
            }
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
        public void PrintOrder(string[][] items) {
            Console.WriteLine("In PrintOrder()");
            try {
                
                if (printerConn == null) {
                    //Get the connection to the printer driver
                    printerConn = FindConnection();
                }

                //We open the connection to printer, then get the instance of the printer
                try {
                    printerConn.Open();
                    ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, printerConn);

                    Console.WriteLine("Printer Connection opened and got instance");
                    for (int i=0; i<items.Length; i++) {
                        printer.PrintStoredFormat(PrintTemplatePath, items[i]);
                    }

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

        /* Returns a 2 dimensional array, where each value of the array is another 
         * array that represents the fields of the label that will be printed out.
         * The length of this 2-dim array is the number of items in the given order.
         * Used for supplying fields to fill PrintStoredFormat's template
         * -- Need to refactor into 2 methods
         */
        private string[][] OrderToArray(Order order) {

            int itemArrSize = ArrayFieldOffset + TemplateFieldCount;
            string[][] orderArr = new string[order.TotalItemCount][];

            List<Item> itemsList = order.ItemList;
            for (int i=0; i< orderArr.Length; i++) {

                //Initialize array for each item
                orderArr[i] = new string[itemArrSize];
                //Fill values within the offeset with empty strings since they aren't going to be used
                for (int j = 0; j < ArrayFieldOffset; j++) {
                    orderArr[i][j] = "";
                }

                Item item = itemsList[i];
                //Construct actual values for the indices corresponding to fields
                orderArr[i][ArrayFieldOffset    ] = item.ItemCount;
                orderArr[i][ArrayFieldOffset + 1] = order.Service;
                orderArr[i][ArrayFieldOffset + 2] = order.CustomerName;
                orderArr[i][ArrayFieldOffset + 3] = item.ItemName;
                orderArr[i][ArrayFieldOffset + 4] = (item.Size == "Large") ? "Large" : "";
                orderArr[i][ArrayFieldOffset + 5] = (item.Temperature == "Hot") ? "Hot" : "";
                orderArr[i][ArrayFieldOffset + 6] = (item.IceLevel != "Standard") ? item.IceLevel : "";
                orderArr[i][ArrayFieldOffset + 7] = (item.SugarLevel != "Standard") ? item.SugarLevel : "";
                orderArr[i][ArrayFieldOffset + 8] = (item.MilkSubsitution != null) ? item.MilkSubsitution : "";

                string addOns = "";
                if (item.AddOnList != null) {
                    foreach (string addOn in item.AddOnList) {
                        addOns = addOns + addOn + ", ";
                    }
                }
                orderArr[i][ArrayFieldOffset + 9] = addOns;

                orderArr[i][ArrayFieldOffset + 10] = "";
                orderArr[i][ArrayFieldOffset + 11] = "";
                if (item.SpecialInstructions != null && item.SpecialInstructions.Length > 48) {
                    orderArr[i][ArrayFieldOffset + 10] = item.SpecialInstructions.Substring(0, 47);
                    orderArr[i][ArrayFieldOffset + 11] = item.SpecialInstructions.Substring(48);
                }
            }
            return orderArr;
        }

        /* Sets the page description langauge to ZPL */
        private bool SetPrintLangauge(Connection conn) {
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

        /* Returns true if the printer is ready to print, else false 
         * Called before printing 
         */
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
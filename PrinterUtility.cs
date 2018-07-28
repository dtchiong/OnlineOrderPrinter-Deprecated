using System;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Printer;
using System.Collections.Generic;

namespace GmailQuickstart {

    public class PrinterUtility {

        public PrinterUtility() {

        }

        public void TestPrint() {
            try {
                foreach (DiscoveredPrinterDriver printer in UsbDiscoverer.GetZebraDriverPrinters()) {
                    Console.WriteLine(printer);

                    /*
                    try {
                        Connection conn = printer.GetConnection();
                        Example1(conn);
                        //Example2(conn);
                        //Example3(conn);
                    }catch (ConnectionException e) {
                        Console.WriteLine(e.ToString());
                    }
                    */
                }

                foreach (DiscoveredUsbPrinter usbPrinter in UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter())) {
                    Console.WriteLine(usbPrinter);


                }
            } catch (ConnectionException e) {
                Console.WriteLine($"Error discovering local printers: {e.Message}");
            }

            Console.WriteLine("Done discovering local printers.");
        }


        /// Print a stored format with the given variables. This ZPL will store a format on a printer, for use with example1.
        /// 
        /// ^XA
        /// ^DFE:FORMAT1.ZPL
        /// ^FS
        /// ^FT26,243^A0N,56,55^FH\^FN12"First Name"^FS
        /// ^FT26,296^A0N,56,55^FH\^FN11"Last Name"^FS
        /// ^FT258,73^A0N,39,38^FH\^FDVisitor^FS
        /// ^BY2,4^FT403,376^B7N,4,0,2,2,N^FH^FDSerial Number^FS
        /// ^FO5,17^GB601,379,8^FS
        /// ^XZ
        private void Example1(Connection conn) {



            Connection connection = conn;
            try {
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);

                Dictionary<int, string> vars = new Dictionary<int, string> {
                { 12, "John" },
                { 11, "Smith" }
            };

                printer.PrintStoredFormat("E:FORMAT1.ZPL", vars);
            } catch (ConnectionException e) {
                Console.WriteLine(e.ToString());
            } catch (ZebraPrinterLanguageUnknownException e) {
                Console.WriteLine(e.ToString());
            } finally {
                connection.Close();
            }
        }
    }
}
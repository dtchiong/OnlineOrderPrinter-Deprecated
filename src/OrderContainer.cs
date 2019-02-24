using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderPrinter {
    /* The object that will be data-bound to the orderGridView */
    [DataObject]
    public class OrderContainer {

        /* Constructor */
        public OrderContainer(Order _order) {
            Order = _order;
            OrderArray = PrinterUtility.OrderToArray(Order);
            Status = "Active";
            PrintStatus = "Not Printed"; //maybe use Enum for print status later?
            PrintCount = 0;
        }

        public Order Order { get; set; }
        public string[][] OrderArray { get; set; }

        public string Status { get; set; }

        public string Service {
            get { return Order.Service; }
        }

        public string Name {
            get { return Order.CustomerName; }
        }

        public string ItemCount {
            get { return Order.OrderSize.ToString(); }
        }

        /* Returns the time if the order is from today, else it returns the date */
        public string TimeReceived {
            get {
                string dateNow = DateTime.Now.Date.ToString("d");
                string receivedDate = Order.TimeReceived.ToString("d");
                string receivedTime = Order.TimeReceived.ToString(@"hh\:mm tt");

                bool orderIsFromToday = dateNow == receivedDate;

                if (orderIsFromToday)
                    return receivedTime;
                return receivedDate;
            }
        }

        public string PickUpTime {
            get {
                string dateNow = DateTime.Now.Date.ToString("d");
                string pickUpDate = Order.PickUpTime.ToString("d");
                string pickUpTime = Order.PickUpTime.ToString(@"hh\:mm tt");

                bool orderIsFromToday = dateNow == pickUpDate;

                if (orderIsFromToday)
                    return pickUpTime;
                return pickUpDate;
            }
        }

        public long TimeReceivedTicks {
            get { return Order.TimeReceived.Ticks; }
        }

        public string PrintStatus { get; set; }

        public int PrintCount { get; set; }
    }

    /* A work around for binding a List<string> to a dataGridView.
     * dataGridView looks for properties of containing objects, so we have to wrap string in a class
     */
    public class StringWrapper {
        public StringWrapper(string s) {
            Value = s;
        }

        public string Value { get; set; }
    }
}

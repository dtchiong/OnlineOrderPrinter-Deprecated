using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GmailQuickstart {
    public partial class Form1 : Form {

        public static BindingSource orderListBindingSrc = new BindingSource();
        public static BindingSource orderInfoBindingSrc = new BindingSource();

        public static MySortableBindingList<OrderContainer> orderList = new MySortableBindingList<OrderContainer>();

        public Form1() {
            InitializeComponent();
            //Prevents columns from auto populating with OrderContainer fields. 
            //Need to set before setting datasource, or else columns get duplicated for some reason.
            dataGridView1.AutoGenerateColumns = false; 
            orderListBindingSrc.DataSource = orderList;
            dataGridView1.DataSource = orderListBindingSrc;

            dataGridView1.Columns.Add(NewTextBoxCol("Service", "Service"));
            dataGridView1.Columns.Add(NewTextBoxCol("Name", "Name"));
            dataGridView1.Columns.Add(NewTextBoxCol("ItemCount", "Item Count"));
            dataGridView1.Columns.Add(NewTextBoxCol("PrintStatus", "Print Status"));
            dataGridView1.Columns.Add(NewTextBoxCol("TimeReceived", "Time Received"));
            dataGridView1.Columns.Add(NewTextBoxCol("TimeReceivedTicks", "Ticks"));
            dataGridView1.Columns["Ticks"].Visible = false;
        }

        /* Returns a new DataGridViewColumn given the databinding propertyname, and the header name */
        DataGridViewColumn NewTextBoxCol(string propertyName, string headerName) {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = propertyName;
            col.Name = headerName;
            return col;
        }

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e) {

        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e) {

        }

        /* Gets the order that is currently selected, if any, and then prints it, and sets the Print Status */
        private void print_Click(object sender, EventArgs e) {
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            
            if (selectedRows.Count > 0) {
                OrderContainer orderCon = (OrderContainer)selectedRows[0].DataBoundItem;
                if (orderCon == null) return; //incase there are no orders

                bool printed = PrinterUtility.PrintOrder(orderCon.orderArray);
                if (printed) {
                    orderCon.PrintStatus = "Printed";
                }else {
                    orderCon.PrintStatus = "Error";
                }
            }
        }

        /* Encapsulates an order with an OrderContainer, then add it to the order list */
        public void AddOrderToList(Order order) {
            OrderContainer orderCon = new OrderContainer(order);
            orderList.Add(orderCon);           //Add the OrderContainer to the OrderList for tracking unprinted orders
            
        }

        /* The Form's load event calls the InitApp() to start checking and processing emails */
        private void Form1_Load(object sender, EventArgs e) {
            Program.InitApp();
        }

        /* Sort new rows by the TimeReceieved field of the Orders */
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            dataGridView1.Sort(dataGridView1.Columns["Ticks"], ListSortDirection.Descending);
        }
    }

    /* The object that will be data-bound to the orderGridView */
    [DataObject]
    public class OrderContainer {

        public Order order;
        public string[][] orderArray;

        /* Constructor */
        public OrderContainer(Order _order) {
            order = _order;
            orderArray = PrinterUtility.OrderToArray(order);
            PrintStatus = "Not Printed"; //maybe use Enum for print status later?
            //PrintCount = 0;
        }

        public string Service {
            get { return order.Service; }
        }

        public string Name {
            get { return order.CustomerName; }
        }

        public string ItemCount {
            get { return order.TotalItemCount.ToString(); }
        }

        /* Returns the time if the order is from today, else it returns the date */
        public string TimeReceived {
            get {     
                string dateNow = DateTime.Now.Date.ToString("d");
                string dateReceived = order.TimeReceived.ToString("d");
                string dateTime = order.TimeReceived.ToString(@"hh\:mm tt");

                bool orderIsFromToday = dateNow == dateReceived;

                if (orderIsFromToday) 
                    return dateTime;
                return dateReceived;    
            }
        }

        //public string PickUpTime {
        //    get { return null; }
        //}

        public long TimeReceivedTicks {
            get { return order.TimeReceived.Ticks; }
        }

        public string PrintStatus { get; set; }

        //public int PrintCount { get; set; }
    }
}

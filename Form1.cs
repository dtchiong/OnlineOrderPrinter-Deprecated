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

        public static MySortableBindingList<OrderContainer> OrderList = new MySortableBindingList<OrderContainer>();

        public Form1() {
            InitializeComponent();

            Text = "Derek's Online Order Printer v1.1.0";

            //Initialize dgv columns and properties
            //Prevents columns from auto populating with OrderContainer fields. 
            //Need to set before setting datasource, or else columns get duplicated for some reason.
            dataGridView1.AutoGenerateColumns = false;
            orderListBindingSrc.DataSource = OrderList;
            dataGridView1.DataSource = orderListBindingSrc;
            orderListBindingSrc.Sort = "TimeReceivedTicks DESC"; //set to sort DESC on the Ticks property

            dataGridView1.Columns.Add(NewTextBoxCol("Service", "Service"));
            dataGridView1.Columns.Add(NewTextBoxCol("Name", "Name"));
            dataGridView1.Columns.Add(NewTextBoxCol("ItemCount", "Order Size")); 
            dataGridView1.Columns.Add(NewTextBoxCol("TimeReceived", "Time Received"));
            dataGridView1.Columns.Add(NewTextBoxCol("PickUpTime", "Pick-Up Time"));
            dataGridView1.Columns.Add(NewTextBoxCol("PrintStatus", "Print Status"));

            dataGridView1.Columns.Add(NewTextBoxCol("TimeReceivedTicks", "TimeReceivedTicks"));
            dataGridView1.Columns["TimeReceivedTicks"].Visible = false;
        }

        /* Returns a new DataGridViewColumn given the databinding propertyname, and the header name */
        DataGridViewColumn NewTextBoxCol(string propertyName, string headerName) {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = propertyName;
            col.Name = headerName;
            return col;
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
                //To force the form to update the print status value. 
                //dataGridView1.Refresh() maybe useful if using a "Print All" button
                orderListBindingSrc.ResetCurrentItem();
            }
        }

        /* Encapsulates an order with an OrderContainer, then add it to the order list */
        public void AddOrderToList(Order order) {
            OrderContainer orderCon = new OrderContainer(order);
            OrderList.Add(orderCon); //Add the OrderContainer to the OrderList for tracking unprinted orders

            //Update the Item List in the GUI to match the selected row
            UpdateItemListDgv();
        }

        /* Updates the Item List in the UI to match the currently selected row */
        private void UpdateItemListDgv() {
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            OrderContainer selectedRow = (OrderContainer)selectedRows[0].DataBoundItem;
            dataGridView2.DataSource = selectedRow.order.ItemList;
        }

        /* The Form's load event calls the InitApp() to start checking and processing emails */
        private void Form1_Load(object sender, EventArgs e) {
            Program.InitApp();
        }

        /* Sort new rows by the TimeReceieved field of the Orders */
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            //OrderContainer orderCon = new OrderContainer(new Order());
            //PropertyDescriptor pd = TypeDescriptor.GetProperties(orderCon)["TimeReceivedTicks"];
            dataGridView1.Sort(dataGridView1.Columns["TimeReceivedTicks"], ListSortDirection.Descending);
        }

        /* Changes the backcolor when the is visited. Doesn't follow the row yet and only highlights
         * the row's location, so it's not sort proof
         */
        private void dataGridView1_SelectionChanged(object sender, EventArgs e) {
            
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count > 0) {
                //selectedRows[0].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                UpdateItemListDgv();
            }
            

            
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
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
            PrintCount = 0;
        }

        public string Service {
            get { return order.Service; }
        }

        public string Name {
            get { return order.CustomerName; }
        }

        public string ItemCount {
            get { return order.OrderSize.ToString(); }
        }

        /* Returns the time if the order is from today, else it returns the date */
        public string TimeReceived {
            get {     
                string dateNow = DateTime.Now.Date.ToString("d");
                string receivedDate = order.TimeReceived.ToString("d");
                string receivedTime = order.TimeReceived.ToString(@"hh\:mm tt");

                bool orderIsFromToday = dateNow == receivedDate;

                if (orderIsFromToday) 
                    return receivedTime;
                return receivedDate;    
            }
        }

        public string PickUpTime {
            get {
                string dateNow = DateTime.Now.Date.ToString("d");
                string pickUpDate = order.PickUpTime.ToString("d");
                string pickUpTime = order.PickUpTime.ToString(@"hh\:mm tt");

                bool orderIsFromToday = dateNow == pickUpDate;

                if (orderIsFromToday)
                    return pickUpTime;
                return pickUpDate;
            }
        }

        public long TimeReceivedTicks {
            get { return order.TimeReceived.Ticks; }
        }

        public string PrintStatus { get; set; }

        public int PrintCount { get; set; }
    }
}

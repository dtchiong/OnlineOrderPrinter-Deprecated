using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GmailQuickstart {
    public partial class Form1 : Form {

        public static BindingSource orderListBindingSrc = new BindingSource();
        public static BindingSource orderInfoBindingSrc = new BindingSource();
        public List<OrderContainer> orderList = new List<OrderContainer>();

        public Form1() {
            InitializeComponent();

            dataGridView1.DataSource = orderListBindingSrc;

            dataGridView1.AutoGenerateColumns = false; //Prevents columns from auto populating with OrderContainer fields
            dataGridView1.Columns.Add(NewTextBoxCol("Service", "Service"));
            dataGridView1.Columns.Add(NewTextBoxCol("Name", "Name"));
            dataGridView1.Columns.Add(NewTextBoxCol("ItemCount", "Item Count"));
            dataGridView1.Columns.Add(NewTextBoxCol("PrintStatus", "Print Status"));
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

        /* Takes the queue of parsed Orders, encases each order in a OrderContainer and then adds it to the list */
        public void AddAllOrdersToList(Queue<Order> orderQ) {
            while(orderQ.Count > 0) {
                OrderContainer orderCon = new OrderContainer(orderQ.Dequeue());
                orderListBindingSrc.Add(orderCon); //Add the OrderContainer to the binding source
                orderList.Add(orderCon);           //Add the OrderContainer to the OrderList for tracking unprinted orders
            }
        }

        public void AddOrderToList(Order order) {
            OrderContainer orderCon = new OrderContainer(order);
            orderListBindingSrc.Add(orderCon);
            orderList.Add(orderCon);
        }

        /* The Form's load event calls the InitApp() to start checking and processing emails */
        private void Form1_Load(object sender, EventArgs e) {
            Program.InitApp();
        }
    }

    /* The object that will be data-bound to the orderGridView */
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
            get { return order.TotalItemCount.ToString(); }
        }

        public string TimeReceived {
            get { return null; }
        }

        public string PickUpTime {
            get { return null; }
        }

        public string PrintStatus { get; set; }

        public int PrintCount { get; set; }
    }
}

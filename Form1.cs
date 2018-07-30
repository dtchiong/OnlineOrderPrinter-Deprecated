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

        public Form1() {
            InitializeComponent();
            dataGridView1.DataSource = orderListBindingSrc;

            dataGridView1.AutoGenerateColumns = false; //Prevents columns from auto populating with OrderContainer fields
            dataGridView1.Columns.Add(NewTextBoxCol("Service", "Service"));
            dataGridView1.Columns.Add(NewTextBoxCol("Name", "Name"));
            dataGridView1.Columns.Add(NewTextBoxCol("ItemCount", "Item Count"));
        }

        /* Returns a new DataGridViewColumn given the databinding propertyname, and the header name */
        DataGridViewColumn NewTextBoxCol(string propertyName, string headerName) {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = propertyName;
            col.Name = headerName;
            return col;
        }

        /* Adds an OrderContainer to the databinding list */
        public static void AddToOrderListSrc(OrderContainer orderContainer) {
            orderListBindingSrc.Add(orderContainer);
        }

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e) {

        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e) {

        }

        private void print_Click(object sender, EventArgs e) {
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            
            if (selectedRows.Count > 0) {
                OrderContainer orderCon = (OrderContainer)selectedRows[0].DataBoundItem;
                bool printed = PrinterUtility.PrintOrder(orderCon.orderArray);
                if (printed) {
                    orderCon.PrintStatus = "Printed";
                }else {
                    orderCon.PrintStatus = "Error";
                }
            }
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
        public string PrintStatus { get; set; }
        public int PrintCount { get; set; }

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


    }
}

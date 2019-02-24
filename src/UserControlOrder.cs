using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace OnlineOrderPrinter.src {
    public partial class UserControlOrder : UserControl {

        public string test = "4Head";
        public static BindingSource orderListBindingSrc = new BindingSource();
        public static MySortableBindingList<OrderContainer> OrderList = new MySortableBindingList<OrderContainer>();

        public UserControlOrder() {
            InitializeComponent();
            Debug.WriteLine("USER CONTROL");
            CreateDgvForOrders(); //creating dgv at runtime instead of designer to show example
        }

        /* Initialize dgv columns and properties
         * Prevents columns from auto populating with OrderContainer fields. Need to set autogenerate 
         * columns before setting datasource, or else columns get duplicated for some reason.
         */
        private void CreateDgvForOrders() {
            dataGridView1.AutoGenerateColumns = false;
            orderListBindingSrc.DataSource = OrderList;
            dataGridView1.DataSource = orderListBindingSrc;
            orderListBindingSrc.Sort = "TimeReceivedTicks DESC"; //set to sort DESC on the Ticks property

            dataGridView1.Columns.Add(NewTextBoxCol("Service", "Service"));
            dataGridView1.Columns.Add(NewTextBoxCol("Name", "Name"));
            dataGridView1.Columns.Add(NewTextBoxCol("ItemCount", "Order Size"));
            dataGridView1.Columns.Add(NewTextBoxCol("TimeReceived", "Time Received"));
            dataGridView1.Columns.Add(NewTextBoxCol("PickUpTime", "Pick-Up Time"));
            dataGridView1.Columns.Add(NewTextBoxCol("Status", "Order Status"));
            dataGridView1.Columns.Add(NewTextBoxCol("PrintStatus", "Print Status"));

            dataGridView1.Columns["Service"].FillWeight = 80;
            dataGridView1.Columns["Order Size"].FillWeight = 80;

            dataGridView1.Columns.Add(NewTextBoxCol("TimeReceivedTicks", "TIMERECEIVEDTICKS"));
            dataGridView1.Columns["TIMERECEIVEDTICKS"].Visible = false;
        }

        /* Returns a new DataGridViewColumn given the databinding propertyname, and the header name */
        DataGridViewColumn NewTextBoxCol(string propertyName, string headerName) {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = propertyName;
            col.Name = headerName;
            return col;
        }

        /* Gets the order that is currently selected, if any, and then prints it, and sets the Print Status 
         * To protect from spam, we disable the button on click, and then only enable the button after the 
         * print status is set. Maybe use a timer that starts afer print status is set.
         */
        public void HandlePrintButtonClick() {
            Form1 mainForm = (Form1)ParentForm;
            mainForm.SetPrintButtonEnabledStatus(false);
            mainForm.SetPrintButtonColor("#90979B");

            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            if (selectedRows.Count > 0) {
                OrderContainer orderCon = (OrderContainer)selectedRows[0].DataBoundItem;
                if (orderCon == null) return; //incase there are no orders

                bool printed = PrinterUtility.PrintOrder(orderCon);
                if (printed) {
                    orderCon.PrintStatus = "Printed";
                }//else error messages are handled in the print utility

                //To force the form to update the print status value. 
                //dataGridView1.Refresh() maybe useful if using a "Print All" button
                orderListBindingSrc.ResetCurrentItem();
            }

            mainForm.SetPrintButtonEnabledStatus(true);
            mainForm.SetPrintButtonColor("#4A5157");
        }

        /* Given the messageId, change the associated order's status to Ignore */
        public void ChangeStatusToAdjusted(string messageId) {
            OrderContainer orderCon;
            if (OrderTableByMsgId.TryGetValue(messageId, out orderCon)) {
                orderCon.Status = "Ignore(See adjusted)";
                //refresh the view so that the status in the UI is updated, 
                //would be more efficient if I had the current binded object to reset, instead of the entire view
                dataGridView1.Refresh();
            }
        }
    }
}

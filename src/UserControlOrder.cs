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
using System.Media;
using System.Net;

namespace OnlineOrderPrinter.src {

    public partial class UserControlOrder : UserControl {

        public static BindingSource orderListBindingSrc = new BindingSource();
        public static MySortableBindingList<OrderContainer> OrderList = new MySortableBindingList<OrderContainer>();

        //the dictionary of orders where the key is the messageId, used to track order status
        public static Dictionary<string, OrderContainer> OrderTableByMsgId = new Dictionary<string, OrderContainer>();
        //the dictionary of orders where the key is the orderNum, used to track grubhub cancelled orders
        public static Dictionary<string, List<OrderContainer>> OrderTableByOrdNum = new Dictionary<string, List<OrderContainer>>();

        public UserControlOrder() {
            InitializeComponent();
            CreateDgvForOrders(); //creating dgv at runtime instead of designer to have an example to show
        }

        /* Initialize dgv columns and properties
         * Prevents columns from auto populating with OrderContainer fields. Need to set autogenerate 
         * columns before setting datasource, or else columns get duplicated for some reason.
         */
        private void CreateDgvForOrders() {
            dataGridViewOrderList.AutoGenerateColumns = false;
            orderListBindingSrc.DataSource = OrderList;
            dataGridViewOrderList.DataSource = orderListBindingSrc;
            orderListBindingSrc.Sort = "TimeReceivedTicks DESC"; //set to sort DESC on the Ticks property

            dataGridViewOrderList.Columns.Add(NewTextBoxCol("Service", "Service"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("Name", "Name"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("ItemCount", "Order Size"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("TimeReceived", "Time Received"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("PickUpTime", "Pick-Up Time"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("Status", "Order Status"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("ConfirmStatus", "Confirm Status"));
            dataGridViewOrderList.Columns.Add(NewTextBoxCol("PrintStatus", "Print Status"));

            dataGridViewOrderList.Columns["Service"].FillWeight = 80;
            dataGridViewOrderList.Columns["Order Size"].FillWeight = 80;

            dataGridViewOrderList.Columns.Add(NewTextBoxCol("TimeReceivedTicks", "TIMERECEIVEDTICKS"));
            dataGridViewOrderList.Columns["TIMERECEIVEDTICKS"].Visible = false;
        }

        /* Returns a new DataGridViewColumn given the databinding propertyname, and the header name */
        DataGridViewColumn NewTextBoxCol(string propertyName, string headerName) {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = propertyName;
            col.Name = headerName;
            return col;
        }

        /* Sort new rows by the TimeReceieved field of the Orders */
        private void DataGridViewOrderList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            //OrderContainer orderCon = new OrderContainer(new Order());
            //PropertyDescriptor pd = TypeDescriptor.GetProperties(orderCon)["TimeReceivedTicks"];
            dataGridViewOrderList.Sort(dataGridViewOrderList.Columns["TimeReceivedTicks"], ListSortDirection.Descending);
        }

        /* Changes the backcolor when the is visited. Doesn't follow the row yet and only highlights
         * the row's location, so it's not sort proof
         */
        private void DataGridViewOrderList_SelectionChanged(object sender, EventArgs e) {

            DataGridViewSelectedRowCollection selectedRows = dataGridViewOrderList.SelectedRows;
            if (selectedRows.Count > 0) {
                //selectedRows[0].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                UpdateOrderUI();
            }
        }

        /* Updates the item details to match the currently selected item if the row changes */
        private void DataGridViewItemList_SelectionChanged(object sender, EventArgs e) {
            DataGridViewSelectedRowCollection selectedRows = dataGridViewItemList.SelectedRows;
            if (selectedRows.Count > 0) {
                UpdateItemDetailsUI();
            }
        }

        /* Called by form1's actual button handle function
         * Gets the order that is currently selected, if any, and then prints it, and sets the Print Status 
         * To protect from spam, we disable the button on click, and then only enable the button after the 
         * print status is set. Maybe use a timer that starts afer print status is set.
         */
        public void HandlePrintButtonClick() {
            Form1 mainForm = (Form1)ParentForm;
            mainForm.SetPrintButtonEnabledStatus(false);
            mainForm.SetPrintButtonColor("#90979B");

            DataGridViewSelectedRowCollection selectedRows = dataGridViewOrderList.SelectedRows;

            if (selectedRows.Count > 0) {
                OrderContainer orderCon = (OrderContainer)selectedRows[0].DataBoundItem;
                if (orderCon == null) return; //incase there are no orders

                bool printed = PrinterUtility.PrintOrder(orderCon);
                if (printed) {
                    orderCon.PrintStatus = "Printed";
                }//else error messages are handled in the print utility

                //To force the form to update the print status value. 
                //dataGridViewOrderList.Refresh() maybe useful if using a "Print All" button
                orderListBindingSrc.ResetCurrentItem();
            }

            mainForm.SetPrintButtonEnabledStatus(true);
            mainForm.SetPrintButtonColor("#4A5157");
        }

        /* Called by form1's wrapper function
         * Encapsulates an order with an OrderContainer, adds the order to the order list.
         * Then confirms the order
         */
        public void DoAddOrderToListAndConfirmOrder(Order order, bool isAdjustedOrder) {
            OrderContainer orderCon = new OrderContainer(order);
            if (isAdjustedOrder) orderCon.Status = "Active(Adjusted)";

            OrderList.Add(orderCon); //Add the OrderContainer to the OrderList to update the UI list

            //there seems to be a bug where an existing messageId tries to be inserted again 
            try {
                OrderTableByMsgId.Add(order.MessageId, orderCon); //Insert the entry into the table for easily updating order status
            } catch (Exception e) {
                Debug.WriteLine(order.MessageId + " already inserted in OrderTable. " + e.Message);
            }

            if (order.Service == "GrubHub") InsertToOrderNumTable(orderCon); //We insert the entry into the table with orderNum as the key

            //Update the Item List in the GUI to match the selected row 
            UpdateOrderUI();
            PlaySound(Program.NotificationSoundPath); //play the nofication sound
        }

        /* If the orderCon's orderNum already exists in the table, we add the orderCon to the list, else
         * we insert a new entry with the new orderCon inserted into the entry's list
         */
        public void InsertToOrderNumTable(OrderContainer orderCon) {
            List<OrderContainer> orderConList;
            if (OrderTableByOrdNum.TryGetValue(orderCon.Order.OrderNumber, out orderConList)) {
                orderConList.Add(orderCon);
            } else {
                orderConList = new List<OrderContainer>();
                orderConList.Add(orderCon);
                OrderTableByOrdNum.Add(orderCon.Order.OrderNumber, orderConList); //Insert the entry into the table for tracking grubhub cancelled orders
            }
        }

        /* Called by form1's wrapper function
         * Given the messageId, change the associated order's status to Ignore */
        public void DoChangeStatusToAdjusted(string messageId) {
            OrderContainer orderCon;
            if (OrderTableByMsgId.TryGetValue(messageId, out orderCon)) {
                orderCon.Status = "Ignore(See adjusted)";
                //refresh the view so that the status in the UI is updated, 
                //would be more efficient if I had the current binded object to reset, instead of the entire view
                dataGridViewOrderList.Refresh();
            }
        }

        /* Called by form1's wrapper function
         * Given the orderNum, set the associated orders' status to Cancelled */
        public void DoSetOrderToCancelled(string orderNum) {
            List<OrderContainer> orderConList;
            bool cancelledOrder = false;
            if (OrderTableByOrdNum.TryGetValue(orderNum, out orderConList)) {
                foreach (var orderCon in orderConList) {
                    cancelledOrder = true;
                    orderCon.Status = "Cancelled";
                    dataGridViewOrderList.Refresh();
                }
            }
            //this prevents from playing sound if no orders in the list were actually cancelled
            if (cancelledOrder) PlaySound(Program.CancelledOrderSoundPath);
        }

        //Plays the sound given the path of it
        private void PlaySound(string soundPath) {
            SoundPlayer sound = new SoundPlayer(soundPath);
            sound.Play();
        }

        /* Updates the UI to match the order in the selected row by
         * updating the Order Details and Item List
         */
        private void UpdateOrderUI() {
            DataGridViewSelectedRowCollection selectedRows = dataGridViewOrderList.SelectedRows;
            OrderContainer selectedRow = (OrderContainer)selectedRows[0].DataBoundItem;

            UpdateItemListUI(selectedRow);
            UpdateOrderDetailsUI(selectedRow);
            UpdateItemDetailsUI();
        }

        /* Updates the Item List in the UI to match the currently selected row */
        private void UpdateItemListUI(OrderContainer orderCon) {
            dataGridViewItemList.DataSource = orderCon.Order.ItemList;
            //dataGridViewItemList.DataSource = orderCon.Order.ItemList;

        }

        /* Updates the fields of Order Details */
        private void UpdateOrderDetailsUI(OrderContainer orderCon) {
            Order order = orderCon.Order;

            nameTextBox.Text = order.CustomerName;
            contactNumTextBox.Text = order.ContactNumber;
            orderNumTextBox.Text = order.OrderNumber;

            orderSizeTextBox.Text = order.OrderSize.ToString();
            if (order.NumOfDrinks > 0 && order.NumOfSnacks == 0) {
                orderSizeTextBox.Text += " (all Drinks)";
            } else if (order.NumOfSnacks > 0 && order.NumOfDrinks == 0) {
                orderSizeTextBox.Text += " (all Snacks)";
            } else if (order.NumOfDrinks > 0 && order.NumOfSnacks > 0) {
                orderSizeTextBox.Text += " (" + order.NumOfDrinks + " Drinks, " + order.NumOfSnacks + " Snacks)";
            }

            messageIdTextBox.Text = order.MessageId;
        }

        /* Updates the Item List UI to match the currently selected item in the Item List UI */
        private void UpdateItemDetailsUI() {

            //Get the currectly selected row's item
            DataGridViewSelectedRowCollection selectedRows = dataGridViewItemList.SelectedRows;
            Item item = (Item)selectedRows[0].DataBoundItem;

            textBoxQty.Text = item.Quantity.ToString();
            textBoxItemName.Text = item.ItemName;
            textBoxInstructions.Text = item.SpecialInstructions;

            List<StringWrapper> adjustmentList = new List<StringWrapper>();
            if (item.ItemType == "Drink") {
                adjustmentList.Add(new StringWrapper(item.Size));
                adjustmentList.Add(new StringWrapper(item.Temperature));
                adjustmentList.Add(new StringWrapper(item.IceLevel.Contains('%') ? item.IceLevel : item.IceLevel + " I"));
                adjustmentList.Add(new StringWrapper(item.SugarLevel.Contains('%') ? item.SugarLevel : item.SugarLevel + " S"));
                if (item.MilkSubsitution != null) adjustmentList.Add(new StringWrapper(item.MilkSubsitution));
            } else { //This is snack
                if (item.Size != "Regular") { //Handle the case where Oolong Tea eggs have size
                    adjustmentList.Add(new StringWrapper(item.Size));
                }
            }

            List<StringWrapper> addOnList = new List<StringWrapper>();
            if (item.AddOnList != null) {
                foreach (string s in item.AddOnList) {
                    addOnList.Add(new StringWrapper(s));
                }
            }

            dataGridViewAdjustments.DataSource = adjustmentList;
            dataGridViewToppings.DataSource = addOnList;
        }

        /* Initializes the callback function and calls the appropriate
         * function to confirm the order
         */
        private void ConfirmOrder(OrderContainer orderCon) {
            var cb = new Requests.ConfirmOrderCallBack(UpdateConfirmStatusUI);
            switch(orderCon.Service) {
                case "DoorDash":
                    Requests.ConfirmDoorDashOrder(orderCon, cb);
                    break;
                case "GrubHub":
                    Requests.ConfirmGrubHubOrder(orderCon, cb);
                    break;
                default:
                    Debug.WriteLine("Unhandled confirm for service: " + orderCon.Service);
                    break;
            }
        }

        /* The callback used by Request's Confirm order functions to update
         * the orderCon's confirm status in the associated row
         */
        private void UpdateConfirmStatusUI(OrderContainer orderCon, HttpStatusCode code) {
            switch(code) {
                case HttpStatusCode.OK:
                    orderCon.ConfirmStatus = "Confirmed";
                    break;
                default:
                    orderCon.ConfirmStatus = "Failed";
                    break;
            }
            dataGridViewOrderList.Refresh();
        }
    }
}

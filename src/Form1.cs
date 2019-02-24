using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineOrderPrinter {

    public partial class Form1 : Form {

        public static string CurrentForm = "Last Orders"; //there's a Form.ActiveForm

        public Form1() {
            InitializeComponent();
            LoadSideBarIcons();
        }

        /* This solves the weird bug with degrading quality of images in the imagelist 
         * over compiles by loading all the images from the resources at runtime
         */
        private void LoadSideBarIcons() {
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.Images.Clear(); //Clears the images in the designer

            Bitmap homeImage = new Bitmap(OnlineOrderPrinter.Properties.Resources.home_white2);
            Bitmap AnalyticsImage = new Bitmap(OnlineOrderPrinter.Properties.Resources.bar_chart_white);
            Bitmap MenusImage = new Bitmap(OnlineOrderPrinter.Properties.Resources.cutlery_white);
            Bitmap ActionsImage = new Bitmap(OnlineOrderPrinter.Properties.Resources.error_advice_sign);
            Bitmap SettingsImage = new Bitmap(OnlineOrderPrinter.Properties.Resources.settings_5_white);
            Bitmap AboutImage = new Bitmap(OnlineOrderPrinter.Properties.Resources.information);

            imageList1.Images.Add(homeImage);
            imageList1.Images.Add(AnalyticsImage);
            imageList1.Images.Add(MenusImage);
            imageList1.Images.Add(ActionsImage);
            imageList1.Images.Add(SettingsImage);
            imageList1.Images.Add(AboutImage);
        }

        /* Calls the userControlOrder's method to handle the printing because
         * the data required to print is only accessible within the userControl
         * while the printButton is located in the form rather than the userControl
         */
        private void print_Click(object sender, EventArgs e) {
            userControlOrder1.HandlePrintButtonClick();
        }

        /* Calls the userControlOrder's method to add the other to the list */
        public void AddOrderToList(Order order, bool isAdjustedOrder) {
            userControlOrder1.DoAddOrderToList(order, isAdjustedOrder);
        }

        /* Given the messageId, calls the userControlOrder to
         * set the associated order's status to Adjusted
         */
        public void ChangeStatusToAdjusted(string messageId) {
            userControlOrder1.DoChangeStatusToAdjusted(messageId);
        }

        /* Given the orderNum, calls the userControlOrder to 
         * set the associated order's status to Cancelled 
         */
        public void SetOrderToCancelled(string orderNum) {
            userControlOrder1.DoSetOrderToCancelled(orderNum);
        }

        private void Form1_Load(object sender, EventArgs e) {         
        }

        /* Only start checking and parsing emails when the form is shown */
        private void Form1_Shown(object sender, EventArgs e) {
            Program.InitApp();
        }

        /* Handles the click for each of the side bar buttons. 
         * Currently switches tabs to show a different control, but later 
         * we will show the Usercontrol corresponding to the button pressed, 
         * and set the color highlighting appropriately for each button
         */
        private void handleSideBarButtonClick(object sender, EventArgs e) {

            //Gets the current button's tag and returns if it's already the selected page
            string pressedTag = (string)((Button)sender).Tag;
       
            if (pressedTag == CurrentForm) {
                return;
            }

            List<Button> buttons = new List<Button>() {
                buttonOrdersTab,
                buttonSettingsTab,
                buttonAboutTab,
                buttonAnalyticsTab,
                buttonActionsTab,
                buttonMenusTab
            };

            //Iterate through the buttons to appropriately set the correct properties
            foreach (Button button in buttons) {

                string currentTag = (string)button.Tag;

                if (currentTag != pressedTag) {
                    button.BackColor = ColorTranslator.FromHtml("#2A2729");
                }else {
                    button.BackColor = ColorTranslator.FromHtml("#171516");
                    CurrentForm = currentTag;
                    labelTitle.Text = currentTag;

                    switchUserControl(CurrentForm);
                }
            }
        }

        /* TODO: Refactor to switch views using userControls later */ 
        private void switchUserControl(string CurrentForm) {
            switch(CurrentForm) {
                case "Last Orders":
                    userControlOrder1.Show();
                    printbutton.Enabled = true;
                    break;
                case "Analytics":
                case "Menus":
                case "Actions":
                case "Settings":
                case "About":
                    userControlOrder1.Hide();
                    printbutton.Enabled = false;
                    break;
            }
        }

        private void button_MouseEnter(object sender, EventArgs e) {
            Cursor = Cursors.Hand;
        }

        private void button_MouseLeave(object sender, EventArgs e) {
            Cursor = Cursors.Default;
        }

        /* Called by userControlOrder to change the enabled status of the print button */
        public void SetPrintButtonEnabledStatus(bool status) {
            printbutton.Enabled = status;
        }

        /* Called by userControlOrder to change the color of the print button */
        public void SetPrintButtonColor(string hexColor) {
            printbutton.BackColor = ColorTranslator.FromHtml(hexColor);
        }
    }


}

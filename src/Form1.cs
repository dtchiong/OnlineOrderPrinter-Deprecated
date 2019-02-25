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

        public static string CurrentControlName = "Last Orders"; //there's a Form.ActiveForm
        public UserControl CurrentShownControl = null; //Maintain this variable to more easily hide the shown form

        public Form1() {
            InitializeComponent();
            LoadSideBarIcons();
            CurrentShownControl = userControlOrder1; //Can only be initialized here for some reason
        }

        /* Only start checking and parsing emails when the form is shown */
        private void Form1_Shown(object sender, EventArgs e) {
            Program.InitApp();
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
        private void HandlePrintButton_Click(object sender, EventArgs e) {
            userControlOrder1.HandlePrintButtonClick();
        }

        private void PrintButton_MouseEnter(object sender, EventArgs e) {
            Cursor = Cursors.Hand;
        }

        private void PrintButton_MouseLeave(object sender, EventArgs e) {
            Cursor = Cursors.Default;
        }

        /* Called by the userControlOrder to change 
         * the enabled status of the print button 
         */
        public void SetPrintButtonEnabledStatus(bool status) {
            printbutton.Enabled = status;
        }

        /* Called by the userControlOrder to change
         * the color of the print button
         */
        public void SetPrintButtonColor(string hexColor) {
            printbutton.BackColor = ColorTranslator.FromHtml(hexColor);
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

        /* Handles the click for each of the side bar buttons. 
         * Currently switches tabs to show a different control, but later 
         * we will show the Usercontrol corresponding to the button pressed, 
         * and set the color highlighting appropriately for each button
         */
        private void HandleSideBarButtonClick(object sender, EventArgs e) {

            //Gets the current button's tag and returns if it's already the selected page
            string pressedTag = (string)((Button)sender).Tag;

            if (pressedTag == CurrentControlName) return;

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
                } else {
                    button.BackColor = ColorTranslator.FromHtml("#171516");
                    CurrentControlName = currentTag;
                    labelTitle.Text = currentTag;

                    SwitchUserControl(CurrentControlName);
                }
            }
        }

        /* Hides the old control and shows the new clicked control */
        private void SwitchUserControl(string currentFormLabel) {
            printbutton.Enabled = (currentFormLabel == "Last Orders") ? true : false;
            CurrentShownControl.Hide();
            switch (currentFormLabel) {
                case "Last Orders":
                    CurrentShownControl = userControlOrder1;
                    break;
                case "Analytics":
                    CurrentShownControl = userControlAnalytics1;
                    break;
                case "Menus":
                    CurrentShownControl = userControlMenus1;
                    break;
                case "Actions":
                    CurrentShownControl = userControlActions1;
                    break;
                case "Settings":
                    CurrentShownControl = userControlSettings1;
                    break;
                case "About":
                    CurrentShownControl = userControlAbout1;
                    break;
            }
            CurrentShownControl.Show();
        }
    }


}

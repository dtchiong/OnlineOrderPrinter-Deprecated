﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineOrderPrinter.src {
    public partial class UserControlActions : UserControl {
        public UserControlActions() {
            InitializeComponent();
        }

        private void UserControlActions_Load(object sender, EventArgs e) {
            Hide();
        }
    }
}

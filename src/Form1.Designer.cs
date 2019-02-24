namespace OnlineOrderPrinter {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelTabBar = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.splitContainerTabTopBound = new System.Windows.Forms.SplitContainer();
            this.panelBarTopSeperator = new System.Windows.Forms.Panel();
            this.splitContainerTabBotBoundary = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel_SideBar = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOrdersTab = new System.Windows.Forms.Button();
            this.buttonAboutTab = new System.Windows.Forms.Button();
            this.buttonAnalyticsTab = new System.Windows.Forms.Button();
            this.buttonMenusTab = new System.Windows.Forms.Button();
            this.buttonActionsTab = new System.Windows.Forms.Button();
            this.buttonSettingsTab = new System.Windows.Forms.Button();
            this.printbutton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelTitleBar = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.userControlOrder1 = new OnlineOrderPrinter.src.UserControlOrder();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stringValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tableLayoutPanelTabBar.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTabTopBound)).BeginInit();
            this.splitContainerTabTopBound.Panel1.SuspendLayout();
            this.splitContainerTabTopBound.Panel2.SuspendLayout();
            this.splitContainerTabTopBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTabBotBoundary)).BeginInit();
            this.splitContainerTabBotBoundary.Panel1.SuspendLayout();
            this.splitContainerTabBotBoundary.SuspendLayout();
            this.tableLayoutPanel_SideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringValueBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "home-white2.png");
            this.imageList1.Images.SetKeyName(1, "bar-chart-white.png");
            this.imageList1.Images.SetKeyName(2, "cutlery-white.png");
            this.imageList1.Images.SetKeyName(3, "error-advice-sign.png");
            this.imageList1.Images.SetKeyName(4, "settings-5-white.png");
            this.imageList1.Images.SetKeyName(5, "information.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "print-white.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(999, 626);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.splitContainer4.Panel1.Controls.Add(this.tableLayoutPanelTabBar);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.splitContainer4.Panel2.Controls.Add(this.printbutton);
            this.splitContainer4.Panel2.Padding = new System.Windows.Forms.Padding(15, 30, 15, 15);
            this.splitContainer4.Size = new System.Drawing.Size(150, 626);
            this.splitContainer4.SplitterDistance = 465;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // tableLayoutPanelTabBar
            // 
            this.tableLayoutPanelTabBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.tableLayoutPanelTabBar.ColumnCount = 1;
            this.tableLayoutPanelTabBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTabBar.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanelTabBar.Controls.Add(this.splitContainerTabTopBound, 0, 1);
            this.tableLayoutPanelTabBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTabBar.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTabBar.Name = "tableLayoutPanelTabBar";
            this.tableLayoutPanelTabBar.RowCount = 2;
            this.tableLayoutPanelTabBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.875F));
            this.tableLayoutPanelTabBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.125F));
            this.tableLayoutPanelTabBar.Size = new System.Drawing.Size(150, 465);
            this.tableLayoutPanelTabBar.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.15789F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.94737F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.38322F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(144, 95);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "T4 Milpitas";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "Store";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainerTabTopBound
            // 
            this.splitContainerTabTopBound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTabTopBound.IsSplitterFixed = true;
            this.splitContainerTabTopBound.Location = new System.Drawing.Point(3, 104);
            this.splitContainerTabTopBound.Name = "splitContainerTabTopBound";
            this.splitContainerTabTopBound.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTabTopBound.Panel1
            // 
            this.splitContainerTabTopBound.Panel1.Controls.Add(this.panelBarTopSeperator);
            // 
            // splitContainerTabTopBound.Panel2
            // 
            this.splitContainerTabTopBound.Panel2.Controls.Add(this.splitContainerTabBotBoundary);
            this.splitContainerTabTopBound.Size = new System.Drawing.Size(144, 358);
            this.splitContainerTabTopBound.SplitterDistance = 25;
            this.splitContainerTabTopBound.TabIndex = 2;
            this.splitContainerTabTopBound.TabStop = false;
            // 
            // panelBarTopSeperator
            // 
            this.panelBarTopSeperator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panelBarTopSeperator.CausesValidation = false;
            this.panelBarTopSeperator.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarTopSeperator.Location = new System.Drawing.Point(0, 0);
            this.panelBarTopSeperator.Margin = new System.Windows.Forms.Padding(0);
            this.panelBarTopSeperator.MaximumSize = new System.Drawing.Size(0, 3);
            this.panelBarTopSeperator.Name = "panelBarTopSeperator";
            this.panelBarTopSeperator.Size = new System.Drawing.Size(144, 3);
            this.panelBarTopSeperator.TabIndex = 0;
            this.panelBarTopSeperator.Visible = false;
            // 
            // splitContainerTabBotBoundary
            // 
            this.splitContainerTabBotBoundary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTabBotBoundary.IsSplitterFixed = true;
            this.splitContainerTabBotBoundary.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTabBotBoundary.Name = "splitContainerTabBotBoundary";
            this.splitContainerTabBotBoundary.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTabBotBoundary.Panel1
            // 
            this.splitContainerTabBotBoundary.Panel1.Controls.Add(this.tableLayoutPanel_SideBar);
            this.splitContainerTabBotBoundary.Size = new System.Drawing.Size(144, 329);
            this.splitContainerTabBotBoundary.SplitterDistance = 296;
            this.splitContainerTabBotBoundary.TabIndex = 0;
            this.splitContainerTabBotBoundary.TabStop = false;
            // 
            // tableLayoutPanel_SideBar
            // 
            this.tableLayoutPanel_SideBar.ColumnCount = 1;
            this.tableLayoutPanel_SideBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_SideBar.Controls.Add(this.buttonOrdersTab, 0, 0);
            this.tableLayoutPanel_SideBar.Controls.Add(this.buttonAboutTab, 0, 5);
            this.tableLayoutPanel_SideBar.Controls.Add(this.buttonAnalyticsTab, 0, 1);
            this.tableLayoutPanel_SideBar.Controls.Add(this.buttonMenusTab, 0, 2);
            this.tableLayoutPanel_SideBar.Controls.Add(this.buttonActionsTab, 0, 3);
            this.tableLayoutPanel_SideBar.Controls.Add(this.buttonSettingsTab, 0, 4);
            this.tableLayoutPanel_SideBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_SideBar.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_SideBar.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_SideBar.Name = "tableLayoutPanel_SideBar";
            this.tableLayoutPanel_SideBar.RowCount = 6;
            this.tableLayoutPanel_SideBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_SideBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_SideBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_SideBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_SideBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_SideBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel_SideBar.Size = new System.Drawing.Size(144, 296);
            this.tableLayoutPanel_SideBar.TabIndex = 0;
            // 
            // buttonOrdersTab
            // 
            this.buttonOrdersTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(22)))));
            this.buttonOrdersTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOrdersTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(22)))));
            this.buttonOrdersTab.FlatAppearance.BorderSize = 0;
            this.buttonOrdersTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOrdersTab.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOrdersTab.ForeColor = System.Drawing.Color.White;
            this.buttonOrdersTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOrdersTab.ImageIndex = 0;
            this.buttonOrdersTab.ImageList = this.imageList1;
            this.buttonOrdersTab.Location = new System.Drawing.Point(0, 0);
            this.buttonOrdersTab.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOrdersTab.Name = "buttonOrdersTab";
            this.buttonOrdersTab.Size = new System.Drawing.Size(144, 49);
            this.buttonOrdersTab.TabIndex = 0;
            this.buttonOrdersTab.TabStop = false;
            this.buttonOrdersTab.Tag = "Last Orders";
            this.buttonOrdersTab.Text = "  Orders";
            this.buttonOrdersTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOrdersTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOrdersTab.UseVisualStyleBackColor = true;
            this.buttonOrdersTab.Click += new System.EventHandler(this.handleSideBarButtonClick);
            this.buttonOrdersTab.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.buttonOrdersTab.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // buttonAboutTab
            // 
            this.buttonAboutTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAboutTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonAboutTab.FlatAppearance.BorderSize = 0;
            this.buttonAboutTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAboutTab.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAboutTab.ForeColor = System.Drawing.Color.Transparent;
            this.buttonAboutTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAboutTab.ImageIndex = 5;
            this.buttonAboutTab.ImageList = this.imageList1;
            this.buttonAboutTab.Location = new System.Drawing.Point(0, 245);
            this.buttonAboutTab.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAboutTab.Name = "buttonAboutTab";
            this.buttonAboutTab.Size = new System.Drawing.Size(144, 51);
            this.buttonAboutTab.TabIndex = 2;
            this.buttonAboutTab.TabStop = false;
            this.buttonAboutTab.Tag = "About";
            this.buttonAboutTab.Text = "  About";
            this.buttonAboutTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAboutTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAboutTab.UseVisualStyleBackColor = true;
            this.buttonAboutTab.Click += new System.EventHandler(this.handleSideBarButtonClick);
            this.buttonAboutTab.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.buttonAboutTab.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // buttonAnalyticsTab
            // 
            this.buttonAnalyticsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAnalyticsTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonAnalyticsTab.FlatAppearance.BorderSize = 0;
            this.buttonAnalyticsTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnalyticsTab.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAnalyticsTab.ForeColor = System.Drawing.Color.White;
            this.buttonAnalyticsTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAnalyticsTab.ImageIndex = 1;
            this.buttonAnalyticsTab.ImageList = this.imageList1;
            this.buttonAnalyticsTab.Location = new System.Drawing.Point(0, 49);
            this.buttonAnalyticsTab.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAnalyticsTab.Name = "buttonAnalyticsTab";
            this.buttonAnalyticsTab.Size = new System.Drawing.Size(144, 49);
            this.buttonAnalyticsTab.TabIndex = 4;
            this.buttonAnalyticsTab.TabStop = false;
            this.buttonAnalyticsTab.Tag = "Analytics";
            this.buttonAnalyticsTab.Text = "  Analytics";
            this.buttonAnalyticsTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAnalyticsTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAnalyticsTab.UseVisualStyleBackColor = true;
            this.buttonAnalyticsTab.Click += new System.EventHandler(this.handleSideBarButtonClick);
            this.buttonAnalyticsTab.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.buttonAnalyticsTab.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // buttonMenusTab
            // 
            this.buttonMenusTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMenusTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonMenusTab.FlatAppearance.BorderSize = 0;
            this.buttonMenusTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenusTab.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenusTab.ForeColor = System.Drawing.Color.White;
            this.buttonMenusTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenusTab.ImageIndex = 2;
            this.buttonMenusTab.ImageList = this.imageList1;
            this.buttonMenusTab.Location = new System.Drawing.Point(0, 98);
            this.buttonMenusTab.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMenusTab.Name = "buttonMenusTab";
            this.buttonMenusTab.Size = new System.Drawing.Size(144, 49);
            this.buttonMenusTab.TabIndex = 3;
            this.buttonMenusTab.TabStop = false;
            this.buttonMenusTab.Tag = "Menus";
            this.buttonMenusTab.Text = "  Menus";
            this.buttonMenusTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenusTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMenusTab.UseVisualStyleBackColor = true;
            this.buttonMenusTab.Click += new System.EventHandler(this.handleSideBarButtonClick);
            this.buttonMenusTab.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.buttonMenusTab.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // buttonActionsTab
            // 
            this.buttonActionsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonActionsTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonActionsTab.FlatAppearance.BorderSize = 0;
            this.buttonActionsTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActionsTab.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonActionsTab.ForeColor = System.Drawing.Color.White;
            this.buttonActionsTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonActionsTab.ImageIndex = 3;
            this.buttonActionsTab.ImageList = this.imageList1;
            this.buttonActionsTab.Location = new System.Drawing.Point(0, 147);
            this.buttonActionsTab.Margin = new System.Windows.Forms.Padding(0);
            this.buttonActionsTab.Name = "buttonActionsTab";
            this.buttonActionsTab.Size = new System.Drawing.Size(144, 49);
            this.buttonActionsTab.TabIndex = 5;
            this.buttonActionsTab.TabStop = false;
            this.buttonActionsTab.Tag = "Actions";
            this.buttonActionsTab.Text = "  Actions";
            this.buttonActionsTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonActionsTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonActionsTab.UseVisualStyleBackColor = true;
            this.buttonActionsTab.Click += new System.EventHandler(this.handleSideBarButtonClick);
            this.buttonActionsTab.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.buttonActionsTab.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // buttonSettingsTab
            // 
            this.buttonSettingsTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSettingsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSettingsTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonSettingsTab.FlatAppearance.BorderSize = 0;
            this.buttonSettingsTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettingsTab.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettingsTab.ForeColor = System.Drawing.Color.White;
            this.buttonSettingsTab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettingsTab.ImageIndex = 4;
            this.buttonSettingsTab.ImageList = this.imageList1;
            this.buttonSettingsTab.Location = new System.Drawing.Point(0, 196);
            this.buttonSettingsTab.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSettingsTab.Name = "buttonSettingsTab";
            this.buttonSettingsTab.Size = new System.Drawing.Size(144, 49);
            this.buttonSettingsTab.TabIndex = 1;
            this.buttonSettingsTab.TabStop = false;
            this.buttonSettingsTab.Tag = "Settings";
            this.buttonSettingsTab.Text = "  Settings";
            this.buttonSettingsTab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSettingsTab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSettingsTab.UseVisualStyleBackColor = true;
            this.buttonSettingsTab.Click += new System.EventHandler(this.handleSideBarButtonClick);
            this.buttonSettingsTab.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.buttonSettingsTab.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // printbutton
            // 
            this.printbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(81)))), ((int)(((byte)(87)))));
            this.printbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.printbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printbutton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printbutton.FlatAppearance.BorderSize = 0;
            this.printbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printbutton.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printbutton.ImageIndex = 0;
            this.printbutton.ImageList = this.imageList2;
            this.printbutton.Location = new System.Drawing.Point(15, 30);
            this.printbutton.Margin = new System.Windows.Forms.Padding(0);
            this.printbutton.Name = "printbutton";
            this.printbutton.Size = new System.Drawing.Size(120, 112);
            this.printbutton.TabIndex = 0;
            this.printbutton.TabStop = false;
            this.printbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.printbutton.UseVisualStyleBackColor = false;
            this.printbutton.Click += new System.EventHandler(this.print_Click);
            this.printbutton.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.printbutton.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanelTitleBar);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.splitContainer2.Size = new System.Drawing.Size(845, 626);
            this.splitContainer2.SplitterDistance = 45;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanelTitleBar
            // 
            this.tableLayoutPanelTitleBar.ColumnCount = 2;
            this.tableLayoutPanelTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanelTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanelTitleBar.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanelTitleBar.Controls.Add(this.userControlOrder1, 1, 0);
            this.tableLayoutPanelTitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelTitleBar.Name = "tableLayoutPanelTitleBar";
            this.tableLayoutPanelTitleBar.RowCount = 1;
            this.tableLayoutPanelTitleBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitleBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelTitleBar.Size = new System.Drawing.Size(845, 45);
            this.tableLayoutPanelTitleBar.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(22)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.labelTitle.Size = new System.Drawing.Size(241, 45);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Last Orders";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userControlOrder1
            // 
            this.userControlOrder1.Location = new System.Drawing.Point(244, 3);
            this.userControlOrder1.Name = "userControlOrder1";
            this.userControlOrder1.Size = new System.Drawing.Size(92, 39);
            this.userControlOrder1.TabIndex = 1;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(OnlineOrderPrinter.Item);
            // 
            // stringValueBindingSource
            // 
            this.stringValueBindingSource.DataSource = typeof(OnlineOrderPrinter.StringWrapper);
            // 
            // itemBindingSource1
            // 
            this.itemBindingSource1.DataSource = typeof(OnlineOrderPrinter.Item);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 626);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tableLayoutPanelTabBar.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainerTabTopBound.Panel1.ResumeLayout(false);
            this.splitContainerTabTopBound.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTabTopBound)).EndInit();
            this.splitContainerTabTopBound.ResumeLayout(false);
            this.splitContainerTabBotBoundary.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTabBotBoundary)).EndInit();
            this.splitContainerTabBotBoundary.ResumeLayout(false);
            this.tableLayoutPanel_SideBar.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanelTitleBar.ResumeLayout(false);
            this.tableLayoutPanelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringValueBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button printbutton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.BindingSource itemBindingSource1;
        private System.Windows.Forms.BindingSource stringValueBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTitleBar;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTabBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer splitContainerTabTopBound;
        private System.Windows.Forms.SplitContainer splitContainerTabBotBoundary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_SideBar;
        private System.Windows.Forms.Button buttonActionsTab;
        private System.Windows.Forms.Button buttonAnalyticsTab;
        private System.Windows.Forms.Button buttonMenusTab;
        private System.Windows.Forms.Button buttonAboutTab;
        private System.Windows.Forms.Button buttonSettingsTab;
        private System.Windows.Forms.Button buttonOrdersTab;
        private System.Windows.Forms.Panel panelBarTopSeperator;
        private src.UserControlOrder userControlOrder1;
    }
}
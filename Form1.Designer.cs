namespace GmailQuickstart {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelItemDetails = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxQty = new System.Windows.Forms.TextBox();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.listBoxAdjustments = new System.Windows.Forms.ListBox();
            this.listBoxToppings = new System.Windows.Forms.ListBox();
            this.textBoxInstructions = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelOrderDetails = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.contactNumTextBox = new System.Windows.Forms.TextBox();
            this.orderNumTextBox = new System.Windows.Forms.TextBox();
            this.orderSizeTextBox = new System.Windows.Forms.TextBox();
            this.messageIdTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControlAppTabs = new System.Windows.Forms.TabControl();
            this.tabHome = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.labelOrderDetails = new System.Windows.Forms.Label();
            this.labelItemList = new System.Windows.Forms.Label();
            this.labelItemDetails = new System.Windows.Forms.Label();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanelItemDetails.SuspendLayout();
            this.tableLayoutPanelOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControlAppTabs.SuspendLayout();
            this.tabHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(15, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 127);
            this.button1.TabIndex = 0;
            this.button1.Text = "PRINT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.print_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.quantityDataGridViewTextBoxColumn,
            this.itemNameDataGridViewTextBoxColumn});
            this.tableLayoutPanelInfo.SetColumnSpan(this.dataGridView2, 4);
            this.dataGridView2.DataSource = this.itemBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(88)))), ((int)(((byte)(97)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(288, 23);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(163, 124);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // tableLayoutPanelItemDetails
            // 
            this.tableLayoutPanelItemDetails.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelItemDetails.ColumnCount = 4;
            this.tableLayoutPanelInfo.SetColumnSpan(this.tableLayoutPanelItemDetails, 9);
            this.tableLayoutPanelItemDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelItemDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelItemDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelItemDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelItemDetails.Controls.Add(this.textBoxQty, 0, 0);
            this.tableLayoutPanelItemDetails.Controls.Add(this.textBoxItemName, 1, 0);
            this.tableLayoutPanelItemDetails.Controls.Add(this.listBoxAdjustments, 0, 1);
            this.tableLayoutPanelItemDetails.Controls.Add(this.listBoxToppings, 2, 1);
            this.tableLayoutPanelItemDetails.Controls.Add(this.textBoxInstructions, 0, 3);
            this.tableLayoutPanelItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelItemDetails.Location = new System.Drawing.Point(452, 23);
            this.tableLayoutPanelItemDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelItemDetails.Name = "tableLayoutPanelItemDetails";
            this.tableLayoutPanelItemDetails.RowCount = 4;
            this.tableLayoutPanelItemDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanelItemDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanelItemDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanelItemDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanelItemDetails.Size = new System.Drawing.Size(378, 124);
            this.tableLayoutPanelItemDetails.TabIndex = 0;
            // 
            // textBoxQty
            // 
            this.textBoxQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxQty.Location = new System.Drawing.Point(3, 3);
            this.textBoxQty.Name = "textBoxQty";
            this.textBoxQty.ReadOnly = true;
            this.textBoxQty.Size = new System.Drawing.Size(88, 20);
            this.textBoxQty.TabIndex = 0;
            // 
            // textBoxItemName
            // 
            this.tableLayoutPanelItemDetails.SetColumnSpan(this.textBoxItemName, 3);
            this.textBoxItemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxItemName.Location = new System.Drawing.Point(97, 3);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.ReadOnly = true;
            this.textBoxItemName.Size = new System.Drawing.Size(278, 20);
            this.textBoxItemName.TabIndex = 1;
            // 
            // listBoxAdjustments
            // 
            this.tableLayoutPanelItemDetails.SetColumnSpan(this.listBoxAdjustments, 2);
            this.listBoxAdjustments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAdjustments.FormattingEnabled = true;
            this.listBoxAdjustments.Location = new System.Drawing.Point(3, 34);
            this.listBoxAdjustments.Name = "listBoxAdjustments";
            this.tableLayoutPanelItemDetails.SetRowSpan(this.listBoxAdjustments, 2);
            this.listBoxAdjustments.Size = new System.Drawing.Size(182, 56);
            this.listBoxAdjustments.TabIndex = 2;
            // 
            // listBoxToppings
            // 
            this.tableLayoutPanelItemDetails.SetColumnSpan(this.listBoxToppings, 2);
            this.listBoxToppings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxToppings.FormattingEnabled = true;
            this.listBoxToppings.Location = new System.Drawing.Point(191, 34);
            this.listBoxToppings.Name = "listBoxToppings";
            this.tableLayoutPanelItemDetails.SetRowSpan(this.listBoxToppings, 2);
            this.listBoxToppings.Size = new System.Drawing.Size(184, 56);
            this.listBoxToppings.TabIndex = 3;
            // 
            // textBoxInstructions
            // 
            this.tableLayoutPanelItemDetails.SetColumnSpan(this.textBoxInstructions, 4);
            this.textBoxInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxInstructions.Location = new System.Drawing.Point(3, 96);
            this.textBoxInstructions.Multiline = true;
            this.textBoxInstructions.Name = "textBoxInstructions";
            this.textBoxInstructions.ReadOnly = true;
            this.textBoxInstructions.Size = new System.Drawing.Size(372, 25);
            this.textBoxInstructions.TabIndex = 4;
            // 
            // tableLayoutPanelOrderDetails
            // 
            this.tableLayoutPanelOrderDetails.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelOrderDetails.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelOrderDetails.ColumnCount = 2;
            this.tableLayoutPanelInfo.SetColumnSpan(this.tableLayoutPanelOrderDetails, 7);
            this.tableLayoutPanelOrderDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.99184F));
            this.tableLayoutPanelOrderDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.00816F));
            this.tableLayoutPanelOrderDetails.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.nameTextBox, 1, 0);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.contactNumTextBox, 1, 1);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.orderNumTextBox, 1, 2);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.orderSizeTextBox, 1, 3);
            this.tableLayoutPanelOrderDetails.Controls.Add(this.messageIdTextBox, 1, 4);
            this.tableLayoutPanelOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOrderDetails.Location = new System.Drawing.Point(1, 23);
            this.tableLayoutPanelOrderDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelOrderDetails.Name = "tableLayoutPanelOrderDetails";
            this.tableLayoutPanelOrderDetails.RowCount = 5;
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelOrderDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelOrderDetails.Size = new System.Drawing.Size(286, 124);
            this.tableLayoutPanelOrderDetails.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAME";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.label2.Location = new System.Drawing.Point(4, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CONTACT #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.label3.Location = new System.Drawing.Point(4, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ORDER #";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.label4.Location = new System.Drawing.Point(4, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "ORDER SIZE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.label5.Location = new System.Drawing.Point(4, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "MESSAGE ID";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.nameTextBox.Location = new System.Drawing.Point(81, 4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(201, 15);
            this.nameTextBox.TabIndex = 5;
            // 
            // contactNumTextBox
            // 
            this.contactNumTextBox.BackColor = System.Drawing.Color.White;
            this.contactNumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contactNumTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactNumTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactNumTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.contactNumTextBox.Location = new System.Drawing.Point(81, 28);
            this.contactNumTextBox.Name = "contactNumTextBox";
            this.contactNumTextBox.ReadOnly = true;
            this.contactNumTextBox.Size = new System.Drawing.Size(201, 15);
            this.contactNumTextBox.TabIndex = 6;
            // 
            // orderNumTextBox
            // 
            this.orderNumTextBox.BackColor = System.Drawing.Color.White;
            this.orderNumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.orderNumTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderNumTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderNumTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.orderNumTextBox.Location = new System.Drawing.Point(81, 52);
            this.orderNumTextBox.Name = "orderNumTextBox";
            this.orderNumTextBox.ReadOnly = true;
            this.orderNumTextBox.Size = new System.Drawing.Size(201, 15);
            this.orderNumTextBox.TabIndex = 7;
            // 
            // orderSizeTextBox
            // 
            this.orderSizeTextBox.BackColor = System.Drawing.Color.White;
            this.orderSizeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.orderSizeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderSizeTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSizeTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.orderSizeTextBox.Location = new System.Drawing.Point(81, 76);
            this.orderSizeTextBox.Name = "orderSizeTextBox";
            this.orderSizeTextBox.ReadOnly = true;
            this.orderSizeTextBox.Size = new System.Drawing.Size(201, 15);
            this.orderSizeTextBox.TabIndex = 8;
            // 
            // messageIdTextBox
            // 
            this.messageIdTextBox.BackColor = System.Drawing.Color.White;
            this.messageIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageIdTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageIdTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.messageIdTextBox.Location = new System.Drawing.Point(81, 100);
            this.messageIdTextBox.Name = "messageIdTextBox";
            this.messageIdTextBox.ReadOnly = true;
            this.messageIdTextBox.Size = new System.Drawing.Size(201, 15);
            this.messageIdTextBox.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(831, 430);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
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
            this.splitContainer1.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseDown);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(999, 626);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 0;
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
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControlAppTabs);
            this.splitContainer2.Size = new System.Drawing.Size(845, 626);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControlAppTabs
            // 
            this.tabControlAppTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlAppTabs.Controls.Add(this.tabHome);
            this.tabControlAppTabs.Controls.Add(this.tabAbout);
            this.tabControlAppTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAppTabs.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControlAppTabs.Location = new System.Drawing.Point(0, 0);
            this.tabControlAppTabs.Name = "tabControlAppTabs";
            this.tabControlAppTabs.SelectedIndex = 0;
            this.tabControlAppTabs.Size = new System.Drawing.Size(845, 597);
            this.tabControlAppTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlAppTabs.TabIndex = 0;
            // 
            // tabHome
            // 
            this.tabHome.Controls.Add(this.splitContainer3);
            this.tabHome.Location = new System.Drawing.Point(4, 5);
            this.tabHome.Name = "tabHome";
            this.tabHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabHome.Size = new System.Drawing.Size(837, 588);
            this.tabHome.TabIndex = 0;
            this.tabHome.Text = "Home";
            this.tabHome.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanelInfo);
            this.splitContainer3.Size = new System.Drawing.Size(831, 582);
            this.splitContainer3.SplitterDistance = 430;
            this.splitContainer3.TabIndex = 0;
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelInfo.ColumnCount = 20;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelInfo.Controls.Add(this.dataGridView2, 7, 1);
            this.tableLayoutPanelInfo.Controls.Add(this.tableLayoutPanelItemDetails, 11, 1);
            this.tableLayoutPanelInfo.Controls.Add(this.tableLayoutPanelOrderDetails, 0, 1);
            this.tableLayoutPanelInfo.Controls.Add(this.labelOrderDetails, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.labelItemList, 7, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.labelItemDetails, 11, 0);
            this.tableLayoutPanelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 2;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(831, 148);
            this.tableLayoutPanelInfo.TabIndex = 0;
            // 
            // tabAbout
            // 
            this.tabAbout.Location = new System.Drawing.Point(4, 5);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabAbout.Size = new System.Drawing.Size(837, 588);
            this.tabAbout.TabIndex = 1;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.button1);
            this.splitContainer4.Panel2.Padding = new System.Windows.Forms.Padding(15);
            this.splitContainer4.Size = new System.Drawing.Size(150, 626);
            this.splitContainer4.SplitterDistance = 465;
            this.splitContainer4.TabIndex = 0;
            // 
            // labelOrderDetails
            // 
            this.labelOrderDetails.AutoSize = true;
            this.labelOrderDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.tableLayoutPanelInfo.SetColumnSpan(this.labelOrderDetails, 7);
            this.labelOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOrderDetails.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrderDetails.ForeColor = System.Drawing.Color.White;
            this.labelOrderDetails.Location = new System.Drawing.Point(1, 1);
            this.labelOrderDetails.Margin = new System.Windows.Forms.Padding(0);
            this.labelOrderDetails.Name = "labelOrderDetails";
            this.labelOrderDetails.Size = new System.Drawing.Size(286, 21);
            this.labelOrderDetails.TabIndex = 1;
            this.labelOrderDetails.Text = "ORDER DETAILS";
            this.labelOrderDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelItemList
            // 
            this.labelItemList.AutoSize = true;
            this.labelItemList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.tableLayoutPanelInfo.SetColumnSpan(this.labelItemList, 4);
            this.labelItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelItemList.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemList.ForeColor = System.Drawing.Color.White;
            this.labelItemList.Location = new System.Drawing.Point(288, 1);
            this.labelItemList.Margin = new System.Windows.Forms.Padding(0);
            this.labelItemList.Name = "labelItemList";
            this.labelItemList.Size = new System.Drawing.Size(163, 21);
            this.labelItemList.TabIndex = 2;
            this.labelItemList.Text = "ITEM LIST";
            this.labelItemList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelItemDetails
            // 
            this.labelItemDetails.AutoSize = true;
            this.labelItemDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.tableLayoutPanelInfo.SetColumnSpan(this.labelItemDetails, 9);
            this.labelItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelItemDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDetails.ForeColor = System.Drawing.Color.White;
            this.labelItemDetails.Location = new System.Drawing.Point(452, 1);
            this.labelItemDetails.Margin = new System.Windows.Forms.Padding(0);
            this.labelItemDetails.Name = "labelItemDetails";
            this.labelItemDetails.Size = new System.Drawing.Size(378, 21);
            this.labelItemDetails.TabIndex = 3;
            this.labelItemDetails.Text = "ITEM DETAILS";
            this.labelItemDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            this.quantityDataGridViewTextBoxColumn.FillWeight = 25F;
            this.quantityDataGridViewTextBoxColumn.HeaderText = "QTY";
            this.quantityDataGridViewTextBoxColumn.MaxInputLength = 4;
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemNameDataGridViewTextBoxColumn
            // 
            this.itemNameDataGridViewTextBoxColumn.DataPropertyName = "ItemName";
            this.itemNameDataGridViewTextBoxColumn.HeaderText = "NAME";
            this.itemNameDataGridViewTextBoxColumn.Name = "itemNameDataGridViewTextBoxColumn";
            this.itemNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(GmailQuickstart.Item);
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
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanelItemDetails.ResumeLayout(false);
            this.tableLayoutPanelItemDetails.PerformLayout();
            this.tableLayoutPanelOrderDetails.ResumeLayout(false);
            this.tableLayoutPanelOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControlAppTabs.ResumeLayout(false);
            this.tabHome.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelItemDetails;
        private System.Windows.Forms.TextBox textBoxQty;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.ListBox listBoxAdjustments;
        private System.Windows.Forms.ListBox listBoxToppings;
        private System.Windows.Forms.TextBox textBoxInstructions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOrderDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox contactNumTextBox;
        private System.Windows.Forms.TextBox orderNumTextBox;
        private System.Windows.Forms.TextBox orderSizeTextBox;
        private System.Windows.Forms.TextBox messageIdTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControlAppTabs;
        private System.Windows.Forms.TabPage tabHome;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label labelOrderDetails;
        private System.Windows.Forms.Label labelItemList;
        private System.Windows.Forms.Label labelItemDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
    }
}
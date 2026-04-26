namespace ForkliftApp
{
    partial class frmInformation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridContaunersInList = new System.Windows.Forms.DataGridView();
            this.lblHaderIn = new System.Windows.Forms.Label();
            this.lblHaderOut = new System.Windows.Forms.Label();
            this.dataGridContaunersOutList = new System.Windows.Forms.DataGridView();
            this.timerfrmInformation = new System.Windows.Forms.Timer(this.components);
            this.lblCoutRecordesOut = new System.Windows.Forms.Label();
            this.lblCoutRecordesIn = new System.Windows.Forms.Label();
            this.lblWorkshader = new System.Windows.Forms.Label();
            this.lblTest = new System.Windows.Forms.Label();
            this.lblShikuf = new System.Windows.Forms.Label();
            this.checkEM = new System.Windows.Forms.CheckBox();
            this.dataGridContaunersOutEmpty = new System.Windows.Forms.DataGridView();
            this.lblHaderEMOut = new System.Windows.Forms.Label();
            this.ViewType = new System.Windows.Forms.ComboBox();
            this.lblViewType = new System.Windows.Forms.Label();
            this.btnUnloading = new System.Windows.Forms.Button();
            this.btnLoading = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnWorks = new System.Windows.Forms.Button();
            this.btnInformation = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btndatagridIn = new System.Windows.Forms.Button();
            this.btnDatagridOut = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBoxBtuonsInOut = new System.Windows.Forms.GroupBox();
            this.btnDOWN = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.btnDatagridEMOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersInList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersOutList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersOutEmpty)).BeginInit();
            this.groupBoxBtuonsInOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridContaunersInList
            // 
            this.dataGridContaunersInList.AllowUserToAddRows = false;
            this.dataGridContaunersInList.AllowUserToDeleteRows = false;
            this.dataGridContaunersInList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridContaunersInList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersInList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.dataGridContaunersInList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridContaunersInList.DefaultCellStyle = dataGridViewCellStyle38;
            this.dataGridContaunersInList.Location = new System.Drawing.Point(6, 32);
            this.dataGridContaunersInList.MultiSelect = false;
            this.dataGridContaunersInList.Name = "dataGridContaunersInList";
            this.dataGridContaunersInList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersInList.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.dataGridContaunersInList.RowHeadersVisible = false;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersInList.RowsDefaultCellStyle = dataGridViewCellStyle40;
            this.dataGridContaunersInList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersInList.RowTemplate.Height = 35;
            this.dataGridContaunersInList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridContaunersInList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 211);
            this.dataGridContaunersInList.TabIndex = 0;
            this.dataGridContaunersInList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersInList_CellContentClick);
            this.dataGridContaunersInList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersInList_CellDoubleClick);
            this.dataGridContaunersInList.SelectionChanged += new System.EventHandler(this.dataGridContaunersInList_SelectionChanged);
            // 
            // lblHaderIn
            // 
            this.lblHaderIn.AutoSize = true;
            this.lblHaderIn.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHaderIn.ForeColor = System.Drawing.Color.Navy;
            this.lblHaderIn.Location = new System.Drawing.Point(5, 1);
            this.lblHaderIn.Name = "lblHaderIn";
            this.lblHaderIn.Size = new System.Drawing.Size(78, 25);
            this.lblHaderIn.TabIndex = 1;
            this.lblHaderIn.Text = "פריקה";
            // 
            // lblHaderOut
            // 
            this.lblHaderOut.AutoSize = true;
            this.lblHaderOut.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHaderOut.ForeColor = System.Drawing.Color.Navy;
            this.lblHaderOut.Location = new System.Drawing.Point(5, 244);
            this.lblHaderOut.Name = "lblHaderOut";
            this.lblHaderOut.Size = new System.Drawing.Size(76, 25);
            this.lblHaderOut.TabIndex = 2;
            this.lblHaderOut.Text = "טעינה";
            // 
            // dataGridContaunersOutList
            // 
            this.dataGridContaunersOutList.AllowUserToAddRows = false;
            this.dataGridContaunersOutList.AllowUserToDeleteRows = false;
            this.dataGridContaunersOutList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersOutList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridContaunersOutList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridContaunersOutList.DefaultCellStyle = dataGridViewCellStyle42;
            this.dataGridContaunersOutList.Location = new System.Drawing.Point(6, 269);
            this.dataGridContaunersOutList.MultiSelect = false;
            this.dataGridContaunersOutList.Name = "dataGridContaunersOutList";
            this.dataGridContaunersOutList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle43.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle43.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersOutList.RowHeadersDefaultCellStyle = dataGridViewCellStyle43;
            this.dataGridContaunersOutList.RowHeadersVisible = false;
            dataGridViewCellStyle44.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersOutList.RowsDefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridContaunersOutList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersOutList.RowTemplate.Height = 35;
            this.dataGridContaunersOutList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridContaunersOutList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 211);
            this.dataGridContaunersOutList.TabIndex = 3;
            this.dataGridContaunersOutList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersOutList_CellContentClick);
            this.dataGridContaunersOutList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersOutList_CellDoubleClick);
            this.dataGridContaunersOutList.SelectionChanged += new System.EventHandler(this.dataGridContaunersOutList_SelectionChanged);
            // 
            // timerfrmInformation
            // 
            this.timerfrmInformation.Enabled = true;
            this.timerfrmInformation.Interval = 60000;
            this.timerfrmInformation.Tick += new System.EventHandler(this.timerfrmInformation_Tick);
            // 
            // lblCoutRecordesOut
            // 
            this.lblCoutRecordesOut.AutoSize = true;
            this.lblCoutRecordesOut.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCoutRecordesOut.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblCoutRecordesOut.Location = new System.Drawing.Point(100, 250);
            this.lblCoutRecordesOut.Name = "lblCoutRecordesOut";
            this.lblCoutRecordesOut.Size = new System.Drawing.Size(88, 16);
            this.lblCoutRecordesOut.TabIndex = 44;
            this.lblCoutRecordesOut.Text = "0000000000";
            // 
            // lblCoutRecordesIn
            // 
            this.lblCoutRecordesIn.AutoSize = true;
            this.lblCoutRecordesIn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCoutRecordesIn.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCoutRecordesIn.Location = new System.Drawing.Point(100, 7);
            this.lblCoutRecordesIn.Name = "lblCoutRecordesIn";
            this.lblCoutRecordesIn.Size = new System.Drawing.Size(88, 16);
            this.lblCoutRecordesIn.TabIndex = 45;
            this.lblCoutRecordesIn.Text = "0000000000";
            // 
            // lblWorkshader
            // 
            this.lblWorkshader.AutoSize = true;
            this.lblWorkshader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblWorkshader.Location = new System.Drawing.Point(1051, 9);
            this.lblWorkshader.Name = "lblWorkshader";
            this.lblWorkshader.Size = new System.Drawing.Size(133, 19);
            this.lblWorkshader.TabIndex = 46;
            this.lblWorkshader.Text = "עבודות פתוחות:";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTest.ForeColor = System.Drawing.Color.Blue;
            this.lblTest.Location = new System.Drawing.Point(1189, 5);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(22, 21);
            this.lblTest.TabIndex = 47;
            this.lblTest.Text = "ב";
            // 
            // lblShikuf
            // 
            this.lblShikuf.AutoSize = true;
            this.lblShikuf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShikuf.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblShikuf.ForeColor = System.Drawing.Color.Blue;
            this.lblShikuf.Location = new System.Drawing.Point(1212, 5);
            this.lblShikuf.Name = "lblShikuf";
            this.lblShikuf.Size = new System.Drawing.Size(24, 21);
            this.lblShikuf.TabIndex = 48;
            this.lblShikuf.Text = "ש";
            // 
            // checkEM
            // 
            this.checkEM.AutoSize = true;
            this.checkEM.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkEM.Location = new System.Drawing.Point(904, 2);
            this.checkEM.Name = "checkEM";
            this.checkEM.Size = new System.Drawing.Size(141, 27);
            this.checkEM.TabIndex = 49;
            this.checkEM.Text = "ריקות בלבד";
            this.checkEM.UseVisualStyleBackColor = true;
            this.checkEM.Visible = false;
            this.checkEM.CheckedChanged += new System.EventHandler(this.checkEM_CheckedChanged);
            // 
            // dataGridContaunersOutEmpty
            // 
            this.dataGridContaunersOutEmpty.AllowUserToAddRows = false;
            this.dataGridContaunersOutEmpty.AllowUserToDeleteRows = false;
            this.dataGridContaunersOutEmpty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridContaunersOutEmpty.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle45.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle45.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersOutEmpty.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle45;
            this.dataGridContaunersOutEmpty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle46.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridContaunersOutEmpty.DefaultCellStyle = dataGridViewCellStyle46;
            this.dataGridContaunersOutEmpty.Location = new System.Drawing.Point(6, 507);
            this.dataGridContaunersOutEmpty.MultiSelect = false;
            this.dataGridContaunersOutEmpty.Name = "dataGridContaunersOutEmpty";
            this.dataGridContaunersOutEmpty.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle47.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle47.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersOutEmpty.RowHeadersDefaultCellStyle = dataGridViewCellStyle47;
            this.dataGridContaunersOutEmpty.RowHeadersVisible = false;
            dataGridViewCellStyle48.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersOutEmpty.RowsDefaultCellStyle = dataGridViewCellStyle48;
            this.dataGridContaunersOutEmpty.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersOutEmpty.RowTemplate.Height = 35;
            this.dataGridContaunersOutEmpty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridContaunersOutEmpty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridContaunersOutEmpty.Size = new System.Drawing.Size(1242, 182);
            this.dataGridContaunersOutEmpty.TabIndex = 50;
            this.dataGridContaunersOutEmpty.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersOutEmpty_CellDoubleClick);
            this.dataGridContaunersOutEmpty.SelectionChanged += new System.EventHandler(this.dataGridContaunersOutEmpty_SelectionChanged);
            // 
            // lblHaderEMOut
            // 
            this.lblHaderEMOut.AutoSize = true;
            this.lblHaderEMOut.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHaderEMOut.ForeColor = System.Drawing.Color.Navy;
            this.lblHaderEMOut.Location = new System.Drawing.Point(12, 482);
            this.lblHaderEMOut.Name = "lblHaderEMOut";
            this.lblHaderEMOut.Size = new System.Drawing.Size(144, 25);
            this.lblHaderEMOut.TabIndex = 51;
            this.lblHaderEMOut.Text = "טעינת ריקות";
            // 
            // ViewType
            // 
            this.ViewType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.ViewType.FormattingEnabled = true;
            this.ViewType.Items.AddRange(new object[] {
            "הכל",
            "ריקות בלבד"});
            this.ViewType.Location = new System.Drawing.Point(466, -1);
            this.ViewType.Name = "ViewType";
            this.ViewType.Size = new System.Drawing.Size(189, 31);
            this.ViewType.TabIndex = 52;
            this.ViewType.SelectedIndexChanged += new System.EventHandler(this.ViewType_SelectedIndexChanged);
            // 
            // lblViewType
            // 
            this.lblViewType.AutoSize = true;
            this.lblViewType.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblViewType.Location = new System.Drawing.Point(397, 4);
            this.lblViewType.Name = "lblViewType";
            this.lblViewType.Size = new System.Drawing.Size(63, 19);
            this.lblViewType.TabIndex = 53;
            this.lblViewType.Text = "תצוגה:";
            // 
            // btnUnloading
            // 
            this.btnUnloading.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnloading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUnloading.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUnloading.ForeColor = System.Drawing.Color.Navy;
            this.btnUnloading.Location = new System.Drawing.Point(575, 17);
            this.btnUnloading.Name = "btnUnloading";
            this.btnUnloading.Size = new System.Drawing.Size(99, 80);
            this.btnUnloading.TabIndex = 46;
            this.btnUnloading.Text = "פריקה F6";
            this.btnUnloading.UseVisualStyleBackColor = false;
            this.btnUnloading.Click += new System.EventHandler(this.btnUnloading_Click);
            // 
            // btnLoading
            // 
            this.btnLoading.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoading.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnLoading.ForeColor = System.Drawing.Color.Navy;
            this.btnLoading.Location = new System.Drawing.Point(462, 17);
            this.btnLoading.Name = "btnLoading";
            this.btnLoading.Size = new System.Drawing.Size(99, 80);
            this.btnLoading.TabIndex = 48;
            this.btnLoading.Text = "טעינה F7";
            this.btnLoading.UseVisualStyleBackColor = false;
            this.btnLoading.Click += new System.EventHandler(this.btnLoading_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnQuery.ForeColor = System.Drawing.Color.Navy;
            this.btnQuery.Location = new System.Drawing.Point(329, 17);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(123, 80);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "שאילתה F3";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnWorks
            // 
            this.btnWorks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnWorks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWorks.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnWorks.ForeColor = System.Drawing.Color.Navy;
            this.btnWorks.Location = new System.Drawing.Point(210, 17);
            this.btnWorks.Name = "btnWorks";
            this.btnWorks.Size = new System.Drawing.Size(110, 80);
            this.btnWorks.TabIndex = 1;
            this.btnWorks.Text = "עבודות F4";
            this.btnWorks.UseVisualStyleBackColor = false;
            this.btnWorks.Click += new System.EventHandler(this.btnWorks_Click);
            // 
            // btnInformation
            // 
            this.btnInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInformation.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnInformation.ForeColor = System.Drawing.Color.Navy;
            this.btnInformation.Location = new System.Drawing.Point(110, 17);
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(89, 80);
            this.btnInformation.TabIndex = 2;
            this.btnInformation.Text = "מידע F5";
            this.btnInformation.UseVisualStyleBackColor = false;
            this.btnInformation.Click += new System.EventHandler(this.btnInformation_Click);
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(10, 17);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 80);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "יציאה  F10";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSelect.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSelect.ForeColor = System.Drawing.Color.Navy;
            this.btnSelect.Location = new System.Drawing.Point(686, 17);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(99, 80);
            this.btnSelect.TabIndex = 44;
            this.btnSelect.Text = "אישור F8";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btndatagridIn
            // 
            this.btndatagridIn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btndatagridIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btndatagridIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btndatagridIn.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btndatagridIn.ForeColor = System.Drawing.Color.Purple;
            this.btndatagridIn.Location = new System.Drawing.Point(910, 18);
            this.btndatagridIn.Name = "btndatagridIn";
            this.btndatagridIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btndatagridIn.Size = new System.Drawing.Size(69, 78);
            this.btndatagridIn.TabIndex = 49;
            this.btndatagridIn.Text = "1 F2";
            this.btndatagridIn.UseVisualStyleBackColor = false;
            this.btndatagridIn.Click += new System.EventHandler(this.btndatagridIn_Click);
            // 
            // btnDatagridOut
            // 
            this.btnDatagridOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDatagridOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDatagridOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDatagridOut.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnDatagridOut.ForeColor = System.Drawing.Color.Purple;
            this.btnDatagridOut.Location = new System.Drawing.Point(993, 18);
            this.btnDatagridOut.Name = "btnDatagridOut";
            this.btnDatagridOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDatagridOut.Size = new System.Drawing.Size(69, 78);
            this.btnDatagridOut.TabIndex = 50;
            this.btnDatagridOut.Text = "2 F9";
            this.btnDatagridOut.UseVisualStyleBackColor = false;
            this.btnDatagridOut.Click += new System.EventHandler(this.btnDatagridOut_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Red;
            this.btnRefresh.Location = new System.Drawing.Point(799, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(102, 80);
            this.btnRefresh.TabIndex = 51;
            this.btnRefresh.Text = "רענן F1";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // groupBoxBtuonsInOut
            // 
            this.groupBoxBtuonsInOut.Controls.Add(this.btnDOWN);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnUP);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnDatagridEMOut);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnRefresh);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnDatagridOut);
            this.groupBoxBtuonsInOut.Controls.Add(this.btndatagridIn);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnSelect);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnExit);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnInformation);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnWorks);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnQuery);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnLoading);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnUnloading);
            this.groupBoxBtuonsInOut.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBoxBtuonsInOut.Location = new System.Drawing.Point(10, 617);
            this.groupBoxBtuonsInOut.Name = "groupBoxBtuonsInOut";
            this.groupBoxBtuonsInOut.Size = new System.Drawing.Size(1238, 102);
            this.groupBoxBtuonsInOut.TabIndex = 43;
            this.groupBoxBtuonsInOut.TabStop = false;
            // 
            // btnDOWN
            // 
            this.btnDOWN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDOWN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDOWN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDOWN.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnDOWN.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.btnDOWN.Location = new System.Drawing.Point(1162, 60);
            this.btnDOWN.Name = "btnDOWN";
            this.btnDOWN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDOWN.Size = new System.Drawing.Size(69, 43);
            this.btnDOWN.TabIndex = 54;
            this.btnDOWN.Text = "v";
            this.btnDOWN.UseVisualStyleBackColor = false;
            this.btnDOWN.Click += new System.EventHandler(this.btnDOWN_Click);
            // 
            // btnUP
            // 
            this.btnUP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUP.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUP.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.btnUP.Location = new System.Drawing.Point(1162, 12);
            this.btnUP.Name = "btnUP";
            this.btnUP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUP.Size = new System.Drawing.Size(69, 43);
            this.btnUP.TabIndex = 53;
            this.btnUP.Text = "^";
            this.btnUP.UseVisualStyleBackColor = false;
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
            // 
            // btnDatagridEMOut
            // 
            this.btnDatagridEMOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDatagridEMOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDatagridEMOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDatagridEMOut.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnDatagridEMOut.ForeColor = System.Drawing.Color.Purple;
            this.btnDatagridEMOut.Location = new System.Drawing.Point(1076, 18);
            this.btnDatagridEMOut.Name = "btnDatagridEMOut";
            this.btnDatagridEMOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDatagridEMOut.Size = new System.Drawing.Size(69, 78);
            this.btnDatagridEMOut.TabIndex = 52;
            this.btnDatagridEMOut.Text = "3 F11";
            this.btnDatagridEMOut.UseVisualStyleBackColor = false;
            this.btnDatagridEMOut.Visible = false;
            this.btnDatagridEMOut.Click += new System.EventHandler(this.btnDatagridEMOut_Click);
            // 
            // frmInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(239)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1260, 722);
            this.ControlBox = false;
            this.Controls.Add(this.lblViewType);
            this.Controls.Add(this.ViewType);
            this.Controls.Add(this.lblHaderEMOut);
            this.Controls.Add(this.dataGridContaunersOutEmpty);
            this.Controls.Add(this.checkEM);
            this.Controls.Add(this.lblShikuf);
            this.Controls.Add(this.lblTest);
            this.Controls.Add(this.lblWorkshader);
            this.Controls.Add(this.lblCoutRecordesIn);
            this.Controls.Add(this.lblCoutRecordesOut);
            this.Controls.Add(this.groupBoxBtuonsInOut);
            this.Controls.Add(this.lblHaderOut);
            this.Controls.Add(this.lblHaderIn);
            this.Controls.Add(this.dataGridContaunersInList);
            this.Controls.Add(this.dataGridContaunersOutList);
            this.KeyPreview = true;
            this.Name = "frmInformation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מסך פעולות";
            this.Load += new System.EventHandler(this.frmInformation_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInformation_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersInList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersOutList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersOutEmpty)).EndInit();
            this.groupBoxBtuonsInOut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridContaunersInList;
        private System.Windows.Forms.Label lblHaderIn;
        private System.Windows.Forms.Label lblHaderOut;
        private System.Windows.Forms.DataGridView dataGridContaunersOutList;
        private System.Windows.Forms.Timer timerfrmInformation;
        private System.Windows.Forms.Label lblCoutRecordesOut;
        private System.Windows.Forms.Label lblCoutRecordesIn;
        private System.Windows.Forms.Label lblWorkshader;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Label lblShikuf;
        private System.Windows.Forms.CheckBox checkEM;
        private System.Windows.Forms.DataGridView dataGridContaunersOutEmpty;
        private System.Windows.Forms.Label lblHaderEMOut;
        private System.Windows.Forms.ComboBox ViewType;
        private System.Windows.Forms.Label lblViewType;
        private System.Windows.Forms.Button btnUnloading;
        private System.Windows.Forms.Button btnLoading;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnWorks;
        private System.Windows.Forms.Button btnInformation;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btndatagridIn;
        private System.Windows.Forms.Button btnDatagridOut;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox groupBoxBtuonsInOut;
        private System.Windows.Forms.Button btnDOWN;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Button btnDatagridEMOut;
    }
}
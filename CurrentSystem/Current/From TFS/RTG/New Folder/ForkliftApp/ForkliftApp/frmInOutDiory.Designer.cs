namespace ForkliftApp
{
    partial class frmInOutDiory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridInOutDiory = new System.Windows.Forms.DataGridView();
            this.lblNoData = new System.Windows.Forms.Label();
            this.btnShowAllRecords = new System.Windows.Forms.Button();
            this.lblInOutDiory = new System.Windows.Forms.Label();
            this.cmbInOutDiory = new System.Windows.Forms.ComboBox();
            this.lblLoction = new System.Windows.Forms.Label();
            this.cnbLoction = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBoxBtuonsInOut = new System.Windows.Forms.GroupBox();
            this.btnInformation = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cnbHazardousSubstances = new System.Windows.Forms.ComboBox();
            this.txtSumMovments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerInOutDiory = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DELETE = new System.Windows.Forms.Button();
            this.Zero = new System.Windows.Forms.Button();
            this.Eight = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.Four = new System.Windows.Forms.Button();
            this.Nine = new System.Windows.Forms.Button();
            this.Seven = new System.Windows.Forms.Button();
            this.Six = new System.Windows.Forms.Button();
            this.Five = new System.Windows.Forms.Button();
            this.THREE = new System.Windows.Forms.Button();
            this.TWO = new System.Windows.Forms.Button();
            this.One = new System.Windows.Forms.Button();
            this.txtLocatin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInOutDiory)).BeginInit();
            this.groupBoxBtuonsInOut.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridInOutDiory
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.dataGridInOutDiory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridInOutDiory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridInOutDiory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInOutDiory.Location = new System.Drawing.Point(14, 81);
            this.dataGridInOutDiory.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridInOutDiory.MultiSelect = false;
            this.dataGridInOutDiory.Name = "dataGridInOutDiory";
            this.dataGridInOutDiory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridInOutDiory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridInOutDiory.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.dataGridInOutDiory.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridInOutDiory.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridInOutDiory.RowTemplate.Height = 35;
            this.dataGridInOutDiory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridInOutDiory.Size = new System.Drawing.Size(856, 469);
            this.dataGridInOutDiory.TabIndex = 0;
            this.dataGridInOutDiory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridInOutDiory_CellContentClick);
            this.dataGridInOutDiory.SelectionChanged += new System.EventHandler(this.dataGridInOutDiory_SelectionChanged);
            // 
            // lblNoData
            // 
            this.lblNoData.AutoEllipsis = true;
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Tahoma", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoData.Location = new System.Drawing.Point(190, 201);
            this.lblNoData.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(534, 116);
            this.lblNoData.TabIndex = 55;
            this.lblNoData.Text = "אין נתונים";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            this.lblNoData.Click += new System.EventHandler(this.lblNoData_Click);
            // 
            // btnShowAllRecords
            // 
            this.btnShowAllRecords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowAllRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowAllRecords.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnShowAllRecords.ForeColor = System.Drawing.Color.Navy;
            this.btnShowAllRecords.Location = new System.Drawing.Point(417, 7);
            this.btnShowAllRecords.Margin = new System.Windows.Forms.Padding(5);
            this.btnShowAllRecords.Name = "btnShowAllRecords";
            this.btnShowAllRecords.Size = new System.Drawing.Size(112, 67);
            this.btnShowAllRecords.TabIndex = 63;
            this.btnShowAllRecords.Text = "הכל  F1";
            this.btnShowAllRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowAllRecords.UseVisualStyleBackColor = false;
            this.btnShowAllRecords.Click += new System.EventHandler(this.btnShowAllRecords_Click);
            // 
            // lblInOutDiory
            // 
            this.lblInOutDiory.AutoSize = true;
            this.lblInOutDiory.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblInOutDiory.ForeColor = System.Drawing.Color.Navy;
            this.lblInOutDiory.Location = new System.Drawing.Point(24, 21);
            this.lblInOutDiory.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblInOutDiory.Name = "lblInOutDiory";
            this.lblInOutDiory.Size = new System.Drawing.Size(134, 27);
            this.lblInOutDiory.TabIndex = 62;
            this.lblInOutDiory.Text = "בחר תנועה";
            // 
            // cmbInOutDiory
            // 
            this.cmbInOutDiory.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmbInOutDiory.FormattingEnabled = true;
            this.cmbInOutDiory.Items.AddRange(new object[] {
            "כניסה",
            "יציאה"});
            this.cmbInOutDiory.Location = new System.Drawing.Point(168, 14);
            this.cmbInOutDiory.Margin = new System.Windows.Forms.Padding(5);
            this.cmbInOutDiory.Name = "cmbInOutDiory";
            this.cmbInOutDiory.Size = new System.Drawing.Size(239, 50);
            this.cmbInOutDiory.TabIndex = 61;
            this.cmbInOutDiory.SelectedIndexChanged += new System.EventHandler(this.cmbInOutDiory_SelectedIndexChanged);
            this.cmbInOutDiory.Click += new System.EventHandler(this.cmbInOutDiory_Click);
            // 
            // lblLoction
            // 
            this.lblLoction.AutoSize = true;
            this.lblLoction.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblLoction.ForeColor = System.Drawing.Color.Navy;
            this.lblLoction.Location = new System.Drawing.Point(566, 24);
            this.lblLoction.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblLoction.Name = "lblLoction";
            this.lblLoction.Size = new System.Drawing.Size(128, 27);
            this.lblLoction.TabIndex = 66;
            this.lblLoction.Text = "בחר איתור";
            this.lblLoction.Click += new System.EventHandler(this.lblLoction_Click);
            // 
            // cnbLoction
            // 
            this.cnbLoction.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cnbLoction.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbLoction.FormattingEnabled = true;
            this.cnbLoction.Items.AddRange(new object[] {
            "כניסה",
            "יציאה"});
            this.cnbLoction.Location = new System.Drawing.Point(694, 14);
            this.cnbLoction.Margin = new System.Windows.Forms.Padding(5);
            this.cnbLoction.Name = "cnbLoction";
            this.cnbLoction.Size = new System.Drawing.Size(199, 50);
            this.cnbLoction.TabIndex = 65;
            this.cnbLoction.SelectedIndexChanged += new System.EventHandler(this.cnbLoction_SelectedIndexChanged);
            this.cnbLoction.Click += new System.EventHandler(this.cnbLoction_Click);
            this.cnbLoction.Leave += new System.EventHandler(this.cnbLoction_Leave);
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(115, 19);
            this.btnExit.Margin = new System.Windows.Forms.Padding(5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(191, 71);
            this.btnExit.TabIndex = 70;
            this.btnExit.Text = "יציאה  F10";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBoxBtuonsInOut
            // 
            this.groupBoxBtuonsInOut.Controls.Add(this.btnInformation);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnExit);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnQuery);
            this.groupBoxBtuonsInOut.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBoxBtuonsInOut.Location = new System.Drawing.Point(47, 603);
            this.groupBoxBtuonsInOut.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxBtuonsInOut.Name = "groupBoxBtuonsInOut";
            this.groupBoxBtuonsInOut.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxBtuonsInOut.Size = new System.Drawing.Size(1155, 96);
            this.groupBoxBtuonsInOut.TabIndex = 71;
            this.groupBoxBtuonsInOut.TabStop = false;
            // 
            // btnInformation
            // 
            this.btnInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInformation.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnInformation.ForeColor = System.Drawing.Color.Navy;
            this.btnInformation.Location = new System.Drawing.Point(484, 17);
            this.btnInformation.Margin = new System.Windows.Forms.Padding(5);
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(218, 71);
            this.btnInformation.TabIndex = 2;
            this.btnInformation.Text = "מידע   F5";
            this.btnInformation.UseVisualStyleBackColor = false;
            this.btnInformation.Click += new System.EventHandler(this.btnInformation_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ForeColor = System.Drawing.Color.Navy;
            this.btnQuery.Location = new System.Drawing.Point(827, 17);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(5);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(227, 71);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "שאילתה   F3";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cnbHazardousSubstances
            // 
            this.cnbHazardousSubstances.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cnbHazardousSubstances.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cnbHazardousSubstances.FormattingEnabled = true;
            this.cnbHazardousSubstances.Items.AddRange(new object[] {
            "כניסה",
            "יציאה"});
            this.cnbHazardousSubstances.Location = new System.Drawing.Point(1047, 14);
            this.cnbHazardousSubstances.Margin = new System.Windows.Forms.Padding(5);
            this.cnbHazardousSubstances.Name = "cnbHazardousSubstances";
            this.cnbHazardousSubstances.Size = new System.Drawing.Size(199, 50);
            this.cnbHazardousSubstances.TabIndex = 72;
            this.cnbHazardousSubstances.SelectedIndexChanged += new System.EventHandler(this.cnbHazardousSubstances_SelectedIndexChanged);
            this.cnbHazardousSubstances.Click += new System.EventHandler(this.cnbHazardousSubstances_Click);
            // 
            // txtSumMovments
            // 
            this.txtSumMovments.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtSumMovments.Location = new System.Drawing.Point(14, 567);
            this.txtSumMovments.Margin = new System.Windows.Forms.Padding(5);
            this.txtSumMovments.Name = "txtSumMovments";
            this.txtSumMovments.Size = new System.Drawing.Size(810, 33);
            this.txtSumMovments.TabIndex = 73;
            this.txtSumMovments.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(903, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 27);
            this.label1.TabIndex = 74;
            this.label1.Text = "בחר חומ\"ס";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DELETE);
            this.groupBox1.Controls.Add(this.Zero);
            this.groupBox1.Controls.Add(this.Eight);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.Four);
            this.groupBox1.Controls.Add(this.Nine);
            this.groupBox1.Controls.Add(this.Seven);
            this.groupBox1.Controls.Add(this.Six);
            this.groupBox1.Controls.Add(this.Five);
            this.groupBox1.Controls.Add(this.THREE);
            this.groupBox1.Controls.Add(this.TWO);
            this.groupBox1.Controls.Add(this.One);
            this.groupBox1.Controls.Add(this.txtLocatin);
            this.groupBox1.Location = new System.Drawing.Point(908, 81);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(338, 519);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // DELETE
            // 
            this.DELETE.BackColor = System.Drawing.Color.Coral;
            this.DELETE.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DELETE.Location = new System.Drawing.Point(115, 427);
            this.DELETE.Margin = new System.Windows.Forms.Padding(5);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(190, 76);
            this.DELETE.TabIndex = 18;
            this.DELETE.Tag = "Back";
            this.DELETE.Text = "מחק";
            this.DELETE.UseVisualStyleBackColor = false;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // Zero
            // 
            this.Zero.BackColor = System.Drawing.Color.PowderBlue;
            this.Zero.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Zero.Location = new System.Drawing.Point(15, 327);
            this.Zero.Margin = new System.Windows.Forms.Padding(5);
            this.Zero.Name = "Zero";
            this.Zero.Size = new System.Drawing.Size(90, 90);
            this.Zero.TabIndex = 17;
            this.Zero.Text = "0";
            this.Zero.UseVisualStyleBackColor = false;
            this.Zero.Click += new System.EventHandler(this.Zero_Click);
            // 
            // Eight
            // 
            this.Eight.BackColor = System.Drawing.Color.PowderBlue;
            this.Eight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Eight.Location = new System.Drawing.Point(115, 227);
            this.Eight.Margin = new System.Windows.Forms.Padding(5);
            this.Eight.Name = "Eight";
            this.Eight.Size = new System.Drawing.Size(90, 90);
            this.Eight.TabIndex = 16;
            this.Eight.Text = "8";
            this.Eight.UseVisualStyleBackColor = false;
            this.Eight.Click += new System.EventHandler(this.Eight_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(115, 327);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 90);
            this.btnOK.TabIndex = 19;
            this.btnOK.Tag = "Back";
            this.btnOK.Text = "אישור";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Four
            // 
            this.Four.BackColor = System.Drawing.Color.PowderBlue;
            this.Four.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Four.Location = new System.Drawing.Point(15, 127);
            this.Four.Margin = new System.Windows.Forms.Padding(5);
            this.Four.Name = "Four";
            this.Four.Size = new System.Drawing.Size(90, 90);
            this.Four.TabIndex = 15;
            this.Four.Text = "4";
            this.Four.UseVisualStyleBackColor = false;
            this.Four.Click += new System.EventHandler(this.Four_Click);
            // 
            // Nine
            // 
            this.Nine.BackColor = System.Drawing.Color.PowderBlue;
            this.Nine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nine.Location = new System.Drawing.Point(215, 227);
            this.Nine.Margin = new System.Windows.Forms.Padding(5);
            this.Nine.Name = "Nine";
            this.Nine.Size = new System.Drawing.Size(90, 90);
            this.Nine.TabIndex = 14;
            this.Nine.Text = "9";
            this.Nine.UseVisualStyleBackColor = false;
            this.Nine.Click += new System.EventHandler(this.Nine_Click);
            // 
            // Seven
            // 
            this.Seven.BackColor = System.Drawing.Color.PowderBlue;
            this.Seven.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Seven.Location = new System.Drawing.Point(15, 227);
            this.Seven.Margin = new System.Windows.Forms.Padding(5);
            this.Seven.Name = "Seven";
            this.Seven.Size = new System.Drawing.Size(90, 90);
            this.Seven.TabIndex = 13;
            this.Seven.Text = "7";
            this.Seven.UseVisualStyleBackColor = false;
            this.Seven.Click += new System.EventHandler(this.Seven_Click);
            // 
            // Six
            // 
            this.Six.BackColor = System.Drawing.Color.PowderBlue;
            this.Six.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Six.Location = new System.Drawing.Point(215, 127);
            this.Six.Margin = new System.Windows.Forms.Padding(5);
            this.Six.Name = "Six";
            this.Six.Size = new System.Drawing.Size(90, 90);
            this.Six.TabIndex = 12;
            this.Six.Text = "6";
            this.Six.UseVisualStyleBackColor = false;
            this.Six.Click += new System.EventHandler(this.Six_Click);
            // 
            // Five
            // 
            this.Five.BackColor = System.Drawing.Color.PowderBlue;
            this.Five.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Five.Location = new System.Drawing.Point(115, 127);
            this.Five.Margin = new System.Windows.Forms.Padding(5);
            this.Five.Name = "Five";
            this.Five.Size = new System.Drawing.Size(90, 90);
            this.Five.TabIndex = 11;
            this.Five.Text = "5";
            this.Five.UseVisualStyleBackColor = false;
            this.Five.Click += new System.EventHandler(this.Five_Click);
            // 
            // THREE
            // 
            this.THREE.BackColor = System.Drawing.Color.PowderBlue;
            this.THREE.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.THREE.Location = new System.Drawing.Point(215, 27);
            this.THREE.Margin = new System.Windows.Forms.Padding(5);
            this.THREE.Name = "THREE";
            this.THREE.Size = new System.Drawing.Size(90, 90);
            this.THREE.TabIndex = 7;
            this.THREE.Text = "3";
            this.THREE.UseVisualStyleBackColor = false;
            this.THREE.Click += new System.EventHandler(this.THREE_Click);
            // 
            // TWO
            // 
            this.TWO.BackColor = System.Drawing.Color.PowderBlue;
            this.TWO.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TWO.Location = new System.Drawing.Point(115, 27);
            this.TWO.Margin = new System.Windows.Forms.Padding(5);
            this.TWO.Name = "TWO";
            this.TWO.Size = new System.Drawing.Size(90, 90);
            this.TWO.TabIndex = 6;
            this.TWO.Text = "2";
            this.TWO.UseVisualStyleBackColor = false;
            this.TWO.Click += new System.EventHandler(this.TWO_Click);
            // 
            // One
            // 
            this.One.BackColor = System.Drawing.Color.PowderBlue;
            this.One.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.One.Location = new System.Drawing.Point(15, 27);
            this.One.Margin = new System.Windows.Forms.Padding(5);
            this.One.Name = "One";
            this.One.Size = new System.Drawing.Size(90, 90);
            this.One.TabIndex = 5;
            this.One.Text = "1";
            this.One.UseVisualStyleBackColor = false;
            this.One.Click += new System.EventHandler(this.One_Click);
            // 
            // txtLocatin
            // 
            this.txtLocatin.Location = new System.Drawing.Point(10, 476);
            this.txtLocatin.Margin = new System.Windows.Forms.Padding(5);
            this.txtLocatin.Name = "txtLocatin";
            this.txtLocatin.Size = new System.Drawing.Size(95, 27);
            this.txtLocatin.TabIndex = 70;
            this.txtLocatin.Visible = false;
            // 
            // frmInOutDiory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(239)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1260, 713);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSumMovments);
            this.Controls.Add(this.cnbHazardousSubstances);
            this.Controls.Add(this.groupBoxBtuonsInOut);
            this.Controls.Add(this.lblLoction);
            this.Controls.Add(this.cnbLoction);
            this.Controls.Add(this.btnShowAllRecords);
            this.Controls.Add(this.lblInOutDiory);
            this.Controls.Add(this.cmbInOutDiory);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.dataGridInOutDiory);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmInOutDiory";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "יומן תנועות";
            this.Load += new System.EventHandler(this.frmInOutDiory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInOutDiory_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInOutDiory)).EndInit();
            this.groupBoxBtuonsInOut.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridInOutDiory;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.Button btnShowAllRecords;
        private System.Windows.Forms.Label lblInOutDiory;
        private System.Windows.Forms.ComboBox cmbInOutDiory;
        private System.Windows.Forms.Label lblLoction;
        private System.Windows.Forms.ComboBox cnbLoction;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBoxBtuonsInOut;
        private System.Windows.Forms.Button btnInformation;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cnbHazardousSubstances;
        private System.Windows.Forms.TextBox txtSumMovments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerInOutDiory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DELETE;
        private System.Windows.Forms.Button Zero;
        private System.Windows.Forms.Button Eight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button Four;
        private System.Windows.Forms.Button Nine;
        private System.Windows.Forms.Button Seven;
        private System.Windows.Forms.Button Six;
        private System.Windows.Forms.Button Five;
        private System.Windows.Forms.Button THREE;
        private System.Windows.Forms.Button TWO;
        private System.Windows.Forms.Button One;
        private System.Windows.Forms.TextBox txtLocatin;
    }
}
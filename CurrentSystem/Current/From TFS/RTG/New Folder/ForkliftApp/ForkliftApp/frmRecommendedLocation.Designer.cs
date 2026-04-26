namespace ForkliftApp
{
    partial class frmRecommendedLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblLocationExpected = new System.Windows.Forms.Label();
            this.txtRecommendedLocation = new System.Windows.Forms.TextBox();
            this.cmbContainerTypeCode = new System.Windows.Forms.ComboBox();
            this.cmbContainerLength = new System.Windows.Forms.ComboBox();
            this.lblContainerLength = new System.Windows.Forms.Label();
            this.cmbClientCode = new System.Windows.Forms.ComboBox();
            this.lblShipAgentCode = new System.Windows.Forms.Label();
            this.cmbShipingLine = new System.Windows.Forms.ComboBox();
            this.lblHandlingTypeCode = new System.Windows.Forms.Label();
            this.lblContainerTypeCode = new System.Windows.Forms.Label();
            this.lblShipingLine = new System.Windows.Forms.Label();
            this.groupBoxBtuonsInOut = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnInformation = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dataGridRecommendedLocation = new System.Windows.Forms.DataGridView();
            this.txtHandlingTypeCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLocatin = new System.Windows.Forms.TextBox();
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
            this.btnA = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.groupBoxBtuonsInOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRecommendedLocation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLocationExpected
            // 
            this.lblLocationExpected.AutoSize = true;
            this.lblLocationExpected.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationExpected.ForeColor = System.Drawing.Color.Black;
            this.lblLocationExpected.Location = new System.Drawing.Point(128, 3);
            this.lblLocationExpected.Name = "lblLocationExpected";
            this.lblLocationExpected.Size = new System.Drawing.Size(174, 29);
            this.lblLocationExpected.TabIndex = 101;
            this.lblLocationExpected.Text = ":איתור מומלץ";
            // 
            // txtRecommendedLocation
            // 
            this.txtRecommendedLocation.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecommendedLocation.Location = new System.Drawing.Point(133, 35);
            this.txtRecommendedLocation.Name = "txtRecommendedLocation";
            this.txtRecommendedLocation.Size = new System.Drawing.Size(169, 52);
            this.txtRecommendedLocation.TabIndex = 100;
            this.txtRecommendedLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbContainerTypeCode
            // 
            this.cmbContainerTypeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmbContainerTypeCode.FormattingEnabled = true;
            this.cmbContainerTypeCode.Location = new System.Drawing.Point(626, 36);
            this.cmbContainerTypeCode.Name = "cmbContainerTypeCode";
            this.cmbContainerTypeCode.Size = new System.Drawing.Size(95, 50);
            this.cmbContainerTypeCode.TabIndex = 95;
            this.cmbContainerTypeCode.Click += new System.EventHandler(this.cmbContainerTypeCode_Click);
            // 
            // cmbContainerLength
            // 
            this.cmbContainerLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmbContainerLength.FormattingEnabled = true;
            this.cmbContainerLength.Location = new System.Drawing.Point(751, 35);
            this.cmbContainerLength.Name = "cmbContainerLength";
            this.cmbContainerLength.Size = new System.Drawing.Size(101, 50);
            this.cmbContainerLength.TabIndex = 94;
            this.cmbContainerLength.SelectedIndexChanged += new System.EventHandler(this.cmbContainerLength_SelectedIndexChanged);
            this.cmbContainerLength.Click += new System.EventHandler(this.cmbContainerLength_Click);
            // 
            // lblContainerLength
            // 
            this.lblContainerLength.AutoSize = true;
            this.lblContainerLength.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblContainerLength.Location = new System.Drawing.Point(776, 3);
            this.lblContainerLength.Name = "lblContainerLength";
            this.lblContainerLength.Size = new System.Drawing.Size(76, 29);
            this.lblContainerLength.TabIndex = 93;
            this.lblContainerLength.Text = ":גודל";
            // 
            // cmbClientCode
            // 
            this.cmbClientCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmbClientCode.FormattingEnabled = true;
            this.cmbClientCode.Location = new System.Drawing.Point(906, 35);
            this.cmbClientCode.Name = "cmbClientCode";
            this.cmbClientCode.Size = new System.Drawing.Size(313, 45);
            this.cmbClientCode.TabIndex = 91;
            this.cmbClientCode.SelectedIndexChanged += new System.EventHandler(this.cmbClientCode_SelectedIndexChanged);
            this.cmbClientCode.Click += new System.EventHandler(this.cmbClientCode_Click);
            // 
            // lblShipAgentCode
            // 
            this.lblShipAgentCode.AutoSize = true;
            this.lblShipAgentCode.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblShipAgentCode.Location = new System.Drawing.Point(1137, 3);
            this.lblShipAgentCode.Name = "lblShipAgentCode";
            this.lblShipAgentCode.Size = new System.Drawing.Size(82, 29);
            this.lblShipAgentCode.TabIndex = 92;
            this.lblShipAgentCode.Text = ":לקוח";
            // 
            // cmbShipingLine
            // 
            this.cmbShipingLine.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShipingLine.FormattingEnabled = true;
            this.cmbShipingLine.Location = new System.Drawing.Point(351, 36);
            this.cmbShipingLine.Name = "cmbShipingLine";
            this.cmbShipingLine.Size = new System.Drawing.Size(124, 53);
            this.cmbShipingLine.TabIndex = 98;
            this.cmbShipingLine.Click += new System.EventHandler(this.cmbShipingLine_Click);
            // 
            // lblHandlingTypeCode
            // 
            this.lblHandlingTypeCode.AutoSize = true;
            this.lblHandlingTypeCode.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHandlingTypeCode.Location = new System.Drawing.Point(512, 4);
            this.lblHandlingTypeCode.Name = "lblHandlingTypeCode";
            this.lblHandlingTypeCode.Size = new System.Drawing.Size(90, 29);
            this.lblHandlingTypeCode.TabIndex = 97;
            this.lblHandlingTypeCode.Text = ":טיפול";
            // 
            // lblContainerTypeCode
            // 
            this.lblContainerTypeCode.AutoSize = true;
            this.lblContainerTypeCode.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblContainerTypeCode.Location = new System.Drawing.Point(658, 4);
            this.lblContainerTypeCode.Name = "lblContainerTypeCode";
            this.lblContainerTypeCode.Size = new System.Drawing.Size(63, 29);
            this.lblContainerTypeCode.TabIndex = 96;
            this.lblContainerTypeCode.Text = ":סוג";
            // 
            // lblShipingLine
            // 
            this.lblShipingLine.AutoSize = true;
            this.lblShipingLine.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipingLine.ForeColor = System.Drawing.Color.Black;
            this.lblShipingLine.Location = new System.Drawing.Point(358, 4);
            this.lblShipingLine.Name = "lblShipingLine";
            this.lblShipingLine.Size = new System.Drawing.Size(130, 29);
            this.lblShipingLine.TabIndex = 99;
            this.lblShipingLine.Text = ":קו ספנות";
            // 
            // groupBoxBtuonsInOut
            // 
            this.groupBoxBtuonsInOut.Controls.Add(this.btnExit);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnInformation);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnQuery);
            this.groupBoxBtuonsInOut.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBoxBtuonsInOut.Location = new System.Drawing.Point(363, 605);
            this.groupBoxBtuonsInOut.Name = "groupBoxBtuonsInOut";
            this.groupBoxBtuonsInOut.Size = new System.Drawing.Size(858, 106);
            this.groupBoxBtuonsInOut.TabIndex = 104;
            this.groupBoxBtuonsInOut.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(40, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(144, 86);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "יציאה   F10";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnInformation
            // 
            this.btnInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInformation.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnInformation.ForeColor = System.Drawing.Color.Black;
            this.btnInformation.Location = new System.Drawing.Point(345, 15);
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(144, 86);
            this.btnInformation.TabIndex = 2;
            this.btnInformation.Text = "מידע    F5";
            this.btnInformation.UseVisualStyleBackColor = false;
            this.btnInformation.Click += new System.EventHandler(this.btnInformation_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ForeColor = System.Drawing.Color.Black;
            this.btnQuery.Location = new System.Drawing.Point(659, 15);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(144, 86);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "שאילתה   F3";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dataGridRecommendedLocation
            // 
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridRecommendedLocation.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridRecommendedLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRecommendedLocation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridRecommendedLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridRecommendedLocation.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridRecommendedLocation.Location = new System.Drawing.Point(363, 96);
            this.dataGridRecommendedLocation.MultiSelect = false;
            this.dataGridRecommendedLocation.Name = "dataGridRecommendedLocation";
            this.dataGridRecommendedLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridRecommendedLocation.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRecommendedLocation.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridRecommendedLocation.RowHeadersVisible = false;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dataGridRecommendedLocation.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridRecommendedLocation.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridRecommendedLocation.RowTemplate.Height = 35;
            this.dataGridRecommendedLocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridRecommendedLocation.Size = new System.Drawing.Size(856, 509);
            this.dataGridRecommendedLocation.TabIndex = 103;
            // 
            // txtHandlingTypeCode
            // 
            this.txtHandlingTypeCode.Enabled = false;
            this.txtHandlingTypeCode.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtHandlingTypeCode.Location = new System.Drawing.Point(517, 36);
            this.txtHandlingTypeCode.Name = "txtHandlingTypeCode";
            this.txtHandlingTypeCode.Size = new System.Drawing.Size(78, 52);
            this.txtHandlingTypeCode.TabIndex = 105;
            this.txtHandlingTypeCode.Text = "EM";
            this.txtHandlingTypeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnD);
            this.groupBox1.Controls.Add(this.btnA);
            this.groupBox1.Controls.Add(this.txtLocatin);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 605);
            this.groupBox1.TabIndex = 106;
            this.groupBox1.TabStop = false;
            // 
            // txtLocatin
            // 
            this.txtLocatin.Location = new System.Drawing.Point(6, 574);
            this.txtLocatin.Name = "txtLocatin";
            this.txtLocatin.Size = new System.Drawing.Size(100, 20);
            this.txtLocatin.TabIndex = 70;
            this.txtLocatin.Visible = false;
            // 
            // DELETE
            // 
            this.DELETE.BackColor = System.Drawing.Color.Coral;
            this.DELETE.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DELETE.Location = new System.Drawing.Point(122, 504);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(199, 90);
            this.DELETE.TabIndex = 18;
            this.DELETE.Tag = "Back";
            this.DELETE.Text = "מחק";
            this.DELETE.UseVisualStyleBackColor = false;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // Zero
            // 
            this.Zero.BackColor = System.Drawing.Color.PowderBlue;
            this.Zero.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Zero.Location = new System.Drawing.Point(16, 313);
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
            this.Eight.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Eight.Location = new System.Drawing.Point(122, 216);
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
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(122, 408);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(199, 90);
            this.btnOK.TabIndex = 19;
            this.btnOK.Tag = "Back";
            this.btnOK.Text = "אישור";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Four
            // 
            this.Four.BackColor = System.Drawing.Color.PowderBlue;
            this.Four.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Four.Location = new System.Drawing.Point(15, 117);
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
            this.Nine.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nine.Location = new System.Drawing.Point(231, 216);
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
            this.Seven.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Seven.Location = new System.Drawing.Point(16, 216);
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
            this.Six.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Six.Location = new System.Drawing.Point(231, 117);
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
            this.Five.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Five.Location = new System.Drawing.Point(122, 117);
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
            this.THREE.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.THREE.Location = new System.Drawing.Point(231, 19);
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
            this.TWO.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TWO.Location = new System.Drawing.Point(122, 19);
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
            this.One.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.One.Location = new System.Drawing.Point(15, 19);
            this.One.Name = "One";
            this.One.Size = new System.Drawing.Size(90, 90);
            this.One.TabIndex = 5;
            this.One.Text = "1";
            this.One.UseVisualStyleBackColor = false;
            this.One.Click += new System.EventHandler(this.One_Click);
            // 
            // btnA
            // 
            this.btnA.BackColor = System.Drawing.Color.Thistle;
            this.btnA.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnA.Location = new System.Drawing.Point(122, 313);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(90, 90);
            this.btnA.TabIndex = 71;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = false;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnD
            // 
            this.btnD.BackColor = System.Drawing.Color.Thistle;
            this.btnD.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnD.Location = new System.Drawing.Point(231, 313);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(90, 90);
            this.btnD.TabIndex = 72;
            this.btnD.Text = "D";
            this.btnD.UseVisualStyleBackColor = false;
            this.btnD.Click += new System.EventHandler(this.btnD_Click);
            // 
            // frmRecommendedLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 713);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLocationExpected);
            this.Controls.Add(this.txtRecommendedLocation);
            this.Controls.Add(this.cmbContainerTypeCode);
            this.Controls.Add(this.cmbContainerLength);
            this.Controls.Add(this.lblContainerLength);
            this.Controls.Add(this.cmbClientCode);
            this.Controls.Add(this.lblShipAgentCode);
            this.Controls.Add(this.cmbShipingLine);
            this.Controls.Add(this.lblHandlingTypeCode);
            this.Controls.Add(this.lblContainerTypeCode);
            this.Controls.Add(this.lblShipingLine);
            this.Controls.Add(this.groupBoxBtuonsInOut);
            this.Controls.Add(this.dataGridRecommendedLocation);
            this.Controls.Add(this.txtHandlingTypeCode);
            this.Name = "frmRecommendedLocation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "איתורים מומלצים";
            this.groupBoxBtuonsInOut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRecommendedLocation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLocationExpected;
        private System.Windows.Forms.TextBox txtRecommendedLocation;
        private System.Windows.Forms.ComboBox cmbContainerTypeCode;
        private System.Windows.Forms.ComboBox cmbContainerLength;
        private System.Windows.Forms.Label lblContainerLength;
        private System.Windows.Forms.ComboBox cmbClientCode;
        private System.Windows.Forms.Label lblShipAgentCode;
        private System.Windows.Forms.ComboBox cmbShipingLine;
        private System.Windows.Forms.Label lblHandlingTypeCode;
        private System.Windows.Forms.Label lblContainerTypeCode;
        private System.Windows.Forms.Label lblShipingLine;
        private System.Windows.Forms.GroupBox groupBoxBtuonsInOut;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnInformation;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dataGridRecommendedLocation;
        private System.Windows.Forms.TextBox txtHandlingTypeCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLocatin;
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
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Button btnA;
    }
}
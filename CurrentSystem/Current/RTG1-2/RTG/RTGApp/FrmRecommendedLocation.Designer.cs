namespace RTGApp
{
    partial class FrmRecommendedLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.txtHandlingTypeCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
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
            this.dataGridRecommendedLocation = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRecommendedLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLocationExpected
            // 
            this.lblLocationExpected.AutoSize = true;
            this.lblLocationExpected.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.lblLocationExpected.ForeColor = System.Drawing.Color.Navy;
            this.lblLocationExpected.Location = new System.Drawing.Point(380, 11);
            this.lblLocationExpected.Name = "lblLocationExpected";
            this.lblLocationExpected.Size = new System.Drawing.Size(137, 26);
            this.lblLocationExpected.TabIndex = 116;
            this.lblLocationExpected.Text = "איתור מומלץ";
            // 
            // txtRecommendedLocation
            // 
            this.txtRecommendedLocation.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.txtRecommendedLocation.Location = new System.Drawing.Point(363, 40);
            this.txtRecommendedLocation.Name = "txtRecommendedLocation";
            this.txtRecommendedLocation.Size = new System.Drawing.Size(169, 40);
            this.txtRecommendedLocation.TabIndex = 115;
            this.txtRecommendedLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbContainerTypeCode
            // 
            this.cmbContainerTypeCode.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.cmbContainerTypeCode.FormattingEnabled = true;
            this.cmbContainerTypeCode.Items.AddRange(new object[] {
            "",
            "FL",
            "HC",
            "OT",
            "RF",
            "RG",
            "RH",
            "TK",
            "PW"});
            this.cmbContainerTypeCode.Location = new System.Drawing.Point(764, 41);
            this.cmbContainerTypeCode.Name = "cmbContainerTypeCode";
            this.cmbContainerTypeCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbContainerTypeCode.Size = new System.Drawing.Size(95, 40);
            this.cmbContainerTypeCode.TabIndex = 110;
            this.cmbContainerTypeCode.Click += new System.EventHandler(this.cmbContainerTypeCode_Click);
            // 
            // cmbContainerLength
            // 
            this.cmbContainerLength.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.cmbContainerLength.FormattingEnabled = true;
            this.cmbContainerLength.Items.AddRange(new object[] {
            "",
            "20",
            "40",
            "45"});
            this.cmbContainerLength.Location = new System.Drawing.Point(872, 40);
            this.cmbContainerLength.Name = "cmbContainerLength";
            this.cmbContainerLength.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbContainerLength.Size = new System.Drawing.Size(101, 40);
            this.cmbContainerLength.TabIndex = 109;
            this.cmbContainerLength.Click += new System.EventHandler(this.cmbContainerLength_Click);
            // 
            // lblContainerLength
            // 
            this.lblContainerLength.AutoSize = true;
            this.lblContainerLength.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.lblContainerLength.ForeColor = System.Drawing.Color.Navy;
            this.lblContainerLength.Location = new System.Drawing.Point(906, 11);
            this.lblContainerLength.Name = "lblContainerLength";
            this.lblContainerLength.Size = new System.Drawing.Size(56, 26);
            this.lblContainerLength.TabIndex = 108;
            this.lblContainerLength.Text = "גודל";
            // 
            // cmbClientCode
            // 
            this.cmbClientCode.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.cmbClientCode.FormattingEnabled = true;
            this.cmbClientCode.Location = new System.Drawing.Point(983, 40);
            this.cmbClientCode.Name = "cmbClientCode";
            this.cmbClientCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbClientCode.Size = new System.Drawing.Size(313, 40);
            this.cmbClientCode.TabIndex = 106;
            this.cmbClientCode.SelectedIndexChanged += new System.EventHandler(this.cmbClientCode_SelectedIndexChanged);
            this.cmbClientCode.Click += new System.EventHandler(this.cmbClientCode_Click);
            // 
            // lblShipAgentCode
            // 
            this.lblShipAgentCode.AutoSize = true;
            this.lblShipAgentCode.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.lblShipAgentCode.ForeColor = System.Drawing.Color.Navy;
            this.lblShipAgentCode.Location = new System.Drawing.Point(1230, 11);
            this.lblShipAgentCode.Name = "lblShipAgentCode";
            this.lblShipAgentCode.Size = new System.Drawing.Size(61, 26);
            this.lblShipAgentCode.TabIndex = 107;
            this.lblShipAgentCode.Text = "לקוח";
            // 
            // cmbShipingLine
            // 
            this.cmbShipingLine.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.cmbShipingLine.FormattingEnabled = true;
            this.cmbShipingLine.Location = new System.Drawing.Point(543, 41);
            this.cmbShipingLine.Name = "cmbShipingLine";
            this.cmbShipingLine.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbShipingLine.Size = new System.Drawing.Size(124, 40);
            this.cmbShipingLine.TabIndex = 113;
            this.cmbShipingLine.Click += new System.EventHandler(this.cmbShipingLine_Click);
            // 
            // lblHandlingTypeCode
            // 
            this.lblHandlingTypeCode.AutoSize = true;
            this.lblHandlingTypeCode.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.lblHandlingTypeCode.ForeColor = System.Drawing.Color.Navy;
            this.lblHandlingTypeCode.Location = new System.Drawing.Point(686, 11);
            this.lblHandlingTypeCode.Name = "lblHandlingTypeCode";
            this.lblHandlingTypeCode.Size = new System.Drawing.Size(68, 26);
            this.lblHandlingTypeCode.TabIndex = 112;
            this.lblHandlingTypeCode.Text = "טיפול";
            // 
            // lblContainerTypeCode
            // 
            this.lblContainerTypeCode.AutoSize = true;
            this.lblContainerTypeCode.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.lblContainerTypeCode.ForeColor = System.Drawing.Color.Navy;
            this.lblContainerTypeCode.Location = new System.Drawing.Point(792, 11);
            this.lblContainerTypeCode.Name = "lblContainerTypeCode";
            this.lblContainerTypeCode.Size = new System.Drawing.Size(46, 26);
            this.lblContainerTypeCode.TabIndex = 111;
            this.lblContainerTypeCode.Text = "סוג";
            // 
            // lblShipingLine
            // 
            this.lblShipingLine.AutoSize = true;
            this.lblShipingLine.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.lblShipingLine.ForeColor = System.Drawing.Color.Navy;
            this.lblShipingLine.Location = new System.Drawing.Point(555, 11);
            this.lblShipingLine.Name = "lblShipingLine";
            this.lblShipingLine.Size = new System.Drawing.Size(102, 26);
            this.lblShipingLine.TabIndex = 114;
            this.lblShipingLine.Text = "קו ספנות";
            // 
            // txtHandlingTypeCode
            // 
            this.txtHandlingTypeCode.Enabled = false;
            this.txtHandlingTypeCode.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.txtHandlingTypeCode.Location = new System.Drawing.Point(676, 41);
            this.txtHandlingTypeCode.Name = "txtHandlingTypeCode";
            this.txtHandlingTypeCode.Size = new System.Drawing.Size(78, 40);
            this.txtHandlingTypeCode.TabIndex = 117;
            this.txtHandlingTypeCode.Text = "EM";
            this.txtHandlingTypeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnD);
            this.groupBox1.Controls.Add(this.btnA);
            this.groupBox1.Controls.Add(this.txtLocation);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 605);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(6, 518);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(146, 81);
            this.btnExit.TabIndex = 120;
            this.btnExit.Text = "יציאה";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.txtLocation.Location = new System.Drawing.Point(200, 518);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(121, 40);
            this.txtLocation.TabIndex = 70;
            // 
            // DELETE
            // 
            this.DELETE.BackColor = System.Drawing.Color.Coral;
            this.DELETE.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DELETE.Location = new System.Drawing.Point(15, 409);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(147, 90);
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
            this.btnOK.Location = new System.Drawing.Point(174, 408);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(147, 90);
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
            // dataGridRecommendedLocation
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridRecommendedLocation.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridRecommendedLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRecommendedLocation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridRecommendedLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridRecommendedLocation.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridRecommendedLocation.Location = new System.Drawing.Point(363, 86);
            this.dataGridRecommendedLocation.MultiSelect = false;
            this.dataGridRecommendedLocation.Name = "dataGridRecommendedLocation";
            this.dataGridRecommendedLocation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridRecommendedLocation.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRecommendedLocation.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridRecommendedLocation.RowHeadersVisible = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dataGridRecommendedLocation.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridRecommendedLocation.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridRecommendedLocation.RowTemplate.Height = 35;
            this.dataGridRecommendedLocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridRecommendedLocation.Size = new System.Drawing.Size(933, 509);
            this.dataGridRecommendedLocation.TabIndex = 119;
            // 
            // FrmRecommendedLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1307, 632);
            this.Controls.Add(this.dataGridRecommendedLocation);
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
            this.Controls.Add(this.txtHandlingTypeCode);
            this.Name = "FrmRecommendedLocation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "איתורים מומלצים";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRecommendedLocation)).EndInit();
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
        private System.Windows.Forms.TextBox txtHandlingTypeCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.TextBox txtLocation;
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
        private System.Windows.Forms.DataGridView dataGridRecommendedLocation;
        private System.Windows.Forms.Button btnExit;
    }
}
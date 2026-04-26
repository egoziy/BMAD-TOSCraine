namespace ForkliftApp
{
    partial class frmEmptyContainers
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
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
         this.dataGridEMContainers = new System.Windows.Forms.DataGridView();
         this.cmbShipingLine = new System.Windows.Forms.ComboBox();
         this.lblShipingLine = new System.Windows.Forms.Label();
         this.btnShowAllRecords = new System.Windows.Forms.Button();
         this.groupBoxBtuonsInOut = new System.Windows.Forms.GroupBox();
         this.btnExit = new System.Windows.Forms.Button();
         this.btnInformation = new System.Windows.Forms.Button();
         this.btnSelect = new System.Windows.Forms.Button();
         this.btnQuery = new System.Windows.Forms.Button();
         this.lblNoData = new System.Windows.Forms.Label();
         this.timerfrmEMContainers = new System.Windows.Forms.Timer(this.components);
         this.txtSumContainers = new System.Windows.Forms.TextBox();
         this.lbl40 = new System.Windows.Forms.Label();
         this.lbl20 = new System.Windows.Forms.Label();
         this.btn40 = new System.Windows.Forms.Button();
         this.btn20 = new System.Windows.Forms.Button();
         this.cmbcontainerType = new System.Windows.Forms.ComboBox();
         this.cOContainersDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.lblContainerType = new System.Windows.Forms.Label();
         this.cmbContainerLength = new System.Windows.Forms.ComboBox();
         this.lblContainerLength = new System.Windows.Forms.Label();
         this.cmbHandlingTypeCode = new System.Windows.Forms.ComboBox();
         this.lblHandlingTypeCode = new System.Windows.Forms.Label();
         this.terminalDataDataSet1 = new ForkliftApp.TerminalDataDataSet1();
         this.tCContainerTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.tC_ContainerTypeTableAdapter = new ForkliftApp.TerminalDataDataSet1TableAdapters.TC_ContainerTypeTableAdapter();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridEMContainers)).BeginInit();
         this.groupBoxBtuonsInOut.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.cOContainersDataTableBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.terminalDataDataSet1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.tCContainerTypeBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // dataGridEMContainers
         // 
         dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.dataGridEMContainers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
         this.dataGridEMContainers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
         dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
         dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
         dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
         this.dataGridEMContainers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
         this.dataGridEMContainers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridEMContainers.Cursor = System.Windows.Forms.Cursors.Default;
         this.dataGridEMContainers.Location = new System.Drawing.Point(55, 71);
         this.dataGridEMContainers.Name = "dataGridEMContainers";
         this.dataGridEMContainers.RowHeadersVisible = false;
         dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.dataGridEMContainers.RowsDefaultCellStyle = dataGridViewCellStyle3;
         this.dataGridEMContainers.RowTemplate.Height = 35;
         this.dataGridEMContainers.Size = new System.Drawing.Size(1151, 495);
         this.dataGridEMContainers.TabIndex = 0;
         // 
         // cmbShipingLine
         // 
         this.cmbShipingLine.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.cmbShipingLine.FormattingEnabled = true;
         this.cmbShipingLine.Location = new System.Drawing.Point(198, 12);
         this.cmbShipingLine.Name = "cmbShipingLine";
         this.cmbShipingLine.Size = new System.Drawing.Size(152, 47);
         this.cmbShipingLine.TabIndex = 1;
         this.cmbShipingLine.SelectedValueChanged += new System.EventHandler(this.cmbShipingLine_SelectedValueChanged);
         this.cmbShipingLine.Click += new System.EventHandler(this.cmbShipingLine_Click);
         // 
         // lblShipingLine
         // 
         this.lblShipingLine.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lblShipingLine.ForeColor = System.Drawing.Color.Navy;
         this.lblShipingLine.Location = new System.Drawing.Point(56, 25);
         this.lblShipingLine.Name = "lblShipingLine";
         this.lblShipingLine.Size = new System.Drawing.Size(136, 33);
         this.lblShipingLine.TabIndex = 2;
         this.lblShipingLine.Text = "קו ספנות";
         // 
         // btnShowAllRecords
         // 
         this.btnShowAllRecords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btnShowAllRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
         this.btnShowAllRecords.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btnShowAllRecords.ForeColor = System.Drawing.Color.Navy;
         this.btnShowAllRecords.Location = new System.Drawing.Point(1002, 15);
         this.btnShowAllRecords.Name = "btnShowAllRecords";
         this.btnShowAllRecords.Size = new System.Drawing.Size(204, 51);
         this.btnShowAllRecords.TabIndex = 3;
         this.btnShowAllRecords.Text = "הכל   F9";
         this.btnShowAllRecords.UseVisualStyleBackColor = false;
         this.btnShowAllRecords.Click += new System.EventHandler(this.btnShowAllRecords_Click);
         // 
         // groupBoxBtuonsInOut
         // 
         this.groupBoxBtuonsInOut.Controls.Add(this.btnExit);
         this.groupBoxBtuonsInOut.Controls.Add(this.btnInformation);
         this.groupBoxBtuonsInOut.Controls.Add(this.btnSelect);
         this.groupBoxBtuonsInOut.Controls.Add(this.btnQuery);
         this.groupBoxBtuonsInOut.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.groupBoxBtuonsInOut.Location = new System.Drawing.Point(55, 606);
         this.groupBoxBtuonsInOut.Name = "groupBoxBtuonsInOut";
         this.groupBoxBtuonsInOut.Size = new System.Drawing.Size(1151, 107);
         this.groupBoxBtuonsInOut.TabIndex = 48;
         this.groupBoxBtuonsInOut.TabStop = false;
         // 
         // btnExit
         // 
         this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
         this.btnExit.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btnExit.ForeColor = System.Drawing.Color.Navy;
         this.btnExit.Location = new System.Drawing.Point(84, 19);
         this.btnExit.Name = "btnExit";
         this.btnExit.Size = new System.Drawing.Size(146, 81);
         this.btnExit.TabIndex = 3;
         this.btnExit.Text = "יציאה F10";
         this.btnExit.UseVisualStyleBackColor = false;
         this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
         // 
         // btnInformation
         // 
         this.btnInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
         this.btnInformation.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btnInformation.ForeColor = System.Drawing.Color.Navy;
         this.btnInformation.Location = new System.Drawing.Point(363, 19);
         this.btnInformation.Name = "btnInformation";
         this.btnInformation.Size = new System.Drawing.Size(142, 81);
         this.btnInformation.TabIndex = 2;
         this.btnInformation.Text = "מידע   F5";
         this.btnInformation.UseVisualStyleBackColor = false;
         this.btnInformation.Click += new System.EventHandler(this.btnInformation_Click);
         // 
         // btnSelect
         // 
         this.btnSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
         this.btnSelect.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btnSelect.ForeColor = System.Drawing.Color.Navy;
         this.btnSelect.Location = new System.Drawing.Point(912, 19);
         this.btnSelect.Name = "btnSelect";
         this.btnSelect.Size = new System.Drawing.Size(144, 81);
         this.btnSelect.TabIndex = 51;
         this.btnSelect.Text = "אישור  F8";
         this.btnSelect.UseVisualStyleBackColor = false;
         this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
         // 
         // btnQuery
         // 
         this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
         this.btnQuery.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btnQuery.ForeColor = System.Drawing.Color.Navy;
         this.btnQuery.Location = new System.Drawing.Point(632, 19);
         this.btnQuery.Name = "btnQuery";
         this.btnQuery.Size = new System.Drawing.Size(175, 81);
         this.btnQuery.TabIndex = 0;
         this.btnQuery.Text = "שאילתה F3";
         this.btnQuery.UseVisualStyleBackColor = false;
         this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
         // 
         // lblNoData
         // 
         this.lblNoData.AutoEllipsis = true;
         this.lblNoData.AutoSize = true;
         this.lblNoData.Font = new System.Drawing.Font("Tahoma", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
         this.lblNoData.Location = new System.Drawing.Point(153, 167);
         this.lblNoData.Name = "lblNoData";
         this.lblNoData.Size = new System.Drawing.Size(534, 116);
         this.lblNoData.TabIndex = 54;
         this.lblNoData.Text = "אין נתונים";
         this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lblNoData.Visible = false;
         // 
         // txtSumContainers
         // 
         this.txtSumContainers.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.txtSumContainers.Location = new System.Drawing.Point(55, 572);
         this.txtSumContainers.Name = "txtSumContainers";
         this.txtSumContainers.Size = new System.Drawing.Size(502, 33);
         this.txtSumContainers.TabIndex = 56;
         this.txtSumContainers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // lbl40
         // 
         this.lbl40.AutoSize = true;
         this.lbl40.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lbl40.ForeColor = System.Drawing.Color.Navy;
         this.lbl40.Location = new System.Drawing.Point(928, 581);
         this.lbl40.Name = "lbl40";
         this.lbl40.Size = new System.Drawing.Size(63, 25);
         this.lbl40.TabIndex = 60;
         this.lbl40.Text = "{F2}";
         this.lbl40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lbl40.Visible = false;
         // 
         // lbl20
         // 
         this.lbl20.AutoSize = true;
         this.lbl20.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lbl20.ForeColor = System.Drawing.Color.Navy;
         this.lbl20.Location = new System.Drawing.Point(805, 580);
         this.lbl20.Name = "lbl20";
         this.lbl20.Size = new System.Drawing.Size(63, 25);
         this.lbl20.TabIndex = 59;
         this.lbl20.Text = "{F1}";
         this.lbl20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.lbl20.Visible = false;
         // 
         // btn40
         // 
         this.btn40.AutoSize = true;
         this.btn40.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btn40.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btn40.ForeColor = System.Drawing.Color.Navy;
         this.btn40.Location = new System.Drawing.Point(874, 577);
         this.btn40.Name = "btn40";
         this.btn40.Size = new System.Drawing.Size(48, 35);
         this.btn40.TabIndex = 58;
         this.btn40.Text = "40";
         this.btn40.UseVisualStyleBackColor = true;
         this.btn40.Visible = false;
         this.btn40.Click += new System.EventHandler(this.btn40_Click);
         // 
         // btn20
         // 
         this.btn20.AutoSize = true;
         this.btn20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.btn20.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.btn20.ForeColor = System.Drawing.Color.Navy;
         this.btn20.Location = new System.Drawing.Point(751, 577);
         this.btn20.Name = "btn20";
         this.btn20.Size = new System.Drawing.Size(48, 35);
         this.btn20.TabIndex = 57;
         this.btn20.Text = "20";
         this.btn20.UseVisualStyleBackColor = true;
         this.btn20.Visible = false;
         this.btn20.Click += new System.EventHandler(this.btn20_Click);
         // 
         // cmbcontainerType
         // 
         this.cmbcontainerType.Cursor = System.Windows.Forms.Cursors.Default;
         this.cmbcontainerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.cmbcontainerType.FormattingEnabled = true;
         this.cmbcontainerType.Location = new System.Drawing.Point(625, 14);
         this.cmbcontainerType.Name = "cmbcontainerType";
         this.cmbcontainerType.Size = new System.Drawing.Size(96, 45);
         this.cmbcontainerType.TabIndex = 61;
         this.cmbcontainerType.SelectedValueChanged += new System.EventHandler(this.cmbcontainerType_SelectedValueChanged);
         this.cmbcontainerType.Click += new System.EventHandler(this.cmbcontainerType_Click);
         // 
         // cOContainersDataTableBindingSource
         // 
         this.cOContainersDataTableBindingSource.DataSource = typeof(ForkliftApp.Entities.ForkliftAppDS.CO_ContainersDataTable);
         // 
         // lblContainerType
         // 
         this.lblContainerType.AutoSize = true;
         this.lblContainerType.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lblContainerType.ForeColor = System.Drawing.Color.Navy;
         this.lblContainerType.Location = new System.Drawing.Point(558, 25);
         this.lblContainerType.Name = "lblContainerType";
         this.lblContainerType.Size = new System.Drawing.Size(61, 33);
         this.lblContainerType.TabIndex = 62;
         this.lblContainerType.Text = "סוג";
         this.lblContainerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // cmbContainerLength
         // 
         this.cmbContainerLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.cmbContainerLength.FormattingEnabled = true;
         this.cmbContainerLength.Items.AddRange(new object[] {
            "",
            "20",
            "40",
            "45"});
         this.cmbContainerLength.Location = new System.Drawing.Point(440, 14);
         this.cmbContainerLength.Name = "cmbContainerLength";
         this.cmbContainerLength.Size = new System.Drawing.Size(97, 45);
         this.cmbContainerLength.TabIndex = 63;
         this.cmbContainerLength.SelectedValueChanged += new System.EventHandler(this.cmbContainerLength_SelectedValueChanged);
         this.cmbContainerLength.Click += new System.EventHandler(this.cmbContainerLength_Click);
         // 
         // lblContainerLength
         // 
         this.lblContainerLength.AutoSize = true;
         this.lblContainerLength.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lblContainerLength.ForeColor = System.Drawing.Color.Navy;
         this.lblContainerLength.Location = new System.Drawing.Point(363, 25);
         this.lblContainerLength.Name = "lblContainerLength";
         this.lblContainerLength.Size = new System.Drawing.Size(76, 33);
         this.lblContainerLength.TabIndex = 64;
         this.lblContainerLength.Text = "גודל";
         this.lblContainerLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // cmbHandlingTypeCode
         // 
         this.cmbHandlingTypeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.cmbHandlingTypeCode.FormattingEnabled = true;
         this.cmbHandlingTypeCode.Items.AddRange(new object[] {
            "",
            "HH",
            "PP",
            "FR",
            "EX"});
         this.cmbHandlingTypeCode.Location = new System.Drawing.Point(842, 15);
         this.cmbHandlingTypeCode.Name = "cmbHandlingTypeCode";
         this.cmbHandlingTypeCode.Size = new System.Drawing.Size(96, 45);
         this.cmbHandlingTypeCode.TabIndex = 65;
         this.cmbHandlingTypeCode.SelectedValueChanged += new System.EventHandler(this.cmbHandlingTypeCode_SelectedValueChanged);
         this.cmbHandlingTypeCode.Click += new System.EventHandler(this.cmbHandlingTypeCode_Click);
         // 
         // lblHandlingTypeCode
         // 
         this.lblHandlingTypeCode.AutoSize = true;
         this.lblHandlingTypeCode.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
         this.lblHandlingTypeCode.ForeColor = System.Drawing.Color.Navy;
         this.lblHandlingTypeCode.Location = new System.Drawing.Point(745, 25);
         this.lblHandlingTypeCode.Name = "lblHandlingTypeCode";
         this.lblHandlingTypeCode.Size = new System.Drawing.Size(91, 33);
         this.lblHandlingTypeCode.TabIndex = 66;
         this.lblHandlingTypeCode.Text = "טיפול";
         this.lblHandlingTypeCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // terminalDataDataSet1
         // 
         this.terminalDataDataSet1.DataSetName = "TerminalDataDataSet1";
         this.terminalDataDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
         // 
         // tCContainerTypeBindingSource
         // 
         this.tCContainerTypeBindingSource.DataMember = "TC_ContainerType";
         this.tCContainerTypeBindingSource.DataSource = this.terminalDataDataSet1;
         // 
         // tC_ContainerTypeTableAdapter
         // 
         this.tC_ContainerTypeTableAdapter.ClearBeforeFill = true;
         // 
         // frmEmptyContainers
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(239)))), ((int)(((byte)(221)))));
         this.ClientSize = new System.Drawing.Size(1260, 713);
         this.ControlBox = false;
         this.Controls.Add(this.lblHandlingTypeCode);
         this.Controls.Add(this.cmbHandlingTypeCode);
         this.Controls.Add(this.lblContainerLength);
         this.Controls.Add(this.cmbContainerLength);
         this.Controls.Add(this.lblContainerType);
         this.Controls.Add(this.cmbcontainerType);
         this.Controls.Add(this.lbl40);
         this.Controls.Add(this.lbl20);
         this.Controls.Add(this.btn40);
         this.Controls.Add(this.btn20);
         this.Controls.Add(this.txtSumContainers);
         this.Controls.Add(this.lblNoData);
         this.Controls.Add(this.groupBoxBtuonsInOut);
         this.Controls.Add(this.btnShowAllRecords);
         this.Controls.Add(this.lblShipingLine);
         this.Controls.Add(this.cmbShipingLine);
         this.Controls.Add(this.dataGridEMContainers);
         this.KeyPreview = true;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmEmptyContainers";
         this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
         this.RightToLeftLayout = true;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Tag = "";
         this.Text = "מכולות ריקות לסוכן";
         ((System.ComponentModel.ISupportInitialize)(this.dataGridEMContainers)).EndInit();
         this.groupBoxBtuonsInOut.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.cOContainersDataTableBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.terminalDataDataSet1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.tCContainerTypeBindingSource)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridEMContainers;
        private System.Windows.Forms.ComboBox cmbShipingLine;
        private System.Windows.Forms.Label lblShipingLine;
        private System.Windows.Forms.Button btnShowAllRecords;
        private System.Windows.Forms.GroupBox groupBoxBtuonsInOut;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnInformation;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.Timer timerfrmEMContainers;
        private System.Windows.Forms.TextBox txtSumContainers;
        private System.Windows.Forms.Label lbl40;
        private System.Windows.Forms.Label lbl20;
        private System.Windows.Forms.Button btn40;
        private System.Windows.Forms.Button btn20;
        private System.Windows.Forms.ComboBox cmbcontainerType;
        private System.Windows.Forms.BindingSource cOContainersDataTableBindingSource;
        private TerminalDataDataSet1 terminalDataDataSet1;
        private System.Windows.Forms.BindingSource tCContainerTypeBindingSource;
        private TerminalDataDataSet1TableAdapters.TC_ContainerTypeTableAdapter tC_ContainerTypeTableAdapter;
        private System.Windows.Forms.Label lblContainerType;
        private System.Windows.Forms.ComboBox cmbContainerLength;
        private System.Windows.Forms.Label lblContainerLength;
        private System.Windows.Forms.ComboBox cmbHandlingTypeCode;
        private System.Windows.Forms.Label lblHandlingTypeCode;
    }
}
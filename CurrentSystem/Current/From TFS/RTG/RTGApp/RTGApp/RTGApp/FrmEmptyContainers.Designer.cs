namespace RTGApp
{
    partial class FrmEmptyContainers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmptyContainers));
            this.timerfrmEMContainers = new System.Windows.Forms.Timer(this.components);
            this.lblContainerLength = new System.Windows.Forms.Label();
            this.cmbContainerLength = new System.Windows.Forms.ComboBox();
            this.lblContainerType = new System.Windows.Forms.Label();
            this.cmbcontainerType = new System.Windows.Forms.ComboBox();
            this.txtSumContainers = new System.Windows.Forms.TextBox();
            this.lblNoData = new System.Windows.Forms.Label();
            this.btnShowAllRecords = new System.Windows.Forms.Button();
            this.lblShipingLine = new System.Windows.Forms.Label();
            this.cmbShipingLine = new System.Windows.Forms.ComboBox();
            this.dataGridEMContainers = new System.Windows.Forms.DataGridView();
            this.tCContainerTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cOContainersDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEMContainers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCContainerTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cOContainersDataTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContainerLength
            // 
            this.lblContainerLength.AutoSize = true;
            this.lblContainerLength.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.lblContainerLength.ForeColor = System.Drawing.Color.Navy;
            this.lblContainerLength.Location = new System.Drawing.Point(401, 12);
            this.lblContainerLength.Name = "lblContainerLength";
            this.lblContainerLength.Size = new System.Drawing.Size(70, 32);
            this.lblContainerLength.TabIndex = 81;
            this.lblContainerLength.Text = "גודל";
            this.lblContainerLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbContainerLength
            // 
            this.cmbContainerLength.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold);
            this.cmbContainerLength.FormattingEnabled = true;
            this.cmbContainerLength.Items.AddRange(new object[] {
            "",
            "20",
            "40",
            "45"});
            this.cmbContainerLength.Location = new System.Drawing.Point(477, 4);
            this.cmbContainerLength.Name = "cmbContainerLength";
            this.cmbContainerLength.Size = new System.Drawing.Size(97, 46);
            this.cmbContainerLength.TabIndex = 80;
            this.cmbContainerLength.SelectedIndexChanged += new System.EventHandler(this.cmbContainerLength_SelectedIndexChanged);
            this.cmbContainerLength.Click += new System.EventHandler(this.cmbContainerLength_Click);
            // 
            // lblContainerType
            // 
            this.lblContainerType.AutoSize = true;
            this.lblContainerType.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.lblContainerType.ForeColor = System.Drawing.Color.Navy;
            this.lblContainerType.Location = new System.Drawing.Point(596, 12);
            this.lblContainerType.Name = "lblContainerType";
            this.lblContainerType.Size = new System.Drawing.Size(58, 32);
            this.lblContainerType.TabIndex = 79;
            this.lblContainerType.Text = "סוג";
            this.lblContainerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbcontainerType
            // 
            this.cmbcontainerType.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbcontainerType.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold);
            this.cmbcontainerType.FormattingEnabled = true;
            this.cmbcontainerType.Items.AddRange(new object[] {
            "",
            "FL",
            "HC",
            "OT",
            "RF",
            "RG",
            "RH",
            "TK",
            "PW"});
            this.cmbcontainerType.Location = new System.Drawing.Point(662, 4);
            this.cmbcontainerType.Name = "cmbcontainerType";
            this.cmbcontainerType.Size = new System.Drawing.Size(96, 46);
            this.cmbcontainerType.TabIndex = 78;
            this.cmbcontainerType.SelectedIndexChanged += new System.EventHandler(this.cmbcontainerType_SelectedIndexChanged);
            this.cmbcontainerType.Click += new System.EventHandler(this.cmbcontainerType_Click);
            // 
            // txtSumContainers
            // 
            this.txtSumContainers.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.txtSumContainers.ForeColor = System.Drawing.Color.Teal;
            this.txtSumContainers.Location = new System.Drawing.Point(30, 543);
            this.txtSumContainers.Name = "txtSumContainers";
            this.txtSumContainers.Size = new System.Drawing.Size(302, 33);
            this.txtSumContainers.TabIndex = 73;
            // 
            // lblNoData
            // 
            this.lblNoData.AutoEllipsis = true;
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Verdana", 70F, System.Drawing.FontStyle.Bold);
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoData.Location = new System.Drawing.Point(399, 196);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(523, 114);
            this.lblNoData.TabIndex = 72;
            this.lblNoData.Text = "אין נתונים";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // btnShowAllRecords
            // 
            this.btnShowAllRecords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowAllRecords.BackColor = System.Drawing.Color.MistyRose;
            this.btnShowAllRecords.Font = new System.Drawing.Font("Verdana", 22F, System.Drawing.FontStyle.Bold);
            this.btnShowAllRecords.ForeColor = System.Drawing.Color.Navy;
            this.btnShowAllRecords.Location = new System.Drawing.Point(785, 4);
            this.btnShowAllRecords.Name = "btnShowAllRecords";
            this.btnShowAllRecords.Size = new System.Drawing.Size(139, 51);
            this.btnShowAllRecords.TabIndex = 70;
            this.btnShowAllRecords.Text = "הכל";
            this.btnShowAllRecords.UseVisualStyleBackColor = false;
            this.btnShowAllRecords.Click += new System.EventHandler(this.btnShowAllRecords_Click);
            // 
            // lblShipingLine
            // 
            this.lblShipingLine.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.lblShipingLine.ForeColor = System.Drawing.Color.Navy;
            this.lblShipingLine.Location = new System.Drawing.Point(94, 12);
            this.lblShipingLine.Name = "lblShipingLine";
            this.lblShipingLine.Size = new System.Drawing.Size(136, 33);
            this.lblShipingLine.TabIndex = 69;
            this.lblShipingLine.Text = "קו ספנות";
            // 
            // cmbShipingLine
            // 
            this.cmbShipingLine.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold);
            this.cmbShipingLine.FormattingEnabled = true;
            this.cmbShipingLine.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cmbShipingLine.Location = new System.Drawing.Point(228, 6);
            this.cmbShipingLine.Name = "cmbShipingLine";
            this.cmbShipingLine.Size = new System.Drawing.Size(152, 46);
            this.cmbShipingLine.TabIndex = 68;
            this.cmbShipingLine.SelectedIndexChanged += new System.EventHandler(this.cmbShipingLine_SelectedIndexChanged);
            this.cmbShipingLine.Click += new System.EventHandler(this.cmbShipingLine_Click);
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
            this.dataGridEMContainers.Location = new System.Drawing.Point(30, 60);
            this.dataGridEMContainers.Name = "dataGridEMContainers";
            this.dataGridEMContainers.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridEMContainers.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridEMContainers.RowTemplate.Height = 35;
            this.dataGridEMContainers.Size = new System.Drawing.Size(1236, 466);
            this.dataGridEMContainers.TabIndex = 67;
            // 
            // tCContainerTypeBindingSource
            // 
            this.tCContainerTypeBindingSource.DataMember = "TC_ContainerType";
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1167, 539);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 68);
            this.btnExit.TabIndex = 89;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmEmptyContainers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1307, 632);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblContainerLength);
            this.Controls.Add(this.cmbContainerLength);
            this.Controls.Add(this.lblContainerType);
            this.Controls.Add(this.cmbcontainerType);
            this.Controls.Add(this.txtSumContainers);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.btnShowAllRecords);
            this.Controls.Add(this.lblShipingLine);
            this.Controls.Add(this.cmbShipingLine);
            this.Controls.Add(this.dataGridEMContainers);
            this.Name = "FrmEmptyContainers";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מכולות ריקות";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEMContainers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCContainerTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cOContainersDataTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tCContainerTypeBindingSource;
        private System.Windows.Forms.Timer timerfrmEMContainers;
        private System.Windows.Forms.BindingSource cOContainersDataTableBindingSource;
        private System.Windows.Forms.Label lblContainerLength;
        private System.Windows.Forms.ComboBox cmbContainerLength;
        private System.Windows.Forms.Label lblContainerType;
        private System.Windows.Forms.ComboBox cmbcontainerType;
        private System.Windows.Forms.TextBox txtSumContainers;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.Button btnShowAllRecords;
        private System.Windows.Forms.Label lblShipingLine;
        private System.Windows.Forms.ComboBox cmbShipingLine;
        private System.Windows.Forms.DataGridView dataGridEMContainers;
        private System.Windows.Forms.Button btnExit;
    }
}
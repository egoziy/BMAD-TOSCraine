namespace RTGApp
{
    partial class FrmInOutDiory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnShowAllRecords = new System.Windows.Forms.Button();
            this.lblInOutDiory = new System.Windows.Forms.Label();
            this.cmbInOutDiory = new System.Windows.Forms.ComboBox();
            this.lblNoData = new System.Windows.Forms.Label();
            this.txtSumMovments = new System.Windows.Forms.TextBox();
            this.dataGridInOutDiory = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInOutDiory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShowAllRecords
            // 
            this.btnShowAllRecords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowAllRecords.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnShowAllRecords.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnShowAllRecords.ForeColor = System.Drawing.Color.Navy;
            this.btnShowAllRecords.Location = new System.Drawing.Point(432, 6);
            this.btnShowAllRecords.Margin = new System.Windows.Forms.Padding(5);
            this.btnShowAllRecords.Name = "btnShowAllRecords";
            this.btnShowAllRecords.Size = new System.Drawing.Size(112, 47);
            this.btnShowAllRecords.TabIndex = 80;
            this.btnShowAllRecords.Text = "הכל";
            this.btnShowAllRecords.UseVisualStyleBackColor = false;
            this.btnShowAllRecords.Click += new System.EventHandler(this.btnShowAllRecords_Click);
            // 
            // lblInOutDiory
            // 
            this.lblInOutDiory.AutoSize = true;
            this.lblInOutDiory.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.lblInOutDiory.ForeColor = System.Drawing.Color.Navy;
            this.lblInOutDiory.Location = new System.Drawing.Point(27, 17);
            this.lblInOutDiory.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblInOutDiory.Name = "lblInOutDiory";
            this.lblInOutDiory.Size = new System.Drawing.Size(146, 29);
            this.lblInOutDiory.TabIndex = 79;
            this.lblInOutDiory.Text = "בחר תנועה";
            // 
            // cmbInOutDiory
            // 
            this.cmbInOutDiory.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmbInOutDiory.FormattingEnabled = true;
            this.cmbInOutDiory.Items.AddRange(new object[] {
            "כניסה",
            "יציאה"});
            this.cmbInOutDiory.Location = new System.Drawing.Point(183, 3);
            this.cmbInOutDiory.Margin = new System.Windows.Forms.Padding(5);
            this.cmbInOutDiory.Name = "cmbInOutDiory";
            this.cmbInOutDiory.Size = new System.Drawing.Size(239, 50);
            this.cmbInOutDiory.TabIndex = 78;
            this.cmbInOutDiory.SelectedIndexChanged += new System.EventHandler(this.cmbInOutDiory_SelectedIndexChanged);
            this.cmbInOutDiory.Click += new System.EventHandler(this.cmbInOutDiory_Click);
            // 
            // lblNoData
            // 
            this.lblNoData.AutoEllipsis = true;
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Tahoma", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoData.Location = new System.Drawing.Point(213, 164);
            this.lblNoData.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(534, 116);
            this.lblNoData.TabIndex = 77;
            this.lblNoData.Text = "אין נתונים";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // txtSumMovments
            // 
            this.txtSumMovments.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtSumMovments.ForeColor = System.Drawing.Color.Teal;
            this.txtSumMovments.Location = new System.Drawing.Point(25, 534);
            this.txtSumMovments.Margin = new System.Windows.Forms.Padding(5);
            this.txtSumMovments.Name = "txtSumMovments";
            this.txtSumMovments.Size = new System.Drawing.Size(385, 33);
            this.txtSumMovments.TabIndex = 85;
            // 
            // dataGridInOutDiory
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridInOutDiory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridInOutDiory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridInOutDiory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridInOutDiory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInOutDiory.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridInOutDiory.Location = new System.Drawing.Point(23, 60);
            this.dataGridInOutDiory.Name = "dataGridInOutDiory";
            this.dataGridInOutDiory.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridInOutDiory.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridInOutDiory.RowTemplate.Height = 35;
            this.dataGridInOutDiory.Size = new System.Drawing.Size(1236, 466);
            this.dataGridInOutDiory.TabIndex = 86;
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(1113, 539);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(146, 81);
            this.btnExit.TabIndex = 95;
            this.btnExit.Text = "יציאה";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmInOutDiory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1307, 632);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridInOutDiory);
            this.Controls.Add(this.btnShowAllRecords);
            this.Controls.Add(this.lblInOutDiory);
            this.Controls.Add(this.cmbInOutDiory);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.txtSumMovments);
            this.Name = "FrmInOutDiory";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "יומן תנועות";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInOutDiory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowAllRecords;
        private System.Windows.Forms.Label lblInOutDiory;
        private System.Windows.Forms.ComboBox cmbInOutDiory;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.TextBox txtSumMovments;
        private System.Windows.Forms.DataGridView dataGridInOutDiory;
        private System.Windows.Forms.Button btnExit;
    }
}
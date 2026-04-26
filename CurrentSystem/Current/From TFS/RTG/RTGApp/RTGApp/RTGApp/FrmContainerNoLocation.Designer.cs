namespace RTGApp
{
    partial class FrmContainerNoLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSumContainers = new System.Windows.Forms.TextBox();
            this.lblNoData = new System.Windows.Forms.Label();
            this.dataGridConNoLocation = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConNoLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSumContainers
            // 
            this.txtSumContainers.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold);
            this.txtSumContainers.ForeColor = System.Drawing.Color.Teal;
            this.txtSumContainers.Location = new System.Drawing.Point(40, 509);
            this.txtSumContainers.Name = "txtSumContainers";
            this.txtSumContainers.Size = new System.Drawing.Size(302, 33);
            this.txtSumContainers.TabIndex = 76;
            // 
            // lblNoData
            // 
            this.lblNoData.AutoEllipsis = true;
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Verdana", 70F, System.Drawing.FontStyle.Bold);
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoData.Location = new System.Drawing.Point(409, 162);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(523, 114);
            this.lblNoData.TabIndex = 75;
            this.lblNoData.Text = "אין נתונים";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // dataGridConNoLocation
            // 
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridConNoLocation.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridConNoLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridConNoLocation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridConNoLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridConNoLocation.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridConNoLocation.Location = new System.Drawing.Point(40, 26);
            this.dataGridConNoLocation.Name = "dataGridConNoLocation";
            this.dataGridConNoLocation.RowHeadersVisible = false;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridConNoLocation.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridConNoLocation.RowTemplate.Height = 35;
            this.dataGridConNoLocation.Size = new System.Drawing.Size(1236, 466);
            this.dataGridConNoLocation.TabIndex = 74;
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(1130, 509);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(146, 81);
            this.btnExit.TabIndex = 77;
            this.btnExit.Text = "יציאה";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmContainerNoLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1307, 632);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtSumContainers);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.dataGridConNoLocation);
            this.Name = "FrmContainerNoLocation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מכולות ללא איתור";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConNoLocation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSumContainers;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.DataGridView dataGridConNoLocation;
        private System.Windows.Forms.Button btnExit;
    }
}
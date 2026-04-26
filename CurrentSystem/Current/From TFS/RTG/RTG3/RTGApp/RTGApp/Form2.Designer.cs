namespace RTGApp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHaderOut = new System.Windows.Forms.Label();
            this.lblHaderIn = new System.Windows.Forms.Label();
            this.dataGridContaunersInList = new System.Windows.Forms.DataGridView();
            this.dataGridContaunersOutList = new System.Windows.Forms.DataGridView();
            this.BtmRTG = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersInList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersOutList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHaderOut
            // 
            this.lblHaderOut.AutoSize = true;
            this.lblHaderOut.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHaderOut.ForeColor = System.Drawing.Color.Navy;
            this.lblHaderOut.Location = new System.Drawing.Point(1191, 313);
            this.lblHaderOut.Name = "lblHaderOut";
            this.lblHaderOut.Size = new System.Drawing.Size(87, 29);
            this.lblHaderOut.TabIndex = 8;
            this.lblHaderOut.Text = "טעינה";
            // 
            // lblHaderIn
            // 
            this.lblHaderIn.AutoSize = true;
            this.lblHaderIn.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblHaderIn.ForeColor = System.Drawing.Color.Navy;
            this.lblHaderIn.Location = new System.Drawing.Point(1191, 1);
            this.lblHaderIn.Name = "lblHaderIn";
            this.lblHaderIn.Size = new System.Drawing.Size(88, 29);
            this.lblHaderIn.TabIndex = 5;
            this.lblHaderIn.Text = "פריקה";
            // 
            // dataGridContaunersInList
            // 
            this.dataGridContaunersInList.AllowUserToAddRows = false;
            this.dataGridContaunersInList.AllowUserToDeleteRows = false;
            this.dataGridContaunersInList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridContaunersInList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersInList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridContaunersInList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridContaunersInList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridContaunersInList.Location = new System.Drawing.Point(40, 30);
            this.dataGridContaunersInList.MultiSelect = false;
            this.dataGridContaunersInList.Name = "dataGridContaunersInList";
            this.dataGridContaunersInList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersInList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridContaunersInList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersInList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridContaunersInList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersInList.RowTemplate.Height = 35;
            this.dataGridContaunersInList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridContaunersInList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 275);
            this.dataGridContaunersInList.TabIndex = 4;
            this.dataGridContaunersInList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersInList_CellClick);
            // 
            // dataGridContaunersOutList
            // 
            this.dataGridContaunersOutList.AllowUserToAddRows = false;
            this.dataGridContaunersOutList.AllowUserToDeleteRows = false;
            this.dataGridContaunersOutList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersOutList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridContaunersOutList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridContaunersOutList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridContaunersOutList.Location = new System.Drawing.Point(40, 343);
            this.dataGridContaunersOutList.MultiSelect = false;
            this.dataGridContaunersOutList.Name = "dataGridContaunersOutList";
            this.dataGridContaunersOutList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridContaunersOutList.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridContaunersOutList.RowHeadersVisible = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersOutList.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridContaunersOutList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridContaunersOutList.RowTemplate.Height = 35;
            this.dataGridContaunersOutList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridContaunersOutList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 275);
            this.dataGridContaunersOutList.TabIndex = 9;
            this.dataGridContaunersOutList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridContaunersOutList_CellClick);
            // 
            // BtmRTG
            // 
            this.BtmRTG.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtmRTG.BackColor = System.Drawing.Color.LightGreen;
            this.BtmRTG.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BtmRTG.ForeColor = System.Drawing.Color.Navy;
            this.BtmRTG.Location = new System.Drawing.Point(40, 622);
            this.BtmRTG.Name = "BtmRTG";
            this.BtmRTG.Size = new System.Drawing.Size(141, 54);
            this.BtmRTG.TabIndex = 11;
            this.BtmRTG.Text = "בחר";
            this.BtmRTG.UseVisualStyleBackColor = false;
            this.BtmRTG.Click += new System.EventHandler(this.BtmRTG_Click);
            // 
            // btnBack
            // 
            this.btnBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.Black;
            this.btnBack.Location = new System.Drawing.Point(188, 622);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(141, 54);
            this.btnBack.TabIndex = 68;
            this.btnBack.Text = "חזרה ";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 679);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.BtmRTG);
            this.Controls.Add(this.lblHaderOut);
            this.Controls.Add(this.lblHaderIn);
            this.Controls.Add(this.dataGridContaunersInList);
            this.Controls.Add(this.dataGridContaunersOutList);
            this.Name = "frmInformation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "משאיות 2025-01-08";
            this.Load += new System.EventHandler(this.frmInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersInList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContaunersOutList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHaderOut;
        private System.Windows.Forms.Label lblHaderIn;
        private System.Windows.Forms.DataGridView dataGridContaunersInList;
        private System.Windows.Forms.DataGridView dataGridContaunersOutList;
        private System.Windows.Forms.Button BtmRTG;
        private System.Windows.Forms.Button btnBack;
    }
}
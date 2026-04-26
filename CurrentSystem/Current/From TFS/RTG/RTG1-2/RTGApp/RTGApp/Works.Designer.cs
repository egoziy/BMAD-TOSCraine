namespace RTGApp
{
    partial class FrmWorks
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
            this.lblTargetDate = new System.Windows.Forms.Label();
            this.groupWorks = new System.Windows.Forms.GroupBox();
            this.cboWorks = new System.Windows.Forms.ComboBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.dateTimePickerTargetDate = new System.Windows.Forms.DateTimePicker();
            this.lblNoData = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.dataGridWorks = new System.Windows.Forms.DataGridView();
            this.BtmRTG = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.BOND1 = new System.Windows.Forms.Button();
            this.BOND2 = new System.Windows.Forms.Button();
            this.groupLocation = new System.Windows.Forms.GroupBox();
            this.groupWorks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridWorks)).BeginInit();
            this.groupLocation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTargetDate
            // 
            this.lblTargetDate.AutoSize = true;
            this.lblTargetDate.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTargetDate.ForeColor = System.Drawing.Color.Navy;
            this.lblTargetDate.Location = new System.Drawing.Point(862, 25);
            this.lblTargetDate.Name = "lblTargetDate";
            this.lblTargetDate.Size = new System.Drawing.Size(91, 23);
            this.lblTargetDate.TabIndex = 64;
            this.lblTargetDate.Text = "לתאריך:";
            // 
            // groupWorks
            // 
            this.groupWorks.Controls.Add(this.cboWorks);
            this.groupWorks.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupWorks.Location = new System.Drawing.Point(392, 20);
            this.groupWorks.Name = "groupWorks";
            this.groupWorks.Size = new System.Drawing.Size(464, 111);
            this.groupWorks.TabIndex = 65;
            this.groupWorks.TabStop = false;
            this.groupWorks.Text = "עבודה";
            // 
            // cboWorks
            // 
            this.cboWorks.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cboWorks.FormattingEnabled = true;
            this.cboWorks.Location = new System.Drawing.Point(23, 32);
            this.cboWorks.Name = "cboWorks";
            this.cboWorks.Size = new System.Drawing.Size(435, 45);
            this.cboWorks.TabIndex = 4;
            this.cboWorks.SelectedIndexChanged += new System.EventHandler(this.cboWorks_SelectedIndexChanged);
            this.cboWorks.Click += new System.EventHandler(this.cboWorks_Click);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblComment.Location = new System.Drawing.Point(21, 593);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(90, 25);
            this.lblComment.TabIndex = 68;
            this.lblComment.Text = "הערות:";
            // 
            // dateTimePickerTargetDate
            // 
            this.dateTimePickerTargetDate.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dateTimePickerTargetDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dateTimePickerTargetDate.Location = new System.Drawing.Point(862, 52);
            this.dateTimePickerTargetDate.Name = "dateTimePickerTargetDate";
            this.dateTimePickerTargetDate.Size = new System.Drawing.Size(309, 31);
            this.dateTimePickerTargetDate.TabIndex = 67;
            this.dateTimePickerTargetDate.Value = new System.DateTime(2011, 2, 2, 0, 0, 0, 0);
            // 
            // lblNoData
            // 
            this.lblNoData.AutoEllipsis = true;
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Tahoma", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoData.Location = new System.Drawing.Point(322, 249);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(534, 116);
            this.lblNoData.TabIndex = 66;
            this.lblNoData.Text = "אין נתונים";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // txtComment
            // 
            this.txtComment.AcceptsTab = true;
            this.txtComment.BackColor = System.Drawing.Color.LightGray;
            this.txtComment.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtComment.ForeColor = System.Drawing.Color.Red;
            this.txtComment.Location = new System.Drawing.Point(117, 593);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(1089, 30);
            this.txtComment.TabIndex = 69;
            // 
            // dataGridWorks
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridWorks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridWorks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridWorks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridWorks.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridWorks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridWorks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridWorks.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridWorks.Location = new System.Drawing.Point(19, 137);
            this.dataGridWorks.MultiSelect = false;
            this.dataGridWorks.Name = "dataGridWorks";
            this.dataGridWorks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridWorks.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridWorks.RowHeadersVisible = false;
            this.dataGridWorks.RowHeadersWidth = 43;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridWorks.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridWorks.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dataGridWorks.RowTemplate.Height = 30;
            this.dataGridWorks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridWorks.Size = new System.Drawing.Size(1187, 450);
            this.dataGridWorks.TabIndex = 61;
            this.dataGridWorks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridWorks_CellClick);
            this.dataGridWorks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridWorks_CellDoubleClick);
            // 
            // BtmRTG
            // 
            this.BtmRTG.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtmRTG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtmRTG.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BtmRTG.ForeColor = System.Drawing.Color.Navy;
            this.BtmRTG.Location = new System.Drawing.Point(1096, 629);
            this.BtmRTG.Name = "BtmRTG";
            this.BtmRTG.Size = new System.Drawing.Size(110, 50);
            this.BtmRTG.TabIndex = 70;
            this.BtmRTG.Text = "RTG";
            this.BtmRTG.UseVisualStyleBackColor = false;
            this.BtmRTG.Click += new System.EventHandler(this.BtmRTG_Click);
            // 
            // btnAll
            // 
            this.btnAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAll.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAll.ForeColor = System.Drawing.Color.Navy;
            this.btnAll.Location = new System.Drawing.Point(19, 52);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(131, 48);
            this.btnAll.TabIndex = 3;
            this.btnAll.TabStop = false;
            this.btnAll.Text = "הכל";
            this.btnAll.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // BOND1
            // 
            this.BOND1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BOND1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BOND1.ForeColor = System.Drawing.Color.Navy;
            this.BOND1.Location = new System.Drawing.Point(125, 34);
            this.BOND1.Name = "BOND1";
            this.BOND1.Size = new System.Drawing.Size(95, 48);
            this.BOND1.TabIndex = 4;
            this.BOND1.Text = "BOND1";
            this.BOND1.UseVisualStyleBackColor = true;
            this.BOND1.Click += new System.EventHandler(this.BOND1_Click);
            // 
            // BOND2
            // 
            this.BOND2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BOND2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BOND2.ForeColor = System.Drawing.Color.Navy;
            this.BOND2.Location = new System.Drawing.Point(19, 34);
            this.BOND2.Name = "BOND2";
            this.BOND2.Size = new System.Drawing.Size(95, 48);
            this.BOND2.TabIndex = 5;
            this.BOND2.Text = "BOND2";
            this.BOND2.UseVisualStyleBackColor = true;
            this.BOND2.Click += new System.EventHandler(this.BOND2_Click);
            // 
            // groupLocation
            // 
            this.groupLocation.BackColor = System.Drawing.SystemColors.Control;
            this.groupLocation.Controls.Add(this.BOND2);
            this.groupLocation.Controls.Add(this.BOND1);
            this.groupLocation.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupLocation.Location = new System.Drawing.Point(156, 18);
            this.groupLocation.Name = "groupLocation";
            this.groupLocation.Size = new System.Drawing.Size(231, 113);
            this.groupLocation.TabIndex = 63;
            this.groupLocation.TabStop = false;
            this.groupLocation.Text = "ערוגות";
            // 
            // FrmWorks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 691);
            this.Controls.Add(this.BtmRTG);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.lblTargetDate);
            this.Controls.Add(this.groupWorks);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.dateTimePickerTargetDate);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.groupLocation);
            this.Controls.Add(this.dataGridWorks);
            this.Name = "FrmWorks";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "עבודות";
            this.Load += new System.EventHandler(this.FrmWorks_Load);
            this.groupWorks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridWorks)).EndInit();
            this.groupLocation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTargetDate;
        private System.Windows.Forms.GroupBox groupWorks;
        private System.Windows.Forms.ComboBox cboWorks;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.DateTimePicker dateTimePickerTargetDate;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.DataGridView dataGridWorks;
        private System.Windows.Forms.Button BtmRTG;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button BOND1;
        private System.Windows.Forms.Button BOND2;
        private System.Windows.Forms.GroupBox groupLocation;
    }
}
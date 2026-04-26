namespace ForkliftApp
{
    partial class frmWorks
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxBtuonsInOut = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnInformation = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dataGridWorks = new System.Windows.Forms.DataGridView();
            this.timerWorks = new System.Windows.Forms.Timer(this.components);
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.lbl40 = new System.Windows.Forms.Label();
            this.lbl20 = new System.Windows.Forms.Label();
            this.lblAll = new System.Windows.Forms.Label();
            this.btn40 = new System.Windows.Forms.Button();
            this.btn20 = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.lblTargetDate = new System.Windows.Forms.Label();
            this.groupWorks = new System.Windows.Forms.GroupBox();
            this.cboWorks = new System.Windows.Forms.ComboBox();
            this.lblNoData = new System.Windows.Forms.Label();
            this.dateTimePickerTargetDate = new System.Windows.Forms.DateTimePicker();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.groupBoxBtuonsInOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridWorks)).BeginInit();
            this.groupSize.SuspendLayout();
            this.groupWorks.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBtuonsInOut
            // 
            this.groupBoxBtuonsInOut.Controls.Add(this.btnExit);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnInformation);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnSelect);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnQuery);
            this.groupBoxBtuonsInOut.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBoxBtuonsInOut.Location = new System.Drawing.Point(12, 604);
            this.groupBoxBtuonsInOut.Name = "groupBoxBtuonsInOut";
            this.groupBoxBtuonsInOut.Size = new System.Drawing.Size(1187, 108);
            this.groupBoxBtuonsInOut.TabIndex = 47;
            this.groupBoxBtuonsInOut.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(141, 17);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(154, 84);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "יציאה  F10";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnInformation
            // 
            this.btnInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInformation.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnInformation.ForeColor = System.Drawing.Color.Navy;
            this.btnInformation.Location = new System.Drawing.Point(379, 17);
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(154, 84);
            this.btnInformation.TabIndex = 2;
            this.btnInformation.Text = "מידע  F5";
            this.btnInformation.UseVisualStyleBackColor = false;
            this.btnInformation.Click += new System.EventHandler(this.btnInformation_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSelect.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSelect.ForeColor = System.Drawing.Color.Navy;
            this.btnSelect.Location = new System.Drawing.Point(853, 17);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(154, 84);
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
            this.btnQuery.Location = new System.Drawing.Point(603, 17);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(154, 84);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "שאילתה  F3";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
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
            this.dataGridWorks.Location = new System.Drawing.Point(12, 129);
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
            this.dataGridWorks.TabIndex = 0;
            this.dataGridWorks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridWorks_CellClick);
            this.dataGridWorks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridWorks_CellDoubleClick);
            this.dataGridWorks.SelectionChanged += new System.EventHandler(this.dataGridWorks_SelectionChanged);
            // 
            // groupSize
            // 
            this.groupSize.Controls.Add(this.lbl40);
            this.groupSize.Controls.Add(this.lbl20);
            this.groupSize.Controls.Add(this.lblAll);
            this.groupSize.Controls.Add(this.btn40);
            this.groupSize.Controls.Add(this.btn20);
            this.groupSize.Controls.Add(this.btnAll);
            this.groupSize.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupSize.Location = new System.Drawing.Point(12, 10);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(417, 113);
            this.groupSize.TabIndex = 52;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "גודל מכולה";
            // 
            // lbl40
            // 
            this.lbl40.AutoSize = true;
            this.lbl40.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl40.ForeColor = System.Drawing.Color.Navy;
            this.lbl40.Location = new System.Drawing.Point(85, 22);
            this.lbl40.Name = "lbl40";
            this.lbl40.Size = new System.Drawing.Size(57, 23);
            this.lbl40.TabIndex = 5;
            this.lbl40.Text = "{F2}";
            this.lbl40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl20
            // 
            this.lbl20.AutoSize = true;
            this.lbl20.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl20.ForeColor = System.Drawing.Color.Navy;
            this.lbl20.Location = new System.Drawing.Point(185, 22);
            this.lbl20.Name = "lbl20";
            this.lbl20.Size = new System.Drawing.Size(57, 23);
            this.lbl20.TabIndex = 4;
            this.lbl20.Text = "{F1}";
            this.lbl20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblAll.ForeColor = System.Drawing.Color.Navy;
            this.lblAll.Location = new System.Drawing.Point(288, 21);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(57, 23);
            this.lblAll.TabIndex = 3;
            this.lblAll.Text = "{F9}";
            this.lblAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn40
            // 
            this.btn40.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn40.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn40.ForeColor = System.Drawing.Color.Navy;
            this.btn40.Location = new System.Drawing.Point(65, 48);
            this.btn40.Name = "btn40";
            this.btn40.Size = new System.Drawing.Size(84, 48);
            this.btn40.TabIndex = 2;
            this.btn40.Text = "40";
            this.btn40.UseVisualStyleBackColor = true;
            this.btn40.Click += new System.EventHandler(this.btn40_Click);
            // 
            // btn20
            // 
            this.btn20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn20.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn20.ForeColor = System.Drawing.Color.Navy;
            this.btn20.Location = new System.Drawing.Point(171, 48);
            this.btn20.Name = "btn20";
            this.btn20.Size = new System.Drawing.Size(84, 48);
            this.btn20.TabIndex = 1;
            this.btn20.Text = "20";
            this.btn20.UseVisualStyleBackColor = true;
            this.btn20.Click += new System.EventHandler(this.btn20_Click);
            // 
            // btnAll
            // 
            this.btnAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAll.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAll.ForeColor = System.Drawing.Color.Navy;
            this.btnAll.Location = new System.Drawing.Point(274, 48);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(84, 48);
            this.btnAll.TabIndex = 0;
            this.btnAll.TabStop = false;
            this.btnAll.Text = "הכל";
            this.btnAll.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // lblTargetDate
            // 
            this.lblTargetDate.AutoSize = true;
            this.lblTargetDate.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTargetDate.ForeColor = System.Drawing.Color.Navy;
            this.lblTargetDate.Location = new System.Drawing.Point(906, 17);
            this.lblTargetDate.Name = "lblTargetDate";
            this.lblTargetDate.Size = new System.Drawing.Size(91, 23);
            this.lblTargetDate.TabIndex = 54;
            this.lblTargetDate.Text = "לתאריך:";
            // 
            // groupWorks
            // 
            this.groupWorks.Controls.Add(this.cboWorks);
            this.groupWorks.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupWorks.Location = new System.Drawing.Point(436, 12);
            this.groupWorks.Name = "groupWorks";
            this.groupWorks.Size = new System.Drawing.Size(464, 111);
            this.groupWorks.TabIndex = 56;
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
            // lblNoData
            // 
            this.lblNoData.AutoEllipsis = true;
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Tahoma", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNoData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoData.Location = new System.Drawing.Point(129, 204);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(534, 116);
            this.lblNoData.TabIndex = 57;
            this.lblNoData.Text = "אין נתונים";
            this.lblNoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoData.Visible = false;
            // 
            // dateTimePickerTargetDate
            // 
            this.dateTimePickerTargetDate.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dateTimePickerTargetDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dateTimePickerTargetDate.Location = new System.Drawing.Point(906, 44);
            this.dateTimePickerTargetDate.Name = "dateTimePickerTargetDate";
            this.dateTimePickerTargetDate.Size = new System.Drawing.Size(309, 31);
            this.dateTimePickerTargetDate.TabIndex = 58;
            this.dateTimePickerTargetDate.Value = new System.DateTime(2011, 2, 2, 0, 0, 0, 0);
            this.dateTimePickerTargetDate.CloseUp += new System.EventHandler(this.dateTimePickerTargetDate_CloseUp);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblComment.Location = new System.Drawing.Point(14, 585);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(90, 25);
            this.lblComment.TabIndex = 59;
            this.lblComment.Text = "הערות:";
            // 
            // txtComment
            // 
            this.txtComment.AcceptsTab = true;
            this.txtComment.BackColor = System.Drawing.Color.LightGray;
            this.txtComment.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtComment.ForeColor = System.Drawing.Color.Red;
            this.txtComment.Location = new System.Drawing.Point(110, 585);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(887, 30);
            this.txtComment.TabIndex = 60;
            // 
            // frmWorks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(239)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1260, 713);
            this.ControlBox = false;
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.dateTimePickerTargetDate);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.groupWorks);
            this.Controls.Add(this.lblTargetDate);
            this.Controls.Add(this.groupSize);
            this.Controls.Add(this.dataGridWorks);
            this.Controls.Add(this.groupBoxBtuonsInOut);
            this.KeyPreview = true;
            this.Name = "frmWorks";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "עבודות";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmWorks_KeyDown);
            this.groupBoxBtuonsInOut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridWorks)).EndInit();
            this.groupSize.ResumeLayout(false);
            this.groupSize.PerformLayout();
            this.groupWorks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBtuonsInOut;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnInformation;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridView dataGridWorks;
        private System.Windows.Forms.Timer timerWorks;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.Button btn40;
        private System.Windows.Forms.Button btn20;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Label lbl20;
        private System.Windows.Forms.Label lbl40;
        private System.Windows.Forms.Label lblTargetDate;
        private System.Windows.Forms.GroupBox groupWorks;
        private System.Windows.Forms.ComboBox cboWorks;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.DateTimePicker dateTimePickerTargetDate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
    }
}
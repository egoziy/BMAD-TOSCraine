namespace ForkliftApp
{
    partial class frmMessage
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
            this.BtnRead = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GridMessage = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnRead
            // 
            this.BtnRead.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BtnRead.Location = new System.Drawing.Point(30, 449);
            this.BtnRead.Name = "BtnRead";
            this.BtnRead.Size = new System.Drawing.Size(94, 43);
            this.BtnRead.TabIndex = 0;
            this.BtnRead.Text = "קראתי";
            this.BtnRead.UseVisualStyleBackColor = true;
            this.BtnRead.Click += new System.EventHandler(this.BtnRead_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GridMessage
            // 
            this.GridMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridMessage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridMessage.Location = new System.Drawing.Point(12, 31);
            this.GridMessage.Name = "GridMessage";
            this.GridMessage.Size = new System.Drawing.Size(559, 412);
            this.GridMessage.TabIndex = 1;
            // 
            // frmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 531);
            this.Controls.Add(this.GridMessage);
            this.Controls.Add(this.BtnRead);
            this.Name = "frmMessage";
            this.Text = "frmMessage";
            ((System.ComponentModel.ISupportInitialize)(this.GridMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnRead;
        private System.Windows.Forms.DataGridView GridMessage;
        public System.Windows.Forms.Timer timer1;
    }
}
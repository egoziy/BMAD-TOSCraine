namespace ForkliftApp
{
    partial class frmSpecialLocation
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
            this.SpecialLocation = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SpecialLocation
            // 
            this.SpecialLocation.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.SpecialLocation.FormattingEnabled = true;
            this.SpecialLocation.ItemHeight = 39;
            this.SpecialLocation.Location = new System.Drawing.Point(12, 12);
            this.SpecialLocation.Name = "SpecialLocation";
            this.SpecialLocation.Size = new System.Drawing.Size(321, 433);
            this.SpecialLocation.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnOK.Location = new System.Drawing.Point(34, 451);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(148, 65);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "אישור";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Info;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.Location = new System.Drawing.Point(223, 451);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 65);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "יציאה";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmSpecialLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(350, 528);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.SpecialLocation);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecialLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSpecialLocation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox SpecialLocation;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnExit;
    }
}
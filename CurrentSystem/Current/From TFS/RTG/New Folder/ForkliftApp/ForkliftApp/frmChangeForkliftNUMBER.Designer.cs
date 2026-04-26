namespace ForkliftApp
{
    partial class frmChangeForkliftNumber
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNewForkLiftNumber = new System.Windows.Forms.Label();
            this.txtNewForkLiftNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnOK.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnOK.Location = new System.Drawing.Point(209, 52);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "אישור";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Crimson;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnCancel.ForeColor = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(11, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "ביטול";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNewForkLiftNumber
            // 
            this.lblNewForkLiftNumber.AutoSize = true;
            this.lblNewForkLiftNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNewForkLiftNumber.Location = new System.Drawing.Point(8, 16);
            this.lblNewForkLiftNumber.Name = "lblNewForkLiftNumber";
            this.lblNewForkLiftNumber.Size = new System.Drawing.Size(163, 16);
            this.lblNewForkLiftNumber.TabIndex = 2;
            this.lblNewForkLiftNumber.Text = "הקש/י מספר מלגזה חדש";
            // 
            // txtNewForkLiftNumber
            // 
            this.txtNewForkLiftNumber.Location = new System.Drawing.Point(184, 14);
            this.txtNewForkLiftNumber.Name = "txtNewForkLiftNumber";
            this.txtNewForkLiftNumber.Size = new System.Drawing.Size(100, 20);
            this.txtNewForkLiftNumber.TabIndex = 3;
            // 
            // frmChangeForkliftNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 84);
            this.ControlBox = false;
            this.Controls.Add(this.txtNewForkLiftNumber);
            this.Controls.Add(this.lblNewForkLiftNumber);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangeForkliftNumber";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "שינוי מספר מלגזה";
            this.Load += new System.EventHandler(this.frmChangeForkliftNumber_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNewForkLiftNumber;
        private System.Windows.Forms.TextBox txtNewForkLiftNumber;
    }
}
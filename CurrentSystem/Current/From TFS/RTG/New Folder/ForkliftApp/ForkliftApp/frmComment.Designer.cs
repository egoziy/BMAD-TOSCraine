namespace ForkliftApp
{
    partial class frmComment
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnClose.Location = new System.Drawing.Point(115, 96);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 40);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "סגור";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnUpdate.Location = new System.Drawing.Point(416, 96);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 40);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "עדכן";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblComment.Location = new System.Drawing.Point(12, 35);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(97, 18);
            this.lblComment.TabIndex = 2;
            this.lblComment.Text = "הוסיפו הערה";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(115, 35);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(496, 20);
            this.txtComment.TabIndex = 3;
            this.txtComment.TextChanged += new System.EventHandler(this.txtComment_TextChanged);
            // 
            // frmComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 187);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComment";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הוספת הערה";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
    }
}
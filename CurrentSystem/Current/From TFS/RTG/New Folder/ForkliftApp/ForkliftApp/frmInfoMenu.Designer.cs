namespace ForkliftApp
{
    partial class frmInfoMenu
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
            this.btnEmptyContainers = new System.Windows.Forms.Button();
            this.btnEmptyLoction = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblEmptyContainers = new System.Windows.Forms.Label();
            this.lblEmptyLocation = new System.Windows.Forms.Label();
            this.lblCloseForm = new System.Windows.Forms.Label();
            this.timerfrmInfoMenu = new System.Windows.Forms.Timer(this.components);
            this.btnInOutDiory = new System.Windows.Forms.Button();
            this.lblInOutDiory = new System.Windows.Forms.Label();
            this.btnForLoction = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExpectedContainers = new System.Windows.Forms.Button();
            this.btnRecommendedLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEmptyContainers
            // 
            this.btnEmptyContainers.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnEmptyContainers.ForeColor = System.Drawing.Color.Red;
            this.btnEmptyContainers.Location = new System.Drawing.Point(191, 118);
            this.btnEmptyContainers.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmptyContainers.Name = "btnEmptyContainers";
            this.btnEmptyContainers.Size = new System.Drawing.Size(199, 101);
            this.btnEmptyContainers.TabIndex = 0;
            this.btnEmptyContainers.Text = "מכולות ריקות";
            this.btnEmptyContainers.UseVisualStyleBackColor = true;
            this.btnEmptyContainers.Click += new System.EventHandler(this.btnEmptyContainers_Click);
            // 
            // btnEmptyLoction
            // 
            this.btnEmptyLoction.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnEmptyLoction.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnEmptyLoction.ForeColor = System.Drawing.Color.Red;
            this.btnEmptyLoction.Location = new System.Drawing.Point(450, 118);
            this.btnEmptyLoction.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmptyLoction.Name = "btnEmptyLoction";
            this.btnEmptyLoction.Size = new System.Drawing.Size(244, 101);
            this.btnEmptyLoction.TabIndex = 1;
            this.btnEmptyLoction.Text = "מכולות ללא איתור";
            this.btnEmptyLoction.UseVisualStyleBackColor = true;
            this.btnEmptyLoction.Click += new System.EventHandler(this.btnEmptyLoction_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Location = new System.Drawing.Point(982, 380);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(176, 101);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "סגור";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblEmptyContainers
            // 
            this.lblEmptyContainers.AutoSize = true;
            this.lblEmptyContainers.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblEmptyContainers.ForeColor = System.Drawing.Color.Red;
            this.lblEmptyContainers.Location = new System.Drawing.Point(247, 79);
            this.lblEmptyContainers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmptyContainers.Name = "lblEmptyContainers";
            this.lblEmptyContainers.Size = new System.Drawing.Size(72, 29);
            this.lblEmptyContainers.TabIndex = 3;
            this.lblEmptyContainers.Text = "{F1}";
            // 
            // lblEmptyLocation
            // 
            this.lblEmptyLocation.AutoSize = true;
            this.lblEmptyLocation.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblEmptyLocation.ForeColor = System.Drawing.Color.Red;
            this.lblEmptyLocation.Location = new System.Drawing.Point(532, 79);
            this.lblEmptyLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmptyLocation.Name = "lblEmptyLocation";
            this.lblEmptyLocation.Size = new System.Drawing.Size(72, 29);
            this.lblEmptyLocation.TabIndex = 4;
            this.lblEmptyLocation.Text = "{F2}";
            // 
            // lblCloseForm
            // 
            this.lblCloseForm.AutoSize = true;
            this.lblCloseForm.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCloseForm.ForeColor = System.Drawing.Color.Red;
            this.lblCloseForm.Location = new System.Drawing.Point(1033, 347);
            this.lblCloseForm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCloseForm.Name = "lblCloseForm";
            this.lblCloseForm.Size = new System.Drawing.Size(87, 29);
            this.lblCloseForm.TabIndex = 5;
            this.lblCloseForm.Text = "{F10}";
            // 
            // btnInOutDiory
            // 
            this.btnInOutDiory.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnInOutDiory.ForeColor = System.Drawing.Color.Red;
            this.btnInOutDiory.Location = new System.Drawing.Point(749, 118);
            this.btnInOutDiory.Name = "btnInOutDiory";
            this.btnInOutDiory.Size = new System.Drawing.Size(176, 101);
            this.btnInOutDiory.TabIndex = 6;
            this.btnInOutDiory.Text = "יומן תנועות";
            this.btnInOutDiory.UseVisualStyleBackColor = true;
            this.btnInOutDiory.Click += new System.EventHandler(this.btnInOutDiory_Click);
            // 
            // lblInOutDiory
            // 
            this.lblInOutDiory.AutoSize = true;
            this.lblInOutDiory.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblInOutDiory.ForeColor = System.Drawing.Color.Red;
            this.lblInOutDiory.Location = new System.Drawing.Point(799, 79);
            this.lblInOutDiory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInOutDiory.Name = "lblInOutDiory";
            this.lblInOutDiory.Size = new System.Drawing.Size(72, 29);
            this.lblInOutDiory.TabIndex = 7;
            this.lblInOutDiory.Text = "{F3}";
            // 
            // btnForLoction
            // 
            this.btnForLoction.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnForLoction.ForeColor = System.Drawing.Color.Red;
            this.btnForLoction.Location = new System.Drawing.Point(982, 118);
            this.btnForLoction.Margin = new System.Windows.Forms.Padding(4);
            this.btnForLoction.Name = "btnForLoction";
            this.btnForLoction.Size = new System.Drawing.Size(176, 101);
            this.btnForLoction.TabIndex = 8;
            this.btnForLoction.Text = "מכולות לגוש";
            this.btnForLoction.UseVisualStyleBackColor = true;
            this.btnForLoction.Click += new System.EventHandler(this.btnForLoction_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(1033, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "{F4}";
            // 
            // btnExpectedContainers
            // 
            this.btnExpectedContainers.AutoSize = true;
            this.btnExpectedContainers.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExpectedContainers.ForeColor = System.Drawing.Color.Red;
            this.btnExpectedContainers.Location = new System.Drawing.Point(731, 380);
            this.btnExpectedContainers.Margin = new System.Windows.Forms.Padding(4);
            this.btnExpectedContainers.Name = "btnExpectedContainers";
            this.btnExpectedContainers.Size = new System.Drawing.Size(194, 101);
            this.btnExpectedContainers.TabIndex = 10;
            this.btnExpectedContainers.Text = "מכולות צפויות";
            this.btnExpectedContainers.UseVisualStyleBackColor = true;
            this.btnExpectedContainers.Click += new System.EventHandler(this.btnExpectedContainers_Click);
            // 
            // btnRecommendedLocation
            // 
            this.btnRecommendedLocation.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.btnRecommendedLocation.ForeColor = System.Drawing.Color.Red;
            this.btnRecommendedLocation.Location = new System.Drawing.Point(450, 380);
            this.btnRecommendedLocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecommendedLocation.Name = "btnRecommendedLocation";
            this.btnRecommendedLocation.Size = new System.Drawing.Size(229, 101);
            this.btnRecommendedLocation.TabIndex = 12;
            this.btnRecommendedLocation.Text = "איתורים מומלצים";
            this.btnRecommendedLocation.UseVisualStyleBackColor = true;
            this.btnRecommendedLocation.Click += new System.EventHandler(this.btnRecommendedLocation_Click_1);
            // 
            // frmInfoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(239)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1260, 713);
            this.ControlBox = false;
            this.Controls.Add(this.btnRecommendedLocation);
            this.Controls.Add(this.btnExpectedContainers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnForLoction);
            this.Controls.Add(this.lblInOutDiory);
            this.Controls.Add(this.btnInOutDiory);
            this.Controls.Add(this.lblCloseForm);
            this.Controls.Add(this.lblEmptyLocation);
            this.Controls.Add(this.lblEmptyContainers);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEmptyLoction);
            this.Controls.Add(this.btnEmptyContainers);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmInfoMenu";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "תפריט למידע";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInfoMenu_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEmptyContainers;
        private System.Windows.Forms.Button btnEmptyLoction;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblEmptyContainers;
        private System.Windows.Forms.Label lblEmptyLocation;
        private System.Windows.Forms.Label lblCloseForm;
        private System.Windows.Forms.Timer timerfrmInfoMenu;
        private System.Windows.Forms.Button btnInOutDiory;
        private System.Windows.Forms.Label lblInOutDiory;
        private System.Windows.Forms.Button btnForLoction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExpectedContainers;
        private System.Windows.Forms.Button btnRecommendedLocation;
    }
}
namespace RTGApp
{
    partial class FrmMenu
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
            this.btnRecommendedLocation = new System.Windows.Forms.Button();
            this.btnExpectedContainers = new System.Windows.Forms.Button();
            this.btnForLoction = new System.Windows.Forms.Button();
            this.btnInOutDiory = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEmptyLoction = new System.Windows.Forms.Button();
            this.btnEmptyContainers = new System.Windows.Forms.Button();
            this.btnUpdateLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRecommendedLocation
            // 
            this.btnRecommendedLocation.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnRecommendedLocation.ForeColor = System.Drawing.Color.Navy;
            this.btnRecommendedLocation.Location = new System.Drawing.Point(397, 351);
            this.btnRecommendedLocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecommendedLocation.Name = "btnRecommendedLocation";
            this.btnRecommendedLocation.Size = new System.Drawing.Size(244, 101);
            this.btnRecommendedLocation.TabIndex = 19;
            this.btnRecommendedLocation.Text = "איתורים מומלצים";
            this.btnRecommendedLocation.UseVisualStyleBackColor = true;
            this.btnRecommendedLocation.Click += new System.EventHandler(this.btnRecommendedLocation_Click);
            // 
            // btnExpectedContainers
            // 
            this.btnExpectedContainers.AutoSize = true;
            this.btnExpectedContainers.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnExpectedContainers.ForeColor = System.Drawing.Color.Navy;
            this.btnExpectedContainers.Location = new System.Drawing.Point(692, 351);
            this.btnExpectedContainers.Margin = new System.Windows.Forms.Padding(4);
            this.btnExpectedContainers.Name = "btnExpectedContainers";
            this.btnExpectedContainers.Size = new System.Drawing.Size(194, 101);
            this.btnExpectedContainers.TabIndex = 18;
            this.btnExpectedContainers.Text = "מכולות צפויות";
            this.btnExpectedContainers.UseVisualStyleBackColor = true;
            this.btnExpectedContainers.Click += new System.EventHandler(this.btnExpectedContainers_Click);
            // 
            // btnForLoction
            // 
            this.btnForLoction.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnForLoction.ForeColor = System.Drawing.Color.Navy;
            this.btnForLoction.Location = new System.Drawing.Point(943, 89);
            this.btnForLoction.Margin = new System.Windows.Forms.Padding(4);
            this.btnForLoction.Name = "btnForLoction";
            this.btnForLoction.Size = new System.Drawing.Size(199, 101);
            this.btnForLoction.TabIndex = 17;
            this.btnForLoction.Text = "מכולות לגוש";
            this.btnForLoction.UseVisualStyleBackColor = true;
            this.btnForLoction.Click += new System.EventHandler(this.btnForLoction_Click);
            // 
            // btnInOutDiory
            // 
            this.btnInOutDiory.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnInOutDiory.ForeColor = System.Drawing.Color.Navy;
            this.btnInOutDiory.Location = new System.Drawing.Point(692, 89);
            this.btnInOutDiory.Name = "btnInOutDiory";
            this.btnInOutDiory.Size = new System.Drawing.Size(194, 101);
            this.btnInOutDiory.TabIndex = 16;
            this.btnInOutDiory.Text = "יומן תנועות";
            this.btnInOutDiory.UseVisualStyleBackColor = true;
            this.btnInOutDiory.Click += new System.EventHandler(this.btnInOutDiory_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(943, 351);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(199, 101);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "RTG";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEmptyLoction
            // 
            this.btnEmptyLoction.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnEmptyLoction.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnEmptyLoction.ForeColor = System.Drawing.Color.Navy;
            this.btnEmptyLoction.Location = new System.Drawing.Point(397, 89);
            this.btnEmptyLoction.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmptyLoction.Name = "btnEmptyLoction";
            this.btnEmptyLoction.Size = new System.Drawing.Size(244, 101);
            this.btnEmptyLoction.TabIndex = 14;
            this.btnEmptyLoction.Text = "מכולות ללא איתור";
            this.btnEmptyLoction.UseVisualStyleBackColor = true;
            this.btnEmptyLoction.Click += new System.EventHandler(this.btnEmptyLoction_Click);
            // 
            // btnEmptyContainers
            // 
            this.btnEmptyContainers.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnEmptyContainers.ForeColor = System.Drawing.Color.Navy;
            this.btnEmptyContainers.Location = new System.Drawing.Point(138, 89);
            this.btnEmptyContainers.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmptyContainers.Name = "btnEmptyContainers";
            this.btnEmptyContainers.Size = new System.Drawing.Size(199, 101);
            this.btnEmptyContainers.TabIndex = 13;
            this.btnEmptyContainers.Text = "מכולות ריקות";
            this.btnEmptyContainers.UseVisualStyleBackColor = true;
            this.btnEmptyContainers.Click += new System.EventHandler(this.btnEmptyContainers_Click);
            // 
            // btnUpdateLocation
            // 
            this.btnUpdateLocation.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
            this.btnUpdateLocation.ForeColor = System.Drawing.Color.Navy;
            this.btnUpdateLocation.Location = new System.Drawing.Point(138, 351);
            this.btnUpdateLocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateLocation.Name = "btnUpdateLocation";
            this.btnUpdateLocation.Size = new System.Drawing.Size(199, 101);
            this.btnUpdateLocation.TabIndex = 20;
            this.btnUpdateLocation.Text = "עדכון איתור";
            this.btnUpdateLocation.UseVisualStyleBackColor = true;
            this.btnUpdateLocation.Click += new System.EventHandler(this.btnUpdateLocation_Click);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 632);
            this.Controls.Add(this.btnUpdateLocation);
            this.Controls.Add(this.btnRecommendedLocation);
            this.Controls.Add(this.btnExpectedContainers);
            this.Controls.Add(this.btnForLoction);
            this.Controls.Add(this.btnInOutDiory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEmptyLoction);
            this.Controls.Add(this.btnEmptyContainers);
            this.Name = "FrmMenu";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "תפריט מידע";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecommendedLocation;
        private System.Windows.Forms.Button btnExpectedContainers;
        private System.Windows.Forms.Button btnForLoction;
        private System.Windows.Forms.Button btnInOutDiory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEmptyLoction;
        private System.Windows.Forms.Button btnEmptyContainers;
        private System.Windows.Forms.Button btnUpdateLocation;
    }
}
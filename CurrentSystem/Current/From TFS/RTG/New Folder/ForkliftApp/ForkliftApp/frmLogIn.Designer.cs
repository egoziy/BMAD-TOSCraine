namespace ForkliftApp
{
    partial class frmLogIn
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
            this.lbkUserName = new System.Windows.Forms.Label();
            this.lblPassWord = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.MaskedTextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.lblForkliftNumber = new System.Windows.Forms.Label();
            this.txtForkliftNumber = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DELETE = new System.Windows.Forms.Button();
            this.Zero = new System.Windows.Forms.Button();
            this.Eight = new System.Windows.Forms.Button();
            this.Four = new System.Windows.Forms.Button();
            this.Nine = new System.Windows.Forms.Button();
            this.Seven = new System.Windows.Forms.Button();
            this.Six = new System.Windows.Forms.Button();
            this.Five = new System.Windows.Forms.Button();
            this.THREE = new System.Windows.Forms.Button();
            this.TWO = new System.Windows.Forms.Button();
            this.One = new System.Windows.Forms.Button();
            this.cmbTerminal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbkUserName
            // 
            this.lbkUserName.AutoSize = true;
            this.lbkUserName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbkUserName.Location = new System.Drawing.Point(113, 15);
            this.lbkUserName.Name = "lbkUserName";
            this.lbkUserName.Size = new System.Drawing.Size(165, 29);
            this.lbkUserName.TabIndex = 1;
            this.lbkUserName.Text = "שם משתמש:";
            this.lbkUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPassWord
            // 
            this.lblPassWord.AutoSize = true;
            this.lblPassWord.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassWord.Location = new System.Drawing.Point(174, 53);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(104, 29);
            this.lblPassWord.TabIndex = 2;
            this.lblPassWord.Text = "סיסמה:";
            // 
            // txtPassWord
            // 
            this.txtPassWord.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtPassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassWord.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtPassWord.Location = new System.Drawing.Point(284, 53);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(208, 36);
            this.txtPassWord.TabIndex = 3;
            this.txtPassWord.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtPassWord_enter);
            // 
            // btnGo
            // 
            this.btnGo.AutoSize = true;
            this.btnGo.BackColor = System.Drawing.Color.Yellow;
            this.btnGo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnGo.ForeColor = System.Drawing.Color.Black;
            this.btnGo.Location = new System.Drawing.Point(39, 339);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(150, 80);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "אישור {F1}";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblForkliftNumber
            // 
            this.lblForkliftNumber.AutoSize = true;
            this.lblForkliftNumber.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblForkliftNumber.Location = new System.Drawing.Point(106, 153);
            this.lblForkliftNumber.Name = "lblForkliftNumber";
            this.lblForkliftNumber.Size = new System.Drawing.Size(172, 29);
            this.lblForkliftNumber.TabIndex = 5;
            this.lblForkliftNumber.Text = "מספר מלגזה:";
            // 
            // txtForkliftNumber
            // 
            this.txtForkliftNumber.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtForkliftNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtForkliftNumber.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtForkliftNumber.Location = new System.Drawing.Point(284, 151);
            this.txtForkliftNumber.Name = "txtForkliftNumber";
            this.txtForkliftNumber.Size = new System.Drawing.Size(208, 36);
            this.txtForkliftNumber.TabIndex = 4;
            this.txtForkliftNumber.DoubleClick += new System.EventHandler(this.txtForkliftNumber_DoubleClick);
            // 
            // btnExit
            // 
            this.btnExit.AllowDrop = true;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.GreenYellow;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(39, 452);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 80);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "ביטול {F10}";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtUserName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.FormattingEnabled = true;
            this.txtUserName.Location = new System.Drawing.Point(284, 11);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(207, 33);
            this.txtUserName.TabIndex = 7;
            this.txtUserName.SelectedIndexChanged += new System.EventHandler(this.txtUserName_SelectedIndexChanged);
            this.txtUserName.Click += new System.EventHandler(this.txtUserName_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DELETE);
            this.groupBox1.Controls.Add(this.Zero);
            this.groupBox1.Controls.Add(this.Eight);
            this.groupBox1.Controls.Add(this.Four);
            this.groupBox1.Controls.Add(this.Nine);
            this.groupBox1.Controls.Add(this.Seven);
            this.groupBox1.Controls.Add(this.Six);
            this.groupBox1.Controls.Add(this.Five);
            this.groupBox1.Controls.Add(this.THREE);
            this.groupBox1.Controls.Add(this.TWO);
            this.groupBox1.Controls.Add(this.One);
            this.groupBox1.Location = new System.Drawing.Point(234, 225);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 325);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // DELETE
            // 
            this.DELETE.BackColor = System.Drawing.Color.Coral;
            this.DELETE.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DELETE.Location = new System.Drawing.Point(92, 237);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(156, 70);
            this.DELETE.TabIndex = 18;
            this.DELETE.Tag = "Back";
            this.DELETE.Text = "מחק";
            this.DELETE.UseVisualStyleBackColor = false;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // Zero
            // 
            this.Zero.BackColor = System.Drawing.Color.PowderBlue;
            this.Zero.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Zero.Location = new System.Drawing.Point(6, 237);
            this.Zero.Name = "Zero";
            this.Zero.Size = new System.Drawing.Size(70, 70);
            this.Zero.TabIndex = 17;
            this.Zero.Text = "0";
            this.Zero.UseVisualStyleBackColor = false;
            this.Zero.Click += new System.EventHandler(this.Zero_Click);
            // 
            // Eight
            // 
            this.Eight.BackColor = System.Drawing.Color.PowderBlue;
            this.Eight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Eight.Location = new System.Drawing.Point(92, 162);
            this.Eight.Name = "Eight";
            this.Eight.Size = new System.Drawing.Size(70, 70);
            this.Eight.TabIndex = 16;
            this.Eight.Text = "8";
            this.Eight.UseVisualStyleBackColor = false;
            this.Eight.Click += new System.EventHandler(this.Eight_Click);
            // 
            // Four
            // 
            this.Four.BackColor = System.Drawing.Color.PowderBlue;
            this.Four.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Four.Location = new System.Drawing.Point(6, 86);
            this.Four.Name = "Four";
            this.Four.Size = new System.Drawing.Size(70, 70);
            this.Four.TabIndex = 15;
            this.Four.Text = "4";
            this.Four.UseVisualStyleBackColor = false;
            this.Four.Click += new System.EventHandler(this.Four_Click);
            // 
            // Nine
            // 
            this.Nine.BackColor = System.Drawing.Color.PowderBlue;
            this.Nine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nine.Location = new System.Drawing.Point(178, 161);
            this.Nine.Name = "Nine";
            this.Nine.Size = new System.Drawing.Size(70, 70);
            this.Nine.TabIndex = 14;
            this.Nine.Text = "9";
            this.Nine.UseVisualStyleBackColor = false;
            this.Nine.Click += new System.EventHandler(this.Nine_Click);
            // 
            // Seven
            // 
            this.Seven.BackColor = System.Drawing.Color.PowderBlue;
            this.Seven.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Seven.Location = new System.Drawing.Point(6, 162);
            this.Seven.Name = "Seven";
            this.Seven.Size = new System.Drawing.Size(70, 70);
            this.Seven.TabIndex = 13;
            this.Seven.Text = "7";
            this.Seven.UseVisualStyleBackColor = false;
            this.Seven.Click += new System.EventHandler(this.Seven_Click);
            // 
            // Six
            // 
            this.Six.BackColor = System.Drawing.Color.PowderBlue;
            this.Six.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Six.Location = new System.Drawing.Point(178, 86);
            this.Six.Name = "Six";
            this.Six.Size = new System.Drawing.Size(70, 70);
            this.Six.TabIndex = 12;
            this.Six.Text = "6";
            this.Six.UseVisualStyleBackColor = false;
            this.Six.Click += new System.EventHandler(this.Six_Click);
            // 
            // Five
            // 
            this.Five.BackColor = System.Drawing.Color.PowderBlue;
            this.Five.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Five.Location = new System.Drawing.Point(92, 86);
            this.Five.Name = "Five";
            this.Five.Size = new System.Drawing.Size(70, 70);
            this.Five.TabIndex = 11;
            this.Five.Text = "5";
            this.Five.UseVisualStyleBackColor = false;
            this.Five.Click += new System.EventHandler(this.Five_Click);
            // 
            // THREE
            // 
            this.THREE.BackColor = System.Drawing.Color.PowderBlue;
            this.THREE.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.THREE.Location = new System.Drawing.Point(178, 10);
            this.THREE.Name = "THREE";
            this.THREE.Size = new System.Drawing.Size(70, 70);
            this.THREE.TabIndex = 7;
            this.THREE.Text = "3";
            this.THREE.UseVisualStyleBackColor = false;
            this.THREE.Click += new System.EventHandler(this.THREE_Click);
            // 
            // TWO
            // 
            this.TWO.BackColor = System.Drawing.Color.PowderBlue;
            this.TWO.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TWO.Location = new System.Drawing.Point(92, 10);
            this.TWO.Name = "TWO";
            this.TWO.Size = new System.Drawing.Size(70, 70);
            this.TWO.TabIndex = 6;
            this.TWO.Text = "2";
            this.TWO.UseVisualStyleBackColor = false;
            this.TWO.Click += new System.EventHandler(this.TWO_Click);
            // 
            // One
            // 
            this.One.BackColor = System.Drawing.Color.PowderBlue;
            this.One.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.One.Location = new System.Drawing.Point(6, 10);
            this.One.Name = "One";
            this.One.Size = new System.Drawing.Size(70, 70);
            this.One.TabIndex = 5;
            this.One.Text = "1";
            this.One.UseVisualStyleBackColor = false;
            this.One.Click += new System.EventHandler(this.One_Click);
            // 
            // cmbTerminal
            // 
            this.cmbTerminal.BackColor = System.Drawing.Color.LightSteelBlue;
            this.cmbTerminal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTerminal.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTerminal.FormattingEnabled = true;
            this.cmbTerminal.Items.AddRange(new object[] {
            "אשדוד",
            "חיפה"});
            this.cmbTerminal.Location = new System.Drawing.Point(285, 100);
            this.cmbTerminal.Name = "cmbTerminal";
            this.cmbTerminal.Size = new System.Drawing.Size(207, 41);
            this.cmbTerminal.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(193, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 29);
            this.label1.TabIndex = 62;
            this.label1.Text = "מסוף:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 560);
            this.ControlBox = false;
            this.Controls.Add(this.cmbTerminal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtForkliftNumber);
            this.Controls.Add(this.lblForkliftNumber);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.lblPassWord);
            this.Controls.Add(this.lbkUserName);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmLogIn";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "שם משתמש וסיסמה";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbkUserName;
        private System.Windows.Forms.Label lblPassWord;
        private System.Windows.Forms.MaskedTextBox txtPassWord;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblForkliftNumber;
        private System.Windows.Forms.TextBox txtForkliftNumber;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox txtUserName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button One;
        private System.Windows.Forms.Button THREE;
        private System.Windows.Forms.Button TWO;
        private System.Windows.Forms.Button Zero;
        private System.Windows.Forms.Button Eight;
        private System.Windows.Forms.Button Four;
        private System.Windows.Forms.Button Nine;
        private System.Windows.Forms.Button Seven;
        private System.Windows.Forms.Button Six;
        private System.Windows.Forms.Button Five;
        private System.Windows.Forms.Button DELETE;
        private System.Windows.Forms.ComboBox cmbTerminal;
        private System.Windows.Forms.Label label1;
    }
}
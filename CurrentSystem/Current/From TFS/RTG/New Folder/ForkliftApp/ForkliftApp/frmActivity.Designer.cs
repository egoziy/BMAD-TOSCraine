using System.Windows.Forms;
namespace ForkliftApp
{
    partial class frmActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActivity));
            this.txtContAlpha = new System.Windows.Forms.TextBox();
            this.txtContSize = new System.Windows.Forms.TextBox();
            this.txtContType = new System.Windows.Forms.TextBox();
            this.txtHandelingType = new System.Windows.Forms.TextBox();
            this.txtTchoula = new System.Windows.Forms.TextBox();
            this.txtCostumer = new System.Windows.Forms.TextBox();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtNewLocation = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblContainer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWh = new System.Windows.Forms.MaskedTextBox();
            this.lblTchoula = new System.Windows.Forms.Label();
            this.lblCostumer = new System.Windows.Forms.Label();
            this.lblline = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblNewLocation = new System.Windows.Forms.Label();
            this.lblTruck = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBoxBtuonsInOut = new System.Windows.Forms.GroupBox();
            this.btnReserve = new System.Windows.Forms.Button();
            this.btnDeal = new System.Windows.Forms.Button();
            this.btnDamage = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnWorks = new System.Windows.Forms.Button();
            this.btnInformation = new System.Windows.Forms.Button();
            this.lblOK = new System.Windows.Forms.Label();
            this.timerfrmactivity = new System.Windows.Forms.Timer(this.components);
            this.txtWork = new System.Windows.Forms.TextBox();
            this.lblWork = new System.Windows.Forms.Label();
            this.txtTruckNumber = new System.Windows.Forms.MaskedTextBox();
            this.cboContainers = new System.Windows.Forms.ComboBox();
            this.chkTrain = new System.Windows.Forms.CheckBox();
            this.chkTruck = new System.Windows.Forms.CheckBox();
            this.txtContNum = new System.Windows.Forms.TextBox();
            this.txtLocationExp = new System.Windows.Forms.TextBox();
            this.btnComment = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ClassificationClassCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UNCode1 = new System.Windows.Forms.TextBox();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.txtInDate = new System.Windows.Forms.MaskedTextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtAVDM = new System.Windows.Forms.TextBox();
            this.btnKaron = new System.Windows.Forms.Button();
            this.txtPermit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTerminal = new System.Windows.Forms.TextBox();
            this.btnLockLocation = new System.Windows.Forms.Button();
            this.SpecialLocation = new System.Windows.Forms.Button();
            this.btnUnLockLocation = new System.Windows.Forms.Button();
            this.touchScreen1 = new NumericKeyPad.TouchScreen();
            this.groupBoxBtuonsInOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtContAlpha
            // 
            this.txtContAlpha.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtContAlpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContAlpha.Font = new System.Drawing.Font("Tahoma", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContAlpha.Location = new System.Drawing.Point(1022, 5);
            this.txtContAlpha.Name = "txtContAlpha";
            this.txtContAlpha.Size = new System.Drawing.Size(206, 71);
            this.txtContAlpha.TabIndex = 1;
            this.txtContAlpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtContAlpha.TextChanged += new System.EventHandler(this.txtContAlpha_TextChanged);
            this.txtContAlpha.Enter += new System.EventHandler(this.txtContAlpha_Enter);
            // 
            // txtContSize
            // 
            this.txtContSize.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtContSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContSize.Font = new System.Drawing.Font("Tahoma", 40F, System.Drawing.FontStyle.Bold);
            this.txtContSize.Location = new System.Drawing.Point(552, 5);
            this.txtContSize.Name = "txtContSize";
            this.txtContSize.Size = new System.Drawing.Size(146, 72);
            this.txtContSize.TabIndex = 2;
            this.txtContSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtContSize.Enter += new System.EventHandler(this.txtContSize_Enter);
            // 
            // txtContType
            // 
            this.txtContType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtContType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContType.Font = new System.Drawing.Font("Tahoma", 40F, System.Drawing.FontStyle.Bold);
            this.txtContType.Location = new System.Drawing.Point(400, 5);
            this.txtContType.Name = "txtContType";
            this.txtContType.Size = new System.Drawing.Size(146, 72);
            this.txtContType.TabIndex = 3;
            this.txtContType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtContType.Enter += new System.EventHandler(this.txtContType_Enter);
            // 
            // txtHandelingType
            // 
            this.txtHandelingType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHandelingType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHandelingType.Font = new System.Drawing.Font("Tahoma", 40F, System.Drawing.FontStyle.Bold);
            this.txtHandelingType.Location = new System.Drawing.Point(208, 5);
            this.txtHandelingType.Name = "txtHandelingType";
            this.txtHandelingType.Size = new System.Drawing.Size(186, 72);
            this.txtHandelingType.TabIndex = 4;
            this.txtHandelingType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHandelingType.Enter += new System.EventHandler(this.txtHandelingType_Enter);
            // 
            // txtTchoula
            // 
            this.txtTchoula.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTchoula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTchoula.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtTchoula.Location = new System.Drawing.Point(834, 87);
            this.txtTchoula.Name = "txtTchoula";
            this.txtTchoula.Size = new System.Drawing.Size(240, 65);
            this.txtTchoula.TabIndex = 6;
            this.txtTchoula.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTchoula.Enter += new System.EventHandler(this.txtTchoula_Enter);
            // 
            // txtCostumer
            // 
            this.txtCostumer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCostumer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostumer.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtCostumer.Location = new System.Drawing.Point(208, 162);
            this.txtCostumer.Name = "txtCostumer";
            this.txtCostumer.Size = new System.Drawing.Size(490, 52);
            this.txtCostumer.TabIndex = 7;
            this.txtCostumer.TextChanged += new System.EventHandler(this.txtCostumer_TextChanged);
            this.txtCostumer.Enter += new System.EventHandler(this.txtCostumer_Enter);
            // 
            // txtLine
            // 
            this.txtLine.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLine.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtLine.Location = new System.Drawing.Point(834, 162);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(393, 52);
            this.txtLine.TabIndex = 8;
            this.txtLine.Tag = "";
            this.txtLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLine.TextChanged += new System.EventHandler(this.txtLine_TextChanged);
            this.txtLine.Enter += new System.EventHandler(this.txtLine_Enter);
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtLocation.Location = new System.Drawing.Point(208, 223);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(180, 50);
            this.txtLocation.TabIndex = 9;
            this.txtLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLocation.Enter += new System.EventHandler(this.txtLocation_Enter);
            // 
            // txtNewLocation
            // 
            this.txtNewLocation.AllowDrop = true;
            this.txtNewLocation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNewLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewLocation.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtNewLocation.Location = new System.Drawing.Point(835, 222);
            this.txtNewLocation.Name = "txtNewLocation";
            this.txtNewLocation.Size = new System.Drawing.Size(392, 56);
            this.txtNewLocation.TabIndex = 10;
            this.txtNewLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewLocation.Enter += new System.EventHandler(this.txtNewLocation_Enter);
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComment.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtComment.Location = new System.Drawing.Point(208, 361);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(796, 40);
            this.txtComment.TabIndex = 13;
            this.txtComment.Enter += new System.EventHandler(this.txtComment_Enter);
            // 
            // lblContainer
            // 
            this.lblContainer.AutoSize = true;
            this.lblContainer.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblContainer.Location = new System.Drawing.Point(79, 17);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(123, 36);
            this.lblContainer.TabIndex = 14;
            this.lblContainer.Text = "מכולה:";
            this.lblContainer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(86, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 36);
            this.label1.TabIndex = 15;
            this.label1.Text = "משקל:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWh
            // 
            this.txtWh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtWh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWh.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtWh.Location = new System.Drawing.Point(208, 88);
            this.txtWh.Name = "txtWh";
            this.txtWh.Size = new System.Drawing.Size(217, 65);
            this.txtWh.TabIndex = 5;
            this.txtWh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWh.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtWh_MaskInputRejected);
            this.txtWh.Enter += new System.EventHandler(this.txtWh_Enter);
            // 
            // lblTchoula
            // 
            this.lblTchoula.AutoSize = true;
            this.lblTchoula.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTchoula.Location = new System.Drawing.Point(744, 87);
            this.lblTchoula.Name = "lblTchoula";
            this.lblTchoula.Size = new System.Drawing.Size(94, 36);
            this.lblTchoula.TabIndex = 17;
            this.lblTchoula.Text = "תוכן:";
            this.lblTchoula.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCostumer
            // 
            this.lblCostumer.AutoSize = true;
            this.lblCostumer.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCostumer.Location = new System.Drawing.Point(104, 162);
            this.lblCostumer.Name = "lblCostumer";
            this.lblCostumer.Size = new System.Drawing.Size(101, 36);
            this.lblCostumer.TabIndex = 18;
            this.lblCostumer.Text = "לקוח:";
            this.lblCostumer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCostumer.Click += new System.EventHandler(this.lblCostumer_Click);
            // 
            // lblline
            // 
            this.lblline.AutoSize = true;
            this.lblline.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblline.Location = new System.Drawing.Point(780, 163);
            this.lblline.Name = "lblline";
            this.lblline.Size = new System.Drawing.Size(58, 36);
            this.lblline.TabIndex = 19;
            this.lblline.Text = "קו:";
            this.lblline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblline.Click += new System.EventHandler(this.lblline_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblLocation.Location = new System.Drawing.Point(13, 224);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(192, 36);
            this.lblLocation.TabIndex = 20;
            this.lblLocation.Text = "איתור/צפוי:";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNewLocation
            // 
            this.lblNewLocation.AutoSize = true;
            this.lblNewLocation.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNewLocation.Location = new System.Drawing.Point(744, 223);
            this.lblNewLocation.Name = "lblNewLocation";
            this.lblNewLocation.Size = new System.Drawing.Size(95, 36);
            this.lblNewLocation.TabIndex = 21;
            this.lblNewLocation.Text = "חדש:";
            this.lblNewLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTruck
            // 
            this.lblTruck.AutoSize = true;
            this.lblTruck.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTruck.Location = new System.Drawing.Point(76, 289);
            this.lblTruck.Name = "lblTruck";
            this.lblTruck.Size = new System.Drawing.Size(132, 36);
            this.lblTruck.TabIndex = 22;
            this.lblTruck.Text = "משאית:";
            this.lblTruck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblComment.Location = new System.Drawing.Point(119, 370);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(83, 24);
            this.lblComment.TabIndex = 23;
            this.lblComment.Text = "הערות:";
            this.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(746, 466);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(147, 139);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "אישור";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBoxBtuonsInOut
            // 
            this.groupBoxBtuonsInOut.Controls.Add(this.btnReserve);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnDeal);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnDamage);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnExit);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnQuery);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnWorks);
            this.groupBoxBtuonsInOut.Controls.Add(this.btnInformation);
            this.groupBoxBtuonsInOut.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBoxBtuonsInOut.Location = new System.Drawing.Point(786, 510);
            this.groupBoxBtuonsInOut.Name = "groupBoxBtuonsInOut";
            this.groupBoxBtuonsInOut.Size = new System.Drawing.Size(473, 222);
            this.groupBoxBtuonsInOut.TabIndex = 16;
            this.groupBoxBtuonsInOut.TabStop = false;
            // 
            // btnReserve
            // 
            this.btnReserve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReserve.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnReserve.ForeColor = System.Drawing.Color.Navy;
            this.btnReserve.Location = new System.Drawing.Point(338, 124);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(103, 89);
            this.btnReserve.TabIndex = 55;
            this.btnReserve.Text = "שמורות F7";
            this.btnReserve.UseVisualStyleBackColor = false;
            this.btnReserve.Click += new System.EventHandler(this.btnReserve_Click);
            // 
            // btnDeal
            // 
            this.btnDeal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeal.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnDeal.ForeColor = System.Drawing.Color.Navy;
            this.btnDeal.Location = new System.Drawing.Point(229, 123);
            this.btnDeal.Name = "btnDeal";
            this.btnDeal.Size = new System.Drawing.Size(103, 89);
            this.btnDeal.TabIndex = 53;
            this.btnDeal.Text = "אחים   F6";
            this.btnDeal.UseVisualStyleBackColor = false;
            this.btnDeal.Click += new System.EventHandler(this.btnDeal_Click);
            // 
            // btnDamage
            // 
            this.btnDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDamage.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnDamage.ForeColor = System.Drawing.Color.Navy;
            this.btnDamage.Location = new System.Drawing.Point(120, 123);
            this.btnDamage.Name = "btnDamage";
            this.btnDamage.Size = new System.Drawing.Size(103, 89);
            this.btnDamage.TabIndex = 8;
            this.btnDamage.Text = "נזק   F2";
            this.btnDamage.UseVisualStyleBackColor = false;
            this.btnDamage.Click += new System.EventHandler(this.btnDamage_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(11, 123);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(103, 90);
            this.btnExit.TabIndex = 62;
            this.btnExit.Text = "יציאה  F10";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnQuery.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ForeColor = System.Drawing.Color.Navy;
            this.btnQuery.Location = new System.Drawing.Point(11, 18);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(103, 89);
            this.btnQuery.TabIndex = 62;
            this.btnQuery.Text = "שאילתה F3";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnWorks
            // 
            this.btnWorks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWorks.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnWorks.ForeColor = System.Drawing.Color.Navy;
            this.btnWorks.Location = new System.Drawing.Point(229, 18);
            this.btnWorks.Name = "btnWorks";
            this.btnWorks.Size = new System.Drawing.Size(103, 89);
            this.btnWorks.TabIndex = 62;
            this.btnWorks.Text = "עבודות F4";
            this.btnWorks.UseVisualStyleBackColor = false;
            this.btnWorks.Click += new System.EventHandler(this.btnWorks_Click);
            // 
            // btnInformation
            // 
            this.btnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInformation.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnInformation.ForeColor = System.Drawing.Color.Navy;
            this.btnInformation.Location = new System.Drawing.Point(120, 18);
            this.btnInformation.Name = "btnInformation";
            this.btnInformation.Size = new System.Drawing.Size(103, 89);
            this.btnInformation.TabIndex = 62;
            this.btnInformation.Text = "מידע   F5";
            this.btnInformation.UseVisualStyleBackColor = false;
            this.btnInformation.Click += new System.EventHandler(this.btnInformation_Click);
            // 
            // lblOK
            // 
            this.lblOK.AutoSize = true;
            this.lblOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblOK.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblOK.Location = new System.Drawing.Point(785, 562);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(63, 25);
            this.lblOK.TabIndex = 43;
            this.lblOK.Text = "{F8}";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWork
            // 
            this.txtWork.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtWork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWork.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtWork.ForeColor = System.Drawing.Color.Red;
            this.txtWork.Location = new System.Drawing.Point(208, 409);
            this.txtWork.Name = "txtWork";
            this.txtWork.Size = new System.Drawing.Size(796, 40);
            this.txtWork.TabIndex = 45;
            // 
            // lblWork
            // 
            this.lblWork.AutoSize = true;
            this.lblWork.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblWork.Location = new System.Drawing.Point(119, 412);
            this.lblWork.Name = "lblWork";
            this.lblWork.Size = new System.Drawing.Size(81, 24);
            this.lblWork.TabIndex = 46;
            this.lblWork.Text = "עבודה:";
            this.lblWork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTruckNumber
            // 
            this.txtTruckNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTruckNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTruckNumber.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTruckNumber.Location = new System.Drawing.Point(208, 289);
            this.txtTruckNumber.Name = "txtTruckNumber";
            this.txtTruckNumber.Size = new System.Drawing.Size(300, 65);
            this.txtTruckNumber.TabIndex = 47;
            this.txtTruckNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTruckNumber.Enter += new System.EventHandler(this.txtTruckNumber_Enter);
            // 
            // cboContainers
            // 
            this.cboContainers.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.cboContainers.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cboContainers.FormattingEnabled = true;
            this.cboContainers.Location = new System.Drawing.Point(844, 85);
            this.cboContainers.Name = "cboContainers";
            this.cboContainers.Size = new System.Drawing.Size(198, 43);
            this.cboContainers.TabIndex = 48;
            this.cboContainers.Visible = false;
            this.cboContainers.SelectedValueChanged += new System.EventHandler(this.cboContainers_SelectedValueChanged);
            // 
            // chkTrain
            // 
            this.chkTrain.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTrain.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chkTrain.Location = new System.Drawing.Point(632, 677);
            this.chkTrain.Name = "chkTrain";
            this.chkTrain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkTrain.Size = new System.Drawing.Size(180, 33);
            this.chkTrain.TabIndex = 14;
            this.chkTrain.Text = "העמסה לרכבת";
            this.chkTrain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTrain.UseMnemonic = false;
            this.chkTrain.UseVisualStyleBackColor = true;
            this.chkTrain.CheckedChanged += new System.EventHandler(this.chkTrain_CheckedChanged);
            // 
            // chkTruck
            // 
            this.chkTruck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTruck.AutoSize = true;
            this.chkTruck.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chkTruck.Location = new System.Drawing.Point(632, 638);
            this.chkTruck.Name = "chkTruck";
            this.chkTruck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkTruck.Size = new System.Drawing.Size(180, 33);
            this.chkTruck.TabIndex = 51;
            this.chkTruck.Text = "העמסה למשאית";
            this.chkTruck.UseMnemonic = false;
            this.chkTruck.UseVisualStyleBackColor = true;
            this.chkTruck.CheckedChanged += new System.EventHandler(this.chkTruck_CheckedChanged);
            // 
            // txtContNum
            // 
            this.txtContNum.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtContNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContNum.Font = new System.Drawing.Font("Tahoma", 40F, System.Drawing.FontStyle.Bold);
            this.txtContNum.Location = new System.Drawing.Point(704, 4);
            this.txtContNum.MaxLength = 7;
            this.txtContNum.Name = "txtContNum";
            this.txtContNum.Size = new System.Drawing.Size(312, 72);
            this.txtContNum.TabIndex = 0;
            this.txtContNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtContNum.TextChanged += new System.EventHandler(this.txtContNum_Enter);
            this.txtContNum.Leave += new System.EventHandler(this.txtContNum_Leave);
            // 
            // txtLocationExp
            // 
            this.txtLocationExp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLocationExp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocationExp.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtLocationExp.Location = new System.Drawing.Point(393, 223);
            this.txtLocationExp.Name = "txtLocationExp";
            this.txtLocationExp.Size = new System.Drawing.Size(180, 50);
            this.txtLocationExp.TabIndex = 52;
            this.txtLocationExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnComment
            // 
            this.btnComment.AutoSize = true;
            this.btnComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnComment.Location = new System.Drawing.Point(14, 370);
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(99, 26);
            this.btnComment.TabIndex = 53;
            this.btnComment.Text = "הוספת הערה";
            this.btnComment.UseVisualStyleBackColor = true;
            this.btnComment.Click += new System.EventHandler(this.btnComment_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(1018, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 24);
            this.label2.TabIndex = 58;
            this.label2.Text = "קוד:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ClassificationClassCode
            // 
            this.ClassificationClassCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClassificationClassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClassificationClassCode.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ClassificationClassCode.ForeColor = System.Drawing.Color.Red;
            this.ClassificationClassCode.Location = new System.Drawing.Point(1075, 408);
            this.ClassificationClassCode.Name = "ClassificationClassCode";
            this.ClassificationClassCode.Size = new System.Drawing.Size(152, 40);
            this.ClassificationClassCode.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(1010, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 24);
            this.label3.TabIndex = 56;
            this.label3.Text = "או\"ם:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UNCode1
            // 
            this.UNCode1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.UNCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNCode1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.UNCode1.Location = new System.Drawing.Point(1075, 360);
            this.UNCode1.Name = "UNCode1";
            this.UNCode1.Size = new System.Drawing.Size(152, 40);
            this.UNCode1.TabIndex = 55;
            // 
            // txtDriverName
            // 
            this.txtDriverName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDriverName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDriverName.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtDriverName.ForeColor = System.Drawing.Color.Red;
            this.txtDriverName.Location = new System.Drawing.Point(835, 288);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(393, 65);
            this.txtDriverName.TabIndex = 60;
            // 
            // txtInDate
            // 
            this.txtInDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtInDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInDate.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtInDate.Location = new System.Drawing.Point(431, 88);
            this.txtInDate.Name = "txtInDate";
            this.txtInDate.Size = new System.Drawing.Size(267, 56);
            this.txtInDate.TabIndex = 61;
            this.txtInDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnGo.Location = new System.Drawing.Point(656, 466);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(84, 139);
            this.btnGo.TabIndex = 62;
            this.btnGo.Text = "המשך {F12}";
            this.btnGo.UseCompatibleTextRendering = true;
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtAVDM
            // 
            this.txtAVDM.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAVDM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAVDM.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtAVDM.Location = new System.Drawing.Point(1092, 87);
            this.txtAVDM.Name = "txtAVDM";
            this.txtAVDM.Size = new System.Drawing.Size(136, 65);
            this.txtAVDM.TabIndex = 63;
            this.txtAVDM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnKaron
            // 
            this.btnKaron.BackColor = System.Drawing.Color.PeachPuff;
            this.btnKaron.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKaron.Location = new System.Drawing.Point(667, 642);
            this.btnKaron.Name = "btnKaron";
            this.btnKaron.Size = new System.Drawing.Size(113, 80);
            this.btnKaron.TabIndex = 65;
            this.btnKaron.Text = "קרון";
            this.btnKaron.UseVisualStyleBackColor = false;
            this.btnKaron.Visible = false;
            this.btnKaron.Click += new System.EventHandler(this.btnKaron_Click);
            // 
            // txtPermit
            // 
            this.txtPermit.BackColor = System.Drawing.Color.GreenYellow;
            this.txtPermit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPermit.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtPermit.ForeColor = System.Drawing.Color.Black;
            this.txtPermit.Location = new System.Drawing.Point(899, 464);
            this.txtPermit.Name = "txtPermit";
            this.txtPermit.Size = new System.Drawing.Size(328, 52);
            this.txtPermit.TabIndex = 66;
            this.txtPermit.Text = "למכולה יש התרה";
            this.txtPermit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPermit.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(510, 299);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 36);
            this.label4.TabIndex = 67;
            this.label4.Text = "מסוף:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTerminal
            // 
            this.txtTerminal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTerminal.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtTerminal.Location = new System.Drawing.Point(610, 298);
            this.txtTerminal.Name = "txtTerminal";
            this.txtTerminal.Size = new System.Drawing.Size(136, 50);
            this.txtTerminal.TabIndex = 68;
            this.txtTerminal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnLockLocation
            // 
            this.btnLockLocation.BackColor = System.Drawing.Color.White;
            this.btnLockLocation.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLockLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnLockLocation.Image")));
            this.btnLockLocation.Location = new System.Drawing.Point(577, 220);
            this.btnLockLocation.Name = "btnLockLocation";
            this.btnLockLocation.Size = new System.Drawing.Size(67, 72);
            this.btnLockLocation.TabIndex = 69;
            this.btnLockLocation.UseVisualStyleBackColor = false;
            this.btnLockLocation.Click += new System.EventHandler(this.btnLockLocation_Click);
            // 
            // SpecialLocation
            // 
            this.SpecialLocation.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.SpecialLocation.Location = new System.Drawing.Point(651, 225);
            this.SpecialLocation.Name = "SpecialLocation";
            this.SpecialLocation.Size = new System.Drawing.Size(91, 63);
            this.SpecialLocation.TabIndex = 70;
            this.SpecialLocation.Text = "איתור מיוחד";
            this.SpecialLocation.UseVisualStyleBackColor = true;
            this.SpecialLocation.Click += new System.EventHandler(this.SpecialLocation_Click);
            // 
            // btnUnLockLocation
            // 
            this.btnUnLockLocation.BackColor = System.Drawing.Color.White;
            this.btnUnLockLocation.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnLockLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnUnLockLocation.Image")));
            this.btnUnLockLocation.Location = new System.Drawing.Point(577, 220);
            this.btnUnLockLocation.Name = "btnUnLockLocation";
            this.btnUnLockLocation.Size = new System.Drawing.Size(67, 72);
            this.btnUnLockLocation.TabIndex = 71;
            this.btnUnLockLocation.UseVisualStyleBackColor = false;
            this.btnUnLockLocation.Click += new System.EventHandler(this.btnUnLockLocation_Click);
            // 
            // touchScreen1
            // 
            this.touchScreen1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.touchScreen1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.touchScreen1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.touchScreen1.Location = new System.Drawing.Point(3, 466);
            this.touchScreen1.Name = "touchScreen1";
            this.touchScreen1.Size = new System.Drawing.Size(648, 266);
            this.touchScreen1.TabIndex = 59;
            this.touchScreen1.Load += new System.EventHandler(this.touchScreen1_Load);
            // 
            // frmActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(239)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1260, 735);
            this.ControlBox = false;
            this.Controls.Add(this.btnUnLockLocation);
            this.Controls.Add(this.SpecialLocation);
            this.Controls.Add(this.btnLockLocation);
            this.Controls.Add(this.txtTerminal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPermit);
            this.Controls.Add(this.btnKaron);
            this.Controls.Add(this.txtAVDM);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtInDate);
            this.Controls.Add(this.txtDriverName);
            this.Controls.Add(this.touchScreen1);
            this.Controls.Add(this.chkTrain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClassificationClassCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UNCode1);
            this.Controls.Add(this.btnComment);
            this.Controls.Add(this.txtLocationExp);
            this.Controls.Add(this.chkTruck);
            this.Controls.Add(this.txtTruckNumber);
            this.Controls.Add(this.lblWork);
            this.Controls.Add(this.lblOK);
            this.Controls.Add(this.txtWork);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblTruck);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblCostumer);
            this.Controls.Add(this.txtWh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.txtNewLocation);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.txtCostumer);
            this.Controls.Add(this.txtTchoula);
            this.Controls.Add(this.txtHandelingType);
            this.Controls.Add(this.txtContType);
            this.Controls.Add(this.txtContSize);
            this.Controls.Add(this.txtContNum);
            this.Controls.Add(this.txtContAlpha);
            this.Controls.Add(this.cboContainers);
            this.Controls.Add(this.lblline);
            this.Controls.Add(this.lblNewLocation);
            this.Controls.Add(this.lblTchoula);
            this.Controls.Add(this.groupBoxBtuonsInOut);
            this.KeyPreview = true;
            this.Name = "frmActivity";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.frmActivity_Activated);
            this.Load += new System.EventHandler(this.frmActivity_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmActivity_KeyDown);
            this.groupBoxBtuonsInOut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContAlpha;
        private System.Windows.Forms.TextBox txtContSize;
        private System.Windows.Forms.TextBox txtContType;
        private System.Windows.Forms.TextBox txtHandelingType;
        private System.Windows.Forms.TextBox txtTchoula;
        private System.Windows.Forms.TextBox txtCostumer;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtNewLocation;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtWh;
        private System.Windows.Forms.Label lblTchoula;
        private System.Windows.Forms.Label lblCostumer;
        private System.Windows.Forms.Label lblline;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblNewLocation;
        private System.Windows.Forms.Label lblTruck;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBoxBtuonsInOut;
        private Label lblOK;
        private Timer timerfrmactivity;
        private TextBox txtWork;
        private Label lblWork;
        private MaskedTextBox txtTruckNumber;
        private ComboBox cboContainers;
        private CheckBox chkTrain;
        private CheckBox chkTruck;
        private TextBox txtContNum;
        private Button btnDamage;
        private TextBox txtLocationExp;
        private Button btnDeal;
        private Button btnComment;
        private Label label2;
        private TextBox ClassificationClassCode;
        private Label label3;
        private TextBox UNCode1;
        private Button btnReserve;
        private TextBox txtDriverName;
        private MaskedTextBox txtInDate;
        private Button btnQuery;
        private Button btnWorks;
        private Button btnInformation;
        private Button btnExit;
        private Button btnGo;
        private NumericKeyPad.TouchScreen touchScreen1;
        private TextBox txtAVDM;
        private Button btnKaron;
        private TextBox txtPermit;
        private Label label4;
        private TextBox txtTerminal;
        private Button btnLockLocation;
        private Button SpecialLocation;
        private Button btnUnLockLocation;
    }
}
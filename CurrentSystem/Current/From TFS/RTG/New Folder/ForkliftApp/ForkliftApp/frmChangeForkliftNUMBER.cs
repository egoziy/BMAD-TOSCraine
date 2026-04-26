using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml;
using ForkliftApp.BusinessLogic;

namespace ForkliftApp
{
    public partial class frmChangeForkliftNumber : Form
    {
        private string cwd = Directory.GetCurrentDirectory();
        private string ForkLiftNum;
        private XmlDocument doc = new XmlDocument();

        public frmChangeForkliftNumber()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmChangeForkliftNumber_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            ForkLiftNum = cwd + ("\\" + "ForkLiftNumber.xml");
            doc.Load(ForkLiftNum);
            this.txtNewForkLiftNumber.Text = doc.DocumentElement.GetElementsByTagName("ForkLiftID")[0].InnerText;
            this.Text = this.Text + " - גירסה " + doc.DocumentElement.GetElementsByTagName("ForkliftAppVertzia")[0].InnerText;
            ForkliftAppBL folapp = new ForkliftAppBL(int.Parse(this.txtNewForkLiftNumber.Text), doc.DocumentElement.GetElementsByTagName("ForkliftAppVertzia")[0].InnerText);
            this.txtNewForkLiftNumber.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            doc.Load(ForkLiftNum);
            XmlElement rootNode = doc.DocumentElement;
            rootNode.GetElementsByTagName("ForkLiftID").Item(0).InnerText = this.txtNewForkLiftNumber.Text;  
            doc.Save(ForkLiftNum);
            doc.RemoveAll();
            frmLogIn frmlogIn = (frmLogIn)Application.OpenForms["frmLogIn"];
            frmlogIn.frmLogInLoad();
            this.Dispose();
        }
    }
}

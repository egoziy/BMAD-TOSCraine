using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ForkliftApp.BusinessLogic;
using ForkliftApp.DataAccess;

namespace ForkliftApp
{
    public partial class frmComment : Form
    {
        private string _stmanifest;
        private string _stcontainer;
        private string _stcomment;

        public frmComment()
        {
            InitializeComponent();
            _stmanifest = ForkliftAppBL.GetManifest();
            _stcontainer = ForkliftAppBL.GateContainer();
            _stcomment = ForkliftAppBL.Comment;
            this.Text += " למכולה - " + _stcontainer;
            this.txtComment.Focus();
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            if (this.txtComment.Text.Length> 20)
            {
                MessageBox.Show("לא ניתן להקיש יותר מעשרים תווים");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_stcomment == string.Empty)
            {
                _stcomment += this.txtComment.Text;
            }
            else
            {
                _stcomment += " " + this.txtComment.Text;
            }
            ForkliftAppDA.UpDateComment(_stmanifest, _stcontainer, _stcomment);
            frmActivity frmactivity = (frmActivity)Application.OpenForms["frmActivity"];
            frmactivity.PreBindControls();
            this.Dispose();
        }
    }
}

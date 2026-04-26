using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ForkliftApp.Entities;
using ForkliftApp.DataAccess;
using ForkliftApp.BusinessLogic;
using NumericKeyPad;

namespace ForkliftApp
{
    public partial class frmSpecialLocation : Form
    {
        private DataTable tbSpecialLocation = new DataTable();
        private ForkliftAppBL BL;

        public frmSpecialLocation()
        {
            InitializeComponent();
            tbSpecialLocation = ForkliftAppDA.GetSpecialLocation();
            this.SpecialLocation.DataSource = tbSpecialLocation;
            this.SpecialLocation.DisplayMember = "LocationCode";
            this.SpecialLocation.ValueMember = "LocationCode";
            this.SpecialLocation.Text = string.Empty;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SpecialLocation.Items.Count != 0)
            {
                ForkliftAppBL.m_SpecialLocation = this.SpecialLocation.SelectedValue.ToString().Trim();
            }
                this.Close();
            
        }

        private void OpenFormActivity()
        {
            this.Dispose();
            frmActivity frm = new frmActivity();
            frm.ShowDialog();
            frm.Focus();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

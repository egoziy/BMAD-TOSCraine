using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ForkliftApp.BusinessLogic;


namespace ForkliftApp
{
    public partial class frmSameDealNumber : Form
    {
        public frmSameDealNumber()
        {
            InitializeComponent();
            string dealnumber = ForkliftAppBL.GetDealNymber();
            DataTable dt = new DataTable();
            dt = ForkliftAppBL.GetTableForGrid();
            dataGridContsInDeal.DataSource = dt;
            this.Text = dt.Rows.Count + " " + this.Text +  " - " + dealnumber;
            this.txtSumContainers.Text = " סהכ " + dataGridContsInDeal.Rows.Count.ToString() + " מכולות ";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm(121);
        }

        private void CloseForm(int k)
        {
            this.Dispose();
            //          if (k == 121)
            //          {
 //           frmInformation frm = new frmInformation();
 //           frm.ShowDialog();
            //           }
           
           
        }
    }
}

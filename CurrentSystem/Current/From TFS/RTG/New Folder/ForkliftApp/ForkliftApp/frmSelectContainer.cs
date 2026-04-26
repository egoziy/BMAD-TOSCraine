using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ForkliftApp.Entities;
using ForkliftApp.DataAccess;
using ForkliftApp.BusinessLogic;

namespace ForkliftApp
{
    public partial class frmSelectContainer : Form
    {
        private ForkliftAppBL BL = null;
        private ForkliftAppDS ds = null;
        private DataTable dt = null;
        private string _container;
        private string _Manifest;
        private string _act;

        public frmSelectContainer()
        {
            InitializeComponent();
            BL = new ForkliftAppBL();
            dt = ForkliftAppBL.GetDT();
        }

        private void frmSelectContainer_Load(object sender, EventArgs e)
        {
            this.dataGridSelectContainer.DataSource = dt;
            int cn = dt.Columns.Count;
            for(int i=0; i<=cn-1;i++)
            {
                if (i!=0 && i!=15)
                {
                    this.dataGridSelectContainer.Columns[i].Visible = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            dgv = this.dataGridSelectContainer;
            int rcn = dgv.CurrentCell.RowIndex;
            _act = ForkliftAppBL.GetActivity();
            _container = dgv[0, rcn].Value.ToString();
            _Manifest = dgv[15, rcn].Value.ToString();
            ds = null;
            ds = ForkliftAppDA.GetContainersActivityMoreThen1(_Manifest, _container);
            DataTable dt = new DataTable();
            dt = ds.Tables["ContainersQuery"];
            if (dt.Rows.Count>0)
            {
                BL = new ForkliftAppBL(ds);
            }
            else
            {
                BL = new ForkliftAppBL(_Manifest,_container,string.Empty);
            }
            
            this.Dispose();
            frmActivity frm = new frmActivity();
            frm.ShowDialog();
        }
    }
}

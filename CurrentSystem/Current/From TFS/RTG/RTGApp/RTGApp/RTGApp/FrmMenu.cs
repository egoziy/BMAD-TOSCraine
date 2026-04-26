using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RTGApp
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void btnEmptyContainers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FrmEmptyContainers frm = new FrmEmptyContainers();
            frm.Show();
            frm.Focus();
            Cursor.Current = Cursors.Default;
        }

        private void btnEmptyLoction_Click(object sender, EventArgs e)
        {

            FrmContainerNoLocation frm = new FrmContainerNoLocation();
            frm.Show();
            frm.Focus();
        }

        private void btnInOutDiory_Click(object sender, EventArgs e)
        {

            FrmInOutDiory frm = new FrmInOutDiory();
            frm.Show();
            frm.Focus();
        }

        private void btnForLoction_Click(object sender, EventArgs e)
        {

            FrmContainersByBloc frm = new FrmContainersByBloc();
            frm.Show();
            frm.Focus();
        }

        private void btnRecommendedLocation_Click(object sender, EventArgs e)
        {

            FrmRecommendedLocation frm = new FrmRecommendedLocation();
            frm.Show();
            frm.Focus();
        }

        private void btnExpectedContainers_Click(object sender, EventArgs e)
        {

            FrmExpectedContainers frm = new FrmExpectedContainers();
            frm.Show();
            frm.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            ContainerLocation frm = new ContainerLocation();
            frm.Show();
            frm.Focus();
        }
    }
}

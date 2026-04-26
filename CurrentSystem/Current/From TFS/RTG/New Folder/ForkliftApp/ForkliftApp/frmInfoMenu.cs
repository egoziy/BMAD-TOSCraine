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
    public partial class frmInfoMenu : Form
    {
        private ForkliftAppBL BL;
        private string act;
        private DateTime StartTime;
        private DateTime TimeForExit;
        private string _Versia;
        private bool validuser;
        private string un;
        private string pw;

        public frmInfoMenu()
        {
            InitializeComponent();
            _Versia = ForkliftAppBL.GetVersia();
            this.Text = this.Text + " גירסה - " + _Versia;
            StartTime = DateTime.Now;
            TimeForExit = DateTime.Now;

            //// Instantiate the timer
            //timerfrmInfoMenu = new Timer();

            //// Setup timer
            //timerfrmInfoMenu.Interval = 1000; //1000ms = 1sec
            //timerfrmInfoMenu.Tick += new EventHandler(timerfrmInfoMenu_Tick);
            //timerfrmInfoMenu.Start();
        }

        /// <summary>
        /// חישוב זמן לסגירת מסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerfrmInfoMenu_Tick(object sender, EventArgs e)
        {
            if (sender == timerfrmInfoMenu)
            {
                BL = new ForkliftAppBL();
                DateTime endtime = DateTime.Now;
                int timeForExit = BL.PreDateDiffForExit(StartTime, endtime);

                //if (timeForExit == 30 || timeForExit > 30)
                //{
                //    timerfrmInfoMenu.Stop();
                //    this.Hide();
                //    frmLogIn frm = new frmLogIn();
                //    frm.ShowDialog();
                //}
            }
        }

        private void frmInfoMenu_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    BL = new ForkliftAppBL("EmptyContainers");
                    OpenFormEmptyContainers();
                    break;
                case Keys.F2:
                    BL = new ForkliftAppBL("EmptyLocation");
                    OpenFormEmptyContainers();
                    break;
                case Keys.F3:
                    OpenFormInOutDiory();
                    break;
                case Keys.F4:
                    BL = new ForkliftAppBL("ContainersForLoction");
                    OpenFormInOutDiory();
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                  break;
                case Keys.F10:
                    CloseForm();
                    break;
                default:
                    break;
            }       
        }

        private void CloseForm()
        {
            this.Dispose();
            frmInformation frm = new frmInformation();
            frm.ShowDialog();
        }


        private void btnExpectedContainers_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("ExpectedContainers");
            OpenFormEmptyContainers();
        }




        private void btnEmptyContainers_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("EmptyContainers");
            OpenFormEmptyContainers();
        }

        /// <summary>
        /// פתיחת מסך מידע
        /// </summary>
        private void OpenFormEmptyContainers()
        {
            this.Dispose();
            frmEmptyContainers frm = new frmEmptyContainers();
            frm.ShowDialog();
            frm.Focus();
        }

        private void btnEmptyLoction_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("EmptyLocation");
            OpenFormEmptyContainers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnInOutDiory_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("InOutDiory");
            OpenFormInOutDiory();
        }

        private void OpenFormInOutDiory()
        {
            this.Dispose();
            frmInOutDiory frm = new frmInOutDiory();
            frm.ShowDialog();
            frm.Focus();
        }

        private void btnForLoction_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("ContainersForLoction");
            OpenFormInOutDiory();
        }

        private void OpenFormRecommendedLocation()
        {
            this.Dispose();
            frmRecommendedLocation frm = new frmRecommendedLocation();
            frm.ShowDialog();
            frm.Focus();
        }


        private void btnRecommendedLocation_Click_1(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("RecommendedLocation");
            OpenFormRecommendedLocation();
        }
    }
}

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
    public partial class frmInOutDiory : Form
    {
        private DataTable table = new DataTable();
        private DataTable tbShioindLines = new DataTable();
        private DataGridView dgv;
        private DataView dv = new DataView();
        private ForkliftAppBL BL;
        private string act;
        private int rcn;
        private DateTime StartTime;
        private DateTime TimeForExit;
        private ForkliftAppDS ds = null;
        private string _Versia;
        private bool IsDiory;

        public frmInOutDiory()
        {

         //   tbShioindLines = ForkliftAppDA.GetHazardousSubstances();
        // this.cnbHazardousSubstances.DataSource = tbShioindLines;
          //  this.cnbHazardousSubstances.DisplayMember = "ClassificationClassCode";
         //   this.cnbHazardousSubstances.ValueMember = "ClassificationClassCode";
         //   this.cnbHazardousSubstances.Text = string.Empty;

            InitializeComponent();
            _Versia = ForkliftAppBL.GetVersia();
            this.Text = this.Text + " גירסה - " + _Versia;
            act = ForkliftAppBL.GetActivity();
            if (act == "InOutDiory")
            {
                this.cmbInOutDiory.Visible = true;
                this.lblInOutDiory.Visible = true;
                this.btnShowAllRecords.Visible = true;
               // this.lblAllRecords.Visible = true;
                this.cnbLoction.Visible = false;
                this.lblLoction.Visible = false;
                IsDiory = true;
                LoadData(IsDiory);
                
            }
            else
            {
                DataTable tbLoction = new DataTable();
                this.cmbInOutDiory.Visible = false;
                this.lblInOutDiory.Visible = false;
                this.btnShowAllRecords.Visible = false;
              //  this.lblAllRecords.Visible = false;
                this.cnbLoction.Visible = true;
                this.lblLoction.Visible = true;
                tbLoction = ForkliftAppDA.GetLoction();
                this.cnbLoction.DataSource = tbLoction;
                this.cnbLoction.DisplayMember = "LocationCode";
                this.cnbLoction.ValueMember = "LocationCode";
                this.cnbLoction.Text = string.Empty;


                tbLoction = ForkliftAppDA.GetCboSubstances();
                this.cnbHazardousSubstances.DataSource = tbLoction;
                this.cnbHazardousSubstances.DisplayMember = "ClassificationClassCode";
                this.cnbHazardousSubstances.ValueMember = "ClassificationClassCode";
                this.cnbHazardousSubstances.Text = string.Empty;
                this.cnbLoction.Focus();
            }
            
            StartTime = DateTime.Now;
            TimeForExit = DateTime.Now;

            //// Instantiate the timer
            //timerInOutDiory = new Timer();

            //// Setup timer
            //timerInOutDiory.Interval = 1000; //1000ms = 1sec
            //timerInOutDiory.Tick += new EventHandler(timerfrmEMContainers_Tick);
            //timerInOutDiory.Start();
        }

        /// <summary>
        /// חישוב זמן לסגירת מסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerfrmEMContainers_Tick(object sender, EventArgs e)
        {
            if (sender == timerInOutDiory)
            {
                BL = new ForkliftAppBL();
                DateTime endtime = DateTime.Now;
                int timeForExit = BL.PreDateDiffForExit(StartTime, endtime);

                //if (timeForExit == 30 || timeForExit > 30)
                //{
                //    timerfrmEMContainers.Stop();
                //    this.Hide();
                //    frmLogIn frm = new frmLogIn();
                //    frm.ShowDialog();
                //}
            }
        }

        /// <summary>
        /// רשומות נתונים למסך
        /// </summary>
        /// <param name="Start"></param>
        private void LoadData(bool isdiory)
        {
            if (isdiory == true)
            {
                table = ForkliftAppDA.GetInOutDiory();
            }
            else
            {
                table = null; // ForkliftAppDA.GetContsInForLoction(this.cnbLoction.Text);
            }
            if (table.Rows.Count > 0)
            {
                try
                {
                    this.dataGridInOutDiory.DataSource = table;
                    this.lblNoData.Visible = false;
                  //  dataGridInOutDiory.Focus();
                    
               }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("אין נתונים");
                
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm(121);
        }

        private void CloseForm(int k)
        {
            this.Dispose();
            if (k==121)
            {
                frmInformation frm = new frmInformation();
                frm.ShowDialog();
            }
            else if (k==114)
            {
                BL = new ForkliftAppBL("Query");
                frmActivity frm = new frmActivity();
                frm.ShowDialog();
            }
            else if (k==116)
            {
                frmInfoMenu frm = new frmInfoMenu();
                frm.ShowDialog();
            }
        }

        private void frmInOutDiory_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    ShowAllRecords();
                    break;
                case Keys.F3:
                    CloseForm(114);
                    break;
                case Keys.F5:
                    CloseForm(116);
                    break;
                case Keys.F10:
                    CloseForm(121);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ביטול סינון
        /// </summary>
        private void ShowAllRecords()
        {
            dv = new DataView(table);
            this.dataGridInOutDiory.DataSource = dv;
            this.lblNoData.Visible = false;
            this.dataGridInOutDiory.Visible = true;
            dataGridInOutDiory_RowEnter();
            this.cmbInOutDiory.Text = string.Empty;
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            CloseForm(116);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            CloseForm(114);
        }

        private void dataGridInOutDiory_SelectionChanged(object sender, EventArgs e)
        {
            dataGridInOutDiory.Focus();
            dataGridInOutDiory.Select();
            dataGridInOutDiory_RowEnter();
            
        }

        private void dataGridInOutDiory_RowEnter()
        {
            //BL = new ForkliftAppBL("In");
            dgv = this.dataGridInOutDiory;
            SelectRecord(dgv);
        }

        private void SelectRecord(DataGridView dgv)
        {
            try
            {
                rcn = dgv.CurrentCell.RowIndex;
                int recCountIn = dgv.RowCount-1;
                rcn = rcn += 1;
                txtSumMovments.Text = "רשומה: " + rcn + " מתוך " + recCountIn + " רשומות";
            }
            catch(NullReferenceException n)
            {
                string s = n.Message;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void frmInOutDiory_Load(object sender, EventArgs e)
        {
            dataGridInOutDiory_RowEnter();
        }

        /// <summary>
        /// סינון נתונים לגריד
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void cmbInOutDiory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbInOutDiory.Text!=string.Empty)
            {
                string st = this.cmbInOutDiory.Text;
                st = st.Trim();
                dv = new DataView(table);
                dv.RowFilter = "תנועה like " + "'%" + st + "%'";
                this.dataGridInOutDiory.DataSource = dv;
                if (dv.Count > 0)
                {
                    lblNoData.Visible = false;
                    this.dataGridInOutDiory.Visible = true;
                    dataGridInOutDiory_RowEnter();
                }
                else
                {
                    this.dataGridInOutDiory.Visible = false;
                    lblNoData.Visible = true;
                    this.txtSumMovments.Text = "0";
                }
            }
            else
            {
                LoadData(true);
            }
        }

        /// <summary>
        /// ביטול סינון
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            ShowAllRecords();
        }
       
        private void cnbLoction_Leave(object sender, EventArgs e)
        {
            GetDataForContsForLoction();
        }

        private void GetDataForAreaLocation()
        {
            int SumRows = 0;
                                 
            
            table = ForkliftAppDA.GetAreaLoction(this.txtLocatin.Text.Trim());
            if (table != null)
            {
                try
                {
                    this.dataGridInOutDiory.DataSource = table;
                    this.lblNoData.Visible = false;
                    dataGridInOutDiory.Focus();
                    SumRows = Convert.ToInt16(dataGridInOutDiory.Rows.Count - 1);
                    this.txtSumMovments.Text = " סהכ " + SumRows + " מכולות ";
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("אין נתונים");
                return;
            }
        }



        private void GetDataForContsForLoction()
        {
            int SumRows = 0;
            table = ForkliftAppDA.GetContsForLoction(this.cnbLoction.Text.Trim());
            if (table != null)
            {
                try
                {
                    this.dataGridInOutDiory.DataSource = table;
                    this.lblNoData.Visible = false;
                    dataGridInOutDiory.Focus();
                    SumRows = Convert.ToInt16(dataGridInOutDiory.Rows.Count-1);
                    this.txtSumMovments.Text = " סהכ " + SumRows + " מכולות ";
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("אין נתונים");
                return;
            }
        }


        private void GetDataForHazardousSubstances()
        {
            table = ForkliftAppDA.GetHazardousSubstances(this.cnbHazardousSubstances.Text.Trim());
            if (table != null)
            {
                try
                {
                    this.dataGridInOutDiory.DataSource = table;
                    this.lblNoData.Visible = false;
                    dataGridInOutDiory.Focus();
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("אין נתונים");
                return;
            }
        }

        private void cmbInOutDiory_Click(object senser, EventArgs e)
        {
            this.cmbInOutDiory.DroppedDown = true;
        }

        private void cnbLoction_Click(object senser, EventArgs e)
        {
            this.cnbLoction.DroppedDown = true;
        }

        private void cnbHazardousSubstances_Click(object senser, EventArgs e)
        {
            this.cnbHazardousSubstances.DroppedDown = true;
        }
        

        private void cnbLoction_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDataForContsForLoction();
        }

        private void cnbHazardousSubstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDataForHazardousSubstances();
        }


     
        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.cnbLoction.Text += ctl.Text.ToString();
            this.txtLocatin.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
           
            //if (this.cnbLoction.Text.IndexOf(" ", 0) > 0)
            //{
            //    this.cnbLoction.Text = this.cnbLoction.Text.Replace(" ", "");
            //    this.cnbLoction.Text = this.cnbLoction.Text.Substring(0, cnbLoction.Text.Length - 1);
            //    this.txtLocatin.Text = this.txtLocatin.Text.Replace(" ", "");
            //    this.txtLocatin.Text = this.txtLocatin.Text.Substring(0, txtLocatin.Text.Length - 1);
            //}
            //else
            //{
            //    this.cnbLoction.Text = this.cnbLoction.Text.Substring(0, cnbLoction.Text.Length - 1);
            //    this.txtLocatin.Text = this.txtLocatin.Text.Substring(0, txtLocatin.Text.Length - 1);
            //}
             this.cnbLoction.Text = string.Empty;
             this.txtLocatin.Text = string.Empty;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
           // this.cnbLoction.DroppedDown = true;
            GetDataForAreaLocation();
        }

        private void lblLoction_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridInOutDiory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblNoData_Click(object sender, EventArgs e)
        {

        }


    }
}


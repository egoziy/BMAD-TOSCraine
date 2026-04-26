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
    public partial class frmWorks : Form
    {
        private ForkliftAppDS dsin = null;
        private BindingSource bsin = new BindingSource();
        private DataTable table = new DataTable();
        private DataGridView dgv;
        private ForkliftAppBL BL;
        private string manifest = string.Empty;
        private string ContainerID = string.Empty;
        private string DealNumber = string.Empty;
        private string DealNumberSub = string.Empty;
        private bool ForkLiftDone = false;
        private string size = "0";
        private string Kind = string.Empty;
        private int rcn;
        private DateTime _Date;
        private DateTime TimeForExit;
        private DateTime starttime = new DateTime();
        private string _Versia;
        private string _WorkType;

        public frmWorks()
        {
            InitializeComponent();
            _Versia = ForkliftAppBL.GetVersia();
            this.Text = this.Text + " גירסה - " + _Versia;
            this.dateTimePickerTargetDate.Value = DateTime.Now;
            Kind = "Start";
            LoadData();
            DataTable dtc = new DataTable();
            dtc = ForkliftAppDA.getDataForCombo();
            BL = new ForkliftAppBL("Workes");
            this.cboWorks.DataSource = dtc;
            this.cboWorks.DisplayMember = "WorkTypeDesc";
            this.cboWorks.ValueMember = "WorkTypeCode";
            this.cboWorks.Text = string.Empty;
            starttime = DateTime.Now;
            TimeForExit = DateTime.Now;

            //// Instantiate the timer
            //timerWorks = new Timer();

            //// Setup timer
            //timerWorks.Interval = 1000; //1000ms = 1sec
            //timerWorks.Tick += new EventHandler(timerWorks_Tick);
            //timerWorks.Start();
        }

        /// <summary>
        /// רשומות נתונים לעבודות
        /// </summary>
        /// <param name="Start"></param>
        private void LoadData()
        {
            dsin = ForkliftAppDA.GetWorks();
            table = dsin.Tables["Works"];
            if (Kind != "ByDate") // ) this.txtTargetDate.Text != "  /  /"   || Kind == "Start" || Kind == string.Empty
            {
                try
                {
                    if (Kind == "Start" || Kind == string.Empty)
                    {
                        try
                        {
                            bsin.DataSource = table;
                            DataView dv = new DataView(table);
                            dv.Sort = "גו,סוג עבודה,לתאריך";
                            this.dataGridWorks.DataSource = dv;
                            this.lblNoData.Visible = false;
                            this.dataGridWorks.Visible = true;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    else
                    {
                        DataView dv = new DataView(table);
                        dv.RowFilter = "גו = " + size;
                        this.dataGridWorks.DataSource = dv;
                    }
                    this.dataGridWorks.Columns[8].Visible = false;
             //       this.dataGridWorks.Columns[9].Visible = false;
                    this.dataGridWorks.Columns[10].Visible = false;
                    this.dataGridWorks.Columns[11].Visible = false;
                    this.dataGridWorks.Columns[12].Visible = false;
                    this.dataGridWorks.Columns[13].Visible = false;
                    this.dataGridWorks.Columns[14].Visible = false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                    //throw new System.ArgumentOutOfRangeException() ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                bsin.DataSource = table;
                DataView dv = new DataView(table);
                string _DDate = _Date.ToShortDateString();
                dv.RowFilter =  "date1 = " + "'" + _DDate + "'" + "And" + "[סוג עבודה] like " + "'%" + this.cboWorks.Text + "%'";
                dv.Sort = "גו,לתאריך";
                this.dataGridWorks.DataSource = dv;
                this.dataGridWorks.Refresh();
            }

        }

        private void timerWorks_Tick(object sender, EventArgs e)
        {
            //if (sender == timerWorks)
            //{
            //    BL = new ForkliftAppBL();
            //    DateTime endtime = DateTime.Now;
            //    int time = DateDiffForUpdate(starttime, endtime);
            //    int timeForExit = BL.PreDateDiffForExit(TimeForExit, endtime);
            //    //if (timeForExit == 30 || timeForExit > 30)
            //    //{
            //    //    timerWorks.Stop();
            //    //    this.Hide();
            //    //    frmLogIn frm = new frmLogIn();
            //    //    frm.ShowDialog();
            //    //}
            //}
        }

        /// <summary>
        /// חישוב זמן לעידכון מסך
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private int DateDiffForUpdate(DateTime fromDate, DateTime toDate)
        {
            return toDate.Subtract(fromDate).Minutes;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            this.Dispose();
            frmInformation frm = new frmInformation();
            frm.ShowDialog();
        }

        private void frmWorks_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    Click20();
                    break;
                case Keys.F2:
                    Click40();
                    break;
                case Keys.F3:
                    BL = new ForkliftAppBL("Query");
                    OpenFormActivity();
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    this.Dispose();
                    frmInfoMenu frm = new frmInfoMenu();
                    frm.ShowDialog();
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    OpenFormActivity();
                    break;
                case Keys.F9:
                    ClickAll();
                    break;
                case Keys.F10:
                    CloseForm();
                    break;
                default:
                    break;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("Query");
            OpenFormActivity();
        }

        private void btnUnloading_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("NotinListIn");
            OpenFormActivity();
        }

        private void btnLoading_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("NotinLisOut");
            OpenFormActivity();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            ClickAll();
        }

        private void ClickAll()
        {
            Kind = "Start";
            size = "0";
            LoadData();
            this.dateTimePickerTargetDate.Value = DateTime.Now;
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            Click20();
        }

        private void Click20()
        {
            Kind = "Filter";
            size = "20";
            if (dateTimePickerTargetDate.Value != null) 
            {
                _Date = dateTimePickerTargetDate.Value;
            }

            LoadData();
        }

        private void btn40_Click(object sender, EventArgs e)
        {
            Click40();
        }

        private void Click40()
        {
            Kind = "Filter";
            size = "40";
            if (dateTimePickerTargetDate.Value != null) 
            {
                _Date = dateTimePickerTargetDate.Value;
            }

            LoadData();
        }

        private void cboWorks_Click(object senser, EventArgs e)
        {
            this.cboWorks.DroppedDown = true;
        }


        private void cboWorks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string st = this.cboWorks.Text;
            if (this.cboWorks.Text != "System.Data.DataRowView")
            {
                DataView dv = new DataView(table);
                try
                {
                    if (size != "0")
                    {
                        dv.RowFilter = "גו = " + size + "And" + "[סוג עבודה] like " + "'%" + st + "%'";
                        dv.Sort = "גו,לתאריך";
                    }
                    else
                    {
                        dv.RowFilter = "[סוג עבודה] like " + "'%" + st + "%'";
                        dv.Sort = "גו,לתאריך";
                    }
                    this.dataGridWorks.DataSource = dv;
                    if (this.dataGridWorks.RowCount > 1)
                    {
                        this.dataGridWorks.Visible = true;
                        this.lblNoData.Visible = false;
                    }
                    else
                    {
                        this.dataGridWorks.Visible = false;
                        this.lblNoData.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridWorks_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dgv = this.dataGridWorks;
                rcn = dgv.CurrentCell.RowIndex;
                ContainerID = dgv[0, rcn].Value.ToString();
                manifest = dgv[12, rcn].Value.ToString().TrimEnd();
                DealNumber = dgv[10, rcn].Value.ToString();
                DealNumberSub = dgv[11, rcn].Value.ToString();
                _WorkType = dgv[11, rcn].Value.ToString();
                this.txtComment.Text = dgv[8, rcn].Value.ToString();
                if (dgv[7, rcn].Value.ToString() != string.Empty)
                {
                    ForkLiftDone = Convert.ToBoolean(dgv[7, rcn].Value);
                }
                else
                {
                    ForkLiftDone = false;
                }
                BL = new ForkliftAppBL(manifest, ContainerID, DealNumber, DealNumberSub, ForkLiftDone, _WorkType);
            }
            catch (NullReferenceException)
            {
                return;
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

        private void btnInformation_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmInfoMenu frm = new frmInfoMenu();
            frm.ShowDialog();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFormActivity();
        }

        /// <summary>
        /// פתיחת מסך פריקה.טעינה.שאילתה
        /// </summary>
        private void OpenFormActivity()
        {
            //ForkliftAppBL act = new ForkliftAppBL("Workes");
            this.Hide();
            frmActivity frm = new frmActivity();
            frm.ShowDialog();
            frm.Focus();
        }

        private void dataGridWorks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridWorks_SelectionChanged(sender, e);
            OpenFormActivity();
        }

        private void btnAllWorks_Click(object sender, EventArgs e)
        {
            ClickAll();
        }

        /// <summary>
        /// מסנן תאריך בדיקה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerTargetDate_CloseUp(object sender, EventArgs e)
        {
            _Date = this.dateTimePickerTargetDate.Value;
            Kind = "ByDate";
            LoadData();
        }

        /// <summary>
        /// עידכון מלגזן ביצוע או ביטול ביצוע
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridWorks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rcn = dgv.CurrentCell.RowIndex;
            if (ForkLiftDone == false)
            {
                ForkLiftDone = true;
            }
            else
            {
                ForkLiftDone = false;
            }
            BL = new ForkliftAppBL();
            ForkliftAppDA.UpdateWorksForklift(ContainerID, manifest);
        }

        private void lblExit_Click(object sender, EventArgs e)
        {

        }

        private void lblInformation_Click(object sender, EventArgs e)
        {

        }

        private void lblQuery_Click(object sender, EventArgs e)
        {

        }

        private void lblSelect_Click(object sender, EventArgs e)
        {

        }
    }
}
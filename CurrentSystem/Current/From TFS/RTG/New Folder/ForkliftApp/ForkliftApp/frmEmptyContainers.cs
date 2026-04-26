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
    public partial class frmEmptyContainers : Form
    {
        private DataTable table = new DataTable();
        private DataTable table1 = new DataTable();
        private DataTable tbShioindLines = new DataTable();
        private DataTable tbContainerType = new DataTable();
        private DataGridView dgv;
        private ForkliftAppBL BL;
        private string act;
        private string _manifest = string.Empty;
        private string _ContainerID = string.Empty;
        private string _DealNumber = string.Empty;
        private string _DealNumberSub = string.Empty;
        private int rcn;
        private DateTime StartTime;
        private DateTime TimeForExit;
        private ForkliftAppDS ds = null;
        private string size = "0";
        private string Kind = string.Empty;
        private string st = string.Empty;
        private string str = string.Empty;
        private string _Versia;

        public frmEmptyContainers()
        {
            InitializeComponent();
            _Versia = ForkliftAppBL.GetVersia();
            this.Text = this.Text + " גירסה - " + _Versia;
            act = ForkliftAppBL.GetActivity();
            if (act=="EmptyContainers")
            {
                EnbleControls(true);
                this.Text = "מכולות ריקות לסוכן";
                this.cmbHandlingTypeCode.Enabled = false;
            }
            else if (act == "EmptyLocation")
            {
                EnbleControls(false);
                this.Text = "מכולות ללא איתור";
            }
            else if (act == "ExpectedContainers")
            {
                EnbleControls(true);
                this.Text = "מכולות צפויות";
            }

            if (act == "EmptyContainers" || act == "ExpectedContainers")
            {
               tbContainerType = ForkliftAppDA.GetContainerType();
               this.cmbcontainerType.DataSource = tbContainerType;
               this.cmbcontainerType.DisplayMember = "ContainerTypeCode";
               this.cmbcontainerType.ValueMember = "ContainerTypeCode";
               this.cmbcontainerType.Text = string.Empty;
            }

            tbShioindLines = ForkliftAppDA.GetSipingLines();
            this.cmbShipingLine.DataSource = tbShioindLines;
            this.cmbShipingLine.DisplayMember = "CarrierCode";
            this.cmbShipingLine.ValueMember = "CarrierCode";
            this.cmbShipingLine.Text = string.Empty;
            LoadData("Start");




            //StartTime = DateTime.Now;
            //TimeForExit = DateTime.Now;

            //// Instantiate the timer
            //timerfrmEMContainers = new Timer();

            //// Setup timer
            //timerfrmEMContainers.Interval = 1000; //1000ms = 1sec
            //timerfrmEMContainers.Tick += new EventHandler(timerfrmEMContainers_Tick);
            //timerfrmEMContainers.Start();
        }

        /// <summary>
        /// חישוב זמן לסגירת מסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerfrmEMContainers_Tick(object sender, EventArgs e)
        {
            if (sender == timerfrmEMContainers)
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
        private void LoadData(string Kind)
        {
            if (act == "ExpectedContainers")
            {
                table = ForkliftAppDA.GetExpectedContainers();
                if (table.Rows.Count > 0)
                {
                    try
                    {
                        if (Kind == "Start")
                        {
                            this.cmbShipingLine.Text = null;
                            this.cmbcontainerType.Text = null;
                            this.cmbHandlingTypeCode.Text = null;
                            this.cmbContainerLength.Text = null;
                            this.dataGridEMContainers.DataSource = table;
                        }
                        else
                        {
                            this.cmbShipingLine.Text = st;
                            DataView dv = new DataView(table);
                            if (st != string.Empty)
                            {
                                dv.RowFilter = "[קו] like " + "'%" + st + "%'" + "And" + "[גודל] = " + size;
                                if (size == "40")
                                {
                                    dv.Sort = "[גודל] ASC";
                                }
                                this.dataGridEMContainers.DataSource = dv;
                            }
                            else
                            {
                                dv.RowFilter = "[גודל] = " + size;
                                this.dataGridEMContainers.DataSource = dv;
                            }
                        }
                        for (int i = 0; i < dataGridEMContainers.Columns.Count; i++)
                        {
                            if (i >= 8)
                            {
                                this.dataGridEMContainers.Columns[i].Visible = false;
                            }
                        }
                        this.lblNoData.Visible = false;
                        dataGridEMContainers_RowEnter();
                        this.txtSumContainers.Text = this.txtSumContainers.Text = " סהכ " + table.Rows.Count.ToString() + " מכולות ";
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





            }
            else if (this.cmbShipingLine.Enabled)
            {
                this.cmbShipingLine.Text = string.Empty;
                table = ForkliftAppDA.GetContainersEM();
                if (table.Rows.Count>0)
                {
                    try
                    {
                        if (Kind=="Start")
                        {
                            this.cmbShipingLine.Text = null;
                            this.cmbcontainerType.Text = null;
                            this.dataGridEMContainers.DataSource = table;
                        }
                        else
                        {
                            this.cmbShipingLine.Text = st;
                            DataView dv = new DataView(table);
                            if (st!=string.Empty)
                            {
                                dv.RowFilter = "[קו] like " + "'%" + st + "%'" + "And" + "[גודל] = " + size;
                                if (size=="40")
                                {
                                    dv.Sort = "[גודל] ASC";
                                }
                               this.dataGridEMContainers.DataSource = dv;
                            }
                            else
                            {
                                dv.RowFilter = "[גודל] = " + size;
                                this.dataGridEMContainers.DataSource = dv;
                            }
                        }
                        for (int i = 0; i < dataGridEMContainers.Columns.Count; i++)
                        {
                            if (i >= 7)
                            {
                                this.dataGridEMContainers.Columns[i].Visible = false;
                            }
                        }
                        this.lblNoData.Visible = false;
                        dataGridEMContainers_RowEnter();
                        this.txtSumContainers.Text = this.txtSumContainers.Text = " סהכ " + table.Rows.Count.ToString() + " מכולות ";
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
                    this.dataGridEMContainers.Visible = false;
                    this.lblNoData.Visible = true;
                    EnbleControls(false);
                }
            }
            else
            {
                table = ForkliftAppDA.GetEmtyLoction();
                this.dataGridEMContainers.DataSource = table;
                for (int i = 0; i < dataGridEMContainers.Columns.Count; i++)
                {
                    if (i >= 4)
                    {
                        this.dataGridEMContainers.Columns[i].Visible = false;
                    }
                }
                dataGridEMContainers_RowEnter();
                this.txtSumContainers.Text = " סהכ " + table.Rows.Count.ToString() + " מכולות ";
            }
        }

        private void btnShowAllRecordsClick()
        {

            LoadData("Start");
        }

        private void cmbShipingLine_SelectedValueChanged(object sender, EventArgs e)
        {
            string StrFilter = string.Empty;
            if (this.cmbShipingLine.Enabled)
            {
                if (this.cmbShipingLine.Text != string.Empty)
                {
                    st = this.cmbShipingLine.Text;
                    st = st.Trim();
                    StrFilter = " [קו] like " + "'%" + st + "%'";
                }
                if (this.cmbcontainerType.Text != string.Empty)
                {
                    str = this.cmbcontainerType.Text;
                    str = str.Trim();

                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND [סוג] like " + "'%" + str + "%'";
                    }
                    else
                    {
                        StrFilter = " [סוג] like " + "'%" + str + "%'";
                    }
                }
                if (this.cmbContainerLength.Text != string.Empty)
                {
                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND  גודל= '" + this.cmbContainerLength.Text + "'";
                    }
                    else
                    {
                        StrFilter = "  [גודל]= '" + this.cmbContainerLength.Text + "'";
                    }
                }
                if (this.cmbHandlingTypeCode.Text != string.Empty)
                {
                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                    }
                    else
                    {
                        StrFilter = "  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                    }
                }
                if (table.Rows.Count > 0)
                {
                    DataView dv = new DataView(table);
                    dv.RowFilter = StrFilter;
                    this.dataGridEMContainers.DataSource = dv;
                    if (dv.Count > 0)
                    {
                        lblNoData.Visible = false;
                        this.dataGridEMContainers.Visible = true;
                        //  dataGridEMContainers_RowEnter();
                        this.txtSumContainers.Text = " סהכ " + dv.Count.ToString() + " מכולות ";
                    }
                    else
                    {
                        this.dataGridEMContainers.Visible = false;
                        lblNoData.Visible = true;
                        this.txtSumContainers.Text = "0";
                    }
                }
                else
                {
                    LoadData("Start");
                }
            }
        }

        private void cmbHandlingTypeCode_SelectedValueChanged(object sender, EventArgs e)
        {
            string StrFilter = string.Empty;
            if (this.cmbShipingLine.Enabled)
            {
                if (this.cmbShipingLine.Text != string.Empty)
                {
                    st = this.cmbShipingLine.Text;
                    st = st.Trim();
                    StrFilter = " [קו] like " + "'%" + st + "%'";
                }
                if (this.cmbcontainerType.Text != string.Empty)
                {
                    str = this.cmbcontainerType.Text;
                    str = str.Trim();

                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND [סוג] like " + "'%" + str + "%'";
                    }
                    else
                    {
                        StrFilter = " [סוג] like " + "'%" + str + "%'";
                    }
                }
                if (this.cmbContainerLength.Text != string.Empty)
                {
                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND  גודל= '" + this.cmbContainerLength.Text + "'";
                    }
                    else
                    {
                        StrFilter = "  [גודל]= '" + this.cmbContainerLength.Text + "'";
                    }
                }
                    if (this.cmbHandlingTypeCode.Text != string.Empty)
                    {
                        if (StrFilter != string.Empty)
                        {
                            StrFilter = StrFilter + "AND  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                        }
                        else
                        {
                            StrFilter = "  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                        }
                    }
               
                if (table.Rows.Count > 0)
                {
                    DataView dv = new DataView(table);
                    dv.RowFilter = StrFilter;
                    this.dataGridEMContainers.DataSource = dv;
                    if (dv.Count > 0)
                    {
                        lblNoData.Visible = false;
                        this.dataGridEMContainers.Visible = true;
                        //  dataGridEMContainers_RowEnter();
                        this.txtSumContainers.Text = " סהכ " + dv.Count.ToString() + " מכולות ";
                    }
                    else
                    {
                        this.dataGridEMContainers.Visible = false;
                        lblNoData.Visible = true;
                        this.txtSumContainers.Text = "0";
                    }
                }
                else
                {
                    LoadData("Start");
                }
            }




        }




        private void cmbcontainerType_SelectedValueChanged(object sender, EventArgs e)
        {
            string StrFilter = string.Empty;
            if (this.cmbShipingLine.Text != string.Empty)
            {
                st = this.cmbShipingLine.Text;
                st = st.Trim();
                StrFilter = "[קו] like " + "'%" + st + "%'";
            }
                if (this.cmbcontainerType.Text != string.Empty)
                {
                    str = this.cmbcontainerType.Text;
                    str = str.Trim();
                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND [סוג] like " + "'%" + str + "%'";
                    }
                    else
                    {
                        StrFilter = StrFilter + " [סוג] like " + "'%" + str + "%'";
                    }
                }
                if (this.cmbContainerLength.Text != string.Empty)
                {
                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND  [גודל]= '" + this.cmbContainerLength.Text + "'";
                    }
                    else
                    {
                        StrFilter = "  [גודל]= '" + this.cmbContainerLength.Text + "'";
                    }
                }
                if (this.cmbHandlingTypeCode.Text != string.Empty)
                {
                    if (StrFilter != string.Empty)
                    {
                        StrFilter = StrFilter + "AND  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                    }
                    else
                    {
                        StrFilter = "  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                    }
                }
                    if (table.Rows.Count > 0)
                    {
                        DataView dv = new DataView(table);
                        dv.RowFilter = StrFilter;
                        this.dataGridEMContainers.DataSource = dv;
                        if (dv.Count > 0)
                        {
                            lblNoData.Visible = false;
                            this.dataGridEMContainers.Visible = true;
                       //     dataGridEMContainers_RowEnter();
                            this.txtSumContainers.Text = " סהכ " + dv.Count.ToString() + " מכולות ";
                        }
                        else
                        {
                            this.dataGridEMContainers.Visible = false;
                            lblNoData.Visible = true;
                            this.txtSumContainers.Text = "0";
                        }
                    }
                    else
                    {
                        LoadData("Start");
                    }
        }


        private void cmbContainerLength_SelectedValueChanged(object sender, EventArgs e)
        {
            string StrFilter = string.Empty;
            if (this.cmbShipingLine.Text != string.Empty)
            {
                st = this.cmbShipingLine.Text;
                st = st.Trim();
                StrFilter = "[קו] like " + "'%" + st + "%'";
            }
            if (this.cmbcontainerType.Text != string.Empty)
            {
                str = this.cmbcontainerType.Text;
                str = str.Trim();
                if (StrFilter != string.Empty)
                {
                    StrFilter = StrFilter + "AND [סוג] like " + "'%" + str + "%'";
                }
                else
                {
                    StrFilter = StrFilter + " [סוג] like " + "'%" + str + "%'";
                }
            }
            if (this.cmbContainerLength.Text != string.Empty)
            {
                if (StrFilter != string.Empty)
                {
                    StrFilter = StrFilter + "AND  [גודל]= '" + this.cmbContainerLength.Text + "'";
                }
                else
                {
                    StrFilter = " [גודל]= '" + this.cmbContainerLength.Text + "'";
                }
            }
            if (this.cmbHandlingTypeCode.Text != string.Empty)
            {
                if (StrFilter != string.Empty)
                {
                    StrFilter = StrFilter + "AND  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                }
                else
                {
                    StrFilter = "  טיפול= '" + this.cmbHandlingTypeCode.Text + "'";
                }
            }

            if (table.Rows.Count > 0)
            {
                DataView dv = new DataView(table);
                dv.RowFilter = StrFilter;
                this.dataGridEMContainers.DataSource = dv;
                if (dv.Count > 0)
                {
                    lblNoData.Visible = false;
                    this.dataGridEMContainers.Visible = true;
                 //   dataGridEMContainers_RowEnter();
                    this.txtSumContainers.Text = " סהכ " + dv.Count.ToString() + " מכולות ";
                }
                else
                {
                    this.dataGridEMContainers.Visible = false;
                    lblNoData.Visible = true;
                    this.txtSumContainers.Text = "0";
                }
            }
            else
            {
                LoadData("Start");
            }


        }
        
        
        private void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            this.cmbShipingLine.Text = string.Empty;
            this.cmbcontainerType.Text = string.Empty;
            this.cmbContainerLength.Text = string.Empty;
            btnShowAllRecordsClick();

        }

        private void CloseForm()
        {
            this.Dispose();
            frmInformation frm = new frmInformation();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void frmEmptyContainers_KeyDown(object sender, KeyEventArgs e)
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
                    LoadData("Start");
                    break;
                case Keys.F10:
                    CloseForm();
                    break;
                default:
                    break;
            }
        }

        private void EnbleControls(bool EnCo)
        {
            foreach (Control c in Controls)
            {
                if (c is ComboBox )
                {
                    ((ComboBox)c).Enabled = EnCo;
                }
                else if (c is Label)
                {
                    if (c.Name != "lblNoData")
                    {
                        ((Label)c).Enabled = EnCo;
                    }
                }
                else if (c is Button)
                {
                    if (c.Name == "btnShowAllRecords" || c.Name == "btn20" || c.Name == "btn40")
                    {
                        ((Button)c).Enabled = EnCo;
                    }
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFormActivity();
        }

        /// <summary>
        /// בחירת הנתותונים לשימוש הפרמטרים של המכולה והמיצהר
        /// </summary>
        private void SelectRecord()
        {
            string Activety = string.Empty;
            try
            {
                DataGridView dgv = new DataGridView();
                dgv = this.dataGridEMContainers;
                int rcn = dgv.CurrentCell.RowIndex;
                //act = ForkliftAppBL.GetActivity();
                if (act == "EmptyContainers")
                {
                    _ContainerID = dgv[0, rcn].Value.ToString();
                    _manifest = dgv[8, rcn].Value.ToString();
                    _DealNumber = dgv[9, rcn].Value.ToString();
                    _DealNumberSub = dgv[10, rcn].Value.ToString();
                }
                else
                {
                    _ContainerID = dgv[0, rcn].Value.ToString();
                    _manifest = dgv[8, rcn].Value.ToString();
                    _DealNumber = dgv[4, rcn].Value.ToString();
                    _DealNumberSub = dgv[5, rcn].Value.ToString();
                }
                BL = new ForkliftAppBL(_manifest, _ContainerID, _DealNumber, _DealNumberSub,false,string.Empty);
            }
            catch (NullReferenceException ne)
            {
                ne = new NullReferenceException();
                //throw;
            }
            catch (Exception e)
            {
                MessageBox.Show("טעות בטעינת נתונים" + e.Message);
            }
        }

        private void dataGridEMContainers_SelectionChanged(object sender, EventArgs e)
        {
            //BL = new ForkliftAppBL("EmptyContainers");
            SelectRecord();
            dataGridEMContainers.Focus();
            dataGridEMContainers.Select();
            dataGridEMContainers_RowEnter();
        }

        private void dataGridEMContainers_RowEnter()
        {
            dgv = this.dataGridEMContainers;
            SelectRecord(dgv);

        }

        /// <summary>
        /// פתיחת מסך פריקה.טעינה.שאילתה
        /// </summary>
        private void OpenFormActivity()
        {
            this.Dispose();
            frmActivity frm = new frmActivity();
            frm.ShowDialog();
            frm.Focus();
        }

        private void dataGridEMContainers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRecord();
            OpenFormActivity();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("Query");
            OpenFormActivity();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmInfoMenu frm = new frmInfoMenu();
            frm.ShowDialog();
        }

        private void cmbShipingLine_Click(object senser, EventArgs e)
        {
            this.cmbShipingLine.DroppedDown = true;
        }

        private void cmbContainerLength_Click(object senser, EventArgs e)
        {
            this.cmbContainerLength.DroppedDown = true;
        }

        private void cmbcontainerType_Click(object senser, EventArgs e)
        {
            this.cmbcontainerType.DroppedDown = true;
        }

        private void cmbHandlingTypeCode_Click(object senser, EventArgs e)
        {
            this.cmbHandlingTypeCode.DroppedDown = true;
        }


        private void frmEmptyContainers_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'terminalDataDataSet1.TC_ContainerType' table. You can move, or remove it, as needed.
            this.tC_ContainerTypeTableAdapter.Fill(this.terminalDataDataSet1.TC_ContainerType);
            dataGridEMContainers_RowEnter();
        }

        private void SelectRecord(DataGridView dgv)
        {
            try
            {
                rcn = dgv.CurrentCell.RowIndex;
                int recCountIn = dgv.RowCount-1;
                rcn = rcn += 1;
                txtSumContainers.Text = "רשומה: " + rcn + " מתוך " + recCountIn + " רשומות";
            }
            catch (NullReferenceException)
            {
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            Click20();
        }

        private void Click20()
        {
            if (this.cmbShipingLine.Text!=string.Empty)
            {
                st = this.cmbShipingLine.Text;
                st = st.Trim();
            }
            if (this.cmbcontainerType.Text != string.Empty)
            {
                str = this.cmbcontainerType.Text;
                str = str.Trim();
            }

            Kind = "Filter";
            size = "20";
            LoadData("Filter");
        }

        private void btn40_Click(object sender, EventArgs e)
        {
            Click40();
        }

        private void Click40()
        {
            if (this.cmbShipingLine.Text != string.Empty)
            {
                st = this.cmbShipingLine.Text;
                st = st.Trim();
            }
            Kind = "Filter";
            size = "40";
            LoadData("Filter");
        }

        private void lblQuery_Click(object sender, EventArgs e)
        {

        }

       

        private void lblSelect_Click(object sender, EventArgs e)
        {

        }

        private void lblExit_Click(object sender, EventArgs e)
        {

        }

        private void lblInformation_Click(object sender, EventArgs e)
        {

        }

 


    }
}

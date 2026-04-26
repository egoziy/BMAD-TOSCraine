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
using System.Linq;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net.NetworkInformation;



namespace ForkliftApp
{

    public partial class frmInformation : Form
    {
        public event EventHandler DropDown;
        private ForkliftAppDS dsin = null;
        private ForkliftAppDS dsout = null;
        private ForkliftAppDS dsEmout = null;
        private BindingSource bsin = new BindingSource();
        private BindingSource bsout = new BindingSource();
        private DataGridView dgv;
        private ForkliftAppBL BL;
        private string manifest = string.Empty;
        private string ContainerID = string.Empty;
        private string DealNumber = string.Empty;
        private string DealNumberSub = string.Empty;
        private string CarrierCode = string.Empty;
        private string TruckID = string.Empty;
        private int rcn;
        private DateTime TimeForExit;
        private DateTime starttime = new DateTime();
        private string _Versia;
        private DataTable table = new DataTable();
        public Color DefaultBackcolorBtn = new Color();
        
        public frmInformation()
        {
            InitializeComponent();

            _Versia = ForkliftAppBL.GetVersia();
            this.Text = this.Text + " גירסה - " + _Versia;
            //LoadData(true);
            starttime = DateTime.Now;
            TimeForExit = DateTime.Now;
                    if (ForkliftAppBL.IsEM)
                    {
                        this.ViewType.SelectedIndex = 1;
                        this.checkEM.Checked = true;
                    }
                    else
                    {
                        this.ViewType.SelectedIndex = 0;
                        this.checkEM.Checked = false;
                        //dataGridContaunersOutEmpty.Visible = false;
                        //lblHaderEMOut.Visible = false;
                        //Random Rd = new Random();
                        //this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 280);
                        //this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 280);
                        //this.lblHaderOut.Location = new Point(5, 310);
                        //this.lblCoutRecordesOut.Location = new Point(100, 313);
                        //this.dataGridContaunersOutList.Location = new Point(5, 335);
                    }
                    DefaultBackcolorBtn = Color.FromArgb(224, 224, 224);
        }
            //// Instantiate the timer
            //timerfrmInformation = new Timer();

            //// Setup timer
            //timerfrmInformation.Interval = 1000; //1000ms = 1sec
            //timerfrmInformation.Tick += new EventHandler(timerfrmInformation_Tick);
            //timerfrmInformation.Start();


        /// <summary>
        /// רשומות נתונים לפריקה והעמסה
        /// </summary>
        /// <param name="Start"></param>
        private void LoadData(bool Start)
        {
            if (Start == true)
            {
                dsin = ForkliftAppDA.GetContainers("In", ForkliftAppBL.m_Terminal);
                dsout = ForkliftAppDA.GetContainers("Out", ForkliftAppBL.m_Terminal);
                dsEmout = ForkliftAppDA.GetContainers("EMOut", ForkliftAppBL.m_Terminal);
                dataGridContaunersInList.Focus();
            }
            else
            {
                //ds = ForkliftAppDA.GetContainersByNumber(this.txtFundContainer.Text);
            }
            lblTest.Visible = false;
            lblShikuf.Visible = false;
            lblWorkshader.Visible = false;
            DataTable dtw = new DataTable();
            dtw = ForkliftAppBL.GetWorsData();
            DataView dv = new DataView(dtw);
            dv.RowFilter = "WorkTypeCode = 06" + "And" + "[בוצע] = 0";  // +"06";
            int TestCount = dv.Count;
            DataView dvs = new DataView(dtw);
            dvs.RowFilter = "WorkTypeCode = 04" + "And" + "[בוצע] = 0";
            int ShikufCount = dvs.Count;
            if (TestCount > 0 || ShikufCount > 0)
            {
                lblWorkshader.Visible = true;
                if (TestCount > 0)
                {
                    lblTest.Visible = true;
                }
                else
                {
                    lblTest.Visible = false;
                }
                if (ShikufCount > 0)
                {
                    lblShikuf.Visible = true;
                }
                else
                {
                    lblShikuf.Visible = false;
                }
            }
        }

        /// <summary>
        /// איכלוס הפקדים
        /// </summary>
        private void BindControls()
        {
            // גריד פריקת מכולות
            DataTable dti = dsin.Tables["ContainersIn"];
            bsin.DataSource = dti;
            DataView dvi = new DataView(dti);
            if (ForkliftAppBL.IsEM) //this.checkEM.Checked
            {
                dvi.RowFilter = "קוד = 'EM'";
            } 
            dvi.Sort = "המתנה DESC";
            //dv.RowFilter = "Nume like " + "'%" + Cod.Text + "%'";
            //dataGrid1.DataSource = dv;
            dataGridContaunersInList.DataSource = dvi;
            for (int i = 0; i < dataGridContaunersInList.Columns.Count; i++)
            {
                if (i >= 9 & i < 23)  
                {
                    this.dataGridContaunersInList.Columns[i].Visible = false;
                }
            }
            try
            {
                dgv = this.dataGridContaunersInList;
                int recCountIn = dgv.RowCount;
                rcn = dgv.CurrentCell.RowIndex + 1;
                lblCoutRecordesIn.Text = "רשומה: " + rcn + " מתוך " + recCountIn + " רשומות";
            }
            catch (NullReferenceException)
            {
               // if (ViewType.SelectedIndex == 1)//   if (checkEM.Checked)
                //    return;
               // else
                    // MessageBox.Show("אין כרגע מכולות ממתינות לפריקה!");
                 //   return;
             
           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // גריד העמסת מכולות
            DataTable dto = dsout.Tables["ContainersOut"];
            bsout.DataSource = dto;
            DataView dvo = new DataView(dto);
            if (ForkliftAppBL.IsEM)
            {
                dvo.RowFilter = "קוד = 'EM'";
            } 
            dvo.Sort = "המתנה DESC";
            dataGridContaunersOutList.DataSource = dvo;

            for (int i = 0; i < dataGridContaunersOutList.Columns.Count; i++)
            {
                if (i >= 9 & i < 23)  
                {
                    this.dataGridContaunersOutList.Columns[i].Visible = false;
                }
                this.dataGridContaunersOutList.Columns[25].Visible = false;
            }
            //  צובע בגריד של הטעינה שורה של מכולה שיוצאת לשיקוף
            foreach (DataGridViewRow row in dataGridContaunersOutList.Rows)
            if (row.Cells[25].Value.ToString() == "1")
            {
                row.DefaultCellStyle.BackColor = Color.Yellow;  //FromArgb(191, 255, 0);
            }
            try
            {
                dgv = this.dataGridContaunersOutList;
                int recCountOut = dgv.RowCount;
                rcn = dgv.CurrentCell.RowIndex + 1;
                lblCoutRecordesOut.Text = "רשומה: " + rcn + " מתוך " + recCountOut + " רשומות";
            }
            catch (NullReferenceException)
            {
              // if (ViewType.SelectedIndex == 1)// if (checkEM.Checked)
              //      return;
                //else
                  //  MessageBox.Show("אין כרגע מכולות ממתינות להעמסה!");
                   // return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //**************************************************
            // גריד העמסת מכולות ריקות
            //**************************************************

            DataTable dtEmout = dsEmout.Tables["ContainersEMOut"];
            bsout.DataSource = dtEmout;
            DataView dvEMOut = new DataView(dtEmout);

            dvEMOut.Sort = "זמן DESC";
            dataGridContaunersOutEmpty.DataSource = dvEMOut;

            for (int i = 0; i < dataGridContaunersOutEmpty.Columns.Count; i++)
            {
                if (i >= 9 & i <= 24)
                {
                    this.dataGridContaunersOutEmpty.Columns[i].Visible = false;
                }
            }
            try
            {
                dgv = this.dataGridContaunersOutEmpty;
                int recCountOut = dgv.RowCount;
                rcn = dgv.CurrentCell.RowIndex + 1;
                lblCoutRecordesOut.Text = "רשומה: " + rcn + " מתוך " + recCountOut + " רשומות";
            }
            catch (NullReferenceException)
            {
             //   if (ViewType.SelectedIndex == 1)  //if (checkEM.Checked)
             //       return;
               // else
                    //  MessageBox.Show("אין כרגע מכולות ממתינות להעמסה!");
                   // return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //**************************************************

        }

        /// <summary>
        /// טעינת הנתונים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInformation_Load_1(object sender, EventArgs e)
        {
            BindControls();
            dataGridContaunersInList.Focus();
        }






        /// <summary>
        /// בחירת הנתותונים לשימוש הפרמטרים של המכולה והמיצהר
        /// </summary>
        private void SelectRecord(DataGridView dgv)
        {
            string Activety = string.Empty;
            try
            {
                rcn = dgv.CurrentCell.RowIndex;
                manifest = dgv[14, rcn].Value.ToString();
                ContainerID = dgv[0, rcn].Value.ToString();
                DealNumber = dgv[15, rcn].Value.ToString();
                DealNumberSub = dgv[16, rcn].Value.ToString().TrimEnd();


                BL = new ForkliftAppBL(manifest, ContainerID, DealNumber, DealNumberSub, false,string.Empty);

                int recCountIn = dgv.RowCount;
                rcn = rcn += 1;
                if (dgv.Name== "dataGridContaunersInList")
                {
                    lblCoutRecordesIn.Text = "רשומה: " + rcn + " מתוך " + recCountIn + " רשומות";
                }
                else
                {
                    lblCoutRecordesOut.Text = "רשומה: " + rcn + " מתוך " + recCountIn + " רשומות";
                }
            }
            catch (NullReferenceException ne)
            {
                ne = new NullReferenceException();
                //throw;
            }
            catch (Exception e)
            {
                MessageBox.Show("טעות בטעינת נתונים" + e.Message);
                //throw;
            }
        }

        /// <summary>
        /// קריאה למסך הפעולות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.btnDatagridEMOut.BackColor == System.Drawing.Color.Yellow)
            {
                dgv = this.dataGridContaunersOutEmpty;
                rcn = dgv.CurrentCell.RowIndex;
                ContainerID = dgv[0, rcn].Value.ToString();
                CarrierCode = dgv[4, rcn].Value.ToString();
                TruckID = dgv[5, rcn].Value.ToString();
                BL = new ForkliftAppBL(CarrierCode, TruckID);

                BL = new ForkliftAppBL("EMOut");
            }
            OpenFormActivity();
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

        /// <summary>
        /// פתיחת מסך עבודות
        /// </summary>
        private void OpenFormWorks()
        {
            this.Dispose();
            frmWorks frm = new  frmWorks();
            frm.ShowDialog();
            frm.Focus();
        }

        /// <summary>
        /// קריאה לשיגרה של בחירת הפרמטרים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridContaunersInList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //this.Close();
            frmActivity frm = new frmActivity();
           // frm.ShowDialog();
            dgv = this.dataGridContaunersInList;
            BL = new ForkliftAppBL("In");
            SelectRecord(dgv);

            OpenFormActivity();
        }

        /// <summary>
        /// יציאה מהישום
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
           // ForkliftAppBL.ExitAppliction();
            if (MessageBox.Show("האם לצאת מהתוכנית?", "אישור יציאה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Dispose();
                frmLogIn frm = new frmLogIn();
                frm.ShowDialog();
                frm.Focus();
            }
            
        }

        private void dataGridContaunersOutList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv = this.dataGridContaunersOutList;
            BL = new ForkliftAppBL("Out");
            SelectRecord(dgv);
            OpenFormActivity();
        }

        private void dataGridContaunersOutEmpty_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //dgv = this.dataGridContaunersOutList;
            //BL = new ForkliftAppBL("Out");
            //SelectRecord(dgv);
            //OpenFormActivity();
            dgv = this.dataGridContaunersOutEmpty;
            rcn = dgv.CurrentCell.RowIndex;
            ContainerID = dgv[0, rcn].Value.ToString();
            CarrierCode = dgv[4, rcn].Value.ToString();
            TruckID = dgv[5, rcn].Value.ToString();
            BL = new ForkliftAppBL(CarrierCode, TruckID);

            BL = new ForkliftAppBL("EMOut");
 //           SelectRecord(dgv);
            OpenFormActivity();
        }

        /// <summary>
        /// פעולות מקשי פונקציה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInformation_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    LoadData(true);
                    break;
                case Keys.F2:
                    dataGridContaunersInList.Focus();
                    dataGridContaunersInList.Select();
                    dataGridContaunersInList_RowEnter();
                    break;
                case Keys.F3:
                    BL = new ForkliftAppBL("Query");
                    OpenFormActivity();
                    break;
                case Keys.F4:
                    OpenFormWorks();
                    break;
                case Keys.F5:
                    OpenFormInfoMenu();
                    break;
                case Keys.F6:
                    BL = new ForkliftAppBL("NotinListIn");
                    OpenFormActivity();
                    break;
                case Keys.F7:
                    BL = new ForkliftAppBL("NotinLisOut");
                    OpenFormActivity();
                    break;
                case Keys.F8:
                    OpenFormActivity();
                    break;
                case Keys.F9:
                    dataGridContaunersOutList.Focus();
                    dataGridContaunersOutList.Select();
                    dataGridContaunersOutList_RowEnter();
                    break;
                case Keys.F10:
                    ForkliftAppBL.ExitAppliction();
                    break;

                default:
                    break;
            }
        }
 
        /// <summary>
        /// איתחול פעולת פריקה
        /// </summary>
        private void dataGridContaunersInList_RowEnter()
        {
            BL = new ForkliftAppBL("In");
            dgv = this.dataGridContaunersInList;
            SelectRecord(dgv);
        }

        /// <summary>
        /// איחול פעולת העמסה
        /// </summary>
        private void dataGridContaunersOutList_RowEnter()
        {
            BL = new ForkliftAppBL("Out");
            dgv = this.dataGridContaunersOutList;
            SelectRecord(dgv);
        }

        private void dataGridContaunersOutEmpty_RowEnter()
        {
            BL = new ForkliftAppBL("EMOut");
            dgv = this.dataGridContaunersOutEmpty;
            SelectRecord(dgv);
        }

        /// <summary>
        /// מעבר לרשימת פעולות פריקה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndatagridIn_Click(object sender, EventArgs e)
        {
            btndatagridIn.BackColor = DefaultBackcolorBtn;
            btnDatagridOut.BackColor = DefaultBackcolorBtn;
            btnDatagridEMOut.BackColor = DefaultBackcolorBtn;
            dataGridContaunersInList.Focus();
            dataGridContaunersInList.Select();
            dataGridContaunersInList_RowEnter();
            btndatagridIn.BackColor = System.Drawing.Color.Yellow;
        }

        /// <summary>
        /// מעבר לרשימת פעולות העמסה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDatagridOut_Click(object sender, EventArgs e)
        {
            btndatagridIn.BackColor = DefaultBackcolorBtn;
            btnDatagridOut.BackColor = DefaultBackcolorBtn;
            btnDatagridEMOut.BackColor = DefaultBackcolorBtn;
            dataGridContaunersOutList.Focus();
            dataGridContaunersOutList.Select();
            dataGridContaunersOutList_RowEnter();
            btnDatagridOut.BackColor = System.Drawing.Color.Yellow;
        }

        /// מעבר לרשימת פעולות מכולות ריקות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDatagridEMOut_Click(object sender, EventArgs e)
        {
            btndatagridIn.BackColor = DefaultBackcolorBtn;
            btnDatagridOut.BackColor = DefaultBackcolorBtn;
            btnDatagridEMOut.BackColor = DefaultBackcolorBtn;
            dataGridContaunersOutEmpty.Focus();
            dataGridContaunersOutEmpty.Select();
            dataGridContaunersOutEmpty_RowEnter();
            btnDatagridEMOut.BackColor = System.Drawing.Color.Yellow;
        }



        /// <summary>
        /// פתיחת מסך שאילתה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("Query");
            BL = new ForkliftAppBL(string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            OpenFormActivity();
        }

        /// <summary>
        /// פתיחת מסך תפריט מידע
        /// </summary>
        private void OpenFormInfoMenu()
        {
            this.Hide();
            frmInfoMenu frm = new frmInfoMenu();
            frm.ShowDialog();
            frm.Focus();
        }
        
        private void timerfrmInformation_Tick(object sender, EventArgs e)
        {
           // if (sender == timerfrmInformation)
           // {
                //BL = new ForkliftAppBL();
                //DateTime endtime = DateTime.Now;
                //int time = DateDiffForUpdate(starttime, endtime);
                //int timeForExit = BL.PreDateDiffForExit(TimeForExit, endtime);
                //if (time == 1 || time > 1)
                //{
                //    LoadData(true);
                //    starttime = DateTime.Now;
                //}
                //if (timeForExit == 30 || timeForExit > 30)
                //{
                //    timerfrmInformation.Stop();
                //    this.Hide();
                //    frmLogIn frm = new frmLogIn();
                //    frm.ShowDialog();
                //}
            Showmessage(0, e);



           // }
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

        private void dataGridContaunersInList_SelectionChanged(object sender, EventArgs e)
        {
            dataGridContaunersInList.Focus();
            dataGridContaunersInList.Select();
            dataGridContaunersInList_RowEnter();
            //if (dataGridContaunersInList.Focus() == true)
            //{
            //    dataGridContaunersInList_RowEnter();
            //}
            //else
            //{
            //    dataGridContaunersOutList_RowEnter(); 
            //}
            TimeForExit = DateTime.Now;
        }

        private void dataGridContaunersOutList_SelectionChanged(object sender, EventArgs e)
        {
            dataGridContaunersOutList.Focus();
            dataGridContaunersOutList.Select();
            dataGridContaunersOutList_RowEnter();
            //if (dataGridContaunersOutList.Focus() == true)
            //{
            //    dataGridContaunersOutList_RowEnter();
            //}
            //else
            //{
            //    dataGridContaunersInList_RowEnter();
            //}
            TimeForExit = DateTime.Now;
        }


        private void dataGridContaunersOutEmpty_SelectionChanged(object sender, EventArgs e)
        {
            dataGridContaunersOutEmpty.Focus();
            dataGridContaunersOutEmpty.Select();
            dataGridContaunersOutEmpty_RowEnter();
            //if (dataGridContaunersOutList.Focus() == true)
            //{
            //    dataGridContaunersOutList_RowEnter();
            //}
            //else
            //{
            //    dataGridContaunersInList_RowEnter();
            //}
            TimeForExit = DateTime.Now;
        }


        private void btnWorks_Click(object sender, EventArgs e)
        {
            OpenFormWorks();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            OpenFormInfoMenu();
        }

        /// <summary>
        /// רענון מסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(true);
            BindControls();
        }

        private void checkEM_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkEM.Checked)
            //{
            //    lblHaderEMOut.Visible = true;
            //    this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 182);
            //    this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 182);
            //    this.lblHaderOut.Location = new Point(5, 214);
            //    this.lblCoutRecordesOut.Location = new Point(100, 218);
            //    this.dataGridContaunersOutList.Location = new Point(6, 240);
            //    this.dataGridContaunersOutEmpty.Location = new Point(6, 447);
            //    this.lblHaderEMOut.Location = new Point(12, 423);
            //    BL = new ForkliftAppBL(true);
            //}
            //else
            //{
            //    dataGridContaunersOutEmpty.Visible = false;
            //        lblHaderEMOut.Visible = false;
            //        Random Rd = new Random();
            //        this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 280);
            //        this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 280);
            //        this.lblHaderOut.Location = new Point(5, 315);
            //        this.lblCoutRecordesOut.Location = new Point(100, 318);
            //        this.dataGridContaunersOutList.Location = new Point(5, 340);
            //    BL = new ForkliftAppBL(false);
            //}
            //    LoadData(true);
            //    BindControls();
            
        }

        private void dataGridContaunersInList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dataGridContaunersOutList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        public void Showmessage(object sender, EventArgs e)
        {
            this.timerfrmInformation.Stop();
            string connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=new_inter;Password=new_inter";
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlCommandBuilder builder;
            SqlDataAdapter adapter;
            SqlCommand sqlcmd = new SqlCommand();
            string MessageStr = string.Empty;
            string query;
            query = " SELECT Message,  MessageDate FROM dbo.TB_MessageMalgezot WHERE (LEFT(MessageDate, 12) = LEFT(GETDATE(), 12)) AND (NOT (UsersRead LIKE '%"+ frmLogIn.GlobalVariables.LoginNameStr +"%') OR UsersRead IS NULL)";
            connection.ConnectionString = connectionString;
            command.Connection = connection;
            adapter = new SqlDataAdapter(command.CommandText, connection);
            builder = new SqlCommandBuilder(adapter);
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            DataTable dt = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int a = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][1].ToString().IndexOf(frmLogIn.GlobalVariables.LoginNameStr) == -1)
                    {
                        MessageStr = MessageStr + dt.Rows[i][1].ToString().Substring(11,8) + "  " + "הודעה " + a + (char)10;
                        MessageStr = MessageStr + "----------------------------- " + (char)10; 
                        MessageStr = MessageStr + dt.Rows[i][0].ToString() + (char)10 + (char)10;
                        a++;
                    }
                }
                if (MessageStr != "")
                {
                    DialogResult result = MessageBox.Show(MessageStr, "הודעות מההנהלה", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        query = " UPDATE TB_MessageMalgezot SET UsersRead = case when  UsersRead is null then '' else usersread end   +'" + frmLogIn.GlobalVariables.LoginNameStr + "_" + "' WHERE Message = '" + dt.Rows[i][0].ToString() + "'";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    dt.Dispose();
                    connection.Close();
                }
                this.timerfrmInformation.Start();
            }
            
        }

        private void ViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btndatagridIn.BackColor = DefaultBackcolorBtn;
            btnDatagridOut.BackColor = DefaultBackcolorBtn;
            btnDatagridEMOut.BackColor = DefaultBackcolorBtn;
                if (ViewType.SelectedIndex == 1)
                {
                    BL = new ForkliftAppBL(true);
                }
                else
                {
                    BL = new ForkliftAppBL(false);
                }
                LoadData(true);
                BindControls();


                if (ViewType.SelectedIndex == 1)
                {
                    this.btnDatagridEMOut.Visible = true;
                    dataGridContaunersOutEmpty.Visible = true;
                    lblHaderEMOut.Visible = true;
                    this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 182);
                    this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 182);
                    this.lblHaderOut.Location = new Point(5, 214);
                    this.lblCoutRecordesOut.Location = new Point(100, 218);
                    this.dataGridContaunersOutList.Location = new Point(6, 240);
                    this.dataGridContaunersOutEmpty.Location = new Point(6, 447);
                    this.lblHaderEMOut.Location = new Point(12, 423);
                }
                else
                {
                    this.btnDatagridEMOut.Visible = false;
                    dataGridContaunersOutEmpty.Visible = false;
                    lblHaderEMOut.Visible = false;
                    Random Rd = new Random();
                    this.dataGridContaunersInList.Size = new System.Drawing.Size(1242, 280);
                    this.dataGridContaunersOutList.Size = new System.Drawing.Size(1242, 280);
                    this.lblHaderOut.Location = new Point(5, 315);
                    this.lblCoutRecordesOut.Location = new Point(100, 318);
                    this.dataGridContaunersOutList.Location = new Point(5, 340);
                }
            
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            if (btndatagridIn.BackColor == System.Drawing.Color.Yellow)
            {
                dataGridContaunersInList.Focus();
                SendKeys.SendWait("{UP}");
                dataGridContaunersInList.Select();
                dataGridContaunersInList_RowEnter();
            }
            if (btnDatagridOut.BackColor == System.Drawing.Color.Yellow)
            {
                dataGridContaunersOutList.Focus();
                SendKeys.SendWait("{UP}");
                dataGridContaunersOutList.Select();
                dataGridContaunersOutList_RowEnter();
            }
            if (btnDatagridEMOut.BackColor == System.Drawing.Color.Yellow)
            {
                dataGridContaunersOutEmpty.Focus();
                SendKeys.SendWait("{UP}");
                dataGridContaunersOutEmpty.Select();
                dataGridContaunersOutEmpty_RowEnter();
            }
        }

        private void btnDOWN_Click(object sender, EventArgs e)
        {
            if (btndatagridIn.BackColor == System.Drawing.Color.Yellow)
            {
                dataGridContaunersInList.Focus();
                SendKeys.SendWait("{DOWN}");
                dataGridContaunersInList.Select();
                dataGridContaunersInList_RowEnter();
            }
            if (btnDatagridOut.BackColor == System.Drawing.Color.Yellow)
            {
                dataGridContaunersOutList.Focus();
                SendKeys.SendWait("{DOWN}");
                dataGridContaunersOutList.Select();
                dataGridContaunersOutList_RowEnter();
            }
            if (btnDatagridEMOut.BackColor == System.Drawing.Color.Yellow)
            {
                dataGridContaunersOutEmpty.Focus();
                SendKeys.SendWait("{DOWN}");
                dataGridContaunersOutEmpty.Select();
                dataGridContaunersOutEmpty_RowEnter();
            }
        }

        

        //private void ViewType_Click(object sender, EventArgs e)
        //{
        //    ViewType.DropDownStyle = ComboBoxStyle.DropDownList;
        //}
    }

}

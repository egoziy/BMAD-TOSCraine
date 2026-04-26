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
    public partial class frmActivity : Form
    {
        private string activity = string.Empty;
        private ForkliftAppBL BL = null;
        private ForkliftAppDS ds = null;
        private ForkliftAppDS dsw = null;
        private ForkliftAppDS dsEMOut = null;
        private string act;
        private BindingSource bs = new BindingSource();
        private DataTable dt;
        private DataTable dtw;
        private DataTable dtMoreThen1;
        private DataSet dsmore = null;
        private DateTime StartTime;
        private bool chkform;
        private string stFocusdConrol;
        private Control ctl;
        private string _Versia;
        private string _username;
        private string _comment;
        private DataTable dtEL;

        //-----------------------------------------------------//
        // משתנים לעידכון כניסת מכולה                          //
        //-----------------------------------------------------//
        private string _manifest = string.Empty;
        private string _container = string.Empty;
        private string _location = string.Empty;
        private DateTime _forkliftdatein;
        private DateTime _forkliftdateOut;
        private string _forkliftoperatorID = string.Empty;
        private string _entranceforkliftID = string.Empty;
        private string _exitforkliftoperatorID = string.Empty;
        private string _exitforkliftID = string.Empty;
        private string _trucknumber = string.Empty;
        private string _DealNumber = string.Empty;
        private string _SubDealNumber = string.Empty;
        private string _CarrierCode = string.Empty;
        private string _TruckID = string.Empty;
        private string _EmptyContainer = string.Empty;
        
        //-------------------------------------------------------


        private TextBox focusedTextbox = null;
        public frmActivity()
        {
            InitializeComponent();
            if (ForkliftAppBL.m_Terminal == "ILCXQ")
            {
            this.txtTerminal.Text = "אשדוד";
            }
            else
            {
                this.txtTerminal.Text = "חיפה";
            }
            this.txtPermit.Visible = false;
            touchScreen1.OnUserControlButtonClicked += new TouchScreen.ButtonClickedEventHandler(touchScreen1_OnUserControlButtonClicked);  
            FormOpen();
            _username = ForkliftAppBL.GetUserName();
            this.btnComment.Visible = false;
            
        }


       
        protected void touchScreen1_OnUserControlButtonClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (focusedTextbox != null)
            {
                if (b.Text == "אישור {F12}")
                {
                    if (this.txtContNum.Text.Length == 7)
                    {
                        PreBindControls();
                        focusedTextbox = this.txtNewLocation;
                    }
                }
                else
                {
                    if (b.Text == "מחק")
                    {
                        if (focusedTextbox.Text.Length > 1)
                        {
                            focusedTextbox.Text = focusedTextbox.Text.Substring(0, focusedTextbox.Text.Length - 1);
                        }
                        else
                        {
                            focusedTextbox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (MyGlobal.bTouch)
                            focusedTextbox.Text = b.Text;
                        else
                        {
                            MyGlobal.bTouch = false;
                            focusedTextbox.Text += b.Text;
                        }
                    }
                }
            }
        }


        private void FormOpen()
        {
            _Versia = ForkliftAppBL.GetVersia();
            this.Text = this.Text + " - גירסה " + _Versia;
            BL = new ForkliftAppBL();
            act = ForkliftAppBL.GetActivity();
            if (act == "In")
            {
                this.Text = "מסך כניסת מכולה";
                GetDataForInOut(act);
                chkTruck.Visible = false;
                chkTrain.Visible = false;
                btnKaron.Visible = true;
            }
            else if (act == "NotinListIn")
            {
                this.Text = "מסך כניסת מכולה לא ברישמה";
                chkTruck.Visible = false;
                chkTrain.Visible = false;
            }
            else if (act == "Out")
            {
                this.Text = "מסך מסירת מכולה";
                GetDataForInOut(act);
                chkTruck.Visible = false;
                chkTrain.Visible = false;
            }
            else if (act == "NotinLisOut")
            {
                this.Text = "מסך מסירת מכולה לא ברשימה";
                GetDataForInOut(act);
                chkTruck.Visible = true;
                chkTrain.Visible = true;
                chkTruck.Checked = true;
            }
            else if (act == "Workes")
            {
                this.Text = "מסך שאילתה / עיתוק מכולה";
                GetDataForInOut(act);
                chkTruck.Visible = false;
                chkTrain.Visible = false;
            }
            else if (act == "EMOut")
            {
                this.Text = "מסך טעינת מכולה ריקה";
               // GetDataForInOut(act);
                chkTruck.Visible = false;
                chkTrain.Visible = false;
            }
            else
            {
                this.Text = "מסך שאילתה / עיתוק מכולה";
                GetDataForInOut(act);
                chkTruck.Visible = false;
                chkTrain.Visible = false;
            }
        }

        /// <summary>
        /// חישוב זמן לסגירת מסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerfrmactivity_Tick(object sender, EventArgs e)
        {
            if (sender == timerfrmactivity)
            {
                //BL = new ForkliftAppBL();
                //DateTime endtime = DateTime.Now;
                //int timeForExit = BL.PreDateDiffForExit(StartTime, endtime);

                //if (timeForExit == 30 || timeForExit > 30)
                //{
                //    timerfrmactivity.Stop();
                //    this.Hide();
                //    frmLogIn frm = new frmLogIn();
                //    frm.ShowDialog();
                //}
            }
        }

        /// <summary>
        /// שליפת הפרמטרים לאיכלוס אוסף הנתונים למסך
        /// </summary>
        /// <param name="act"></param>
        private void GetDataForInOut(string act)
        {
            if (act == "In" || act == "Out" || act == "EmptyContainers" || act == "EmptyLocation" || act == "Workes")
            {
                _manifest = ForkliftAppBL.GetManifest();
                _container = ForkliftAppBL.GateContainer();
            }
            if (act == "NotinLisOut" || act == "NotinLisOut" || act == "Query")
            {
                _DealNumber = ForkliftAppBL.GetDealNymber();
                _SubDealNumber = ForkliftAppBL.GetSumDealNumber();
            }
        }

        /// <summary>
        /// סגירת המסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            ForkliftAppBL dt = new ForkliftAppBL(dsmore = null);
            ClearControls();
            CloseForm();
        }

        /// <summary>
        /// איסוף הנתונים למסך
        /// </summary>
        private void GetData()
        {
            if (act == "In" || act == "Out" || act == "EmptyContainers" || act == "EmptyLocation" || act == "Workes")
            {
                ds = ForkliftAppDA.GetContainersActivity(_manifest, _container, act);
                dsw = ForkliftAppDA.GetWorkForContainer(_DealNumber, _SubDealNumber);
            }

                //*-*-*

            else if (act == "EMOut")
            {
                _CarrierCode = ForkliftAppBL.GetCarrierCode();
                _TruckID = ForkliftAppBL.GetTruckID();
                _container = this.txtContAlpha.Text + this.txtContNum.Text;
                dsEMOut = ForkliftAppDA.GetContainersEMOutDetails(_CarrierCode, _TruckID);
            }
            else
            {
                PreBindControls();
            }
        }

        /// <summary>
        /// קריאה לתהליך איסוף הנתונים למסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmActivity_Load(object sender, EventArgs e)
        {
            GetData();
            BindControls();
            if (_username == "ohayon" || _username == "OHAYON" || _username == "sa")
            {
                this.btnComment.Visible = true;
            }
        }

        /// <summary>
        /// איכלוס הפקדים עם הנתונים
        /// </summary>
        private void BindControls()
        {
            //if (act == "EMOut")
            //{
            //    this.txtContSize.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            //    this.txtContType.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
            //    this.txtHandelingType.Text = dt.Rows[0].ItemArray.GetValue(3).ToString();
            //    this.txtLine.Text = dt.Rows[0].ItemArray.GetValue(4).ToString();
            //    this.txtTruckNumber.Text = dt.Rows[0].ItemArray.GetValue(5).ToString();
            //    this.txtCostumer.Text = dt.Rows[0].ItemArray.GetValue(12).ToString();
            //}
            //else
            //{

                dt = new DataTable();
                dt = Getdt();
                GetDealNumber();
                dsw = ForkliftAppDA.GetWorkForContainer(_DealNumber, _SubDealNumber);
                dtw = new DataTable();
                dtw = dsw.Tables["WorkForContainer"];
                //dtMoreThen1 = dsmore.Tables[""];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0 || ForkliftAppBL.IsSame) //&& dt.Rows.Count == 1)
                    {
                        try
                        {
                            this.txtContAlpha.DataBindings.Clear();
                            this.txtContAlpha.DataBindings.Add(new Binding("Text", dt, "מכולה", true));
                            this.txtContAlpha.Text = ForkliftAppBL.GetDataForContAlpha(this.txtContAlpha.Text);
                            this.txtContNum.DataBindings.Clear();
                            this.txtContNum.DataBindings.Add(new Binding("Text", dt, "מספר מכולה", true));
                            this.txtContSize.DataBindings.Clear();
                            this.txtContSize.DataBindings.Add(new Binding("Text", dt, "גודל", true));
                            this.txtContType.DataBindings.Clear();
                            this.txtContType.DataBindings.Add(new Binding("Text", dt, "סוג", true));
                            this.txtHandelingType.DataBindings.Clear();
                            this.txtHandelingType.DataBindings.Add(new Binding("Text", dt, "קוד", true));
                            this.txtTchoula.DataBindings.Clear();
                            this.txtTchoula.DataBindings.Add(new Binding("Text", dt, "אפיון", true));
                            this.txtAVDM.DataBindings.Clear();
                            this.txtAVDM.DataBindings.Add(new Binding("Text", dt, "AVDM", true));

                            if (act == "Query")
                            {
                                if (dt.Rows[0][26].ToString().Trim() != "")
                                {
                                    this.txtPermit.Visible = true;
                                    this.txtPermit.Text = "למכולה יש התרה";
                                }
                                else if (dt.Rows[0][27].ToString().Trim() == "1")
                                {
                                    this.txtPermit.Visible = true;
                                    this.txtPermit.Text = "מכולה לשיקוף";
                                }
                                
                                if (ForkliftAppBL.m_Terminal == "ILCXQ")
                                {
                                    this.txtTerminal.Text = "אשדוד";
                                }
                                else
                                {
                                    this.txtTerminal.Text = "חיפה";
                                }
                            }
                            else if (act == "Out")
                            {
                                if (dt.Rows[0][26].ToString().Trim() != "")
                                {
                                    this.txtPermit.Visible = true;
                                    this.txtPermit.Text = "למכולה יש התרה";
                                }
                                else if (dt.Rows[0][27].ToString().Trim() == "1")
                                {
                                    this.txtPermit.Visible = true;
                                    this.txtPermit.Text = "מכולה לשיקוף";
                                }
                            }
                            else if (act == "In")
                            {
                                if (dt.Rows[0][28].ToString().Trim() != "")
                                {
                                    this.txtPermit.Visible = true;
                                    this.txtPermit.Text = "למכולה יש התרה";
                                }
                            }
                            if (this.txtAVDM.Text == "AV")
                            {
                                this.txtAVDM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#58A94F");
                            }
                            else
                            {
                                this.txtAVDM.ForeColor = Color.Red;
                            }
                            if (this.txtTchoula.Text == "FU")
                            {
                                this.txtTchoula.Text = "מלא";
                                this.txtTchoula.ForeColor = Color.Red;
                            }
                            else
                            {
                                this.txtTchoula.Text = "ריק";
                                this.txtTchoula.ForeColor = Color.DarkGreen;
                            }
                            if (act != "EMOut")
                            {
                                this.UNCode1.DataBindings.Clear();
                                this.UNCode1.DataBindings.Add(new Binding("Text", dt, "UNCode1", true));

                                this.ClassificationClassCode.DataBindings.Clear();
                                this.ClassificationClassCode.DataBindings.Add(new Binding("Text", dt, "ClassificationClassCode", true));

                                this.UNCode1.DataBindings.Clear();
                                this.UNCode1.DataBindings.Add(new Binding("Text", dt, "UNCode1", true));

                                this.ClassificationClassCode.DataBindings.Clear();
                                this.ClassificationClassCode.DataBindings.Add(new Binding("Text", dt, "ClassificationClassCode", true));
                                this.txtWh.DataBindings.Clear();
                                this.txtWh.DataBindings.Add(new Binding("Text", dt, "משקל", true));
                            }
                            this.txtCostumer.DataBindings.Clear();
                            if (act == "Query")
                            {
                                if (dt.Rows[0][21].ToString() != string.Empty) // && dt.Rows[0][22].ToString() == string.Empty)
                                {
                                    this.txtInDate.Visible = true;
                                    this.txtInDate.DataBindings.Clear();
                                    this.txtInDate.DataBindings.Add(new Binding("Text", dt, "EntranceDate", true));
                                    this.txtInDate.Text = Convert.ToDateTime(this.txtInDate.Text).ToShortDateString();
                                }
                                if (dt.Rows[0][21].ToString() != string.Empty)
                                {
                                    this.txtWork.DataBindings.Clear();
                                    this.txtWork.DataBindings.Add(new Binding("Text", dt, "עבודה", true));
                                }
                            }
                            else if (act == "In")
                            {
                                if (dt.Rows[0][23].ToString() != string.Empty) // && dt.Rows[0][22].ToString() == string.Empty)
                                {
                                    this.txtInDate.Visible = true;
                                    this.txtInDate.DataBindings.Clear();
                                    this.txtInDate.DataBindings.Add(new Binding("Text", dt, "EntranceDate", true));
                                    this.txtInDate.Text = Convert.ToDateTime(this.txtInDate.Text).ToShortDateString();
                                }
                            }
                            else
                            {
                                this.txtInDate.Visible = false;
                            }
                            //if (this.txtHandelingType.Text == "EX" || this.txtHandelingType.Text == "FR")
                            //{
                            //    this.txtCostumer.DataBindings.Add(new Binding("Text", dt, "RecieveCommision", true));
                            //}
                            //else
                            //{
                            this.txtCostumer.DataBindings.Add(new Binding("Text", dt, "לקוח", true));
                            //}
                            this.txtLine.DataBindings.Clear();
                            this.txtLine.DataBindings.Add(new Binding("Text", dt, "קו", true));
                            //if (this.Text == "מסך כניסת מכולה" || this.Text == "מסך כניסת מכולה לא ברישמה")
                            //{

                            if (act != "EMOut")
                            {
                                this.txtLocationExp.DataBindings.Clear();
                                this.txtLocationExp.DataBindings.Add(new Binding("Text", dt, "איתור צפוי", true));
                                //this.lblLocation.Text = "איתור מומלץ:";
                                //this.lblLocation.Location = new Point(4, 174);
                                //}
                                //else
                                //{
                            }
                            if (act == "EMOut")
                            {
                                if (dt.Rows[0][5].ToString().Length == 8)
                                {
                                    this.txtTruckNumber.Mask = "000-00-000";
                                }
                                else
                                {
                                    this.txtTruckNumber.Mask = "00-000-00";
                                }
                                this.txtTruckNumber.Text = _TruckID;

                                if (dt.TableName != "ContainersQuery")
                                {
                                    this.txtDriverName.DataBindings.Clear();
                                    this.txtDriverName.DataBindings.Add(new Binding("Text", dt, "DriverName", true));
                                }
                                else
                                {
                                    //this.txtDriverName.DataBindings.Clear();
                                    //this.txtDriverName.DataBindings.Add(new Binding("Text", dt, "נהג", true));
                                }
                                this.txtWh.DataBindings.Clear();
                                this.txtWh.DataBindings.Add(new Binding("Text", dt, "משקל", true));

                                this.txtLocationExp.DataBindings.Clear();
                                this.txtLocationExp.DataBindings.Add(new Binding("Text", dt, "איתור צפוי", true));
                                
                            }
                            this.txtLocation.DataBindings.Clear();
                            this.txtLocation.DataBindings.Add(new Binding("Text", dt, "איתור", true));
                            //this.lblLocation.Text = "איתור קיים:";
                            //this.lblLocation.Location = new Point(34, 174);
                            //}
                            if (act != "EMOut")
                            {
                                if (dt.Rows[0][6].ToString().Length == 8)
                                {
                                    this.txtTruckNumber.Mask = "000-00-000";
                                }
                                else
                                {
                                    this.txtTruckNumber.Mask = "00-000-00";
                                }
                            this.txtTruckNumber.DataBindings.Clear();
                            this.txtTruckNumber.DataBindings.Add(new Binding("Text", dt, "משאית", true));
                            
                                this.txtDriverName.DataBindings.Clear();
                                this.txtDriverName.DataBindings.Add(new Binding("Text", dt, "נהג", true));
                                if (this.txtDriverName.Text != "מכולה שמורה") this.txtDriverName.ForeColor = Color.Black;
                            }

                            this.txtComment.DataBindings.Clear();
                            this.txtComment.DataBindings.Add(new Binding("Text", dt, "MarksNumbers", true));
                            if (dt.Rows[0][22].ToString() != string.Empty && dt.Rows[0][22].ToString() == string.Empty)
                            {
                                this.txtComment.DataBindings.Clear();
                                this.txtComment.DataBindings.Add(new Binding("Text", dt, "RegisterDate", true));
                                this.txtComment.Text = "מכולה צפויה לתאריך - " + this.txtComment.Text;
                            }
                            if (dtw.Rows.Count > 0)
                            {
                                this.txtWork.Text = GetWork(dtw);
                            }
                            //if (act == "Query")
                            //{
                            //    this.txtComment.Enabled = true;
                            //    this.txtComment.DataBindings.Clear();
                            //    this.txtComment.DataBindings.Add(new Binding("Text", dt, "נהג", true));
                            //}
                            //else
                            //{
                            //    this.txtComment.Enabled = false;
                            //}
                            if (act != "In")
                            {
                                if (act != "NotinListIn")
                                {
                                    //this.txtWork.Enabled = true;
                                    //this.txtComment.Enabled = true;
                                    //this.txtWork.DataBindings.Clear();
                                    //this.txtWork.DataBindings.Add(new Binding("Text", dt, "עבודה", true));
                                    //this.txtComment.DataBindings.Clear();
                                    ////this.txtComment.DataBindings.Add(new Binding("Text", dt, "Comment", true));
                                    //MessageBox.Show(this.txtWork.Text);
                                }
                            }
                            else
                            {
                                //this.txtWork.Enabled = false;
                                //this.txtComment.Enabled = false;
                            }
                            if (act == "In" || act == "NotinListIn" || act == "Query")
                            {
                                if (this.txtLocation.Text != string.Empty)
                                {
                                    DataTable dtsec = new DataTable();
                                    dtsec = ForkliftAppBL.GetExpectedForLoction(this.txtLocation.Text);

                                    DataTable dtinl = new DataTable();
                                    dtinl = ForkliftAppBL.GetContsInForloction(this.txtLocation.Text);
                                }
                            }

                            //if (this.txtLocation.Text == string.Empty)
                            //    if (act != "Query" && this.txtHandelingType.Text != string.Empty)
                            //    {
                            //        if (this.txtHandelingType.Text == "HH" && this.txtContSize.Text == "20")
                            //        {
                            //            this.txtLocation.Text = "רכבת 20";
                            //        }
                            //        else if (this.txtHandelingType.Text == "HH" && this.txtContSize.Text == "40")
                            //        {
                            //            this.txtLocation.Text = "רכבת 40";
                            //        }
                            //        else if (this.txtHandelingType.Text == "FR")
                            //        {
                            //            this.txtLocation.Text = "חופשי";
                            //        }
                            //        else if (this.txtHandelingType.Text == "EX")
                            //        {
                            //            this.txtLocation.Text = "יצוא";
                            //        }
                            //        else if (this.txtHandelingType.Text == "EM")
                            //        {
                            //            this.txtLocation.Text = string.Empty;
                            //        }
                            //        else
                            //        {
                            //            MessageBox.Show("לא קיים סוג מכולה" + this.txtContType.Text);
                            //        }
                            //    }
                        }
                        catch (ArgumentException ae)
                        {
                            MessageBox.Show(ae.Message);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    //else if (dt.Rows.Count > 1)
                    //{
                    //    MessageBox.Show("יש יותר ממכולה אחת!!!!!!!");
                    //}
                    else if (dt.Rows.Count > 0)
                    {
                        ClearControls();
                        MessageBox.Show("אין מכולה כזו בקונטרם");
                        //this.txtComment.Text = string.Empty;
                        this.txtComment.Focus();
                    }
                    ControlsLooke();
                }
            }
        //}

        private string GetWork(DataTable dtw)
        {
            string st = string.Empty;
            for (int i = 0; i < dtw.Rows.Count; i++)
            {
                st += dtw.Rows[i][0].ToString() + ' ';
            }
            return st;
        }

        /// <summary>
        /// קבלת מזהה עיסקה לבדיקדות למכולה
        /// </summary>
        private void GetDealNumber()
        {
            if (dt.Rows.Count > 0)
            {
                if (act == "In")
                {
                    _DealNumber = dt.Rows[0][17].ToString();
                    _SubDealNumber = dt.Rows[0][17].ToString();
                }
                else if (act == "NotinListIn" || act == "Query")
                {
                    _DealNumber = dt.Rows[0][17].ToString();
                    _SubDealNumber = dt.Rows[0][18].ToString();
                }
                //else if (act == "Query")
                //{
                //    _DealNumber = dt.Rows[0][17].ToString();
                //    _SubDealNumber = dt.Rows[0][18].ToString();
                //}
                else if (act == "Workes")
                {
                    _DealNumber = dt.Rows[0][16].ToString();
                    _SubDealNumber = dt.Rows[0][17].ToString();
                }
            }
        }

        /// <summary>
        /// בדיקת אוסף הנתונים לפני איכלוס הפקדים עם הנתונים 
        /// </summary>
        public void PreBindControls()
        {
            try
            {
                //dt = null;
                if (act == "In" || act == "Out" || act == "EmptyContainers" || act == "EmptyLocation" || act == "Workes" )
                {
                    _container = this.txtContAlpha.Text + this.txtContNum.Text;
                    GetData();
                }
                else
                {
                    if (act == "EMOut" && this.txtContNum.Text.Length == 7)
                    {
                        _container = this.txtContNum.Text;
                        ds = ForkliftAppDA.GetContainersQuery(_container, act, ForkliftAppBL.m_Terminal);
                    }
                    else
                    {
                   // if (act != "EMOut")// && this.txtContNum.Text.Length == 7)
                  //  {
                        _container = this.txtContNum.Text;
                        ds = ForkliftAppDA.GetContainersQuery(_container, act, ForkliftAppBL.m_Terminal);
                    }
                }
                dt = Getdt();
                dsw = ForkliftAppDA.GetWorkForContainer(_DealNumber, _SubDealNumber);

                if (dsmore != null)
                {
                    BindControls();
                }
                else if (dt.Rows.Count > 1)
                {
                    ForkliftAppBL.IsSame = false;
                    string OldManifest = string.Empty;
                    string maifest = string.Empty;
                    //for (int i = 0; i <= dt.Rows.Count; i++)
                    //{
                    //    maifest = dt.Rows[i]["מיצהר"].ToString();
                    //    if (i > 0)
                    //    {
                    //        if (maifest != OldManifest)
                    //        {
                    //            OldManifest = maifest;
                    //        }
                    //        else
                    //        {
                    //            ForkliftAppBL.IsSame = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    if (!ForkliftAppBL.IsSame)
                    {
                        BL = new ForkliftAppBL(dt);
                        this.Hide();
                        frmSelectContainer frm = new frmSelectContainer();
                        frm.ShowDialog();
                    }
                    else
                    {
                        BindControls();    
                    }
                }
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        ClearControls();
                        return;
                    }
                    //MessageBox.Show("מכולה לא נמצאת בקונטרם.");
                    BindControls();
                    this.txtNewLocation.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// בחירת אוסף נתונים למסך
        /// </summary>
        /// <returns></returns>
        private DataTable Getdt()
        {
            if (act == "EMOut" && this.txtContNum.Text.Length == 7)
            {
                _container = this.txtContNum.Text;

                dt = ds.Tables["ContainersQuery"];
                _container = dt.Rows[0][0].ToString();
                _manifest = dt.Rows[0][15].ToString();
                _comment = dt.Rows[0][20].ToString();
                BL = new ForkliftAppBL(_manifest, _container, _comment);

            }

            else if (act != "Query" && act != "NotinListIn" && act != "NotinLisOut" && dt != null && act != "EMOut")
            {
                dt = ds.Tables["ContainersAct"];
                _container = dt.Rows[0][0].ToString();
                _manifest = dt.Rows[0][15].ToString();
                _comment = dt.Rows[0][20].ToString();
                BL = new ForkliftAppBL(_manifest, _container, _comment);
            }
            else
            {
                if (act == "Query") //|| (act == "EMOut" && this.txtContNum.Text.Length != 7))
                {
                    dsmore = ForkliftAppBL.GetDSMore();
                    //DataTable dtmore = new DataTable();
                    //dtmore = dsmore.Tables[0];
                    if (dsmore != null)
                    {
                        dtw = dsmore.Tables["ContainersQuery"];
                        _DealNumber = dtw.Rows[0][17].ToString();
                        _SubDealNumber = dtw.Rows[0][18].ToString();
                    }
                    if (dsmore == null)
                    {
                        dt = ds.Tables["ContainersQuery"];
                        if (dt.Rows.Count > 0)
                        {
                            _container = dt.Rows[0][0].ToString();
                            _manifest = dt.Rows[0][15].ToString();
                            _comment = dt.Rows[0][18].ToString();
                            BL = new ForkliftAppBL(_manifest, _container, _comment);
                        }
                    }
                    else
                    {
                        dt = dsmore.Tables["ContainersQuery"];
                    }
                }





                else if (act == "NotinListIn" && ds != null)
                {
                    dt = ds.Tables["ContainersNotinListIn"];
                }
                else if (act == "NotinLisOut" && ds != null)
                {
                    dsmore = ForkliftAppBL.GetDSMore();
                    if (dsmore != null)
                    {
                        dt = dsmore.Tables["ContainersQuery"];
                    }
                    else
                    {
                        dt = ds.Tables["ContainersNotinListOut"];
                    }
                }
                else if (act == "EMOut" && this.txtContNum.Text.Length != 7)
                {
                    dt = dsEMOut.Tables["ContainersActEMOut"];
                }
                else
                {
                    dt = null;
                }
            }
            return dt;
        }

        /// <summary>
        /// מאפשר פקדים
        /// </summary>
        private void ControlsLooke()
        {
            //if (act == "In" || act == "Out")
            //{
            //    foreach (Control c in Controls)
            //    {
            //        if (c is TextBox)
            //        {
            //            ((TextBox)c).ReadOnly = true;
            //        }
            //        else if (c is MaskedTextBox)
            //        {
            //            ((MaskedTextBox)c).ReadOnly = true;
            //        }
            //        if (c is TextBox && act == "In" && c.Name == "txtNewLocation" )   
            //        {
            //            ((TextBox)c).ReadOnly = false;
            //        }

                  
            //    }
            //}
            //else
            //{
            //    foreach (Control c in Controls)
            //    {

            //        if (c is TextBox)
            //        {
            //            if (c.Name == "txtContNum" || c.Name == "txtNewLocation" || c.Name == "txtTruckNumber")
            //            {
            //                ((TextBox)c).ReadOnly = false;
            //            }
            //            else
            //            {
            //                ((TextBox)c).ReadOnly = true;
            //            }
            //        }
            //        else if (c is MaskedTextBox)
            //        {
            //            ((MaskedTextBox)c).ReadOnly = true;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// תיפקוד מקשים
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmActivity_KeyDown(object sender, KeyEventArgs e)
        {
            string se = sender.ToString();
            switch (e.KeyData)
            {
                case Keys.F2:
                    GetDamage();
                    break;
                case Keys.F3:
                    GetQuery();
                    break;
                case Keys.F4:
                    OpenFormWorks();
                    break;
                case Keys.F5:
                    OpenFormInfoMenu();
                    break;
                case Keys.F6:
                    OpenfrmSameDealNumber();
                    break;

                case Keys.F7:
                    btnReserve_Click(0,e);
                    break;

                case Keys.F8:
                    SaveData();
                    break;
                case Keys.F9:
                    ClearControls();
                    CloseForm();
                    break;
                case Keys.F10:
                    ClearControls();
                    this.Dispose();
                    frmInformation frm = new frmInformation();
                    frm.ShowDialog();
                    break;
                case Keys.F12:
                    if (this.txtContNum.Text.Length == 7)
                    {
                        PreBindControls();
                        focusedTextbox = this.txtNewLocation;
                    }
                    break;
            }
        }

        /// <summary>
        /// בדיקת והצגת נזק למכולה אם קיים.
        /// </summary>
        private void GetDamage()
        {
            if (act == "NotinLisOut" || act == "NotinLisOut" || act == "Query")
            {
                _manifest = dt.Rows[0][15].ToString();
                _container = dt.Rows[0][0].ToString();
            }
            DataTable dtd = new DataTable();
            dtd = ForkliftAppBL.GetDamage(_manifest,_container);
            if (dtd.Rows.Count > 0)
            {
                string st=string.Empty;
                for (int i = 0; i < dtd.Rows.Count; i++)
                {
                    if (st == string.Empty)
                    {
                        st += dtd.Rows[0][0].ToString() + "\n" + dtd.Rows[0][1].ToString() + "\n" + dtd.Rows[0][4].ToString();
                    }
                    else
                    {
                        st += "\n" + dtd.Rows[0][0].ToString() + "\n" + dtd.Rows[0][1].ToString() + "\n" + dtd.Rows[0][4].ToString();
                    }
                }
                MessageBox.Show(st.ToString());
            }
            else
            {
                MessageBox.Show("אין נזקים", "תיאור הנזק");
            }
        }

        /// <summary>
        /// מעבר לשאילתה ממסך פריקה או מסך העמסה
        /// </summary>
        private void GetQuery()
        {
            dsmore = null;
            BL = new ForkliftAppBL("Query");
            BL = new ForkliftAppBL(dsmore);
            _DealNumber = string.Empty;
            act = ForkliftAppBL.GetActivity();
            if (act == "Query")
            {
                //MessageBox.Show("זוהי שאילה");
                //this.Text = "מסך שאילתה / עיתוק מכולה";
                FormOpen();
                ClearControls();
                ControlsLooke();
                this.txtContNum.Focus();
            }
        }

        /// <summary>
        /// בחירת פקד לפוקוס
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmActivity_Activated(object sender, EventArgs e)
        {
            if (ForkliftAppBL.m_SpecialLocation != string.Empty)
            {
                this.txtNewLocation.Text = ForkliftAppBL.m_SpecialLocation;
            }
            //this.Focus();
            if (act == "Query" || act == "NotinListIn" || act == "NotinLisOut" || act == "EMOut")
            {
                if (this.txtContNum.Text == string.Empty)
                {
                    this.txtContNum.Focus();
                }
                else
                {

                    //PreBindControls();
                }
            }
            else
            {
                  this.txtNewLocation.Focus();
            }
        }

        /// <summary>
        /// קריאה לעידכון רשומת מכולה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            chkform = false;
            SaveData();
        }

        private void SaveData()
        {

            //if (this.txtNewLocation.Text.Length >= 3)
            //{
               // string test = this.txtNewLocation.Text.Substring(0, 3);
               // bool allDigits = this.txtNewLocation.Text.Substring(0, 3).All(char.IsDigit);
                //if (this.txtNewLocation.Text.Substring(0, 3).All(char.IsDigit))
                //{
                //    if (Convert.ToInt32(this.txtNewLocation.Text.Substring(0, 3)) >= 100 & Convert.ToInt32(this.txtNewLocation.Text.Substring(0, 3)) <= 185)
                //    {
            dtEL = ForkliftAppBL.GetExistingLocation(this.txtNewLocation.Text, ForkliftAppBL.m_Terminal);
                        if (dtEL.Rows[0][0].ToString() == "0")
                        {
                            MessageBox.Show(" איתור לא תקין ");
                            this.txtNewLocation.Text = "";
                            this.txtNewLocation.Focus();
                            return;
                        }
            //        }
            //    }
            //}
            chkform = CheckForm();
            if (act == "In")
            {
                //chkform = CheckForm();
                if (chkform == true)
                {
                    this.txtNewLocation.Focus();
                    ContainersInUpDate();
                    CloseForm();
                }
                else
                {
                    return;
                }
            }
            else if (act == "NotinListIn")
            {
                //chkform = CheckForm();
                if (chkform == true)
                {
                    ContainersNotinListInUpDate();
                    CloseForm();
                }
                else
                {
                    return;
                }
            }
            else if (act == "Out" || act == "EMOut")
            {
                if (this.txtTruckNumber.Text == string.Empty)
                {
                    this.txtTruckNumber.Text = "0";
                    return;
                }
                else
                {
                    ContainersOutUpDate();
                    CloseForm();
                }
            }
            else if (act == "NotinLisOut")
            {
                if (this.txtTruckNumber.Text == string.Empty)
                {
                    this.txtTruckNumber.Text = "0";
                    return;
                }
                else
                {
                    ContainersNotinListOutUpDate();
                    if (this.chkTrain.Checked)
                    {
                        ContainersLoctionUpDate();
                    }
                    if (this.chkTrain.Checked)
                    {
                        ClearControls();
                        this.txtContNum.Focus();
                    }
                    else
                    {
                        CloseForm();
                    }
                }
            }
            else if (act == "Workes")
            {
                ContainersLoctionUpDate();
                ClearControls();
                txtContNum.FindForm();
            }
            else
            {
                if (this.chkTruck.Enabled == false && this.chkTrain.Enabled == false)
                {
                    //chkform = CheckForm();
                    if (chkform == true)
                    {
                        if (this.txtNewLocation.Text != string.Empty && this.txtLocation.Text == String.Empty)
                        {
                            _manifest = dt.Rows[0][15].ToString(); ;
                            _container = this.txtContAlpha.Text + this.txtContNum.Text;
                            _DealNumber = string.Empty;
                            _SubDealNumber = string.Empty;
                            BL = new ForkliftAppBL(_manifest, _container, _DealNumber, _SubDealNumber, false, string.Empty);
                            ContainersInUpDate();
                        }
                        else
                        {
                            ContainersLoctionUpDate();
                        }
                        PreBindControls();
                    }
                    else
                    {
                       this.txtNewLocation.Focus();  
                        return;
                        //ContainersLoctionUpDate();
                        //MessageBox.Show("אין איתור חדש");
                    }
                }
                else
                {
                    if (act == "Query")
                    {
                        if (chkform == true)
                        {
                            if (this.chkTruck.Checked)
                            {
                                //chkform = CheckForm();
                                if (chkform == true)
                                {
                                    ContainersNotinListOutUpDate();
                                }
                            }
                            else if (this.chkTrain.Enabled)
                            {
                                //chkform = CheckForm();
                                if (chkform == true)
                                {
                                    ContainersLoctionUpDate();
                                }
                            }
                            PreBindControls();
                            ClearControls();
                            

                            this.txtContNum.Focus();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (this.chkTruck.Checked)
                        {
                            ContainersNotinListOutUpDate();
                        }
                        else if (this.chkTrain.Enabled)
                        {
                            ContainersLoctionUpDate();
                        }
                        PreBindControls();
                        ClearControls();
                        this.txtContNum.Focus();
                        //GetData();
                    }
                }
            }
            
        }

        /// <summary>
        /// בדיקת פקדים 
        /// </summary>
        /// <returns></returns>
        private bool CheckForm()
        {
            bool chf = true;
            if (act == "In" || act == "NotinListIn" || act == "Query")
            {
                if (this.txtNewLocation.Text == string.Empty)
                {
                    this.txtNewLocation.BackColor = Color.Orange;
                    MessageBox.Show(" !!! " + "חובה להקיש איתור חדש", "נתון חסר", MessageBoxButtons.OK, MessageBoxIcon.None);
                    chf = false;
                }
                else
                {
                    this.txtNewLocation.BackColor = Color.LightSteelBlue;
                }
            }
            else if (act == "Out" || act == "NotinLisOut")
            {
                if (this.txtTruckNumber.Text == string.Empty)
                {
                    this.txtTruckNumber.Text = "0";
                }
            }
            return chf;
        }

        /// <summary>
        /// עידכון עיתוק
        /// </summary>
        private void ContainersLoctionUpDate()
        {
            DataTable table = new DataTable();
            try
            {
                if (act == "Workes" || act == "EmptyContainers")
                {
                    table = ds.Tables["ContainersAct"];
                }
                else
                {
                    if (this.chkTruck.Checked || this.chkTrain.Checked)
                    {
                        table = ds.Tables["ContainersNotInListOut"];
                    }
                    else
                    {
                        table = ds.Tables["ContainersQuery"];
                    }
                }
                if (table == null || table.Rows.Count == 0)
                {
                     _container = this.txtContNum.Text;
                     ds = ForkliftAppDA.GetContainersQuery(_container, act, ForkliftAppBL.m_Terminal);
                    table = Getdt();
                }
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    DataColumn column = table.Columns["Manifest"];
                    if (act == "Workes")
                    {
                        _manifest = (row[13]).ToString();
                    }
                    else if (act == "EmptyContainers")
                    {
                        _manifest = (row[14]).ToString();
                    }
                    else
                    {
                        _manifest = (row[15]).ToString();
                    }
                    _container = this.txtContAlpha.Text + this.txtContNum.Text;
                    if (this.chkTrain.Checked)
                    {
                        _location = "קרון";
                    }
                    else
                    {
                        _location = this.txtNewLocation.Text;
                    }
                    
                    ForkliftAppDA.UpdateLoction(_container, _manifest, _location);
                }
            }
            catch (IndexOutOfRangeException iore)
            {
                //MessageBox.Show(iore.Message);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// עידכון פריקת מכולה
        /// </summary>
        private void ContainersInUpDate()
        {
            BL = new ForkliftAppBL();
            if (this.txtNewLocation.Text == null || this.txtNewLocation.Text == string.Empty)
            {
                _location = this.txtLocation.Text;
            }
            else
            {
                _location = this.txtNewLocation.Text;
            }

            _forkliftdatein = DateTime.Now;
            _forkliftoperatorID = string.Empty;
            _entranceforkliftID = BL.GetForkliftID();
            _forkliftoperatorID = BL.GetForkliftOperatorID();
            ForkliftAppDA.UpdateContainersIn(_container, _manifest, _location, _forkliftdatein, _forkliftoperatorID, _entranceforkliftID, act);
            ForkliftAppBL.m_SpecialLocation = string.Empty;
        }

        /// <summary>
        /// עידכון פריקת מכולה לא מרשימה
        /// </summary>
        private void ContainersNotinListInUpDate()
        {
            BL = new ForkliftAppBL();
            _location = this.txtNewLocation.Text;
            _forkliftdatein = DateTime.Now;
            _forkliftoperatorID = string.Empty;
            _entranceforkliftID = BL.GetForkliftID();
            _forkliftoperatorID = BL.GetForkliftOperatorID();
            _trucknumber = this.txtTruckNumber.Text;
            ForkliftAppDA.UpdateContainersInNotinList(_container, _manifest, _location, _forkliftdatein, _forkliftoperatorID, _entranceforkliftID, _trucknumber, act);
        }

        /// <summary>
        /// עידכון העמסת מכולה
        /// </summary>
        private void ContainersOutUpDate()
        {
            BL = new ForkliftAppBL();
            _exitforkliftoperatorID = string.Empty;
            _exitforkliftID = BL.GetForkliftID();
            _exitforkliftoperatorID = BL.GetForkliftOperatorID();
            _forkliftdateOut = DateTime.Now;
            _trucknumber = this.txtTruckNumber.Text;
            if (act != "EMOut")
            {
                ForkliftAppDA.UpdateContainersOut(_container, _manifest, _exitforkliftoperatorID, _exitforkliftID, _forkliftdateOut); //_exitforkliftID,
            }
            else
            {
                _EmptyContainer = this.txtContAlpha.Text + this.txtContNum.Text;
                ForkliftAppDA.UpdateContainersEMOut(_EmptyContainer, _TruckID, _CarrierCode, _exitforkliftoperatorID, _exitforkliftID, _forkliftdateOut);
            }
        }

        /// <summary>
        /// עידכון העמסת מכולה לא מרשימה
        /// </summary>
        private void ContainersNotinListOutUpDate()
        {
            if (this.chkTruck.Checked)
            {
                //if (this.txtTruckNumber.Text.Length != 9 || this.txtTruckNumber.Text == "00-000-00")
                //{
                //    this.txtTruckNumber.ReadOnly = false;
                //    this.txtTruckNumber.BackColor = Color.Orange;
                //    MessageBox.Show(" !!! " + "חובה להקיש מספר משאית", "נתון חסר", MessageBoxButtons.OK, MessageBoxIcon.None);
                //    this.txtTruckNumber.Focus();
                //    return;
                //}
                _manifest = dt.Rows[0][15].ToString();
                _container = this.txtContAlpha.Text + this.txtContNum.Text;
            }
            BL = new ForkliftAppBL();
            _exitforkliftoperatorID = string.Empty;
            _exitforkliftID = BL.GetForkliftID();
            _exitforkliftoperatorID = BL.GetForkliftOperatorID();
            _forkliftdateOut = DateTime.Now;
            _trucknumber = this.txtTruckNumber.Text;
            _manifest = dt.Rows[0][15].ToString();
            _container = this.txtContAlpha.Text + this.txtContNum.Text;
            if (this.chkTruck.Checked==false)
            {
                CheckForm();
            }
            ForkliftAppDA.UpdateContainersNotinlistOutUpDate(_container, _manifest, _exitforkliftoperatorID, _exitforkliftID, _forkliftdateOut, _trucknumber);
            if (this.chkTrain.Checked)
            {
                ContainersLoctionUpDate();
            }
        }

        /// <summary>
        /// איפוס שדות
        /// </summary>
        private void ClearControls()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).Clear();
                }
            }
        }

        /// <summary>
        /// פעולות סגירת טופס
        /// </summary>
        private void CloseForm()
        {
            this.Dispose();
            frmInformation frm = new frmInformation();
            frm.ShowDialog();
        }

        /// <summary>
        /// שאילתה למכולה - לאחר הקשת מספר מכולה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        
        private void txtContNum_Leave(object sender, EventArgs e)
        {
       //   this.txtContNum.Focus();
          focusedTextbox = (TextBox)sender;
      
            //this.txtNewLocation.Focus();

        }
        private void btnWorks_Click(object sender, EventArgs e)
        {
            OpenFormWorks();
        }

        private void OpenFormWorks()
        {
            this.Dispose();
            frmWorks frm = new frmWorks();
            frm.ShowDialog();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetQuery();
        }

        private void cboContainers_SelectedValueChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            OpenFormInfoMenu();
        }

        /// <summary>
        /// פתיחת מסך תפריט מידע
        /// </summary>
        private void OpenFormInfoMenu()
        {
            this.Dispose();
            frmInfoMenu frm = new frmInfoMenu();
            frm.ShowDialog();
            frm.Focus();
        }

       #region Focusd control

        private void txtContAlpha_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtContAlpha;
        }

        private void txtContNum_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtContNum;
            this.txtPermit.Visible = false;
            //ctl.Focus();

           
        
        }

        private void txtNewLocation_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtNewLocation;

            focusedTextbox = this.txtNewLocation;
        }

        private void txtContSize_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtContSize;
        }

        private void txtContType_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtContType;
        }

        private void txtHandelingType_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtHandelingType;
        }

        private void txtTchoula_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtTchoula;
        }

        private void txtWh_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtWh;
        }

        private void txtLine_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtLine;
        }

        private void txtCostumer_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtCostumer;
        }

        private void txtLocation_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtLocation;
        }

        private void txtDriverName_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtDriverName;

            if (ctl.Text != "מכולה שמורה") ctl.ForeColor = Color.Black; 
        }

        private void txtTruckNumber_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtTruckNumber;
        }

        private void txtComment_Enter(object sender, EventArgs e)
        {
            ctl = null;
            ctl = this.txtComment;
        }

        #endregion

        #region Keybord on the screen

       

        private void btnClrar_Click(object sender, EventArgs e)
        {
            ctl.Text = string.Empty;
            ctl.Focus();
        }

        #endregion

        private void btnDamage_Click(object sender, EventArgs e)
        {
            GetDamage();
        }

        /// <summary>
        /// קריאה להצגת מסך מכולות בעיסקה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeal_Click(object sender, EventArgs e)
        {
            OpenfrmSameDealNumber();
        }

        /// <summary>
        /// הצגת מסך מכולות בעיסקה
        /// </summary>
        private void OpenfrmSameDealNumber()
        {
            try
            {
                if (_DealNumber == string.Empty)
                {
                    _DealNumber = dt.Rows[0][17].ToString();
                }
                DataTable dtdeal = new DataTable();
                dtdeal = ForkliftAppBL.GetConsForDealNumber(_DealNumber);
                if (dtdeal.Rows.Count > 1)
                {
                    ForkliftAppBL fbl = new ForkliftAppBL(string.Empty, string.Empty, _DealNumber, string.Empty, false, string.Empty);
                    frmSameDealNumber frm = new frmSameDealNumber();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("אין אחים", "אחים");
                }
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show(nre.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnComment_Click(object sender, EventArgs e)
        {
            if (_manifest == string.Empty)
            {
                _manifest = dt.Rows[0][15].ToString();
                _container = dt.Rows[0][0].ToString();
            }
            frmComment frm = new frmComment();
            frm.ShowDialog();
        }

        private void chkTruck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTruck.Checked == true)
            {
                this.chkTrain.Checked = false;
            }
            
            ToggleChck();
        }

        private void chkTrain_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTrain.Checked == true)
            {
                this.chkTruck.Checked = false;
            }
            ToggleChck();
        }

        private void ToggleChck()
        {
            if (this.chkTruck.Checked == true)
            {
                this.chkTruck.BackColor = Color.Red;
                //this.chkTrain.Checked = false;
                this.chkTrain.BackColor = Color.Green;
            }
            else if (this.chkTrain.Checked == true)
            {
                this.chkTruck.BackColor = Color.Green;
                this.chkTrain.BackColor = Color.Red;
            }
            else if (this.chkTruck.Checked == false && this.chkTrain.Checked == false)
            {
                this.chkTruck.BackColor = Color.Green;
                this.chkTrain.BackColor = Color.Green;
            }
        }

        private void txtWh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (this.txtContNum.Text.Length == 7)
            {
                if (act == "EMOut")
                {
                    DataTable dtCheckEMOut = new DataTable();
                    dtCheckEMOut = ForkliftAppDA.GetCheckEMOut(this.txtContNum.Text, this.txtLine.Text, this.txtContType.Text, this.txtContSize.Text);


                    // (string p_ContainerNum, string p_CarrierCode, string p_ContainerTypeCode, string p_ContainerLength )

                    if (dtCheckEMOut.Rows[0].ItemArray.GetValue(0).ToString() == "0")
                    {
                        MessageBox.Show("! מכולה לא תואמת לנתונים");
                        this.txtContNum.Text = string.Empty;
                        this.txtContNum.Focus(); 
                        return;
                    }
                }

                PreBindControls();
                focusedTextbox = this.txtNewLocation;
                DataTable dtNoPaper = new DataTable();
                dtNoPaper = ForkliftAppDA.GetGatePassNoPaper(txtContNum.Text);
                if (dtNoPaper.Rows.Count > 0)
                {
                        MessageBox.Show(" מכולה הוזמנה בגטפס ללא ניירת " + Environment.NewLine + Environment.NewLine + "בתאריך " + dtNoPaper.Rows[0][0].ToString());
                }

                DataTable dtCheckLock = new DataTable();
                dtCheckLock = ForkliftAppDA.GetLockLocation(_container, _manifest);
                if (dtCheckLock.Rows[0].ItemArray.GetValue(0).ToString() == "True")
                {
                    txtLocation.BackColor = System.Drawing.Color.OrangeRed;
                    btnLockLocation.Visible = false;
                    btnUnLockLocation.Visible = true;
                }
                else
                {
                    txtLocation.BackColor = System.Drawing.Color.White;
                    btnLockLocation.Visible = true;
                    btnUnLockLocation.Visible = false;
                }
            }
        }

        
        private void btnReserve_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDataReserve = new DataTable();
                dtDataReserve = ForkliftAppBL.GetConsReserve();
                if (dtDataReserve.Rows.Count > 1)
                {
                    ForkliftAppBL fbl = new ForkliftAppBL(string.Empty, string.Empty, _DealNumber, string.Empty, false, string.Empty);
                    frmSameDealNumber frm = new frmSameDealNumber();
                    frm.Text = "מכולות שמורות";
                    frm.BackColor = Color.Red;
                    frm.ShowDialog();
                }
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show(nre.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void txtContAlpha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNewLocation_Leave(object sender, EventArgs e)
        {
            //   this.txtContNum.Focus();
            focusedTextbox = (TextBox)sender;

            //this.txtNewLocation.Focus();
        }

        private void touchScreen1_Load(object sender, EventArgs e)
        {

        }

        private void btnKaron_Click(object sender, EventArgs e)
        {
            txtNewLocation.Text = "קרון";
            _location = "קרון";
        }

        private void btnLockLocation_Click(object sender, EventArgs e)
        {
            ForkliftAppDA.UpdateContainerLockLocation(_container, _manifest);
            btnGo_Click(0, e);
        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblline_Click(object sender, EventArgs e)
        {

        }

        private void txtCostumer_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCostumer_Click(object sender, EventArgs e)
        {

        }

        private void SpecialLocation_Click(object sender, EventArgs e)
        {
            OpenFormSpecialLocation();

        }

        private void OpenFormSpecialLocation()
        {
            //this.Dispose();
            frmSpecialLocation frm = new frmSpecialLocation();
            frm.ShowDialog();
            frm.Focus();
        }

        private void btnUnLockLocation_Click(object sender, EventArgs e)
        {
            ForkliftAppDA.UpdateContainerLockLocation(_container, _manifest);
            btnGo_Click(0, e);
        }
        

       

       

      


    }
}
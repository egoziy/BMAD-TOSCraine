using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ForkliftApp.DataAccess;
using ForkliftApp.BusinessLogic;
using NumericKeyPad;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net.NetworkInformation;



namespace ForkliftApp
{

    public partial class frmLogIn : Form
    {
        private ForkliftAppBL BL = null;
        
        public class GlobalVariables
        {
            public static string LoginNameStr = string.Empty;
            public static string StrUserRead = string.Empty;

        } 

        //private const string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        private string un;
        private string pw;
        private bool validuser;
        private string ForkLiftNum;
        private string cwd = Directory.GetCurrentDirectory();
       // public string LoginNameStr;
        //@"c:\temp\ForkLiftNumber.xml";

        private MaskedTextBox focusedTextbox = null;
        private Control ctl;

        public frmLogIn()
        {

          InitializeComponent();

          //  touchScreen1.OnUserControlButtonClicked += new TouchScreen.ButtonClickedEventHandler(touchScreen1_OnUserControlButtonClicked);  
 
     //       InitializeComponent();
            //this.txtUserName.Text = "sa";
            //this.txtPassWord.Text = "3334606";
            //this.txtForkliftNumber.Text = "25";
            frmLogInLoad();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            validuser = IsValidUser();
            if (validuser)
            {
                GlobalVariables.LoginNameStr = this.txtUserName.Text.Trim().ToString();
                GoOn();
            }
            else
            {
                MessageBox.Show("לא קיים משתמש כזה \nנסה שנית בבקשה.");
            }
        }

        private void txtUserName_Click(object senser, EventArgs e)
        {
            this.txtUserName.DroppedDown = true;
        }
        


        private void GoOn()
        {
            int flnu;

            un = this.txtUserName.Text;
            pw = this.txtPassWord.Text;
            if (cmbTerminal.Text == "אשדוד")
            {
                ForkliftAppBL.m_Terminal = "ILCXQ";
                ForkliftAppDA.m_Terminal = "ILCXQ";
            }
            else
            {
                ForkliftAppBL.m_Terminal = "ILGBH";
                ForkliftAppDA.m_Terminal = "ILGBH";
            }
            ForkLiftNum = this.txtForkliftNumber.Text;
            if (this.txtUserName.Text == "sa")
            {
                flnu = 0;
            }
            else
            {
                flnu = int.Parse(this.txtForkliftNumber.Text);
            }

            //ForkliftAppDA.IsValidUser(un, pw,flnu);

            this.Hide();
            try
            {
                frmInformation frm = new frmInformation();
                frm.ShowDialog();
                frm.Focus();
            }
            //catch (NullReferenceException nr)
            //{
            //    //throw new NullReferenceException("NullReferenceException");
            //    return;
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //private void frmLogIn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.F1)
        //    {
        //        validuser = IsValidUser();
        //        if (validuser)
        //        {
        //            GoOn();
        //        }
        //        else
        //        {
        //            MessageBox.Show("לא קיים משתמש כזה \nנסה שנית בבקשה.");
        //        }
        //    }
        //    else if (e.KeyData == Keys.F10)
        //    {
        //        ExitApp();
        //    }
        //}

        private void ExitApp()
        {
            Application.Exit();
        }

        private bool IsValidUser()
        {
            un = this.txtUserName.Text;
            pw = this.txtPassWord.Text;
            ForkLiftNum = this.txtForkliftNumber.Text;
            bool validuser;
            validuser = ForkliftAppDA.IsValidUser(un, pw, ForkLiftNum); //"sa", "3334606",
            return validuser;
        }

        private void txtPassWord_Leave(object sender, EventArgs e)
        {
            btnGo.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void txtForkliftNumber_DoubleClick(object sender, EventArgs e)
        {
            frmChangeForkliftNumber frm = new frmChangeForkliftNumber();
            frm.ShowDialog();
        }

        public void frmLogInLoad()
        {

            XmlDocument doc = new XmlDocument();
            ForkLiftNum = cwd + ("\\" + "ForkLiftNumber.xml");
            doc.Load(ForkLiftNum);
            txtForkliftNumber.Text = doc.DocumentElement.GetElementsByTagName("ForkLiftID")[0].InnerText;
            this.Text = this.Text + " - גירסה " + doc.DocumentElement.GetElementsByTagName("ForkliftAppVertsia")[0].InnerText;
            ForkliftAppBL folapp = new ForkliftAppBL(int.Parse(this.txtForkliftNumber.Text), doc.DocumentElement.GetElementsByTagName("ForkliftAppVertsia")[0].InnerText);
            txtForkliftNumber.ReadOnly = true;


            // TODO: This line of code loads data into the 'terminalDataDataSet.V_UserMalgezot' table. You can move, or remove it, as needed.
            SqlConnection conn = new SqlConnection(@"Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=sa;Password=z3334606*");
            conn.Open();
            SqlCommand sc = new SqlCommand("select  LoginName from V_UserMalgezot order by LoginName", conn);
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LoginName", typeof(string));
            dt.Load(reader);
            this.txtUserName.ValueMember = "LoginName";
            this.txtUserName.DisplayMember = "LoginName";
            this.txtUserName.DataSource = dt;
            conn.Close();
            reader.Dispose();
          focusedTextbox = this.txtPassWord;

          string StrTermial = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\Terminal.txt");
          if (StrTermial == "1")
          {
              this.cmbTerminal.SelectedItem = "אשדוד";
              this.cmbTerminal.Enabled = false;
          }
          else
          {
              this.cmbTerminal.SelectedItem = "חיפה";
              this.cmbTerminal.Enabled = false;
          }
        }

       




        protected void touchScreen1_OnUserControlButtonClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (focusedTextbox != null)
            {

                if (b.Text == "אישור {F12}")
                {
                    if (this.txtPassWord.Text.Length == 7)
                    {
                       // PreBindControls();
                        focusedTextbox = this.txtPassWord;
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






      

        private void txtPassWord_enter(object sender, MaskInputRejectedEventArgs e)
        {
           //ctl = null;
         //  ctl = this.txtPassWord;
           
        }

        private void txtUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string ID = txtUserName.SelectedValue.ToString();
        }

        private void touchScreen1_Load(object sender, EventArgs e)
        {

        }

        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.txtPassWord.Text+= ctl.Text.ToString();
        
        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.txtPassWord.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            this.txtPassWord.Text = this.txtPassWord.Text.Substring(0, txtPassWord.Text.Length - 1);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        

      

        }

        

      

    
                   


        
    }


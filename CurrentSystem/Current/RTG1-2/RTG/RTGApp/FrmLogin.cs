using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;

using System.Data.Sql;


namespace RTGApp
{
    public partial class FrmLogin : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand(); 
        ConTerminalData Con1 = new ConTerminalData();
        public static string StrCHE = "";
        public static string StrBlockName = "";
        public static string StrOperatorID = "";
 
        public FrmLogin()
        {
            InitializeComponent();
            SetBackColorField();
            DataTable dt = Con1.ReturnDT("SELECT '' AS LoginName FROM TB_Parameters UNION ALL SELECT  dbo.HR_Emp.LoginName " +
                                         " FROM dbo.SC_Users INNER JOIN dbo.HR_Emp ON dbo.SC_Users.EmpID = dbo.HR_Emp.EmpID " +
                                         " WHERE dbo.SC_Users.UserGroupCode = 22 ");
            LoginName.ValueMember = "LoginName";
            LoginName.DisplayMember = "LoginName";
            LoginName.DataSource = dt;
           
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            UserConnection Con = new UserConnection();
            
            Cursor.Current = Cursors.WaitCursor;
            SetBackColorField();
          
            if (BackColorField() == 1) return;

                                   
            DataTable dt = Con1.ReturnDT("SELECT COUNT(*) AS Counter " +
                                         " FROM     dbo.SC_Users INNER JOIN " +
                                         " dbo.SC_AppGroup ON dbo.SC_Users.UserGroupCode = dbo.SC_AppGroup.UserGroupCode INNER JOIN " +
                                         " dbo.HR_Emp ON dbo.SC_Users.EmpID = dbo.HR_Emp.EmpID " +
                                         " WHERE (dbo.SC_Users.UserGroupCode = 22) " +
                                         " AND (dbo.HR_Emp.UserPinCode = " + Password.Text + ")");

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    MessageBox.Show("אינך מורשה להכנס למערכת \n      הקש ססמא שנית");
                  //  Password.Text = string.Empty;
                }
                else
                {
                    StrCHE = CHE.Text;
                    StrBlockName = LiftBlockName.Text;
                    dt = Con1.ReturnDT("SELECT EmpID  FROM dbo.HR_Emp  WHERE LoginName = '" + LoginName.Text + "'");
                    StrOperatorID = dt.Rows[0][0].ToString();

                    dt = Con1.ReturnDT("INSERT dbo.RG_Log(OperatorID, LoginDate,CHE,BlockName)  Values('" + StrOperatorID + "',GetDate(),'" + this.CHE.Text + "','" + this.LiftBlockName.Text + "')");
                    
                    FrmMap01 frm = new FrmMap01();
                    frm.Show();
                   //  this.Password.Text = "";
                }
            }
            Cursor.Current = Cursors.Default;
        }

        
        private void CHE_Click(object sender, EventArgs e)
        {

            this.CHE.DroppedDown = true;
        }

      

        private void LoginName_Click(object sender, EventArgs e)
        {

            this.LoginName.DroppedDown = true;
        }


        private void LiftBlockName_Click(object sender, EventArgs e)
        {

            this.LiftBlockName.DroppedDown = true;
        }


     









        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.Password.Text += ctl.Text.ToString();

        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.Password.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.Password.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.Password.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            if (this.Password.Text != string.Empty)
            {
                this.Password.Text = this.Password.Text.Substring(0, Password.Text.Length - 1);
            }
        }

        protected void SetBackColorField()
        {
            Color myColor = Color.FromArgb(255, 255, 255);
            this.CHE.BackColor = myColor;
            this.LiftBlockName.BackColor = myColor;
            this.LoginName.BackColor = myColor;
            this.Password.BackColor = myColor;
        }

        protected int BackColorField()
        {
            Color myColor = Color.FromArgb(255, 255, 255);
            Color mustColor = Color.FromArgb(245, 191, 106);
            int Ans = 0;
            if (this.CHE.Visible == true & this.CHE.BackColor == myColor & this.CHE.Text == string.Empty)
            { this.CHE.BackColor = mustColor; Ans = 1; }
            if (this.LiftBlockName.Visible == true & this.LiftBlockName.BackColor == myColor & this.LiftBlockName.Text == string.Empty)
            { this.LiftBlockName.BackColor = mustColor; Ans = 1; }
            if (this.LoginName.Visible == true & this.LoginName.BackColor == myColor & this.LoginName.Text == string.Empty)
            { this.LoginName.BackColor = mustColor; Ans = 1; }
            if (this.Password.Visible == true & this.Password.BackColor == myColor & this.Password.Text == string.Empty)
            { this.Password.BackColor = mustColor; Ans = 1; }

            if (Ans == 1) return 1; else return 0;

        }

        private void LoginName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Password.Text = string.Empty;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\RTG\CHEName.txt");
             foreach (string line in lines)
            {
               this.CHE.Text = line;
            }

        }

      
        
    }
}

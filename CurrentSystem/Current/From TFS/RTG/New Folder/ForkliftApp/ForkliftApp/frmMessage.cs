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
 

namespace ForkliftApp
{
    public partial class frmMessage : Form
    {
        string connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=new_inter;Password=new_inter";
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlCommandBuilder builder;
            SqlDataAdapter adapter;
            SqlCommand sqlcmd = new SqlCommand();


        public frmMessage()
        {
            InitializeComponent();
        }

    

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            string query;
            connection.Open();
            for (int i = 0; i < GridMessage.Rows.Count-1; i++)
            {
                query = " UPDATE TB_MessageMalgezot SET UsersRead = '" + frmLogIn.GlobalVariables.StrUserRead + frmLogIn.GlobalVariables.LoginNameStr + "' WHERE Message = '" + GridMessage.Rows[i].Cells[0].Value.ToString() + "'";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            timer1.Start();
            GridMessage.Dispose();
            connection.Close();
            frmMessage.ActiveForm.Visible = false;
        }


        public int Showmessage(object sender, EventArgs e)
        {
            string query;
            query = " SELECT Message, UsersRead, MessageDate FROM dbo.TB_MessageMalgezot WHERE (LEFT(MessageDate, 12) = LEFT(GETDATE(), 12))  ";
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
                GridMessage.Columns.Add("Message", "Message");
                GridMessage.Columns.Add("UsersRead", "UsersRead");
                GridMessage.Columns.Add("MessageDate", "MessageDate");
                GridMessage.Rows.Add(dt.Rows.Count);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][1].ToString().IndexOf(frmLogIn.GlobalVariables.LoginNameStr) == -1)
                    {
                        frmLogIn.GlobalVariables.StrUserRead = dt.Rows[i][1].ToString();
                        GridMessage.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                        GridMessage.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                        GridMessage.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                    }
                }
            }
            connection.Close();
            timer1.Stop();
            

            return 1;
        }


    }
}

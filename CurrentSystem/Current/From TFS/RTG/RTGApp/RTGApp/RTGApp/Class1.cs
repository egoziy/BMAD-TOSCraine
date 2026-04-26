using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms; 

namespace RTGApp
{
    


    class ConTerminalData
    {

         public  DataTable ReturnDT(string instr)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlCommandBuilder builder;
            SqlDataAdapter adapter;
            SqlCommand sqlcmd = new SqlCommand();
            DataTable dt100 = new DataTable();
            string connectionString;
            string UserNameStr;
            string PasswordStr;

            UserConnection Con = new UserConnection();

            if (System.Windows.Forms.Application.OpenForms.Count == 0)
            {
                UserNameStr = "Malgezot";
                PasswordStr = "12345678";

                if (instr.IndexOf("syslogins") > 0 || instr.IndexOf("RG_A1") > 0 || instr.IndexOf("RefreshMapRTG") > 0)
                {
                    connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=malgezot;Password=12345678";
           
                }


                else
                {
                    connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=" + UserNameStr + ";Password=" + PasswordStr;
                }
                
                connection.ConnectionString = connectionString;
                command.Connection = connection;
                adapter = new SqlDataAdapter(command.CommandText, connection);
                builder = new SqlCommandBuilder(adapter);
                command.CommandType = CommandType.Text;
                command.CommandText = instr;


                if (instr.Substring(0, 6).ToString() == "SELECT")
                {
                    adapter.SelectCommand = command;
                    adapter.Fill(dt100);
                }

                else
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    dt100.Columns.Add(instr.Substring(0, 6).ToString());
                }











            }
            else
            {

                foreach (Form f in System.Windows.Forms.Application.OpenForms)
                {
                    if (f.Name == "FrmLogin")
                    {
                        //  local_X = f.X;   // access value here and set in local variable

                        Control[] cl = f.Controls.Find("LoginName", true);
                        ComboBox ComboBox1 = (ComboBox)cl[0];
                        UserNameStr = ComboBox1.Text;

                        cl = f.Controls.Find("Password", true);
                        MaskedTextBox TextBox2 = (MaskedTextBox)cl[0];
                        PasswordStr = TextBox2.Text;

                        //if (frm.lo== null)
                        //{
                        //    Con.SystemUserName = "malgezot";
                        //}


                        //if (Con.SystemPassword == null)
                        //{
                        //    Con.SystemPassword = "12345678";
                        //}


                        if (instr.IndexOf("syslogins") > 0 || instr.IndexOf("RG_A1") > 0 || instr.IndexOf("RefreshMapRTG") > 0)
                        {
                            connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=malgezot;Password=12345678";

                        }


                        else
                        {
                            connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=" + UserNameStr + ";Password=" + PasswordStr;
                           

                        }
                
                        connection.ConnectionString = connectionString;
                        command.Connection = connection;
                        adapter = new SqlDataAdapter(command.CommandText, connection);
                        builder = new SqlCommandBuilder(adapter);
                        command.CommandType = CommandType.Text;
                        command.CommandText = instr;


                        if (instr.Substring(0, 6).ToString() == "SELECT")
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt100);
                        }

                        else
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            dt100.Columns.Add(instr.Substring(0, 6).ToString());
                        }

                    }

                    break;
                }
            }  
            return dt100;
        }

    }
}

  
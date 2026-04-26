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
using RTGApp.Properties;

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
            connectionString = Settings.Default.TerminalDataConnectionString;

            if (System.Windows.Forms.Application.OpenForms.Count == 0)
            {
 
              
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
                            //if (!instr.StartsWith("INSERT INTO RG_ErrorLog"))
                            //{
                            //    string sql = "INSERT INTO RG_ErrorLog(CHE, BlockName, Program_Version, ErrorNumber, Msg) VALUES(" +
                            //            "'" + FrmLogin.StrCHE.ToString() + "'," +
                            //            "'" + FrmLogin.StrBlockName.ToString() + "'," +
                            //            "'2023-01-23'," +
                            //            "'999'," +
                            //            "'" + instr + "')";
                            //    command.ExecuteNonQuery();

                            //}


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

  
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
 

public class Crc16Ccitt
{
    




    public string calcChecksum(string instr)
    {
        int Checksum = 0;
        for (int i = 0; i < instr.Length; i++)
        {
            Checksum += (int)(instr[i]);
        }

        ushort twosComp = (ushort)(~Checksum + 1);
        string h = string.Format("{0:X}", twosComp);

        return h;
    }




    public DataTable ReturnDT(string instr)
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommandBuilder builder;
        SqlDataAdapter adapter;
        SqlCommand sqlcmd = new SqlCommand();
        string connectionString;
        connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;Integrated Security=SSPI;";
        connection.ConnectionString = connectionString;
        command.Connection = connection;
        adapter = new SqlDataAdapter(command.CommandText, connection);
        builder = new SqlCommandBuilder(adapter);
        command.CommandType = CommandType.Text;
        command.CommandText = instr;
        DataTable dt100 = new DataTable();

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

        return dt100;
    }




 





     
  
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace ForkliftApp.Framework
{
    public class DatabaseManager
    {
        private static SqlConnection conn;

        private static string m_User;

        public static string User
        {
            get { return DatabaseManager.m_User; }
            set { DatabaseManager.m_User = value; }
        }

        private static string m_PassWoed;

        public static string PassWoed
        {
            get { return DatabaseManager.m_PassWoed; }
            set { DatabaseManager.m_PassWoed = value; }
        }

        private static string m_ForkliftNumber;

        public string ForkliftNum
        {
            get { return m_ForkliftNumber; }
            set { m_ForkliftNumber = value; }
        }

        public DatabaseManager(string usernamen, string password, string forkliftnum )
        {
            m_User = usernamen;
            m_PassWoed = password;
            m_ForkliftNumber = forkliftnum;
        }
        
        public DatabaseManager(string usernamen, string password)
        {
            m_User = usernamen;
            m_PassWoed = password;
        }

        public DatabaseManager()
        { }

        public static SqlConnection GetConnection()
        {
            string connString = "Data Source=192.6.8.52;Initial Catalog=TerminalData; User ID=" + m_User + "; Password=" + m_PassWoed + ";";
            
            //"Data Source=SQLSRV;Initial Catalog=TerminalData;User ID=sa;Password=302961";
            //System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
            //"Persist Security Info=False; Integrated Security=false; Database=tbl_conect; Server=192.6.8.50,1433; Connect Timeout=30; "
            //+ "User ID=" + userName + "; Password=" + userPass + ";";
            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (SqlException sqe)
            {
                if (conn.State.ToString()=="Closed")
                {
                    //int args = Environment.GetCommandLineArgs();
                    MessageBox.Show(sqe.Message + "\n לא קיים משתמש בשם הזה, נסו שנית בבקשה");
                    Environment.Exit(1);
                }
            }
            catch (Exception)
            {
                connString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;User ID=" + m_User + ";Password=" + m_PassWoed + ";";
                //ShowSqlException(connString);
            }
            return conn;
        }

        private static bool IsValidUser()
        {
            return false;
        }

        public static void CloseConection()
        {
            conn.Close();
        }

        public string GetUserName()
        {
            return m_User;
        }

        public string GetPassWoed()
        {
            return m_PassWoed;
        }

        public string GetForkliftNumber()
        {
            return m_ForkliftNumber;
        }
    }
}

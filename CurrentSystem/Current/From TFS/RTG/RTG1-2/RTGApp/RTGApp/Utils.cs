using RTGApp.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace RTGApp
{
    internal class Utils
    {

        public static void WriteLog(string strLog, string error="")
        {
            try
            {
                ConTerminalData Con1 = new ConTerminalData();
                string sql = "INSERT INTO RG_ErrorLog(CHE, BlockName, Program_Version, ErrorNumber, Msg) VALUES(" +
                    "'" + FrmLogin.StrCHE.ToString() + "'," +
                    "'" + FrmLogin.StrBlockName.ToString() + "'," +
                    "'2025-08-04'," +
                    "'" + error + "'," +
                    "'" + strLog + "')";
                Con1.ReturnDT(sql);
            }
            catch { 
            }
        }

    }
}

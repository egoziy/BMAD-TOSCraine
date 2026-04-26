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
    public partial class FrmContainerNoLocation : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand(); 
        ConTerminalData Con1 = new ConTerminalData();

        public FrmContainerNoLocation()
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = Con1.ReturnDT("SELECT CO_Containers.Container AS מכולה, CO_Containers.EntranceDate AS [תאריך כניסה], CP_Deal.HandlingTypeCode AS [קוד טיפול], " +
                                         "CO_Containers.CarrierCode AS [קו ספנות], CO_Containers.LocationCode, CO_Containers.ExitDate, CO_Containers.DealNumber, " +
                                         "CO_Containers.DealNumberSub, CO_Containers.Manifest " +
                                         "FROM CO_Containers INNER JOIN " +
                                         "CP_Deal ON CO_Containers.DealNumber = CP_Deal.DealNumber " +
                                         "WHERE (CO_Containers.EntranceDate IS NOT NULL AND CO_Containers.EntranceDate >= CONVERT(DATETIME, '2011-01-01 00:00:00', 102)) AND " +
                                         "(CO_Containers.ExitDate IS NULL) AND (CO_Containers.LocationCode IS NULL)");
            if (dt.Rows.Count > 0)
            {
                this.dataGridConNoLocation.DataSource = dt;
                this.txtSumContainers.Text = " סהכ " + dt.Rows.Count.ToString() + " מכולות ";
                for (int i = 0; i < dataGridConNoLocation.Columns.Count; i++)
                {
                    if (i >= 4)
                    {
                        this.dataGridConNoLocation.Columns[i].Visible = false;
                    }
                }
            }
            else
            {
                this.dataGridConNoLocation.Visible = false;
                this.lblNoData.Visible = true;
                this.txtSumContainers.Text = string.Empty;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

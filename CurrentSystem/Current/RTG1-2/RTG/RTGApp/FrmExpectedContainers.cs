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
    public partial class FrmExpectedContainers : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand();
        ConTerminalData Con1 = new ConTerminalData();
        public static bool ShipingLine = false;

        public FrmExpectedContainers()
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = Con1.ReturnDT("SELECT RTRIM(LTRIM(dbo.CO_Containers.CarrierCode)) AS CarrierCode, dbo.TC_Client.ClientName, dbo.TC_Client.ClientNameHe " +
                                          "FROM dbo.CO_Containers INNER JOIN " +
                                          "dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber INNER JOIN " +
                                          "dbo.TC_Client ON dbo.CO_Containers.CarrierCode = dbo.TC_Client.ClientCode " +
                                          "WHERE (dbo.CP_Deal.HandlingTypeCode = 'EM') AND (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitDate IS NULL) " +
                                          "GROUP BY dbo.CO_Containers.CarrierCode, dbo.TC_Client.ClientName, dbo.TC_Client.ClientNameHe " +
                                          "HAVING (dbo.TC_Client.ClientName IS NOT NULL) AND (RTRIM(LTRIM(dbo.CO_Containers.CarrierCode)) <> 'CSV') " +
                                          "ORDER BY CarrierCode");
            this.cmbShipingLine.ValueMember = "CarrierCode";
            this.cmbShipingLine.DisplayMember = "CarrierCode";
            this.cmbShipingLine.DataSource = dt;
            this.cmbShipingLine.SelectedIndex = -1;
            ShipingLine = true;

            DataTable dt1 = Con1.ReturnDT("SELECT CO_Containers.Container AS מכולה, CO_ContainerProfile.ContainerLength AS גודל, CO_ContainerProfile.ContainerTypeCode AS סוג, " +
                                         "CP_Deal.HandlingTypeCode AS טיפול, CO_Containers.RecommendedLocationCode AS [איתור צפוי], TC_Client.ShortClientName AS לקוח, " +
                                         "RTRIM(LTRIM(dbo.CO_Containers.CarrierCode)) AS [קו], CO_Containers.RegisterDate AS [תאריך שידור] " +
                                         "FROM         TC_Client INNER JOIN " +
                                         "CP_Deal ON TC_Client.ClientCode = CP_Deal.RecieveCommisionCode RIGHT OUTER JOIN " +
                                         "CO_Containers INNER JOIN " +
                                         "CO_ContainerProfile ON CO_Containers.Container = CO_ContainerProfile.Container LEFT OUTER JOIN " +
                                         "TC_Client AS TC_Client_1 ON CO_Containers.ShipAgentCode = TC_Client_1.ClientCode ON CP_Deal.DealNumber = CO_Containers.DealNumber " +
                                         "WHERE     (CO_Containers.ExitDate IS NULL) AND (CP_Deal.HandlingTypeCode = 'HH' OR " +
                                         "CP_Deal.HandlingTypeCode = 'PP' OR CP_Deal.HandlingTypeCode = 'FR' OR " +
                                         "CP_Deal.HandlingTypeCode = 'EX') AND (CO_Containers.EntranceDate IS NULL) AND (CO_Containers.RegisterDate > GETDATE() - 7)");

            this.dataGridExpectedContainers.DataSource = dt1;
            Cursor.Current = Cursors.Default;
        }

        private void cmbShipingLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShipingLine)
            {
                GetFilter();
            }
        }

        private void cmbContainerLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFilter();
        }

        private void cmbcontainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFilter();
        }

        private void cmbHandlingTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFilter();
        }


        protected void GetFilter()
        {
            Cursor.Current = Cursors.WaitCursor;
            string Filter = string.Empty;
            DataView dv = new DataView();
            this.dataGridExpectedContainers.Visible = true;
            this.lblNoData.Visible = false;

            

            DataTable dt1 = Con1.ReturnDT("SELECT CO_Containers.Container AS מכולה, CO_ContainerProfile.ContainerLength AS גודל, CO_ContainerProfile.ContainerTypeCode AS סוג, " +
                                         "CP_Deal.HandlingTypeCode AS טיפול, CO_Containers.RecommendedLocationCode AS [איתור צפוי], TC_Client.ShortClientName AS לקוח, " +
                                         "RTRIM(LTRIM(dbo.CO_Containers.CarrierCode)) AS [קו], CO_Containers.RegisterDate AS [תאריך שידור] " +
                                         "FROM         TC_Client INNER JOIN " +
                                         "CP_Deal ON TC_Client.ClientCode = CP_Deal.RecieveCommisionCode RIGHT OUTER JOIN " +
                                         "CO_Containers INNER JOIN " +
                                         "CO_ContainerProfile ON CO_Containers.Container = CO_ContainerProfile.Container LEFT OUTER JOIN " +
                                         "TC_Client AS TC_Client_1 ON CO_Containers.ShipAgentCode = TC_Client_1.ClientCode ON CP_Deal.DealNumber = CO_Containers.DealNumber " +
                                         "WHERE     (CO_Containers.ExitDate IS NULL) AND (CP_Deal.HandlingTypeCode = 'HH' OR " +
                                         "CP_Deal.HandlingTypeCode = 'PP' OR CP_Deal.HandlingTypeCode = 'FR' OR " +
                                         "CP_Deal.HandlingTypeCode = 'EX') AND (CO_Containers.EntranceDate IS NULL) AND (CO_Containers.RegisterDate > GETDATE() - 7)" + Filter);
            dv = new DataView(dt1);
            dv.RowFilter = "";

            if (this.cmbShipingLine.Text != "")
            {
                Filter = Filter + "קו = '" + this.cmbShipingLine.Text + "' AND ";
            }
            if (this.cmbContainerLength.Text != "")
            {
                Filter = Filter + "גודל = '" + this.cmbContainerLength.Text + "' AND ";
            }
            if (this.cmbcontainerType.Text != "")
            {
                Filter = Filter + "סוג = '" + this.cmbcontainerType.Text + "' AND ";
            }

            if (Filter.Substring(Filter.Length - 4, 4) == "AND ")
            {
                Filter = Filter.Substring(0, Filter.Length - 4);
            }

            dv.RowFilter = Filter;
            
            if (dv.Count > 0)
            {
                this.dataGridExpectedContainers.DataSource = dt1;
                this.txtSumContainers.Text = " סהכ " + dt1.Rows.Count.ToString() + " מכולות ";
            }
            else
            {
                this.dataGridExpectedContainers.Visible = false;
                this.lblNoData.Visible = true;
                this.txtSumContainers.Text = string.Empty;
            }
            Cursor.Current = Cursors.Default;
        }

        private void cmbShipingLine_Click(object sender, EventArgs e)
        {
            this.cmbShipingLine.DroppedDown = true;
        }

        private void cmbContainerLength_Click(object sender, EventArgs e)
        {
            this.cmbContainerLength.DroppedDown = true;
        }

        private void cmbcontainerType_Click(object sender, EventArgs e)
        {
            this.cmbcontainerType.DroppedDown = true;
        }

        private void cmbHandlingTypeCode_Click(object sender, EventArgs e)
        {
            this.cmbHandlingTypeCode.DroppedDown = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

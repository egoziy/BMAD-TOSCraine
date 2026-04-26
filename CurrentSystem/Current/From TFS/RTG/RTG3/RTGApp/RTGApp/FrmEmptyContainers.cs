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
    public partial class FrmEmptyContainers : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand(); 
        ConTerminalData Con1 = new ConTerminalData();
        public static bool ShipingLine = false;

        public FrmEmptyContainers()
        {
            try { 
            InitializeComponent();
            this.dataGridEMContainers.Visible = true;
            this.lblNoData.Visible = false;
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
             
             DataTable dt1 = Con1.ReturnDT("SELECT TOP (100) PERCENT dbo.CO_Containers.Container AS מכולה, dbo.CO_ContainerProfile.ContainerLength AS גודל, " +
                                           "dbo.CO_ContainerProfile.ContainerTypeCode AS סוג, dbo.CO_Containers.LocationCode AS איתור, dbo.CO_Containers.EntranceDate AS [תאריך כניסה], " +
                                           "dbo.CO_Containers.CarrierCode AS קו, dbo.TC_Client.ClientName AS לקוח, dbo.CO_ContainerProfile.ContainerNumber AS [מספר מכולה], " +
                                           "dbo.CO_Containers.Manifest, dbo.CO_Containers.DealNumber, dbo.CO_Containers.DealNumberSub, dbo.CP_Deal.HandlingTypeCode, " +
                                           "dbo.CO_Containers.ShipAgentCode, dbo.CO_Containers.ExitDate " +
                                           "FROM dbo.CO_Containers INNER JOIN " +
                                           "dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container LEFT OUTER JOIN " +
                                           "dbo.TC_Client ON dbo.CO_Containers.ShipAgentCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN " +
                                           "dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber " +
                                           "WHERE (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitDate IS NULL) AND (dbo.CP_Deal.HandlingTypeCode = 'EM') " +
                                           "AND (dbo.CO_Containers.EntranceDate >= CONVERT(DATETIME, '2010-10-01 00:00:00', 102)) AND (dbo.CO_Containers.ReserveDate IS NULL) " +
                                           "ORDER BY [תאריך כניסה]");
             if (dt1.Rows.Count > 0)
             {

                 DataView dv = new DataView(dt);
                 dv.RowFilter = "";
                 this.dataGridEMContainers.DataSource = dv;


                 this.dataGridEMContainers.DataSource = dt1;
                 this.txtSumContainers.Text = " סהכ " + dt1.Rows.Count.ToString() + " מכולות ";
                 for (int i = 0; i < dataGridEMContainers.Columns.Count; i++)
                 {
                     if (i >= 7)
                     {
                         this.dataGridEMContainers.Columns[i].Visible = false;
                     }
                 }
             }
             else
             {
                 this.dataGridEMContainers.Visible = false;
                 this.lblNoData.Visible = true;
                 this.txtSumContainers.Text = string.Empty;
             }
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in FrmEmptyContainers FrmEmptyContainers : " + ex);
            }
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


        private void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            try { 
            Cursor.Current = Cursors.WaitCursor;
            this.dataGridEMContainers.Visible = true;
            DataTable dt1 = Con1.ReturnDT("SELECT TOP (100) PERCENT dbo.CO_Containers.Container AS מכולה, dbo.CO_ContainerProfile.ContainerLength AS גודל, " +
                                           "dbo.CO_ContainerProfile.ContainerTypeCode AS סוג, dbo.CO_Containers.LocationCode AS איתור, dbo.CO_Containers.EntranceDate AS [תאריך כניסה], " +
                                           "dbo.CO_Containers.CarrierCode AS קו, dbo.TC_Client.ClientName AS לקוח, dbo.CO_ContainerProfile.ContainerNumber AS [מספר מכולה], " +
                                           "dbo.CO_Containers.Manifest, dbo.CO_Containers.DealNumber, dbo.CO_Containers.DealNumberSub, dbo.CP_Deal.HandlingTypeCode, " +
                                           "dbo.CO_Containers.ShipAgentCode, dbo.CO_Containers.ExitDate " +
                                           "FROM dbo.CO_Containers INNER JOIN " +
                                           "dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container LEFT OUTER JOIN " +
                                           "dbo.TC_Client ON dbo.CO_Containers.ShipAgentCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN " +
                                           "dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber " +
                                           "WHERE (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitDate IS NULL) AND (dbo.CP_Deal.HandlingTypeCode = 'EM') " +
                                           "AND (dbo.CO_Containers.EntranceDate >= CONVERT(DATETIME, '2010-10-01 00:00:00', 102)) AND (dbo.CO_Containers.ReserveDate IS NULL) " +
                                           "ORDER BY [תאריך כניסה]");
            if (dt1.Rows.Count > 0)
            {
                this.dataGridEMContainers.DataSource = dt1;
                this.txtSumContainers.Text = " סהכ " + dt1.Rows.Count.ToString() + " מכולות ";
            }
            else
            {
                this.dataGridEMContainers.Visible = false;
                this.lblNoData.Visible = true;
                this.txtSumContainers.Text = string.Empty;
            }
            this.cmbShipingLine.SelectedIndex = -1;
            this.cmbContainerLength.SelectedIndex = 0;
            this.cmbcontainerType.SelectedIndex = 0;
            Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in FrmEmptyContainers btnShowAllRecords_Click : " + ex);
            }
        }



        protected void GetFilter()
        {
            try { 
            Cursor.Current = Cursors.WaitCursor;
            string Filter = string.Empty;
            DataView dv = new DataView();
            this.dataGridEMContainers.Visible = true;
            this.lblNoData.Visible = false;

            DataTable dt1 = Con1.ReturnDT("SELECT TOP (100) PERCENT dbo.CO_Containers.Container AS מכולה, dbo.CO_ContainerProfile.ContainerLength AS גודל, " +
                                          "dbo.CO_ContainerProfile.ContainerTypeCode AS סוג, dbo.CO_Containers.LocationCode AS איתור, dbo.CO_Containers.EntranceDate AS [תאריך כניסה], " +
                                          "dbo.CO_Containers.CarrierCode AS קו, dbo.TC_Client.ClientName AS לקוח, dbo.CO_ContainerProfile.ContainerNumber AS [מספר מכולה], " +
                                          "dbo.CO_Containers.Manifest, dbo.CO_Containers.DealNumber, dbo.CO_Containers.DealNumberSub, dbo.CP_Deal.HandlingTypeCode, " +
                                          "dbo.CO_Containers.ShipAgentCode, dbo.CO_Containers.ExitDate " +
                                          "FROM dbo.CO_Containers INNER JOIN " +
                                          "dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container LEFT OUTER JOIN " +
                                          "dbo.TC_Client ON dbo.CO_Containers.ShipAgentCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN " +
                                          "dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber " +
                                          "WHERE (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitDate IS NULL) AND (dbo.CP_Deal.HandlingTypeCode = 'EM') " +
                                          "AND (dbo.CO_Containers.EntranceDate >= CONVERT(DATETIME, '2010-10-01 00:00:00', 102)) AND (dbo.CO_Containers.ReserveDate IS NULL) " + Filter +
                                          "ORDER BY [תאריך כניסה]");
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

            if(Filter.Substring(Filter.Length-4,4) == "AND ")
            {
                Filter = Filter.Substring(0,Filter.Length - 4);
            }

            dv.RowFilter = Filter;
            dv.Sort = "[תאריך כניסה]";
            

            if (dv.Count > 0)
            {
                this.dataGridEMContainers.DataSource = dv;
                this.txtSumContainers.Text = " סהכ " + dt1.Rows.Count.ToString() + " מכולות ";
            }
            else
            {
                this.dataGridEMContainers.Visible = false;
                this.lblNoData.Visible = true;
                this.txtSumContainers.Text = string.Empty;
            }
            Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in FrmEmptyContainers GetFilter : " + ex);
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }


}

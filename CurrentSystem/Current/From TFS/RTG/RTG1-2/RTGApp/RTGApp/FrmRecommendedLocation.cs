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
    public partial class FrmRecommendedLocation : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand();
        ConTerminalData Con1 = new ConTerminalData();
        private string ClientCode = string.Empty;
        private string ContainerLength = string.Empty;
        private string ContainerTypeCode = string.Empty;
        private string ShipingLine = string.Empty;
        private string RecommendedLocation = string.Empty;

        public FrmRecommendedLocation()
        {
            InitializeComponent();
            string StrClient = string.Empty;
            string StrCarrierCode = string.Empty;
            DataTable dt = Con1.ReturnDT("SELECT     dbo.TB_RecommendedLocation.ClientCode + dbo.TC_Client.ShortClientName as [לקוח], dbo.TB_RecommendedLocation.ContainerLength as [גודל], " +
                                         "dbo.TB_RecommendedLocation.ContainerTypeCode as [סוג],  dbo.TB_RecommendedLocation.CarrierCode as [ספנות], " +
                                         "dbo.TB_RecommendedLocation.RecommendedLocationCode as [א מומלץ] " +
                                         "FROM dbo.TB_RecommendedLocation INNER JOIN " +
                                         "dbo.TC_Client ON dbo.TB_RecommendedLocation.ClientCode = dbo.TC_Client.ClientCode " +
                                         "WHERE     (dbo.TB_RecommendedLocation.HandlingTypeCode = 'EM') " +
                                         "ORDER BY dbo.TB_RecommendedLocation.ClientCode");
            this.dataGridRecommendedLocation.DataSource = dt;
            foreach (DataRow oRow in dt.Rows)
            {
                if (oRow.ItemArray[0].ToString() != StrClient)
                {
                    this.cmbClientCode.Items.Add(oRow.ItemArray[0]);
                    StrClient = oRow.ItemArray[0].ToString();
                }
            }
        }

        private void cmbClientCode_Click(object sender, EventArgs e)
        {
            this.cmbClientCode.DroppedDown = true;
        }

        private void cmbClientCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbContainerLength.Items.Clear();
            this.cmbContainerTypeCode.Items.Clear();
            this.cmbShipingLine.Items.Clear();
            this.cmbContainerLength.Text = string.Empty;
            this.cmbContainerTypeCode.Text = string.Empty;
            this.cmbShipingLine.Text = string.Empty;
            DataTable dt = Con1.ReturnDT("SELECT     dbo.TB_RecommendedLocation.ClientCode + dbo.TC_Client.ShortClientName as [לקוח], dbo.TB_RecommendedLocation.ContainerLength as [גודל], " +
                                         "dbo.TB_RecommendedLocation.ContainerTypeCode as [סוג],  dbo.TB_RecommendedLocation.CarrierCode as [ספנות], " +
                                         "dbo.TB_RecommendedLocation.RecommendedLocationCode as [א מומלץ] " +
                                         "FROM dbo.TB_RecommendedLocation INNER JOIN " +
                                         "dbo.TC_Client ON dbo.TB_RecommendedLocation.ClientCode = dbo.TC_Client.ClientCode " +
                                         "WHERE     (dbo.TB_RecommendedLocation.HandlingTypeCode = 'EM') " +
                                         "ORDER BY dbo.TB_RecommendedLocation.ClientCode");
            DataView dv = new DataView(dt);
            string strContainerLength = string.Empty;
            string strContainerTypeCode = string.Empty;
            string strShipingLine = string.Empty;
            DataView dv1;
            dv1 = new DataView(dt, "לקוח = '" + this.cmbClientCode.Text + "'", "לקוח Desc", DataViewRowState.CurrentRows);
            this.dataGridRecommendedLocation.DataSource = dv1;

            foreach (DataRow oRow in dv.Table.Rows)
            {
                if (oRow.ItemArray[0].ToString() == this.cmbClientCode.Text)
                {
                    if (!strContainerLength.Contains(oRow.ItemArray[1].ToString()))
                    {
                        this.cmbContainerLength.Items.Add(oRow.ItemArray[1]);
                        strContainerLength = strContainerLength + oRow.ItemArray[1].ToString();
                    }
                    if (!strContainerTypeCode.Contains(oRow.ItemArray[2].ToString()))
                    {
                        this.cmbContainerTypeCode.Items.Add(oRow.ItemArray[2]);
                        strContainerTypeCode = strContainerTypeCode + oRow.ItemArray[2].ToString();
                    }
                    if (!strShipingLine.Contains(oRow.ItemArray[3].ToString().Trim()))
                    {
                        this.cmbShipingLine.Items.Add(oRow.ItemArray[3].ToString().Trim());
                        strShipingLine = strShipingLine + oRow.ItemArray[3].ToString().Trim();
                    }
                }
            }
        }

        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.txtLocation.Text += ctl.Text.ToString();

        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            if (this.txtRecommendedLocation.Text != string.Empty || this.txtLocation.Text != string.Empty)
            {
                //this.txtRecommendedLocation.Text = this.txtRecommendedLocation.Text.Substring(0, txtRecommendedLocation.Text.Length - 1);
                this.txtLocation.Text = this.txtLocation.Text.Substring(0, txtLocation.Text.Length - 1);
                
            }
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = btnA;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = btnD;
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.txtRecommendedLocation.Text = this.txtLocation.Text;
            ConTerminalData Con3 = new ConTerminalData();
            if (this.cmbClientCode.Text != "" & this.cmbContainerLength.Text != "" & this.cmbContainerTypeCode.Text != "" & this.cmbShipingLine.Text != "")
            {
                ClientCode = this.cmbClientCode.Text.Substring(0, 3);
                ContainerLength = this.cmbContainerLength.Text;
                ContainerTypeCode = this.cmbContainerTypeCode.Text;
                ShipingLine = this.cmbShipingLine.Text;
                RecommendedLocation = this.txtRecommendedLocation.Text;
                DataTable dt = new DataTable();
                dt = Con3.ReturnDT("UPDATE TB_RecommendedLocation SET  RecommendedLocationCode = '" + RecommendedLocation + "'" +
                                   "WHERE     (ClientCode = '" + ClientCode + "') AND (ContainerLength = '" + ContainerLength + "') AND (ContainerTypeCode = '" +
                                    ContainerTypeCode + "') AND (CarrierCode = '" + ShipingLine + "') AND (HandlingTypeCode = 'EM')");
                Cursor.Current = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(2000);
                DataTable dt1 = Con1.ReturnDT("SELECT     dbo.TB_RecommendedLocation.ClientCode + dbo.TC_Client.ShortClientName as [לקוח], dbo.TB_RecommendedLocation.ContainerLength as [גודל], " +
                             "dbo.TB_RecommendedLocation.ContainerTypeCode as [סוג],  dbo.TB_RecommendedLocation.CarrierCode as [ספנות], " +
                             "dbo.TB_RecommendedLocation.RecommendedLocationCode as [א מומלץ] " +
                             "FROM dbo.TB_RecommendedLocation INNER JOIN " +
                             "dbo.TC_Client ON dbo.TB_RecommendedLocation.ClientCode = dbo.TC_Client.ClientCode " +
                             "WHERE     (dbo.TB_RecommendedLocation.HandlingTypeCode = 'EM') " +
                             "ORDER BY dbo.TB_RecommendedLocation.ClientCode");
                DataView dv1;
                dv1 = new DataView(dt1, "לקוח = '" + this.cmbClientCode.Text + "'", "לקוח Desc", DataViewRowState.CurrentRows);
                this.dataGridRecommendedLocation.DataSource = dv1;
                this.txtLocation.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("עליך למלא את כל הפרטים לעדכון בשדות שלמעלה");
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

        private void cmbContainerTypeCode_Click(object sender, EventArgs e)
        {
            this.cmbContainerTypeCode.DroppedDown = true;
        }

        private void cmbcmbClientCode_Click(object sender, EventArgs e)
        {
            this.cmbClientCode.DroppedDown = true;
        }

       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

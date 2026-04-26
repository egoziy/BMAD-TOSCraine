using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForkliftApp.Entities;
using ForkliftApp.DataAccess;
using ForkliftApp.BusinessLogic;

namespace ForkliftApp
{
    public partial class frmRecommendedLocation : Form
    {
        private ForkliftAppBL BL;
        private string ClientCode = string.Empty;
        private string ContainerLength = string.Empty;
        private string ContainerTypeCode = string.Empty;
        private string ShipingLine = string.Empty;
        private string RecommendedLocation = string.Empty;
        public frmRecommendedLocation()
        {
            InitializeComponent();
            DataTable ClientName = new DataTable();
            string StrClient = string.Empty;
            string StrCarrierCode = string.Empty;
            ClientName = ForkliftAppDA.GetRecommendedLocation();
            DataView dv = new DataView(ClientName);
            dv.Sort = "לקוח";
            this.dataGridRecommendedLocation.DataSource = dv;
            foreach (DataRow oRow in dv.Table.Rows)
            {
                if (oRow.ItemArray[0].ToString() != StrClient)
                {
                    this.cmbClientCode.Items.Add(oRow.ItemArray[0]);
                    StrClient = oRow.ItemArray[0].ToString();
                }
            }

            //foreach (DataRow oRow in dv.Table.Rows)
            //{
            //    if (oRow.ItemArray[3].ToString().Trim() != StrCarrierCode)
            //    {
            //        if (oRow.ItemArray[3].ToString().Trim() != "")
            //        {
            //            this.cmbShipingLine.Items.Add(oRow.ItemArray[3].ToString().Trim());
            //            StrCarrierCode = oRow.ItemArray[3].ToString().Trim();
            //        }
            //    }
            //}
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            ClientCode = this.cmbClientCode.Text.Substring(0, 3);
            ContainerLength = this.cmbContainerLength.Text;
            ContainerTypeCode = this.cmbContainerTypeCode.Text;
            ShipingLine = this.cmbShipingLine.Text;
            RecommendedLocation = this.txtRecommendedLocation.Text;
            DataTable dt = new DataTable();

            BL = new ForkliftAppBL(ClientCode, ContainerLength, ContainerTypeCode, ShipingLine, RecommendedLocation);

            if (this.cmbClientCode.Text != "" & this.cmbContainerLength.Text != "" & this.cmbContainerTypeCode.Text != "" & this.cmbShipingLine.Text != "")
            {
                ForkliftAppDA.UpdateRecommendedLocation(ClientCode, ContainerLength, ContainerTypeCode, ShipingLine, RecommendedLocation);
                DataView dv1;
                dt = ForkliftAppDA.GetRecommendedLocation();
                dv1 = new DataView(dt, "לקוח = '" + this.cmbClientCode.Text + "'", "לקוח Desc", DataViewRowState.CurrentRows);
                this.dataGridRecommendedLocation.DataSource = dv1;
            }
            else
            {
                MessageBox.Show("עליך למלא את כל הפרטים לעדכון");
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
            DataTable dt = new DataTable();
            dt = ForkliftAppDA.GetRecommendedLocation();
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

        private void cmbContainerLength_Click(object sender, EventArgs e)
        {
            this.cmbContainerLength.DroppedDown = true;
        }


        private void cmbContainerLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbContainerTypeCode.Items.Clear();
            this.cmbShipingLine.Items.Clear();
            DataTable dt = new DataTable();
            dt = ForkliftAppDA.GetRecommendedLocation();
            DataView dv = new DataView(dt);
            string strContainerTypeCode = string.Empty;
            string strShipingLine = string.Empty;

            foreach (DataRow oRow in dv.Table.Rows)
            {
                if (oRow.ItemArray[0].ToString() == this.cmbClientCode.Text & oRow.ItemArray[1].ToString() == this.cmbContainerLength.Text)
                {
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
            this.txtRecommendedLocation.Text += ctl.Text.ToString();

        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
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
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            if (this.txtRecommendedLocation.Text != string.Empty)
            {
                this.txtRecommendedLocation.Text = this.txtRecommendedLocation.Text.Substring(0, txtRecommendedLocation.Text.Length - 1);
            }
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = btnA;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = btnD;
            this.txtRecommendedLocation.Text += ctl.Text.ToString();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BL = new ForkliftAppBL("Query");
            OpenFormActivity();
        }
        private void OpenFormActivity()
        {
            //ForkliftAppBL act = new ForkliftAppBL("Workes");
            this.Hide();
            frmActivity frm = new frmActivity();
            frm.ShowDialog();
            frm.Focus();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmInfoMenu frm = new frmInfoMenu();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        private void CloseForm()
        {
            this.Dispose();
            frmInformation frm = new frmInformation();
            frm.ShowDialog();
        }

        private void lblInformation_Click(object sender, EventArgs e)
        {

        }

        private void lblQuery_Click(object sender, EventArgs e)
        {

        }

        private void cmbContainerTypeCode_Click(object sender, EventArgs e)
        {
            this.cmbContainerTypeCode.DroppedDown = true;
        }

        private void cmbShipingLine_Click(object sender, EventArgs e)
        {
            this.cmbShipingLine.DroppedDown = true;
        }
    }
}

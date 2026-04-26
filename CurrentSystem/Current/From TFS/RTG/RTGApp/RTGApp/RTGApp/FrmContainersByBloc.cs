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
using System;
using System.Collections.Generic;





namespace RTGApp
{
    public partial class FrmContainersByBloc : Form
    {
                    SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand();
        ConTerminalData Con1 = new ConTerminalData();
        public static bool Location = false;
        public static bool HazardousSubstances = false;
        public FrmContainersByBloc()
        {
            InitializeComponent();
            this.dataGridContainersByBloc.Visible = true;
            this.lblNoData.Visible = false;
            DataTable dt = Con1.ReturnDT("SELECT RTRIM(LocationCode)as LocationCode FROM CO_Containers WHERE (EntranceDate IS NOT NULL) AND (ExitDate IS NULL) GROUP BY LocationCode ORDER BY LocationCode");
            if (dt.Rows.Count > 0)
            {
                this.cmbLocation.ValueMember = "LocationCode";
                this.cmbLocation.DisplayMember = "LocationCode";
                this.cmbLocation.DataSource = dt;
            }
            Location = true;
            Cursor.Current = Cursors.WaitCursor;
            dt = Con1.ReturnDT("SELECT TC_HazardousSubstances.ClassificationClassCode " +
                             "FROM         TC_HazardousSubstances INNER JOIN " +
                             "CO_Containers ON TC_HazardousSubstances.UNCode = CO_Containers.UNCode1 " +
                             "WHERE     (CO_Containers.EntranceDate IS NOT NULL) AND (CO_Containers.ExitDate IS NULL) " +
                             "GROUP BY TC_HazardousSubstances.ClassificationClassCode " +
                             "ORDER BY TC_HazardousSubstances.ClassificationClassCode");
            if (dt.Rows.Count > 0)
            {
                this.cmbHazardousSubstances.ValueMember = "ClassificationClassCode";
                this.cmbHazardousSubstances.DisplayMember = "ClassificationClassCode";
                this.cmbHazardousSubstances.DataSource = dt;
            }
            Cursor.Current = Cursors.Default;
            HazardousSubstances = true;
        }

        private void cmbLoction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Location)
            {
                this.dataGridContainersByBloc.Visible = true;
                this.lblNoData.Visible = false;
                //this.cmbHazardousSubstances.SelectedIndex = -1;
                //this.cmbHazardousSubstances.Text = "";
                Cursor.Current = Cursors.WaitCursor;
                DataTable dt = Con1.ReturnDT("SELECT CO_Containers.Container AS מכולה, CO_ContainerProfile.ContainerLength AS גודל, CP_Deal.HandlingTypeCode AS סוג, " +
                                             "CO_Containers.EntranceDate AS [תאריך כניסה], TC_HazardousSubstances.ClassificationClassCode AS חומס, CO_Containers.LocationCode AS איתור " +
                                             "FROM CO_Containers INNER JOIN " +
                                             "CO_ContainerProfile ON CO_Containers.Container = CO_ContainerProfile.Container INNER JOIN " +
                                             "CP_Deal ON CO_Containers.DealNumber = CP_Deal.DealNumber LEFT OUTER JOIN " +
                                             "TC_HazardousSubstances ON CO_Containers.UNCode1 = TC_HazardousSubstances.UNCode " +
                                             "WHERE (CO_Containers.ExitDate IS NULL) AND (CO_Containers.LocationCode = '" + this.cmbLocation.Text + "') AND (CO_Containers.EntranceDate IS NOT NULL)");
                if (dt.Rows.Count > 0)
                {
                    this.dataGridContainersByBloc.DataSource = dt;
                //    this.cmbHazardousSubstances.Text = "";  //*********************************************** 
                    this.txtSumContainers.Text = " סהכ " + dt.Rows.Count.ToString() + " מכולות ";
                }
                else
                {
                    this.dataGridContainersByBloc.Visible = false;
                    this.lblNoData.Visible = true;
                    this.txtSumContainers.Text = string.Empty;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void cmbHazardousSubstances_Click(object sender, EventArgs e)
        {
            this.cmbHazardousSubstances.DroppedDown = true;
        }

        private void cmbLocation_Click(object sender, EventArgs e)
        {
            this.cmbLocation.DroppedDown = true;
        }

        private void cmbHazardousSubstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HazardousSubstances)
            {
                this.dataGridContainersByBloc.Visible = true;
                this.lblNoData.Visible = false;
                Cursor.Current = Cursors.WaitCursor;
                //this.cmbLoction.SelectedIndex = -1;
                //this.cmbLoction.Text = "";
                DataTable dt = Con1.ReturnDT("SELECT CO_Containers.Container AS מכולה, CO_ContainerProfile.ContainerLength AS גודל, CO_Containers.EntranceDate AS [תאריך כניסה], " +
                                             "CO_Containers.LocationCode AS איתור " +
                                             "FROM CO_Containers INNER JOIN " +
                                             "CO_ContainerProfile ON CO_Containers.Container = CO_ContainerProfile.Container INNER JOIN " +
                                             "TC_HazardousSubstances ON CO_Containers.UNCode1 = TC_HazardousSubstances.UNCode " +
                                             "WHERE (CO_Containers.ExitDate IS NULL) AND (TC_HazardousSubstances.ClassificationClassCode = '" + this.cmbHazardousSubstances.Text + "') " +
                                             "AND (CO_Containers.EntranceDate IS NOT NULL)");
                if (dt.Rows.Count > 0)
                {
                    this.dataGridContainersByBloc.DataSource = dt;
                    this.cmbLocation.Text = "";//***********************************************
            //        this.txtSumContainers.Text = " סהכ " + dt.Rows.Count.ToString() + " מכולות ";
                }
                else
                {
                    this.dataGridContainersByBloc.Visible = false;
                    this.lblNoData.Visible = true;
                    this.txtSumContainers.Text = string.Empty;
                }
                Cursor.Current = Cursors.Default;
            }

        }
        private void cmbLoction_Click(object sender, EventArgs e)
        {
            Location = true;
        }



        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            //this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
           // this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
           // this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
           // this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
          //  this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
          //  this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
          //  this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
          //  this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
          //  this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
         //   this.cmbLocation.Text += ctl.Text.ToString();
            this.txtLocation.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            if (this.cmbLocation.Text != string.Empty || this.txtLocation.Text != string.Empty)
            {
                this.cmbLocation.Text = string.Empty;
                this.txtLocation.Text = string.Empty;
                this.dataGridContainersByBloc.DataSource = null;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.cmbLocation.Text = this.txtLocation.Text;
            cmbLoction_SelectedIndexChanged(0,e);
            Cursor.Current = Cursors.Default;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

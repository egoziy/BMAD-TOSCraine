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

namespace RTGApp
{
    public partial class FrmInOutDiory : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand();
        ConTerminalData Con1 = new ConTerminalData();

        public FrmInOutDiory()

        {
            try { 
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = Con1.ReturnDT("SELECT  תאריך, תנועה, מכולה, גודל, סוג, טיפול, מלגזן, איתור, מלגזה " +
                                         "FROM dbo.V_RTG_App_ContainerMovement " +
                                         "ORDER BY תאריך DESC");
            if (dt.Rows.Count > 0)
            {
                this.dataGridInOutDiory.DataSource = dt;
                this.txtSumMovments.Text = " סהכ " + dt.Rows.Count.ToString() + " מכולות ";
                for (int i = 0; i < dataGridInOutDiory.Columns.Count; i++)
                {
                    if (i >= 4)
                    {
                        this.dataGridInOutDiory.Columns[i].Visible = false;
                    }
                }
            }
            else
            {
                this.dataGridInOutDiory.Visible = false;
                this.lblNoData.Visible = true;
                this.txtSumMovments.Text = string.Empty;
            }
            Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in FrmInOutDiory FrmInOutDiory : " + ex);
            }
        }

        private void cmbInOutDiory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try { 
            if (this.cmbInOutDiory.Text != string.Empty)
            {
                DataTable dt = Con1.ReturnDT("SELECT  תאריך, תנועה, מכולה, גודל, סוג, טיפול, מלגזן, איתור, מלגזה " +
                                             "FROM  dbo.V_RTG_App_ContainerMovement " +
                                             "WHERE     (תנועה = '" + this.cmbInOutDiory.Text + "') " +
                                             "ORDER BY תאריך DESC");
                if (dt.Rows.Count > 0)
                {
                    this.dataGridInOutDiory.DataSource = dt;
                    this.txtSumMovments.Text = " סהכ " + dt.Rows.Count.ToString() + " מכולות ";
                    for (int i = 0; i < dataGridInOutDiory.Columns.Count; i++)
                    {
                        if (i >= 4)
                        {
                            this.dataGridInOutDiory.Columns[i].Visible = false;
                        }
                    }
                }
                else
                {
                    this.dataGridInOutDiory.Visible = false;
                    this.lblNoData.Visible = true;
                    this.txtSumMovments.Text = string.Empty;
                }
            }
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in FrmInOutDiory cmbInOutDiory_SelectedIndexChanged : " + ex);
            }
        }

        private void cmbInOutDiory_Click(object sender, System.EventArgs e)
        {
            this.cmbInOutDiory.DroppedDown = true;
        }

        private void btnShowAllRecords_Click(object sender, System.EventArgs e)
        {
            try { 
            Cursor.Current = Cursors.WaitCursor;
           // this.dataGridInOutDiory.Columns[i].Visible = true;
            DataTable dt = Con1.ReturnDT("SELECT  תאריך, תנועה, מכולה, גודל, סוג, טיפול, מלגזן, איתור, מלגזה " +
                                         "FROM dbo.V_RTG_App_ContainerMovement " +
                                         "ORDER BY תאריך DESC");
            if (dt.Rows.Count > 0)
            {
                this.dataGridInOutDiory.DataSource = dt;
                this.txtSumMovments.Text = " סהכ " + dt.Rows.Count.ToString() + " מכולות ";
                for (int i = 0; i < dataGridInOutDiory.Columns.Count; i++)
                {
                    if (i >= 4)
                    {
                        this.dataGridInOutDiory.Columns[i].Visible = false;
                    }
                }
            }
            else
            {
                this.dataGridInOutDiory.Visible = false;
                this.lblNoData.Visible = true;
                this.txtSumMovments.Text = string.Empty;
            }
            Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in FrmInOutDiory btnShowAllRecords_Click : " + ex);
            }
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

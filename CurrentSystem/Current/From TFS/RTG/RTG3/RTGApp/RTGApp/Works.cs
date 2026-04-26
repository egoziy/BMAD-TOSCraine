using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RTGApp
{
   public partial class FrmWorks : Form
   {
      string StringBlocCode = "";
      
      public static bool cmbWorksBool = false;
      public FrmWorks()
      {
         InitializeComponent();
      }

      private void BtmRTG_Click(object sender, EventArgs e)
      {
         this.Hide();
      }

      private void FrmWorks_Load(object sender, EventArgs e)
      {
            try { 
         string WorksSql = "";
         if (FrmLogin.StrCHE.ToString() == "GOLD3")
         {
            BOND1.Visible = false;
            BOND2.Visible = false;
         }
         Cursor.Current = Cursors.WaitCursor;
         this.BOND1.BackColor = Color.LightGray;
         this.BOND2.BackColor = Color.LightGray;
         this.btnAll.BackColor = Color.LightGray;

         ConTerminalData Con3 = new ConTerminalData();

            WorksSql = "SELECT     dbo.CP_Order.Container AS מכולה, dbo.CO_ContainerProfile.ContainerLength AS [גו'], dbo.CO_ContainerProfile.ContainerTypeCode AS סוג, " +
             "  REPLACE(CONVERT(varchar, CAST(dbo.CO_Containers.NetoWeight AS money), 1), '.00', '') AS [מש'], REPLACE(dbo.CO_Containers.LocationCode, '-', '') AS [איתור], RIGHT(RTRIM(dbo.CO_Containers.LocationCode), 1)  " +
             "  + '/' + CAST(dbo.V_LocationCountRTG3.Count AS char(1)) AS [מי'], dbo.CP_Order.TargetDate AS לתאריך ," +
             "   ltrim(rtrim([EmptyLocation])) as [אז'],dbo.CP_Order.Comment AS הערות , dbo.TB_WorkType.WorkTypeDesc AS [סוג עבודה]  " +
             "  FROM         dbo.CP_Order INNER JOIN " +
             "  dbo.TB_WorkType ON dbo.CP_Order.WorkTypeCode = dbo.TB_WorkType.WorkTypeCode INNER JOIN " +
             "  dbo.CO_ContainerProfile ON dbo.CP_Order.Container = dbo.CO_ContainerProfile.Container INNER JOIN " +
             "  dbo.CO_Containers ON dbo.CP_Order.Container = dbo.CO_Containers.Container AND dbo.CP_Order.Manifest = dbo.CO_Containers.Manifest INNER JOIN " +
             "  dbo.TB_Location ON dbo.CO_Containers.LocationCode = dbo.TB_Location.LocationCode INNER JOIN " +
             "  dbo.V_LocationCountRTG3 ON LEFT(dbo.CO_Containers.LocationCode, 4) = dbo.V_LocationCountRTG3.LocationCodeL " +
             " WHERE dbo.TB_Location.BlocCode='BOND3' and (dbo.CP_Order.PerformCode = 0) AND (dbo.TB_WorkType.Gold3 = 1) AND (dbo.CO_Containers.ExitDate IS NULL) ";

       

         this.dataGridWorks.DataSource = Con3.ReturnDT(WorksSql);




         this.cboWorks.DataSource = Con3.ReturnDT("SELECT TB_WorkType.WorkTypeCode, TB_WorkType.WorkTypeDesc " +
        " FROM TB_WorkType WHERE     (TB_WorkType.gold3 = 1)");
         this.cboWorks.DisplayMember = "WorkTypeDesc";
         this.cboWorks.ValueMember = "WorkTypeCode";
         this.cboWorks.Text = string.Empty;


         cmbWorksBool = true;
         Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in Works FrmWorks_Load : " + ex);
            }
        }

      private void cboWorks_SelectedIndexChanged(object sender, EventArgs e)
      {
         string Con3Sql = "";
            try { 
         if (cmbWorksBool)
         {
            Cursor.Current = Cursors.WaitCursor;
            string st = this.cboWorks.Text;
            DataTable dt = new DataTable();
            ConTerminalData Con3 = new ConTerminalData();
          
               Con3Sql = "SELECT     dbo.CP_Order.Container AS מכולה, dbo.CO_ContainerProfile.ContainerLength AS גודל, dbo.CO_ContainerProfile.ContainerTypeCode AS סוג, " +
   "  REPLACE(CONVERT(varchar, CAST(dbo.CO_Containers.NetoWeight AS money), 1), '.00', '') AS משקל, REPLACE(dbo.CO_Containers.LocationCode, '-', '') AS איתור, RIGHT(RTRIM(dbo.CO_Containers.LocationCode), 1)  " +
   "  + '/' + CAST(dbo.V_LocationCountRTG3.Count AS char(1)) AS מיקום, dbo.CP_Order.TargetDate AS לתאריך, dbo.TB_WorkType.WorkTypeDesc AS [סוג עבודה], " +
   "  dbo.CP_Order.Comment AS הערות, dbo.CP_Order.ExaminTestType AS [סוג הבדיקה], dbo.TB_Location.BlocCode " +
   "  FROM         dbo.CP_Order INNER JOIN " +
   "  dbo.TB_WorkType ON dbo.CP_Order.WorkTypeCode = dbo.TB_WorkType.WorkTypeCode INNER JOIN " +
   "  dbo.CO_ContainerProfile ON dbo.CP_Order.Container = dbo.CO_ContainerProfile.Container INNER JOIN " +
   "  dbo.CO_Containers ON dbo.CP_Order.Container = dbo.CO_Containers.Container AND dbo.CP_Order.Manifest = dbo.CO_Containers.Manifest INNER JOIN " +
   "  dbo.TB_Location ON dbo.CO_Containers.LocationCode = dbo.TB_Location.LocationCode INNER JOIN " +
   "  dbo.V_LocationCountRTG3 ON LEFT(dbo.CO_Containers.LocationCode, 4) = dbo.V_LocationCountRTG3.LocationCodeL " +
   " WHERE  dbo.TB_Location.BlocCode = 'BOND3' and    (dbo.CP_Order.PerformCode = 0) AND (dbo.TB_WorkType.gold3 = 1) AND (dbo.CO_Containers.ExitDate IS NULL) ";
          
            dt = Con3.ReturnDT(Con3Sql);




            //   dt = (DataTable)dataGridWorks.DataSource;


            DataView dv = new DataView(dt);
            dv.RowFilter = "";
            this.dataGridWorks.DataSource = dv;

            try
            {

               switch (StringBlocCode)
               {
                  case "BOND1":
                     dv.RowFilter = "BlocCode = " + "'BOND1'" + " And " + "[סוג עבודה] like " + "'%" + st + "%'";
                     dv.Sort = "גודל,לתאריך";
                     break;
                  case "BOND2":
                     dv.RowFilter = "BlocCode = " + "'BOND2'" + " And" + "[סוג עבודה] like " + "'%" + st + "%'";
                     dv.Sort = "גודל,לתאריך";
                     break;
                  default:
                     dv.RowFilter = "[סוג עבודה] like " + "'%" + st + "%'";
                     dv.Sort = "גודל,לתאריך";
                     break;
               }


               this.dataGridWorks.DataSource = dv;
               if (this.dataGridWorks.RowCount > 1)
               {
                  this.dataGridWorks.Visible = true;
                  this.lblNoData.Visible = false;
               }
               else
               {
                  this.dataGridWorks.Visible = false;
                  this.lblNoData.Visible = true;
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;
         }
            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in Works cboWorks_SelectedIndexChanged : " + ex);
            }
        }

      private void cboWorks_Click(object sender, EventArgs e)
      {
         this.cboWorks.DroppedDown = true;
      }

     



      private void dataGridWorks_CellClick(object sender, DataGridViewCellEventArgs e)
      {
         DataTable dt = new DataTable();
         ConTerminalData Con3 = new ConTerminalData();
         dt = Con3.ReturnDT("SELECT  Comment FROM  dbo.CP_Order WHERE (Container = '" + dataGridWorks.Rows[e.RowIndex].Cells[0].Value.ToString() + "') AND (ForkliftDone = 0) AND (PerformCode = 0)");


         if (dt.Rows.Count > 0)
         {
            this.txtComment.Text = dt.Rows[0][0].ToString();
         }
         else
         {
            this.txtComment.Text = "";
         }

      }



      private void dataGridWorks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
      {
            try { 

         DataTable dt = new DataTable();
         DataTable dt1 = new DataTable();

         ConTerminalData Con4 = new ConTerminalData();




         this.Hide();

         if (dataGridWorks.Rows[e.RowIndex].Cells[5].Value.ToString().Substring(0, 1) == dataGridWorks.Rows[e.RowIndex].Cells[5].Value.ToString().Substring(2, 1))
         {


            foreach (Form f in System.Windows.Forms.Application.OpenForms)
            {
               if (f.Name == "FrmMap01")
               {
                  //  local_X = f.X;   // access value here and set in local variable

                  Control[] cl = f.Controls.Find("Container", true);
                  TextBox textBox1 = (TextBox)cl[0];
                  textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[0].Value.ToString();



                  cl = f.Controls.Find("ContainerLenghth", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[1].Value.ToString();




                  cl = f.Controls.Find("HandlingTypeCode", true);
                  textBox1 = (TextBox)cl[0];




                  dt = Con4.ReturnDT("SELECT  HandlingTypeCode FROM  dbo.CP_Order WHERE (Container = '" + dataGridWorks.Rows[e.RowIndex].Cells[0].Value.ToString() + "') AND (ForkliftDone = 0) AND (PerformCode = 0)");


                  if (dt.Rows.Count > 0)
                  {

                     textBox1.Text = dt.Rows[0][0].ToString();


                  }
                  else
                  {
                     textBox1.Text = "";
                  }








                  cl = f.Controls.Find("FromLocation", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[4].Value.ToString();



                  cl = f.Controls.Find("ContainerTarget", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "";

                  cl = f.Controls.Find("ContainerLenghthTarget", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "";

                  cl = f.Controls.Find("HandlingTypeCodeTarget", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "";


                  cl = f.Controls.Find("FromLocationTarget", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "";




                  cl = f.Controls.Find("DriverName", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "נהג גורר";


                  cl = f.Controls.Find("TruckID", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "גורר";



                  cl = f.Controls.Find("Weight", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = String.Format("{0:0,0}", dataGridWorks.Rows[e.RowIndex].Cells[3].Value.ToString());


               }
            }









         }

         else
         {

            foreach (Form f in System.Windows.Forms.Application.OpenForms)
            {
               if (f.Name == "FrmMap01")
               {





                  Control[] cl = f.Controls.Find("ContainerTarget", true);
                  TextBox textBox1 = (TextBox)cl[0];
                  textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[0].Value.ToString();
                  textBox1.BackColor = Color.LightGreen;

                  cl = f.Controls.Find("ContainerLenghthTarget", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[1].Value.ToString();
                  textBox1.BackColor = Color.LightGreen;
















                  cl = f.Controls.Find("HandlingTypeCodeTarget", true);
                  textBox1 = (TextBox)cl[0];
                  // textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[2].Value.ToString();



                  dt = Con4.ReturnDT("SELECT  HandlingTypeCode FROM  dbo.CP_Order WHERE (Container = '" + dataGridWorks.Rows[e.RowIndex].Cells[0].Value.ToString() + "') AND (ForkliftDone = 0) AND (PerformCode = 0)");
                  if (dt.Rows.Count > 0)
                  {

                     textBox1.Text = dt.Rows[0][0].ToString();


                  }
                  else
                  {
                     textBox1.Text = "";
                  }








                  textBox1.BackColor = Color.LightGreen;


                  cl = f.Controls.Find("FromLocationTarget", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dataGridWorks.Rows[e.RowIndex].Cells[4].Value.ToString();
                  textBox1.BackColor = Color.LightGreen;


                  ConTerminalData Con3 = new ConTerminalData();


                  dt1 = Con3.ReturnDT("SELECT dbo.TB_Location.Container, dbo.CO_ContainerProfile.ContainerLength, REPLACE(dbo.TB_Location.LocationCode, '-', '') AS LocationCode, dbo.CO_ContainerProfile.ContainerTypeCode FROM         dbo.TB_Location INNER JOIN   dbo.CO_ContainerProfile ON dbo.TB_Location.Container = dbo.CO_ContainerProfile.Container WHERE (REPLACE(LocationCode, '-', '') = '" + dataGridWorks.Rows[e.RowIndex].Cells[4].Value.ToString().Substring(0, 4) + Convert.ToString(Convert.ToInt32(dataGridWorks.Rows[e.RowIndex].Cells[5].Value.ToString().Substring(2, 1))) + "')");


                  cl = f.Controls.Find("Container", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dt1.Rows[0][0].ToString();

                  cl = f.Controls.Find("ContainerLenghth", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dt1.Rows[0][1].ToString();

                  cl = f.Controls.Find("HandlingTypeCode", true);
                  textBox1 = (TextBox)cl[0];
                  // textBox1.Text = dt1.Rows[0][3].ToString();





                  dt = Con4.ReturnDT("SELECT  HandlingTypeCode FROM  dbo.CP_Order WHERE (Container = '" + dataGridWorks.Rows[e.RowIndex].Cells[0].Value.ToString() + "') AND (ForkliftDone = 0) AND (PerformCode = 0)");
                  if (dt.Rows.Count > 0)
                  {

                     textBox1.Text = dt.Rows[0][0].ToString();


                  }
                  else
                  {
                     textBox1.Text = "";
                  }










                  cl = f.Controls.Find("FromLocation", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = dt1.Rows[0][2].ToString();


                  //  dt = Con3.ReturnDT("SELECT DriverName FROM     dbo.TB_Drivers WHERE   DriverID = '" + dataGridWorks.Rows[e.RowIndex].Cells[12].Value.ToString() + "'");

                  cl = f.Controls.Find("DriverName", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "נהג גורר";


                  cl = f.Controls.Find("TruckID", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = "גורר";


                  cl = f.Controls.Find("Weight", true);
                  textBox1 = (TextBox)cl[0];
                  textBox1.Text = String.Format("{0:0,0}", dataGridWorks.Rows[e.RowIndex].Cells[3].Value.ToString());


               }
            }







         }

            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in Works dataGridWorks_CellDoubleClick : " + ex);
            }

        }





   }

}

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
    public partial class frmInformation : Form
    {

        public static string ContainerString = "";
        public static string ContainerLengthString = "";
        public static string FromLocationString = "";



        public static string ContainerTargetString = "";
        public static string ContainerLengthTargetString = "";
        public static string FromLocationTargetString = "";


  

        public frmInformation()
        {
            InitializeComponent();
        }

        private void frmInformation_Load(object sender, EventArgs e)
        {

           string SqlStr="";
            FromLocationString = FrmMap01.FromLocationGString.ToString();



            if (FromLocationString != "")
            {
                if (Convert.ToInt32(FromLocationString.Substring(0, 3).ToString()) % 2 == 0)
                {
                    SqlStr = "(dbo.CO_ContainerProfile.ContainerLength= '40') AND ";
                }
                else
                {
                    SqlStr = "(dbo.CO_ContainerProfile.ContainerLength= '20') AND ";
                }
            }


            
             ConTerminalData Con3 = new ConTerminalData();
             // פריקה
    
           

             this.dataGridContaunersInList.DataSource = Con3.ReturnDT("SELECT  מכולה, גודל, סוג, משקל, קוד, קו, משאית, איתור, המתנה, UN, AVDM, EntranceDriverID FROM   dbo.V_ContainerUnloadRG ORDER BY מכולה");

            // טעינה

             this.dataGridContaunersOutList.DataSource = Con3.ReturnDT("SELECT    מכולה, גודל, סוג, משקל, קוד, קו, משאית, איתור, מיקום, המתנה, UN, AVDM, ExitDriverID , Type   FROM V_RTGLoad ORDER BY המתנה DESC");
      
            Int32 selectedRowCount = dataGridContaunersOutList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Selected=false;

                    if (Convert.ToInt32 ( dataGridContaunersOutList.Rows[i].Cells[13].Value) == 0)
                    {
                        dataGridContaunersOutList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridContaunersOutList.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }


                }
            }


        



 
             selectedRowCount = dataGridContaunersInList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Selected = false;
                }
            }


        


        }






        private void dataGridContaunersInList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 selectedRowCount = dataGridContaunersOutList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Selected=false;
                }
            }


        }




        private void dataGridContaunersOutList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 selectedRowCount = dataGridContaunersInList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Selected = false;
                }
            }


        }




        private void BtmRTG_Click(object sender, EventArgs e)
        {
            
            this.Hide();

            if (dataGridContaunersInList.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {

            foreach (Form f in System.Windows.Forms.Application.OpenForms)
            {
                if (f.Name == "FrmMap01")
                {
                    //  local_X = f.X;   // access value here and set in local variable

                   
                        Control[] cl = f.Controls.Find("Container", true);
                        TextBox textBox1 = (TextBox)cl[0];
                        textBox1.Text = dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Cells[0].Value.ToString();






                        cl = f.Controls.Find("ContainerLenghth", true);
                        textBox1 = (TextBox)cl[0];
                        textBox1.Text = dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Cells[1].Value.ToString();

                        cl = f.Controls.Find("HandlingTypeCode", true);
                        textBox1 = (TextBox)cl[0];
                        textBox1.Text = dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Cells[4].Value.ToString();


                        cl = f.Controls.Find("FromLocation", true);
                        textBox1 = (TextBox)cl[0];
                        textBox1.Text = FromLocationString.ToString();


                        ConTerminalData Con3 = new ConTerminalData();
                        DataTable dt = Con3.ReturnDT("SELECT DriverName FROM     dbo.TB_Drivers WHERE   DriverID = '" + dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Cells[11].Value.ToString() + "'");

                        cl = f.Controls.Find("DriverName", true);
                        textBox1 = (TextBox)cl[0];
                        textBox1.Text = dt.Rows[0][0].ToString();


                        cl = f.Controls.Find("TruckID", true);
                        textBox1 = (TextBox)cl[0];
                        textBox1.Text = dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Cells[6].Value.ToString();


                        cl = f.Controls.Find("Weight", true);
                        textBox1 = (TextBox)cl[0];
                        // textBox1.Text = dataGridContaunersInList.Rows[e.RowIndex].Cells[3].Value.ToString();
                        textBox1.Text = String.Format("{0:0,0}", dataGridContaunersInList.Rows[dataGridContaunersInList.SelectedRows[0].Index].Cells[3].Value.ToString());
                    }




                
            }


            }
















            if (dataGridContaunersOutList.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {




                if (dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[8].Value.ToString().Substring(0, 1) == dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[8].Value.ToString().Substring(2, 1))
                {

                    foreach (Form f in System.Windows.Forms.Application.OpenForms)
                    {
                        if (f.Name == "FrmMap01")
                        {
                            //  local_X = f.X;   // access value here and set in local variable





                            Control[] cl = f.Controls.Find("BlockName", true);
                            TextBox textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[0].Value.ToString();


                            if (dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[1].Value.ToString() == "20" & textBox1.Text == "BOND1")
                            {

                                MessageBox.Show("לא ניתן לאחסן מכולה 20 בטח זה.");
                                return;
                            }


                            cl = f.Controls.Find("Container", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[0].Value.ToString();



                            cl = f.Controls.Find("ContainerLenghth", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[1].Value.ToString();


                            cl = f.Controls.Find("HandlingTypeCode", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersInList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[4].Value.ToString();







                            cl = f.Controls.Find("FromLocation", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[7].Value.ToString();



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




                            ConTerminalData Con3 = new ConTerminalData();
                            DataTable dt = Con3.ReturnDT("SELECT DriverName FROM     dbo.TB_Drivers WHERE   DriverID = '" + dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[12].Value.ToString() + "'");

                            cl = f.Controls.Find("DriverName", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dt.Rows[0][0].ToString();


                            cl = f.Controls.Find("TruckID", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[6].Value.ToString();


                            cl = f.Controls.Find("Weight", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = String.Format("{0:0,0}", dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[3].Value.ToString());


                        }
                    }




                }

                else
                {

                    foreach (Form f in System.Windows.Forms.Application.OpenForms)
                    {
                        if (f.Name == "FrmMap01")
                        {


                            Control[] cl = f.Controls.Find("BlockName", true);
                            TextBox textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[0].Value.ToString();


                            if (dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[1].Value.ToString() == "20" & textBox1.Text == "BOND1")
                            {

                                MessageBox.Show("לא ניתן לאחסן מכולה 20 בטח זה.");
                                return;
                            }

                            cl = f.Controls.Find("ContainerTarget", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[0].Value.ToString();
                            textBox1.BackColor = Color.LightGreen;

                            cl = f.Controls.Find("ContainerLenghthTarget", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[1].Value.ToString();
                            textBox1.BackColor = Color.LightGreen;


                            cl = f.Controls.Find("HandlingTypeCodeTarget", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[4].Value.ToString();
                            textBox1.BackColor = Color.LightGreen;



                            cl = f.Controls.Find("FromLocationTarget", true);
                            textBox1 = (TextBox)cl[0];
                            textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[7].Value.ToString();
                            textBox1.BackColor = Color.LightGreen;


                            ConTerminalData Con3 = new ConTerminalData();
                          //  DataTable dt = Con3.ReturnDT("SELECT dbo.TB_Location.Container, dbo.CO_ContainerProfile.ContainerLength, REPLACE(dbo.TB_Location.LocationCode, '-', '') AS LocationCode FROM         dbo.TB_Location INNER JOIN   dbo.CO_ContainerProfile ON dbo.TB_Location.Container = dbo.CO_ContainerProfile.Container WHERE (REPLACE(LocationCode, '-', '') = '" + dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[7].Value.ToString().Substring(0, 4) + Convert.ToString(Convert.ToInt32(dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[8].Value.ToString().Substring(2, 1))) + "')");




                            DataTable   dt = Con3.ReturnDT("SELECT     dbo.TB_Location.Container, dbo.CO_ContainerProfile.ContainerLength, REPLACE(dbo.TB_Location.LocationCode, '-', '') AS LocationCode, dbo.CP_Deal.HandlingTypeCode" +
               " FROM         dbo.TB_Location INNER JOIN " +
               " dbo.CO_ContainerProfile ON dbo.TB_Location.Container = dbo.CO_ContainerProfile.Container INNER JOIN " +
               " dbo.CO_Containers ON dbo.TB_Location.Container = dbo.CO_Containers.Container INNER JOIN " +
               " dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber " +
               " WHERE     (REPLACE(dbo.TB_Location.LocationCode, '-', '') = '" + dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[7].Value.ToString().Substring(0, 4) + Convert.ToString(Convert.ToInt32(dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[8].Value.ToString().Substring(2, 1))) + "') AND (dbo.CO_Containers.EntranceDate IS NOT NULL OR  dbo.CO_Containers.RegisterDate IS NOT NULL) AND " +
               " (dbo.CO_Containers.ExitGAteDate IS NULL) ");









                            if (dt.Rows.Count > 0)
                            {



                                cl = f.Controls.Find("Container", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = dt.Rows[0][0].ToString();

                                cl = f.Controls.Find("ContainerLenghth", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = dt.Rows[0][1].ToString();


                                cl = f.Controls.Find("HandlingTypeCode", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = dt.Rows[0][3].ToString();




                                cl = f.Controls.Find("FromLocation", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = dt.Rows[0][2].ToString();



                            }




                            dt = Con3.ReturnDT("SELECT DriverName FROM     dbo.TB_Drivers WHERE   DriverID = '" + dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[12].Value.ToString() + "'");

                            if (dt.Rows.Count > 0)
                            {
                                cl = f.Controls.Find("DriverName", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = dt.Rows[0][0].ToString();


                                cl = f.Controls.Find("TruckID", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[6].Value.ToString();


                                cl = f.Controls.Find("Weight", true);
                                textBox1 = (TextBox)cl[0];
                                textBox1.Text = String.Format("{0:0,0}", dataGridContaunersOutList.Rows[dataGridContaunersOutList.SelectedRows[0].Index].Cells[3].Value.ToString());

                            }
                        }
                    }


                }



            }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        }

     
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
         

         
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void btnInformation_Click(object sender, EventArgs e)
        {
         
        
        
        
        
        }

        private void btnWorks_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

      
    
    
    
    
    
    
    
    }
}

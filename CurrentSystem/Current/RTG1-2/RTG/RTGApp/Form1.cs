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
    public partial class FrmMap01 : Form
    {

        public static String SetValueHeightStr = "";


        public static string FromLocationGString = "";


        public static string TimeByTimer ="";
        public static double SecondsByTimer;
        public static int CounterConnect = 0;
        


        
        
        bool FirstComplete = false;
        string BlocDefForApp = "";
        string BlocDef = "";



        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlCommand sqlcmd = new SqlCommand();
        DataTable TranCol;

        




        public FrmMap01()
        {
            InitializeComponent();

        }






        private void DG_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string StrLocation = string.Empty;
            ConTerminalData Con1 = new ConTerminalData();
            if (DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "" & Convert.ToInt32(e.ColumnIndex) >= 1)
            {
                DataGridViewRow GDRow = new DataGridViewRow();
                GDRow = DG.Rows[e.RowIndex];
                //  this.Container.Text = ReturnRowIndex(GDRow.Index.ToString());
                DataGridViewColumn GDColoumn = new DataGridViewColumn();
                GDColoumn = DG.Columns[e.ColumnIndex];
                StrLocation = DG.Columns[e.ColumnIndex].HeaderText.ToString() + ReturnRowIndex(GDRow.Index.ToString()) + DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                StrLocation = StrLocation.Substring(0, 3) + StrLocation.Substring(3, 1);

              


                DataTable dt2 = Con1.ReturnDT("SELECT   SUBSTRING(dbo.TB_Location.LocationCode, 5, 1) " +
            " + ' ' + dbo.CO_Containers.Container + ' ' + dbo.CO_ContainerProfile.ContainerLength + ' ' + dbo.CO_ContainerProfile.ContainerTypeCode + ' ' + dbo.CP_Deal.HandlingTypeCode " +
            " + ' ' + CAST(CAST(dbo.CO_Containers.NetoWeight AS int) AS varchar(5)) + ' ' + CASE WHEN dbo.CO_Containers.UNCode1 IS NULL " +
            " THEN '' ELSE RTRIM(dbo.CO_Containers.UNCode1) END + ' ' + LTRIM(RTRIM(dbo.TC_Client.ShortClientName)) + ' ' + CASE WHEN OrderDesc IS NULL  " +
            " THEN '' ELSE OrderDesc END AS  '" + StrLocation + "'  " +
            " FROM         dbo.TB_Location INNER JOIN  " +
            " dbo.CO_Containers ON dbo.TB_Location.Container = dbo.CO_Containers.Container INNER JOIN  " +
            " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container INNER JOIN  " +
            " dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber INNER JOIN  " +
            " dbo.TC_Client ON dbo.CP_Deal.RecieveCommisionCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN  " +
            " dbo.V_OrderForkLift ON dbo.CO_Containers.Container = dbo.V_OrderForkLift.Container AND   " +
            " dbo.CO_Containers.DealNumber = dbo.V_OrderForkLift.DealNumber  " +
            " WHERE     (LEFT(dbo.TB_Location.LocationCode, 4) =  '" + StrLocation + "') " +
            "AND (dbo.CO_Containers.EntranceDate IS NOT NULL   ) AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) " +
            " > 1 THEN exitdate ELSE exitgatedate END IS NULL) " +
            " ORDER BY dbo.TB_Location.LocationCode DESC ");










                DGC.DataSource = dt2;
                DG.Visible = false;
                DGC.Visible = true;
                btnBack.Visible = true;
            }

            else
            {

                if (BlocDef == "BOND2" & e.ColumnIndex > 17)
                {

                    DataTable dt2 = Con1.ReturnDT("SELECT  COUNT(*) AS Counter FROM   dbo.TB_Location WHERE  (BlocCode = 'BOND2') AND (LEFT(LocationCode, 3) = '" + DG.Columns[e.ColumnIndex].HeaderText.ToString() + "' OR LEFT(LocationCode, 3) = '" + DG.Columns[e.ColumnIndex].HeaderText.ToString() + "') AND (Container IS NOT NULL)");
                    if (Convert.ToInt32(dt2.Rows[0][0].ToString()) == 0)
                    {
                        if (Convert.ToInt32(DG.Columns[e.ColumnIndex].HeaderText.ToString()) % 2 == 0)
                        {
                            DG.Columns[e.ColumnIndex - 1].Visible = true;
                            DG.Columns[e.ColumnIndex].Visible = false;
                            DG.Columns[e.ColumnIndex + 1].Visible = true;



                            DG.Columns[e.ColumnIndex - 1].Width = 36;
                            DG.Columns[e.ColumnIndex + 1].Width = 36;
                            DG.Columns[e.ColumnIndex].Width = 72;



                        }

                        else
                        {

                            if (e.ColumnIndex != 57)
                            {
                                if (Convert.ToInt32(DG.Columns[e.ColumnIndex].HeaderText.ToString()) % 2 == 0)
                                {
                                    DG.Columns[e.ColumnIndex].Visible = false;
                                    DG.Columns[e.ColumnIndex + 1].Visible = true;
                                    DG.Columns[e.ColumnIndex + 2].Visible = false;
                                }
                            }

                        }

                        FirstComplete = false;
                        btnOK.Enabled = true;

                        if (BlocDef == "BOND2")
                        {
                            DG.FirstDisplayedScrollingColumnIndex = DG.ColumnCount - 1;
                        }

                        if (BlocDef == "BOND1")
                        {
                            DG.FirstDisplayedScrollingColumnIndex = 0;
                        }




                        return;

                    }
                }


            }


        }








        private void DGC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string StrLocation = string.Empty;
            ConTerminalData Con1 = new ConTerminalData();
            if (DGC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {

                if (DGC.RowCount > 1)
                {

                    StrLocation = DGC.Columns[0].HeaderText.Replace("-", "") + DGC.Rows[0].Cells[e.ColumnIndex].Value.ToString().Substring(0, 1).ToString();
                    this.FromLocation.Text = StrLocation;
                    this.Container.Text = DGC.Rows[0].Cells[e.ColumnIndex].Value.ToString().Substring(2, 11).ToString();
                    this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                    this.ContainerLenghth.Text = DGC.Rows[0].Cells[e.ColumnIndex].Value.ToString().Substring(14, 2).ToString();

                    this.HandlingTypeCode.Text = DGC.Rows[0].Cells[e.ColumnIndex].Value.ToString().Substring(20, 2).ToString();


                    if (DGC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(3, 11).ToString() != this.Container.Text)
                    {
                        StrLocation = DGC.Columns[0].HeaderText.Replace("-", "") + DGC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(0, 1).ToString();
                        this.FromLocationTarget.Text = StrLocation;
                        this.ContainerTarget.Text = DGC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(2, 11).ToString();
                        this.ContainerLenghthTarget.Text = DGC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(14, 2).ToString();
                        this.HandlingTypeCodeTarget.Text = DGC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(20, 2).ToString();

                        this.ContainerTarget.BackColor = Color.LightGreen;
                        this.ContainerLenghthTarget.BackColor = Color.LightGreen;
                        this.HandlingTypeCodeTarget.BackColor = Color.LightGreen;
                        this.FromLocationTarget.BackColor = Color.LightGreen;
                    }





                    DG.Visible = true;
                    DGC.Visible = false;
                    btnBack.Visible = false;
                    FirstComplete = true;
                }


                else
                {

                    DG.Visible = true;
                    DGC.Visible = false;
                    btnBack.Visible = false;
                    FirstComplete = true;
                }







            }


        }



























        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == -1) return;

            
            
            if (this.FromLocation.Text != "" & this.ToLocation.Text == "")
            {
                FirstComplete = true;
            }

            else
            {
                FirstComplete = false;
            }



            this.FromLocation.BackColor = Color.White;
            this.ToLocation.BackColor = Color.White;

            if (this.FromLocation.BackColor == Color.LightGreen)
            {
                return;

            }

            foreach (DataGridViewCell cell in DG.Rows[6].Cells)
            {
                if (cell.Style.BackColor == Color.LightGreen) cell.Style.BackColor = Color.LightGray;
            }

            foreach (DataGridViewCell cell in DG.Rows[7].Cells)
            {
                if (cell.Style.BackColor == Color.LightGreen) cell.Style.BackColor = Color.LightGray;
            }



            btnOK.Enabled = false;
            if (e.ColumnIndex > 0)
            {
            
            
            if (FirstComplete == false)
            {
                this.ToLocation.Text = "";

              
                    if (DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }



                    if (DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "" & Convert.ToInt32(e.ColumnIndex) >= 1)
                    {
                        DataGridViewRow GDRow = new DataGridViewRow();
                        GDRow = DG.Rows[e.RowIndex];
                        this.Container.Text = ReturnRowIndex(GDRow.Index.ToString());
                        this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                        DataGridViewColumn GDColoumn = new DataGridViewColumn();
                        GDColoumn = DG.Columns[e.ColumnIndex];
                        this.FromLocation.Text = "";
                        this.FromLocation.Text = DG.Columns[GDColoumn.Index].HeaderText.ToString() + ReturnRowIndex(GDRow.Index.ToString()) + DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                        // 271117


                        ConTerminalData Con1 = new ConTerminalData();


                        DataTable dt2 = Con1.ReturnDT("SELECT     dbo.TB_Location.Container, dbo.CO_ContainerProfile.ContainerLength, dbo.CP_Deal.HandlingTypeCode" +
                        " FROM         dbo.TB_Location INNER JOIN " +
                        " dbo.CO_ContainerProfile ON dbo.TB_Location.Container = dbo.CO_ContainerProfile.Container INNER JOIN " +
                        " dbo.CO_Containers ON dbo.TB_Location.Container = dbo.CO_Containers.Container INNER JOIN " +
                        " dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber " +
                        " WHERE     (REPLACE(dbo.TB_Location.LocationCode, '-', '') = '" + this.FromLocation.Text.Trim() + "') AND (dbo.TB_Location." + BlocDefForApp + ") AND (dbo.CO_Containers.EntranceDate IS NOT NULL) AND " +
                        " (dbo.CO_Containers.ExitGAteDate IS NULL) ");







                        if (dt2.Rows.Count > 0)
                        {
                            this.Container.Text = dt2.Rows[0][0].ToString();
                            this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                            this.ContainerLenghth.Text = dt2.Rows[0][1].ToString();
                            this.HandlingTypeCode.Text = dt2.Rows[0][2].ToString();
                        }

                        FirstComplete = true;
                    }
                    else
                    {
                        // if (DG.SelectedCells.Count != 0)
                        // {

                        if (e.RowIndex == 6 || e.RowIndex == 7)
                        {
                            DataGridViewRow GDRow = new DataGridViewRow();
                            GDRow = DG.Rows[e.RowIndex];

                            if (FromLocation.Text.Length > 3)
                            {


                                if (!(FromLocation.Text.Substring(3, 1) == "G" || FromLocation.Text.Substring(3, 1) == "T"))
                                {
                                    this.Container.Text = ReturnRowIndex(GDRow.Index.ToString());
                                    this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                                }
                                else
                                {
                                    FirstComplete = true;
                                }




                            }

                            DataGridViewColumn GDColoumn = new DataGridViewColumn();
                            GDColoumn = DG.Columns[e.ColumnIndex];
                            this.FromLocation.Text = "";


                            // (Convert.ToInt32(ReturnColumnDataTable(GDColoumn.Index.ToString(), TranCol)[0]) % 2 == 0)



                            if (Convert.ToInt32(DG.Columns[GDColoumn.Index].HeaderText.ToString()) % 2 == 0)
                            {
                                if (this.ContainerLenghth.Text == "40" || this.ContainerLenghth.Text == "")
                                {
                                    this.FromLocation.Text = DG.Columns[GDColoumn.Index].HeaderText.ToString() + ReturnRowIndex(GDRow.Index.ToString()) + DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + "1";
                                }
                            }
                            else
                            {
                                if (this.ContainerLenghth.Text == "20" || this.ContainerLenghth.Text == "")
                                {
                                    this.FromLocation.Text = DG.Columns[GDColoumn.Index].HeaderText.ToString() + ReturnRowIndex(GDRow.Index.ToString()) + DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + "1";
                                }
                            }


                            FromLocationGString = this.FromLocation.Text;

                            foreach (DataGridViewCell cell in DG.Rows[6].Cells)
                            {
                                if (cell.Style.BackColor == Color.LightGreen) cell.Style.BackColor = Color.LightGray;
                            }

                            DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightGreen;



                        }
                        else
                        {



                            if (FromLocation.Text.Length > 0)
                            {
                                if (!(FromLocation.Text.Substring(3, 1) == "G" || FromLocation.Text.Substring(3, 1) == "T"))
                                {
                                    this.FromLocation.Text = "";
                                }

                                else
                                {
                                    FirstComplete = true;
                                }


                            }










                        }



                        if (FromLocation.Text.Length > 0)
                        {
                            if (!(FromLocation.Text.Substring(3, 1) == "G" || FromLocation.Text.Substring(3, 1) == "T"))
                            {


                                this.Container.Text = "";
                                this.ContainerLenghth.Text = "";
                                this.HandlingTypeCode.Text = "";
                            }

                        }
                        else
                        {
                            this.Container.Text = "";
                            this.ContainerLenghth.Text = "";
                            this.HandlingTypeCode.Text = "";
                        }




                        this.FromLocationTarget.Text = "";
                        this.ContainerTarget.Text = "";
                        this.ContainerLenghthTarget.Text = "";
                        this.HandlingTypeCodeTarget.Text = "";


                        this.ContainerTarget.BackColor = Color.White;
                        this.ContainerLenghthTarget.BackColor = Color.White;
                        this.HandlingTypeCodeTarget.BackColor = Color.White;

                        this.FromLocationTarget.BackColor = Color.White;


                        // }
                    }

                }

                else
                {

                    if (DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }

                    if (Convert.ToInt32(e.RowIndex) <= 7 & Convert.ToInt32(e.ColumnIndex) >= 1 & DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "6")
                    {


                        string HeightStr = "";
                        DataGridViewRow GDRow = new DataGridViewRow();
                        GDRow = DG.Rows[e.RowIndex];
                        DataGridViewColumn GDColoumn = new DataGridViewColumn();
                        GDColoumn = DG.Columns[e.ColumnIndex];


                        if (DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                        {

                            HeightStr = "1";

                        }
                        else
                        {


                            if (e.ColumnIndex == 58)
                            {
                                HeightStr = Convert.ToString(Convert.ToUInt32(DG.Rows[e.RowIndex].Cells[57].Value.ToString()) + 1);
                            }

                            else
                            {


                                HeightStr = Convert.ToString(Convert.ToUInt32(DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) + 1);
                            }



                        }




                        this.ToLocation.Text = "";

                        if ((this.ContainerLenghth.Text == "40" & BlocDef == "BOND2" & e.ColumnIndex > 17 & Convert.ToInt32(DG.Columns[GDColoumn.Index].HeaderText.ToString()) % 2 > 0) || (this.ContainerLenghth.Text == "20" & BlocDef == "BOND2" & e.ColumnIndex > 17 & Convert.ToInt32(DG.Columns[GDColoumn.Index].HeaderText.ToString()) % 2 == 0))
                        {

                            this.ToLocation.Text = "";

                        }


                        else
                        {

                            this.ToLocation.Text = DG.Columns[GDColoumn.Index].HeaderText.ToString() + ReturnRowIndex(GDRow.Index.ToString()) + HeightStr;



                            if (e.RowIndex == 6 || e.RowIndex == 7)
                            {




                                DG.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightGreen;

                            }




                            if (this.Container.Text.Length > 0)
                            {

                                FirstComplete = false;
                                btnOK.Enabled = true;
                            }



                        }


                        //  DG.ClearSelection();
                    }







                }

            }


        }









        static Color ReturnColor(string instr)
        {
            Color cl = new Color();

            switch (instr)
            {

                case "0":
                    cl = System.Drawing.Color.Black;

                    break;
                  
                
                case "1":
                    cl = System.Drawing.Color.YellowGreen;

                    break;
                case "2":
                    cl = System.Drawing.Color.Blue;
                    break;
                case "3":
                    cl = System.Drawing.Color.Sienna;
                    break;

                case "4":
                    cl = System.Drawing.Color.Orange;
                    break;
                case "5":
                    cl = System.Drawing.Color.Salmon;
                    break;
                case "6":
                    cl = System.Drawing.Color.Pink;
                    break;
            
            }

            return cl;

        }



        static Color ReturnPermitColor(string Container)
        {
            Color cl = new Color();
            ConTerminalData Con3 = new ConTerminalData();


            DataTable dt5 =  Con3.ReturnDT("SELECT     dbo.CP_Order.Container  " +
          " FROM         dbo.CP_Order INNER JOIN  " +
          " dbo.TB_WorkType ON dbo.CP_Order.WorkTypeCode = dbo.TB_WorkType.WorkTypeCode INNER JOIN  " +
          " dbo.CO_Containers ON dbo.CP_Order.Container = dbo.CO_Containers.Container AND dbo.CP_Order.Manifest = dbo.CO_Containers.Manifest  " +
          " WHERE     (dbo.CP_Order.PerformCode = 0) AND (dbo.TB_WorkType.ForkLiftUse = 1) AND (dbo.CO_Containers.ExitDate IS NULL) AND   " +
          " (CAST(dbo.udf_GetNumeric(LEFT(dbo.CO_Containers.LocationCode, 3)) AS int) > 100) AND (CAST(dbo.udf_GetNumeric(LEFT(dbo.CO_Containers.LocationCode, 3)) AS int)  " +
          " < 200) AND (dbo.CP_Order.Container = '" + Container + "') OR  " +
          " (dbo.CP_Order.PerformCode = 0) AND (dbo.TB_WorkType.ForkLiftUse = 2) AND (dbo.CO_Containers.ExitDate IS NULL) AND   " +
          " (CAST(dbo.udf_GetNumeric(LEFT(dbo.CO_Containers.LocationCode, 3)) AS int) > 100) AND (CAST(dbo.udf_GetNumeric(LEFT(dbo.CO_Containers.LocationCode, 3)) AS int)  " +
          " < 200) AND (dbo.CP_Order.Container = '" + Container + "')");



            if (dt5.Rows.Count > 0)
            {

                if (dt5.Rows[0][0].ToString() != "")
                {
                    cl = System.Drawing.Color.Yellow;
                }
                else
                {
                    cl = System.Drawing.Color.White;
                }
            }
            else
            {
                cl = System.Drawing.Color.White;
            }

                        
            
            
             dt5 = Con3.ReturnDT("SELECT  RTRIM(dbo.CP_Deal.Permit) AS Permit FROM  dbo.CO_Containers INNER JOIN  dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber WHERE   (dbo.CO_Containers.Container = '" + Container + "') AND (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitDate IS NULL)");

            if (dt5.Rows.Count > 0)
            {

                if (dt5.Rows[0][0].ToString() != "")
                {
                    cl = System.Drawing.Color.YellowGreen;
                }
                else
                {
                   
                  if   (cl.Name  != "Yellow")  
                  {
                    cl = System.Drawing.Color.White;
                  }
                
                
                
                }
            }
            else
            {
                if (cl.Name != "Yellow")
                {
                    cl = System.Drawing.Color.White;
                }
            }

            
            
            return cl;

        }



        public string calcChecksum(string instr)
        {
            int Checksum = 0;
            for (int i = 0; i < instr.Length; i++)
            {
                Checksum += (int)(instr[i]);
            }

            ushort twosComp = (ushort)(~Checksum + 1);
            string h = string.Format("{0:X}", twosComp);

            return h;
        }

        

        public bool MappingSquare()
        {
            bool Succied = false;
            ConTerminalData Con = new ConTerminalData();
            DataTable dt;
            
            
            int k = 0;
            
            if (BlocDef == "BOND1")
            {
                 dt = Con.ReturnDT("SELECT BONDR, [100], [102], [104], [106], [108], [110], [112], [114], [116], [118], [120], [122], [124], [126], [128], [130], [132] FROM  dbo.V_MapRTGBond1");
            }
            else
            {
                dt = Con.ReturnDT("SELECT   BONDR, [133], [134], [135], [137], [138], [139], [141], [142], [143], [145], [146], [147], [149], [150], [151], [153], [154], [155], [157], [158], [159], [161], [162], [163], [165], [166], [167], [169], [170], [171], [173], [174], [175], [177], [178], [179], [181], [182], [183], [185]   FROM dbo.V_MapRTGBond2");
                k = 0;
            }


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    if (dt.Rows[i][j].ToString() != "0")
                    {

                        if (dt.Columns[j].ColumnName == DG.Columns[j + k].HeaderText)
                        {
                            DG.Rows[i].Cells[j + k].Value = dt.Rows[i][j].ToString();
                        }
                    
                    }
                    else
                    {
                         if (dt.Columns[j].ColumnName == DG.Columns[j + k].HeaderText)
                        {
                            DG.Rows[i].Cells[j + k].Value = "";
                        }
                      
                    }
                     
                    if (j > 0)
                    {
                        DG.Rows[i].Cells[j + k].Style.ForeColor = ReturnColor(dt.Rows[i][j].ToString());
                        DG.Rows[i].Cells[j + k].Style.BackColor = Color.White;
                    }
                
                }
            }
            


            // 13022018

            if (BlocDef == "BOND1")
            {
                DataTable dt2 = Con.ReturnDT("SELECT LEFT(dbo.CO_Containers.LocationCode, 3) AS ColumnStr, " +
                                             " SUBSTRING(dbo.CO_Containers.LocationCode, 4, 1) AS RowStr, dbo.RG_ColDG.IndexCol " +
                                             " FROM  dbo.CO_Containers INNER JOIN " +
                                             " dbo.RG_ColDG ON LEFT(dbo.CO_Containers.LocationCode, 3) = dbo.RG_ColDG.ColS INNER JOIN " +
                                             " dbo.TB_Location ON dbo.CO_Containers.LocationCode = dbo.TB_Location.LocationCode " +
                                             " WHERE(dbo.CO_Containers.EntranceDate > '01/03/2018') " +
                                             " AND (dbo.CO_Containers.ExitDate > '01/03/2018') " +
                                             " AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) > 0 THEN exitdate ELSE exitgatedate END IS NULL) " +
                                             " AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) > 0 THEN exitdate ELSE ReleaseForkliftDate END IS NULL) " +
                                             " AND (dbo.CO_Containers.WaittingTimeExitDate < 30000) " +
                                             " AND (dbo.CO_Containers.ReserveDate IS NULL) " +
                                             " AND (LEFT(dbo.CO_Containers.LocationCode, 3) = '100' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '102' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '104' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '106' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '108' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '110' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '112' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '114' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '116' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '118' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '120' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '122' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '124' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '126' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '128' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '130' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '132') " +
                                            " AND (dbo.RG_ColDG.HBBlockName = 'BOND1') " +
                                            " AND (dbo.TB_Location.BlocCode = 'BOND1') " +
                                            " GROUP BY LEFT(dbo.CO_Containers.LocationCode, 3), SUBSTRING(dbo.CO_Containers.LocationCode, 4, 1), dbo.RG_ColDG.IndexCol");




                for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                {
                    DG.Rows[Convert.ToInt32(dt2.Rows[j][1].ToString().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7").Replace("   ", "0"))].Cells[Convert.ToInt32(dt2.Rows[j][2].ToString())].Style.BackColor = Color.LightSeaGreen;
                                   
                }


            }

            
            if (BlocDef == "BOND2")
            {
                DataTable dt2 = Con.ReturnDT("SELECT LEFT(dbo.CO_Containers.LocationCode, 3) AS ColumnStr, " +
                                             " SUBSTRING(dbo.CO_Containers.LocationCode, 4, 1) AS RowStr, dbo.RG_ColDG.IndexCol " +
                                             " FROM  dbo.CO_Containers INNER JOIN " +
                                             " dbo.RG_ColDG ON LEFT(dbo.CO_Containers.LocationCode, 3) = dbo.RG_ColDG.ColS INNER JOIN " +
                                             " dbo.TB_Location ON dbo.CO_Containers.LocationCode = dbo.TB_Location.LocationCode " +
                                             " WHERE(dbo.CO_Containers.EntranceDate > '01/03/2018') " +
                                             " AND (dbo.CO_Containers.ExitDate > '01/03/2018') " +
                                             " AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) > 0 THEN exitdate ELSE exitgatedate END IS NULL) " +
                                             " AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) > 0 THEN exitdate ELSE ReleaseForkliftDate END IS NULL) " +
                                             " AND (dbo.CO_Containers.WaittingTimeExitDate < 30000) " +
                                             " AND (dbo.CO_Containers.ReserveDate IS NULL) " +
                                             " AND (LEFT(dbo.CO_Containers.LocationCode, 3) = '133' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '134' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '135' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '137' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '138' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '139' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '141' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '142' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '143' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '145' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '146' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '147' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '149' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '150' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '151' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '153' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '154' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '155' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '157' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '158' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '159' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '161' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '162' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '163' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '165' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '166' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '167' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '169' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '170' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '171' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '173' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '174' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '175' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '177' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '178' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '179' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '181' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '182' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '183' OR " +
                                                  " LEFT(dbo.CO_Containers.LocationCode, 3) = '185') " +
                                             " AND (dbo.RG_ColDG.HBBlockName = 'BOND2') " +
                                             " AND (dbo.TB_Location.BlocCode = 'BOND2') " +
                                             " GROUP BY LEFT(dbo.CO_Containers.LocationCode, 3), SUBSTRING(dbo.CO_Containers.LocationCode, 4, 1), dbo.RG_ColDG.IndexCol");



                for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                {
                    if (dt2.Rows[j][2].ToString() !="")
                    {
                        DG.Rows[Convert.ToInt32(dt2.Rows[j][1].ToString().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7").Replace("   ", "0"))].Cells[Convert.ToInt32(dt2.Rows[j][2].ToString())].Style.BackColor = Color.LightSeaGreen;
                  
                       
                    }
                   }
            }
            
            
             
            
            
            // DG.Rows[j].Cells[i].Style.ForeColor = ReturnColor(dt.Rows[0][0].ToString());


            Succied = true;
            return Succied;

        }





        public bool MappingError()
        {
            bool Succied = false;
            ConTerminalData Con = new ConTerminalData();
            DataTable dt;
            int k = 0;
            int l = 0;


            if (BlocDef == "BOND1")
            {
                dt = Con.ReturnDT("SELECT   BONDR, [100], [102], [104], [106], [108], [110], [112], [114], [116], [118], [120], [122], [124], [126], [128], [130], [132] FROM  dbo.V_MapRTGBond1");
            }
            else
            {
                dt = Con.ReturnDT("SELECT    [133], [134], [135], [137], [138], [139], [141], [142], [143], [145], [146], [147], [149], [150], [151], [153], [154], [155], [157], [158], [159], [161], [162], [163], [165], [166], [167], [169], [170], [171], [173], [174], [175], [177], [178], [179], [181], [182], [183], [185] , BONDR    FROM dbo.V_MapRTGBond2");
               // k = 18;
               // l = 40;
            }


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 1; j <= dt.Columns.Count - 1; j++)
                {
 
              
                     DataTable dt2 = Con.ReturnDT("SELECT   COUNT(dbo.CO_Containers.Container) AS Counter " +
                   " FROM     dbo.TB_Location INNER JOIN " +
                   " dbo.CO_Containers ON dbo.TB_Location.Container = dbo.CO_Containers.Container INNER JOIN " +
                   " dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber LEFT OUTER JOIN " +
                   " dbo.V_OrderForkLift ON dbo.CO_Containers.Container = dbo.V_OrderForkLift.Container AND  " +
                   " dbo.CO_Containers.DealNumber = dbo.V_OrderForkLift.DealNumber " +
                   " WHERE     (LEFT(dbo.TB_Location.LocationCode, 4) = '" + DG.Columns[j + k].HeaderText + dt.Rows[i][l].ToString() + "') AND (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) " +
                    "  > 1 THEN exitdate ELSE exitgatedate END IS NULL) ");



                     if (dt.Rows[i][j].ToString() != dt2.Rows[0][0].ToString())
                     {

                         if (j != 40)
                         {
                             Succied = true;
                             DG.Rows[i].Cells[j + k].Style.BackColor = Color.Red;

                         }
                     }
                }
            }

           

            return Succied;

        }


















        static string ReturnRowIndex(string instr)
        {
            string ReturnRowIndexStr = "";

            switch (instr)
            {
                case "0":
                    ReturnRowIndexStr = "A";

                    break;
                case "1":
                    ReturnRowIndexStr = "B";
                    break;
                case "2":
                    ReturnRowIndexStr = "C";
                    break;

                case "3":
                    ReturnRowIndexStr = "D";
                    break;
                case "4":
                    ReturnRowIndexStr = "E";
                    break;
                case "5":
                    ReturnRowIndexStr = "F";
                    break;
                case "6":
                    ReturnRowIndexStr = "G";
                    break;

                case "7":
                    ReturnRowIndexStr = "T";
                    break;

            }

            return ReturnRowIndexStr;

        }


        static string ReturnColoumIndex(string instr)
        {
            string ReturnColoumIndexStr = "";

            switch (instr)
            {
                case "0":
                    ReturnColoumIndexStr = "A";

                    break;
                case "1":
                    ReturnColoumIndexStr = "B";
                    break;
                case "2":
                    ReturnColoumIndexStr = "C";
                    break;

                case "3":
                    ReturnColoumIndexStr = "D";
                    break;
                case "4":
                    ReturnColoumIndexStr = "E";
                    break;
                case "5":
                    ReturnColoumIndexStr = "F";
                    break;
                case "6":
                    ReturnColoumIndexStr = "G";
                    break;
                case "7":
                    ReturnColoumIndexStr = "T";
                    break;

            }

            return ReturnColoumIndexStr;

        }






        static string ReturnRowIndexU(string instr)
        {
            string ReturnRowIndexStr = "";

            switch (instr)
            {
                case "A":
                    ReturnRowIndexStr = "0";

                    break;
                case "B":
                    ReturnRowIndexStr = "1";
                    break;
                case "C":
                    ReturnRowIndexStr = "2";
                    break;

                case "D":
                    ReturnRowIndexStr = "3";
                    break;
                case "E":
                    ReturnRowIndexStr = "4";
                    break;
                case "F":
                    ReturnRowIndexStr = "5";
                    break;
                case "G":
                    ReturnRowIndexStr = "6";
                    break;

                case "T":
                    ReturnRowIndexStr = "7";
                    break;

            }

            return ReturnRowIndexStr;

        }













       






        private void FrmMap01_Load(object sender, EventArgs e)
        {


            CHE.Text = FrmLogin.StrCHE.ToString();
            BlockName.Text = FrmLogin.StrBlockName.ToString();
            OperatorID.Text = FrmLogin.StrOperatorID.ToString();

            ConTerminalData Con3 = new ConTerminalData();


            DataTable dt5 = Con3.ReturnDT("SELECT  HBBlockName FROM   dbo.RG_A1 where CHE = '" + this.CHE.Text + "'");
            BlocDef = this.BlockName.Text;     // dt5.Rows[0][0].ToString().Trim();
            //  var cellRectangle = DG.GetCellDisplayRectangle(Convert.ToInt32(ReturnIndexDataTable(dt5.Rows[0][3].ToString(), TranCol)), Convert.ToInt32(dt5.Rows[0][4].ToString().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6")), true);
            BlocDefForApp = " BlocCode = '" + BlocDef + "'";

            ConTerminalData Con1 = new ConTerminalData();

                 if (BlocDef == "BOND2")
            {
             

                DG.Columns.Add("", "BOND2");
                DG.Columns.Add("", "133");
                DG.Columns.Add("", "134");
                DG.Columns.Add("", "135");
                DG.Columns.Add("", "137");
                DG.Columns.Add("", "138");
                DG.Columns.Add("", "139");
                DG.Columns.Add("", "141");
                DG.Columns.Add("", "142");
                DG.Columns.Add("", "143");
                DG.Columns.Add("", "145");
                DG.Columns.Add("", "146");
                DG.Columns.Add("", "147");
                DG.Columns.Add("", "149");
                DG.Columns.Add("", "150");
                DG.Columns.Add("", "151");
                DG.Columns.Add("", "153");
                DG.Columns.Add("", "154");
                DG.Columns.Add("", "155");
                DG.Columns.Add("", "157");
                DG.Columns.Add("", "158");
                DG.Columns.Add("", "159");
                DG.Columns.Add("", "161");
                DG.Columns.Add("", "162");
                DG.Columns.Add("", "163");
                DG.Columns.Add("", "165");
                DG.Columns.Add("", "166");
                DG.Columns.Add("", "167");
                DG.Columns.Add("", "169");
                DG.Columns.Add("", "170");
                DG.Columns.Add("", "171");
                DG.Columns.Add("", "173");
                DG.Columns.Add("", "174");
                DG.Columns.Add("", "175");
                DG.Columns.Add("", "177");
                DG.Columns.Add("", "178");
                DG.Columns.Add("", "179");
                DG.Columns.Add("", "181");
                DG.Columns.Add("", "182");
                DG.Columns.Add("", "183");
                DG.Columns.Add("", "185");
            
             
            }

            if (BlocDef == "BOND1")
            {
               // DG.FirstDisplayedScrollingColumnIndex = 0;

                DG.Columns.Add("", "BOND1");
                DG.Columns.Add("", "100");
                DG.Columns.Add("", "102");
                DG.Columns.Add("", "104");
                DG.Columns.Add("", "106");
                DG.Columns.Add("", "108");
                DG.Columns.Add("", "110");
                DG.Columns.Add("", "112");
                DG.Columns.Add("", "114");
                DG.Columns.Add("", "116");
                DG.Columns.Add("", "118");
                DG.Columns.Add("", "120");
                DG.Columns.Add("", "122");
                DG.Columns.Add("", "124");
                DG.Columns.Add("", "126");
                DG.Columns.Add("", "128");
                DG.Columns.Add("", "130");
                DG.Columns.Add("", "132");
            
             
            }



            DG.Rows.Add("A");
            DG.Rows.Add("B");
            DG.Rows.Add("C");
            DG.Rows.Add("D");
            DG.Rows.Add("E");
            DG.Rows.Add("F");
            DG.Rows.Add("G");
            DG.Rows.Add("T");

            //    DG.Rows[1].Cells[1].Style.BackColor = Color.PaleVioletRed;

            DG.Columns[0].Width = 72;
            for (int i = 1; i < DG.Columns.Count ; i++)
            {

                if (BlocDef == "BOND2")
                {

                     if (i == 40)
                    {
                        DG.Columns[i].Width = 70;
                        DG.Columns[i].Visible = true;

                    }

                    else
                    {

                        DataTable dt2 = Con1.ReturnDT("SELECT     COUNT(*) AS Counter FROM  dbo.TB_Location INNER JOIN       dbo.CO_ContainerProfile ON dbo.TB_Location.Container = dbo.CO_ContainerProfile.Container WHERE     (dbo.TB_Location.BlocCode = 'BOND2') AND (LEFT(dbo.TB_Location.LocationCode, 3) = '" + DG.Columns[i].HeaderText.ToString() + "' OR LEFT(dbo.TB_Location.LocationCode, 3) = '" + DG.Columns[i + 2].HeaderText.ToString() + "') AND (dbo.TB_Location.Container IS NOT NULL) AND     (dbo.CO_ContainerProfile.ContainerLength = '20')");
                        if (Convert.ToInt32(dt2.Rows[0][0].ToString()) == 0)    // אין מכולות על השורות
                        {

                            DG.Columns[i].Visible = false;
                            DG.Columns[i + 2].Visible = false;
                            DG.Columns[i + 1].Visible = true;
                            DG.Columns[i + 1].Width = 86;
                        }

                        else
                        {

                            DG.Columns[i].Width = 43;
                            DG.Columns[i + 2].Width = 43;
                            DG.Columns[i].Width = 86;
                            DG.Columns[i].Visible = true;
                            DG.Columns[i + 2].Visible = true;
                            DG.Columns[i + 1].Visible = false;


                        }

  
                    }

                    i = i + 2;
                }
                else
                {
                    DG.Columns[i].Width = 70;
                   

                }

                 

            }

          

            DG.Rows[0].Height = 60;
            DG.Rows[1].Height = 60;
            DG.Rows[2].Height = 60;
            DG.Rows[3].Height = 60;
            DG.Rows[4].Height = 60;
            DG.Rows[5].Height = 60;
            DG.Rows[6].Height = 60;
            DG.Rows[7].Height = 60;
            DG.Rows[8].Height = 60;



            //   Mapping the Data  
            MappingSquare();


            DG.MultiSelect = false;

        }




        private void btnOK_Click(object sender, EventArgs e)
        {

            string MessageStr = "";
            string TruckTypeStr = "";

            if (this.TruckID.Text == "גורר")
            {
                TruckTypeStr = "גורר";
            }

            else
            {
                TruckTypeStr = "משאית";
            }



            Byte[] bytes = new Byte[256];
            ConTerminalData Con3 = new ConTerminalData();


            DataTable dt5 = Con3.ReturnDT("SELECT     MAX(CounterID) AS CounterID FROM dbo.RG_B3 WHERE CHE = '" + this.CHE.Text + "'");
            if (dt5.Rows[0][0].ToString() == "255")
            {
                dt5 = Con3.ReturnDT("DELETE dbo.RG_B3  WHERE CHE = '" + this.CHE.Text + "'");
                dt5 = Con3.ReturnDT("INSERT RG_B3 (CounterID,Counter,CHE) values(1,0,'" + this.CHE.Text + "')");
            }



            dt5 = Con3.ReturnDT("SELECT MAX(CounterID + 1) AS CounterID  FROM dbo.RG_B3 WHERE  CHE = '" + this.CHE.Text + "'");
            this.Counter.Text = dt5.Rows[0][0].ToString();

            dt5 = Con3.ReturnDT("INSERT RG_B3 (CounterID,Counter, Time, CHE, ContID1, LiftBlockName, LiftBayNumber, LiftRowNumber, LiftHeight, PlaceBlockName, PlaceBayNumber, PlaceRowNumber, PlaceHeight, Len,OperatorID,TruckType)" +
                       "  SELECT TOP 1 " + this.Counter.Text + ",  dbo.IntToHex(" + this.Counter.Text + ") AS Counter, RIGHT(CONVERT(char(4), DATEPART(yyyy, DATEADD(mi, 5, GETDATE()))), 4) + CASE len(datepart(mm, DATEADD(mi, 5, " +
                       " GetDate()))) WHEN 1 THEN '0' ELSE '' END + CONVERT(varchar(2), DATEPART(mm, DATEADD(mi, 5, GETDATE()))) + CASE len(datepart(dd, DATEADD(mi, 5, GetDate()))) " +
                       " WHEN 1 THEN '0' ELSE '' END + CONVERT(varchar(2), DATEPART(dd, DATEADD(mi, 5, GETDATE()))) + CASE len(datepart(hh, DATEADD(mi, 5, GetDate()))) " +
                       " WHEN 1 THEN '0' ELSE '' END + CONVERT(varchar(2), DATEPART(hh, DATEADD(mi, 5, GETDATE()))) + CASE len(datepart(mi, DATEADD(mi, 5, GetDate()))) " +
                       " WHEN 1 THEN '0' ELSE '' END + CONVERT(varchar(2), DATEPART(mi, DATEADD(mi, 5, GETDATE()))) + CASE len(datepart(ss, DATEADD(ss, 5, GetDate()))) " +
                       " WHEN 1 THEN '0' ELSE '' END + CONVERT(varchar(2), DATEPART(ss, DATEADD(ss, 5, GETDATE())))   AS Time, '" + this.CHE.Text + "' AS CHE,'" + this.Container.Text + "'," +
                       " '" + BlocDef + "   ','" + this.FromLocation.Text.Substring(0, 3).ToString() + "','" + this.FromLocation.Text.Substring(3, 1).ToString() + "  ','" + this.FromLocation.Text.Substring(4, 1).ToString() + "   '," +
                       " '" + BlocDef + "   ','" + this.ToLocation.Text.Substring(0, 3).ToString() + "','" + this.ToLocation.Text.Substring(3, 1).ToString() + "  ','" + this.ToLocation.Text.Substring(4, 1).ToString() + "   ','" +
                       this.ContainerLenghth.Text.ToString() + "','" + this.OperatorID.Text + "','" + TruckTypeStr + "'" +
                       " FROM  dbo.RG_B3 ");

            dt5 = Con3.ReturnDT("SELECT MAX(CounterID) AS CounterID  FROM dbo.RG_B3 WHERE  CHE = '" + this.CHE.Text + "'");
            this.Counter.Text = dt5.Rows[0][0].ToString();


            dt5 = Con3.ReturnDT("SELECT PreMessage  FROM dbo.RG_B3  WHERE   (CounterID = " + this.Counter.Text + ") AND  CHE = '" + this.CHE.Text + "'");
            MessageStr = "FFFF" + ReturnAsciText(dt5.Rows[0][0].ToString() + calcChecksum(dt5.Rows[0][0].ToString()));
            dt5 = Con3.ReturnDT("UPDATE RG_B3  set   Message ='" + MessageStr + "' WHERE CounterID =  (SELECT MAX(CounterID) AS Expr1  FROM  dbo.RG_B3 AS RG_B3_1 WHERE CHE = '" + this.CHE.Text + "') AND  CHE = '" + this.CHE.Text + "'");

            System.Threading.Thread.Sleep(2000);

            dt5 = Con3.ReturnDT("SELECT  COUNT(*) AS Counter FROM dbo.RG_B3 WHERE  CounterID =  " + this.Counter.Text + " AND (PickDate IS NULL) AND (PlaceDate IS NULL) AND  CHE = '" + this.CHE.Text + "'");

            if (Convert.ToInt32(dt5.Rows[0][0].ToString()) == 1)
            {
                this.FromLocation.BackColor = Color.LightGreen;
                this.ToLocation.BackColor = Color.White;
            }

            else
            {
                this.FromLocation.BackColor = Color.White;
                this.ToLocation.BackColor = Color.White;
            }


            if (this.FromLocation.Text.Substring(3, 1) == "G" || this.FromLocation.Text.Substring(3, 1) == "T")
            {
                dt5 = Con3.ReturnDT("INSERT  dbo.RG_Container(Container,CHE,OperatorID ) Values('" + this.Container.Text + "','" + this.CHE.Text + "','" + this.OperatorID.Text + "')");
            }



            btnOK.Enabled = false;
            // btmCancel.Enabled = true;
            DG.ClearSelection();
            DG.Enabled = false;

            this.Container.Enabled = false;
            this.ContainerLenghth.Enabled = false;
            this.HandlingTypeCode.Enabled = false;
            this.FromLocation.Enabled = false;
            this.ToLocation.Enabled = false;




        }


        public static byte[] ToByteArray(String hexString)
        {
            byte[] retval = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
                retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            return retval;
        }

        static string ReturnIndexDataTable(String StringCol, DataTable DT)
        {
            string StringIndex = "";
            for (int i = 0; i < DT.Rows.Count - 1; i++)
            {

                if (StringCol.Length == 3)
                {
                    if (DT.Rows[i][0].ToString() == StringCol.Substring(0, 3))
                    {
                        StringIndex = DT.Rows[i][1].ToString();
                        return StringIndex;
                    }
                }
            }

            return StringIndex;
        }



        static string[] ReturnColumnDataTable(String StringIndex, DataTable DT)
        {
            string[] StringCol = new string[4];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i][1].ToString() == StringIndex.Trim())
                {
                    StringCol[0] = DT.Rows[i][0].ToString();
                    StringCol[1] = DT.Rows[i][1].ToString();
                    StringCol[2] = DT.Rows[i][2].ToString();
                    StringCol[3] = DT.Rows[i][3].ToString();
                    return StringCol;
                }

            }

            return StringCol;
        }



        static string ReturnAsciText(string instr)
        {
            string str = instr;
            char[] array = str.ToCharArray();
            string final = "";
            foreach (var i in array)
            {
                string hex = String.Format("{0:X}", Convert.ToInt32(i));
                final += hex;

                //final += hex.Insert(0, "0X") + " ";


            }
            final = final.TrimEnd();
            Console.WriteLine(final);

            return final;

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DG.Visible = true;
            DGC.Visible = false;
            btnBack.Visible = false;
        }

        public static int Abs(int value)
        {
            if (value >= 0)
            {
                return value;
            }
            return AbsHelper(value);
        }
        private static int AbsHelper(int value)
        {
            
            return -value;
        }

        private void TimerCHE_Tick_1(object sender, EventArgs e)
        {
        
          


            ConTerminalData Con3 = new ConTerminalData();
            DGH.DataSource = Con3.ReturnDT("SELECT Time, Status, HBBlockName, RTRIM(HBBayNumber) + RTRIM(HBRowNumber) +  RTRIM(HBHeight) AS Position, CraneStatus, GPSStatus, CHE, PLC, Len, TwistLock FROM   dbo.RG_A1  WHERE CHE = '" + this.CHE.Text + "'");

            DGH.Columns[0].Width = 60;
            DGH.Columns[1].Width = 60;
            DGH.Columns[2].Width = 60;
            DGH.Columns[3].Width = 60;
            DGH.Columns[4].Width = 60;
            DGH.Columns[5].Width = 60;
            DGH.Columns[6].Width = 60;
            DGH.Columns[7].Width = 60;
            DGH.Columns[8].Width = 60;
            DGH.Columns[9].Width = 60;





            // place trigger


            //string time = DateTime.Now.ToString("HHmmss").Substring(0, 2) + ":" + DateTime.Now.ToString("HHmmss").Substring(2, 2) + ":" + DateTime.Now.ToString("HHmmss").Substring(4, 2); // "00:01:05";
            //double seconds = TimeSpan.Parse(time).TotalSeconds;  //
          //   time = DGH.Rows[0].Cells[0].Value.ToString().Substring(0, 2) + ":" + DGH.Rows[0].Cells[0].Value.ToString().Substring(2, 2) + ":" + DGH.Rows[0].Cells[0].Value.ToString().Substring(4, 2); // "00:01:05";

            if (TimeByTimer == "")
            {
               TimeByTimer= DateTime.Now.ToString("HHmmss").Substring(0, 2) + ":" + DateTime.Now.ToString("HHmmss").Substring(2, 2) + ":" + DateTime.Now.ToString("HHmmss").Substring(4, 2);
            }
            
            SecondsByTimer = TimeSpan.Parse(TimeByTimer).TotalSeconds;
            
             TimeByTimer= DGH.Rows[0].Cells[0].Value.ToString().Substring(0, 2) + ":" + DGH.Rows[0].Cells[0].Value.ToString().Substring(2, 2) + ":" + DGH.Rows[0].Cells[0].Value.ToString().Substring(4, 2); // "00:01:05";
            
           








             SecondsByTimer =  Abs(Convert.ToInt32(SecondsByTimer - TimeSpan.Parse(TimeByTimer).TotalSeconds));
            this.EnconsoleButtom.Enabled = false;
            if (SecondsByTimer == 0.0)     // || SecondsByTimer == 0.0 & CounterConnect == 0) 
            {
                CounterConnect++;
               if (CounterConnect > 90)
                {
                    this.EnconsoleButtom.Enabled = true;

                    // this.EnconsoleButtom.Image = RTGApp.Properties.Resources.Play;
                    //this.groupBox2.Visible = true;
                    return;

                }
            }

            else
            {
               // if (CounterConnect > 90)
              //  {

                    CounterConnect = 0;
                    this.EnconsoleButtom.Enabled = false;
              //  }
                 
                 
                 }



            DataTable dt5;




            if (this.CHE.Text == "GOLD1")
            {
                dt5 = Con3.ReturnDT("SELECT  ContainerPick1 FROM   dbo.TB_Parameters");
            }
            else
            {
                dt5 = Con3.ReturnDT("SELECT  ContainerPick2 FROM   dbo.TB_Parameters");
            }



            if (dt5.Rows[0][0].ToString() == "True")
            {

                this.ContainerPick.Visible = true;
                return;
            }




            if (this.CHE.Text == "GOLD1")
            {
                if (this.BlockName.Text == "BOND1")
                {
                    dt5 = Con3.ReturnDT("SELECT     RefreshMapRTG FROM   dbo.TB_Parameters");
                }
                else
                {
                    dt5 = Con3.ReturnDT("SELECT     RefreshMapRTG2 FROM   dbo.TB_Parameters");
                }
            }

            else
            {
                if (this.BlockName.Text == "BOND1")
                {
                    dt5 = Con3.ReturnDT("SELECT     RefreshMapRTG3 FROM   dbo.TB_Parameters");
                }
                else
                {
                    dt5 = Con3.ReturnDT("SELECT     RefreshMapRTG4 FROM   dbo.TB_Parameters");
                }
            }





            if (dt5.Rows[0][0].ToString() == "True")
            {
                MappingSquare();

                if (this.CHE.Text == "GOLD1")
                {


                    if (this.BlockName.Text == "BOND1")
                    {
                        dt5 = Con3.ReturnDT("UPDATE  dbo.TB_Parameters SET  RefreshMapRTG ='FALSE'");
                    }
                    else
                    {
                        dt5 = Con3.ReturnDT("UPDATE  dbo.TB_Parameters SET  RefreshMapRTG2 ='FALSE'");
                    }

                }

                else
                {


                    if (this.BlockName.Text == "BOND1")
                    {
                        dt5 = Con3.ReturnDT("UPDATE  dbo.TB_Parameters SET  RefreshMapRTG3 ='FALSE'");
                    }
                    else
                    {
                        dt5 = Con3.ReturnDT("UPDATE  dbo.TB_Parameters SET  RefreshMapRTG4 ='FALSE'");
                    }

                }









                // 211217
                if (this.ContainerTarget.Text.Length == 0)
                {

                    //  btmCancel_Click(0, e);

                    this.Container.Text = "";
                    this.ContainerLenghth.Text = "";
                    this.HandlingTypeCode.Text = "";
                    this.FromLocation.Text = "";
                    this.ToLocation.Text = "";
                    this.Counter.Text = "";

                    // 260418
                    this.DriverName.Text = "";
                    this.TruckID.Text = "";
                    this.Weight.Text = "";
                    // 260418

                    this.Container.Enabled = true;
                    this.ContainerLenghth.Enabled = true;
                    this.HandlingTypeCode.Enabled = true;
                    this.FromLocation.Enabled = true;
                    this.ToLocation.Enabled = true;
                    this.Container.BackColor = Color.White;
                    this.ContainerLenghth.BackColor = Color.White;
                    this.HandlingTypeCode.BackColor = Color.White;
                    this.FromLocation.BackColor = Color.White;
                    this.ToLocation.BackColor = Color.White;
                    this.btnOK.Enabled = false;
                    DG.Enabled = true;

                }

            }


            ConTerminalData Con = new ConTerminalData();
            DataTable dt;


            //DataTable TranCol = Con.ReturnDT("SELECT ColS, ColD,ColS1,ColS2 , CASE WHEN ColD > ColS1 THEN CASE WHEN ColD > ColS2 THEN ColD ELSE ColS2 END ELSE CASE WHEN ColS1 > ColS2 THEN ColS1 ELSE ColS2 END END  as NewRec FROM  RG_ColDG");




            if (POS1.Text != DGH.Rows[0].Cells[3].Value.ToString().Trim() || (btnOK.Enabled == false & this.ToLocation.Text.Trim() != ""))
            {



                //if (POS1.Text.Length > 0)
                //{

                //    if (POS1.Text.Length == 5)
                //    {
                //        DG.Rows[Convert.ToInt32(POS1.Text.Substring(3, 1).ToString().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7"))].Cells[Convert.ToInt32(ReturnIndexDataTable(POS1.Text.Substring(0, 3).ToString(), TranCol))].Style.BackColor = Color.White;
                //    }

                //}



             //   dt5 = Con3.ReturnDT("SELECT Time, Status, HBBlockName, HBBayNumber, HBRowNumber, HBHeight, CraneStatus, GPSStatus, CHE, PLC, Len, TwistLock FROM   dbo.RG_A1 WHERE CHE = '" + this.CHE.Text + "'");




             dt5 = Con3.ReturnDT("SELECT     dbo.RG_A1.Time, dbo.RG_A1.Status, dbo.RG_A1.HBBlockName, dbo.RG_A1.HBBayNumber, dbo.RG_A1.HBRowNumber, dbo.RG_A1.HBHeight, " +
           " dbo.RG_A1.CraneStatus, dbo.RG_A1.GPSStatus, dbo.RG_A1.CHE, dbo.RG_A1.PLC, dbo.RG_A1.Len, dbo.RG_A1.TwistLock, dbo.RG_ColDG.IndexCol " +
           " FROM         dbo.RG_A1 INNER JOIN  " +
           " dbo.RG_ColDG ON dbo.RG_A1.HBBlockName = dbo.RG_ColDG.HBBlockName AND dbo.RG_A1.HBBayNumber = dbo.RG_ColDG.ColS  " +
           " WHERE  dbo.RG_A1.CHE = '" + this.CHE.Text + "'");










                if (dt5.Rows[0][10].ToString() == "20")
                {
                    POS1.Width = 15;
                    POS1.Height = 30;
                }
                else
                {
                    POS1.Width = 30;
                    POS1.Height = 60;
                }



                if (dt5.Rows[0][11].ToString() == "O")
                {
                    POS1.BackColor = Color.Green;
                }
                else
                {
                    POS1.BackColor = Color.Red;
                }


                if (DGH.Rows[0].Cells[3].Value.ToString().Trim().Length > 0)
                {


                    if (DG.Columns[0].HeaderText == dt5.Rows[0][2].ToString().Trim())
                    {

                        POS1.Text = DGH.Rows[0].Cells[3].Value.ToString().Trim().Substring(4, 1);

                        if (POS1.Text == "7" & POS1.BackColor == Color.Red)
                        {
                            if (POS1.Visible == true)
                            {
                                POS1.Visible = false;
                                POS1.Width = 30;
                                POS1.Height = 60;
                            }
                            else
                            {
                                POS1.Visible = true;
                                POS1.Width = 60;
                                POS1.Height = 90;
                            }

                        }
                        else
                        {
                            POS1.Visible = true;
                            POS1.Width = 30;
                            POS1.Height = 60;
                        }
                    }
                

                    else
                    {
                        POS1.Visible = true;

                        if (DG.Columns[0].HeaderText == "BOND1")
                        {
                            POS1.Text = "החלף ערוגה>>";
                        }
                        else
                        {
                            POS1.Left = 1180;

                            POS1.Text = "<<החלף ערוגה";
                        }



                        POS1.Width = 140;
                        POS1.Height = 60;
                        POS1.BackColor = Color.Orange;

                       
                    }









                
                }







                if (dt5.Rows[0][3].ToString().Trim().Length == 3)
                {

                  // 18/03/2018
                    SetValueHeightStr = dt5.Rows[0][3].ToString().Trim();
                    
                    if (dt5.Rows[0][12].ToString().Trim() !="")
                    {

                        if (DG.Columns[0].HeaderText == dt5.Rows[0][2].ToString().Trim())
                        //   31052018
                        {
                            var cellRectangle = DG.GetCellDisplayRectangle(Convert.ToInt32(dt5.Rows[0][12].ToString()), Convert.ToInt32(dt5.Rows[0][4].ToString().Trim().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7").Replace("   ", "0")), true);

                            POS1.Left = cellRectangle.Left + 10;
                            POS1.Top = cellRectangle.Top + 25; //35
                        }
                    
                    }
                }

                if (dt5.Rows[0][4].ToString() == "C")
                {

                    DG.Rows[Convert.ToInt32(dt5.Rows[0][4].ToString().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7").Replace("   ", "0"))].Cells[Convert.ToInt32(dt5.Rows[0][12].ToString())].Style.BackColor = Color.White;

                }

                else
                {
                    if (dt5.Rows[0][3].ToString().Trim().Length == 3)
                    {

                        if (dt5.Rows[0][12].ToString().Trim()  !="")
                        {

                            if (DG.Columns[0].HeaderText == dt5.Rows[0][2].ToString().Trim())
                            {


                                DG.Rows[Convert.ToInt32(dt5.Rows[0][4].ToString().Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7").Replace("   ", "0"))].Cells[Convert.ToInt32(dt5.Rows[0][12].ToString())].Style.BackColor = Color.White;
                            }
                        }
                    }
                }












                if (this.Counter.Text != "")
                {

                    dt5 = Con3.ReturnDT("SELECT  COUNT(*) AS Counter FROM dbo.RG_B3 WHERE  CounterID = " + this.Counter.Text + " AND (FinishDate IS NULL) AND CHE = '" + this.CHE.Text + "'");
                    if (Convert.ToInt32(dt5.Rows[0][0].ToString()) == 1)
                    {


                        dt5 = Con3.ReturnDT("SELECT  COUNT(*) AS Counter FROM dbo.RG_B3 WHERE  CounterID = " + this.Counter.Text + " AND (PickDate IS NOT NULL) AND (PlaceDate IS NULL) AND CHE = '" + this.CHE.Text + "'");

                        if (Convert.ToInt32(dt5.Rows[0][0].ToString()) == 1)
                        {
                            this.FromLocation.BackColor = Color.White;
                            this.ToLocation.BackColor = Color.LightGreen;
                        }

                        else
                        {



                            this.FromLocation.BackColor = Color.LightGreen;
                            this.ToLocation.BackColor = Color.White;

                            dt5 = Con3.ReturnDT("SELECT  COUNT(*) AS Counter FROM dbo.RG_B3 WHERE  CounterID =  " + this.Counter.Text + " AND (PickDate IS NOT NULL) AND (PlaceDate IS NOT NULL) AND ( FinishDate IS NULL)  AND CHE = '" + this.CHE.Text + "'");
                            if (Convert.ToInt32(dt5.Rows[0][0].ToString()) == 1)
                            {
                                dt5 = Con3.ReturnDT("UPDATE RG_B3 set FinishDate= GetDate()  WHERE CounterID =   " + this.Counter.Text + " AND CHE = '" + this.CHE.Text + "'");
                                this.Counter.Text = "";

                                if (this.ContainerTarget.Text != "")
                                {
                                   // System.Threading.Thread.Sleep(10000);
                                    if (Convert.ToInt32(this.FromLocation.Text.Substring(4, 1).ToString()) - Convert.ToInt32(this.FromLocationTarget.Text.Substring(4, 1).ToString()) == 1)
                                    {
                                        this.Container.Text = this.ContainerTarget.Text;
                                        this.Container.BackColor = ReturnPermitColor(this.Container.Text);

                                        this.ContainerLenghth.Text = this.ContainerLenghthTarget.Text;
                                        this.HandlingTypeCode.Text = this.HandlingTypeCodeTarget.Text;
                                        this.FromLocation.Text = this.FromLocationTarget.Text;
                                        this.ContainerTarget.Text = "";
                                        this.ContainerLenghthTarget.Text = "";
                                        this.HandlingTypeCodeTarget.Text = "";
                                        this.FromLocationTarget.Text = "";



                                        this.ContainerTarget.BackColor = Color.White;
                                        this.ContainerLenghthTarget.BackColor = Color.White;
                                        this.HandlingTypeCodeTarget.BackColor = Color.White;
                                        this.FromLocationTarget.BackColor = Color.White;




                                    }

                                    else
                                    {



                                        dt5 = Con3.ReturnDT("SELECT  dbo.TB_Location.Container, REPLACE(dbo.TB_Location.LocationCode, '-', '') AS LocationCode , dbo.CO_ContainerProfile.ContainerLength , dbo.CO_ContainerProfile.ContainerTypeCode " +
                                                            " FROM         dbo.TB_Location INNER JOIN " +
                                                            " dbo.CO_ContainerProfile ON dbo.TB_Location.Container = dbo.CO_ContainerProfile.Container " +
                                                            " WHERE     (dbo.TB_Location.BlocCode = '" + BlocDef + "') AND (REPLACE(dbo.TB_Location.LocationCode, '-', '') = '" + this.FromLocation.Text.Substring(0, 4) + (Convert.ToInt32(this.FromLocation.Text.Substring(4, 1).ToString()) - 1).ToString() + "')");





                                        this.Container.Text = dt5.Rows[0][0].ToString();
                                        this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                                        this.ContainerLenghth.Text = dt5.Rows[0][2].ToString();
                                        this.HandlingTypeCode.Text = dt5.Rows[0][3].ToString();
                                        
                                        
                                        this.FromLocation.Text = dt5.Rows[0][1].ToString();
                                        this.FromLocation.BackColor = Color.White;



                                    }

                                    this.ToLocation.Text = "";


                                    FirstComplete = true;



                                }

                                else
                                {

                                    btmCancel_Click(0, e);
                                }







                                if (this.Container.Text.Length > 0)
                                {

                                    this.Container.BackColor = ReturnPermitColor(this.Container.Text);

                                    this.btnOK.Enabled = true;
                                    // this.btmCancel.Enabled = false;
                                    DG.Enabled = true;
                                    this.Container.Enabled = true;
                                    this.ContainerLenghth.Enabled = true;
                                    this.HandlingTypeCode.Enabled = true;
                                    this.FromLocation.Enabled = true;
                                    this.ToLocation.Enabled = true;
                                }




                            }


                        }


                        //   Mapping the Data  



                        if (this.Counter.Text == "")
                        {
                            MappingSquare();
                        }


                    }





                }


            }




        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.Container.Text.Trim().Length == 0)
            {

                return;
            }


            string StrLocation = string.Empty;
            ConTerminalData Con1 = new ConTerminalData();
    
          
                //DataTable dt2 = Con1.ReturnDT("SELECT     TOP (100) PERCENT dbo.CO_Containers.Container, dbo.CO_ContainerProfile.ContainerLength, dbo.CO_ContainerProfile.ContainerTypeCode, " +
                //" dbo.CP_Deal.HandlingTypeCode, dbo.CO_Containers.LocationCode, CAST(CAST(dbo.CO_Containers.NetoWeight AS int) AS varchar(5)) AS NetoWeight, " +
                //" CASE WHEN dbo.CO_Containers.UNCode1 IS NULL THEN '' ELSE RTRIM(dbo.CO_Containers.UNCode1) END AS UNCode, " +
                //" LTRIM(RTRIM(dbo.TC_Client.ShortClientName)) AS ShortClientName, CASE WHEN OrderDesc IS NULL THEN '' ELSE right(OrderDesc,15) END AS OrderDesc, " + 
                //" dbo.CP_Deal.Permit " +
                //" FROM  dbo.CO_Containers INNER JOIN " +
                //" dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container INNER JOIN " +
                //" dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber INNER JOIN " +
                //" dbo.TC_Client ON dbo.CP_Deal.RecieveCommisionCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN " +
                //" dbo.V_OrderForkLift ON dbo.CO_Containers.Container = dbo.V_OrderForkLift.Container AND  " +
                //" dbo.CO_Containers.DealNumber = dbo.V_OrderForkLift.DealNumber " +
                //" WHERE     (dbo.CO_Containers.EntranceDate IS NOT NULL OR dbo.CO_Containers.RegisterDate IS NOT NULL) AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) > 1 THEN exitdate ELSE exitgatedate END IS NULL) AND " +
                //" (dbo.CO_ContainerProfile.Container = '"  + this.Container.Text + "') " +
                //"ORDER BY dbo.CO_Containers.LocationCode DESC ");


            DataTable dt2 = Con1.ReturnDT("SELECT * FROM  dbo.v_RtgContainerData " +
            " WHERE  (dbo.v_RtgContainerData.Container = '" + this.Container.Text + "') " +
            "ORDER BY dbo.v_RtgContainerData.LocationCode DESC ");





              String str = "מכולה: " ;
              str += dt2.Rows[0][0].ToString() + (char)10 + (char)10;

              str += "גודל: " ;
              str += dt2.Rows[0][1].ToString() + (char)10 + (char)10;

              str += "סוג: " ;
              str += dt2.Rows[0][2].ToString() + (char)10 + (char)10;

              str += "טיפול: ";
              str += dt2.Rows[0][3].ToString() + (char)10 + (char)10;

              str += "איתור: ";
              str += dt2.Rows[0][4].ToString() + (char)10 + (char)10;

              str += "משקל: " ;
              str += dt2.Rows[0][5].ToString() + (char)10 + (char)10;


              str += "סיכון: " ;
              str += dt2.Rows[0][6].ToString() + (char)10 ;


              str += "לקוח: " ;
              str += dt2.Rows[0][7].ToString() + (char)10 ;

              str += "עבודה: ";
              str += dt2.Rows[0][8].ToString() + (char)10 ;

              str += "התרה: ";
              str += dt2.Rows[0][9].ToString() + (char)10;


              str += "קפסיטי: ";
              str += dt2.Rows[0][10].ToString() + (char)10 ;


              str += "קו ספנות: ";
              str += dt2.Rows[0][11].ToString() + (char)10 + (char)10;

              this.groupBox3.Visible = true;


              this.ContainerInfo.Text = str;


              dt2 = Con1.ReturnDT("SELECT     TOP (100) PERCENT dbo.CO_Containers.Container AS מכולה,dbo.CO_Containers.LocationCode AS איתור, dbo.CO_ContainerProfile.ContainerLength AS גודל, dbo.CO_ContainerProfile.ContainerTypeCode AS סוג,   " +
             "  CAST(CAST(dbo.CO_Containers.NetoWeight AS int) AS varchar(5)) AS משקל,   " +
             " CASE WHEN dbo.CO_Containers.UNCode1 IS NULL THEN '' ELSE RTRIM(dbo.CO_Containers.UNCode1) END AS מסוכן,  CASE WHEN OrderDesc IS NULL THEN '' ELSE OrderDesc END AS הזמנה, dbo.CP_Deal.Permit AS התרה    " +
             " FROM         dbo.CO_Containers INNER JOIN  " +
             " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container INNER JOIN  " +
             " dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber INNER JOIN  " +
             " dbo.TC_Client ON dbo.CP_Deal.RecieveCommisionCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN  " +
             " dbo.V_OrderForkLift ON dbo.CO_Containers.Container = dbo.V_OrderForkLift.Container AND   " +
             " dbo.CO_Containers.DealNumber = dbo.V_OrderForkLift.DealNumber  " +
             " WHERE     (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (CASE WHEN DATEDIFF(d, exitdate, getdate()) > 1 THEN exitdate ELSE exitgatedate END IS NULL) AND   " +
             " (dbo.CO_Containers.DealNumber =  " +
             "  (SELECT   TOP 1  DealNumber FROM    dbo.CO_Containers AS CO_Containers_1   " +
             "  WHERE  Container =  '" + this.Container.Text + "'" +// michael 2021 גם מכולות שעוד לא הגיעו'   AND EntranceDate IS NOT NULL  " +
             "  AND  CASE WHEN DATEDIFF(d, exitdate, getdate()) > 1 THEN exitdate ELSE exitgatedate END IS NULL    AND DealNumber IS NOT NULL) ) AND (dbo.CO_Containers.Container <>  '" + this.Container.Text + "')");


           





              DGDealContainer.DataSource = dt2;

              DGDealContainer.Visible = true;
             



















        }





        private void btmCancel_Click(object sender, EventArgs e)
        {



            if (this.Counter.Text.Length > 0)
            {
                string MessageStr = "";
                Byte[] bytes = new Byte[256];
                ConTerminalData Con3 = new ConTerminalData();
                DataTable dt5;



                dt5 = Con3.ReturnDT("SELECT   PreMessageCancel  FROM dbo.RG_B3  WHERE     (CounterID =  (SELECT     MAX(CounterID) AS Expr1  FROM   dbo.RG_B3 AS RG_B3_1 WHERE CHE = '" + this.CHE.Text + "'))  AND CHE = '" + this.CHE.Text + "'");
                MessageStr = "FFFF" + ReturnAsciText(dt5.Rows[0][0].ToString() + calcChecksum(dt5.Rows[0][0].ToString()));
                dt5 = Con3.ReturnDT("UPDATE RG_B3  set  MessageCancel ='" + MessageStr + "' WHERE CounterID =  (SELECT MAX(CounterID) AS Expr1  FROM  dbo.RG_B3 AS RG_B3_1 WHERE CHE = '" + this.CHE.Text + "')  AND CHE = '" + this.CHE.Text + "'");
            }

            this.ContainerTarget.Text = "";
            this.ContainerLenghthTarget.Text = "";
            this.HandlingTypeCodeTarget.Text = "";
            this.FromLocationTarget.Text = "";
            this.ContainerTarget.BackColor = Color.White;
            this.ContainerLenghthTarget.BackColor = Color.White;
            this.HandlingTypeCodeTarget.BackColor = Color.White;
            this.FromLocationTarget.BackColor = Color.White;
            this.FromLocation.BackColor = Color.White;
            this.ToLocation.Text = "";
            FirstComplete = false;
            this.Container.Text = "";
            this.ContainerLenghth.Text = "";
            this.HandlingTypeCode.Text = "";
            this.FromLocation.Text = "";
            this.ToLocation.Text = "";
            this.Container.BackColor = Color.White;
            this.FromLocation.BackColor = Color.White;
            this.ToLocation.BackColor = Color.White;
            this.btnOK.Enabled = true;
            // this.btmCancel.Enabled = false;
            DG.Enabled = true;
            this.Container.Enabled = true;
            this.ContainerLenghth.Enabled = true;

            this.HandlingTypeCode.Enabled = true;
            
            this.FromLocation.Enabled = true;
            this.ToLocation.Enabled = true;
            this.Counter.Text = "";

            this.DriverName.Text = "";
            this.TruckID.Text = "";
            this.Weight.Text = "";
            this.btnOK.Enabled = false;
        }

        private void CmdTruck_Click(object sender, EventArgs e)
        {


            // this.Hide();
            frmInformation frm = new frmInformation();
            frm.Show();

        }

        private void BlockName_TextChanged(object sender, EventArgs e)
        {

        }

        private void CHE_TextChanged(object sender, EventArgs e)
        {

        }

        private void FromLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void Container_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = true;
        }

        private void CmdExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }



        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.Container.Text += ctl.Text.ToString();

        }


        private void One_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }




        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.Container.Text += ctl.Text.ToString();
        }

        private void TWO_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }


        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.Container.Text += ctl.Text.ToString();
        }


        private void THREE_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }


        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.Container.Text += ctl.Text.ToString();
        }



        private void Four_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }






        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.Container.Text += ctl.Text.ToString();
        }




        private void Five_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }











        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.Container.Text += ctl.Text.ToString();
        }





        private void Six_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }












        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.Container.Text += ctl.Text.ToString();
        }





        private void Seven_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }








        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.Container.Text += ctl.Text.ToString();
        }




        private void Eight_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.Container.Text += ctl.Text.ToString();
            this.ContainerPick1.Text += ctl.Text.ToString();
        }








        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.Container.Text += ctl.Text.ToString();
        }





        private void Nine_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.ContainerPick1.Text += ctl.Text.ToString();
        }








        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.Container.Text += ctl.Text.ToString();
        }





        private void Zero_1_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;

            this.ContainerPick1.Text += ctl.Text.ToString();
        }








        private void DELETE_Click(object sender, EventArgs e)
        {


            if (this.Container.Text.Length != 0)
            {
                this.Container.Text = this.Container.Text.Substring(0, Container.Text.Length - 1);
            }

        }




        private void DELETE_1_Click(object sender, EventArgs e)
        {


            if (this.ContainerPick1.Text.Length != 0)
            {
                this.ContainerPick1.Text = this.ContainerPick1.Text.Substring(0, this.ContainerPick1.Text.Length - 1);
            }

        }









        private void Close_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = false;
        }






        private void Close_1_Click(object sender, EventArgs e)
        {
            ConTerminalData Con1 = new ConTerminalData();
            DataTable dt1;

            if (this.CHE.Text == "GOLD1")
            {
                dt1 = Con1.ReturnDT("UPDATE dbo.TB_Parameters SET ContainerPick1 ='FALSE'");
            }
            else
            {
                dt1 = Con1.ReturnDT("UPDATE dbo.TB_Parameters SET ContainerPick2 ='FALSE'");
            }

            
            
            
            
            this.ContainerPick.Visible = false;





        }





        private void cmdWork_Click(object sender, EventArgs e)
        {
            FrmWorks frm = new FrmWorks();
            frm.Show();
        }

        private void btmMenu_Click(object sender, EventArgs e)
        {
            FrmMenu frm = new FrmMenu();
            frm.Show();
        }

        private void BtmFindContainer_Click(object sender, EventArgs e)
        {
         //   button1_Click(0, e);

            this.groupBox1.Visible = false;


            ConTerminalData Con1 = new ConTerminalData();
            DataTable dt2;



            dt2 = Con1.ReturnDT("SELECT     COUNT(*) AS Count, MIN(Container) AS Container " +
              " FROM         (SELECT     TOP (100) PERCENT Container " +
              " FROM          dbo.CO_Containers " +
              " WHERE      (EntranceDate IS NOT NULL OR RegisterDate IS NOT NULL) AND (ExitDate IS NULL) " +
             "  UNION ALL " +
             "  SELECT DISTINCT TOP (100) PERCENT TP_KaronFromHaifa1_1.Container " +
             "  FROM         dbo.TP_KaronFromHaifa1 AS TP_KaronFromHaifa1_1 INNER JOIN " +
             "  dbo.CO_Containers AS CO_Containers_1 ON TP_KaronFromHaifa1_1.Container = CO_Containers_1.Container INNER JOIN " +
             "  dbo.CP_Deal ON CO_Containers_1.DealNumber = dbo.CP_Deal.DealNumber " +
             "  WHERE     (CO_Containers_1.EntranceDate IS NULL  ) AND (CO_Containers_1.ExitDate IS NULL) AND (NOT (dbo.CP_Deal.HandlingTypeCode = 'EM' OR " +
             "  dbo.CP_Deal.HandlingTypeCode = 'DP'))) AS derivedtbl_1 " +
              " WHERE     (Container LIKE '%" + this.Container.Text + "%')");



            if (Convert.ToInt32(dt2.Rows[0][0].ToString()) == 0)
            {
                MessageBox.Show(" מכולה לא קיימת  ");
                return;

            }


            






            if (this.FromLocation.Text.Length > 0)
            {
                if (this.FromLocation.Text.Substring(3, 1) == "G" || this.FromLocation.Text.Substring(3, 1) == "T")
                {
                    this.Container.Text = dt2.Rows[0][1].ToString();
                    this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                    //  DataTable dt1 = Con1.ReturnDT("SELECT  ContainerLength ,ContainerTypeCode FROM  dbo.CO_ContainerProfile WHERE Container = '" + this.Container.Text + "'");


                    DataTable dt1 = Con1.ReturnDT("SELECT dbo.CO_ContainerProfile.ContainerLength, dbo.CP_Deal.HandlingTypeCode " +
                   " FROM         dbo.CP_Deal INNER JOIN " +
                   " dbo.CO_Containers ON dbo.CP_Deal.DealNumber = dbo.CO_Containers.DealNumber INNER JOIN " +
                   " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container " +
                   " WHERE   (dbo.CO_Containers.EntranceDate IS NOT NULL  OR dbo.CO_Containers.RegisterDate IS NOT NULL) AND (dbo.CO_Containers.ExitDate IS NULL) AND (dbo.CO_Containers.Container =  '" + this.Container.Text + "')");








                    this.ContainerLenghth.Text = dt1.Rows[0][0].ToString();
                    this.HandlingTypeCode.Text = dt1.Rows[0][1].ToString();
                }





                else
                {



                    if (Convert.ToInt32(dt2.Rows[0][0].ToString()) == 1)
                    {


                        // בדיקה האם מופיע בשכבה העליונה

                        DataTable dt1 = Con1.ReturnDT("SELECT     COUNT(*) AS Counter  " +
                         " FROM         dbo.V_Location_BOND_TOP INNER JOIN  " +
                         " dbo.TB_Location ON dbo.V_Location_BOND_TOP.LocationCode = dbo.TB_Location.LocationCode  " +
                         " WHERE     (dbo.TB_Location.Container = '" + dt2.Rows[0][1].ToString() + "' AND dbo.V_Location_BOND_TOP.BlocCode = '" + BlocDef + "') ");



                        if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 1)
                        {
                            this.Container.Text = dt2.Rows[0][1].ToString();
                            this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                            // dt1 = Con1.ReturnDT("SELECT  ContainerLength, ContainerTypeCode  FROM  dbo.CO_ContainerProfile WHERE Container = '" + this.Container.Text + "'");


                            dt1 = Con1.ReturnDT("SELECT dbo.CO_ContainerProfile.ContainerLength, dbo.CP_Deal.HandlingTypeCode " +
                            " FROM         dbo.CP_Deal INNER JOIN " +
                            " dbo.CO_Containers ON dbo.CP_Deal.DealNumber = dbo.CO_Containers.DealNumber INNER JOIN " +
                            " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container " +
                            " WHERE   (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitGAteDate IS NULL) AND (dbo.CO_Containers.Container =  '" + this.Container.Text + "')");










                            this.ContainerLenghth.Text = dt1.Rows[0][0].ToString();
                            this.HandlingTypeCode.Text = dt1.Rows[0][1].ToString();

                            dt1 = Con1.ReturnDT("SELECT  REPLACE(LocationCode, '-', '') AS Location  FROM  dbo.TB_Location WHERE Container = '" + this.Container.Text + "'");
                            this.FromLocation.Text = dt1.Rows[0][0].ToString();
                        }

                        else
                        {
                            // קיימת קבורה יש מעליה מכולות


                            dt1 = Con1.ReturnDT("SELECT  REPLACE(LocationCode, '-', '') AS Location  FROM  dbo.TB_Location WHERE Container = '" + dt2.Rows[0][1].ToString() + "'");

                            // Convert.ToInt32( ReturnIndexDataTable(dt1.Rows[0][3].ToString(), TranCol))

                            string StrLocation = string.Empty;

                            dt1 = Con1.ReturnDT("SELECT LocationCode FROM  dbo.TB_Location  WHERE  Container =  '" + dt2.Rows[0][1].ToString() + "'");


                            StrLocation = dt1.Rows[0][0].ToString().Substring(0, 3);


                            dt2 = Con1.ReturnDT("SELECT  SUBSTRING(dbo.TB_Location.LocationCode, 5, 1)  + ' ' + dbo.CO_Containers.Container  + ' ' +  dbo.CO_ContainerProfile.ContainerLength  + ' ' +  dbo.CO_ContainerProfile.ContainerTypeCode + ' ' + dbo.CP_Deal.HandlingTypeCode + ' '+" +
                                              " cast( cast(dbo.CO_Containers.NetoWeight  as int) as varchar(5)) + ' ' + case when  dbo.CO_Containers.UNCode1  is null then '' else  RTRIM(dbo.CO_Containers.UNCode1) END + ' ' + LTRIM(RTRIM(dbo.TC_Client.ShortClientName))  + ' ' + CASE WHEN OrderDesc IS NULL THEN '' ELSE OrderDesc END      AS '" + StrLocation + "'" +
                                            " FROM         dbo.TB_Location INNER JOIN " +
                                            "  dbo.CO_Containers ON dbo.TB_Location.Container = dbo.CO_Containers.Container INNER JOIN  " +
                                            "  dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container INNER JOIN  " +
                                            "  dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber INNER JOIN  " +
                                            "  dbo.TC_Client ON dbo.CP_Deal.RecieveCommisionCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN  " +
                                            "  dbo.V_OrderForkLift ON dbo.CO_Containers.Container = dbo.V_OrderForkLift.Container AND   " +
                                            "  dbo.CO_Containers.DealNumber = dbo.V_OrderForkLift.DealNumber  " +
                                            " WHERE     (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitGateDate IS NULL) AND " +
                                            " (LEFT(dbo.TB_Location.LocationCode, 4) = '" + StrLocation + "') ORDER BY dbo.TB_Location.LocationCode DESC ");







                            DGC.DataSource = dt2;
                            DG.Visible = false;
                            DGC.Visible = true;
                            btnBack.Visible = true;




                            DGC.Rows[0].Selected = false;
                            DGC.Rows[DGC.Rows.Count - Convert.ToInt32(dt1.Rows[0][0].ToString().Trim().Substring(6, 1))].Selected = true;

                        }





                    }










                }

            }







            else
            {


                if (Convert.ToInt32(dt2.Rows[0][0].ToString()) == 1)
                {


                    // בדיקה האם מופיע בשכבה העליונה

                    DataTable dt1 = Con1.ReturnDT("SELECT     COUNT(*) AS Counter  " +
                     " FROM         dbo.V_Location_BOND_TOP INNER JOIN  " +
                     " dbo.TB_Location ON dbo.V_Location_BOND_TOP.LocationCode = dbo.TB_Location.LocationCode  " +
                     " WHERE     (dbo.TB_Location.Container = '" + dt2.Rows[0][1].ToString() + "')  AND dbo.V_Location_BOND_TOP.BlocCode = '" + BlocDef + "'");   //   28052018  this.BlockName.Text



                    if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 1)
                    {
                        this.Container.Text = dt2.Rows[0][1].ToString();
                        this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                        // dt1 = Con1.ReturnDT("SELECT  ContainerLength, ContainerTypeCode  FROM  dbo.CO_ContainerProfile WHERE Container = '" + this.Container.Text + "'");



                        dt1 = Con1.ReturnDT("SELECT dbo.CO_ContainerProfile.ContainerLength, dbo.CP_Deal.HandlingTypeCode " +
                        " FROM         dbo.CP_Deal INNER JOIN " +
                        " dbo.CO_Containers ON dbo.CP_Deal.DealNumber = dbo.CO_Containers.DealNumber INNER JOIN " +
                        " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container " +
                        " WHERE   (dbo.CO_Containers.EntranceDate IS NOT NULL OR dbo.CO_Containers.RegisterDate IS NOT NULL) AND (dbo.CO_Containers.ExitGAteDate IS NULL) AND (dbo.CO_Containers.Container =  '" + this.Container.Text + "')");








                        this.ContainerLenghth.Text = dt1.Rows[0][0].ToString();
                        this.HandlingTypeCode.Text = dt1.Rows[0][1].ToString();
                        // dt1 = Con1.ReturnDT("SELECT  REPLACE(LocationCode, '-', '') AS Location  FROM  dbo.TB_Location WHERE Container = '" + this.Container.Text + "'");



                        dt1 = Con1.ReturnDT("SELECT     REPLACE(dbo.TB_Location.LocationCode, '-', '') AS Location, dbo.RG_ColDG.IndexCol " +
                       " FROM         dbo.TB_Location INNER JOIN " +
                       " dbo.RG_ColDG ON LEFT(dbo.TB_Location.LocationCode, 3) = dbo.RG_ColDG.ColS " +
                       " WHERE     (dbo.TB_Location.Container = '" + this.Container.Text + "')");





                        this.FromLocation.Text = dt1.Rows[0][0].ToString();


                        // DGC.Rows[0].Selected = false;
                        //  DGC.Rows[DGC.Rows.Count - Convert.ToInt32(dt1.Rows[0][0].ToString().Trim().Substring(4, 1))].Selected = true;

                        DG.Rows[Convert.ToInt32(dt1.Rows[0][0].ToString().Substring(3, 1).Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5").Replace("G", "6").Replace("T", "7").Replace("   ", "0"))].Cells[Convert.ToInt32(dt1.Rows[0][1].ToString())].Selected = true;



                    }

                    else
                    {
                        // קיימת קבורה יש מעליה מכולות


                        //   dt1 = Con1.ReturnDT("SELECT  REPLACE(LocationCode, '-', '') AS Location  FROM  dbo.TB_Location WHERE Container = '" + dt2.Rows[0][1].ToString() + "' AND BlocCode = '" + this.BlockName.Text + "'");

                        // Convert.ToInt32( ReturnIndexDataTable(dt1.Rows[0][3].ToString(), TranCol))

                        string StrLocation = string.Empty;




                        dt1 = Con1.ReturnDT("SELECT LocationCode FROM  dbo.TB_Location  WHERE  Container =  '" + dt2.Rows[0][1].ToString() + "' AND BlocCode = '" + this.BlockName.Text + "'");


                        if (dt1.Rows.Count > 0)
                        {
                            StrLocation = dt1.Rows[0][0].ToString().Substring(0, 4);

                            dt2 = Con1.ReturnDT("SELECT  SUBSTRING(dbo.TB_Location.LocationCode, 5, 1)  + ' ' + dbo.CO_Containers.Container  + ' ' +  dbo.CO_ContainerProfile.ContainerLength  + ' ' +  dbo.CO_ContainerProfile.ContainerTypeCode + ' ' + dbo.CP_Deal.HandlingTypeCode + ' '+" +
                                              " cast( cast(dbo.CO_Containers.NetoWeight  as int) as varchar(5)) + ' ' + case when  dbo.CO_Containers.UNCode1  is null then '' else  RTRIM(dbo.CO_Containers.UNCode1) END + ' ' + LTRIM(RTRIM(dbo.TC_Client.ShortClientName))  + ' ' + CASE WHEN OrderDesc IS NULL THEN '' ELSE OrderDesc END      AS '" + StrLocation + "'" +
                                            " FROM         dbo.TB_Location INNER JOIN " +
                                            "  dbo.CO_Containers ON dbo.TB_Location.Container = dbo.CO_Containers.Container INNER JOIN  " +
                                            "  dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container INNER JOIN  " +
                                            "  dbo.CP_Deal ON dbo.CO_Containers.DealNumber = dbo.CP_Deal.DealNumber INNER JOIN  " +
                                            "  dbo.TC_Client ON dbo.CP_Deal.RecieveCommisionCode = dbo.TC_Client.ClientCode LEFT OUTER JOIN  " +
                                            "  dbo.V_OrderForkLift ON dbo.CO_Containers.Container = dbo.V_OrderForkLift.Container AND   " +
                                            "  dbo.CO_Containers.DealNumber = dbo.V_OrderForkLift.DealNumber  " +
                                            " WHERE     (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitGateDate IS NULL) AND " +
                                            " (LEFT(dbo.TB_Location.LocationCode, 4) = '" + StrLocation + "') ORDER BY dbo.TB_Location.LocationCode DESC ");




                            DGC.DataSource = dt2;
                            DG.Visible = false;
                            DGC.Visible = true;
                            btnBack.Visible = true;


                            DGC.Rows[0].Selected = false;
                            DGC.Rows[DGC.Rows.Count - Convert.ToInt32(dt1.Rows[0][0].ToString().Trim().Substring(4, 1))].Selected = true;

                        }


                        else
                        {
                            this.Container.Text = dt2.Rows[0][1].ToString();
                            this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                            // dt1 = Con1.ReturnDT("SELECT  ContainerLength,ContainerTypeCode  FROM  dbo.CO_ContainerProfile WHERE Container = '" + this.Container.Text + "'");



                            dt1 = Con1.ReturnDT("SELECT dbo.CO_ContainerProfile.ContainerLength, dbo.CP_Deal.HandlingTypeCode " +
                            " FROM         dbo.CP_Deal INNER JOIN " +
                            " dbo.CO_Containers ON dbo.CP_Deal.DealNumber = dbo.CO_Containers.DealNumber INNER JOIN " +
                            " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container " +
                            " WHERE   (dbo.CO_Containers.EntranceDate IS NOT NULL OR  dbo.CO_Containers.RegisterDate IS NOT NULL) AND (dbo.CO_Containers.ExitGAteDate IS NULL) AND (dbo.CO_Containers.Container =  '" + this.Container.Text + "')");




                            this.ContainerLenghth.Text = dt1.Rows[0][0].ToString();
                            this.HandlingTypeCode.Text = dt1.Rows[0][1].ToString();
                        }








                    }













                }

                else
                {
                    this.Container.Text = dt2.Rows[0][1].ToString();
                    this.Container.BackColor = ReturnPermitColor(this.Container.Text);
                    //   DataTable dt1 = Con1.ReturnDT("SELECT  ContainerLength , ContainerTypeCode FROM  dbo.CO_ContainerProfile WHERE Container = '" + this.Container.Text + "'");




                    DataTable dt1 = Con1.ReturnDT("SELECT dbo.CO_ContainerProfile.ContainerLength, dbo.CP_Deal.HandlingTypeCode " +
                    " FROM         dbo.CP_Deal INNER JOIN " +
                    " dbo.CO_Containers ON dbo.CP_Deal.DealNumber = dbo.CO_Containers.DealNumber INNER JOIN " +
                    " dbo.CO_ContainerProfile ON dbo.CO_Containers.Container = dbo.CO_ContainerProfile.Container " +
                    " WHERE   (dbo.CO_Containers.EntranceDate IS NOT NULL) AND (dbo.CO_Containers.ExitGAteDate IS NULL) AND (dbo.CO_Containers.Container =  '" + this.Container.Text + "')");





                    if (dt1.Rows.Count > 0)
                    {

                        this.ContainerLenghth.Text = dt1.Rows[0][0].ToString();
                        this.HandlingTypeCode.Text = dt1.Rows[0][1].ToString();
                    }
                }
            }




            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            this.groupBox1.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.groupBox2.Visible = false;
        }

        private void BtmFindContainer_1_Click(object sender, EventArgs e)
        {

            ConTerminalData Con1 = new ConTerminalData();
            DataTable dt2;
 
               dt2 = Con1.ReturnDT("SELECT     COUNT(*) AS Count, MIN(Container) AS Container " +
               " FROM         (SELECT     TOP (100) PERCENT Container " +
               " FROM          dbo.CO_Containers " +
               " WHERE      (EntranceDate IS NOT NULL OR RegisterDate IS NOT NULL) AND (ExitDate IS NULL) " +
              "  UNION ALL " +
              "  SELECT DISTINCT TOP (100) PERCENT TP_KaronFromHaifa1_1.Container " +
              "  FROM         dbo.TP_KaronFromHaifa1 AS TP_KaronFromHaifa1_1 INNER JOIN " +
              "  dbo.CO_Containers AS CO_Containers_1 ON TP_KaronFromHaifa1_1.Container = CO_Containers_1.Container INNER JOIN " +
              "  dbo.CP_Deal ON CO_Containers_1.DealNumber = dbo.CP_Deal.DealNumber " +
              "  WHERE     (CO_Containers_1.EntranceDate IS NULL) AND (CO_Containers_1.ExitDate IS NULL) AND (NOT (dbo.CP_Deal.HandlingTypeCode = 'EM' OR " +
              "  dbo.CP_Deal.HandlingTypeCode = 'DP'))) AS derivedtbl_1 " +
               " WHERE     (Container LIKE '%" + this.ContainerPick1.Text + "%')");
            


            if (Convert.ToInt32(dt2.Rows[0][0].ToString()) == 0)
            {
                MessageBox.Show(" מכולה לא קיימת  ");
                return;

            }

            this.ContainerPick1.Text = dt2.Rows[0][1].ToString();


            this.BtmUpdateContainer_1.Enabled = true;

        }

        private void BtmUpdateContainer_1_Click(object sender, EventArgs e)
        {

            ConTerminalData Con1 = new ConTerminalData();
            DataTable dt2;
             

         


                DataTable dt1 = Con1.ReturnDT("INSERT  dbo.RG_Container(Container,CHE,OperatorID ) Values('" + this.ContainerPick1.Text + "','" + this.CHE.Text + "','" + this.OperatorID.Text + "')");


           if (this.CHE.Text == "GOLD1")
             {    
                dt1 = Con1.ReturnDT("UPDATE dbo.TB_Parameters SET ContainerPick1 ='FALSE'");
             }
           else
             {
               dt1 = Con1.ReturnDT("UPDATE dbo.TB_Parameters SET ContainerPick2 ='FALSE'");
             }

           this.Container.Text = this.ContainerPick1.Text;

            this.ContainerPick1.Text = "";
            this.ContainerPick.Visible = false;


          
            this.Container.BackColor = ReturnPermitColor(this.Container.Text);
           
            
            // 270518
            dt1 = Con1.ReturnDT("SELECT  ContainerLength , ContainerTypeCode  FROM  dbo.CO_ContainerProfile WHERE Container = '" + this.Container.Text + "'");
           
            
            
           
            
            
            
            
            
            
            
            this.ContainerLenghth.Text = dt1.Rows[0][0].ToString();
            this.HandlingTypeCode.Text = dt1.Rows[0][1].ToString();

        }

        private void EnconsoleButtom_Click(object sender, EventArgs e)
        {
            this.EnconsoleButtom.Enabled = false;
            
            string connectionString = null;
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            SqlCommand command1 = new SqlCommand();

            connectionString = "Data Source=192.6.8.52;Initial Catalog=TerminalData;Persist Security Info=True;User ID=malgezot;Password=12345678";
            connection = new SqlConnection(connectionString);
            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command1.Connection = connection;
            command1.CommandType = CommandType.StoredProcedure;
           
            if (this.CHE.Text == "GOLD2")
            {
                command1.CommandText = "KillToss2";
                command1.ExecuteNonQuery();
                command1.CommandText = "RunEnconsoleRTG2";
            }       
            else
            {
                command1.CommandText = "KillToss1";
                command1.ExecuteNonQuery();
                command1.CommandText = "RunEnconsoleRTG1";
            }

              try
            {
            
                command1.ExecuteNonQuery();

                CounterConnect = 0;

             }
                catch
                {
                    return;
                }
        
        
        }

        private void buttomError_Click(object sender, EventArgs e)
        {
            MappingError();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.groupBox3.Visible = false;
        }


    } 
}

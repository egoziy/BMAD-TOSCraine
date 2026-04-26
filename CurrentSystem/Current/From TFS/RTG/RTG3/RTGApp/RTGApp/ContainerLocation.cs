using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RTGApp
{
    public partial class ContainerLocation : Form
    {
        public ContainerLocation()
        {
            InitializeComponent();

            ConTerminalData Con1 = new ConTerminalData();

            // SetBackColorField();


            //DataTable dt = Con1.ReturnDT("SELECT ContainerNumber   FROM  dbo.CO_Containers   WHERE     (EntranceDate IS NOT NULL) AND (ExitDate IS NULL)   ORDER BY ContainerNumber");
            //this.ContainerNumber.ValueMember = "ContainerNumber";
            //this.ContainerNumber.DisplayMember = "ContainerNumber";
            //this.ContainerNumber.DataSource = dt;

           

        }

        private void ContainerLocation_Load(object sender, EventArgs e)
        {

            this.cboContainerLetter.Text = "";

            this.Location1.Text = FrmMap01.SetValueHeightStr.ToString();
            this.Location2.Text = FrmMap01.SetValueHeightStr2.ToString();
            this.Location3.Text = FrmMap01.SetValueHeightStr3.ToString();
             

            
        
        }



        private void One_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = One;
            this.ContainerNumber.Text += ctl.Text.ToString();

        }

        private void TWO_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = TWO;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void THREE_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = THREE;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Four;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Five;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Six;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Seven;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Eight;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Nine;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void Zero_Click(object sender, EventArgs e)
        {
            Button ctl = new Button();
            ctl = Zero;
            this.ContainerNumber.Text += ctl.Text.ToString();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            if (this.ContainerNumber.Text != string.Empty)
            {
                this.ContainerNumber.Text = this.ContainerNumber.Text.Substring(0, ContainerNumber.Text.Length - 1);
                this.cboContainerLetter.Text = "";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try { 
           // this.cboContainerLetter.Text = "";
            ConTerminalData Con1 = new ConTerminalData();
            


            UserConnection Con = new UserConnection();

            Cursor.Current = Cursors.WaitCursor;
            SetBackColorField();

            if (BackColorField() == 1) return;

//            DataTable  dt = Con1.ReturnDT("UPDATE TB_Location set Container = NULL  WHERE Container = '" + this.cboContainerLetter.Text + this.ContainerNumber.Text + "'");
//            dt = Con1.ReturnDT("UPDATE TB_Location set Container = '" + this.cboContainerLetter.Text + this.ContainerNumber.Text + "'  WHERE LocationCode = '" + this.Location1.Text + this.Location2.Text + this.Location3.Text + "'");
//            dt = Con1.ReturnDT("UPDATE CO_Containers set LocationCode = '" + this.Location1.Text + this.Location2.Text + this.Location3.Text + "'  WHERE  EntranceDate IS NOT NULL AND ExitDate IS NULL AND (Container = '" + this.cboContainerLetter.Text + this.ContainerNumber.Text + "')");
//             dt = Con1.ReturnDT("UPDATE dbo.TB_Parameters SET RefreshMapRTG ='TRUE',RefreshMapRTG2 ='TRUE',RefreshMapRTG3 ='TRUE',RefreshMapRTG4 ='TRUE'");
            // update    ashdod locasion only

            string str = string.Format("UPDATE TB_Location set Container = NULL  WHERE Terminal='ILCXQ' and Container = '{0}'", this.cboContainerLetter.Text + this.ContainerNumber.Text);
            WriteLog(str);
            DataTable dt = Con1.ReturnDT(str);

            str = string.Format("UPDATE TB_Location set Container = '{0}'  WHERE Terminal='ILCXQ' and LocationCode = '{1}'", this.cboContainerLetter.Text + this.ContainerNumber.Text, this.Location1.Text + this.Location2.Text + this.Location3.Text);
            WriteLog(str);
            dt = Con1.ReturnDT(str);

            str = string.Format("UPDATE CO_Containers set  LocationCode = '{0}'  WHERE  Terminal='ILCXQ' and (EntranceDate IS NOT NULL OR RegisterDate IS NOT NULL) AND (ExitDate IS NULL) AND (Container = '{1}')", this.Location1.Text + this.Location2.Text + this.Location3.Text, this.cboContainerLetter.Text + this.ContainerNumber.Text);
            WriteLog(str);
            dt = Con1.ReturnDT(str);

            str = "UPDATE dbo.TB_Parameters SET RefreshMapRTG ='TRUE',RefreshMapRTG2 ='TRUE',RefreshMapRTG3 ='TRUE',RefreshMapRTG4 ='TRUE'";
            WriteLog(str);
            dt = Con1.ReturnDT(str);


             this.Close();

                //  MessageBox.Show("המכולה עודכנה \n      בהצלחה");


            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in ContainerLocation btnOK_Click : " + ex);
            }
        }



        protected void SetBackColorField()
        {
            Color myColor = Color.FromArgb(255, 255, 255);
            this.cboContainerLetter.BackColor = myColor;
            this.ContainerNumber.BackColor = myColor;
            this.Location1.BackColor = myColor;
            this.Location2.BackColor = myColor;
            this.Location3.BackColor = myColor;
        }



        protected int BackColorField()
        {
            Color myColor = Color.FromArgb(255, 255, 255);
            Color mustColor = Color.FromArgb(245, 191, 106);
            int Ans = 0;
            if (this.ContainerNumber.Visible == true & this.ContainerNumber.BackColor == myColor & this.ContainerNumber.Text == string.Empty)
            { this.ContainerNumber.BackColor = mustColor; Ans = 1; }
            if (this.cboContainerLetter.Visible == true & this.cboContainerLetter.BackColor == myColor & this.cboContainerLetter.Text == string.Empty)
            { this.cboContainerLetter.BackColor = mustColor; Ans = 1; }
            if (this.Location1.Visible == true & this.Location1.BackColor == myColor & this.Location1.Text == string.Empty)
            { this.Location1.BackColor = mustColor; Ans = 1; }

            if (this.Location2.Visible == true & this.Location2.BackColor == myColor & this.Location2.Text == string.Empty)
            { this.Location2.BackColor = mustColor; Ans = 1; }

            if (this.Location3.Visible == true & this.Location3.BackColor == myColor & this.Location3.Text == string.Empty)
            { this.Location3.BackColor = mustColor; Ans = 1; }


            if (Ans == 1) return 1; else return 0;

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Location2_Click(object sender, EventArgs e)
        {
            this.Location2.DroppedDown=true;
        }

        private void Location3_Click(object sender, EventArgs e)
        {
            this.Location3.DroppedDown = true;
        }


        private void Location1_Click(object sender, EventArgs e)
        {
            this.Location1.DroppedDown = true;
        }

        private void RemoveButtom_Click(object sender, EventArgs e)
        {
            try { 
            this.cboContainerLetter.Text = "";
            ConTerminalData Con1 = new ConTerminalData();
            DataTable dt = Con1.ReturnDT("SELECT ContainerLetter FROM CO_Containers WHERE (EntranceDate IS NOT NULL OR RegisterDate IS NOT NULL)    AND ExitDate IS NOT NULL AND  ContainerNumber = '" + this.ContainerNumber.Text + "'");
            if (dt.Rows.Count > 0)
            {
                this.cboContainerLetter.Text = dt.Rows[0][0].ToString();

            }


            UserConnection Con = new UserConnection();

          //  Cursor.Current = Cursors.WaitCursor;
         //   SetBackColorField();

            if (BackColorField() == 1) return;

            dt = Con1.ReturnDT("UPDATE TB_Location set Container = NULL  WHERE Container = '" + this.cboContainerLetter.Text + this.ContainerNumber.Text + "'");
            dt = Con1.ReturnDT("UPDATE dbo.TB_Parameters SET RefreshMapRTG ='TRUE',RefreshMapRTG2 ='TRUE',RefreshMapRTG3 ='TRUE',RefreshMapRTG4 ='TRUE'");
           
           
            this.Close();

                //  MessageBox.Show("המכולה עודכנה \n      בהצלחה");

            }
            catch (Exception ex)
            {
                ContainerLocation.WriteLog("Error in ContainerLocation RemoveButtom_Click : " + ex);
            }
        }

        private void ContainerNumber_TextChanged(object sender, EventArgs e)
        {
            // this.cboContainerLetter.Text = "";
            ConTerminalData Con1 = new ConTerminalData();
            DataTable dt = Con1.ReturnDT("SELECT ContainerLetter FROM CO_Containers WHERE (EntranceDate IS NOT NULL OR RegisterDate IS NOT NULL)    AND ExitDate IS NULL AND  ContainerNumber = '" + this.ContainerNumber.Text + "'");
            if (dt.Rows.Count > 0)
            {
                // this.cboContainerLetter.Text = dt.Rows[0][0].ToString();

                this.cboContainerLetter.ValueMember = "ContainerLetter";
                this.cboContainerLetter.DisplayMember = "ContainerLetter";
                this.cboContainerLetter.DataSource = dt;

                if (dt.Rows.Count > 1)
                {
                    this.cboContainerLetter.Text = "";
                    cboContainerLetter.DroppedDown = true;
                }

                //this.cboContainerLetter.Text = dt.Rows[0][0].ToString();

            }
        }


        public static void WriteLog(string strLog, string LogFileName = "")
        {
            try
            {
                ConTerminalData Con1 = new ConTerminalData();
                DataTable dt = Con1.ReturnDT("INSERT INTO RG_ErrorLog (CHE, BlockName, Program_Version, Msg) VALUES('GOLD3', 'BOND3', '2025-01-08.1', ' " + strLog.Replace("'", "''") + "')");
            }
            catch { }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForkliftApp.Entities;
using ForkliftApp.Framework;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;



namespace ForkliftApp.DataAccess 
{
    public class ForkliftAppDA  
    {
        
        private static DataTable dtf = null;
        public static string m_Terminal = string.Empty;

        /// <summary>
        /// אוסף רשומות למסך בחירת פעולה.
        /// </summary>
        /// <param name="InOut"></param>
        /// <returns></returns>
        public static ForkliftAppDS GetContainers(string InOut, string p_Terminal)
        {
            
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;

            sqlParam1 = new SqlParameter("@Terminal", p_Terminal);
            ForkliftAppDS ds = new ForkliftAppDS();

            if (InOut=="In")
            {
                spName = "sp_ForkLiftContainersIn";   
                tableNames = new string[] { "ContainersIn" };
            }
            else if (InOut == "EMOut")
            {
                spName = "sp_ForkLiftContainersEMOut";
                tableNames = new string[] { "ContainersEMOut" };
            }
            else
            {
                spName = "sp_ForkLiftContainersOut";
                tableNames = new string[] { "ContainersOut" };
            }
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames, sqlParam1);

            return ds;
        }

        /// <summary>
        /// אוסף רשומות לפי מספר מכולה ללא אותיות
        /// </summary>
        /// <param name="ConParm"></param>
        /// <returns></returns>
        private static ForkliftAppDS GetContainersByNumber(string ConParms,string act, string p_Terminal)
        {
            //string ContainerNumber = ConParm;
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", p_Terminal);
            if (act == "Query" || act == "EmptyContainers" || act == "EMOut")
            {
                spName = "sp_ForkLiftContainersActQuery";
                tableNames = new string[] { "ContainersQuery" };
            }
            else if (act =="NotinListIn")
            {
                spName = "sp_ForkLiftContainerNotinlistIn";
                tableNames = new string[] { "ContainersNotinListIn" }; 
            }
            else if(act=="NotinLisOut")
            {
                spName = "sp_ForkLiftContainersNotinlistOut";
                tableNames = new string[] { "ContainersNotinListOut" };
            }
            else
            {
            //    spName = "sp_ForkLiftContainersMoreThen1";
                tableNames = new string[] {null};
            }
            if (tableNames!=null)
	        {
                if (act == "In")
                {
                    string manifest;
                    string container;
                    //manifest = ForkliftApp.
                    //GetContainersActivity(act);
                }
                else
                {
                    SqlParameter sqlParam = new SqlParameter("@containernumber", ConParms);
                    SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                    spName, ds, tableNames, sqlParam, sqlParam1);
                }
	        }
            return ds;
        }

        /// <summary>
        /// אוסף רשומות למסך פעולות
        /// </summary>
        /// <returns></returns>
        public static ForkliftAppDS GetContainersActivity(string p_manifest,string p_container, string p_InOut)
        {
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string spNameWork = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;
           
            sqlParam1 = new SqlParameter("@container", p_container);
            sqlParam2 = new SqlParameter("@manifest", p_manifest);

            if (p_InOut=="In")
            {
                spName = "sp_ForkLiftContainersActIn";
            }
            else if (p_InOut == "Out")
            {
                spName = "sp_ForkLiftContainersActOut";
            }
            else if (p_InOut == "EMOut")
            {
                spName = "sp_ForkLiftContainersActEMOut";
            }
            else if (p_InOut == "EmptyContainers") 
            {
                spName = "sp_ForkLiftContainersActEM";
            }
            else if (p_InOut == "EmptyLocation")
            {
                spName = "sp_ForkLiftActEmptyLoction";
            }
            else if (p_InOut == "Workes")
            {
                spName = "sp_ForkLiftActWorks";
            }
            tableNames = new string[] { "ContainersAct" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                    spName, ds, tableNames, sqlParam1, sqlParam2);
            return ds;
        }





        public static ForkliftAppDS GetContainersEMOutDetails(string p_CarrierCode, string p_TruckID)   
        {

            ForkliftAppDS dsEMOut = new ForkliftAppDS();
            string[] tableNames = new string[] { "ContainersActEMOut" };
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;
            SqlParameter sqlParam3;
           // SqlParameter sqlParam4;

        //    sqlParam1 = new SqlParameter("@CarrierCode", p_CarrierCode);
      //      sqlParam2 = new SqlParameter("@TruckID", p_TruckID);
            
            sqlParam1 = new SqlParameter("@CarrierCode", p_CarrierCode);
            sqlParam2 = new SqlParameter("@TruckID", p_TruckID);
            sqlParam3 = new SqlParameter("@Terminal", m_Terminal);
            //sqlParam4 = new SqlParameter("@Container", p_container);
            string spName = "sp_ForkLiftContainersActEMOut";

            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                     spName, dsEMOut, tableNames, sqlParam1, sqlParam2, sqlParam3);
            return dsEMOut;
        }
        /// <summary>
        /// אוסף רשומות לעבודות עבור מכולה
        /// </summary>
        /// <param name="p_DN"></param>
        /// <param name="p_SDN"></param>
        /// <returns></returns>
        public static ForkliftAppDS GetWorkForContainer(string p_DN, string p_SDN)
        {
            ForkliftAppDS dsw = new ForkliftAppDS();
            string[] tableNames  = new string[] { "WorkForContainer" };
            SqlParameter sqlParam1 = new SqlParameter("@dealnumber", p_DN);
            SqlParameter sqlParam2 = new SqlParameter("@subdealnumber", p_SDN);
            string spName = "sp_ForkLiftWorksForContainer";

            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                      spName, dsw, tableNames, sqlParam1, sqlParam2);
            return dsw;
        }

        /// <summary>
        /// עידכון רשומת פריקת מכולה
        /// </summary>
        /// <param name="pcontainer"></param>
        /// <param name="pmanifest"></param>
        /// <param name="plocation"></param>
        /// <param name="pdate"></param>
        /// <param name="pforkliftopratorid"></param>
        /// <param name="pentranceforkliftid"></param>
        /// <returns></returns>
        public static int UpdateContainersIn(string p_container, string p_manifest, string p_location, DateTime p_date, string p_forkliftopratorid,string p_entranceforkliftid,string act)
        {
                // Set up parameters (6 input ) 
                SqlParameter[] arParms = new SqlParameter[6];
                // @container Input Parameter 
                arParms[0] = new SqlParameter("@container", SqlDbType.Char);
                arParms[0].Value = p_container;

                // @manifest Input Parameter 
                arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
                arParms[1].Value = p_manifest;

                // @location Input Parameter
                arParms[2] = new SqlParameter("@location", SqlDbType.Char);
                arParms[2].Value = p_location;

                // @forkliftDatein Input Parameter
                arParms[3] = new SqlParameter("@forkliftDatein", SqlDbType.DateTime);
                arParms[3].Value = p_date;

                // @ForkliftOperatorID Input Parameter
                arParms[4] = new SqlParameter("@ForkliftOperatorID", SqlDbType.Char);
                arParms[4].Value = p_forkliftopratorid;

                // @entranceforkliftID Input Parameter
                arParms[5] = new SqlParameter("@entranceforkliftID", SqlDbType.Char);
                arParms[5].Value = p_entranceforkliftid;

                // Call ExecuteNonQuery static method of SqlHelper class
                // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersInUpDate", arParms);

            //}
            return 1;
        }
        
        public static int UpdateContainerLockLocation(string p_container, string p_manifest)
        {
             SqlParameter[] arParms = new SqlParameter[2];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_container;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_manifest;
            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainerLockLocation", arParms);
            return 1;
        }




        /// <summary>
        /// עידכון רשומת פריקת מכולה לא מרשימה
        /// </summary>
        /// <param name="p_container"></param>
        /// <param name="p_manifest"></param>
        /// <param name="p_location"></param>
        /// <param name="p_forkliftdatein"></param>
        /// <param name="p_forkliftoperatorID"></param>
        /// <param name="p_entranceforkliftID"></param>
        /// <param name="p_trucknumber"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static int UpdateContainersInNotinList(string p_container, string p_manifest, string p_location, DateTime p_forkliftdatein, string p_forkliftoperatorID, string p_entranceforkliftID, string p_trucknumber, string act)
        {
            // Set up parameters (8 input ) 
            SqlParameter[] arParms = new SqlParameter[8];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_container;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_manifest;

            // @location Input Parameter
            arParms[2] = new SqlParameter("@location", SqlDbType.Char);
            arParms[2].Value = p_location;

            // @forkliftDatein Input Parameter
            arParms[3] = new SqlParameter("@forkliftDatein", SqlDbType.DateTime);
            arParms[3].Value = p_forkliftdatein;

            // @ForkliftOperatorID Input Parameter
            arParms[4] = new SqlParameter("@ForkliftOperatorID", SqlDbType.Char);
            arParms[4].Value = p_forkliftoperatorID;

            // @entranceforkliftID Input Parameter
            arParms[5] = new SqlParameter("@entranceforkliftID", SqlDbType.Char);
            arParms[5].Value = p_entranceforkliftID;
            
            // @entranceforkliftID Input Parameter
            arParms[6] = new SqlParameter("@indate", SqlDbType.DateTime);
            arParms[6].Value = DateTime.Now;
           
            // @entranceforkliftID Input Parameter
            arParms[7] = new SqlParameter("@TruckNuber", SqlDbType.Char);
            arParms[7].Value = p_trucknumber;
            
            // Call ExecuteNonQuery static method of SqlHelper class
            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersNotinListInUpDate", arParms);
            return 1;
        }

        /// <summary>
        /// עידכון רשומת טעינת מכולה
        /// </summary>
        /// <param name="pcontainer"></param>
        /// <param name="pmanifest"></param>
        /// <param name="pexitforkliftoperatorID"></param>
        /// <param name="pexitforkliftID"></param>
        /// <param name="pforkliftdateOut"></param>
        /// <returns></returns>
        public static int UpdateContainersOut(string p_container, string p_manifest, string p_exitforkliftoperatorID, string p_exitforkliftID, DateTime p_forkliftdateOut)
        {
            // Set up parameters (5 input ) 
            SqlParameter[] arParms = new SqlParameter[5];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_container;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_manifest;

            // @exitforkliftoperatorID Input Parameter
            arParms[2] = new SqlParameter("@exitforkliftoperatorID", SqlDbType.Char);
            arParms[2].Value = p_exitforkliftoperatorID;

            // @exitforkliftID Input Parameter
            arParms[3] = new SqlParameter("@exitforkliftID", SqlDbType.Char);
            arParms[3].Value = p_exitforkliftID;

            // @releaseforkliftDate Input Parameter
            arParms[4] = new SqlParameter("@releaseforkliftDate", SqlDbType.DateTime);
            arParms[4].Value = p_forkliftdateOut;

            // Call ExecuteNonQuery static method of SqlHelper class
            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersOutUpDate", arParms);

            return 1;
        }


        public static int UpdateContainersEMOut(string p_container, string p_TruckID, string p_CarrierCode, string p_exitforkliftoperatorID, string p_exitforkliftID,  DateTime p_forkliftdateOut)
        {
            // Set up parameters (5 input ) 
            SqlParameter[] arParms = new SqlParameter[6];

            arParms[0] = new SqlParameter("@TruckID", SqlDbType.Char);
            arParms[0].Value = p_TruckID;

            arParms[1] = new SqlParameter("@CarrierCode", SqlDbType.Char);
            arParms[1].Value = p_CarrierCode;

            arParms[2] = new SqlParameter("@container", SqlDbType.Char);
            arParms[2].Value = p_container;

            arParms[3] = new SqlParameter("@exitforkliftoperatorID", SqlDbType.Char);
            arParms[3].Value = p_exitforkliftoperatorID;

            // @exitforkliftID Input Parameter
            arParms[4] = new SqlParameter("@exitforkliftID", SqlDbType.Char);
            arParms[4].Value = p_exitforkliftID;

            // @releaseforkliftDate Input Parameter
            arParms[5] = new SqlParameter("@releaseforkliftDate", SqlDbType.DateTime);
            arParms[5].Value = p_forkliftdateOut;



            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersEMOutUpDate", arParms);

            return 1;
        }

        /// <summary>
        /// עידכון רשומת טעינת מכולה לא מרשימה
        /// </summary>
        /// <param name="p_container"></param>
        /// <param name="p_manifest"></param>
        /// <param name="p_exitforkliftoperatorID"></param>
        /// <param name="p_exitforkliftID"></param>
        /// <param name="p_forkliftdateOut"></param>
        /// <param name="p_trucknumber"></param>
        /// <returns></returns>
        public static int UpdateContainersNotinlistOutUpDate(string p_container, string p_manifest, string p_exitforkliftoperatorID, string p_exitforkliftID, DateTime p_forkliftdateOut, string p_trucknumber)
        {
            // Set up parameters (5 input ) 
            SqlParameter[] arParms = new SqlParameter[6];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_container;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_manifest;

            // @exitforkliftoperatorID Input Parameter
            arParms[2] = new SqlParameter("@exitforkliftoperatorID", SqlDbType.Char);
            arParms[2].Value = p_exitforkliftoperatorID;

            // @exitforkliftID Input Parameter
            arParms[3] = new SqlParameter("@exitforkliftID", SqlDbType.Char);
            arParms[3].Value = p_exitforkliftID;

            // @releaseforkliftDate Input Parameter
            arParms[4] = new SqlParameter("@releaseforkliftDate", SqlDbType.DateTime);
            arParms[4].Value = p_forkliftdateOut;

            // @releaseforkliftDate Input Parameter
            arParms[5] = new SqlParameter("@TruckNuber", SqlDbType.Char);
            arParms[5].Value = p_trucknumber;

            // Call ExecuteNonQuery static method of SqlHelper class
            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersNotinlistOutUpDate", arParms);

            return 1;
        }


        /// <summary>
        /// עידכון עיתוק
        /// </summary>
        /// <param name="p_container"></param>
        /// <param name="p_manifest"></param>
        /// <param name="p_location"></param>
        /// <returns></returns>
        public static int UpdateLoction(string p_container, string p_manifest, string p_location)
        {
            // Set up parameters (3 input ) 
            SqlParameter[] arParms = new SqlParameter[3];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_container;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_manifest;

            // @location Input Parameter
            arParms[2] = new SqlParameter("@location", SqlDbType.Char);
            arParms[2].Value = p_location;

           
            // Call ExecuteNonQuery static method of SqlHelper class
            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersLoctionUpDate", arParms);

            return 1;
        }

        public static int UpdateRecommendedLocation(string p_ClientCode, string p_ContainerLength, string p_ContainerType, string p_CarrierCode, string p_RecommendedLocationCode)
        {
            // Set up parameters (5 input ) 
            SqlParameter[] arParms = new SqlParameter[5];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@ClientCode", SqlDbType.Char);
            arParms[0].Value = p_ClientCode;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@ContainerLength", SqlDbType.Char);
            arParms[1].Value = p_ContainerLength;

            // @location Input Parameter
            arParms[2] = new SqlParameter("@ContainerType", SqlDbType.Char);
            arParms[2].Value = p_ContainerType;

            arParms[3] = new SqlParameter("@CarrierCode", SqlDbType.Char);
            arParms[3].Value = p_CarrierCode;

            // @manifest Input Parameter 
            arParms[4] = new SqlParameter("@RecommendedLocationCode", SqlDbType.Char);
            arParms[4].Value = p_RecommendedLocationCode;

            arParms[5] = new SqlParameter("@Terminal", SqlDbType.Char);
            arParms[5].Value = m_Terminal;

            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftUpdateRecommendedLocation", arParms);

            return 1;
        }


        public static bool IsValidUser(string un, string pw,string fn) //string _un, string _pw,
        {
            ForkliftAppDS ds = null;

            DatabaseManager user = new DatabaseManager(un, pw,fn);
            ds = GetForkliftOperatorID(un, pw);
            if (ds.Tables["Forkliftoperatotid"].Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ForkliftAppDS GetForkliftOperatorID(string un,string pw)
        {
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkliftOperatorID";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@username", un);
            sqlParam2 = new SqlParameter("@password", pw);

            tableNames = new string[] { "Forkliftoperatotid" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return ds;
        }

        public static ForkliftAppDS GetContainersQuery(string container,string act, string p_Terminal)
        {
            ForkliftAppDS ds = new ForkliftAppDS();
            ds = GetContainersByNumber(container, act, p_Terminal);
            return ds;
        }

        /// <summary>
        /// אוסף רשומות ליותר ממכולה אחת לספרות
        /// </summary>
        /// <param name="p_Manifest"></param>
        /// <param name="p_container"></param>
        /// <returns></returns>
        public static ForkliftAppDS GetContainersActivityMoreThen1(string p_Manifest, string p_container)
        {
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkLiftActQueryMorethen1";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@container", p_container);
            sqlParam2 = new SqlParameter("@manifest", p_Manifest);

            tableNames = new string[] { "ContainersQuery" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return ds;
        }

        /// <summary>
        /// איסוף רשומות לעבודות לפי תאריך
        /// </summary>
        /// <returns></returns>
        public static ForkliftAppDS GetWorks()
        {
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            spName = "sp_ForkLiftWorks";
            tableNames = new string[] { "Works" };
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames, sqlParam1);

            return ds;
        }

        /// <summary>
        /// איסוף רשומות לעבודות לפי גודל מכולה
        /// </summary>
        /// <param name="p_Size"></param>
        /// <returns></returns>
        public static ForkliftAppDS GetRowsByDate(DateTime p_Date)
        {
            ForkliftAppDS ds = new ForkliftAppDS();
            string[] tableNames = new string[] { "Works" };
            string p_DDate;
            p_DDate =  p_Date.ToShortDateString(); //p_Date.ToString().Substring(3, 2) + p_Date.ToString().Substring(2, 1) + p_Date.ToString().Substring(0, 2) + p_Date.ToString().Substring(5, p_Date.ToString().Length - 5);
            p_DDate = p_DDate.ToString().Substring(3, 2) + p_DDate.ToString().Substring(2, 1) + p_DDate.ToString().Substring(0, 2) + p_DDate.ToString().Substring(5, p_DDate.ToString().Length - 5);
            p_DDate = "'" + p_DDate + "'";
            SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_DDate);
            SqlParameter sqlParam2 = new SqlParameter("@Terminal", m_Terminal);
            string spName = "sp_ForkLiftWorksByDate";

            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                   spName, ds, tableNames, sqlParam1, sqlParam2);
            //dtf=dsw.Tables["WorksBySize"];
            return ds;
        }

        /// <summary>
        /// איסוף נתונים לתיבת רשימה לעבודות
        /// </summary>
        /// <returns></returns>
        public static DataTable getDataForCombo()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkliftWorksCombo";
            string [] tableNames = new string[] { "DataForCombo" };

            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames);
            dt = ds.Tables["DataForCombo"];
            return dt;
        }

        /// <summary>
        /// איסוף נתונים למסך - מכולות ריקות לסוכן
        /// </summary>
        /// <returns></returns>
        public static DataTable GetContainersEM()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkLiftContainersEM";
            string[] tableNames = new string[] { "ContainersEM" };
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
            spName, ds, tableNames, sqlParam1);
            dt = ds.Tables["ContainersEM"];
            return dt;
        }

        /// <summary>
        /// נתונים לקומבו חברת ספנות
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSipingLines()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkliftShipingLinesCombo";
            string[] tableNames = new string[] { "ShipingLines" };
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames, sqlParam1);
            dt = ds.Tables["ShipingLines"];
            return dt;
        }

        public static DataTable GetContainerType()
        {
           DataTable dt = new DataTable();
           ForkliftAppDS ds = new ForkliftAppDS();
           //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
           string spName = "sp_ForkliftContainerTypeCombo";
           string[] tableNames = new string[] { "ContainerType" };
           SqlHelper.FillDataset(DatabaseManager.GetConnection(),
             spName, ds, tableNames);
           dt = ds.Tables["ContainerType"];
           return dt;
        }


        public static DataTable GetSpecialLocation()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;

            spName = "sp_ForkliftSpecialLocationCombo";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);

            tableNames = new string[] { "tbSpecialLocation" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1);

            return dt = ds.Tables["tbSpecialLocation"];

        }


        public static DataTable GetHazardousSubstances(string ClassificationClassCode)
        {
             DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;
            SqlParameter sqlParam3;

            spName = "sp_ForkLiftForHazardousSubstancesConts";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@ClassificationClassCode", ClassificationClassCode.Trim());
            sqlParam2 = new SqlParameter("@ClassificationClassCode", ClassificationClassCode.Trim());
            sqlParam3 = new SqlParameter("@Terminal", m_Terminal);
            tableNames = new string[] { "tbContsForLoction" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam3);

            return dt = ds.Tables["tbContsForLoction"];
        
        }




        public static DataTable GetCboSubstances()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkliftHazardousSubstances";
            string[] tableNames = new string[] { "ShipingLines" };

            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames);
            dt = ds.Tables["ShipingLines"];
            return dt;
        }












        
        
        /// <summary>
        /// נתונים למסך - יומן תנועות
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmtyLoction()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkLiftEmptyLoction";
            string[] tableNames = new string[] { "NoLoctuion" };
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames, sqlParam1);
            dt = ds.Tables["NoLoctuion"];
            return dt;
        }

         public static DataTable GetExpectedContainers()
         {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            SqlParameter sqlParam1;
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkLiftExpectedContainers";
            string[] tableNames = new string[] { "ExpectedContainers" };
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames, sqlParam1);
            dt = ds.Tables["ExpectedContainers"];
            return dt;
        }

         public static DataTable GetGatePassNoPaper(string p_container)
         {
             DataTable dt = new DataTable();
             ForkliftAppDS ds = new ForkliftAppDS();
             SqlParameter sqlParam1;

             string spName = "sp_ForkLiftContainersNoPaper";
             //tableNames = new string[] { "ContainersActOut" };
             sqlParam1 = new SqlParameter("@ContainerNumber", p_container);

             string[] tableNames = new string[] { "tbContainersNoPaper" };
             SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                 spName, ds, tableNames, sqlParam1);

             return dt = ds.Tables["tbContainersNoPaper"];
         }

         public static DataTable GetLockLocation(string p_container, string p_manifest)
         {
             DataTable dt = new DataTable();
             ForkliftAppDS dsCheckLock = new ForkliftAppDS();
             string spName = "sp_ForkLiftCheckLock";
             string[] tableNames = new string[] { "CheckContainerLock" };
             SqlParameter sqlParam1;
             SqlParameter sqlParam2;
             sqlParam1 = new SqlParameter("@Container", p_container);
             sqlParam2 = new SqlParameter("@manifest", p_manifest);

             SqlHelper.FillDataset(DatabaseManager.GetConnection(), spName, dsCheckLock, tableNames, sqlParam1, sqlParam2);
             return dt = dsCheckLock.Tables["CheckContainerLock"];
         }

         public static DataTable GetCheckEMOut(string p_ContainerNum, string p_CarrierCode, string p_ContainerTypeCode, string p_ContainerLength )
         {
             DataTable dt = new DataTable();
             ForkliftAppDS dsCheckEMOut = new ForkliftAppDS();
             string spName = "sp_ForkLiftContainersCheckEMOut";
             string[] tableNames = new string[] { "CheckContainerEMOut" };
             SqlParameter sqlParam1;
             SqlParameter sqlParam2;
             SqlParameter sqlParam3;
             SqlParameter sqlParam4;
             SqlParameter sqlParam5;

             sqlParam1 = new SqlParameter("@ContainerNumber", p_ContainerNum);
             sqlParam2 = new SqlParameter("@ContainerTypeCode", p_CarrierCode);
             sqlParam3 = new SqlParameter("@ContainerLength", p_ContainerTypeCode);
             sqlParam4 = new SqlParameter("@CarrierCode", p_ContainerLength);
             sqlParam5 = new SqlParameter("@Terminal", m_Terminal);

             SqlHelper.FillDataset(DatabaseManager.GetConnection(), spName, dsCheckEMOut, tableNames, sqlParam1, sqlParam2, sqlParam3, sqlParam4, sqlParam5);
             return dt = dsCheckEMOut.Tables["CheckContainerEMOut"];
         }


         public static DataTable GetRecommendedLocation()
         {
             DataTable dt = new DataTable();
             ForkliftAppDS ds = new ForkliftAppDS();
             //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
             string spName = "sp_ForkLiftRecommendedLocation";
             string[] tableNames = new string[] { "RecommendedLocation" };
             SqlParameter sqlParam1;
             sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
             SqlHelper.FillDataset(DatabaseManager.GetConnection(),
               spName, ds, tableNames, sqlParam1);
             dt = ds.Tables["RecommendedLocation"];
             return dt;
         }






        public static void UpdateWorksForklift(string p_container, string p_manifest)
        {
            SqlParameter[] arParms = new SqlParameter[2];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_container;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_manifest;

            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftWorksUpdate", arParms);
        }

        public static DataTable GetInOutDiory()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkLiftInOutDiory";
            string[] tableNames = new string[] { "InOutDiory" };
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
            spName, ds, tableNames, sqlParam1);
            dt = ds.Tables["InOutDiory"];
            return dt;
        }

        public static DataTable Getdtm(string stcontainer, string stmanifest)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkLiftActQueryMorethen1";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@container", stcontainer);
            sqlParam2 = new SqlParameter("@manifest", stmanifest);

            tableNames = new string[] { "QueryContainers" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["QueryConts"];
        }


        public static DataTable GetExpectedForLoction(string loction)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;
            spName = "sp_ForkLiftExpectedForLoction";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@locationCode", loction);
            sqlParam2 = new SqlParameter("@Terminal", m_Terminal);
            tableNames = new string[] { "ExpectedForLoction" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["tbExpectedForLoction"];
        }

        public static DataTable GetContsInForLoction(string loction)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkLiftFInForLoction";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@locationCode", loction.Trim());
            sqlParam2 = new SqlParameter("@Terminal", m_Terminal);

            tableNames = new string[] { "InForLoction" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["InForLoction"];
        }

        public static DataTable GetLoction()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            //SqlParameter sqlParam1 = new SqlParameter("@targetdate", p_Date);
            string spName = "sp_ForkliftLoctionCmbo";
            string[] tableNames = new string[] { "ForkliftLoctionCmbo" };
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
              spName, ds, tableNames, sqlParam1);
            dt = ds.Tables["ForkliftLoctionCmbo"];
            return dt;
        }

        
        public static DataTable GetAreaLoction(string loction)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;
            spName = "sp_ForkLiftForAreaLoctionConts";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@locationCode", loction.Trim());
            sqlParam2 = new SqlParameter("@Terminal", m_Terminal);
            tableNames = new string[] { "tbContsForAreaLoction" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["tbContsForAreaLoction"];
        }


        public static DataTable GetContsForLoction(string loction)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkLiftForLoctionConts";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@locationCode", loction.Trim());
            sqlParam2 = new SqlParameter("@Terminal", m_Terminal);
            tableNames = new string[] { "tbContsForLoction" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["tbContsForLoction"];
        }

        public static DataTable GetWorksData()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);

            spName = "sp_ForkLiftWorks";
            tableNames = new string[] { "WorksData" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1);

            return dt = ds.Tables["WorksData"];
        }

        public static DataTable GetDamage(string manifest, string container)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkliftDamage";
            sqlParam1 = new SqlParameter("@container", container);
            sqlParam2 = new SqlParameter("@manifest", manifest);

            tableNames = new string[] { "tbDamage" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["tbDamage"];
        }

        /// <summary>
        /// טבלת מכולות בעיסקה
        /// </summary>
        /// <param name="_DealNumber"></param>
        /// <returns></returns>
     
        
        public static DataTable GetConsForDealNumber(string _DealNumber)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;
            spName = "sp_ForkliftForDealNumberConts";
            //tableNames = new string[] { "ContainersActOut" };
            sqlParam1 = new SqlParameter("@@dealnumber", _DealNumber);
            sqlParam2 = new SqlParameter("@Terminal", m_Terminal);
            tableNames = new string[] { "tbContsForDealNumber" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["tbContsForDealNumber"];
        }




        public static DataTable GetConsForReserve()
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            sqlParam1 = new SqlParameter("@Terminal", m_Terminal);
            spName = "sp_ForkliftForReserveConts";
            //tableNames = new string[] { "ContainersActOut" };
           

            tableNames = new string[] { "tbContsForDealNumber" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(), spName, ds, tableNames, sqlParam1);

            return dt = ds.Tables["tbContsForDealNumber"];
        }
        
        
        /// <summary>
        /// עידכון הערות למלגזן ע"י אבי אוחיון
        /// </summary>
        /// <param name="p_stmanifest"></param>
        /// <param name="p_stcontainer"></param>
        /// <param name="p_comment"></param>
        public static void UpDateComment(string p_stmanifest, string p_stcontainer, string p_comment)
        {
            // Set up parameters (6 input ) 
            SqlParameter[] arParms = new SqlParameter[3];
            // @container Input Parameter 
            arParms[0] = new SqlParameter("@container", SqlDbType.Char);
            arParms[0].Value = p_stcontainer;

            // @manifest Input Parameter 
            arParms[1] = new SqlParameter("@manifest", SqlDbType.Char);
            arParms[1].Value = p_stmanifest;

            // @location Input Parameter
            arParms[2] = new SqlParameter("@marksNumbers", SqlDbType.Char);
            arParms[2].Value = p_comment;

            // Call ExecuteNonQuery static method of SqlHelper class
            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
            SqlHelper.ExecuteNonQuery(DatabaseManager.GetConnection(), CommandType.StoredProcedure, "sp_ForkLiftContainersCommentUpDate", arParms);
        }

        public static DataTable GetExistingLocation(string _NewLocation, string _Terminal)
        {
            DataTable dt = new DataTable();
            ForkliftAppDS ds = new ForkliftAppDS();
            string spName = string.Empty;
            string[] tableNames;
            SqlParameter sqlParam1;
            SqlParameter sqlParam2;

            spName = "sp_ForkliftExistingLocation";
            sqlParam1 = new SqlParameter("@Location", _NewLocation);
            sqlParam2 = new SqlParameter("@Terminal", _Terminal);
            tableNames = new string[] { "tbExistingLocation" };
            SqlHelper.FillDataset(DatabaseManager.GetConnection(),
                spName, ds, tableNames, sqlParam1, sqlParam2);

            return dt = ds.Tables["tbExistingLocation"];
        }
    }
}

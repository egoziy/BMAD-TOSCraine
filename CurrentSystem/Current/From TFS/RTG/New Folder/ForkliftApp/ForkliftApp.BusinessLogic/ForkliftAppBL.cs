using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForkliftApp.Framework;
using ForkliftApp.DataAccess;
using ForkliftApp.Entities;
using System.Data;


namespace ForkliftApp.BusinessLogic
{
    public class ForkliftAppBL
    {
        private DatabaseManager dbm = null;
        private static string m_manifest;
        private ForkliftAppDS ds;
        private static DataTable m_dtdeal;
        private static DataTable dtEL;
        private string username;
        private string password;
        public static string m_Terminal = string.Empty;
        public static string m_SpecialLocation = string.Empty;
        
        #region Propertis

        private static bool m_IsSame;

        public static bool IsSame
        {
            get { return ForkliftAppBL.m_IsSame; }
            set { ForkliftAppBL.m_IsSame = value; }
        }

        private static string m_ForkLiftAppVersia;

        private static string m_Comment;
        private static string m_CarrierCode;
        private static string m_TruckID;

        public static string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public static string ForkLiftAppVertzia
        {
            get { return m_ForkLiftAppVersia; }
            set { m_ForkLiftAppVersia = value; }
        }

        private static int m_ForkLiftNum;

        public static int ForkLiftNum
        {
            get { return m_ForkLiftNum; }
            set { m_ForkLiftNum = value; }
        }
        
        public string Manifest
        {
            get { return m_manifest; }
            set { m_manifest = value; }
        }

        private static string m_Container;

        public string Container
        {
            get { return m_Container; }
            set { m_Container = value; }
        }

        private static  string m_DealNumber;

        public string DealNumber
        {
            get { return m_DealNumber; }
            set { m_DealNumber = value; }
        }

        private static string m_DealNumberSub;

        public string DealNumberSub
        {
            get { return m_DealNumberSub; }
            set { m_DealNumberSub = value; }
        }

        private static string m_activety;

        public static string Activety
        {
            get { return m_activety; }
            set { m_activety = value; }
        }

        private static bool m_ForkliftDoune;

        public static bool ForkliftDoune
        {
            get { return m_ForkliftDoune; }
            set { m_ForkliftDoune = value; }
        }

        private static DataTable dt;

        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; }
        }

        private static DataSet dsmore;

        public DataSet Dsmore
        {
            get { return dsmore; }
            set { dsmore = value; }
        }

        private static string m_Kaind;

        public string Kaind
        {
            get { return m_Kaind; }
            set { m_Kaind = value; }
        }

        private string m_WorkType;

        
        public string m_ClientCode;
        public string m_ContainerLength;
        public string m_ContainerTypeCode;
        public string m_ShipingLine;
        public string m_RecommendedLocation;


        public string WorkType
        {
            get { return m_WorkType; }
            set { m_WorkType = value; }
        }

        private static bool m_IsEM;

        public static bool IsEM
        {
            get { return ForkliftAppBL.m_IsEM; }
            set { ForkliftAppBL.m_IsEM = value; }
        }

        #endregion

        #region Constractors

        public ForkliftAppBL()
        { }

        public ForkliftAppBL(bool _IsEM)
        {
            m_IsEM = _IsEM;
        }

        public ForkliftAppBL(int forkliftnum,string forkliftappversia)
        {
            m_ForkLiftNum = forkliftnum;
            m_ForkLiftAppVersia = forkliftappversia;
        }

        public ForkliftAppBL(string ClientCode, string ContainerLength, string ContainerTypeCode, string ShipingLine, string RecommendedLocation)
        {
            m_ClientCode = ClientCode;
            m_ContainerLength = ContainerLength;
            m_ContainerTypeCode = ContainerTypeCode;
            m_ShipingLine = ShipingLine;
            m_RecommendedLocation = RecommendedLocation;
        }

        public ForkliftAppBL(string CarrierCode, string TruckID)
        {
            m_CarrierCode = CarrierCode;
            m_TruckID = TruckID;
        }


        public ForkliftAppBL(string manifest, string container, string dealnymber, string dealnymbersub, bool forkliftdoune,string worktype)
        {
            m_manifest = manifest;
            m_Container = container;
            m_DealNumber = dealnymber;
            m_DealNumberSub = dealnymbersub;
            m_ForkliftDoune = forkliftdoune;
            m_WorkType = worktype;
        }

        public ForkliftAppBL(string manifest, string container,string comment)
        {
            m_manifest = manifest;
            m_Container = container;
            m_Comment = comment;
        }

        public ForkliftAppBL(string activety)
        {
            m_activety = activety;
        }

 

        public ForkliftAppBL(DataTable _dt)
        {
            dt = _dt;
        }

        public ForkliftAppBL(DataSet _dsMore)
        {
            dsmore = _dsMore;
        }
 
        #endregion

        /// <summary>
        /// מחזיר ערך למסך פעילויות
        /// </summary>
        /// <returns></returns>
        public static string GetActivity()
        {
            string act;
            if (m_activety=="In")
            {
                act = "In";
            }
            else if (m_activety == "NotinListIn")
            {
                act = "NotinListIn";
            }
            else if(m_activety=="Out")
            {
                act = "Out";
            }
            else if (m_activety == "NotinLisOut")
            {
                act = "NotinLisOut";
            }
            else if (m_activety=="EmptyContainers")
            {
                act = "EmptyContainers";
            }
            else if (m_activety == "EmptyLocation")
            {
                act = "EmptyLocation";
            }
            else if (m_activety == "Workes")
            {
                act = "Workes";
            }
            else if (m_activety == "InOutDiory")
            {
                act = "InOutDiory";
            }
            else if (m_activety == "ContainersForLoction")
            {
                act = "ContainersForLoction";
            }
            else if (m_activety == "ExpectedContainers")
            {
                act = "ExpectedContainers";
            }
            else if (m_activety == "RecommendedLocation")
            {
                act = "RecommendedLocation";
            }
            else if (m_activety == "EMOut")
            {
                act = "EMOut";
            }
            else
            {
                act = "Query";
            }
            return act;
        }

        /// <summary>
        /// קבלת מיצהר אחרון דנבחר
        /// </summary>
        /// <returns></returns>
        public static string GetManifest()
        {
            return m_manifest;
        }

        public static string GetCarrierCode()
        {
            return m_CarrierCode;
        }

        public static string GetTruckID()
        {
            return m_TruckID;
        }

        /// <summary>
        /// קבלת מכולה אחרונה שנבחרה
        /// </summary>
        /// <returns></returns>
        public static string GateContainer()
        {
            return m_Container;
        }

        /// <summary>
        /// קבלת אותיות של מספר מכולה
        /// </summary>
        /// <param name="pContAlpha"></param>
        /// <returns></returns>
        public static string GetDataForContAlpha(string pContAlpha)
        {
            string st = string.Empty;
            if (pContAlpha!=string.Empty)
            {
                for (int i = 0; i < 4; i++)
                {
                    st += pContAlpha[i];
                }
            }

            return st;
        }

        /// <summary>
        /// קבלת סוג רשומות למסך עבודו
        /// </summary>
        /// <returns></returns>
        public static string GetKaind()
        {
            return m_Kaind;
        }

        /// <summary>
        /// יציאה מהתוכנית
        /// </summary>
        public static void ExitAppliction()
        {
            if (MessageBox.Show("האם לצאת מהתוכנית?",
                "אישור יציאה",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                
                
                
                Application.Exit();
        }

        /// <summary>
        /// פרוצדורה פרטית לקבלת מספר תעודת זהות של המפעיל
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetForkliftOperatorID(string username, string password)
        {
            string st=st=string.Empty;
            ds = ForkliftAppDA.GetForkliftOperatorID(username, password);
            dt = new DataTable();
            dt = ds.Tables["Forkliftoperatotid"];
            DataRow[] rows = dt.Select();

            if (ds!=null)
            {
                st +=(rows[0][0]).ToString();
            }
            return st;
        }

        /// <summary>
        /// פרוצדורה ציבורית לקבלת מספר מלגזה
        /// </summary>
        /// <returns></returns>
        public string GetForkliftID()
        {
            string forkliftid = string.Empty;
            dbm = new DatabaseManager();
            forkliftid = dbm.GetForkliftNumber();
            return forkliftid;
        }

        /// <summary>
        /// פרוצדורה ציבורית לקבלת מספר סידורי בעיסקה.
        /// </summary>
        /// <returns></returns>
        public static string GetSumDealNumber()
        {
            return m_DealNumberSub;
        }

        /// <summary>
        /// חישוב זמן סגירת מסך ופתיחת מסך כניסה למערכת
        /// </summary>
        /// <param name="TimeForExit"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public int PreDateDiffForExit(DateTime _TimeForExit, DateTime _endtime)
        {
            int MinithFromStart = DateDiffForExit(_TimeForExit, _endtime);
            return MinithFromStart;
        }
        
        /// <summary>
        /// חישוב זמן סגירת מסך ופתיחת מסך כניסה למערכת
        /// </summary>
        /// <param name="TimeForExit"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        private int DateDiffForExit(DateTime timeForExit, DateTime Endtime)
        {
            return Endtime.Subtract(timeForExit).Minutes;
        }

        /// <summary>
        /// מחזיר טבלה עם יותר ממכולה 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDT()
        {
            return dt;
        }

        public static DataTable Getdtmore(string stcontainer, string stmanifest)
        {
            DataTable dtm = new DataTable();
            dtm = ForkliftAppDA.Getdtm(stcontainer,stmanifest);
            return dtm;
        }

        public static DataTable GetExpectedForLoction(string loction)
        {
            DataTable dtec = new DataTable();
            dtec = ForkliftAppDA.GetExpectedForLoction(loction);
            return dtec;
        }

        public static DataTable GetContsInForloction(string loction)
        {
            DataTable dtinl = new DataTable();
            dtinl = ForkliftAppDA.GetContsInForLoction(loction);
            return dtinl;
        }

        public static DataTable GetWorsData()
        {
            DataTable dtw = new DataTable();
            dtw = ForkliftAppDA.GetWorksData();
            return dtw;
        }

        public static DataTable GetDamage(string manifest, string container)
        {
            DataTable dtd = new DataTable();
            dtd = ForkliftAppDA.GetDamage(manifest, container);
            return dtd;
        }

        public static DataTable GetConsForDealNumber(string _DealNumber)
        {
            m_dtdeal = new DataTable();
            m_dtdeal = ForkliftAppDA.GetConsForDealNumber(_DealNumber);
            //if (m_dtdeal.Rows.Count == 0)
            //{
            //    m_dtdeal = null;
            //}
            return m_dtdeal;
        }


        public static DataTable GetConsReserve()
        {
            m_dtdeal = new DataTable();
            m_dtdeal = ForkliftAppDA.GetConsForReserve();
            //if (m_dtdeal.Rows.Count == 0)
            //{
            //    m_dtdeal = null;
            //}
            return m_dtdeal;
        }

        public static DataTable GetExistingLocation(string _NewLocation, string _Terminal)
        {
            dtEL = new DataTable();
            dtEL = ForkliftAppDA.GetExistingLocation(_NewLocation, _Terminal);
            return dtEL;
        }


        public static DataTable GetTableForGrid()
        {
            return m_dtdeal;
        }

        public static string GetUserName()
        {
            string user = DatabaseManager.User;
            return user;
        }

        public static string GetVersia()
        {
            return m_ForkLiftAppVersia;
        }

        public static int GetForkLiftNum()
        {
            return m_ForkLiftNum;
        }

        public static string GetComment()
        {
            return m_Comment;
        }

        /// <summary>
        /// פרוצדורה ציבורית לקבלת מספר תעודת זהות של המפעיל 
        /// </summary>
        /// <returns></returns>
        public string GetForkliftOperatorID()
        {

            string ForkliftOperatorID = string.Empty;
            username = string.Empty;
            password = string.Empty;
            dbm = new DatabaseManager();
            username = dbm.GetUserName();
            password = dbm.GetPassWoed();
            ForkliftOperatorID = GetForkliftOperatorID(username, password);

            return ForkliftOperatorID;
        }

        /// <summary>
        /// מחזיר אוסף רשומות ליותר ממכולה אחת לפי ספרות
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDSMore()
        {
            return dsmore;
        }

        /// <summary>
        /// פרוצדורה ציבורית לקבלת מספר עיסקה
        /// </summary>
        /// <returns></returns>
        public static string GetDealNymber()
        {
            return m_DealNumber;
        }

    }
}

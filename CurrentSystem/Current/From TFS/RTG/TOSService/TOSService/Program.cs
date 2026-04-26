using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;


using System.IO;

using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace TOSService
{

    class Program
    {
         void Main(string[] args)
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                WriteLog(" Program allredy running ");
                return;
            }

            WriteLog("START Program");
            while (true)
            {

                //   TOS("Gold1", 30701);
                TOS("Gold2", 30702);
                //   TOS("Gold3", 30703);
                WriteLog("take 1 minute 0ff");
                Thread.Sleep(1000);
            }

        }

         void TOS(string gold, Int32 port)
        {

            TcpListener server = null;
            try
            {
                string ContainerPick = "";
                string FromLocationStr = "";
                string ToLocationStr = "";
                string OperatorIDPick = "";
                string TruckTypeStr = "";


                IPAddress localAddr = IPAddress.Parse("192.6.8.52");
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();
                Byte[] bytes1 = new Byte[256];

                Byte[] bytes = new Byte[256];
                String data = null;
                String data1 = null;

                WriteLog("Waiting for a connection " + gold + " ... ");

                // Perform a blocking call to accept requests.
            
                TcpClient client = server.AcceptTcpClient();
                WriteLog("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream_Read = client.GetStream();

               NetworkStream stream_Write = client.GetStream();

                try
                {

                    Crc16Ccitt CC = new Crc16Ccitt();
                    DataTable dt = CC.ReturnDT("SELECT Message FROM dbo.RG_B3 WHERE  CHE = '" + gold + "'  AND A3Date IS NULL  AND (PickDate IS NULL)  UNION ALL SELECT  MessageCancel FROM dbo.RG_B3 WHERE   (CHE = '" + gold + "') AND (MessageCancel IS NOT NULL) AND (CancelDate IS NULL)");


                    // send messages to crane
                    if (dt.Rows.Count > 0)
                  
                    {
                        for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                        {
                            data = "";
                            byte[] msg1 = ToByteArray(dt.Rows[k][0].ToString());
                            stream_Write.Write(msg1, 0, msg1.Length);
                            data1 = System.Text.Encoding.ASCII.GetString(msg1, 0, msg1.Length);
                            WriteLog(String.Format("Sent: {0}", data1));

                        }
                    }


                    // Loop to receive all the data sent by the client.
                    int i = 0;
               //     while (( i = stream_Read.Read(bytes, 0, bytes.Length)) != 0)
                  {

                        i = stream_Read.Read(bytes, 0, bytes.Length);
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        // WriteLog("Received: {0}", data);
                        // Process the data sent by the client.
                        WriteLog(data);
                        data = data.ToUpper();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        // Send back a response.
                        //  stream.Write(msg, 0, msg.Length);

                        if (data.Substring(0, 2) == "??")
                        {
                            if (data.Substring(4, 2) == "A1")
                            {

                                dt = CC.ReturnDT("UPDATE dbo.RG_A1  set    Time= '" + data.Substring(12, 6) + "', " +
                                                                     "   Status ='" + data.Substring(18, 4) + "', " +
                                                                  "  HBBlockName='" + data.Substring(22, 8) + "', " +
                                                                 "  HBBayNumber ='" + data.Substring(30, 3) + "', " +
                                                                 "  HBRowNumber ='" + data.Substring(33, 3) + "', " +
                                                                   "   HBHeight ='" + data.Substring(36, 4) + "', " +
                                                                  " CraneStatus ='" + data.Substring(40, 2) + "', " +
                                                                   "  GPSStatus ='" + data.Substring(42, 2) + "', " +
                                                                        "   CHE ='" + data.Substring(44, 5) + "', " +
                                                                         "  PLC ='" + data.Substring(55, 2) + "', " +
                                                                          " Len ='" + data.Substring(75, 2) + "', " +
                                                                    " TwistLock ='" + data.Substring(77, 1) + "' WHERE CHE = '" + gold + "'");

                            }
                            if (data.Substring(4, 2) == "A2")
                            {

                                if (data.Substring(14, 2) == "03")  // PICK
                                {

                                    // בודק במקרה שהוכנסה מכולה מG T ללא גוב
                                    if (data.Substring(89, 1).ToString() == "G" || data.Substring(89, 1).ToString() == "T")
                                    {

                                        dt = CC.ReturnDT("SELECT Count(*) as Counter FROM  dbo.RG_Container WHERE CHE = '" + gold + "'");
                                        if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 0)
                                        {
                                            dt = CC.ReturnDT("UPDATE dbo.TB_Parameters SET ContainerPick2 ='TRUE'");
                                        }
                                    }


                                    // Counter='" + data.Substring(12, 2) + "'  AND
                                    dt = CC.ReturnDT("UPDATE RG_B3 SET PickDate = GetDate() WHERE (CounterID =(SELECT MAX(CounterID) AS Expr1 FROM dbo.RG_B3 AS RG_B3_1 WHERE  CHE = '" + gold + "')   AND CHE = '" + gold + "')");
                                    dt = CC.ReturnDT("SELECT Container FROM dbo.TB_Location  WHERE   LocationCode = '" + data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1) + "' AND BlocCode = '" + data.Substring(78, 5) + "'");
                                    if (dt.Rows.Count > 0)
                                    {
                                        ContainerPick = dt.Rows[0][0].ToString();
                                        dt = CC.ReturnDT("SELECT OperatorID, TruckType FROM  dbo.RG_B3 WHERE (CounterID =(SELECT MAX(CounterID) AS Expr1 FROM dbo.RG_B3 AS RG_B3_1 WHERE  CHE = '" + gold + "')   AND CHE = '" + gold + "')");
                                        OperatorIDPick = dt.Rows[0][0].ToString();
                                        TruckTypeStr = dt.Rows[0][1].ToString();

                                        FromLocationStr = data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1);


                                        byte[] msg1 = StrToByteArray("FFFF" + ReturnAsciText("04B2" + data.Substring(12, 2) + calcChecksum("04B2" + data.Substring(12, 2))));

                                        stream_Write.Write(msg1, 0, msg1.Length);

                                        // FFFF30344232304346454235    B2
                                    }

                                }


                                if (data.Substring(14, 2) == "04")  // PLACE
                                {
                                    ToLocationStr = data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1);
                                    dt = CC.ReturnDT("UPDATE RG_B3 SET PlaceDate = GetDate() WHERE (CounterID =(SELECT MAX(CounterID) AS Expr1 FROM dbo.RG_B3 AS RG_B3_1 WHERE  CHE = '" + gold + "')   AND CHE = '" + gold + "')");
                                    dt = CC.ReturnDT("SELECT COUNT(Container) AS Counter FROM     dbo.RG_Container WHERE CHE = '" + gold + "'");
                                    if (Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                                    {
                                        dt = CC.ReturnDT("SELECT Container, OperatorID FROM  dbo.RG_Container WHERE CHE = '" + gold + "'");
                                        ContainerPick = dt.Rows[0][0].ToString();
                                        OperatorIDPick = dt.Rows[0][1].ToString();

                                        dt = CC.ReturnDT("DELETE dbo.RG_Container WHERE CHE = '" + gold + "'");
                                        dt = CC.ReturnDT("UPDATE dbo.CO_Containers SET EntranceForkliftDate = GetDate(), EntranceForkliftOperatorID='" + OperatorIDPick + "', EntranceForkliftNumber='1'   WHERE   Container  =  '" + ContainerPick + "' AND EntranceDate is not null AND  EntranceForkliftDate IS NULL");

                                    }

                                    if (ContainerPick.Trim().Length == 11)
                                    {
                                        dt = CC.ReturnDT("INSERT  dbo.RG_Shifting (OperatorID, CHE, BlockName, ShiftDate, Container, FromLocation, ToLocation) " +
                                             "  SELECT     dbo.RG_Log.OperatorID , dbo.RG_Log.CHE, '" + data.Substring(78, 5) + "', GetDate() , '" + ContainerPick + "','" +
                                               FromLocationStr + "','" + ToLocationStr + "' " +
                                             "  FROM  dbo.V_RG_CurrentOperator INNER JOIN " +
                                              " dbo.RG_Log ON dbo.V_RG_CurrentOperator.LoginDate = dbo.RG_Log.LoginDate AND dbo.V_RG_CurrentOperator.CHE = dbo.RG_Log.CHE " +
                                              " WHERE     (dbo.RG_Log.CHE = '" + gold + "')");

                                        dt = CC.ReturnDT("UPDATE dbo.TB_Location SET Container  = Null , CHE  = '" + gold + "'  WHERE Container = '" + ContainerPick + "'");

                                        if (data.Substring(89, 1).ToString() == "G" || data.Substring(89, 1).ToString() == "T")
                                        {
                                            //  dt = CC.ReturnDT("UPDATE dbo.TB_Location SET Container  =  '" + ContainerPick + "'  WHERE     REPLACE(LocationCode, '-', '') = '" + data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1) + "' AND BlocCode = '" + data.Substring(78, 5) + "'");
                                            dt = CC.ReturnDT("UPDATE dbo.CO_Containers SET  RtgWeight = '" + data.Substring(40, 5) + "',LocationCode  = '" + TruckTypeStr + "', ReleaseForkliftDate = GetDate(), ExitForkliftOperatorID='" + OperatorIDPick + "', ExitForkliftNumber='1'   WHERE   DATEDIFF(minute, CASE WHEN EntranceForkliftDate IS NULL THEN getdate() - 2 ELSE EntranceForkliftDate END, GETDATE()) > 1  AND  Container  =  '" + ContainerPick + "' AND (EntranceDate is not null  OR RegisterDate is not null) AND (ExitDate IS NULL OR ExitGateDate IS NULL) ");
                                            TruckTypeStr = "";
                                        }
                                        else
                                        {
                                            dt = CC.ReturnDT("UPDATE dbo.TB_Location SET Container  =  '" + ContainerPick + "', CHE = '" + gold + "'  WHERE    LocationCode  = '" + data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1) + "' AND BlocCode = '" + data.Substring(78, 5) + "'");
                                            dt = CC.ReturnDT("UPDATE dbo.CO_Containers SET  RtgWeight = '" + data.Substring(40, 5) + "',LocationCode  = '" + data.Substring(86, 3) + data.Substring(89, 1) + data.Substring(92, 1) + "'  WHERE   Container  =  '" + ContainerPick + "' AND (EntranceDate is not null  OR RegisterDate is not null) AND (ExitDate IS NULL OR ExitGateDate IS NULL) ");
                                        }
                                    }
                                    byte[] msg1 = StrToByteArray("FFFF" + ReturnAsciText("04B2" + data.Substring(12, 2) + calcChecksum("04B2" + data.Substring(12, 2))));
                                    stream_Write.Write(msg1, 0, msg1.Length);
                                }
                            }
                            if (data.Substring(4, 2) == "A3")
                            {
                                dt = CC.ReturnDT("UPDATE RG_B3 SET CancelDate  = GetDate() WHERE  A3Date IS NOT NULL AND  Counter='" + data.Substring(6, 2) + "' AND CHE = '" + gold + "'");
                                dt = CC.ReturnDT("UPDATE RG_B3 SET A3Date  = GetDate() WHERE  A3Date IS  NULL AND  Counter='" + data.Substring(6, 2) + "' AND CHE = '" + gold + "'");

                            }
                            if (data.Substring(4, 2) != "A1" || data.Substring(4, 2) != "A2" || data.Substring(4, 2) != "A3")
                            {
                                byte[] msg2 = StrToByteArray("FFFF303342313046454641");
                                stream_Write.Write(msg2, 0, msg2.Length);
                            }

                        }
                    }
                }
                catch (Exception e)
                {

                    WriteLog(String.Format("{1}   : Exception: {0}", e.ToString(), DateTime.Now.ToString()) + Environment.NewLine);
                    WriteLog("Stop!");
                    server.Stop();
                }

            }
            catch (Exception e)
            {
                WriteLog(String.Format("{1}   : SocketException: {0}", e.ToString(), DateTime.Now.ToString()) + Environment.NewLine);
                WriteLog("Stop!");
                server.Stop();
            }
            finally
            {
                WriteLog("Stop!");
                server.Stop();
            }
            WriteLog("Stop!");
            server.Stop();
        }
        static string calcChecksum(string instr)
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
        public static byte[] ToByteArray(String hexString)
        {
            byte[] retval = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
                retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            return retval;
        }
        public static byte[] StrToByteArray(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (int i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte)i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
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
            WriteLog(final);

            return final;

        }
        public static void WriteLog(string strLog)
        {
            try
            {

                string logFilePath = @"\\Broadcast\Logs\RTG\TosService\Log_" + System.DateTime.Now.DayOfYear.ToString() + "." + "txt";


                FileInfo logFileInfo = new FileInfo(logFilePath);
                DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
                {
                    using (StreamWriter log = new StreamWriter(fileStream))
                    {
                        Console.WriteLine(DateTime.Now + strLog);
                        log.WriteLine(string.Format("version 2022-03-21 {0} : {1} ", DateTime.Now, strLog));
                    }
                }
            }
            catch
            {
                //רק כדי לא לעוף אם יש בעיה ברישום ללוג
            }
        }





    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Net;
using System.Timers;
using System.Management;
using System.Net.Sockets;

namespace hardware.bluetooth
{
    public class SerialPortConnect
    {
        /// <summary>
        /// 实例化串口通讯类
        /// </summary>
        static SerialPort _sp = new SerialPort();
        /// <summary>
        /// /58.49.58.149/URUM0/fmy/fmy123
        /// 
        /// 
        /// IP：113.57.219.8:12307
        ///端口： 12307
        ///用户名：kqgps 密码： kqgnss
        ///源节点有三个：KQT816061010，KQT817011002，KQT816031001
        /// </summary>
        static string _address;
        static public string ip = "";
        static public int port;
        static string _mountPoint;
        static string _account;
        static string _key;
        static string _basicAccountAndKey = "";
        public static int RtcmConnectNumber = 0;
        static int closeNumber = 0;
        static bool _bAccpet;

        public static string str;
        /// <summary>
        /// 线程
        /// </summary>
        static Thread pr = new Thread(_printNmea);
        static Thread th = new Thread(download);
        ///定义状态
        public static bool spListstate = true;
        public static bool spOpenstate = true;
        public static bool spClosestate = true;
        public static bool GetRTCMdatastate = true;
        public static bool printNMEAstate = true;
        /// <summary>
        /// 获取可用端口名称
        /// </summary>
        public static string[] spList()
        {

            
            try
            {
                
                string[] _spList = SerialPort.GetPortNames();

                //string[] _spList = new string[2];
                //_spList[0] = "io大苏打似的";
                //_spList[1] = "技术的绝杀";

                //string[] _spList = MulGetHardwareInfo(HardwareEnum.Win32_SerialPort, "Name");


                spListstate = true;
                // return Newtonsoft.Json.JsonConvert.SerializeObject(_spList);
                return _spList;
            }
            catch (Exception)
            {
                spListstate = false;
                return null;
            }
        }
        //public enum HardwareEnum
        //{

        //    Win32_SerialPort, // 串口
        //}
        //public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        //{

        //    List<string> strs = new List<string>();
        //    try
        //    {
        //        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
        //        {
        //            var hardInfos = searcher.Get();
        //            foreach (var hardInfo in hardInfos)
        //            {
        //                if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
        //                {
        //                    strs.Add(hardInfo.Properties[propKey].Value.ToString());
        //                }

        //            }
        //            searcher.Dispose();
        //        }
        //        spListstate = true;
        //        return strs.ToArray();
        //    }
        //    catch
        //    {
        //        spListstate = false;
        //        return null;
        //    }
        //    finally
        //    { strs = null; }
        //}
        /// <summary>
        /// 开关端口
        /// </summary>
        public static string spOpen(string spname)
        {


            if (_sp.IsOpen)
            {
                return "ok";
            }
            else
            {
                try
                {
                    //端口打开无法设置bug
                    _sp.PortName = spname;  // 端口名 
                    _sp.BaudRate = 115200;  // 波特率
                    _sp.DataBits = 8;       // 数据位
                    _sp.StopBits = (StopBits)int.Parse("1"); // 停止位
                    _sp.Open();
                    _bAccpet = true;
                    printdata();
                    return "ok";
                }
                catch (Exception)
                {
                    spOpenstate = false;
                    return "can't find " + spname;
                }
            }


        }
        public static void printdata()
        {
            try
            {
                // 开启接收数据的线程
                if (closeNumber > 0)
                {
                    pr.Resume();

                }
                else
                {
                    pr.Start();
                    pr.IsBackground = true;
                }
            }
            catch (Exception) { }

        }
        public static string spClose(string spname)
        {
            try
            {
                _bAccpet = false;
                pr.Suspend();

                if (closeNumber > 0 && RtcmConnectNumber > 0) { th.Suspend(); }
                _sp.Close();
                closeNumber++;
                spOpenstate = true;
                return "close ok";
            }
            catch (Exception)
            {
                spOpenstate = false;
                return "error";
            }
        }
        /// <summary>
        /// 发送RTCM数据
        /// </summary>
        public static string setAccountAndKey(string account, string key)
        {
            try
            {
                _account = account;
                _key = key;
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(account + ":" + key);
                _basicAccountAndKey = Convert.ToBase64String(byteArray);
                _basicAccountAndKey = "Basic " + _basicAccountAndKey;
                GetRTCMdatastate = true;
                return "Account set ok";
            }
            catch (Exception)
            {
                GetRTCMdatastate = false;
                return "Account set fail";
            }
        }
        public static string GetRTCMdata(string address, string mountPoint)
        {
            try
            {
                string[] AddressMessage = address.Split(':');
                _address = AddressMessage[0];
                if (AddressMessage.Length != 1)
                    port = int.Parse(AddressMessage[1]);
                else
                    port = 80;
                _mountPoint = mountPoint;

                if (closeNumber > 0 && RtcmConnectNumber > 0)
                {
                    th.Resume();

                }
                else
                {
                    th.Start();
                    th.IsBackground = true;
                }
                RtcmConnectNumber++;
                //th.Start();
                //th.IsBackground = true;
                GetRTCMdatastate = true;
                return "RTCM Transport Success";
            }
            catch (Exception)
            {
                GetRTCMdatastate = false;
                return "RTCM Transport fail";
            }

        }
        public static void download()
        {
            try
            {
                string uriaddress = _address;
                IPAddress ipAddress = IPAddress.Parse(uriaddress);
                IPEndPoint iep = new IPEndPoint(ipAddress, port);
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(iep);

                bool connect = true;

                String requestmsg = "GET /" + _mountPoint
                                        + " HTTP/1.0\r\n";
                requestmsg += "User-Agent: NTRIP GNSSInternetRadio/1.2.0\r\n";

                requestmsg += "Authorization: " + _basicAccountAndKey;
                requestmsg += "\r\n";
                requestmsg += "Accept: */*\r\n";
                requestmsg += "Connection: close\r\n";
                requestmsg += "\r\n";
                string message = requestmsg;
                byte[] sendbytes = System.Text.Encoding.UTF8.GetBytes(message);
                int successSendBtyes = clientSocket.Send(sendbytes, sendbytes.Length, SocketFlags.None);

                byte[] bArr = new byte[1024];
                int size = clientSocket.Receive(bArr);

                while (size > 0 && connect == true)
                {
                    size = clientSocket.Receive(bArr);
                    if (_sp.IsOpen)
                    {
                        _sp.Write(bArr, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 实时收取NEMA数据的线程
        /// </summary>
        public static void _printNmea()
        {
            try
            {
                while (_bAccpet)
                {
                    str = _sp.ReadExisting();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 获取经纬度JSON数据
        /// </summary>
        /// <returns></returns>
        public static List<string> PrintNmeaData()
        {
            //11.16修改
            //int indexR = str.LastIndexOf("$GPRMC");
            //int indexM = str.LastIndexOf("*");
            int indexR = str.LastIndexOf("$GPGGA");
            int indexM = str.IndexOf("*");
            //11.16修改结束
            int indexN = str.Length;
            
            if (indexN > (indexR + 50))
            {
                //11.16修改
                //string model = str.Substring((indexM - 1), 1);
                string model = str.Substring((indexR + 50), 1);
                //11.16修改结束
                string gnrmc = str.Substring((indexR + 17), 32);
                //return gnrmc;//    gnrmc=3031.69748195,N,11421.38629542,E
                string lat = gnrmc.Substring(0, 13);
                string lon = gnrmc.Substring(16, 13);
                if (IsCoordinate(lat) && IsCoordinate(lon) && model == "2")
                {
                    lat = DF2D(lat);
                    lon = DF2D(lon);
                    //string[] od = { lat, lon ,model};
                    string[] od = { lat, lon };
                    List<string> coordinate = new List<string>(od);
                    printNMEAstate = true;
                    return coordinate;
                }
                else
                {
                    lat = "00.000";
                    lon = "00.000";
                    string[] od = { lat, lon };
                    List<string> coordinate = new List<string>(od);
                    printNMEAstate = false;
                    return coordinate;

                }
            }
            else
            {
                string lat = "00.000";
                string lon = "00.000";
                string[] od = { lat, lon };
                List<string> coordinate = new List<string>(od);
                printNMEAstate = false;
                return coordinate;

            }
            bool IsCoordinate(string str)
            {
                try
                {
                    double i = Convert.ToDouble(str);
                    return true;
                }
                catch
                {
                    printNMEAstate = false;
                    return false;
                }
            }
            string DF2D(string s)
            {
                try
                {
                    int i = s.IndexOf(".");
                    string du = s.Substring(0, i - 2);
                    string fen = s.Substring(i - 2, s.Length - i);
                    double fen2du = Convert.ToDouble(fen) / 60;
                    double degree = Convert.ToDouble(du) + fen2du;
                    string Degree = Convert.ToString(degree);
                    return Degree;
                }
                catch (Exception)
                {
                    printNMEAstate = false; return "00.000";
                }
            }
        }
    }

}


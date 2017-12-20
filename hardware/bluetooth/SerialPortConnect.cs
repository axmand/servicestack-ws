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
        static int indexR;
        static int indexN;
        public static string _str;

        public static string _coordinate;
        /// <summary>
        /// 线程
        /// </summary>
        static Thread pr = new Thread(_printNmea);
        static Thread th = new Thread(download);
        ///定义状态
        public static bool spListstate = true;
        public static bool spOpenstate = false;
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
        #region 获取串口名字 效率太慢
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
        #endregion
        /// <summary>
        /// 开关端口
        /// </summary>
        public static string spOpen(string spname)
        {


            if (_sp.IsOpen)
            {
                spOpenstate = true;
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
                    spOpenstate = true;
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
        public static string spClose(string spname)
        {
            try
            {
                _bAccpet = false;
                closeNumber++;
                ////////1212 删除suspend 和resume

                //if (pr.ThreadState == ThreadState.Stopped) { }
                //else { pr.Suspend(); }


                //if (closeNumber > 0 && RtcmConnectNumber > 0)
                //{
                //   
                //    th.Suspend();
                //}
                // 调试用 var s = th.ThreadState;
                _sp.Close();

                spClosestate = true;
                return "close ok";
            }
            catch (Exception e)
            {
                var s = e.ToString();
                spClosestate = false;
                return "close fail";
            }
        }

        // 控制线程pr的开启和挂起
        public static void printdata()
        {
            try
            {
                // 开启接收数据的线程
                //if (closeNumber > 0)
                if (pr.ThreadState == ThreadState.Stopped)
                {
                    pr = new Thread(_printNmea);
                    pr.Start();
                }
                else if (pr.ThreadState == ThreadState.Unstarted)
                {

                    pr.Start();
                    pr.IsBackground = true;

                }
                //else // 不会走到这步
                //{
                //    pr.Resume();
                //}
            }
            catch (Exception) { }

        }
        /// 线程pr 连接蓝牙后接收坐标
        public static void _printNmea()
        {
            try
            {
                while (_bAccpet)
                {
                    _str = _sp.ReadExisting();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 控制发送RTCM数据
        /// </summary>

        // 设置密码
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

        //连接节点 涉及th线程的开启 执行连接接口后的返回值
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

                //if (closeNumber > 0 && RtcmConnectNumber > 0)
                //if(th.ThreadState==ThreadState.Suspended)//||th.ThreadState="ThreadState.Background|Suspended")
                if (th.ThreadState == ThreadState.Unstarted)
                {


                    th.Start();
                    th.IsBackground = true;

                }
                else if (th.ThreadState == ThreadState.Stopped)
                {
                    th = new Thread(download);
                    th.Start();
                }
                //else
                //{
                //    th.Resume();
                //}
                RtcmConnectNumber++;
                //th.Start();
                //th.IsBackground = true;
                GetRTCMdatastate = true;
                return "RTCM Transport Success";
            }
            catch (Exception e)
            {
                GetRTCMdatastate = false;
                return "RTCM Transport fail";
            }

        }
        //线程th的程序
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

                byte[] sendbytes = System.Text.Encoding.UTF8.GetBytes(requestmsg);
                int successSendBtyes = clientSocket.Send(sendbytes, sendbytes.Length, SocketFlags.None);

                byte[] bArr = new byte[1024];



                clientSocket.Receive(bArr);
                //string str = System.Text.Encoding.Default.GetString(bArr);

                //11.22修改
                // 中海达RTK与苍穹返回值不同
                //中海达：
                //indexR = str.LastIndexOf("$GPGGA");
                //
                //苍穹
                //1218 放前面 直到接到坐标后进行下一步
                //while (str.IndexOf("GGA") >= 0)
                //{
                //    indexR = str.IndexOf("GGA");
                //    break;
                //}
                //indexN = str.Length;
                //1218
                //string gnrmc = null;
                //1218
                //while (indexN > (indexR + 50))
                //{
                //    //string model = str.Substring((indexR + 50), 1);
                //    gnrmc = str.Substring((indexR + 14), 32);
                //    break;
                //}
                //1218
                //11.22修改结束
                string time = string.Format("{0:D2}{1:D2}{2:00.00}", DateTime.UtcNow.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                //string GGA_Message = "$GPGGA," + time + ","+ gnrmc + ",1,05,1.0,0.0,M,,M,,*"; //获取当地坐标：
                // "$GPGGA,094233.0000,2300.0000,N,10830.5084,E,1,06,1.2,44.6,M,-5.7,M,,0000*\r\n";
                string GGA_Message = "$GPGGA," + time + ",2249.12951221,N,10822.00052222,E,1,05,1.0,0.0,M,,M,,*"; //南宁八位小数 好用 
                //string GGA_Message = "$GPGGA," + time + ",3031.6979,N,11421.3852,E,1,05,1.0,0.0,M,,M,,*";//武汉坐标 不好用
                //string GGA_Message = "$GPGGA," + time + ",2249.1295,N,10822.0005,E,1,05,1.0,0.0,M,,M,,*"; //南宁坐标好用
                //总结： 南宁cors在武汉用不了。。

                //计算检校和
                char[] CharMsg = GGA_Message.ToCharArray();
                int result, index;
                for (result = CharMsg[1], index = 2; CharMsg[index] != '*'; index++)
                {
                    result ^= CharMsg[index];
                }
                GGA_Message = GGA_Message + result.ToString("X") + "\r\n" + "\r\n";


                byte[] GGA_Bytes = System.Text.Encoding.UTF8.GetBytes(GGA_Message);
                clientSocket.Send(GGA_Bytes, GGA_Bytes.Length, SocketFlags.None);



                int size = clientSocket.Receive(bArr);



                while (size > 0 && connect == true)
                {
                    size = clientSocket.Receive(bArr);

                    //Console.WriteLine(size);
                    // 1218
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
            catch (Exception e)
            {
                var s = e.ToString();
            }
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

            //中海达：
            //indexR = str.LastIndexOf("$GPGGA");
            //
            //苍穹
            //indexR = str.LastIndexOf("$GNGGA");
            //统一
            while (_str.IndexOf("GGA") >= 0)
            {
                indexR = _str.IndexOf("GGA");
                break;
            }
            //11.16修改结束
            indexN = _str.Length;

            if (indexN > (indexR + 50))
            {
                //11.16修改
                //string model = str.Substring((indexM - 1), 1);
                string model = _str.Substring((indexR + 47), 1);
                //11.16修改结束
                string gnrmc = _str.Substring((indexR + 14), 32);
                //return gnrmc;//    gnrmc=3031.69748195,N,11421.38629542,E
                string lat = gnrmc.Substring(0, 13);
                string lon = gnrmc.Substring(16, 13);
                //if (IsCoordinate(lat) && IsCoordinate(lon) && model == "2")
                if (IsCoordinate(lat) && IsCoordinate(lon))
                {
                    lat = DF2D(lat);
                    lon = DF2D(lon);
                    string[] od = { lat, lon ,model};
                    //string[] od = { lat, lon };
                    List<string> coordinate = new List<string>(od);
                    printNMEAstate = true;
                    return coordinate;
                }
                else
                {
                    lat = "00.000";
                    lon = "00.000";
                    string[] od = { lat,lon,model };
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


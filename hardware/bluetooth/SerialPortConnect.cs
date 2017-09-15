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
        /// </summary>
        static string _address;
        static string _mountPoint;
        static string _account;
        static string _key;
        static string _basicAccountAndKey = "";

        static int closeNumber = 0;
        static bool _bAccpet;
        public static string str;
        /// <summary>
        /// 线程
        /// </summary>
        static Thread pr = new Thread(_printNmea);
        static Thread th = new Thread(download);
        /// <summary>
        /// 获取可用端口名称
        /// </summary>
        public static string spList()
        {
            //string[] _spList = SerialPort.GetPortNames();
            string[] _spList = MulGetHardwareInfo(HardwareEnum.Win32_SerialPort, "Name");
            return Newtonsoft.Json.JsonConvert.SerializeObject(_spList);
        }
        public enum HardwareEnum
        {

            Win32_SerialPort, // 串口
        }
        public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                        {
                            strs.Add(hardInfo.Properties[propKey].Value.ToString());
                        }

                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { strs = null; }
        }
        /// <summary>
        /// 开关端口
        /// </summary>
        public static string spOpen(string spname)
        {
            _sp.PortName = spname;  // 端口名 
            _sp.BaudRate = 115200;  // 波特率
            _sp.DataBits = 8;       // 数据位
            _sp.StopBits = (StopBits)int.Parse("1"); // 停止位
            
            if (_sp.IsOpen)
            {
                return "ok";
            }
            else
            {
                try
                {
                    _sp.Open();
                    _bAccpet = true;
                    printdata();
                    return "ok";
                }
                catch (Exception)
                {
                    return "can't find " + spname;
                }
            }
        }
        public static void printdata()
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
        public static string spClose(string spname)
        {
            _bAccpet = false;
            pr.Suspend();
            if (closeNumber > 0) { th.Suspend(); }
            _sp.Close();
            closeNumber++;
            return "close ok";
        }
        /// <summary>
        /// 发送RTCM数据
        /// </summary>
        public static void setAccountAndKey(string account, string key)
        {
            _account = account;
            _key = key;
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(account + ":" + key);
            _basicAccountAndKey = Convert.ToBase64String(byteArray);
            _basicAccountAndKey = "Basic " + _basicAccountAndKey;
        }
        public static string GetRTCMdata(string address, string mountPoint)
        {
            _address = "http://" + address + "/";
            _mountPoint = mountPoint;
            int i = 0;
            if (closeNumber > 0 && i > 0)
            {
                th.Resume();
            }
            else
            {
                th.Start();
                th.IsBackground = true;
            }
            i++;
            //th.Start();
            //th.IsBackground = true;
            return "RTCM Transport Success";


        }
        public static void download()
        {
            string uriaddress = _address + _mountPoint;
            Uri siteUri = new Uri(uriaddress);
            HttpWebRequest httpWebRequestTest = (HttpWebRequest)WebRequest.Create(siteUri);
            //设置请求方法
            httpWebRequestTest.Method = "GET";
            //设置userAgent
            httpWebRequestTest.UserAgent = "NTRIP NtripClientPOSIX/1.50";
            //设置版本
            //httpWebRequestTest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequestTest.Headers.Add("Ntrip-Version", "Ntrip/2.0");
            httpWebRequestTest.Headers.Add("Authorization", _basicAccountAndKey);
            //建立响应对象 用来接收http请求的反映
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequestTest.GetResponse();
            bool connect = true;
            Stream responseStream = myHttpWebResponse.GetResponseStream();
            //创建本地文件写入流
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);

            while (size > 0 && connect == true)
            {
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
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

        /// <summary>
        /// 实时收取NEMA数据的线程
        /// </summary>
        public static void _printNmea()
        {
            while (_bAccpet)
            {
                str = _sp.ReadExisting();
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 获取经纬度JSON数据
        /// </summary>
        /// <returns></returns>
        public static List<string> PrintNmeaData()
        {

            int indexR = str.LastIndexOf("$GPRMC");
            int indexN = str.Length;
            if (indexN > (indexR + 50))
            {
                string gnrmc = str.Substring((indexR + 18), 32);
                //return gnrmc;//    gnrmc=3031.69748195,N,11421.38629542,E
                string lat = gnrmc.Substring(0, 13);
                string lon = gnrmc.Substring(16, 13);
                if (IsCoordinate(lat) && IsCoordinate(lon))
                {
                    lat = DF2D(lat);
                    lon = DF2D(lon);
                    string[] od = { lat, lon };
                    List<string> coordinate = new List<string>(od);

                    return coordinate;
                }
                else
                {
                    lat = "00.000";
                    lon = "00.000";
                    string[] od = { lat, lon };
                    List<string> coordinate = new List<string>(od);

                    return coordinate;

                }
            }
            else
            {
                string lat = "00.000";
                string lon = "00.000";
                string[] od = { lat, lon };
                List<string> coordinate = new List<string>(od);

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
                    return false;
                }
            }
            string DF2D(string s)
            {
                int i = s.IndexOf(".");
                string du = s.Substring(0, i - 2);
                string fen = s.Substring(i - 2, s.Length - i);
                double fen2du = Convert.ToDouble(fen) / 60;
                double degree = Convert.ToDouble(du) + fen2du;
                string Degree = Convert.ToString(degree);
                return Degree;
            }
        }
    }

}


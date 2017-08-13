using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Net;

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
        /// <summary>
        /// 获取可用端口名称
        /// </summary>
        public static string spList()
        {
            string[] _spList = SerialPort.GetPortNames();
            return Newtonsoft.Json.JsonConvert.SerializeObject(_spList);
        }
        /// <summary>
        /// 开关端口
        /// </summary>
        public static void spOpen(string spname)
        {
            _sp.PortName = spname;  // 端口名 
            _sp.BaudRate = 115200;  // 波特率
            _sp.DataBits = 8;       // 数据位
            _sp.StopBits = (StopBits)int.Parse("1"); // 停止位

            if (_sp.IsOpen)
            {
                _sp.Close();
                _sp.Open();
            }
            else
            {
                _sp.Open();
            }
        }
        public static void spClose(string spname)
        {
            _sp.Close();
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
        public static void GetRTCMdata(string address, string mountPoint)
        {
            _address = "http://" + address + "/";
            _mountPoint = mountPoint;
            Thread th = new Thread(download);
            th.IsBackground = true;
            th.Start();
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
                if (_sp.IsOpen)
                {
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                    _sp.Write(bArr, 0, size);
                }
                else
                {
                    break;
                }
            }

        }
        /// <summary>
        /// 打印NMEA格式数据
        /// </summary>
        /// <returns></returns>
        public static string PrintNmeaData()
        {
            string str = _sp.ReadExisting();
            string str2=str.Replace("\r\n", "\n");
            return str2;
        }

    }
}

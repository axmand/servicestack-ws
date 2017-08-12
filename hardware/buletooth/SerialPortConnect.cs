using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Net;

namespace hardware.buletooth
{
    public class SerialPortConnect
    {
        //   /58.49.58.149/URUMO/fmy/fmy123
        static SerialPort _sp = new SerialPort();
        /// <summary>
        /// 数据获取ip地址
        /// </summary>
        static string _address;
        /// <summary>
        /// 用户名
        /// </summary>
        static string _account;
        /// <summary>
        /// 密码
        /// </summary>
        static string _key;
        /// <summary>
        /// store the result of account/key which converts to base64
        /// </summary>
        static string _basicAccountAndKey = "";
        /// <summary>
        /// 
        /// </summary>
        static string _mountPoint;


        public static string spList()
        {
            string[] _spList = SerialPort.GetPortNames();

            return Newtonsoft.Json.JsonConvert.SerializeObject(_spList);

        }

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
    }
}

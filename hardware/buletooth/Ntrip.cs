using System;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Threading;

namespace hardware.buletooth
{

    /// <summary>
    /// RTC数据获取
    /// @example 
    /// Ntrip ntrip = new Ntrip("http://58.49.58.149/","fmy","fmy123");
    /// ntrip.GetRTCMdata("URUM0", "ZIM37.RTCM");
    /// </summary>
    public class Ntrip
    {
        /// <summary>
        /// 数据获取ip地址
        /// </summary>
        string _address;
        /// <summary>
        /// 用户名
        /// </summary>
        string _account;
        /// <summary>
        /// 密码
        /// </summary>
        string _key;
        /// <summary>
        /// store the result of account/key which converts to base64
        /// </summary>
        string _basicAccountAndKey = "";
        /// <summary>
        /// 
        /// </summary>
        string _mountPoint;
        /// <summary>
        /// 
        /// </summary>
        string   _fileNameWithPath;
        /// <summary>
        /// 
        /// </summary>
   

        SerialPort sp = new SerialPort();//实例化串口通讯类

        public Ntrip(string address,string account, string key)
        {
            this._address = address;
            this._account = account;
            this._key = key;
        }

        private void download()
        {
            try
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
                Stream receiveStream = myHttpWebResponse.GetResponseStream();//响应内容
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                //StreamReader readStream = new StreamReader(receiveStream, System.Text.ASCIIEncoding.ASCII);
                Stream responseStream = myHttpWebResponse.GetResponseStream();
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
               
                while (size > 0 && connect == true)
                {
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                    sp.Write(bArr, 0, size);
                }
                receiveStream.Close();
                responseStream.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void setAccountAndKey(string account, string key)
        {
            this._account = account;
            this._key = key;
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(account + ":" + key);
            //先转成byte[];
            _basicAccountAndKey = Convert.ToBase64String(byteArray);
            _basicAccountAndKey = "Basic " + BasicAccountAndKey;
        }

        public string BasicAccountAndKey
        {
            get {
                if (_basicAccountAndKey != "")
                    return _basicAccountAndKey;
                _basicAccountAndKey = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(this._account + ":" + this._key));
                return _basicAccountAndKey;
            }
        }

        public void GetRTCMdata(string mountPoint, string fileNameWithPath)
        {
            this._mountPoint = mountPoint;
            this._fileNameWithPath = fileNameWithPath;
            Thread th = new Thread(download);
            th.IsBackground = true;
            th.Start();
        }

    }
}

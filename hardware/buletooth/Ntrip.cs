using System;
using System.IO;
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
        string _sourceTable="";

        public Ntrip(string address,string account, string key)
        {
            this._address = address;
            this._account = account;
            this._key = key;
        }

        string GetSourceTable()
        {
            try
            {
                if (_sourceTable != "")
                    return _sourceTable;
                //创建一个http请求
                //address = "http://" + address + "/";
                Uri siteUri = new Uri(_address);
                HttpWebRequest httpWebRequestTest = (HttpWebRequest)WebRequest.Create(siteUri);
                //设置请求方法
                httpWebRequestTest.Method = "GET";
                //设置userAgent
                httpWebRequestTest.UserAgent = ".NET Framework Test Client";
                //httpWebRequestTest.UserAgent = @" Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; MyIE2; Alexa Toolbar; mxie; .NET CLR 1.1.4322)";
                //设置版本
                httpWebRequestTest.ProtocolVersion = HttpVersion.Version10;
                httpWebRequestTest.Referer = "";
                //建立响应对象 用来接收http请求的反映
                using(HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequestTest.GetResponse())
                {
                    Encoding encode = System.Text.Encoding.GetEncoding(myHttpWebResponse.CharacterSet);
                    using (Stream receiveStream = myHttpWebResponse.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, encode))
                        {
                            // Pipes the stream to a higher level stream reader with the required encoding format. 
                            char[] read = new char[1000];
                            int count = readStream.Read(read, 0, 1000);
                            while (count > 0)
                            {
                                // Dumps the 256 characters on a string and displays the string to the console.
                                _sourceTable += new String(read, 0, count);
                                count = readStream.Read(read, 0, 1000);
                            }
                        }
                    }
                }
                return _sourceTable;
            }
            catch (WebException ex)
            {
                return "请求失败:" + ex.Message;
            }
        }

        private void download()
        {
            try
            {
                Console.WriteLine("正在接收数据:" + _mountPoint + " 地址：" + _fileNameWithPath);
                string uriaddress = _address + _mountPoint;
                Uri siteUri = new Uri(uriaddress);
                Console.WriteLine(uriaddress);
                HttpWebRequest httpWebRequestTest = (HttpWebRequest)WebRequest.Create(siteUri);
                // httpWebRequestTest.Referer =""+ textBox1.Text;
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
                //创建本地文件写入流
                Stream stream = new FileStream(_fileNameWithPath, FileMode.OpenOrCreate);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                // this.Text = "正在接收数据...";
                while (size > 0 && connect == true)
                {
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                    //stream.Write(bArr, 0, size);
                    Console.WriteLine("接收：" + size + "字节");
                    stream.Flush();
                }
                stream.Close();
                responseStream.Close();
                //this.Text = "接收停止";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //textBox2.AppendText(ex.Message);
                //textBox2.Focus();
                //  goto here;
                Console.Write(ex.Message);
            }
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

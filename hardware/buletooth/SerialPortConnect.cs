using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace hardware.buletooth
{
    public class SerialPortConnect
    {

        static SerialPort _sp = new SerialPort();

        public static string spList()
        {
            string[] _spList = SerialPort.GetPortNames();

            return Newtonsoft.Json.JsonConvert.SerializeObject(_spList);

        }
        
        public static void spOpen(string spname)
        {
            
            //public static string strBaudRate = "";
            //public static string strDataBites = "";
            //public static string strStopBits = "";
            _sp.PortName = spname;  // 端口名 
            _sp.BaudRate = 115200;  // 波特率
            _sp.DataBits = 8;       // 数据位
            _sp.StopBits = (StopBits)int.Parse("1"); // 停止位
           
            if (_sp.IsOpen)
            {
                _sp.Close();
            }
            else
            {
                _sp.Open();
            }
        }
        public static void spClose(string spname)
        {

   
            if (_sp.IsOpen)
            {
                _sp.Close();
            }
            else
            {
                _sp.Close();
            }
        }

    }
}

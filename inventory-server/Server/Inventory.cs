using inventory_server.Route;
using ServiceStack.ServiceInterface;
using hardware.bluetooth;
using hardware.projectmanager;
using System.Collections.Generic;

namespace inventory_server.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryServer : Service
    {
        /// <summary>
        /// Project
        /// </summary>

        public string Get(ProjectRemoveGet request)
        {
            var s = new
            {
                aaa = request.id
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(s);
        }
        public string Get(ProjecListGet request)
        {
            //List<string> list = new List<string>()
            //{
            //    "项目1", "项目2" ,"项目3"
            //};
            //return new OkResponse(list).ToString();
            return new OkResponse(ImportProject.ShowProj()).ToString();            
        }
        public string Get(ProjectCreate request)
        {
            return new OkResponse(ImportProject.CreateProj(request.name)).ToString();
        }

        public string Get(ProjectOpen request)
        {
            return ImportProject.SendProjData(request.name);
        }

        /// <summary>
        /// 蓝牙
        /// </summary>
        public string Get(GetBlueToothList request)
        {
            return BlueToothList.getlist();
            
        }
        public void Get(ConnectBlueTooth request)
        {
            BlueToothList.connect(request.devicename);
        }
        public string Get(GetSpList request)
        {
            return SerialPortConnect.spList();
        }
        public string  Get(ConnectSp request)
        {
            //SerialPortConnect.spOpen(request.spname);
            return new OkResponse(SerialPortConnect.spOpen(request.spname)).ToString();
        }
        public void Get(CloseSp request)
        {
            SerialPortConnect.spClose(request.spname);
        }
        public string Get(ConnectStation request)
        {
            SerialPortConnect.setAccountAndKey(request.account,request.key);
            //SerialPortConnect.GetRTCMdata(request.address,request.mountpoint);
            return new OkResponse(SerialPortConnect.GetRTCMdata(request.address, request.mountpoint)).ToString();
        }
        public string Get(PrintNmea request)
        {
           // return SerialPortConnect.PrintNmeaData();
            var coordinate = new
            {
                lat = SerialPortConnect.PrintNmeaData()[0],
                lon = SerialPortConnect.PrintNmeaData()[1]
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(coordinate);
        }
    }
}

using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using inventory_server.Route;
using ServiceStack;
using System.Collections.Generic;
using System.Text;
using System;
using hardware.buletooth;

namespace inventory_server.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryServer : Service
    {
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
            return null;
        }
        /// <summary>
        /// 0809
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
        public void Get(ConnectSp request)
        {
            SerialPortConnect.spOpen(request.spname);
        }
    }
}

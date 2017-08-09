using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using inventory_server.Route;
using ServiceStack;
using System.Collections.Generic;
using System.Text;
using System;

namespace inventory_server.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryServer: Service
    {
        public string Get(ProjectRemoveGet request)
        {
            var s =new {
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
            BluetoothClient _blueToothClient = new BluetoothClient();
            BluetoothRadio _blueRadio = BluetoothRadio.PrimaryRadio;
            Dictionary<String, String> _deviceAddresses = new Dictionary<String, String>();
            //Dictionary<String, BluetoothAddress> _deviceAddresses = new Dictionary<String, BluetoothAddress>();
            _blueRadio.Mode = RadioMode.Connectable;
            //_blueRadio.Mode = RadioMode.Discoverable;// 和另一种不知道选哪个
            BluetoothDeviceInfo[] _devices = _blueToothClient.DiscoverDevices();
            _deviceAddresses.Clear();

            string returnMessage="";
            foreach (BluetoothDeviceInfo device in _devices)
            {
                
            
                string msg = Encoding.GetEncoding("gb2312").GetString(Encoding.Default.GetBytes(device.DeviceName));

                // returnMessage += msg+"\n";

                _deviceAddresses[device.DeviceAddress.ToString()] = msg;// device.DeviceAddress;
            }
            // return Newtonsoft.Json.JsonConvert.SerializeObject(_deviceAddresses);
            return Newtonsoft.Json.JsonConvert.SerializeObject(_deviceAddresses);


        }
     

    }
}

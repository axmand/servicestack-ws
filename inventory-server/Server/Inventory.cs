
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using inventory_server.Route;
using ServiceStack;
using System.Collections.Generic;

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
            Dictionary<string, BluetoothAddress> _deviceAddresses = new Dictionary<string, BluetoothAddress>();
            //_blueRadio.Mode = RadioMode.Connectable;
            _blueRadio.Mode = RadioMode.Discoverable;// 和另一种不知道选哪个
            BluetoothDeviceInfo[] _devices = _blueToothClient.DiscoverDevices();
            _deviceAddresses.Clear();
            foreach (BluetoothDeviceInfo device in _devices)
            {
                _deviceAddresses[device.DeviceName] = device.DeviceAddress;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(_deviceAddresses);
        }


    }
}

﻿using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hardware.bluetooth
{
    
    public class BlueToothList
    {
        static BluetoothClient _blueToothClient = new BluetoothClient();
        static Dictionary<String, InTheHand.Net.BluetoothAddress> _deviceAddresses = new Dictionary<String, InTheHand.Net.BluetoothAddress>();
        public static bool getliststate = true;
        public static bool connectstate = true;
        public static string getlist()
        {
            try
            {
                BluetoothRadio _blueRadio = BluetoothRadio.PrimaryRadio;
                string[] _devicename;
                _blueRadio.Mode = RadioMode.Connectable;// 已配对+可连接
                                                        //_blueRadio.Mode = RadioMode.Discoverable;
                BluetoothDeviceInfo[] _devices = _blueToothClient.DiscoverDevices();
                _deviceAddresses.Clear();


                foreach (BluetoothDeviceInfo device in _devices)
                {
                    string aname = Encoding.GetEncoding("gb2312").GetString(Encoding.Default.GetBytes(device.DeviceName));
                    //_deviceAddresses[aname] = device.DeviceAddress.ToString();   // 键是[] 对应 =后面
                    _deviceAddresses[aname] = device.DeviceAddress;
                }

                _devicename = _deviceAddresses.Keys.ToArray();

                return Newtonsoft.Json.JsonConvert.SerializeObject(_devicename);
            }
            catch (Exception)
            {
                getliststate = false;
                return "fail";
            }
            // return Newtonsoft.Json.JsonConvert.SerializeObject(_deviceAddresses);
        }
        public static string connect(string selectdevice, string password)
        {
            try
            {
                _blueToothClient.SetPin(_deviceAddresses[selectdevice], password);//输入密码 "1234"
                                                                                  // _blueToothClient.Connect(_deviceAddresses[selectdevice], BluetoothService.Handsfree);//选择connect RTK名称后  System.Net.Sockets.SocketException:“在其上下文中，该请求的地址无效。” 
                _blueToothClient.Connect(_deviceAddresses[selectdevice], BluetoothService.SerialPort);

                return "connect success";
            }
            catch (Exception) { connectstate = false; return "connect fail"; }
        }
    }
   
}

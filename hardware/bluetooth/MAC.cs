using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace hardware.bluetooth
{
    public  class MAC
    {
        public static string GetActivatedAdaptorMacAddress()
        {
            try
            {
                
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                    {
                        mac = mo["MacAddress"].ToString();
                    }
                }
                return mac;
            }
            catch (Exception e ) { return "0"; };
        }
    }
}

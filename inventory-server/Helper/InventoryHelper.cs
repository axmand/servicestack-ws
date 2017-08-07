using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_server.Helper
{
    /// <summary>
    /// singleton 
    /// </summary>
    public class InventoryHelper
    {
        static AppHost _appHost;

        public static void Start(string url)
        {
            _appHost = new AppHost();
            _appHost.Init().Start(url);
        }
    }
}

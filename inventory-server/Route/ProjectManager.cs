using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_server.Route
{
    [Route("/project/remove/{id}", "GET")]
    public  class ProjectRemoveGet
    {
        public string id { get; set; }
    }
    [Route("/project/list", "GET")]
    public class ProjecListGet
    {

    }

    [Route("/bluetooth/getlist", "GET")]
    public class GetBlueToothList
    {
        public string _list { get; set; }//_list这个是随便写的嘛？ 0809
    }

    [Route("/bluetooth/connect/{devicename}", "GET")]
    public class ConnectBlueTooth
    { 
        public string devicename { get; set; }
    }
    [Route("/bluetooth/connect/splist", "GET")]
    public class GetSpList
    {
        public string[] _splist { get; set; }
    }
    [Route("/bluetooth/connect/sp/{spname}", "GET")]
    public class ConnectSp
    {
        public string spname { get; set; }
    }
    [Route("/bluetooth/connect/closesp/{spname}", "GET")]
    public class CloseSp
    {
        public string spname { get; set; }
    }
}

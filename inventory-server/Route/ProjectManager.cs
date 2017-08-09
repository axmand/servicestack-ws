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
        public string _list { get; set; }
    }

}

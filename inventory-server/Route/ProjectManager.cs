using ServiceStack;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_server.Route
{
    /// <summary>
    /// Project
    /// </summary>
    [Route("/project/remove/{id}", "GET")]
    public class ProjectRemoveGet
    {
        public string id { get; set; }
    }
    [Route("/project/list", "GET")]
    public class ProjecListGet
    {
        public string[] _prolist { get; set; }
    }
    [Route("/project/create/{name}", "GET")]
    public class ProjectCreate
    {
        public string name { get; set; }
    }
    [Route("/project/open/{name}", "GET")]
    public class ProjectOpen
    {
        public string name { get; set; }
    }
    [Route("/project/fill", "GET")]
    public class ProjectFormsFill
    {
        public string response { get; set; }
    }
    [Route("/project/print/{formnumber}", "GET")]
    public class ProjectFormsPrint
    {
        public int formnumber { get; set; }
    }
    [Route("/project/forms/post", "POST")]
    public class ProjectFormsPost : IRequiresRequestStream
    {
        public System.IO.Stream RequestStream{get;set;}
    }
    [Route ("/project/photo","POST")]
    public class ProjectPhoto : IRequiresRequestStream
    {
        public System.IO.Stream RequestStream { get; set; }
    }
    [Route ("/project/photolist","GET")]
    public class ProjectPhotolist
    {
        public string [] _photolist { get; set; }
    }
    /// <summary>
    /// 蓝牙
    /// </summary>
    [Route("/bluetooth/getlist", "GET")]
    public class GetBlueToothList
    {
        public string _list { get; set; }
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
    [Route("/bluetooth/connect/RTK/{address}/{mountpoint}/{account}/{key}", "GET")]
    public class ConnectStation
    {
        public string address { get; set; }
        public string mountpoint { get; set; }
        public string account { get; set; }
        public string key { get; set; }
    }
    [Route("/bluetooth/connect/RTK/printnmea", "GET")]
    public class PrintNmea
    {
        public string nmea { get; set; }
    }
}

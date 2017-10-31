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
    #region 测试
    [Route("/project/remove/{id}", "GET")]
    public class ProjectRemoveGet
    {
        public string id { get; set; }
    }
    #endregion

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
    //[Route("/project/openlayers", "GET")]
    //public class ProjectLayersOpen
    //{
    //    public string name { get; set; }
    //}

    /// <summary>
    /// 这个不用了 10.10
    /// </summary>
    //[Route("/project/fill", "GET")]
    //public class ProjectFormsFill
    //{
    //    public string response { get; set; }
    //}

    [Route("/project/print/{formnumber}", "GET")]
    public class ProjectFormsPrint
    {
        public int formnumber { get; set; }
    }
    [Route("/project/forms/post", "POST")]
    public class ProjectFormsPost : IRequiresRequestStream
    {
        public System.IO.Stream RequestStream { get; set; }
    }
    [Route("/project/photo", "POST")]
    public class ProjectPhoto : IRequiresRequestStream
    {
        public System.IO.Stream RequestStream { get; set; }
    }
    [Route("/project/photolist", "GET")]
    public class ProjectPhotolist
    {
        public string[] _photolist { get; set; }
    }
    [Route("/project/savepicture", "POST")]
    public class ProjectSavePic : IRequiresRequestStream
    {
        public System.IO.Stream RequestStream { get; set; }
    }
    [Route("/project/printpicture","GET")]
    public class ProjectPrintPic
    {
        public string response { get; set; }
    }
    [Route("/project/deletephoto/{photoname}","GET")]
    public class ProjectDeletePhoto
    {
        public string photoname { get; set; }
    }

    /// <summary>
    /// 蓝牙
    /// </summary>
    [Route("/bluetooth/getlist", "GET")]
    public class GetBlueToothList
    {
        public string _list { get; set; }
    }

    [Route("/bluetooth/connect/{devicename}/{key}", "GET")]
    public class ConnectBlueTooth
    {
        public string devicename { get; set; }
        public string key { get; set; }
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

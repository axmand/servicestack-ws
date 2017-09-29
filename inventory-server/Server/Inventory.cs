using inventory_server.Route;
using ServiceStack.ServiceInterface;
using hardware.bluetooth;
using hardware.projectmanager;
using System.Collections.Generic;
using System.IO;
using static hardware.projectmanager.ImportProject;
using static hardware.projectmanager.Photo;

namespace inventory_server.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryServer : Service
    {
        /// <summary>
        /// Project
        /// </summary>

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
            //List<string> list = new List<string>()
            //{
            //    "项目1", "项目2" ,"项目3"
            //};
            //return new OkResponse(list).ToString();
            return new OkResponse(ImportProject.ShowProj()).ToString();
        }
        public string Get(ProjectCreate request)
        {
            return new OkResponse(ImportProject.CreateProj(request.name)).ToString();
        }
        public string Get(ProjectOpen request)
        {
            return new OkResponse(ImportProject.SendProjData(request.name)).ToString();
        }
        //public string Get(ProjectLayersOpen request)
        //{
        //    return new OkResponse(ImportProject.SendLayersData()).ToString();
        //}
        public string Get(ProjectFormsFill request)
        {
            return new OkResponse(FillAndPrintExcel.WriteXls()).ToString();
        }
        public string Get(ProjectFormsPrint request)
        {
            return new OkResponse(FillAndPrintExcel.CreateAndPrintPdf(request.formnumber)).ToString();
        }
        public string Post(ProjectFormsPost request)
        {
            using (StreamReader dat = new StreamReader(request.RequestStream))
            {
                string str = dat.ReadToEnd();
              // return new OkResponse(str).ToString();
             
                return new OkResponse(SaveProject.SavePro(str)).ToString();
            }
        }
        public string Post(ProjectPhoto request)
        {
            using (StreamReader dat = new StreamReader(request.RequestStream))
            {
                string str = dat.ReadToEnd();
                // return new OkResponse(str).ToString();

                return new OkResponse(Photo.Base64ToPng(str)).ToString();
            }
        }
        public string Get(ProjectPhotolist request)
        {
            if (Photo.PngToBase64() == null)
            {
                return new FailResponse("false").ToString();
            }
            else
            {
                return new OkResponse(Photo.PngToBase64()).ToString();
            }
        }
        /// <summary>
        /// 蓝牙
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
        public string  Get(ConnectSp request)
        {
            //SerialPortConnect.spOpen(request.spname);
            return new OkResponse(SerialPortConnect.spOpen(request.spname)).ToString();
        }
        public string Get(CloseSp request)
        {
            return new OkResponse(SerialPortConnect.spClose(request.spname)).ToString();
        }
        public string Get(ConnectStation request)
        {
            SerialPortConnect.setAccountAndKey(request.account,request.key);
            //SerialPortConnect.GetRTCMdata(request.address,request.mountpoint);
            return new OkResponse(SerialPortConnect.GetRTCMdata(request.address, request.mountpoint)).ToString();
        }
        public string Get(PrintNmea request)
        {
            return new OkResponse(SerialPortConnect.PrintNmeaData()).ToString();
        }
    }
}

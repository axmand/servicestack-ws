using inventory_server.Route;
using ServiceStack.ServiceInterface;
using hardware.bluetooth;
using hardware.projectmanager;
using System.Collections.Generic;
using System.IO;
using static hardware.projectmanager.ImportProject;
using static hardware.projectmanager.Photo;
using static hardware.bluetooth.BlueToothList;
using static hardware.bluetooth.SerialPortConnect;
using static hardware.projectmanager.Print;
using System;
using System.Drawing;

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

        #region 测试
        public string Get(ProjectRemoveGet request)
        {
            var s = new
            {
                aaa = request.id
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(s);
        }
        #endregion

        public string Get(GetMac request)
        {
           string result=MAC.GetActivatedAdaptorMacAddress();
            if (result == "0")
            {
                return new FailResponse(result).ToString();
            }
            else
            {
                return new OkResponse(result).ToString();
            }
        }

        public string Get(ProjecListGet request)
        {

            List<string> ans = ImportProject.ShowProj();
            if (ImportProject.showprojstate)
            {
                return new OkResponse(ans).ToString();
            }
            else
            {
                return new FailResponse(ans).ToString();
            }
        }
        public string Get(ProjectCreate request)
        {

            if (ImportProject.CreateProj(request.name) == "OK")
            {
                return new OkResponse("create ok").ToString();
            }
            else
            {
                return new FailResponse("false").ToString();
            }

        }
        public string Get(ProjectOpen request)
        {
            List<Forms> ans = ImportProject.SendProjData(request.name);
            if (ImportProject.projdatastate)
            { return new OkResponse(ans).ToString(); }
            else
            {
                return new FailResponse(ans).ToString();
            }
        }
        //public string Get(ProjectLayersOpen request)
        //{
        //    return new OkResponse(ImportProject.SendLayersData()).ToString();
        //}

        ///这个不用了 10.0
        //public string Get(ProjectFormsFill request)
        //{
        //    return new OkResponse(FillAndPrintExcel.WriteXls()).ToString();
        //}
        public string Get(ProjectFormsPrint request)
        {
            //return new OkResponse(FillAndPrintExcel.CreateAndPrintPdf(request.formnumber)).ToString();
            //if (Print.FillForms() && Print.FillForms2() && Print.FillForms3())
            if (FillForm())
            {


                if (Print.PrintForm())
                {
                    return new OkResponse("success").ToString();
                }
                else
                {
                    return new FailResponse("fail").ToString();
                }

            }
            else
            {
                return new FailResponse("fail").ToString();
            }
        }
        public string Get(ProjectFormsPrint2 request)
        {
            //return new OkResponse(FillAndPrintExcel.CreateAndPrintPdf(request.formnumber)).ToString();
            //if (Print.FillForms() && Print.FillForms2() && Print.FillForms3())
            if (FillForm())
            {


                if (Print.PrintForm2())
                {
                    return new OkResponse("success").ToString();
                }
                else
                {
                    return new FailResponse("fail").ToString();
                }

            }
            else
            {
                return new FailResponse("fail").ToString();
            }
        }
        public string Post(ProjectFormsPost request)
        {
            using (StreamReader dat = new StreamReader(request.RequestStream))
            {
                string str = dat.ReadToEnd();
                // return new OkResponse(str).ToString();
                if (SaveProject.SavePro(str))
                {
                    return new OkResponse("save success").ToString();
                }
                else
                {
                    return new FailResponse("save failed").ToString();
                }

            }
        }
        public string Post(ProjectPhoto request)
        {
            using (StreamReader dat = new StreamReader(request.RequestStream))
            {
                string str = dat.ReadToEnd();
                // return new OkResponse(str).ToString();
                if (Photo.Base64ToPng(str))
                {
                    return new OkResponse("save photo success").ToString();
                }
                else
                {
                    return new FailResponse("save photo failed").ToString();
                }

            }
        }
        public string Post(ProjectSavePic request)
        {
            using (StreamReader dat = new StreamReader(request.RequestStream))
            {
                string str = dat.ReadToEnd();
                if (Pics.SavePic(str))

                { return new OkResponse("save picture success").ToString(); }
                else
                {
                    return new FailResponse("save picture fail").ToString();
                }

            }
        }
        public string Get(ProjectPrintPic request)
        {
            if (Pics.PrintPic()) { return new OkResponse("print success").ToString(); }
            else { return new FailResponse("print fail").ToString(); }

        }
        public string Get(ProjectPhotolist request)
        {
            List<PhotoData> ans = Photo.PngToBase64(request.id);
            if (pngtobase64state)
            {
                return new OkResponse(ans).ToString();
            }
            else
            {
                return new FailResponse(ans).ToString();
            }
        }
        public string Get(ProjectDeletePhoto request)
        {
            if (Photo.DeletePhoto(request.photoname))
            {
                return new OkResponse("Delete Success!").ToString();
            }
            else
            {
                return new FailResponse("Delete Fail!").ToString();
            }
        }
        public string Post(ChangeId request)
        {
            using (StreamReader dat = new StreamReader(request.RequestStream))
            {
                string str = dat.ReadToEnd();
                if (Photo.IdChange(str))

                { return new OkResponse("Id change ok").ToString(); }
                else
                {
                    return new FailResponse("id change fail").ToString();
                }

            }
        }
        /// <summary>
        /// 蓝牙
        /// </summary>
        public string Get(GetBlueToothList request)
        {
            string ans = BlueToothList.getlist();
            if (getliststate)
            { return new OkResponse(ans).ToString(); }
            else
            {
                return new FailResponse(ans).ToString();
            }


        }
        public string Get(ConnectBlueTooth request)
        {
            string ans = BlueToothList.connect(request.devicename, request.key);
            if (connectstate)
            {
                return new OkResponse(ans).ToString();

            }
            else
            {
                return new FailResponse(ans).ToString();
            }

        }
        public string Get(GetSpList request)
        {
            //string ans = SerialPortConnect.spList();
            string[] ans = SerialPortConnect.spList();
            if (spListstate)
            {
                return new OkResponse(ans).ToString();
            }
            else
            {
                return new FailResponse(ans).ToString();
            }

        }
        public string Get(ConnectSp request)
        {

            string ans = SerialPortConnect.spOpen(request.spname);
            if (spOpenstate)
            {
                return new OkResponse(ans).ToString();

            }
            else
            {
                return new FailResponse(ans).ToString();
            }

        }
        public string Get(CloseSp request)
        {

            string ans = SerialPortConnect.spClose(request.spname);
            if (spClosestate)
            {
                return new OkResponse(ans).ToString();

            }
            else
            {
                return new FailResponse(ans).ToString();
            }
        }
        public string Get(ConnectStation request)
        {

            string ansofaccount = SerialPortConnect.setAccountAndKey(request.account, request.key);
            string ansofGetRTCM;
            if (ansofaccount == "Account set ok")
            {
                ansofGetRTCM = SerialPortConnect.GetRTCMdata(request.address, request.mountpoint);
                if (ansofGetRTCM== "RTCM Transport Success")
                {
                    return new OkResponse(ansofaccount).ToString();
                }
                else
                {
                    return new FailResponse(ansofGetRTCM).ToString();
                }
            }
            else
            {
                return new FailResponse(ansofaccount).ToString();
            }
            //SerialPortConnect.GetRTCMdata(request.address,request.mountpoint);
        }
        public string Get(PrintNmea request)
        {
            List<string> ans = SerialPortConnect.PrintNmeaData();
            if (printNMEAstate)
            {
                return new OkResponse(ans).ToString();
            }
            else
            {
                return new FailResponse(ans).ToString();
            }
        }

        #region

        public string Get(BuildVectorTile request)
        {
            try
            {
                if (VectorToTile.IsRunning)
                    return new FailResponse("切图正在进行").ToString();
                else
                    VectorToTile.Build();
                return new OkResponse("已启动切图").ToString();
            }
            catch
            {
                return new FailResponse("切图启动失败").ToString();
            }
        }

        public void Get(PointLayer request)
        {
            string rtPath = AppDomain.CurrentDomain.BaseDirectory + @"Data/Tile/point/" +
              request.z + @"/" + request.x + "_" + request.y + "_" + request.z + ".png";
            try
            {
                Bitmap bmp = Image.FromFile(rtPath) as Bitmap;
                if (bmp != null)
                {
                    Response.ContentType = "image/png";
                    bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    Response.OutputStream.Position = 0;
                    Response.Flush();
                }
            }
            catch
            {

            }
        }

        public void Get(LineLayer request)
        {
            string rtPath = AppDomain.CurrentDomain.BaseDirectory + @"Data/Tile/line/" +
              request.z + @"/" + request.x + "_" + request.y + "_" + request.z + ".png";
            try
            {
                Bitmap bmp = Image.FromFile(rtPath) as Bitmap;
                if (bmp != null)
                {
                    Response.ContentType = "image/png";
                    bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    Response.OutputStream.Position = 0;
                    Response.Flush();
                }
            }
            catch
            {

            }
        }

        public void Get(PolygonLayer request)
        {
            string rtPath = AppDomain.CurrentDomain.BaseDirectory + @"Data/Tile/polygon/" +
           request.z + @"/" + request.x + "_" + request.y + "_" + request.z + ".png";
            try
            {
                Bitmap bmp = Image.FromFile(rtPath) as Bitmap;
                if (bmp != null)
                {
                    Response.ContentType = "image/png";
                    bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    Response.OutputStream.Position = 0;
                    Response.Flush();
                }
            }
            catch
            {

            }
        }

        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static hardware.projectmanager.ImportProject;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace hardware.projectmanager
{
    public class Photo
    {
        public static bool pngtobase64state = true;
        //照照片
        public static bool Base64ToPng(string Base64Data)
        {
            try
            {
                PhotoData Data = JsonConvert.DeserializeObject<PhotoData>(Base64Data);
                byte[] ByteData = Convert.FromBase64String(Data.PhotoString);
                int n = Data.PhotoId.IndexOf("-");
                string photoId = Data.PhotoId.Substring(0, n);
                string PngFileName=path + "\\" + _importProjectName + "\\Photos\\" + photoId;
                if (!Directory.Exists(PngFileName))
                {
                    Directory.CreateDirectory(PngFileName);
                }
                MemoryStream ms = new MemoryStream(ByteData);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(PngFileName + "\\" + Data.PhotoId + ".png", ImageFormat.Png);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //删除照片
        public static bool DeletePhoto(string photoName)
        {
            try
            {
                File.Delete(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\Photos\\" + photoName + ".png");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        //看照片
        public static List<PhotoData> PngToBase64(string photoId)
        {
            try
            { 
                string PngFileName = path + "\\" + _importProjectName + "\\Photos\\"+photoId;
                if (!Directory.Exists(PngFileName))
                {
                    Directory.CreateDirectory(PngFileName);
                }
                

                string[] _photoPathList;
                string[] _photoNameList;
                _photoPathList = Directory.GetFiles(PngFileName);
                _photoNameList = Directory.GetFiles(PngFileName);
                List<PhotoData> AllPhoto = new List<PhotoData>();

                int n = _photoPathList.Length;

                for (int i = 0; i < n; i++)
                {
                    int m = _photoNameList[i].LastIndexOf("\\") + 1;
                    _photoNameList[i] = _photoNameList[i].Substring(m, _photoNameList[i].Length - m);
                }

                for (int i = 0; i < n; i++)
                {
                    Bitmap bmp = new Bitmap(_photoPathList[i]);
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();
                    bmp.Dispose();
                    string base64 = Convert.ToBase64String(arr);
                    PhotoData photo = new PhotoData();
                    photo.PhotoId = _photoNameList[i];
                    photo.PhotoString = base64;
                    AllPhoto.Add(photo);
                }
                pngtobase64state = true;
                return AllPhoto;
            }
            catch (Exception)
            {
                pngtobase64state = false;
                return null;
            }
        }
        //修改照片文件夹ID
        public static bool IdChange(string s)
        {
            try
            {
                List<PhotoIdData> Data = JsonConvert.DeserializeObject<List<PhotoIdData>>(s);

                int n = Data.Count;
                for (int i = 0; i < n; i++)
                {
                    string srcFolderPath = path + "\\" + _importProjectName + "\\Photos\\" + Data[i].BeforeId;
                    string destFolderPath = path + "\\" + _importProjectName + "\\Photos\\" + Data[i].AfterId;

                    if (System.IO.Directory.Exists(srcFolderPath))
                    {
                        System.IO.Directory.Move(srcFolderPath, destFolderPath);
                    }
                    else
                    {
                        continue;
                    }
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
    public class Pics
    {
        public static bool SavePic(string Base64Data)
        {
            try
            {
                string Compass = System.IO.Directory.GetCurrentDirectory()+ "//compass.png";
                string PngFileName = path + "\\" + _importProjectName + "\\Pics\\";
                byte[] ByteData = Convert.FromBase64String(Base64Data);
                MemoryStream ms = new MemoryStream(ByteData);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(PngFileName+"\\unitpics.png", ImageFormat.Png);
                bmp.Dispose();
                ms.Dispose();
                CombinImage(Compass, PngFileName + "\\unitpics.png");
                return true;
            }
            catch (Exception ex)
            {
                var ms = ex;
                return false;
            }
            #region 同时发送好多图 弃用
            //
            //try
            //{
            //    PhotoData Data = JsonConvert.DeserializeObject<PhotoData>(Base64Data);
            //    byte[] ByteData = Convert.FromBase64String(Data.PhotoString);
            //    //int n = Data.PhotoId.IndexOf("-");
            //    //string photoId = Data.PhotoId.Substring(0, n);
            //    string PngFileName = path + "\\" + _importProjectName + "\\Pics";
            //    if (!Directory.Exists(PngFileName))
            //    {
            //        Directory.CreateDirectory(PngFileName);
            //    }
            //    MemoryStream ms = new MemoryStream(ByteData);
            //    Bitmap bmp = new Bitmap(ms);
            //    bmp.Save(PngFileName + "\\" + Data.PhotoId + ".png", ImageFormat.Png);
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
#endregion
        }
        public static void CombinImage(string sourceImg, string destImg)
        {
            try
            {
                string PngFileName = path + "\\" + _importProjectName + "\\Pics\\" ;
                System.Drawing.Image imgBack = System.Drawing.Image.FromFile(sourceImg);     //相框图片   
                System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片  
                                                                                          //从指定的System.Drawing.Image创建新的System.Drawing.Graphics         
                Graphics g = Graphics.FromImage(imgBack);
                g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);  
                                                                                //g.FillRectangle(System.Drawing.Brushes.Black, 16, 16, (int)112 + 2, ((int)73 + 2));//相片四周刷一层黑色边框  
                                                                                //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
                g.DrawImage(img, 410, 85, 40, 36);//(img,起点x，起点y，宽，高)
                GC.Collect();
                //输出文件流
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                imgBack.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(PngFileName+"printpic.png", ImageFormat.Png);
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/png";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                bmp.Dispose();
                ms.Dispose();
                imgBack.Dispose();
            }
            catch (Exception) { };

        }
        public static bool PrintPic()
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                PdfDocument print = new PdfDocument();
                PdfSection section = doc.Sections.Add();
                PdfPageBase page = doc.Pages.Add();

                PdfImage image = PdfImage.FromFile(path + "\\" + _importProjectName + "\\Pics\\printpic.png");
                float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;

                float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;

                float fitRate = Math.Max(widthFitRate, heightFitRate);

                float fitWidth = image.PhysicalDimension.Width / fitRate;

                float fitHeight = image.PhysicalDimension.Height / fitRate;

                page.Canvas.DrawImage(image, 20, 20, fitWidth, fitHeight);
                doc.SaveToFile(path+ "\\" + _importProjectName + "\\Pics\\pic.pdf");
                print.LoadFromFile(path + "\\Project\\" + _importProjectName + "\\Pics\\pic.pdf");
                print.PrintDocument.Print();
                print.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public class PhotoData
    {
        public string PhotoId { get; set; }
        public string PhotoString { get; set; }
    }
    public class PhotoIdData
    {
        public string BeforeId { get; set; }
        public string AfterId { get; set; }
    }
}

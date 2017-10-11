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
        public static bool Base64ToPng(string Base64Data)
        {
            try
            {
                PhotoData Data = JsonConvert.DeserializeObject<PhotoData>(Base64Data);
                byte[] ByteData = Convert.FromBase64String(Data.PhotoString);
                MemoryStream ms = new MemoryStream(ByteData);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\photos\\" + Data.PhotoId + ".png", ImageFormat.Png);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static List<PhotoData> PngToBase64()
        {
            try
            {
                string PngFileName = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\photos";
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
    }
    public class Pics
    {
        public static bool SavePic(string Base64Data)
        {
            try
            {

                byte[] ByteData = Convert.FromBase64String(Base64Data);
                MemoryStream ms = new MemoryStream(ByteData);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\pics\\unitpics.png", ImageFormat.Png);
                bmp.Dispose();
                ms.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var ms = ex;
                return false;
            }

        }
        public static bool PrintPic()
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                PdfDocument print = new PdfDocument();
                PdfSection section = doc.Sections.Add();
                PdfPageBase page = doc.Pages.Add();

                PdfImage image = PdfImage.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\pics\\unitpics.png");
                float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;

                float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;

                float fitRate = Math.Max(widthFitRate, heightFitRate);

                float fitWidth = image.PhysicalDimension.Width / fitRate;

                float fitHeight = image.PhysicalDimension.Height / fitRate;

                page.Canvas.DrawImage(image, 20, 20, fitWidth, fitHeight);
                doc.SaveToFile(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\pics\\pic.pdf");
                print.LoadFromFile(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + _importProjectName + "\\pics\\pic.pdf");
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
}

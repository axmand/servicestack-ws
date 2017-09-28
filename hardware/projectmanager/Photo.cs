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


namespace hardware.projectmanager
{
    public class Photo
    {
        public static bool Base64ToPng(string Base64Data)
        {
            try
            {
                PhotoData Data = JsonConvert.DeserializeObject<PhotoData>(Base64Data);
                byte[] ByteData = Convert.FromBase64String(Data.PhotoString);
                MemoryStream ms = new MemoryStream(ByteData);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + _importProjectName + "\\pics\\" + Data.PhotoId + ".png", ImageFormat.Png);
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
                string PngFileName = System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + _importProjectName + "\\pics";
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
                    photo.PhotoId= _photoNameList[i];
                    photo.PhotoString = base64;
                    AllPhoto.Add(photo);
                }
                return AllPhoto;
            }
            catch (Exception) { return null; }
        }
    }
    public class PhotoData
    {
        public string PhotoId { get; set; }
        public string PhotoString { get; set; }
    }
}

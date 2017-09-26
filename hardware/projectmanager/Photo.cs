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
   public  class Photo
    {
        public static bool Base64ToPng(string Base64Data)
        {
            try
            {
                PhotoData Data = JsonConvert.DeserializeObject<PhotoData>(Base64Data);
                byte[] ByteData = Convert.FromBase64String(Data.PhotoString);
                MemoryStream ms = new MemoryStream(ByteData);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + _importProjectName + "\\pics\\"+Data.PhotoId + ".png", ImageFormat.Png);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    class PhotoData
    {
        public string PhotoId { get; set; }
        public string PhotoString { get; set; }
    }
}

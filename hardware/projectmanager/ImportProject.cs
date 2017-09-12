using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hardware.projectmanager
{
    public class ImportProject
    {

        public static string CreateProj(string projectName)
        {
            string _projPath = @"D:/ProjectTest/" + projectName;// 存成项目名字
            string _formPath = @"D:/ProjectTest/" + projectName + "/Forms";
            string _fingerPath = @"D:/ProjectTest/" + projectName + "/Fingers";
            string _picPath = @"D:/ProjectTest/" + projectName + "/pics";
            if (!Directory.Exists(_projPath))
            {
                Directory.CreateDirectory(_projPath);
                Directory.CreateDirectory(_formPath);
                Directory.CreateDirectory(_fingerPath);
                Directory.CreateDirectory(_picPath);
                return "OK";
            }
            else
            {
                return "false";
            }

        }

        public static string ShowProj()
        {
            string[] _proList;
            _proList = Directory.GetDirectories("D:\\ProjectTest");//项目保存路径
            int n = _proList.Length;
            for (int i = 0; i < n; i++)
            {
                int m = _proList[i].LastIndexOf("\\") + 1;
                _proList[i] = _proList[i].Substring(m, _proList[i].Length - m);

            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(_proList);
        }
    }
}

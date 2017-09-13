using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace hardware.projectmanager
{
    public class ImportProject
    {
        public static string path = System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest";
        public static string CreateProj(string projectName)
        {
            string _projPath = path + "/"+projectName;// 存成项目名字
            string _formPath = path+ "/" + projectName + "\\Forms";
            string _fingerPath = path + "/"+ projectName + "\\Fingers";
            string _picPath = path + "/" + projectName + "\\pics";
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
            
            
            if (File.Exists(path))
            {

            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
            }

            string[] _proList;
            // _proList = Directory.GetDirectories("D:\\ProjectTest");//项目保存路径
            _proList = Directory.GetDirectories(path);
            int n = _proList.Length;
            for (int i = 0; i < n; i++)
            {
                int m = _proList[i].LastIndexOf("\\") + 1;
                _proList[i] = _proList[i].Substring(m, _proList[i].Length - m);

            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(_proList);
        }
        public static string SendProjData(string pro)
        {

            string Data1 = System.IO.File.ReadAllText(path + pro+ "\\Forms\\all.txt"); 
            List<pro> all = JsonConvert.DeserializeObject<List<pro>>(Data1);
            // 取值 ： all[0].f1.PrincipalCertificateType   但是只有一个 能不能就是 all.f1.....而不是list这样all好多页
            return all.ToString();
        }
        public class f1
        {
            public long TableID { get; set; }
            public string ParcelCode { get; set; }
            public string InvestigateOrganization { get; set; }
            public string InvestigateDate { get; set; }// 存时间
            public string OwnPowerSide { get; set; }
            public string UsePowerSide { get; set; }
            public string PowerSideType { get; set; }
            public string PowerSideCertificateType { get; set; }
            public string PowerSideCertificateCode { get; set; }
            public string PowerSideAddress { get; set; }
            public string PowerType { get; set; }
            public string PowerCharacter { get; set; }
            public string LandPowerCertificatePaper { get; set; }
            public string Location { get; set; }
            public string PrincipalName { get; set; }
            public string PrincipalCertificateType { get; set; }
            public string PrincipalCertificateCode { get; set; }
            public string PrincipalCertificateTelephone { get; set; }
            public string ProcuratorName { get; set; }
            public string ProcuratorCertificateType { get; set; }
            public string ProcuratorCertificateCode { get; set; }
            public string ProcuratorCertificateTelephone { get; set; }
            public string PowerSetPattern { get; set; }
            public string NationalEconomyIndustryClassificationCode { get; set; }
            public string PreParcelCode { get; set; }
            public string UnitNumber { get; set; }
            public string MapScale { get; set; }
            public string MapCode { get; set; }
            public string ParcelRangeNorth { get; set; }
            public string ParcelRangeEast { get; set; }
            public string ParcelRangeSouth { get; set; }
            public string ParcelRangeWest { get; set; }
            public string Rank { get; set; }
            public double Price { get; set; }
            public string PermittedUsefor { get; set; }
            public string PermittedTypeCode { get; set; }
            public string PracticalUsefor { get; set; }
            public string PracticalTypeCode { get; set; }
            public double PermittedArea { get; set; }
            public double ParcelArea { get; set; }
            public double BuildLandArea { get; set; }
            public double BuildTotalArea { get; set; }
            public string LandUseStartTime { get; set; }
            public string LandUseEndTime { get; set; }
            public string CommonUse { get; set; }
            public string Explain { get; set; }
        }
        public class f2
        {
            public string[] LandPointCodeList { get; set; }
            public int[] LandPointTypeList { get; set; }
            public double[] LandPointDistance { get; set; }
            public int[] LandBoundaryType { get; set; }
            public int[] LandBoundaryLocation { get; set; }
            public string[] LandBoundaryExplain { get; set; }
        }
        public class pro
        {
            public f1 f1 { get; set; }
            public f2 f2 { get; set; }
        }
    }
}


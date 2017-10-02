using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hardware.projectmanager
{
    /// <summary>
    /// 包含三个部分： 1、创建空项目。2、显示所有项目名称。3、打开项目（发送选中的项目的all.txt信息）
    /// </summary>
    public class ImportProject
    {
        public static string path = System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest";
        public static string _importProjectName;
        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="projectName">输入的项目名字</param>
        /// <returns>“OK”或者“false”</returns>
        public static string CreateProj(string projectName)
        {
            try
            {
                string _projPath = path + "/" + projectName;// 存成项目名字
                string _formPath = path + "/" + projectName + "\\Forms";
                string _fingerPath = path + "/" + projectName + "\\Fingers";
                string _picPath = path + "/" + projectName + "\\pics";
                string _photoPath = path + "/" + projectName + "\\photos";
                if (!Directory.Exists(_projPath))
                {
                    Directory.CreateDirectory(_projPath);
                    Directory.CreateDirectory(_formPath);
                    Directory.CreateDirectory(_fingerPath);
                    Directory.CreateDirectory(_picPath);
                    Directory.CreateDirectory(_photoPath);
                    return "OK";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception) { return "false"; }

        }
        /// <summary>
        /// 显示所有项目名称
        /// </summary>
        /// <returns></returns>
        public static List<string> ShowProj()
        {

            try
            {
                if (File.Exists(path))
                {

                }
                else
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                string[] _proList;
                _proList = Directory.GetDirectories(path);

                int n = _proList.Length;
                for (int i = 0; i < n; i++)
                {
                    int m = _proList[i].LastIndexOf("\\") + 1;
                    _proList[i] = _proList[i].Substring(m, _proList[i].Length - m);
                }
                List<string> list = new List<string>(_proList);

                return list;
            }
            catch (Exception)
            {
                string erro = "Wrong";
                List<string> wrong = JsonConvert.DeserializeObject<List<string>>(erro);
                return wrong;
            }
        }
        /// <summary>
        /// 打开项目（发送选中的项目的all.txt信息）
        /// </summary>
        /// <param name="pro">打开的项目名称</param>
        /// <returns>all.txt项目的内容</returns>
        public static List<Forms> SendProjData(string pro)
        {
            try
            {
                _importProjectName = pro;
                string Data1 = System.IO.File.ReadAllText(path + "\\" + pro + "\\Forms\\all.txt", Encoding.Default);
                List<Forms> all = JsonConvert.DeserializeObject<List<Forms>>(Data1);
                // 取值 ： all[0].f1.PrincipalCertificateType   但是只有一个 能不能就是 all.f1.....而不是list这样all好多页
                return all;
               
            }
            catch (Exception e)
            {

                List<Forms> wrong = null;
                return wrong;// wrong;
            };
        }
        /// <summary>
        /// Forms类型的数据结构
        /// </summary>
        public class Forms
        {
            public F1 F1 { get; set; }
            public F2 F2 { get; set; }
            public F3 F3 { get; set; }
            public F5 F5 { get; set; }
            public F6 F6 { get; set; }
            public F7 F7 { get; set; }
            public Layers L { get; set; }
        }
        public class F1//宗地基本信息表
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
        public class F2//界址标示表
        {
            public string[] LandPointCodeList { get; set; }
            public int[] LandPointTypeList { get; set; }
            public double[] LandPointDistance { get; set; }
            public int[] LandBoundaryType { get; set; }
            public int[] LandBoundaryLocation { get; set; }
            public string[] LandBoundaryExplain { get; set; }
        }
        public class F3//界址签章表
        {
            public string[] StartPointCodeList { get; set; }
            public string[] InnerPointCodeList { get; set; }
            public string[] EndPointCodeList { get; set; }
        }
        public class F5//界址说明表
        {
            public string BoundaryPointExplain { get; set; }
            public string MainBoundaryDirectionExplain { get; set; }
        }
        public class F6//调查审核表
        {
            public string PowerInvestigateRecord { get; set; }
            public string PowerInvestigator { get; set; }
            public string PowerInvestigateDate { get; set; }
            public string SurveyRecord { get; set; }
            public string SurveyRecorder { get; set; }
            public string SurveyRecordDate { get; set; }
            public string AuditOpinion { get; set; }
            public string Auditor { get; set; }
            public string AuditOpinionDate { get; set; }
        }
        public class F7//共用共有宗地面积分摊表
        {
            public string FixedCount { get; set; }
            public string[] FixedCode { get; set; }
            public double[] LandOwnUseArea { get; set; }
            public double[] LandUniqueArea { get; set; }
            public double[] CommonArea { get; set; }
        }
        public class Layers
        {
            public JValue jzdJSONData { get; set; }
            public JValue szJSONData { get; set; }
            public JValue zdJSONData { get; set; }
            public JValue zjJSONData { get; set; }
            
        }//图层
        /// <summary>
        /// 导入layers的数据txt
        /// </summary>
        /// <param name="pro">项目名称</param>
        /// <returns>List of Layers</returns>
        //public static List<Layers> SendLayersData()
        //{
        //    try
        //    {
        //        string Data1 = System.IO.File.ReadAllText(path + "\\" + _importProjectName + "\\layers\\layers.txt", Encoding.Default);
        //        List<Layers> all = JsonConvert.DeserializeObject<List<Layers>>(Data1);
        //        // 取值 ： all[0].f1.PrincipalCertificateType   但是只有一个 能不能就是 all.f1.....而不是list这样all好多页
        //        return all;
        //    }
        //    catch (Exception)
        //    {

        //        List<Layers> wrong = null;
        //        return wrong;
        //    };
        //}


    }
}


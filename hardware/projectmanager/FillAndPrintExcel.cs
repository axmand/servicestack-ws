using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace hardware.projectmanager
{
    public class FillAndPrintExcel
    {
        public static string WriteXls(string projname)
        {
            if (Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname))
            {
                
            }
            else
            {
                return "false";
            }
            string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Form.xlsx";
            //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
            string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\"+projname+"\\Forms\\DataCopy2Print.xlsx";
            bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
            System.IO.File.Copy(sourceFile, destinationFile, isrewrite);
            //启动Excel应用程序
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            //_Workbook book = xls.Workbooks.Add(Missing.Value); //创建一张表，一张表可以包含多个sheet
            //如果表已经存在，可以用下面的命令打开
            _Workbook book = xls.Workbooks.Open(destinationFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            _Worksheet sheet;//定义sheet变量
            xls.Visible = false;//设置Excel后台运行
            xls.DisplayAlerts = false;//设置不显示确认修改提示

            // string json = @"[{'TaskRoleSpaces':'','TaskRoles':'','ProxyUserID':'5d9ad5dc1c5e494db1d1b4d8d79b60a7','UserID':'5d9ad5dc1c5e494db1d1b4d8d79b60a7','UserName':'姓名','UserSystemName':'2234','OperationName':'送合同负责人','OperationValue':'同意','OperationValueText':'','SignDate':'2013-06-19 10:31:26','Comment':'同意','FormDataHashCode':'','SignatureDivID':''},{'TaskRoleSpaces':'','TaskRoles':'','ProxyUserID':'2c96c3943826ea93013826eafe6d0089','UserID':'2c96c3943826ea93013826eafe6d0089','UserName':'姓名2','UserSystemName':'1234','OperationName':'送合同负责人','OperationValue':'同意','OperationValueText':'','SignDate':'2013-06-20 09:37:11','Comment':'同意','FormDataHashCode':'','SignatureDivID':''}]  ";
            string Data1 = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form1.txt");
            string Data2 = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form2.txt");
            string Data3 = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form3.txt");
            string Data5 = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form5.txt");
            string Data6 = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form6.txt");
            string Data7 = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form7.txt");
            List<ZDJBXX> ZdjbxxForm = JsonConvert.DeserializeObject<List<ZDJBXX>>(Data1);
            List<JZBSB> JzbsForm = JsonConvert.DeserializeObject<List<JZBSB>>(Data2);
            List<JZQZ> JzqzForm = JsonConvert.DeserializeObject<List<JZQZ>>(Data3);
            List<JZSM> JzsmForm = JsonConvert.DeserializeObject<List<JZSM>>(Data5);
            List<DCSH> DcshForm = JsonConvert.DeserializeObject<List<DCSH>>(Data6);
            List<GYZD> GyzdForm = JsonConvert.DeserializeObject<List<GYZD>>(Data7);
            for (int i = 1; i < 8; i++)//循环创建并写入数据到sheet
            {
                try
                {
                    sheet = (_Worksheet)book.Worksheets.get_Item(i);//获得第i个sheet，准备写入
                }
                catch (Exception ex)//不存在就增加一个sheet
                {
                    sheet = (_Worksheet)book.Worksheets.Add(Missing.Value, book.Worksheets[book.Sheets.Count], 1, Missing.Value);
                }
                if (i == 1)
                {
                    sheet.Cells[2, 3] = ZdjbxxForm[0].OwnPowerSide;// C2=2,3 D5=5,4 先列后行
                    sheet.Cells[3, 3] = ZdjbxxForm[0].UsePowerSide;
                    sheet.Cells[16, 9] = ZdjbxxForm[0].ParcelCode;
                    sheet.Cells[3, 9] = ZdjbxxForm[0].PowerSideType;
                    sheet.Cells[4, 9] = ZdjbxxForm[0].PowerSideCertificateType;
                    sheet.Cells[5, 9] = ZdjbxxForm[0].PowerSideCertificateCode;
                    sheet.Cells[6, 9] = ZdjbxxForm[0].PowerSideAddress;
                    sheet.Cells[7, 3] = ZdjbxxForm[0].PowerType;
                    sheet.Cells[7, 8] = ZdjbxxForm[0].PowerCharacter;
                    sheet.Cells[7, 10] = ZdjbxxForm[0].LandPowerCertificatePaper;
                    sheet.Cells[8, 3] = ZdjbxxForm[0].Location;
                    sheet.Cells[9, 3] = ZdjbxxForm[0].PrincipalCertificateCode;
                    sheet.Cells[9, 6] = ZdjbxxForm[0].PrincipalCertificateType;
                    sheet.Cells[10, 6] = ZdjbxxForm[0].ProcuratorCertificateCode;
                    sheet.Cells[9, 10] = ZdjbxxForm[0].PrincipalCertificateTelephone;
                    sheet.Cells[11, 3] = ZdjbxxForm[0].ProcuratorName;
                    sheet.Cells[11, 6] = ZdjbxxForm[0].ProcuratorCertificateType;
                    sheet.Cells[12, 6] = ZdjbxxForm[0].ProcuratorCertificateCode;
                    sheet.Cells[11, 10] = ZdjbxxForm[0].ProcuratorCertificateTelephone;
                    sheet.Cells[13, 3] = ZdjbxxForm[0].PowerSetPattern;
                    sheet.Cells[14, 3] = ZdjbxxForm[0].NationalEconomyIndustryClassificationCode;
                    sheet.Cells[16, 3] = ZdjbxxForm[0].PreParcelCode;
                    sheet.Cells[16, 9] = ZdjbxxForm[0].ParcelCode;
                    sheet.Cells[17, 3] = ZdjbxxForm[0].UnitNumber;
                    sheet.Cells[18, 5] = ZdjbxxForm[0].MapScale;
                    sheet.Cells[19, 5] = ZdjbxxForm[0].MapCode;
                    sheet.Cells[20, 3] = ZdjbxxForm[0].ParcelRangeNorth;
                    sheet.Cells[21, 3] = ZdjbxxForm[0].ParcelRangeEast;
                    sheet.Cells[22, 3] = ZdjbxxForm[0].ParcelRangeSouth;
                    sheet.Cells[23, 3] = ZdjbxxForm[0].ParcelRangeWest;
                    sheet.Cells[24, 3] = ZdjbxxForm[0].Rank;
                    sheet.Cells[24, 9] = ZdjbxxForm[0].Price;
                    sheet.Cells[25, 3] = ZdjbxxForm[0].PermittedUsefor;
                    sheet.Cells[26, 5] = ZdjbxxForm[0].PermittedTypeCode;
                    sheet.Cells[25, 8] = ZdjbxxForm[0].PracticalUsefor;
                    sheet.Cells[26, 10] = ZdjbxxForm[0].PracticalTypeCode;
                    sheet.Cells[27, 3] = ZdjbxxForm[0].PermittedArea;
                    sheet.Cells[27, 6] = ZdjbxxForm[0].ParcelArea;
                    sheet.Cells[27, 10] = ZdjbxxForm[0].BuildLandArea;
                    sheet.Cells[29, 10] = ZdjbxxForm[0].BuildTotalArea;
                    string _landUseTime = ZdjbxxForm[0].LandUseStartTime + "--" + ZdjbxxForm[0].LandUseEndTime;
                    sheet.Cells[30, 3] = _landUseTime;//特殊 日期起止
                    sheet.Cells[31, 3] = ZdjbxxForm[0].CommonUse;
                    sheet.Cells[33, 3] = ZdjbxxForm[0].Explain;
                    continue;
                } 
                else if (i == 2)// 表2
                {
                    int l1, l2, l3, l4, l5, l6;
                    l1 = JzbsForm[0].LandPointCodeList.Length;
                    l2 = JzbsForm[0].LandPointTypeList.Length;
                    l3 = JzbsForm[0].LandBoundaryExplain.Length;
                    l4 = JzbsForm[0].LandBoundaryLocation.Length;
                    l5 = JzbsForm[0].LandBoundaryType.Length;
                    l6 = JzbsForm[0].LandPointDistance.Length;

                    if (l1 == l2 && l1 == l3 + 1 && l1 == l4 + 1 && l1 == l5 + 1 && l1 == l6 + 1)
                    {
                        sheet.Cells[4, 1] = JzbsForm[0].LandPointCodeList[0];
                        sheet.Cells[4, (JzbsForm[0].LandPointTypeList[0] + 2)] = "√";

                        for (int n = 0; n < l3; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = JzbsForm[0].LandPointCodeList[n + 1];
                            sheet.Cells[(2 * n + 5), (JzbsForm[0].LandPointTypeList[n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = JzbsForm[0].LandPointDistance[n];
                            sheet.Cells[(2 * n + 4), (JzbsForm[0].LandBoundaryType[n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (JzbsForm[0].LandBoundaryLocation[n] + 16)] = "√";
                            sheet.Cells[(2 * n + 4), 19] = JzbsForm[0].LandBoundaryExplain[0];
                        }
                        continue;
                    }

                    else
                    { break; }

                }
                else if (i == 3)
                {
                    int l1, l2, l3;
                    l1 = JzqzForm[0].StartPointCodeList.Length;
                    l2 = JzqzForm[0].InnerPointCodeList.Length;
                    l3 = JzqzForm[0].EndPointCodeList.Length;
                    if (l1 == l2 && l2 == l3)
                    {
                        for (int n = 0; n < l1; n++)
                        {
                            sheet.Cells[n + 5, 1] = JzqzForm[0].StartPointCodeList[n];
                            sheet.Cells[n + 5, 2] = JzqzForm[0].InnerPointCodeList[n];
                            sheet.Cells[n + 5, 3] = JzqzForm[0].EndPointCodeList[n];
                        }
                        continue;
                    }
                    else
                    { break; }
                }
                else if (i == 4) { continue; }// 图暂时没有
                else if (i == 5)
                {
                    sheet.Cells[2, 2] = JzsmForm[0].BoundaryPointExplain;
                    sheet.Cells[3, 2] = JzsmForm[0].MainBoundaryDirectionExplain;
                }
                else if (i == 6)
                {
                    sheet.Cells[2, 2] = DcshForm[0].PowerInvestigateRecord;
                    sheet.Cells[20, 5] = DcshForm[0].PowerInvestigator;
                    sheet.Cells[20, 8] = DcshForm[0].PowerInvestigateDate;
                    sheet.Cells[21, 2] = DcshForm[0].SurveyRecord;
                    sheet.Cells[22, 5] = DcshForm[0].SurveyRecorder;
                    sheet.Cells[22, 8] = DcshForm[0].SurveyRecordDate;
                    sheet.Cells[23, 2] = DcshForm[0].AuditOpinion;
                    sheet.Cells[24, 5] = DcshForm[0].Auditor;
                    sheet.Cells[24, 8] = DcshForm[0].AuditOpinionDate;
                }
                else if (i == 7)
                {
                    sheet.Cells[4, 4] = GyzdForm[0].FixedCount;
                    for (int n = 0; n < GyzdForm[0].FixedCode.Length; n++)
                    {
                        sheet.Cells[n + 6, 1] = GyzdForm[0].FixedCode[n];
                        sheet.Cells[n + 6, 2] = GyzdForm[0].LandOwnUseArea[n];
                        sheet.Cells[n + 6, 3] = GyzdForm[0].LandUniqueArea[n];
                        sheet.Cells[n + 6, 4] = GyzdForm[0].CommonArea[n];
                    }
                    continue;

                }
                //读入文件  


            }
            //将表另存为
            //book.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //如果表已经存在，直接用下面的命令保存即可
            book.Save();
            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel程序退出
                       //sheet,book,xls设置为null，防止内存泄露
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源
            return "fill form ok";
        }
        public class ZDJBXX
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
        public class JZBSB // Json数组
        {
            public string[] LandPointCodeList { get; set; }
            public int[] LandPointTypeList { get; set; }
            public double[] LandPointDistance { get; set; }
            public int[] LandBoundaryType { get; set; }
            public int[] LandBoundaryLocation { get; set; }
            public string[] LandBoundaryExplain { get; set; }
        }
        public class JZQZ
        {
            public string[] StartPointCodeList { get; set; }
            public string[] InnerPointCodeList { get; set; }
            public string[] EndPointCodeList { get; set; }
        }
        public class JZSM
        {
            public string BoundaryPointExplain { get; set; }
            public string MainBoundaryDirectionExplain { get; set; }
        }
        public class DCSH
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
        public class GYZD
        {
            public string FixedCount { get; set; }
            public string[] FixedCode { get; set; }
            public double[] LandOwnUseArea { get; set; }
            public double[] LandUniqueArea { get; set; }
            public double[] CommonArea { get; set; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using Spire.Pdf;
using hardware.bluetooth;
using static hardware.projectmanager.ImportProject;

namespace hardware.projectmanager
{
#region
    public class FillAndPrintExcel
    {
        ///这个不用了10.10
        
        /// <summary>
        /// 填写表格，将txt数据库中的数据填写到表格中并保存
        /// </summary>
        /// <returns>fill form ok  /  false</returns>
        public static string WriteXls()
        {

            try
            {
                string projname = _importProjectName;
                string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Form.xlsx";   //  此处是默认的表格模板
                                                                                                 //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
                string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\DataCopy2Print.xlsx";
                bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                System.IO.File.Copy(sourceFile, destinationFile, isrewrite);//复制一份模板文档
                                                                            //启动Excel应用程序
                Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
                //_Workbook book = xls.Workbooks.Add(Missing.Value); //创建一张表，一张表可以包含多个sheet
                //如果表已经存在，可以用下面的命令打开
                _Workbook book = xls.Workbooks.Open(destinationFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet sheet;//定义sheet变量
                xls.Visible = false;//设置Excel后台运行
                xls.DisplayAlerts = false;//设置不显示确认修改提示

                string Data = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\all.txt", Encoding.Default);
                List<Forms> _projectData = JsonConvert.DeserializeObject<List<Forms>>(Data);
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
                        sheet.Cells[2, 3] = _projectData[0].F1.OwnPowerSide;// C2=2,3 D5=5,4 先列后行
                        sheet.Cells[3, 3] = _projectData[0].F1.UsePowerSide;
                        sheet.Cells[16, 9] = _projectData[0].F1.ParcelCode;
                        sheet.Cells[3, 9] = _projectData[0].F1.PowerSideType;
                        sheet.Cells[4, 9] = _projectData[0].F1.PowerSideCertificateType;
                        sheet.Cells[5, 9] = _projectData[0].F1.PowerSideCertificateCode;
                        sheet.Cells[6, 9] = _projectData[0].F1.PowerSideAddress;
                        sheet.Cells[7, 3] = _projectData[0].F1.PowerType;
                        sheet.Cells[7, 8] = _projectData[0].F1.PowerCharacter;
                        sheet.Cells[7, 10] = _projectData[0].F1.LandPowerCertificatePaper;
                        sheet.Cells[8, 3] = _projectData[0].F1.Location;
                        sheet.Cells[9, 3] = _projectData[0].F1.PrincipalCertificateCode;
                        sheet.Cells[9, 6] = _projectData[0].F1.PrincipalCertificateType;
                        sheet.Cells[10, 6] = _projectData[0].F1.ProcuratorCertificateCode;
                        sheet.Cells[9, 10] = _projectData[0].F1.PrincipalCertificateTelephone;
                        sheet.Cells[11, 3] = _projectData[0].F1.ProcuratorName;
                        sheet.Cells[11, 6] = _projectData[0].F1.ProcuratorCertificateType;
                        sheet.Cells[12, 6] = _projectData[0].F1.ProcuratorCertificateCode;
                        sheet.Cells[11, 10] = _projectData[0].F1.ProcuratorCertificateTelephone;
                        sheet.Cells[13, 3] = _projectData[0].F1.PowerSetPattern;
                        sheet.Cells[14, 3] = _projectData[0].F1.NationalEconomyIndustryClassificationCode;
                        sheet.Cells[16, 3] = _projectData[0].F1.PreParcelCode;
                        sheet.Cells[16, 9] = _projectData[0].F1.ParcelCode;
                        sheet.Cells[17, 3] = _projectData[0].F1.UnitNumber;
                        sheet.Cells[18, 5] = _projectData[0].F1.MapScale;
                        sheet.Cells[19, 5] = _projectData[0].F1.MapCode;
                        sheet.Cells[20, 3] = _projectData[0].F1.ParcelRangeNorth;
                        sheet.Cells[21, 3] = _projectData[0].F1.ParcelRangeEast;
                        sheet.Cells[22, 3] = _projectData[0].F1.ParcelRangeSouth;
                        sheet.Cells[23, 3] = _projectData[0].F1.ParcelRangeWest;
                        sheet.Cells[24, 3] = _projectData[0].F1.Rank;
                        sheet.Cells[24, 9] = _projectData[0].F1.Price;
                        sheet.Cells[25, 3] = _projectData[0].F1.PermittedUsefor;
                        sheet.Cells[26, 5] = _projectData[0].F1.PermittedTypeCode;
                        sheet.Cells[25, 8] = _projectData[0].F1.PracticalUsefor;
                        sheet.Cells[26, 10] = _projectData[0].F1.PracticalTypeCode;
                        sheet.Cells[27, 3] = _projectData[0].F1.PermittedArea;
                        sheet.Cells[27, 6] = _projectData[0].F1.ParcelArea;
                        sheet.Cells[27, 10] = _projectData[0].F1.BuildLandArea;
                        sheet.Cells[29, 10] = _projectData[0].F1.BuildTotalArea;
                        string _landUseTime = _projectData[0].F1.LandUseStartTime + "--" + _projectData[0].F1.LandUseEndTime;
                        sheet.Cells[30, 3] = _landUseTime;//特殊 日期起止
                        sheet.Cells[31, 3] = _projectData[0].F1.CommonUse;
                        sheet.Cells[33, 3] = _projectData[0].F1.Explain;
                        continue;
                    }
                    else if (i == 2)// 表2
                    {
                        int l1, l2, l3, l4, l5, l6;
                        l1 = _projectData[0].F2.LandPointCodeList.Length;
                        l2 = _projectData[0].F2.LandPointTypeList.Length;
                        l3 = _projectData[0].F2.LandBoundaryExplain.Length;
                        l4 = _projectData[0].F2.LandBoundaryLocation.Length;
                        l5 = _projectData[0].F2.LandBoundaryType.Length;
                        l6 = _projectData[0].F2.LandPointDistance.Length;

                        if (l1 == l2 && l1 == l3 + 1 && l1 == l4 + 1 && l1 == l5 + 1 && l1 == l6 + 1)
                        {
                            sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[0];
                            sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[0] + 2)] = "√";

                            for (int n = 0; n < l3; n++)
                            {
                                sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[n + 1];
                                sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[n + 1] + 2)] = "√";
                                sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[n];
                                sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[n] + 8)] = "√";
                                sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[n] + 16)] = "√";
                                sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n];
                            }
                            continue;
                        }

                        else
                        { break; }

                    }
                    else if (i == 3)
                    {
                        int l1, l2, l3;
                        l1 = _projectData[0].F3.StartPointCodeList.Length;
                        l2 = _projectData[0].F3.InnerPointCodeList.Length;
                        l3 = _projectData[0].F3.EndPointCodeList.Length;
                        if (l1 == l2 && l2 == l3)
                        {
                            for (int n = 0; n < l1; n++)
                            {
                                sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[n];
                                sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[n];
                                sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[n];
                            }
                            continue;
                        }
                        else
                        { break; }
                    }
                    else if (i == 4) { continue; }// 图暂时没有
                    else if (i == 5)
                    {
                        sheet.Cells[2, 2] = _projectData[0].F5.BoundaryPointExplain;
                        sheet.Cells[3, 2] = _projectData[0].F5.MainBoundaryDirectionExplain;
                    }
                    else if (i == 6)
                    {
                        sheet.Cells[2, 2] = _projectData[0].F6.PowerInvestigateRecord;
                        sheet.Cells[20, 5] = _projectData[0].F6.PowerInvestigator;
                        sheet.Cells[20, 8] = _projectData[0].F6.PowerInvestigateDate;
                        sheet.Cells[21, 2] = _projectData[0].F6.SurveyRecord;
                        sheet.Cells[22, 5] = _projectData[0].F6.SurveyRecorder;
                        sheet.Cells[22, 8] = _projectData[0].F6.SurveyRecordDate;
                        sheet.Cells[23, 2] = _projectData[0].F6.AuditOpinion;
                        sheet.Cells[24, 5] = _projectData[0].F6.Auditor;
                        sheet.Cells[24, 8] = _projectData[0].F6.AuditOpinionDate;
                    }
                    else if (i == 7)
                    {
                        sheet.Cells[4, 4] = _projectData[0].F7.FixedCount;
                        for (int n = 0; n < _projectData[0].F7.FixedCode.Length; n++)
                        {
                            sheet.Cells[n + 6, 1] = _projectData[0].F7.FixedCode[n];
                            sheet.Cells[n + 6, 2] = _projectData[0].F7.LandOwnUseArea[n];
                            sheet.Cells[n + 6, 3] = _projectData[0].F7.LandUniqueArea[n];
                            sheet.Cells[n + 6, 4] = _projectData[0].F7.CommonArea[n];
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
                xls.Quit();//Excel程序退出//sheet,book,xls设置为null，防止内存泄露
                sheet = null;
                book = null;
                xls = null;
                GC.Collect();//系统回收资源
                return "fill form ok";
            }
            catch { return "fill form failed"; }
        }
        /// <summary>
        /// 将对应表格号的sheet生成一张pdf并另存，之后打印出来///////////生成后应该删除
        /// </summary>
        /// <param name="i">表格号 代表打印不同的表</param>
        /// <returns></returns>
        public static bool CreateAndPrintPdf(int i)
        {
            try
            {
                string projname = _importProjectName;
                if (Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname))
                {

                }
                else
                {
                    return false;
                }
                string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\DataCopy2Print.xlsx";//填写后的表格
                                                                                                                                              //启动Excel应用程序
                Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
                //如果表已经存在，可以用下面的命令打开
                _Workbook book = xls.Workbooks.Open(sourceFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                _Worksheet sheet;//定义sheet变量
                xls.Visible = false;//设置Excel后台运行
                xls.DisplayAlerts = false;//设置不显示确认修改提示
                                          //获取到对应的表格
                sheet = (_Worksheet)book.Worksheets.get_Item(i);
                sheet.Activate();
                //修改格式
                //if (i == 1)
                //{

                //}





                sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "D:\\ProjectFormTemplet\\form" + i + ".pdf"); //导出位置
                book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
                xls.Quit();//Excel程序退出
                           //sheet,book,xls设置为null，防止内存泄露
                sheet = null;
                book = null;
                xls = null;
                GC.Collect();//系统回收资源
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile("D:\\ProjectFormTemplet\\form" + i + ".pdf");

                doc.PrintDocument.Print();

                doc.Close();
                return true;
            }
            catch(Exception) { return false; }

        }

       
    }
#endregion
}

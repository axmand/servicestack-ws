using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System.IO;
using Spire.Pdf;
using static hardware.projectmanager.ImportProject;

namespace hardware.projectmanager
{
    public class Print
    {
        public static string projname = _importProjectName;
        // public static string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Form.xlsx";   //  此处是默认的表格模板                                                                  //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
        public static string destinationFile;

        public static Application xls;
        public static _Worksheet sheet;//定义sheet变量
        public static _Workbook book;
        public static int _Form2Number;
        public static int _Form3Number;
        #region 需要等待十几秒 淘汰
        /// <summary>
        /// 效率慢
        /// </summary>
        /// <returns></returns>
        public static bool FillForms()
        {
            try
            {
                string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\Form.xlsx";
                if (!File.Exists(destinationFile))
                {
                    string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Form.xlsx";   //  此处是默认的表格模板                                                                     //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourceFile, destinationFile, isrewrite);
                }
                else
                { }
                xls = new Microsoft.Office.Interop.Excel.Application();
                book = xls.Workbooks.Open(destinationFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                string Data = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\all.txt", Encoding.Default);
                List<Forms> _projectData = JsonConvert.DeserializeObject<List<Forms>>(Data);
                for (int formNum = 1; formNum < 3; formNum++)
                {
                    sheet = (_Worksheet)book.Worksheets.get_Item(formNum);
                    sheet.Activate();
                    if (formNum == 1)
                    {
                        sheet.Cells[2, 9] = _projectData[0].F1.TableID;
                        sheet.Cells[26, 6] = _projectData[0].F1.ParcelCode;
                        sheet.Cells[31, 6] = _projectData[0].F1.InvestigateOrganization;
                        sheet.Cells[43, 6] = _projectData[0].F1.InvestigateDate; //特殊 日期起止
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\Cover.pdf");
                    }
                    else if (formNum == 2)
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
                        sheet.Cells[30, 3] = _landUseTime; //特殊 日期起止
                        sheet.Cells[31, 3] = _projectData[0].F1.CommonUse;
                        //sheet.Cells[33, 3] = _projectData[0].F1.Explain;  说明不填
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F1.pdf");

                    }

                }


                book.Save();
                book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
                xls.Quit();//Excel
                sheet = null;
                book = null;
                xls = null;
                GC.Collect();

                return true;

            }
            catch (Exception) { return false; }
        }

        public static bool FillForms2()
        {
            try
            {
                //暂时支持40个点 11.14 
                string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2.xlsx";
                if (!File.Exists(destinationFile))
                {
                    string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\F2.xlsx";   //  此处是默认的表格模板                                                                     //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourceFile, destinationFile, isrewrite);
                }
                else
                { }
                xls = new Microsoft.Office.Interop.Excel.Application();
                book = xls.Workbooks.Open(destinationFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                string Data = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\all.txt", Encoding.Default);
                List<Forms> _projectData = JsonConvert.DeserializeObject<List<Forms>>(Data);
                sheet = (_Worksheet)book.Worksheets.get_Item(1);
                sheet.Activate();

                int l1, l2, l3, l4, l5, l6;
                l1 = _projectData[0].F2.LandPointCodeList.Length;
                l2 = _projectData[0].F2.LandPointTypeList.Length;
                //l3 = _projectData[0].F2.LandBoundaryExplain.Length;
                l4 = _projectData[0].F2.LandBoundaryLocation.Length;
                l5 = _projectData[0].F2.LandBoundaryType.Length;
                l6 = _projectData[0].F2.LandPointDistance.Length;
                if (l1 - 19 <= 0)
                {
                    if (l1 == l2 && l1 == l4 + 1 && l1 == l5 + 1 && l1 == l6 + 1)
                    {
                        sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[0];
                        sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[0] + 2)] = "√";

                        for (int n = 0; n < l4; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[n + 1];
                            sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[n];
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[n] + 16)] = "√";
                            //sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n]; 说明不填写
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2-1.pdf");
                    }
                    else { return false; }

                }
                else if (l1 - 19 <= 19)
                {
                    if (l1 == l2 && l1 == l4 + 1 && l1 == l5 + 1 && l1 == l6 + 1)
                    {
                        sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[0];
                        sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[0] + 2)] = "√";

                        for (int n = 0; n < 19; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[n + 1];
                            sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[n];
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[n] + 16)] = "√";
                            //sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n]; 说明不填写
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2-1.pdf");

                        sheet = (_Worksheet)book.Worksheets.get_Item(2);
                        sheet.Activate();
                        sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[19];
                        sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[19] + 2)] = "√";
                        for (int n = 0; n < l1 - 19; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[19 + n + 1];
                            sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[19 + n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[19 + n];
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[19 + n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[19 + n] + 16)] = "√";
                            //sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n]; 说明不填写
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2-2.pdf");
                    }
                    else { return false; }
                }
                else { return false; }// 超过38个点暂时不支持 11.14
                book.Save();
                book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
                xls.Quit();//Excel
                sheet = null;
                book = null;
                xls = null;
                GC.Collect();

                return true;

            }
            catch (Exception) { return false; }
        }

        public static bool FillForms3()
        {
            try
            {
                //暂时支持40条界址线 11.14 
                string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3.xlsx";
                if (!File.Exists(destinationFile))
                {
                    string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\F3.xlsx";   //  此处是默认的表格模板                                                                     //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourceFile, destinationFile, isrewrite);
                }
                else
                { }
                xls = new Microsoft.Office.Interop.Excel.Application();
                book = xls.Workbooks.Open(destinationFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                string Data = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\all.txt", Encoding.Default);
                List<Forms> _projectData = JsonConvert.DeserializeObject<List<Forms>>(Data);
                sheet = (_Worksheet)book.Worksheets.get_Item(1);
                sheet.Activate();

                int l1, l2, l3;
                l1 = _projectData[0].F3.StartPointCodeList.Length;
                l2 = _projectData[0].F3.InnerPointCodeList.Length;
                l3 = _projectData[0].F3.EndPointCodeList.Length;
                if (l1 == l2 && l2 == l3)
                {
                    if (l1 - 21 <= 0)
                    {
                        for (int n = 0; n < l1; n++)
                        {
                            sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[n];
                            sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[n];
                            sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[n];
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3-1.pdf");


                    }
                    else if (l1 - 21 <= 21)
                    {
                        for (int n = 0; n < 21; n++)
                        {
                            sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[n];
                            sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[n];
                            sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[n];
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3-1.pdf");
                        sheet = (_Worksheet)book.Worksheets.get_Item(2);
                        sheet.Activate();
                        for (int n = 0; n < l1 - 21; n++)
                        {
                            sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[21 + n];
                            sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[21 + n];
                            sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[21 + n];
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3-2.pdf");
                    }
                    else { return false; }// 超过42条线暂时不支持 11.14


                }

                else { return false; }
            }
            catch (Exception) { return false; }
            book.Save();
            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();

            return true;

        }
        #endregion

        public static bool FillForm()
        {
            try
            {
                string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\Forms.xlsx";


                if (!File.Exists(destinationFile))
                {
                    string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Forms.xlsx";   //  此处是默认的表格模板                                                                     //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourceFile, destinationFile, isrewrite);
                }
                else
                { }
                xls = new Microsoft.Office.Interop.Excel.Application();
                book = xls.Workbooks.Open(destinationFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                string Data = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\all.txt", Encoding.Default);
                List<Forms> _projectData = JsonConvert.DeserializeObject<List<Forms>>(Data);
                //填写完Form1
                for (int formNum = 1; formNum < 3; formNum++)
                {
                    sheet = (_Worksheet)book.Worksheets.get_Item(formNum);
                    sheet.Activate();
                    if (formNum == 1)
                    {
                        sheet.Cells[2, 9] = _projectData[0].F1.TableID;
                        sheet.Cells[26, 6] = _projectData[0].F1.ParcelCode;
                        sheet.Cells[31, 6] = _projectData[0].F1.InvestigateOrganization;
                        sheet.Cells[43, 6] = _projectData[0].F1.InvestigateDate; //特殊 日期起止
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\Cover.pdf");
                    }
                    else if (formNum == 2)
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
                        sheet.Cells[30, 3] = _landUseTime; //特殊 日期起止
                        sheet.Cells[31, 3] = _projectData[0].F1.CommonUse;
                        //sheet.Cells[33, 3] = _projectData[0].F1.Explain;  说明不填
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F1.pdf");

                    }

                }
                //填写Form2

                sheet = (_Worksheet)book.Worksheets.get_Item(3);
                sheet.Activate();

                int l1, l2, l3, l4, l5, l6;
                l1 = _projectData[0].F2.LandPointCodeList.Length;
                l2 = _projectData[0].F2.LandPointTypeList.Length;
                //l3 = _projectData[0].F2.LandBoundaryExplain.Length;
                l4 = _projectData[0].F2.LandBoundaryLocation.Length;
                l5 = _projectData[0].F2.LandBoundaryType.Length;
                l6 = _projectData[0].F2.LandPointDistance.Length;

                _Form2Number = l1 / 19;

                if (_Form2Number == 0 || l1 == 19)
                {
                    _Form2Number = 1;
                    if (l1 == l2 && l1 == l4 + 1 && l1 == l5 + 1 && l1 == l6 + 1)
                    {
                        sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[0];
                        sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[0] + 2)] = "√";

                        for (int n = 0; n < l4; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[n + 1];
                            sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[n];
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[n] + 16)] = "√";
                            //sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n]; 说明不填写
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2-1.pdf");
                    }
                    else { return false; }

                }
                else if (_Form2Number == 1 && l1 > 19)
                {
                    _Form2Number = 2;
                    if (l1 == l2 && l1 == l4 + 1 && l1 == l5 + 1 && l1 == l6 + 1)
                    {
                        sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[0];
                        sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[0] + 2)] = "√";

                        for (int n = 0; n < 19; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[n + 1];
                            sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[n];
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[n] + 16)] = "√";
                            //sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n]; 说明不填写
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2-1.pdf");

                        sheet = (_Worksheet)book.Worksheets.get_Item(4);
                        sheet.Activate();
                        sheet.Cells[4, 1] = _projectData[0].F2.LandPointCodeList[19];
                        sheet.Cells[4, (_projectData[0].F2.LandPointTypeList[19] + 2)] = "√";
                        for (int n = 0; n < l1 - 19; n++)
                        {
                            sheet.Cells[(2 * n + 5), 1] = _projectData[0].F2.LandPointCodeList[19 + n + 1];
                            sheet.Cells[(2 * n + 5), (_projectData[0].F2.LandPointTypeList[19 + n + 1] + 2)] = "√";
                            sheet.Cells[(2 * n + 4), 7] = _projectData[0].F2.LandPointDistance[19 + n];
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryType[19 + n] + 8)] = "√";
                            sheet.Cells[(2 * n + 4), (_projectData[0].F2.LandBoundaryLocation[19 + n] + 16)] = "√";
                            //sheet.Cells[(2 * n + 4), 19] = _projectData[0].F2.LandBoundaryExplain[n]; 说明不填写
                        }
                        sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F2-2.pdf");
                    }
                    else { return false; }
                }
                else { return false; }// 超过38个点暂时不支持 11.14

                //填写Form3
                sheet = (_Worksheet)book.Worksheets.get_Item(5);
                sheet.Activate();

                l1 = _projectData[0].F3.StartPointCodeList.Length;
                l2 = _projectData[0].F3.InnerPointCodeList.Length;
                l3 = _projectData[0].F3.EndPointCodeList.Length;
                _Form3Number = l1 / 21;
                if (_Form3Number == 0 || l1 == 21)
                {
                    _Form3Number = 1;
                    for (int n = 0; n < l1; n++)
                    {
                        sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[n];
                        sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[n];
                        sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[n];
                    }
                    sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3-1.pdf");


                }
                else if (_Form3Number == 1 && l1 > 21)
                {
                    _Form3Number = 2;
                    for (int n = 0; n < 21; n++)
                    {
                        sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[n];
                        sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[n];
                        sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[n];
                    }
                    sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3-1.pdf");
                    sheet = (_Worksheet)book.Worksheets.get_Item(6);
                    sheet.Activate();
                    for (int n = 0; n < l1 - 21; n++)
                    {
                        sheet.Cells[n + 5, 1] = _projectData[0].F3.StartPointCodeList[21 + n];
                        sheet.Cells[n + 5, 2] = _projectData[0].F3.InnerPointCodeList[21 + n];
                        sheet.Cells[n + 5, 3] = _projectData[0].F3.EndPointCodeList[21 + n];
                    }
                    sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\F3-2.pdf");

                }
                else { return false; }// 超过42条线暂时不支持 11.14

                book.Save();
                book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
                xls.Quit();//Excel
                sheet = null;
                book = null;
                xls = null;
                GC.Collect();

                return true;

            }
            catch (Exception) { return false; }
        }

        public static bool PrintForm()
        {
            try
            {
                String[] files = new String[2 + _Form2Number + _Form3Number];

                string _filename = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\";

                if (_Form2Number == 1)
                {
                    files = new String[] { _filename + "Cover.pdf", _filename + "F1.pdf", _filename + "F2-1.pdf", _filename + "F3-1.pdf" };
                }
                else if (_Form2Number == 2 && _Form3Number == 2)
                {
                    files = new String[] { _filename + "Cover.pdf", _filename + "F1.pdf", _filename + "F2-1.pdf", _filename + "F2-2.pdf", _filename + "F3-1.pdf", _filename + "F3-2.pdf" };
                }
                else
                {
                    files = new String[] { _filename + "Cover.pdf", _filename + "F1.pdf", _filename + "F2-1.pdf", _filename + "F3-1.pdf", _filename + "F3-2.pdf" };
                }

                string outputFile = _filename + "Print.pdf";
                PdfDocumentBase doc2 = PdfDocument.MergeFiles(files);
                doc2.Save(outputFile, FileFormat.PDF);
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(_filename + "Print.pdf");
                doc.PrintDocument.DefaultPageSettings.PrinterSettings.Duplex = System.Drawing.Printing.Duplex.Vertical;
                doc.PrintDocument.Print();
                doc.Close();
                return true;
            }
            catch (Exception e)
            {
                var s = e.ToString();
                return false;
            }

            
        }
    }
}



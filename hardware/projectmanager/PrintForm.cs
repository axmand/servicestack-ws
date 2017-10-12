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
    public class PrintForm
    {

        public static string projname = _importProjectName;
        // public static string sourceFile = System.IO.Directory.GetCurrentDirectory() + "\\Form.xlsx";   //  此处是默认的表格模板                                                                  //string  = @"D:\\ProjectFormTemplet\\testCopy2.xlsx";
        public static string destinationFile;

        public static Application xls;
        public static _Worksheet sheet;//定义sheet变量
        public static _Workbook book;

        public static bool receivePrintForm(int formNum)
        {
            try
            {
                string destinationFile = System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\Form.xlsx"; ;
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
                sheet = (_Worksheet)book.Worksheets.get_Item(formNum);
                sheet.Activate();

                if (formNum == 1)
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
                    sheet.Cells[33, 3] = _projectData[0].F1.Explain;
                    //排版
                    AutoRange(33, 3, 1);
                }
                else if (formNum == 2)// 表2
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
                            AutoRange((2 * n + 4), 19, 2); // 效率有待提高！！！ 10.10
                        }

                    }

                    else
                    { return false; }

                }
                else if (formNum == 3)
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

                    }
                    else
                    { return false; }
                }
                else if (formNum == 4) { return false; }// 图暂时没有
                else if (formNum == 5)
                {
                    sheet.Cells[2, 2] = _projectData[0].F5.BoundaryPointExplain;
                    sheet.Cells[29, 2] = _projectData[0].F5.MainBoundaryDirectionExplain;
                    AutoRange(29, 2, 5);
                    AutoRange(2, 2, 5);
                }
                else if (formNum == 6)
                {
                    sheet.Cells[2, 2] = _projectData[0].F6.PowerInvestigateRecord;
                    sheet.Cells[19, 5] = _projectData[0].F6.PowerInvestigator;
                    sheet.Cells[19, 8] = _projectData[0].F6.PowerInvestigateDate;
                    sheet.Cells[20, 2] = _projectData[0].F6.SurveyRecord;
                    sheet.Cells[38, 5] = _projectData[0].F6.SurveyRecorder;
                    sheet.Cells[38, 8] = _projectData[0].F6.SurveyRecordDate;
                    sheet.Cells[39, 2] = _projectData[0].F6.AuditOpinion;
                    sheet.Cells[56, 5] = _projectData[0].F6.Auditor;
                    sheet.Cells[56, 8] = _projectData[0].F6.AuditOpinionDate;
                    AutoRange(2, 2, 6);
                    AutoRange(20, 2, 6);
                    AutoRange(39, 2, 6);
                }
                else if (formNum == 7)
                {
                    sheet.Cells[2, 2] = _projectData[0].F1.Location;
                    sheet.Cells[3, 2] = _projectData[0].F1.ParcelCode;
                    sheet.Cells[4, 2] = _projectData[0].F1.ParcelArea;
                    sheet.Cells[4, 4] = _projectData[0].F7.FixedCount;
                    int intFixedCount;
                    int.TryParse(_projectData[0].F7.FixedCount, out intFixedCount);
                    if (intFixedCount > 11)
                    {
                        for (int n = 0; n < intFixedCount - 11; n++)
                        {
                            Range insertRow = (Range)sheet.Rows[8, Missing.Value];
                            insertRow.Insert(Missing.Value, Missing.Value);
                        }
                    }
                    for (int n = 0; n < _projectData[0].F7.FixedCode.Length; n++)
                    {

                        sheet.Cells[n + 6, 1] = _projectData[0].F7.FixedCode[n];
                        sheet.Cells[n + 6, 2] = _projectData[0].F7.LandOwnUseArea[n];
                        sheet.Cells[n + 6, 3] = _projectData[0].F7.LandUniqueArea[n];
                        sheet.Cells[n + 6, 4] = _projectData[0].F7.CommonArea[n];
                    }
                    //  F7 三个合计和备注没有字段

                }
                return true;


            }
            catch (Exception)
            { //var s = e.ToString(); 
                return false;
            }
        }

        public static void AutoRange(int inputRow, int inputCol, int sheetNumber)
        {
            try
            {

                ///此处的item是要选择的
                sheet = (_Worksheet)book.Worksheets.get_Item(sheetNumber);
                sheet.Activate();
                ///开始调整间距

                int firstRowNum = ((Range)sheet.Cells[inputRow, inputCol]).MergeArea.Row;
                int firstColNum = ((Range)sheet.Cells[inputRow, inputCol]).MergeArea.Column;
                int tempRowCount = ((Range)sheet.Cells[inputRow, inputCol]).MergeArea.Rows.Count;
                int tempColCount = ((Range)sheet.Cells[inputRow, inputCol]).MergeArea.Columns.Count;
                double beforeWidth = 0.00;

                for (int n = 1; n <= tempColCount; n++)
                {
                    beforeWidth = beforeWidth + ((Range)sheet.Columns[firstColNum + n - 1]).ColumnWidth;
                }

                for (int n = 1; n <= tempRowCount; n++)
                {
                    ((Range)sheet.Rows[firstRowNum + n - 1, Missing.Value]).Rows.AutoFit();
                }

                Worksheet temp = book.Sheets.Add();
                temp.Cells[1, 1] = ((Range)sheet.Cells[inputRow, inputCol]).Value2;
                temp.Cells.WrapText = true;

                ((Range)temp.Cells[1, 1]).ColumnWidth = beforeWidth;
                ((Range)temp.Cells[1, 1]).Font.Name = ((Range)sheet.Cells[inputRow, inputCol]).Font.Name;
                ((Range)temp.Cells[1, 1]).Font.Size = ((Range)sheet.Cells[inputRow, inputCol]).Font.Size;
                ((Range)temp.Cells[1, 1]).RowHeight = 0;
                ((Range)temp.Cells[1, 1]).Rows.AutoFit();
                double beforeOneRowHeight = ((Range)sheet.Cells[inputRow, inputCol]).RowHeight;// 一个单元格的高度

                double beforeHeight = beforeOneRowHeight * tempRowCount;

                double laterHeight = ((Range)temp.Cells[1, 1]).RowHeight;


                if (beforeHeight < laterHeight)
                {

                    // double tempHeight = ((Range)temp.Cells[1, 1]).RowHeight;

                    for (int n = 1; n <= tempRowCount; n++)
                    {
                        ((Range)sheet.Cells[firstRowNum + n - 1, inputCol]).EntireRow.RowHeight = laterHeight / tempRowCount + 2;
                    }

                }
                else
                {

                }
                xls.DisplayAlerts = false;
                temp.Delete();
            }
            catch (Exception e)
            {
                //var s = e;
                //Console.WriteLine(s.ToString());
                //Console.ReadKey();
            }
        }

        public static void endAndPrintExcel()
        {
            try
            {
                sheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\pirntForm.pdf"); //导出位置
                                                                                                                                                                         ///资源回收
                book.Save();
                book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
                xls.Quit();//Excel
                sheet = null;
                book = null;
                xls = null;
                GC.Collect();

                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(System.IO.Directory.GetCurrentDirectory() + "\\Project\\" + projname + "\\Forms\\pirntForm.pdf");

                doc.PrintDocument.Print();

                doc.Close();
            }
            catch (Exception) { }

        }


    }
}

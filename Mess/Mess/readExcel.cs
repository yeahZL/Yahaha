using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Trash
{
    public class readExcel
    {
        private Workbook wb = null;
        private Application excel = null;

        public void OpenExcel(string strFileName)
        {
            List<TrashInfo> i = new List<TrashInfo>();
            object missing = System.Reflection.Missing.Value;
            excel = new Application();//lauch excel application
            if (excel == null)
            {
                return ;
            }
            else
            {
                excel.Visible = false; excel.UserControl = true;
                // 以只读的形式打开EXCEL文件
                wb = excel.Application.Workbooks.Open(strFileName, missing, true, missing, missing, missing,
                 missing, missing, missing, true, missing, missing, missing, missing, missing);             

            }          
            
        }

        public List<riverInfo> readTables()
        {
            List<riverInfo> items = new List<riverInfo>();
            int n = wb.Worksheets.Count;

            #region 挨个读取每个sheet数据
            //for (int i = 2; i <= n; ++i)
            //{
            //    Worksheet ws = (Worksheet)wb.Worksheets.get_Item(i); //读取第i个sheet
            //    int rowsint = ws.UsedRange.Cells.Rows.Count;

            //    Range rngD = ws.Cells.get_Range("D2", "D" + rowsint);
            //    Range rngF = ws.Cells.get_Range("F2", "F" + rowsint);
            //    Range rngG = ws.Cells.get_Range("G2", "G" + rowsint);

            //    object[,] arrayFigure = (object[,])rngD.Value2;
            //    object[,] arrayCategory = (object[,])rngF.Value2;
            //    object[,] arrayRemarks = (object[,])rngG.Value2;

            //    for (int j = 1; j < rowsint; ++j)
            //    {
            //        if ((arrayCategory[j, 1].ToString() == "建筑垃圾" || arrayCategory[j, 1].ToString() == "生活垃圾") &&
            //            (arrayRemarks[j, 1] == null || arrayRemarks[j, 1].ToString() == " "))
            //        {
            //            TrashInfo item = new TrashInfo()
            //            {
            //                Figure = arrayFigure[j, 1].ToString(),
            //                Category = arrayCategory[j, 1].ToString()
            //            };
            //            items.Add(item);
            //        }
            //    }
            //}
            #endregion

            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1); //读取第几个sheet？
            int rowsint = ws.UsedRange.Cells.Rows.Count;
            //rowsint = 60;

            Range rngA = ws.Cells.get_Range("A3", "A" + rowsint);
            Range rngB = ws.Cells.get_Range("B3", "B" + rowsint);
            Range rngD = ws.Cells.get_Range("D3", "D" + rowsint);
            Range rngE = ws.Cells.get_Range("E3", "E" + rowsint);
            Range rngH = ws.Cells.get_Range("H3", "H" + rowsint);
            Range rngI = ws.Cells.get_Range("I3", "I" + rowsint);

            object[,] arrayIdentifier = (object[,])rngA.Value2;
            object[,] arrayName = (object[,])rngB.Value2;
            object[,] arrayCategory = (object[,])rngD.Value2;
            object[,] arrayRank = (object[,])rngE.Value2;
            object[,] arrayLength = (object[,])rngH.Value2;
            object[,] arrayArea = (object[,])rngI.Value2;

            for (int j = 1; j < rowsint-1; ++j)
            {
                //string strDate1 = DateTime.FromOADate(Convert.ToInt32(arrayDate1[j, 1])).ToString();
                //string strDate2 = DateTime.FromOADate(Convert.ToInt32(arrayDate2[j, 1])).ToString();
                //Investigation i1 = new Investigation()
                //{
                //    date = DateTime.Parse(strDate1).ToString("yyyy-MM-dd"),
                //    Category = Convert.ToString(arrayCategory1[j, 1])
                //};
                //Investigation i2 = new Investigation()
                //{                    
                //    date = DateTime.Parse(strDate2).ToString("yyyy-MM-dd"),
                //    Category = Convert.ToString(arrayCategory2[j, 1])
                //};
                //string figure = Convert.ToString(arrayFigure[j, 1]);
                //string i = figure.Substring(figure.Length - 1, 1);
                //if (!IsNumeric(i)) figure = figure.Remove(figure.Length - 2);

                //riverInfo item = new riverInfo()
                //{
                //    Figure = figure,
                //    invest1 = i1,
                //    invest2 = i2,
                //};
                riverInfo item = new riverInfo()
                {                    
                    identifier = arrayIdentifier[j, 1] == null ? null : arrayIdentifier[j, 1].ToString(),
                    name = arrayName[j, 1] == null ? null : arrayName[j, 1].ToString(),
                    category = arrayCategory[j, 1] == null ? null : arrayCategory[j, 1].ToString(),
                    rank = arrayRank[j, 1] == null ? null : arrayRank[j, 1].ToString(),
                    length = arrayLength[j, 1] == null ? null : arrayLength[j, 1].ToString(),
                    area = arrayArea[j, 1] == null ? null : arrayArea[j, 1].ToString(),
                };

                items.Add(item);
            }
            return items;
        }

        public List<TrashInfo> readTrashTables()
        {
            List<TrashInfo> items = new List<TrashInfo>();
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1); //读取第几个sheet？
            int rowsint = ws.UsedRange.Cells.Rows.Count;
            Range rngB = ws.Cells.get_Range("B2", "B" + rowsint);
            Range rngE = ws.Cells.get_Range("E2", "E" + rowsint);

            object[,] arrayCategory = (object[,])rngB.Value2;
            object[,] arrayFigure = (object[,])rngE.Value2;

            for (int j = 1; j < rowsint; ++j)
            {
                TrashInfo item = new TrashInfo()
                {
                    Category = arrayCategory[j, 1] == null ? null : arrayCategory[j, 1].ToString(),
                    Figure = arrayFigure[j, 1] == null ? null : arrayFigure[j, 1].ToString(),
                };
                items.Add(item);
            }            
            return items;
        }

        public void closeExcel()
        {
            wb.Close();
            excel.Quit();
            excel = null;
            Process[] procs = Process.GetProcessesByName("excel");


            foreach (Process pro in procs)
            {
                pro.Kill();//没有更好的方法,只有杀掉进程
            }
            GC.Collect();
        }

        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

    }
}

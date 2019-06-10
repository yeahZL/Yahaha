using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MSWord = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace Trash
{
    class ReportFromTemplate
    {
        private _Application wordApp = null;
        private _Document wordDoc = null;

        public _Application Application
        {
            get { return wordApp; }
            set { wordApp = value; }
        }

        public _Document Document
        {
            get { return wordDoc; }
            set { wordDoc = value; }
        }
        
        //通过模板创建新文档
        public void CreateNewDucument(string filePath)
        {
            killWinWordProcess();
            wordApp = new ApplicationClass();
            wordApp.Visible = true;
            wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApp.Visible = false;
            object miss = System.Reflection.Missing.Value;
            object templateName = filePath;
            wordDoc = wordApp.Documents.Open(ref templateName, ref miss, ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
        }

        // 杀掉winword.exe进程
        public void killWinWordProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            foreach (System.Diagnostics.Process process in processes)
            {
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }

        //保存新文件
        public void SaveDocument(string filePath)
        {
            object fileName = filePath;
            object format = WdSaveFormat.wdFormatDocument; //保存格式
            object miss = System.Reflection.Missing.Value;
            wordDoc.SaveAs(ref fileName, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            //关闭wordDoc,wordApp对象
            object SaveChanges = WdSaveOptions.wdSaveChanges;
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;
            object RouteDocument = false;
            wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
        }

        //写入页眉
        public void InsertHeader(string docHeader)
        {
            wordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            wordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
            wordApp.ActiveWindow.ActivePane.Selection.InsertAfter(docHeader); //页眉内容
        }
 
        //在书签处插入值
        public bool InsertValue(string bookmark, string value)
        {
            object bkObj = bookmark;
            if (wordApp.ActiveDocument.Bookmarks.Exists(bookmark))
            {
                //该方法会截取value，输入文本内容显示不全
                //wordApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                //wordApp.Selection.TypeText(value);
 
                Range range = wordApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Range;//表格插入位置 
                range.Text = value;//在书签处插入文字内容  
                return true;
            }
            return false;
        }
 
        //在书签处插入表格
        public Table InsertTable(string bookmark, int rows, int columns)
        {
            object miss = System.Reflection.Missing.Value;
            object oStart = bookmark;
            Range range = wordDoc.Bookmarks.get_Item(ref oStart).Range; //表格插入位置
            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);
            //设置表的样式
            newTable.Borders.Enable = 1; //允许有实线边框
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt; //边框宽度
            //newTable.PreferredWidth = 17;
            newTable.AllowAutoFit = true;
            return newTable;
        }
 
        //给表格中单元格插入元素，table所在表格，row行号，column列号,value插入的元素
        public void InsertCell(Microsoft.Office.Interop.Word.Table table, int row, int column, string value)
        {
            table.Cell(row, column).Range.Text = value;
        }
 
        //给表格插入一行数据，n为表格的序号，row行号，column列数，values插入的值
        public void InsertCell(Microsoft.Office.Interop.Word.Table table, int row, int columns, string[] values)
        {
            for (int i = 0; i < columns; i++)
            {
                table.Cell(row, i + 1).Range.Text = values[i];
            }
        }

        public void CopyTable()
        {
            wordDoc.Tables[1].Select();
            wordApp.Selection.Copy();
            object miss = System.Reflection.Missing.Value;
            object unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            wordApp.Selection.EndKey(ref unit, ref miss);
            object pBreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            wordApp.Selection.InsertBreak(ref pBreak);
            wordDoc.Paragraphs.Last.Range.Font.Size = 10;
            wordApp.Selection.Paste();

        }

        public void InsertPicures(Microsoft.Office.Interop.Word.Table table, int row, int column, string value,
            object count, int i, float width = 184.56f, float height = 184.56f )
        {
            //row和column表示行列数,需要设置成上一行的最后一列
            //value表示图片绝对路径
            //count表示从上一行的最后一列开始，到本行本列的最后一个字符的个数。。。。。。。
            //i表示本文档的第i个图片
            //wdith和height表示宽和高
            //1cm=28.35px
            table.Cell(row, column).Select();

            object miss = System.Reflection.Missing.Value;
            object unit = Microsoft.Office.Interop.Word.WdUnits.wdCharacter;//切换字符;
            wordApp.Selection.MoveRight(ref unit, ref count, ref miss);//移动焦点

            object LinkToFile1 = false;
            object SaveWithDocument1 = true;
            object Anchor1 = wordDoc.Application.Selection.Range;
            wordDoc.InlineShapes.AddPicture(value, ref LinkToFile1, ref SaveWithDocument1, ref Anchor1);
            wordDoc.InlineShapes[i].Width = width;//图片宽度
            wordDoc.Application.ActiveDocument.InlineShapes[i].Height = height;//图片高度
        }

        public bool InsertInfo(ref int i, ResultItem value, ref int picIndex, string insitu, string screenShot)
        {
            //青浦垃圾模板
            //i是table的序号，作为ref格式。
            //picIndex是图片的序号
            /*此函数作为青浦垃圾模板生成。
             * 此方法只要将一个截图文件和一个现场照片添加进word中即可。
             */
            InsertCell(wordDoc.Tables[i], 1, 2, value.RegionID);
            InsertCell(wordDoc.Tables[i], 1, 4, value.Figure);
            string Location = value.Address + "\n" + "北纬 " + value.Location.Y + "   " + "东经 " + value.Location.X;
            InsertCell(wordDoc.Tables[i], 2, 2, Location);
            InsertCell(wordDoc.Tables[i], 3, 2, value.Category);
            InsertCell(wordDoc.Tables[i], 4, 2, value.Area);

            string screen = screenShot + @"\" + value.Figure + ".jpg"; ;
            string fileName = findPicture(insitu, value.Figure);

            InsertPicures(wordDoc.Tables[i], 4, 2, screen, 11, picIndex);
            picIndex++;
            if (fileName != null)
            {
                InsertPicures(wordDoc.Tables[i], 5, 1, fileName, 8, picIndex, 360.05f, 238.14f);
                picIndex++;
            }
            else
            {
                InsertCell(wordDoc.Tables[i], 6, 1, "无");
            }


            //InsertCell(wordDoc.Tables[i], 1, 2, Convert.ToString(i-1));
            //InsertCell(wordDoc.Tables[i], 1, 4, value.RegionID);
            //InsertCell(wordDoc.Tables[i], 1, 6, value.Figure);
            //InsertCell(wordDoc.Tables[i], 2, 2, Location);
            //InsertCell(wordDoc.Tables[i], 3, 2, value.Category);
            //InsertCell(wordDoc.Tables[i], 4, 2, value.Area);

            //InsertCell(wordDoc.Tables[i], 5, 2, "2018年8月份");
            //InsertCell(wordDoc.Tables[i], 7, 2, "2018年11月份");

            //插入遥感图像_宁波垃圾截图
            //string fileName = @"C:\Users\DELL\Documents\Visual Studio 2010\Projects\Trash\Trash\bin\Debug\" + value.Figure + ".jpg";
            //插入现场照片
            //string Nov = @"C:\Users\DELL\Desktop\November\" + value.Figure + ".jpg";
            //string Dec = @"C:\Users\DELL\Desktop\December\" + value.Figure + ".jpg";
            //string NovZoom = @"C:\Users\DELL\Desktop\November\zoom\" + value.Figure + ".jpg";
            //string DecZoom = @"C:\Users\DELL\Desktop\December\zoom\" + value.Figure + ".jpg";
            //InsertPicures(wordDoc.Tables[i], 6, 2, Nov, 0, 4 * i - 7, 174.64f, 174.64f);
            //InsertPicures(wordDoc.Tables[i], 6, 3, NovZoom, 0, 4 * i - 6, 223.97f, 223.97f);
            //InsertPicures(wordDoc.Tables[i], 8, 2, Dec, 0, 4 * i - 5, 174.64f, 174.64f);
            //InsertPicures(wordDoc.Tables[i], 8, 3, DecZoom, 0, 4 * i - 4, 223.97f, 223.97f);            

            return true;
            //if (File.Exists(fileName) && File.Exists(Insitu))
            //{
            //    InsertPicures(wordDoc.Tables[i], 4, 2, fileName, 11, 2 * i - 3);
            //    InsertPicures(wordDoc.Tables[i], 5, 1, Insitu, 8, 2 * i - 2, 360.04f, 238.14f);
            //    return true;
            //}
            //else
            //{
            //    Console.WriteLine("下列文件不存在（或其中的一个不存在）:\r\n{0}\r\n{1}\r\n\r\n", fileName, Insitu);
            //    saveLog(fileName, Insitu);
            //    return false;
            //}
        }

        public bool InsertInfo(ref int i, ResultItem value, ref int picIndex, string screenShot)
        {
            //青浦垃圾模板
            //i是table的序号，作为ref格式。
            //picIndex是图片的序号
            /*此函数作为青浦垃圾模板生成。
             * 此方法只要将一个截图文件和一个现场照片添加进word中即可。
             */
            InsertCell(wordDoc.Tables[i], 1, 2, value.District);
            InsertCell(wordDoc.Tables[i], 1, 4, value.Figure);
            string Location = value.Address + "\n" + "北纬 " + value.Location.Y + "   " + "东经 " + value.Location.X;
            InsertCell(wordDoc.Tables[i], 2, 2, Location);
            InsertCell(wordDoc.Tables[i], 3, 2, value.Category);
            InsertCell(wordDoc.Tables[i], 4, 2, value.Area);

            string screen = screenShot + @"\" + value.Figure + ".jpg"; ;
            //string fileName = findPicture(insitu, value.Figure);

            InsertPicures(wordDoc.Tables[i], 4, 2, screen, 11, picIndex, 383.86f, 383.86f);
            picIndex++;

            return true;

        }

        private void saveLog(String name, String town)
        {
            using (FileStream fs = new FileStream(@"C:\Users\DELL\Desktop\log.txt", FileMode.Append, FileAccess.Write))
            {
                fs.Lock(0, fs.Length);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(String.Format("下列文件不存在（或其中的一个不存在）:\r\n{0}\r\n{1}\r\n\r\n", name, town));
                fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。
                sw.Flush();
            }
        }

        private string findPicture(string path, string condition)
        {
            string filename = null;
            string[] file = Directory.GetFiles(path);
            foreach (string f in file)
            {
                if (f.Contains(condition))
                    filename = f;
            }
            return filename;
        }

        public void deletePage(object page)
        {
            object oMissing = System.Reflection.Missing.Value;
            int pages = wordDoc.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages, ref oMissing);
            object objWhat = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage;
            object objWhich = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToAbsolute;
            Microsoft.Office.Interop.Word.Range range1 = wordDoc.GoTo(ref objWhat, ref objWhich, ref page, ref oMissing);
            Microsoft.Office.Interop.Word.Range range2 = range1.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage);
            object bjStart = range1.Start;
            object objEnd = range2.Start;
            object Unit = (int)Microsoft.Office.Interop.Word.WdUnits.wdCharacter;
            object Count = 1;
            wordDoc.Range(ref bjStart, ref  objEnd).Delete(ref  Unit, ref  Count);

            object what=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine;
            object which=Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst;
            object count=1;
            wordApp.Selection.GoTo(ref what, ref which, ref count, ref oMissing).Delete();
        }

        public void insertFirstPage(string[] contents)
        {
            object oMissing = System.Reflection.Missing.Value;
            object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine;
            object which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst;
            object count = 1;
            wordApp.Selection.GoTo(ref what, ref which, ref count, ref oMissing);
            object pBreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            wordApp.Selection.InsertBreak(ref pBreak);

            wordApp.Selection.GoTo(ref what, ref which, ref count, ref oMissing);
            object rowcount = 24;
            for (int i = 0; i < (int)rowcount; ++i)
            {
                wordApp.Selection.TypeParagraph();
            }            
            //wordApp.Selection.GoTo(ref what, ref which, ref rowcount, ref oMissing);
            //wordApp.Selection.TypeText("1111");
            Microsoft.Office.Interop.Word.Range rng1 = wordDoc.Paragraphs[(int)rowcount - 2].Range;       
            rng1.Font.Size = 26;
            rng1.Font.Name = "黑体";            
            rng1.Text = contents[0];
            rng1.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            Microsoft.Office.Interop.Word.Range rng2 = wordDoc.Paragraphs[(int)rowcount - 1].Range;
            rng2.Font.Size = 26;
            rng2.Font.Name = "黑体";
            rng2.Text = contents[1];
            rng2.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }

        public void insertPage(string[] contents)
        {
            object miss = System.Reflection.Missing.Value;
            object unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            wordApp.Selection.EndKey(ref unit, ref miss);
            //object oMissing = System.Reflection.Missing.Value;
            //object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine;
            //object which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToLast;
            //object count = 99999999;
            //wordApp.Selection.GoTo(ref what, ref which, ref count, ref oMissing);
            object pBreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            wordApp.Selection.InsertBreak(ref pBreak);

            wordApp.Selection.EndKey(ref unit, ref miss);
            object rowcount = 22;
            for (int i = 0; i < (int)rowcount; ++i)
            {
                wordApp.Selection.TypeParagraph();
            }
            wordApp.Selection.EndKey(ref unit, ref miss);            
            Range rng1 = wordDoc.Paragraphs.Last.Range;            
            object style = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleHeading1;
            rng1.set_Style(style);            
            rng1.Text = contents[0];
            rng1.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //rng1.ParagraphFormat.OutlineLevel = Microsoft.Office.Interop.Word.WdOutlineLevel.wdOutlineLevelBodyText;

            wordApp.Selection.EndKey(ref unit, ref miss);
            wordApp.Selection.TypeParagraph();
            Range rng2 = wordDoc.Paragraphs.Last.Range;
            rng2.Font.Size = 20;
            rng2.Font.Name = "黑体";
            rng2.Text = contents[1];
            
            rng2.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

        }

        public void insertPageNumber(string strType, bool bHeader)
        {
            #region 此处是为了在word的开头添加分节符
            object miss = System.Reflection.Missing.Value;
            object unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            wordApp.Selection.HomeKey(ref unit, ref miss);

            object pBreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakNextPage;
            wordApp.Selection.InsertBreak(ref pBreak);
            #endregion

            object oAlignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
            object oFirstPage = bHeader;
            WdHeaderFooterIndex WdFooterIndex = WdHeaderFooterIndex.wdHeaderFooterPrimary;
            switch (strType)
            {
                case "Center":
                    oAlignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
                    break;

                case "Right":
                    oAlignment = WdPageNumberAlignment.wdAlignPageNumberRight;
                    break;

                case "Left":
                    oAlignment = WdPageNumberAlignment.wdAlignPageNumberLeft;
                    break;
            }
            wordApp.Selection.Sections[1].Footers[WdFooterIndex].LinkToPrevious = false;
            wordApp.Selection.Sections[1].Footers[WdFooterIndex].PageNumbers.RestartNumberingAtSection = true;
            wordApp.Selection.Sections[1].Footers[WdFooterIndex].PageNumbers.StartingNumber = 1;
            wordApp.Selection.Sections[1].Footers[WdFooterIndex].PageNumbers.Add(ref oAlignment, ref oFirstPage);
        }

    }
}

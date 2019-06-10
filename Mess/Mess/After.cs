using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using Trash;
using System.Collections;
using System.IO;

namespace Mess
{
    public partial class After : Form
    {
        private MayGod mg = new MayGod();
        private List<string> labelItems = new List<string>();  //记录图片标注信息
        List<ResultItem> allValue = new List<ResultItem>();
        private string dstPath;
        private string flag;

        public After()
        {
            InitializeComponent();
            labelItems.Add("图号");
            labelItems.Add("镇名");
            labelItems.Add("位置");
            labelItems.Add("经纬度");
            
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if(phase.Text==null || shpPathText.Text==null|| insituText.Text==null||excelPath.Text==null)
            {
                MessageBox.Show("还有空白未填写！");
                return;
            }
            dstPath = @"D:\青浦区垃圾巡查\" + phase.Text + "期";
            flag = phase.Text.Substring(phase.Text.IndexOf("-"));

            resultShow.AppendText("开始分配类别...\r\n");
            resultShow.Refresh();
            confirmCategory();
            resultShow.AppendText("类别分配完成√ \r\n \r\n");
            resultShow.AppendText("开始截图...\r\n");
            resultShow.Refresh();
            screenShot();
            resultShow.AppendText("截图完成√ \r\n \r\n");
            resultShow.AppendText("开始生成报告...\r\n");
            resultShow.Refresh();
            ThreadTestEach();
            ThreadTestAll();
            resultShow.AppendText("报告完成√\r\n");
        }       
       
        private void confirmCategory()
        {
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpPathText.Text);
            shp[1] = System.IO.Path.GetFileName(shpPathText.Text);

            if (shp[0] == null) return;
            IFeatureClass pFC = mg.openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();

            Hashtable i = mg.Index(pFC);

            readExcel t = new readExcel();
            t.OpenExcel(excelText.Text);
            List<TrashInfo> trashItems = t.readTrashTables();
            t.closeExcel();

            IQueryFilter tQueryFilter = new QueryFilterClass();
            tQueryFilter.WhereClause = "";
            tQueryFilter.AddField(pFC.OIDFieldName);
            //Invoke(new MethodInvoker(() =>
            //{
            //    proWork.Maximum = pFC.FeatureCount(tQueryFilter);
            //    proWork.Value = 0;
            //}));

            //int number = 1;
            while (pFeature != null)
            {
                ResultItem value = new ResultItem();
                value = mg.getFields(i, pFeature);
                
                #region 查找与excel中匹配的数据
                TrashInfo right = trashItems.Find(
                delegate(TrashInfo T)
                {
                    return T.Figure == value.Figure;
                });
                
                pFeature.set_Value((int)i["垃圾类别"], right.Category); //如果垃圾类别没设置，则取消注释
                pFeature.Store();
                #endregion
                //Invoke(new MethodInvoker(() =>
                //{
                //    proWork.Value = number++;
                //}));
                pFeature = pFeatureCur.NextFeature();
            }
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ShpPath = mg.openShpDialog();
            string result = mg.checkValidShp(ShpPath);
            if (result != null && result != "") MessageBox.Show(result, "错误");
            shpPathText.Text = ShpPath[0] + @"\" + ShpPath[1];
        }

        private void screenShot()
        {            
            if (!Directory.Exists(dstPath + @"\截图")) Directory.CreateDirectory(dstPath + @"\截图");
            mg.setFields(@"D:\青浦区垃圾巡查\无标题.mxd", shpPathText.Text, dstPath + @"\截图", labelItems, 15, ref axMapControl1);
            mg.outPut();
            return;
        }

        private void ThreadTestEach()
        {
            List<string> towns = new List<string>();
            towns.Add("金泽镇");
            towns.Add("练塘镇");
            towns.Add("朱家角镇");
            towns.Add("盈浦街道");
            towns.Add("夏阳街道");
            //towns.Add("青东农场");
            towns.Add("徐泾镇");
            towns.Add("赵巷镇");
            towns.Add("白鹤镇");
            towns.Add("重固镇");
            towns.Add("华新镇");
            towns.Add("香花桥街道");

            getAllshp(shpPathText.Text);
            //int n = 1;
            //Invoke(new MethodInvoker(() =>
            //{
            //    proWork.Maximum = towns.Count;
            //    proWork.Value = 0;
            //}));
            foreach (string town in towns)
            {
                createEach(allValue, town);
                createEachFuhe(allValue, town);
                //Invoke(new MethodInvoker(() =>
                //{
                //    proWork.Value = n++;
                //}));
            }            
        }

        private void ThreadTestAll()
        {
            List<string> towns = new List<string>();
            towns.Add("金泽镇");
            towns.Add("练塘镇");
            towns.Add("朱家角镇");
            towns.Add("盈浦街道");
            towns.Add("夏阳街道");
            //towns.Add("青东农场");
            towns.Add("徐泾镇");
            towns.Add("赵巷镇");
            towns.Add("白鹤镇");
            towns.Add("重固镇");
            towns.Add("华新镇");
            towns.Add("香花桥街道");

            getAllshp(shpPathText.Text);
            
            //int n = 1;
            //Invoke(new MethodInvoker(() =>
            //{
            //    proWork.Maximum = towns.Count;
            //    proWork.Value = 0;
            //}));
            //foreach (string town in towns)
            //{
            //    createEach(allValue, town);
            //    createEachFuhe(allValue, town);
            //    //Invoke(new MethodInvoker(() =>
            //    //{
            //    //    proWork.Value = n++;
            //    //}));
            //}
            string directory=dstPath + @"\报告\"+"青浦区" + phase.Text + "期固体废弃物第三方测评报告-总.docx";
            createNewDoc(directory);

            towns.Sort(delegate(string p1, string p2) { return p1.CompareTo(p2); });

            int number = 2;
            int picIndex = 1;
            foreach (string town in towns)
            {
                createAll(allValue, town, directory, ref number, ref picIndex);
            }

            rest(directory);
            MessageBox.Show("完成！");
        }

        private void getAllshp(string shpName)
        {
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpName);
            shp[1] = System.IO.Path.GetFileName(shpName);

            if (shp[0] == null) MessageBox.Show("文件路径问题！");
            IFeatureClass pFC = mg.openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();

            Hashtable i = mg.Index(pFC);

            while (pFeature != null)
            {
                ResultItem value = new ResultItem();
                value = mg.getFields(i, pFeature);
                allValue.Add(value);
                pFeature = pFeatureCur.NextFeature();
            }
            allValue.Sort(delegate(ResultItem p1, ResultItem p2) { return p1.Figure.CompareTo(p2.Figure); });
        }

        private void createEach(List<ResultItem> allValue, string townName)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\青浦区垃圾巡查\0301.docx";
            report.CreateNewDucument(templateFile);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (s.RegionID.Contains(townName) && s.Figure.Contains(flag) && (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾")))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insituText.Text, dstPath + @"\截图"))
                        {
                            number++;
                        }
                        MessageFilter.Revoke();
                        break;
                    }
                    catch (SystemException err)
                    {
                        m_iErrCnt++;
                        if (m_iErrCnt < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //if (number > 20) break;
            }
            report.deletePage(1);
            string[] contents = new string[2];
            contents[0] = "青浦区" + townName + phase.Text + "期";
            contents[1] = "固体废弃物第三方测评报告";
            report.insertFirstPage(contents);

            string townDirectory = dstPath + @"\报告\" + townName + "-" + phase.Text + "期";
            if (!Directory.Exists(townDirectory)) Directory.CreateDirectory(townDirectory);
            report.SaveDocument(townDirectory + @"\青浦区" + townName + phase.Text + "期" + "固体废弃物第三方测评报告" + ".docx");
        }

        private void createEachFuhe(List<ResultItem> allValue, string townName)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\青浦区垃圾巡查\0301.docx";
            report.CreateNewDucument(templateFile);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (s.RegionID.Contains(townName) && !s.Figure.Contains(flag) && (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾")))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insituText.Text, dstPath + @"\截图"))
                        {
                            number++;
                        }
                        MessageFilter.Revoke();
                        break;
                    }
                    catch (SystemException err)
                    {
                        m_iErrCnt++;
                        if (m_iErrCnt < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //if (number > 20) break;
            }
            report.deletePage(1);
            string[] contents = new string[2];
            contents[0] = "青浦区" + townName + phase.Text + "期";
            contents[1] = "固体废弃物复核点位报告";
            report.insertFirstPage(contents);

            string townDirectory = dstPath + @"\报告\" + townName + "-" + phase.Text + "期";
            if (!Directory.Exists(townDirectory)) Directory.CreateDirectory(townDirectory);
            report.SaveDocument(townDirectory + @"\青浦区" + townName + phase.Text + "期" + "固体废弃物复核点位报告" + ".docx");
        }

        private void createAll(List<ResultItem> allValue)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\青浦区垃圾巡查\0301.docx";
            report.CreateNewDucument(templateFile);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (s.Figure.Contains(flag) && (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾")))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insituText.Text, dstPath + @"\截图"))
                        {
                            number++;
                        }
                        MessageFilter.Revoke();
                        break;
                    }
                    catch (SystemException err)
                    {
                        m_iErrCnt++;
                        if (m_iErrCnt < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //if (number > 20) break;
            }
            report.deletePage(1);
            string[] contents = new string[2];
            contents[0] = "青浦区" + phase.Text + "期";
            contents[1] = "固体废弃物第三方测评报告";
            report.insertFirstPage(contents);

            string townDirectory = dstPath + @"\报告\";
            if (!Directory.Exists(townDirectory)) Directory.CreateDirectory(townDirectory);
            report.SaveDocument(townDirectory + @"青浦区" + phase.Text + "期" + "固体废弃物第三方测评报告" + ".docx");
        }

        private void createAllFuhe(List<ResultItem> allValue)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\青浦区垃圾巡查\0301.docx";
            report.CreateNewDucument(templateFile);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (!s.Figure.Contains(flag) && (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾")))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insituText.Text, dstPath + @"\截图"))
                        {
                            number++;
                        }
                        MessageFilter.Revoke();
                        break;
                    }
                    catch (SystemException err)
                    {
                        m_iErrCnt++;
                        if (m_iErrCnt < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //if (number > 20) break;
            }
            report.deletePage(1);
            string[] contents = new string[2];
            contents[0] = "青浦区" + phase.Text + "期";
            contents[1] = "固体废弃物第三方测评报告";
            report.insertFirstPage(contents);

            string townDirectory = dstPath + @"\报告\";
            if (!Directory.Exists(townDirectory)) Directory.CreateDirectory(townDirectory);
            report.SaveDocument(townDirectory + @"青浦区" + phase.Text + "期" + "固体废弃物复核点位报告" + ".docx");
        }

        private void createAll(List<ResultItem> allValue, string town, string directory, ref int number, ref int picIndex)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            report.CreateNewDucument(directory);

            List<ResultItem> XinZeng = new List<ResultItem>();
            List<ResultItem> FuHe = new List<ResultItem>();
            foreach (ResultItem s in allValue)
            {
                if (s.RegionID == town && s.Figure.Contains(flag)) XinZeng.Add(s);
                else if (s.RegionID == town && !s.Figure.Contains(flag)) FuHe.Add(s);
            }

            #region 添加新增点位
            string[] contents = new string[2];
            contents[0] = town + "新增";
            contents[1] = phase.Text + "期固体废弃物新增点位信息";
            report.insertPage(contents);
            foreach (ResultItem s in XinZeng)
            {
                int m_iErrCnt = 0;
                while (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾"))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insituText.Text, dstPath + @"\截图"))
                        {
                            number++;
                        }
                        MessageFilter.Revoke();
                        break;
                    }
                    catch (SystemException err)
                    {
                        m_iErrCnt++;
                        if (m_iErrCnt < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            #endregion

            #region 添加复核点位
            string[] contents2 = new string[2];
            contents2[0] = town + "复核";
            contents2[1] = phase.Text + "期固体废弃物复核点位信息";
            report.insertPage(contents2);
            foreach (ResultItem s in FuHe)
            {
                int m_iErrCnt = 0;
                while (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾"))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insituText.Text, dstPath + @"\截图"))
                        {
                            number++;
                        }
                        MessageFilter.Revoke();
                        break;
                    }
                    catch (SystemException err)
                    {
                        m_iErrCnt++;
                        if (m_iErrCnt < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            #endregion
            
            report.SaveDocument(directory);
        }

        private void rest(string directory)
        {
            //删除第一页，并在每一页下方添加页码
            ReportFromTemplate report = new ReportFromTemplate();
            report.CreateNewDucument(directory);
            report.deletePage(1);
            report.insertPageNumber("center", true);
            report.SaveDocument(directory);
        }

        private void createNewDoc(string docName)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\青浦区垃圾巡查\0301.docx";
            report.CreateNewDucument(templateFile);
            report.SaveDocument(docName);
        }

        private void excelPath_Click(object sender, EventArgs e)
        {
            string strFileName = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开excel文件";
            ofd.Filter = "excel文件|*.xlsx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFileName = ofd.FileName;                
            }
            excelText.Text = strFileName;
        }

        private void insituPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";
            dialog.SelectedPath = @"D:\青浦区垃圾巡查";
            string file = null;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.SelectedPath;
            }
            insituText.Text = file;
        }

        public void setTip()
        {
            //设置鼠标放置上显示的tips
            ToolTip tip_button_tip = new ToolTip();
            tip_button_tip.IsBalloon = true;
            tip_button_tip.AutomaticDelay = 0;            
            tip_button_tip.SetToolTip(this.excelPath, "类别放在B栏，图号放在E栏，均从第二行开始");            
        }

        private void resultShow_TextChanged(object sender, EventArgs e)
        {
            resultShow.SelectionStart = resultShow.Text.Length;
            resultShow.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\青浦区垃圾巡查\0301.docx";
            report.CreateNewDucument(templateFile);
            string[] contents=new string[2];
            contents[0] = "侧是是是是";
            contents[1] = "1123123123123";
            report.insertPage(contents);
            report.deletePage(1);
            report.insertPageNumber("center", true);
            report.SaveDocument(@"D:\" + "aaaaaaaaaaaaaaa.docx");
        }

        
    }
}

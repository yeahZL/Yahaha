using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Trash;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using System.Collections;
using System.Threading;
using System.IO;

namespace Mess
{
    public partial class opFolder : Form
    {
        private string[] ShpPath = null; //记录要截图shpfile文件路径
        private string templateName = null;
        List<ResultItem> allValue = new List<ResultItem>();

        public opFolder()
        {
            InitializeComponent();
        }

        private void QP_Click(object sender, EventArgs e)
        {
            //ReportFromTemplate report = new ReportFromTemplate();
            //string templateFile = templateText.Text;
            //report.CreateNewDucument(templateFile);

            //getAllshp(shpPathText.Text);

            //int number = 2;
            //int picIndex = 1;
            //foreach (ResultItem s in allValue)
            //{
            //    int m_iErrCnt = 0;
            //    while (true)
            //    {
            //        try
            //        {
            //            MessageFilter.Register();
            //            report.CopyTable();
            //            if (report.InsertInfo(ref number, s, ref picIndex, insitu.Text, sShot.Text))
            //            {
            //                number++;
            //            }
            //            MessageFilter.Revoke();
            //            break;
            //        }
            //        catch (SystemException err)
            //        {
            //            m_iErrCnt++;
            //            if (m_iErrCnt < 3)
            //            {
            //                System.Threading.Thread.Sleep(1000);
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }
            //    //if (number > 20) break;
            //}
            //report.deletePage(1);
            //report.SaveDocument(outputPath.Text + "垃圾监测总报告.docx");
            
            Thread pThread = new Thread(new ThreadStart(ThreadTestAll));
            pThread.Start();
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }

        public void setTips()
        {

        }

        private void shpPath_Click(object sender, EventArgs e)
        {
            ShpPath = openShpDialog();
            shpPathText.Text = ShpPath[0] + @"\" + ShpPath[1];
        }

        private string[] openShpDialog()
        {
            string[] str = new string[2];
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开需要截图的矢量文件";
            ofd.Filter = "shapefile文件|*.shp|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                str[0] = System.IO.Path.GetDirectoryName(strFileName);
                str[1] = System.IO.Path.GetFileName(strFileName);
            }
            return str;
        }

        private void template_Click(object sender, EventArgs e)
        {           
            templateText.Text = fileName();
        }

        private string fileName()
        {
            string name = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开模板文件";
            ofd.Filter = "模板文件|*.docx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                name = ofd.FileName;
            }
            return name;
        }

        private string openFolderdialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择图片所在文件夹";
            string file = null;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.SelectedPath;
            }
            return file;
        }

        private void ScreenShot_Click(object sender, EventArgs e)
        {
            string folder = openFolderdialog();
            sShot.Text = folder;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folder = openFolderdialog();
            insitu.Text = folder;
        }

        private void getAllshp(string shpName)
        {
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpName);
            shp[1] = System.IO.Path.GetFileName(shpName);

            if (shp[0] == null) MessageBox.Show("文件路径问题！");
            IFeatureClass pFC = openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();

            Hashtable i = Index(pFC);

            while (pFeature != null)
            {
                ResultItem value = new ResultItem();
                value = getFields(i, pFeature);
                allValue.Add(value);
                pFeature = pFeatureCur.NextFeature();
            }
            allValue.Sort(delegate(ResultItem p1, ResultItem p2) { return p1.Figure.CompareTo(p2.Figure); });
        }

        private IFeatureClass openShapefile(string pFilePath, string pFileName)
        {
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pFilePath, 0);
            IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
            IFeatureClass pFeatureClass;
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName);
            return pFeatureClass;
        }

        //获取各个字段的索引值
        private Hashtable Index(IFeatureClass pFC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("经度", pFC.FindField("Longitude"));
            ht.Add("纬度", pFC.FindField("Latitude"));
            ht.Add("X", pFC.FindField("X"));
            ht.Add("Y", pFC.FindField("Y"));
            ht.Add("镇名", pFC.FindField("镇名"));
            ht.Add("图号", pFC.FindField("图号"));
            ht.Add("垃圾类别", pFC.FindField("垃圾类别"));
            ht.Add("位置", pFC.FindField("位置"));
            ht.Add("面积", pFC.FindField("area"));
            ht.Add("街道", pFC.FindField("街道"));
            ht.Add("村管河道", pFC.FindField("Name_1"));
            ht.Add("镇管河道", pFC.FindField("Name"));
            ht.Add("市区管河道", pFC.FindField("Name_12"));
            ht.Add("第一次时间", pFC.FindField("第一次时间"));
            ht.Add("第二次时间", pFC.FindField("第二次时间"));
            ht.Add("第一次类型", pFC.FindField("第一次类型"));
            ht.Add("第二次类型", pFC.FindField("第二次类型"));
            return ht;
        }

        //根据索引值获取各字段值
        private ResultItem getFields(Hashtable ht, IFeature F)
        {
            ResultItem r = new ResultItem();

            r.Address = ((int)ht["位置"] > -1) ? F.get_Value((int)ht["位置"]).ToString() : null;
            r.Area = ((int)ht["面积"] > -1) ? F.get_Value((int)ht["面积"]).ToString() : null;
            r.Category = ((int)ht["垃圾类别"] > -1) ? F.get_Value((int)ht["垃圾类别"]).ToString() : null;
            r.Figure = ((int)ht["图号"] > -1) ? F.get_Value((int)ht["图号"]).ToString() : null;
            r.Location.X = ((int)ht["经度"] > -1) ? Convert.ToDouble(F.get_Value((int)ht["经度"])) : 0;
            r.Location.Y = ((int)ht["纬度"] > -1) ? Convert.ToDouble(F.get_Value((int)ht["纬度"])) : 0;
            r.Project_XY.X = ((int)ht["X"] > -1) ? Convert.ToDouble(F.get_Value((int)ht["X"])) : 0;
            r.Project_XY.Y = ((int)ht["Y"] > -1) ? Convert.ToDouble(F.get_Value((int)ht["Y"])) : 0;
            r.RegionID = ((int)ht["镇名"] > -1) ? F.get_Value((int)ht["镇名"]).ToString() : null;
            r.RiverID = RiverID(F, (int)ht["村管河道"], (int)ht["镇管河道"], (int)ht["市区管河道"]);
            r.Street = ((int)ht["街道"] > -1) ? F.get_Value((int)ht["街道"]).ToString() : null;

            return r;
        }

        private string RiverID(IFeature pFeature, int i_CunGuan, int i_ZhenGuan, int i_ShiQu)
        {
            if (i_CunGuan < 0 || i_ZhenGuan < 0 || i_ShiQu < 0) return null;
            string River = null;
            try
            {
                if (Convert.ToString(pFeature.get_Value(i_CunGuan)) != " ")
                    River = Convert.ToString(pFeature.get_Value(i_CunGuan));
                else if (Convert.ToString(pFeature.get_Value(i_ZhenGuan)) != " ")
                    River = Convert.ToString(pFeature.get_Value(i_ZhenGuan));
                else if (Convert.ToString(pFeature.get_Value(i_ShiQu)) != " ")
                    River = Convert.ToString(pFeature.get_Value(i_ShiQu));
                return River;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void QPeach_Click(object sender, EventArgs e)
        {
            if (phase.Text == null)
            {
                MessageBox.Show("有未填！");
                return;
            }
            Thread eThread = new Thread(new ThreadStart(ThreadTestEach));
            eThread.Start();
        }

        private void createEachFuhe(List<ResultItem> allValue, string townName)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = templateText.Text; ;
            report.CreateNewDucument(templateFile);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (s.RegionID.Contains(townName) && !s.Figure.Contains("-02") && (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾")))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insitu.Text, sShot.Text))
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
            contents[0] = "青浦区" + townName + "2019" + "02" + "期";
            contents[1] = "固体废弃物复核点位报告";
            report.insertFirstPage(contents);

            string townDirectory = outputPath.Text + @"\" + townName + "-201902期";
            if (!Directory.Exists(townDirectory)) Directory.CreateDirectory(townDirectory);
            report.SaveDocument(townDirectory + @"\青浦区" + townName + "201902期" + "固体废弃物复核点位报告" + ".docx");            
        }

        private void createEach(List<ResultItem> allValue, string townName)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = templateText.Text; ;
            report.CreateNewDucument(templateFile);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (s.RegionID.Contains(townName) && s.Figure.Contains("-02") && (s.Category.Contains("建筑垃圾") || s.Category.Contains("生活垃圾")))
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insitu.Text, sShot.Text))
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
            contents[0] = "青浦区" + townName + "2019" + "02" + "期";
            contents[1] = "固体废弃物第三方测评报告";
            report.insertFirstPage(contents);

            string townDirectory = outputPath.Text + @"\" + townName + "-201902期";
            if (!Directory.Exists(townDirectory)) Directory.CreateDirectory(townDirectory);
            report.SaveDocument(townDirectory + @"\青浦区" + townName +"201902期" + "固体废弃物第三方测评报告" + ".docx");            
        }

        private void NB_Click(object sender, EventArgs e)
        {
            
        }

        private void shpPathText_TextChanged(object sender, EventArgs e)
        {

        }

        private void ThreadTestAll()
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = templateText.Text;
            report.CreateNewDucument(templateFile);
            getAllshp(shpPathText.Text);
            Invoke(new MethodInvoker(() =>
            {
                progWorkQP.Maximum = allValue.Count;
                progWorkQP.Value = 0;
            }));
            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (true)
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, insitu.Text, sShot.Text))
                        {
                            number++;
                            Invoke(new MethodInvoker(() =>
                                {
                                    progWorkQP.Value = number - 2;
                                }));
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
            report.SaveDocument(outputPath.Text + @"\" + "垃圾监测总报告.docx");
            MessageBox.Show("完成！");
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            outputPath.Text = openFolderdialog();
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
            int n = 1;
            Invoke(new MethodInvoker(()=>
                {
                    progWorkQP.Maximum = towns.Count;
                    progWorkQP.Value = 0;
                }));
            foreach (string town in towns)
            {
                createEach(allValue, town);
                createEachFuhe(allValue, town);
                Invoke(new MethodInvoker(() =>
                    {
                        progWorkQP.Value = n++;
                    }));
            }
            MessageBox.Show("完成！");
        }
        
    }
}

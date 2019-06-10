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
    public partial class Other : Form
    {
        private MayGod mg = new MayGod();
        private List<string> labelItems = new List<string>();  //记录图片标注信息
        private string[] ShpPath = null; //记录要截图shpfile文件路径
        private Hashtable i = new Hashtable();
        private Hashtable ht = new Hashtable();
        private string dstPath = @"D:\三个区";
        List<ResultItem> allValue = new List<ResultItem>();



        public Other()
        {
            InitializeComponent();
            labelItems.Add("图号");
            labelItems.Add("区名");
            labelItems.Add("位置");
            labelItems.Add("经纬度");
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

        private void AddLocationFigure()
        {
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpPathText.Text);
            shp[1] = System.IO.Path.GetFileName(shpPathText.Text);

            if (shp[0] == null) return;
            IFeatureClass pFC = openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();

            i = mg.Index(pFC);            

            while (pFeature != null)
            {
                ResultItem value = new ResultItem();
                value = mg.getFields(i, pFeature);

                #region 添加位置
                if (value.Location.X == 0 || value.Location.Y == 0 || value.Project_XY.X == 0 || value.Project_XY.Y == 0 || value.District == " " || (int)i["区名"] == -1)
                {
                    MessageBox.Show(@"在shapefile文件中添加经纬度(Longitude\Latitude)、平面坐标(X\Y)、图号（图号）、垃圾类别（垃圾类别）、位置（位置）和区名（区名) 字段项！");
                    return;
                }
                string pAddress = LocationService.GetLocation(value.Location.Y, value.Location.X); //基于百度API，根据经纬度获取位置信息                
                pFeature.set_Value((int)i["位置"], pAddress);
                pFeature.Store();
                #endregion

                #region 添加图号                
                string content = null;
                content += Judge(value.District);
                int temp;
                if (!ht.ContainsKey(value.District))
                {
                    ht.Add(value.District, 1);
                }
                else
                {
                    temp = (int)ht[value.District];
                    temp += 1;
                    ht[value.District] = temp;
                }
                content += ht[value.District].ToString().PadLeft(3, '0');
                pFeature.set_Value((int)i["图号"], content);
                pFeature.Store();
                #endregion
                
                pFeature = pFeatureCur.NextFeature();
            }
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

        public string Judge(string RegionID)
        {
            if (RegionID == "宝山区")
            {
                return "BSQ";
            }

            else if (RegionID == "嘉定区")
            {
                return "JDQ";
            }

            else if (RegionID == "松江区")
            {
                return "SJQ";
            }            

            else
            {
                return "错误镇名";
            }

        }

        private void screenShot()
        {
            if (!Directory.Exists(dstPath + @"\截图")) Directory.CreateDirectory(dstPath + @"\截图");
            mg.setFields(@"D:\三个区\无标题.mxd", shpPathText.Text, dstPath + @"\截图", labelItems, 15, ref axMapControl1);
            mg.outPut();
            return;
        }

        private void createReport()
        {
            List<string> districts = new List<string>();
            //districts.Add("宝山区");
            //districts.Add("嘉定区");
            districts.Add("松江区");

            getAllshp(shpPathText.Text);

            string directory = dstPath + @"\三个区的报告.docx";
            createNewDoc(directory);

            districts.Sort(delegate(string p1, string p2) { return p1.CompareTo(p2); });

            foreach (string d in districts)
            {
                createAll(allValue, directory, d);
            }
            MessageBox.Show("完成");
        }

        private void createAll(List<ResultItem> all, string directory, string d)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            report.CreateNewDucument(directory);

            int number = 2;
            int picIndex = 1;
            foreach (ResultItem s in allValue)
            {
                int m_iErrCnt = 0;
                while (s.District == d)
                {
                    try
                    {
                        MessageFilter.Register();
                        report.CopyTable();
                        if (report.InsertInfo(ref number, s, ref picIndex, dstPath + @"\截图"))
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
            string output = @"D:\三个区\" + d + ".docx";
            report.SaveDocument(output);
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

        private void createNewDoc(string docName)
        {
            ReportFromTemplate report = new ReportFromTemplate();
            string templateFile = @"D:\三个区\0610.docx";
            report.CreateNewDucument(templateFile);
            report.SaveDocument(docName);
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            //AddLocationFigure();
            //screenShot();
            createReport();
        }

        private void shpPath_Click_1(object sender, EventArgs e)
        {
            ShpPath = openShpDialog();
            shpPathText.Text = ShpPath[0] + @"\" + ShpPath[1];
        }
    }
}

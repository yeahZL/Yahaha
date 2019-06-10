using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using System.Collections;
using Trash;
using System.IO;

namespace Mess
{
    public partial class Before : Form
    {        
        /// <summary>
        /// 这个是前期模块，用于给shapefile添加位置和图号信息
        /// </summary>

        private string[] ShpPath = null; //记录要截图shpfile文件路径
        private MayGod mg = new MayGod();
        private Hashtable i = new Hashtable();
        private Hashtable ht = new Hashtable();
        private string times;

        public Before()
        {
            InitializeComponent();
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

        private void confirm_Click(object sender, EventArgs e)
        {
            Thread pThread = new Thread(new ThreadStart(ThreadTest));
            pThread.Start();
        }

        private void ThreadTest()
        {
            if (!phase.Text.Contains("-"))
            {
                MessageBox.Show("期数格式不正确");
                return;
            }
            times = phase.Text.Substring(phase.Text.IndexOf("-")+1);
            
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpPathText.Text);
            shp[1] = System.IO.Path.GetFileName(shpPathText.Text);

            if (shp[0] == null) return;
            IFeatureClass pFC = openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();

            i = mg.Index(pFC);

            //计算要素总数
            IQueryFilter tQueryFilter = new QueryFilterClass();
            tQueryFilter.WhereClause = "";
            tQueryFilter.AddField(pFC.OIDFieldName);
            Invoke(new MethodInvoker(() =>
            {
                proWork.Maximum = pFC.FeatureCount(tQueryFilter);
                proWork.Value = 0;
            }));

            int number = 1;
            while (pFeature != null)
            {
                ResultItem value = new ResultItem();
                value = mg.getFields(i, pFeature);

                #region 添加位置
                if (value.Location.X == 0 || value.Location.Y == 0 || value.Project_XY.X == 0 || value.Project_XY.Y == 0 || value.RegionID == " " || (int)i["镇名"] == -1)
                {
                    MessageBox.Show(@"在shapefile文件中添加经纬度(Longitude\Latitude)、平面坐标(X\Y)、图号（图号）、垃圾类别（垃圾类别）、位置（位置）和镇名（镇名) 字段项！");
                    return;
                }
                string pAddress = LocationService.GetLocation(value.Location.Y, value.Location.X); //基于百度API，根据经纬度获取位置信息                
                pFeature.set_Value((int)i["位置"], pAddress);
                pFeature.Store();
                #endregion
                
                #region 添加图号
                if (value.Figure != " " || value.Figure != null)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        proWork.Value = number++;
                    }));
                    pFeature = pFeatureCur.NextFeature();
                    continue;
                }
                string content = null;
                content += Judge(value.RegionID);
                int temp;
                if (!ht.ContainsKey(value.RegionID))
                {
                    ht.Add(value.RegionID, 1);
                }
                else
                {
                    temp = (int)ht[value.RegionID];
                    temp += 1;
                    ht[value.RegionID] = temp;
                }
                content += ht[value.RegionID].ToString().PadLeft(3, '0') + "-" + times;
                pFeature.set_Value((int)i["图号"], content);
                pFeature.Store();
                #endregion
                
                Invoke(new MethodInvoker(() =>
                {
                    proWork.Value = number++;
                }));
                pFeature = pFeatureCur.NextFeature();
            }
            if (!Directory.Exists(@"D:\青浦区垃圾巡查\" + phase.Text + "期"))
                Directory.CreateDirectory(@"D:\青浦区垃圾巡查\" + phase.Text + "期");
            MessageBox.Show("完成！");
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
            if (RegionID == "金泽镇")
            {
                return "JZZ";
            }

            else if (RegionID == "练塘镇")
            {
                return "LTZ";
            }

            else if (RegionID == "朱家角镇")
            {
                return "ZJJZ";
            }

            else if (RegionID == "盈浦街道")
            {
                return "YPJD";
            }

            else if (RegionID == "夏阳街道")
            {
                return "XYJD";
            }

            else if (RegionID == "青东农场")
            {
                return "QDNC";
            }

            else if (RegionID == "徐泾镇")
            {
                return "XJZ";
            }

            else if (RegionID == "赵巷镇")
            {
                return "ZXZ";
            }

            else if (RegionID == "白鹤镇")
            {
                return "BHZ";
            }

            else if (RegionID == "重固镇")
            {
                return "ZGZ";
            }

            else if (RegionID == "华新镇")
            {
                return "HXZ";
            }

            else if (RegionID == "香花桥街道")
            {
                return "XHQJD";
            }

            else if (RegionID == "慈城镇")
            {
                return "CCZ";
            }

            else if (RegionID == "洪塘街道")
            {
                return "HTJD";
            }

            else if (RegionID == "孔浦街道")
            {
                return "KPJD";
            }

            else if (RegionID == "甬江街道")
            {
                return "YJJD";
            }

            else if (RegionID == "庄桥街道")
            {
                return "ZQJD";
            }

            else if (RegionID == "白沙街道")
            {
                return "BSJD";
            }

            else if (RegionID == "文教街道")
            {
                return "WJJD";
            }

            else if (RegionID == "中马街道")
            {
                return "ZMJD";
            }

            else
            {
                return "错误镇名";
            }           

        }
    }
}

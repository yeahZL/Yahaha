﻿using System;
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


namespace Mess
{
    public partial class AddLocation : Form
    {
        private string[] ShpPath = null; //记录要截图shpfile文件路径
        private MayGod mg = new MayGod();
        Hashtable i = new Hashtable();

        public AddLocation()
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

        public void setTips()
        {
            ToolTip tip_button_tip = new ToolTip();
            tip_button_tip.IsBalloon = true;
            tip_button_tip.AutomaticDelay = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            Thread pThread = new Thread(new ThreadStart(ThreadTest));
            pThread.Start();
            
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

        private void AddLocation_Load(object sender, EventArgs e)
        {

        }

        private void ThreadTest()        
        {
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpPathText.Text);
            shp[1] = System.IO.Path.GetFileName(shpPathText.Text);

            if (shp[0] == null) return;
            IFeatureClass pFC = openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();

            i = mg.Index(pFC);

            ////计算要素总数
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

                if (value.Location.X == 0 || value.Location.Y == 0 || (int)i["位置"] == -1)
                {
                    MessageBox.Show("在shapefile文件中添加经纬度字段“Longitude”、“Latitude”和 “位置”字段！");
                    return;
                }
                string pAddress = LocationService.GetLocation(value.Location.Y, value.Location.X); //基于百度API，根据经纬度获取位置信息                
                pFeature.set_Value((int)i["位置"], pAddress);
                pFeature.Store();
                Invoke(new MethodInvoker(() =>
                {
                    proWork.Value = number++;
                }));
                pFeature = pFeatureCur.NextFeature();
            }

            MessageBox.Show("添加完成！");
        }

    }
}

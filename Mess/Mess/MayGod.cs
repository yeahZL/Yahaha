using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using System.Collections;
using Trash;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using stdole;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;

namespace Mess
{
    public class MayGod
    {
        //private ESRI.ArcGIS.Controls.AxMapControl axMapControl1 = new AxMapControl();
        public int number = 1;
        public int all = 100;
        public string fileName = "test";

        private string mxdPath;
        private string shpName;
        private string destPath;
        private List<string> labelItems;
        private int fontsize;
        private AxMapControl axMapControl1;
        private object locker;
        private int width = 400;
        private int height = 400;

           
        public IFeatureClass openShapefile(string pFilePath, string pFileName)
        {
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pFilePath, 0);
            IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
            IFeatureClass pFeatureClass;
            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName);
            return pFeatureClass;
        }

        public string[] openShpDialog()
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

        public void outPut(string mxdPath, string shpName, string destPath, List<string> labelItems, int fontsize,
            AxMapControl axMapControl1, object locker,int width = 400, int height = 400)
        {
            /* mxdPath表示mxd文件，包括绝对路径
             * shpName表示要截图的shapefile文件，包括绝对路径
             * destPath表示保存文件的目录
             * labelItems表示图片标注哪些文字
             * fontsize表示标注字体大小
             * axMapControll表示map控件，必须要的
             * width和height表示生成图片的大小
            */

            //打开mxd文件
            axMapControl1.LoadMxFile(mxdPath);

            #region 打开shapefile文件
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpName);
            shp[1] = System.IO.Path.GetFileName(shpName);

            if (shp[0] == null) return;
            IFeatureClass pFC = openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();
            #endregion
            //获取各字段的索引值
            Hashtable i = Index(pFC);

            IEnvelope In_Envelope = new EnvelopeClass();
            In_Envelope.PutCoords(0, 0, width, height);  //若生成缩小图则设100，大图为400

            while (pFeature != null)
            {
                ResultItem value = new ResultItem();
                value = getFields(i, pFeature);

                string SaveFile = null;
                SaveFile = destPath + @"\" + Convert.ToString(value.Figure) + ".jpg";
                number += 1;
                string names = labelName(labelItems, value);

                In_Envelope.CenterAt(value.Project_XY);
                Label(axMapControl1, In_Envelope, value, names, fontsize);
                screenShot(In_Envelope, SaveFile, axMapControl1, 4);  //ratio = 1/Resolution  像素范围(单位：pixels) = 矩形框长宽(In_Envelope单位：米)*ratio
                pFeature = pFeatureCur.NextFeature();

            }
        }

        public void outPut()
        {
            //打开mxd文件
            axMapControl1.LoadMxFile(mxdPath);

            #region 打开shapefile文件
            string[] shp = new string[2];
            shp[0] = System.IO.Path.GetDirectoryName(shpName);
            shp[1] = System.IO.Path.GetFileName(shpName);

            if (shp[0] == null) return;
            IFeatureClass pFC = openShapefile(shp[0], shp[1]);
            IFeatureCursor pFeatureCur = pFC.Search(null, false);
            IFeature pFeature = pFeatureCur.NextFeature();
            #endregion
            //获取各字段的索引值
            Hashtable i = Index(pFC);

            IEnvelope In_Envelope = new EnvelopeClass();
            In_Envelope.PutCoords(0, 0, width, height);  //若生成缩小图则设100，大图为400

            while (pFeature != null)
            {

                ResultItem value = new ResultItem();
                value = getFields(i, pFeature);
                if ((int)i["垃圾类别"] == -1 || (value.Category.Contains("建筑垃圾") || value.Category.Contains("生活垃圾") || value.Category.Contains("疑似垃圾")))
                {
                    string SaveFile = null;
                    SaveFile = destPath + @"\" + Convert.ToString(value.Figure) + ".jpg";

                    string names = labelName(labelItems, value);

                    In_Envelope.CenterAt(value.Project_XY);
                    Label(axMapControl1, In_Envelope, value, names, fontsize);
                    screenShot(In_Envelope, SaveFile, axMapControl1, 4);  //ratio = 1/Resolution  像素范围(单位：pixels) = 矩形框长宽(In_Envelope单位：米)*ratio                    
                }
                pFeature = pFeatureCur.NextFeature();
            }
        }

        //给图片加标签
        private void Label(AxMapControl IN_Axmapcontrols, IEnvelope pEnv, ResultItem r,string name,int fontsize)
        {
            IGraphicsContainer pGraContainer = (IGraphicsContainer)IN_Axmapcontrols.Map;
            pGraContainer.DeleteAllElements();

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(pEnv.XMin, pEnv.YMin + pEnv.Height * 0.15);

            IRgbColor pColor = new RgbColorClass()
            {
                Red = 255,
                Blue = 0,
                Green = 255
            };

            IFontDisp pFont = new StdFont()
            {
                Name = "宋体",
                Bold = true
            } as IFontDisp;

            ITextSymbol pTextSymbol = new TextSymbolClass()
            {
                Color = pColor,
                Font = pFont,
                HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft,
                Size = 15
            };

            ITextElement pTextElement = new TextElementClass()
            {
                Symbol = pTextSymbol,
                ScaleText = true,
                //Size = 15,  //若用缩小图则设置为4，大图为15
                //Text = "河名：" + RiverID + "\n" + "镇名：" + RegionID + "\n" + address +
                //"\n" + "经度：" + Convert.ToString(lng) + "\n" + "纬度：" + Convert.ToString(Lat)
                //Text = "图号：" + r.Figure + "\n" + "镇名：" + r.RegionID + "\n" + "位置：" + address + " " + "\n"
                //+ "经纬度：" + r.Location.X + " " + r.Location.Y + "\n"

                Size = fontsize, //若用缩小图则设置为4，大图为15
                Text = name
            };
            
            IElement pEle = (IElement)pTextElement;
            pEle.Geometry = pPoint;

            pGraContainer.AddElement(pEle, 0);
        }

        public void screenShot(IEnvelope IN_Envelope, string IN_SaveFile, AxMapControl IN_Axmapcontrols, double ratio)
        {
            /*
             * IN_Envelope为输出范围，单位为米
             * IN_SaveFile为输出文件名
             * IN_Axmapcontrols map控件
             * ratio 影像空间分辨率的倒数
            
             */
            if (IN_Envelope == null)
            {
                IN_Envelope = IN_Axmapcontrols.ActiveView.Extent;
            }

            IExport Temp_Exporter = new ExportJPEGClass();  //定义输出设备 Temp_Exporter
            IActiveView Temp_CurrentActiveView = IN_Axmapcontrols.ActiveView;

            tagRECT Temp_TtagRECT; //定义的是输出设备的像素大小
            Temp_TtagRECT.left = 0;
            Temp_TtagRECT.top = 0;
            Temp_TtagRECT.right = (int)(IN_Envelope.Width * ratio);
            Temp_TtagRECT.bottom = (int)(IN_Envelope.Height * ratio);

            int Temp_DPI = 300; //DPI 设置

            IEnvelope Temp_Envelope = new EnvelopeClass(); //定义一个Envelope来确定设备单元中输出的图片大小
            Temp_Envelope.PutCoords(Temp_TtagRECT.left, Temp_TtagRECT.top, Temp_TtagRECT.right, Temp_TtagRECT.bottom);
            //Temp_Envelope.PutCoords(0, 0, 500, 500); 此参数只会显示图片左上角500*500区域

            Temp_Exporter.Resolution = Temp_DPI;
            Temp_Exporter.ExportFileName = IN_SaveFile;
            Temp_Exporter.PixelBounds = Temp_Envelope;

            //IOutputRasterSettings pOutputRasterS = (IOutputRasterSettings)IN_Axmapcontrols.ActiveView.ScreenDisplay.DisplayTransformation;
            IOutputRasterSettings pOutputRasterS = (IOutputRasterSettings)Temp_CurrentActiveView.ScreenDisplay.DisplayTransformation;
            pOutputRasterS.ResampleRatio = 1;   //设置了这个采样比例后，输出图片的像素和原始影像的像素一样！

            Temp_CurrentActiveView.Output(Temp_Exporter.StartExporting(), (int)Temp_DPI, ref Temp_TtagRECT, IN_Envelope, null);

            Temp_Exporter.FinishExporting();
            Temp_Exporter.Cleanup();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(Temp_Exporter);
        }

        //获取各个字段的索引值
        public Hashtable Index(IFeatureClass pFC)
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
            ht.Add("类型", pFC.FindField("类型"));
            ht.Add("区名", pFC.FindField("所属区"));
            return ht;
        }

        //根据索引值获取各字段值
        public ResultItem getFields(Hashtable ht, IFeature F)
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
            r.Category1 = ((int)ht["类型"] > -1) ? F.get_Value((int)ht["类型"]).ToString() : null;
            r.District = ((int)ht["区名"] > -1) ? F.get_Value((int)ht["区名"]).ToString() : null;
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

        private string labelName(List<string> labelItems,ResultItem r)
        {
            string name = null;
            if (labelItems.Contains("图号")) name += "图号：" + r.Figure + "\n";
            if (labelItems.Contains("镇名")) name += "镇名：" + r.RegionID + "\n";
            if (labelItems.Contains("区名")) name += "区名：" + r.District + "\n";
            if (labelItems.Contains("类型")) name += "类型：" + r.Category1 + "\n";
            if (labelItems.Contains("位置")) name += "位置：" + r.Address + " " + "\n";
            if (labelItems.Contains("经纬度")) name += "经纬度：" + r.Location.X + " " + r.Location.Y + "\n";
            return name;
        }

        public void setFields(string omxdPath, string oshpName, string odestPath, List<string> olabelItems, int ofontsize,
            ref AxMapControl oaxMapControl1, int owidth = 400, int oheight = 400)
        {
            mxdPath = omxdPath;
            shpName = oshpName;
            destPath = odestPath;
            labelItems = olabelItems;
            fontsize = ofontsize;
            axMapControl1 = oaxMapControl1;
            
            width = owidth;
            height = oheight;
        }

        public string checkValidShp(string[] shpName)
        {
            if (shpName[0] == null) return "文件名有误！";
            IFeatureClass pFC = openShapefile(shpName[0], shpName[1]);            

            string error = null;           
            error += pFC.FindField("Longitude") == -1 ? "缺少‘Longitude’字段（质心点的经度）\r\n" : null;
            error += pFC.FindField("Latitude") == -1 ? "缺少‘Latitude’字段（质心点的纬度）\r\n" : null;
            error += pFC.FindField("X") == -1 ? "缺少‘X’字段（质心点的X坐标）\r\n" : null;
            error += pFC.FindField("Y") == -1 ? "缺少‘X’字段（质心点的X坐标）\r\n" : null;
            error += pFC.FindField("位置") == -1 ? "缺少‘位置’字段（质心点的位置信息，需要从地图API导入）\r\n" : null;
            error += pFC.FindField("图号") == -1 ? "缺少‘图号’字段（垃圾编号）\r\n" : null;
            error += pFC.FindField("area") == -1 ? "缺少‘area’字段（面积）\r\n" : null;
            //error += pFC.FindField("镇名") == -1 ? "缺少‘镇名’字段 \r\n" : null;
            //error += pFC.FindField("垃圾类别") == -1 ? "缺少‘垃圾类别’字段 \r\n" : null;
            return error;
        }
    }
}

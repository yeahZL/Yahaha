using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Trash
{
    //负责保存从excel中读取的数据
    public class TrashInfo
    {
        public string Figure { get; set; }
        public string Category { get; set; }
        public Investigation invest1 = new Investigation();
        public Investigation invest2 = new Investigation();
    }

    public class RegionInfo
    {
        public string chinese { get; set; }
        public string english { get; set; }

        public RegionInfo(string chinese, string english)
        {
            this.chinese = chinese;
            this.english = english;
        }
        public RegionInfo()
        {
            this.chinese = null;
            this.english = null;
        }
    }

    public class Investigation
    {
        public string date { get; set; }
        public string Category { get; set; }
    }

    public struct riverInfo
    {
        public string identifier;  //水体编码
        public string name;  //水体名称
        public string category;  //水体分类，河道、湖泊等
        public string rank;  //水体等级 市管、区管等
        public string length;  //水体长度
        public string area;  //水体面积
    }

    public struct imageCoordinate
    { 
        //图上坐标
        public int XMin;
        public int XMax;
        public int YMin;
        public int YMax;
        public string imagePath;
    }
}

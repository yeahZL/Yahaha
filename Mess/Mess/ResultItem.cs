using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace Trash
{
    public interface IResultItem
    {
        string RiverID { get; set; }
        string RegionID { get; set; }
        IPoint Location { get; set; }
        IPoint Project_XY { get; set; }
        string Time { get; set; }
        string Path { get; set; }
        int Number { get; set; }
        string Condition { get; set; }
        string Figure { get; set; }
        string Category { get; set; }
        string Address { get; set; }
        string Area { get; set; }
        string Street { get; set; }
        string date1 { get; set; }
        string date2 { get; set; }
        string Category1 { get; set; }
        string Category2 { get; set; }
    }

    public class ResultItem : IResultItem
    {
        public ResultItem()
        {
            this.Location = new PointClass();
            this.Project_XY = new PointClass();
            //this.Number = 0;
        }

        public string RiverID
        {
            get;
            set;
        }

        public string RegionID
        {
            get;
            set;
        }

        public IPoint Location
        {
            get;
            set;
        }

        public IPoint Project_XY
        {
            get;
            set;
        }

        public string Time
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public int Number
        {
            get;
            set;
        }

        public string Condition
        {
            get;
            set;
        }

        public string Figure
        { 
            get; 
            set;
        }

        public string Category 
        { 
            get;
            set; 
        }

        public string Address
        {
            get;
            set;
        }

        public string Area 
        { 
            get;
            set; 
        }

        public string Street 
        { 
            get; 
            set; 
        }

        public string date1
        {
            get;
            set;
        }

        public string date2
        {
            get;
            set;
        }

        public string Category1
        {
            get;
            set;
        }

        public string Category2
        {
            get;
            set;
        }

        public string District
        {
            get;
            set;
        }

        
    }    
}

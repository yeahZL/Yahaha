using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Trash
{
    class LocationService
    {

        private static string url = @"http://api.map.baidu.com/geocoder/v2/?location={0}&coordtype=wgs84ll&output=json&ak=WEc8RlPXzSifaq9RHxE1WW7lRKgbid6Y";
        //private static string url = @"http://api.map.baidu.com/geocoder/v2/?location={0}&coordtype=wgs84ll&output=json&ak=ak=E4805d16520de693a3fe707cdc962045";
        //private static string url = @"http://restapi.amap.com/v3/geocode/regeo?key=0f42352f8db76594648da40eb2b7c63f&location={0}";

        public static string GetLocation(double lat, double lon)
        {
            try
            {
                string pLat = Convert.ToString(lat);
                string pLng = Convert.ToString(lon);
                int a=0;
                HttpClient client = new HttpClient();
                string location = string.Format("{0},{1}", pLat, pLng);
                string bdUrl = string.Format(url, location);
                string result = client.GetStringAsync(bdUrl).Result;
                var locationResult = (JObject)JsonConvert.DeserializeObject(result);
                
                if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"] == null)
                    return string.Empty;

                //var address = "位置信息：" + Convert.ToString(locationResult["result"]["formatted_address"]);
                //if (locationResult["result"]["sematic_description"] != null)
                //    address += Convert.ToString(locationResult["result"]["sematic_description"]);

                string address = null;
                if (locationResult["result"]["sematic_description"] != null)
                    address += Convert.ToString(locationResult["result"]["sematic_description"]);

                if (locationResult["result"]["addressComponent"]["distance"] != null)
                {
                    int presult = 0;
                    int.TryParse(locationResult["result"]["addressComponent"]["distance"].ToString(), out presult);
                    a =presult;            
                } 
                return address;
            }
            catch (System.Exception ex)
            {
                return "未获取到位置信息";
            }
        }

        public static int getDistance(double lat, double lon)
        {
            try
            {
                string pLat = Convert.ToString(lat);
                string pLng = Convert.ToString(lon);
                int a = 0;
                HttpClient client = new HttpClient();
                string location = string.Format("{0},{1}", pLat, pLng);
                string bdUrl = string.Format(url, location);
                string result = client.GetStringAsync(bdUrl).Result;
                var locationResult = (JObject)JsonConvert.DeserializeObject(result);

                if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"] == null)
                    return 0;

                if (locationResult["result"]["addressComponent"]["distance"] != null)
                {
                    int presult = 0;
                    int.TryParse(locationResult["result"]["addressComponent"]["distance"].ToString(), out presult);
                    a = presult;
                }
                return a;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        public static string getStreet(double lat, double lon)
        {
            try
            {
                string pLat = Convert.ToString(lat);
                string pLng = Convert.ToString(lon);
                string street = null;
                HttpClient client = new HttpClient();
                string location = string.Format("{0},{1}", pLat, pLng);
                string bdUrl = string.Format(url, location);
                string result = client.GetStringAsync(bdUrl).Result;
                var locationResult = (JObject)JsonConvert.DeserializeObject(result);

                if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"] == null)
                    return string.Empty;
                if (locationResult["result"]["addressComponent"]["street"] != null)
                {                    
                    street = Convert.ToString(locationResult["result"]["addressComponent"]["street"]);
                }
                return street;
            }
            catch (System.Exception ex)
            {
                return "未获取到位置信息";
            }
        }

       



    }

    public class Baidu
    {
        public string status { get; set; }

        //public string result { get; set; }
    }
}


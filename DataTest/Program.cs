/*
  UdgerParser - DataTest - Local parser lib 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
 
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
*/

using Newtonsoft.Json;
using Udger.Parser;
using System.Net;
using System.IO;
using System;
using System.Web;

namespace DataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Udger.Parser.UserAgent a;
            Udger.Parser.IPAddress i;
            WebClient client;
            string jsonString;
            dynamic jsonResult;
            string tempPath = System.IO.Path.GetTempPath();

            #region Download test data
            Console.WriteLine("download test data from github");
            client = new WebClient();
            client.DownloadFile("https://github.com/udger/test-data/blob/master/data_v3/udgerdb_v3.dat?raw=true", tempPath + "udgerdb_v3.dat");
            client.DownloadFile("https://raw.githubusercontent.com/udger/test-data/master/data_v3/test_ua.json", tempPath + "test_ua.json");
            client.DownloadFile("https://raw.githubusercontent.com/udger/test-data/master/data_v3/test_ip.json", tempPath + "test_ip.json");

            Console.WriteLine("download test data end");
            #endregion

            #region New parser
            UdgerParser parser = new UdgerParser();
            parser.SetDataDir(tempPath);
            #endregion

            #region test IP
            jsonString = File.ReadAllText(tempPath + "test_ip.json");
            jsonResult = JsonConvert.DeserializeObject(jsonString);

            foreach (dynamic x in jsonResult)
            {
                Console.WriteLine("test IP: " + x.test.teststring);
                parser.ip = Convert.ToString(x.test.teststring);
                parser.parse();
                i = parser.ipAddress;

                if (setNullEmpty(i.CrawlerCategory) != Convert.ToString(x.ret.crawler_category))
                    Console.WriteLine("err CrawlerCategory: " + i.CrawlerCategory);

                if (setNullEmpty(i.CrawlerCategoryCode) != Convert.ToString(x.ret.crawler_category_code))
                    Console.WriteLine("err CrawlerCategoryCode: " + i.CrawlerCategoryCode);

                if (setNullEmpty(i.CrawlerFamily) != Convert.ToString(x.ret.crawler_family))
                    Console.WriteLine("err CrawlerFamily: " + i.CrawlerFamily);

                if (setNullEmpty(i.CrawlerFamilyCode) != Convert.ToString(x.ret.crawler_family_code))
                    Console.WriteLine("err CrawlerFamilyCode: " + i.CrawlerFamilyCode);

                if (HttpUtility.UrlDecode(setNullEmpty(i.CrawlerFamilyHomepage)) != Convert.ToString(x.ret.crawler_family_homepage))
                    Console.WriteLine("err CrawlerFamilyHomepage: " + i.CrawlerFamilyHomepage);

                if (setNullEmpty(i.CrawlerFamilyIcon) != Convert.ToString(x.ret.crawler_family_icon))
                    Console.WriteLine("err CrawlerFamilyIcon: " + i.CrawlerFamilyIcon);

                if (HttpUtility.UrlDecode(setNullEmpty(i.CrawlerFamilyInfoUrl)) != Convert.ToString(x.ret.crawler_family_info_url))
                    Console.WriteLine("err CrawlerFamilyInfoUrl: " + i.CrawlerFamilyInfoUrl);

                if (setNullEmpty(i.CrawlerFamilyVendor) != Convert.ToString(x.ret.crawler_family_vendor))
                    Console.WriteLine("err CrawlerFamilyVendor: " + i.CrawlerFamilyVendor);

                if (setNullEmpty(i.CrawlerFamilyVendorCode) != Convert.ToString(x.ret.crawler_family_vendor_code))
                    Console.WriteLine("err CrawlerFamilyVendorCode: " + i.CrawlerFamilyVendorCode);

                if (HttpUtility.UrlDecode(setNullEmpty(i.CrawlerFamilyVendorHomepage)) != Convert.ToString(x.ret.crawler_family_vendor_homepage))
                    Console.WriteLine("err CrawlerFamilyVendorHomepage: " + i.CrawlerFamilyVendorHomepage);
                /*
                if (setNullEmpty(i.CrawlerLastSeen) != Convert.ToString(x.ret.crawler_category))
                    Console.WriteLine("err CrawlerLastSeen: " + i.CrawlerLastSeen);
                */
                if (setNullEmpty(i.CrawlerName) != Convert.ToString(x.ret.crawler_name))
                    Console.WriteLine("err CrawlerName: " + i.CrawlerName);

                if (setNullEmpty(i.CrawlerRespectRobotstxt) != Convert.ToString(x.ret.crawler_respect_robotstxt))
                    Console.WriteLine("err CrawlerRespectRobotstxt: " + i.CrawlerRespectRobotstxt);

                if (setNullEmpty(i.CrawlerVer) != Convert.ToString(x.ret.crawler_ver))
                    Console.WriteLine("err CrawlerVer: " + i.CrawlerVer);

                if (setNullEmpty(i.CrawlerVerMajor) != Convert.ToString(x.ret.crawler_ver_major))
                    Console.WriteLine("err CrawlerVerMajor: " + i.CrawlerVerMajor);

                if (HttpUtility.UrlDecode(setNullEmpty(i.DatacenterHomepage)) != Convert.ToString(x.ret.datacenter_homepage))
                    Console.WriteLine("err DatacenterHomepage: " + i.DatacenterHomepage);

                if (setNullEmpty(i.DatacenterName) != Convert.ToString(x.ret.datacenter_name))
                    Console.WriteLine("err DatacenterName: " + i.DatacenterName);

                if (setNullEmpty(i.DatacenterNameCode) != Convert.ToString(x.ret.datacenter_name_code))
                    Console.WriteLine("err DatacenterNameCode: " + i.DatacenterNameCode);

                if (setNullEmpty(i.Ip) != Convert.ToString(x.ret.ip))
                    Console.WriteLine("err Ip: " + i.Ip);

                if (setNullEmpty(i.IpCity) != Convert.ToString(x.ret.ip_city))
                    Console.WriteLine("err IpCity: " + i.IpCity);

                if (setNullEmpty(i.IpClassification) != Convert.ToString(x.ret.ip_classification))
                    Console.WriteLine("err IpClassification: " + i.IpClassification);

                if (setNullEmpty(i.IpClassificationCode) != Convert.ToString(x.ret.ip_classification_code))
                    Console.WriteLine("err IpClassificationCode: " + i.IpClassificationCode);

                if (setNullEmpty(i.IpCountry) != Convert.ToString(x.ret.ip_country))
                    Console.WriteLine("err IpCountry: " + i.IpCountry);

                if (setNullEmpty(i.IpCountryCode) != Convert.ToString(x.ret.ip_country_code))
                    Console.WriteLine("err IpCountryCode: " + i.IpCountryCode);

                if (setNullEmpty(i.IpHostname) != Convert.ToString(x.ret.ip_hostname))
                    Console.WriteLine("err IpHostname: " + i.IpHostname);
                /*
                if (setNullEmpty(i.IpLastSeen) != Convert.ToString(x.ret.crawler_category))
                    Console.WriteLine("err IpLastSeen: " + i.IpLastSeen);
                */
                if (setNullEmpty(i.IpVer) != Convert.ToString(x.ret.ip_ver))
                    Console.WriteLine("err IpVer: " + i.IpVer);

            }
            #endregion

            #region test UA
            jsonString = File.ReadAllText(tempPath + "test_ua.json");
            jsonResult = JsonConvert.DeserializeObject(jsonString);

            foreach (dynamic x in jsonResult)
            {
                Console.WriteLine("test UA: " + x.test.teststring);
                parser.ua = Convert.ToString(x.test.teststring);
                parser.parse();
                a = parser.userAgent;

                if (setNullEmpty(a.CrawlerCategory) != Convert.ToString(x.ret.crawler_category))
                    Console.WriteLine("err CrawlerCategory: " + a.CrawlerCategory);

                if (setNullEmpty(a.CrawlerCategoryCode) != Convert.ToString(x.ret.crawler_category_code))
                    Console.WriteLine("err CrawlerCategoryCode: " + a.CrawlerCategoryCode);
                /*
                if (setNullEmpty(a.CrawlerLastSeen) != "")
                    Console.WriteLine("err CrawlerLastSeen: " + a.CrawlerLastSeen);
                */
                if (setNullEmpty(a.CrawlerRespectRobotstxt) != Convert.ToString(x.ret.crawler_respect_robotstxt))
                    Console.WriteLine("err CrawlerRespectRobotstxt: " + a.CrawlerRespectRobotstxt);

                if (setNullEmpty(a.DeviceBrand) != Convert.ToString(x.ret.device_brand))
                    Console.WriteLine("err DeviceBrand: " + a.DeviceBrand);

                if (setNullEmpty(a.DeviceBrandCode) != Convert.ToString(x.ret.device_brand_code))
                    Console.WriteLine("err DeviceBrandCode: " + a.DeviceBrandCode);

                if (setNullEmpty(a.DeviceBrandHomepage) != Convert.ToString(x.ret.device_brand_homepage))
                    Console.WriteLine("err DeviceBrandHomepage: " + a.DeviceBrandHomepage);

                if (setNullEmpty(a.DeviceBrandIcon) != Convert.ToString(x.ret.device_brand_icon))
                    Console.WriteLine("err DeviceBrandIcon: " + a.DeviceBrandIcon);

                if (setNullEmpty(a.DeviceBrandIconBig) != Convert.ToString(x.ret.device_brand_icon_big))
                    Console.WriteLine("err DeviceBrandIconBig: " + a.DeviceBrandIconBig);

                if (HttpUtility.UrlDecode(setNullEmpty(a.DeviceBrandInfoUrl)) != Convert.ToString(x.ret.device_brand_info_url))
                    Console.WriteLine("err DeviceBrandInfoUrl: " + a.DeviceBrandInfoUrl);

                if (setNullEmpty(a.DeviceClass) != Convert.ToString(x.ret.device_class))
                    Console.WriteLine("err DeviceClass: " + a.DeviceClass);

                if (setNullEmpty(a.DeviceClassCode) != Convert.ToString(x.ret.device_class_code))
                    Console.WriteLine("err DeviceClassCode: " + a.DeviceClassCode);

                if (setNullEmpty(a.DeviceClassIcon) != Convert.ToString(x.ret.device_class_icon))
                    Console.WriteLine("err DeviceClassIcon: " + a.DeviceClassIcon);

                if (setNullEmpty(a.DeviceClassIconBig) != Convert.ToString(x.ret.device_class_icon_big))
                    Console.WriteLine("err DeviceClassIconBig: " + a.DeviceClassIconBig);

                if (HttpUtility.UrlDecode(setNullEmpty(a.DeviceClassInfoUrl)) != Convert.ToString(x.ret.device_class_info_url))
                    Console.WriteLine("err DeviceClassInfoUrl: " + a.DeviceClassInfoUrl);

                if (setNullEmpty(a.DeviceMarketname) != Convert.ToString(x.ret.device_marketname))
                    Console.WriteLine("err DeviceMarketname: " + a.DeviceMarketname);

                if (setNullEmpty(a.Os) != Convert.ToString(x.ret.os))
                    Console.WriteLine("err Os: " + a.Os);

                if (setNullEmpty(a.OsCode) != Convert.ToString(x.ret.os_code))
                    Console.WriteLine("err OsCode: " + a.OsCode);

                if (setNullEmpty(a.OsFamily) != Convert.ToString(x.ret.os_family))
                    Console.WriteLine("err OsFamily: " + a.OsFamily);

                if (setNullEmpty(a.OsFamilyCode) != Convert.ToString(x.ret.os_family_code))
                    Console.WriteLine("err OsFamilyCode: " + a.OsFamilyCode);

                if (setNullEmpty(a.OsFamilyVendor) != Convert.ToString(x.ret.os_family_vendor))
                    Console.WriteLine("err OsFamilyVendor: " + a.OsFamilyVendor);

                if (setNullEmpty(a.OsFamilyVendorCode) != Convert.ToString(x.ret.os_family_vendor_code))
                    Console.WriteLine("err OsFamilyVendorCode: " + a.OsFamilyVendorCode);

                if (setNullEmpty(a.OsFamilyVendorHomepage) != Convert.ToString(x.ret.os_family_vendor_homepage))
                    Console.WriteLine("err OsFamilyVendorHomepage: " + a.OsFamilyVendorHomepage);

                if (setNullEmpty(a.OsHomepage) != Convert.ToString(x.ret.os_homepage))
                    Console.WriteLine("err OsHomepage: " + a.OsHomepage);

                if (setNullEmpty(a.OsIcon) != Convert.ToString(x.ret.os_icon))
                    Console.WriteLine("err OsIcon: " + a.OsIcon);

                if (setNullEmpty(a.OsIconBig) != Convert.ToString(x.ret.os_icon_big))
                    Console.WriteLine("err OsIconBig: " + a.OsIconBig);

                if (HttpUtility.UrlDecode(setNullEmpty(a.OsInfoUrl)) != Convert.ToString(x.ret.os_info_url))
                    Console.WriteLine("err OsInfoUrl: " + a.OsInfoUrl);

                if (setNullEmpty(a.Ua) != Convert.ToString(x.ret.ua))
                    Console.WriteLine("err Ua: " + a.Ua);

                if (setNullEmpty(a.UaClass) != Convert.ToString(x.ret.ua_class))
                    Console.WriteLine("err UaClass: " + a.UaClass);

                if (setNullEmpty(a.UaClassCode) != Convert.ToString(x.ret.ua_class_code))
                    Console.WriteLine("err UaClassCode: " + a.UaClassCode);

                if (setNullEmpty(a.UaEngine) != Convert.ToString(x.ret.ua_engine))
                    Console.WriteLine("err UaEngine: " + a.UaEngine);

                if (setNullEmpty(a.UaFamily) != Convert.ToString(x.ret.ua_family))
                    Console.WriteLine("err UaFamily: " + a.UaFamily);

                if (setNullEmpty(a.UaFamilyCode) != Convert.ToString(x.ret.ua_family_code))
                    Console.WriteLine("err UaFamilyCode: " + a.UaFamilyCode);

                if (setNullEmpty(a.UaFamilyHompage) != Convert.ToString(x.ret.ua_family_homepage))
                    Console.WriteLine("err UaFamilyHompage: " + a.UaFamilyHompage);

                if (setNullEmpty(a.UaFamilyIcon) != Convert.ToString(x.ret.ua_family_icon))
                    Console.WriteLine("err UaFamilyIcon: " + a.UaFamilyIcon);

                if (setNullEmpty(a.UaFamilyIconBig) != Convert.ToString(x.ret.ua_family_icon_big))
                    Console.WriteLine("err UaFamilyIconBig: " + a.UaFamilyIconBig);

                if (HttpUtility.UrlDecode(setNullEmpty(a.UaFamilyInfoUrl)) != Convert.ToString(x.ret.ua_family_info_url))
                    Console.WriteLine("err UaFamilyInfoUrl: " + a.UaFamilyInfoUrl);

                if (setNullEmpty(a.UaFamilyVendor) != Convert.ToString(x.ret.ua_family_vendor))
                    Console.WriteLine("err UaFamilyVendor: " + a.UaFamilyVendor);

                if (setNullEmpty(a.UaFamilyVendorCode) != Convert.ToString(x.ret.ua_family_vendor_code))
                    Console.WriteLine("err UaFamilyVendorCode: " + a.UaFamilyVendorCode);

                if (setNullEmpty(a.UaFamilyVendorHomepage) != Convert.ToString(x.ret.ua_family_vendor_homepage))
                    Console.WriteLine("err UaFamilyVendorHomepage: " + a.UaFamilyVendorHomepage);

                if (setNullEmpty(a.UaString) != Convert.ToString(x.test.teststring))
                    Console.WriteLine("err UaString: " + a.UaString);
                /*
                if (setNullEmpty(a.UaUptodateCurrentVersion) != Convert.ToString(x.ret.crawler_category))
                    Console.WriteLine("err UaUptodateCurrentVersion: " + a.UaUptodateCurrentVersion);
                */
                if (setNullEmpty(a.UaVersion) != Convert.ToString(x.ret.ua_version))
                    Console.WriteLine("err UaVersion: " + a.UaVersion);

                if (setNullEmpty(a.UaVersionMajor) != Convert.ToString(x.ret.ua_version_major))
                    Console.WriteLine("err UaVersionMajor: " + a.UaVersionMajor);              

            }
            #endregion                      
                        
        }

        #region utils
        static String setNullEmpty(String x)
        {
            if (x == null)
                x = "";

            return x;
        }
        #endregion
    }
}

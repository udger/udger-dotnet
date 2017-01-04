/*
  UdgerParser - Local parser lib
  
  UdgerParser class parses useragent strings based on a database downloaded from udger.com
 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
  license    GNU Lesser General Public License
  link       https://udger.com/products/local_parser
 */

namespace Udger.Parser
{
    public class IPAddress
    {

            #region Properties
            public string Ip { get;  set; }
            public string IpVer { get;  set; }
            public string IpClassification { get;  set; }
            public string IpClassificationCode { get;  set; }
            public string IpHostname { get;  set; }
            public string IpLastSeen { get;  set; }
            public string IpCountry { get;  set; }
            public string IpCountryCode { get;  set; }
            public string IpCity { get;  set; }
            public string CrawlerName { get;  set; }
            public string CrawlerVer { get;  set; }
            public string CrawlerVerMajor { get;  set; }
            public string CrawlerFamily { get;  set; }
            public string CrawlerFamilyCode { get;  set; }
            public string CrawlerFamilyHomepage { get;  set; }
            public string CrawlerFamilyVendor { get;  set; }
            public string CrawlerFamilyVendorCode { get;  set; }
            public string CrawlerFamilyVendorHomepage { get;  set; }
            public string CrawlerFamilyIcon { get;  set; }
            public string CrawlerFamilyInfoUrl { get;  set; }
            public string CrawlerLastSeen { get;  set; }
            public string CrawlerCategory { get;  set; }
            public string CrawlerCategoryCode { get;  set; }
            public string CrawlerRespectRobotstxt { get;  set; }
            public string DatacenterName { get;  set; }
            public string DatacenterNameCode { get;  set; }
            public string DatacenterHomepage { get;  set; }
            #endregion
        
    }
}

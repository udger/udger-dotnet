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
    public class UserAgent
    {
        #region Properties
        [NamePosition(0, Name = "UaString")]
        public string UaString { get;   set; }

        [NamePosition(1, Name = "UaClass")]
        public string UaClass { get;   set; }

        [NamePosition(2, Name = "UaClassCode")]
        public string UaClassCode { get;   set; }

        [NamePosition(3, Name = "Ua")]
        public string Ua { get;   set; }

        [NamePosition(4, Name = "UaVersion")]
        public string UaVersion { get;   set; }

        [NamePosition(5, Name = "UaVersionMajor")]
        public string UaVersionMajor { get;   set; }

        [NamePosition(6, Name = "UaUptodateCurrentVersion")]
        public string UaUptodateCurrentVersion { get;   set; }

        [NamePosition(7, Name = "UaFamily")]
        public string UaFamily { get;   set; }

        [NamePosition(8, Name = "UaFamilyCode")]
        public string UaFamilyCode { get;   set; }

        [NamePosition(9, Name = "UaFamilyHompage")]
        public string UaFamilyHompage { get;   set; }

        [NamePosition(10, Name = "UaFamilyVendor")]
        public string UaFamilyVendor { get;   set; }

        [NamePosition(11, Name = "UaFamilyVendorCode")]
        public string UaFamilyVendorCode { get;   set; }

        [NamePosition(12, Name = "UaFamilyVendorHomepage")]
        public string UaFamilyVendorHomepage { get;   set; }

        [NamePosition(13, Name = "UaFamilyIcon")]
        public string UaFamilyIcon { get;   set; }

        [NamePosition(14, Name = "UaFamilyIconBig")]
        public string UaFamilyIconBig { get;   set; }

        [NamePosition(15, Name = "UaFamilyIconUrl")]
        public string UaFamilyInfoUrl { get;   set; }

        [NamePosition(16, Name = "UaEngine")]
        public string UaEngine { get;   set; }

        [NamePosition(17, Name = "Os")]
        public string Os { get;   set; }

        [NamePosition(18, Name = "OsCode")]
        public string OsCode { get;   set; }

        [NamePosition(19, Name = "OsHomepage")]
        public string OsHomepage { get;   set; }

        [NamePosition(20, Name = "OsIcon")]
        public string OsIcon { get;   set; }

        [NamePosition(21, Name = "OsIconBig")]
        public string OsIconBig { get;   set; }

        [NamePosition(22, Name = "OsInfoUrl")]
        public string OsInfoUrl { get;   set; }

        [NamePosition(23, Name = "OsFamily")]
        public string OsFamily { get;   set; }

        [NamePosition(24, Name = "OsFamilyCode")]
        public string OsFamilyCode { get;   set; }

        [NamePosition(25, Name = "OsFamilyVendor")]
        public string OsFamilyVendor { get;   set; }

        [NamePosition(26, Name = "OsFamilyVendorCode")]
        public string OsFamilyVendorCode { get;   set; }

        [NamePosition(27, Name = "OsFamilyVendorHomepage")]
        public string OsFamilyVendorHomepage { get;   set; }

        [NamePosition(28, Name = "DeviceClass")]
        public string DeviceClass { get;   set; }

        [NamePosition(29, Name = "DeviceClassCode")]
        public string DeviceClassCode { get;   set; }

        [NamePosition(30, Name = "DeviceClassIcon")]
        public string DeviceClassIcon { get;   set; }

        [NamePosition(31, Name = "DeviceClassIconBig")]
        public string DeviceClassIconBig { get;   set; }

        [NamePosition(32, Name = "DeviceClassInfoUrl")]
        public string DeviceClassInfoUrl { get;   set; }

        [NamePosition(33, Name = "CrawlerLastSeen")]
        public string CrawlerLastSeen { get;   set; }

        [NamePosition(34, Name = "CrawlerCategory")]
        public string CrawlerCategory { get;   set; }

        [NamePosition(35, Name = "CrawlerCategoryCode")]
        public string CrawlerCategoryCode { get;   set; }

        [NamePosition(36, Name = "CrawlerRespectRobotstxt")]
        public string CrawlerRespectRobotstxt { get;   set; }

        [NamePosition(37, Name = "DeviceMarketname")]
        public string DeviceMarketname { get; set; }

        [NamePosition(38, Name = "DeviceBrand")]
        public string DeviceBrand { get; set; }

        [NamePosition(39, Name = "DeviceBrandCode")]
        public string DeviceBrandCode { get; set; }

        [NamePosition(40, Name = "DeviceBrandHomepage")]
        public string DeviceBrandHomepage { get; set; }

        [NamePosition(41, Name = "DeviceBrandIcon")]
        public string DeviceBrandIcon { get; set; }

        [NamePosition(42, Name = "DeviceBrandIconBig")]
        public string DeviceBrandIconBig { get; set; }

        [NamePosition(43, Name = "DeviceBrandInfoUrl")]
        public string DeviceBrandInfoUrl { get; set; }

        #endregion
        #region constructors
        public UserAgent()
        {

        }
        #endregion
        #region public
        #endregion
    }
}

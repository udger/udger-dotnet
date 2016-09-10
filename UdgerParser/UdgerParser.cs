/*
  UdgerParser - Local parser lib
  
  UdgerParser class parses useragent strings based on a database downloaded from udger.com
 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
  license    GNU Lesser General Public License
  link       https://udger.com/products/local_parser
  
  Third Party lib:
  ADO.NET Data Provider for SQLite - http://www.sqlite.org/ - Public domain
  RegExpPerl.cs - https://github.com/DEVSENSE/Phalanger/blob/master/Source/ClassLibrary/RegExpPerl.cs - Apache License Version 2.0
 */

using System;
using System.Text;
using System.Data;
using System.IO;

namespace Udger.Parser
{
    public class UdgerParser
    {
        private DataReader dt;
        public UserAgent userAgent { get; private set; }
        public IPAddress ipAddress { get; private set; }

        public string ip { get; set; }
        public string ua { get; set; }

        /// <summary>
        /// Constructor 
        /// </summary> 
        public UdgerParser()
        {
            dt = new DataReader();
            this.ua = "";
            this.ip = "";

        }
        #region setParser method
        /// <summary>
        /// Set the data directory
        /// </summary> 
        /// <param name="dataDir">string path cache directory</param>
        public void SetDataDir(string dataDir)
        {
            if (!Directory.Exists(dataDir))
                throw new Exception("Data dir not found");

            dt.data_dir = dataDir;
            dt.DataSourcePath = dataDir + @"\udgerdb_v3.dat";

            if (!File.Exists(dt.DataSourcePath))
                throw new Exception("Data file udgerdb_v3.dat not found");
        }
        /// <summary>
        /// Set the data directory
        /// </summary> 
        /// <param name="dataDir">string path cache directory</param>
        /// <param name="fileName">string path cache directory</param>
        public void SetDataDir(string dataDir, string fileName)
        {
            if (!Directory.Exists(dataDir))
                throw new Exception("Data dir not found");

            dt.data_dir = dataDir;
            dt.DataSourcePath = dataDir + @"\" + fileName;

            if (!File.Exists(dt.DataSourcePath))
                throw new Exception("Data file " + fileName + " not found");
        }
        #endregion

        #region public method    
        /// <summary>
        /// Parse the useragent string and/or ip address
        /// /// </summary>       
        public void parse()
        {
            this.ipAddress = new IPAddress();
            this.userAgent = new UserAgent();

            dt.connect(this);
            if (dt.Connected)
            {
                if (this.ua != "")
                {                    
                    this.parseUA(this.ua.Replace("'", "''"));
                    this.ua = "";
                }
                if (this.ip != "")
                {                    
                    this.parseIP(this.ip.Replace("'", "''"));
                    this.ip = "";
                }

            }
        }
        #endregion

        #region private method

        #region parse
        private void parseUA(string _userAgent)
        {
            int client_id = 0;
            int client_class_id = -1;
            int os_id = 0;
            int deviceclass_id = 0;
            System.Text.RegularExpressions.Regex searchTerm;
            PerlRegExpConverter regConv;

            if (!string.IsNullOrEmpty(_userAgent))
            {
                userAgent.UaString = this.ua;
                userAgent.UaClass = "Unrecognized";
                userAgent.UaClassCode = "unrecognized";

                if (dt.Connected)
                {
                    DataTable crawlerList = dt.selectQuery(@"SELECT udger_crawler_list.id as botid,name,ver,ver_major,last_seen,respect_robotstxt,family,family_code,family_homepage,family_icon,vendor,vendor_code,vendor_homepage,crawler_classification,crawler_classification_code
                                          FROM udger_crawler_list
                                          LEFT JOIN udger_crawler_class ON udger_crawler_class.id = udger_crawler_list.class_id
                                          WHERE ua_string = '" + _userAgent + "'");
                    // crawler
                    if (crawlerList.Rows.Count > 0)
                    {
                        client_class_id = 99;
                        this.prepareCrawler(crawlerList.Rows[0]);
                    }
                    else
                    {
                        //client
                        DataTable clientRegex = dt.selectQuery(@"SELECT class_id,client_id,regstring,name,name_code,homepage,icon,icon_big,engine,vendor,vendor_code,vendor_homepage,uptodate_current_version,client_classification,client_classification_code
                                              FROM udger_client_regex
                                              JOIN udger_client_list ON udger_client_list.id = udger_client_regex.client_id
                                              JOIN udger_client_class ON udger_client_class.id = udger_client_list.class_id
                                              ORDER BY sequence ASC");

                        if (clientRegex != null)
                            foreach (DataRow row in clientRegex.Rows)
                            {
                                regConv = new PerlRegExpConverter(row["regstring"].ToString(), "", Encoding.UTF8);
                                searchTerm = regConv.Regex;

                                if (searchTerm.IsMatch(_userAgent))
                                {
                                    var match = searchTerm.Match(_userAgent);
                                    if (match.Success && match.Groups.Count > 1)
                                    {
                                        this.prepareClientRegex(row, match, ref client_id, ref client_class_id);
                                        break;
                                    }
                                    else if (match.Success && match.Groups.Count == 1)
                                    {
                                        this.prepareClientRegex(row, match, ref client_id, ref client_class_id);
                                        break;
                                    }
                                }
                            }
                    }
                    // OS
                    DataTable osRegex = dt.selectQuery(@"SELECT os_id,regstring,family,family_code,name,name_code,homepage,icon,icon_big,vendor,vendor_code,vendor_homepage
                                              FROM udger_os_regex
                                              JOIN udger_os_list ON udger_os_list.id = udger_os_regex.os_id
                                              ORDER BY sequence ASC");
                    if (osRegex != null)
                        foreach (DataRow row in osRegex.Rows)
                        {
                            regConv = new PerlRegExpConverter(row["regstring"].ToString(), "", Encoding.UTF8);
                            searchTerm = regConv.Regex;
                            if (searchTerm.IsMatch(_userAgent))
                            {
                                this.prepareOs(row, ref os_id);
                                break;
                            }
                        }
                    // client_os_relation
                    if (os_id == 0 && client_id != 0)
                    {
                        DataTable clientOSRelations = dt.selectQuery(@"SELECT os_id,family,family_code,name,name_code,homepage,icon,icon_big,vendor,vendor_code,vendor_homepage
                                                  FROM udger_client_os_relation
                                                  JOIN udger_os_list ON udger_os_list.id = udger_client_os_relation.os_id
                                                  WHERE client_id = '" + client_id.ToString() + "'");

                        if (clientOSRelations != null && clientOSRelations.Rows.Count > 0)
                        {
                            this.prepareOs(clientOSRelations.Rows[0], ref os_id);
                        }
                    }
                    // device
                    DataTable device = dt.selectQuery(@"SELECT deviceclass_id,regstring,name,name_code,icon,icon_big
                                              FROM udger_deviceclass_regex
                                              JOIN udger_deviceclass_list ON udger_deviceclass_list.id=udger_deviceclass_regex.deviceclass_id
                                              ORDER BY sequence ASC");

                    if (device != null)
                        foreach (DataRow row in device.Rows)
                        {
                            regConv = new PerlRegExpConverter(row["regstring"].ToString(), "", Encoding.UTF8);
                            searchTerm = regConv.Regex;
                            if (searchTerm.IsMatch(_userAgent))
                            {
                                this.prepareDevice(row, ref deviceclass_id);
                                break;
                            }
                        }
                    if (deviceclass_id == 0 && client_class_id != -1)
                    {
                        DataTable deviceList = dt.selectQuery(@"SELECT deviceclass_id,name,name_code,icon,icon_big 
                                                  FROM udger_deviceclass_list
                                                  JOIN udger_client_class ON udger_client_class.deviceclass_id = udger_deviceclass_list.id
                                                  WHERE udger_client_class.id = '" + client_class_id.ToString() + "'");
                        if (deviceList != null && deviceList.Rows.Count > 0)
                        {
                            this.prepareDevice(deviceList.Rows[0], ref deviceclass_id);
                        }
                    }
                }
            }

        }

        private void parseIP(string _ip)
        {
            string ipLoc;
            if (!string.IsNullOrEmpty(_ip))
            {
                ipAddress.Ip = this.ip;

                if (dt.Connected)
                {
                    int ipVer = this.getIPAddressVersion(ip, out ipLoc);
                    if (ipVer != 0)
                    {
                        if (ipLoc != "")
                            _ip = ipLoc;

                        ipAddress.IpVer = UdgerParser.ConvertToStr(ipVer);

                        DataTable ipTable = dt.selectQuery(@"SELECT udger_crawler_list.id as botid,ip_last_seen,ip_hostname,ip_country,ip_city,ip_country_code,ip_classification,ip_classification_code,
                                          name,ver,ver_major,last_seen,respect_robotstxt,family,family_code,family_homepage,family_icon,vendor,vendor_code,vendor_homepage,crawler_classification,crawler_classification_code,crawler_classification
                                          FROM udger_ip_list
                                          JOIN udger_ip_class ON udger_ip_class.id=udger_ip_list.class_id
                                          LEFT JOIN udger_crawler_list ON udger_crawler_list.id=udger_ip_list.crawler_id
                                          LEFT JOIN udger_crawler_class ON udger_crawler_class.id=udger_crawler_list.class_id
                                          WHERE ip=" + '"' + _ip + '"' + " ORDER BY sequence");

                        if (ipTable != null && ipTable.Rows.Count > 0)
                        {
                            this.prepareIp(ipTable.Rows[0]);
                        }
                        if (ipVer == 4)
                        {
                            long ipLong = this.AddrToInt(_ip);//ip2Long.Address;

                            DataTable dataCenter = dt.selectQuery(@"select name, name_code, homepage
                                       FROM udger_datacenter_range
                                       JOIN udger_datacenter_list ON udger_datacenter_range.datacenter_id = udger_datacenter_list.id
                                       where iplong_from <= " + ipLong.ToString() + " AND iplong_to >=" + ipLong.ToString());

                            if (dataCenter != null && dataCenter.Rows.Count > 0)
                            {
                                this.prepareIpDataCenter(dataCenter.Rows[0]);
                            }
                        }

                    }

                }
            }
        }
        #endregion

        #region prepare data methods

        private void prepareCrawler(DataRow _row)
        {
            userAgent.UaClass = "Crawler";
            userAgent.UaClassCode = "crawler";
            userAgent.Ua = UdgerParser.ConvertToStr(_row["name"]);
            userAgent.UaVersion = UdgerParser.ConvertToStr(_row["ver"]);
            userAgent.UaVersionMajor = UdgerParser.ConvertToStr(_row["ver_major"]);
            userAgent.UaFamily = UdgerParser.ConvertToStr(_row["family"]);
            userAgent.UaFamilyCode = UdgerParser.ConvertToStr(_row["family_code"]);
            userAgent.UaFamilyHompage = UdgerParser.ConvertToStr(_row["family_homepage"]);
            userAgent.UaFamilyVendor = UdgerParser.ConvertToStr(_row["vendor"]);
            userAgent.UaFamilyVendorCode = UdgerParser.ConvertToStr(_row["vendor_code"]);
            userAgent.UaFamilyVendorCode = UdgerParser.ConvertToStr(_row["vendor_homepage"]);
            userAgent.UaFamilyIcon = UdgerParser.ConvertToStr(_row["family_icon"]);
            userAgent.UaFamilyIconUrl = "https://udger.com/resources/ua-list/bot-detail?bot=" + UdgerParser.ConvertToStr(_row["family"]) + "#id" + UdgerParser.ConvertToStr(_row["botid"]);
            userAgent.CrawlerLastSeen = UdgerParser.ConvertToStr(_row["last_seen"]);
            userAgent.CrawlerCategory = UdgerParser.ConvertToStr(_row["crawler_classification"]);
            userAgent.CrawlerCategoryCode = UdgerParser.ConvertToStr(_row["crawler_classification_code"]);
            userAgent.CrawlerRespectRobotstxt = UdgerParser.ConvertToStr(_row["respect_robotstxt"]);
        }

        private void prepareClientRegex(DataRow _row, System.Text.RegularExpressions.Match _match, ref int _clienId, ref int _clientClassId)
        {
            _clienId = Convert.ToInt32(_row["client_id"]);
            _clientClassId = Convert.ToInt32(_row["class_id"]);
            userAgent.UaString = this.ua;
            userAgent.UaClass = UdgerParser.ConvertToStr(_row["client_classification"]);//ToString();
            userAgent.UaClassCode = UdgerParser.ConvertToStr(_row["client_classification_code"]);
            userAgent.Ua = UdgerParser.ConvertToStr(_row["name"]) + _match.Groups[1].Value;
            userAgent.UaVersion = _match.Groups[1].Value;
            string[] spliter = _match.Groups[1].Value.Split('.');
            userAgent.UaVersionMajor = spliter[0];
            userAgent.UaUptodateCurrentVersion = UdgerParser.ConvertToStr(_row["uptodate_current_version"]);
            userAgent.UaFamily = UdgerParser.ConvertToStr(_row["name"]);
            userAgent.UaFamilyCode = UdgerParser.ConvertToStr(_row["name_code"]);
            userAgent.UaFamilyHompage = UdgerParser.ConvertToStr(_row["homepage"]);
            userAgent.UaFamilyVendor = UdgerParser.ConvertToStr(_row["vendor"]);
            userAgent.UaFamilyVendorCode = UdgerParser.ConvertToStr(_row["vendor_code"]);
            userAgent.UaFamilyVendorHomepage = UdgerParser.ConvertToStr(_row["vendor_homepage"]);
            userAgent.UaFamilyIcon = UdgerParser.ConvertToStr(_row["icon"]);
            userAgent.UaFamilyIconBig = UdgerParser.ConvertToStr(_row["icon_big"]);
            userAgent.UaFamilyIconUrl = "https://udger.com/resources/ua-list/browser-detail?browser=" + UdgerParser.ConvertToStr(_row["name"]);
            userAgent.UaEngine = UdgerParser.ConvertToStr(_row["engine"]);

        }

        private void prepareOs(DataRow _row, ref int _osId)
        {
            _osId = Convert.ToInt32(_row["os_id"]);
            userAgent.Os = UdgerParser.ConvertToStr(_row["name"]);
            userAgent.OsCode = UdgerParser.ConvertToStr(_row["name_code"]);
            userAgent.OsHomepage = UdgerParser.ConvertToStr(_row["homepage"]);
            userAgent.OsIcon = UdgerParser.ConvertToStr(_row["icon"]);
            userAgent.OsIconBig = UdgerParser.ConvertToStr(_row["icon_big"]);
            userAgent.OsInfoUrl = "https://udger.com/resources/ua-list/os-detail?os=" + UdgerParser.ConvertToStr(_row["name"]);
            userAgent.OsFamily = UdgerParser.ConvertToStr(_row["family"]);
            userAgent.OsFamilyCode = UdgerParser.ConvertToStr(_row["family_code"]);
            userAgent.OsFamilyVendor = UdgerParser.ConvertToStr(_row["vendor"]);
            userAgent.OsFamilyVendorCode = UdgerParser.ConvertToStr(_row["vendor_code"]);
            userAgent.OsFamilyVendorHomepage = UdgerParser.ConvertToStr(_row["vendor_homepage"]);

        }

        private void prepareDevice(DataRow _row, ref int _deviceClassId)
        {

            _deviceClassId = Convert.ToInt32(_row["deviceclass_id"]);
            userAgent.DeviceClass = UdgerParser.ConvertToStr(_row["name"]);
            userAgent.DeviceClassCode = UdgerParser.ConvertToStr(_row["name_code"]);
            userAgent.DeviceClassIcon = UdgerParser.ConvertToStr(_row["icon"]);
            userAgent.DeviceClassIconBig = UdgerParser.ConvertToStr(_row["icon_big"]);
            userAgent.DeviceClassInfoUrl = "https://udger.com/resources/ua-list/device-detail?device=" + UdgerParser.ConvertToStr(_row["name"]);
        }

        private void prepareIp(DataRow _row)
        {
            ipAddress.IpClassification = UdgerParser.ConvertToStr(_row["ip_classification"]);
            ipAddress.IpClassificationCode = UdgerParser.ConvertToStr(_row["ip_classification_code"]);
            ipAddress.IpLastSeen = UdgerParser.ConvertToStr(_row["ip_last_seen"]);
            ipAddress.IpHostname = UdgerParser.ConvertToStr(_row["ip_hostname"]);
            ipAddress.IpCountry = UdgerParser.ConvertToStr(_row["ip_country"]);
            ipAddress.IpCountryCode = UdgerParser.ConvertToStr(_row["ip_country_code"]);
            ipAddress.IpCity = UdgerParser.ConvertToStr(_row["ip_city"]);
            ipAddress.CrawlerName = UdgerParser.ConvertToStr(_row["name"]);
            ipAddress.CrawlerVer = UdgerParser.ConvertToStr(_row["ver"]);
            ipAddress.CrawlerVerMajor = UdgerParser.ConvertToStr(_row["ver_major"]);
            ipAddress.CrawlerFamily = UdgerParser.ConvertToStr(_row["family"]);
            ipAddress.CrawlerFamilyCode = UdgerParser.ConvertToStr(_row["family_code"]);
            ipAddress.CrawlerFamilyHomepage = UdgerParser.ConvertToStr(_row["family_homepage"]);
            ipAddress.CrawlerFamilyVendor = UdgerParser.ConvertToStr(_row["vendor"]);
            ipAddress.CrawlerFamilyVendorCode = UdgerParser.ConvertToStr(_row["vendor_code"]);
            ipAddress.CrawlerFamilyVendorHomepage = UdgerParser.ConvertToStr(_row["vendor_homepage"]);
            ipAddress.CrawlerFamilyIcon = UdgerParser.ConvertToStr(_row["family_icon"]);
            ipAddress.CrawlerLastSeen = UdgerParser.ConvertToStr(_row["last_seen"]);
            ipAddress.CrawlerCategory = UdgerParser.ConvertToStr(_row["crawler_classification"]);
            ipAddress.CrawlerCategoryCode = UdgerParser.ConvertToStr(_row["crawler_classification_code"]);
            if (ipAddress.IpClassificationCode == "crawler")
                ipAddress.CrawlerFamilyInfoUrl = "https://udger.com/resources/ua-list/bot-detail?bot=" + UdgerParser.ConvertToStr(_row["family"]) + "#id" + UdgerParser.ConvertToStr(_row["botid"]);
            ipAddress.CrawlerRespectRobotstxt = UdgerParser.ConvertToStr(_row["respect_robotstxt"]);
        }

        private void prepareIpDataCenter(DataRow _row)
        {
            ipAddress.DatacenterName = UdgerParser.ConvertToStr(_row["name"]);
            ipAddress.DatacenterNameCode = UdgerParser.ConvertToStr(_row["name_code"]);
            ipAddress.DatacenterHomepage = UdgerParser.ConvertToStr(_row["homepage"]);
        }
        #endregion


        private static string ConvertToStr(object value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }

        private static DateTime ConvertToDateTime(string value)
        {
            DateTime dt;
            DateTime.TryParse(value, out dt);

            return dt;
        }

        private int getIPAddressVersion(string _ip, out string _retIp)
        {
            System.Net.IPAddress addr;
            _retIp = "";

            if (System.Net.IPAddress.TryParse(_ip, out addr))
            {
                _retIp = addr.ToString();
                if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return 4;
                if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    return 6;
            }

            return 0;
        }

        private long AddrToInt(string addr)
        {

            return (long)(uint)System.Net.IPAddress.NetworkToHostOrder(
                 (int)System.Net.IPAddress.Parse(addr).Address);
        }
        #endregion
    }
}
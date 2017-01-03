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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Udger.Parser
{
    public class UdgerParser
    {

        public UserAgent userAgent { get; private set; }
        public IPAddress ipAddress { get; private set; }

        public string ip { get; set; }
        public string ua { get; set; }
        #region private Variables
        private struct IdRegString
        {
            public int id;
            public int wordId1;
            public int wordId2;
            public string pattern;
        }

        private LRUCache<string, UserAgent> cache;
        private DataReader dt;
        private static WordDetector clientWordDetector;
        private static WordDetector deviceWordDetector;
        private static WordDetector osWordDetector;

        private static List<IdRegString> clientRegstringList;
        private static List<IdRegString> osRegstringList;
        private static List<IdRegString> deviceRegstringList;
        #endregion
        /// <summary>
        /// Constructor 
        /// </summary> 
        public UdgerParser()
        {
            dt = new DataReader();
            this.ua = "";
            this.ip = "";
           // this.
            cache = new LRUCache<string, UserAgent>();

        }
        public UdgerParser(int cashCapcity)
        {
            dt = new DataReader();
            this.ua = "";
            this.ip = "";
            cache = new LRUCache<string, UserAgent>(cashCapcity);

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
            UserAgent uaCache;

            dt.connect(this);
            UdgerParser.initStaticStructures(dt);
            if (dt.Connected)
            {
                if (this.ua != "")
                {
                    if (cache.TryGetValue(this.ua, out uaCache))
                        userAgent = uaCache;
                    else
                    {
                        this.parseUA(this.ua.Replace("'", "''"));
                        this.ua = "";
                    }
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

            if (!string.IsNullOrEmpty(_userAgent))
            {
                userAgent.UaString = this.ua;
                userAgent.UaClass = "Unrecognized";
                userAgent.UaClassCode = "unrecognized";
                
                if (dt.Connected)
                {
                    //Client
                    this.processClient(_userAgent, ref os_id, ref client_id, ref client_class_id);
                    //OS
                    this.processOS(_userAgent, ref os_id, client_id);
                    // device
                    this.processDevice(_userAgent, ref client_class_id);
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
        #region process methods

        private void processOS(string uaString, ref int os_id, int clientId)
        {

            int rowid = findIdFromList(uaString, osWordDetector.findWords(uaString), osRegstringList);
            if (rowid != -1)
            {
                string q = String.Format(UdgerSqlQuery.SQL_OS, rowid);
                DataTable opSysRs = dt.selectQuery(q);
                this.prepareOs(opSysRs.Rows[0], ref os_id);
            }
            else if(clientId != 0)
            {
                    DataTable opSysRs = dt.selectQuery(String.Format(UdgerSqlQuery.SQL_CLIENT_OS, clientId));
                    if (opSysRs != null && opSysRs.Rows.Count > 0)
                    {
                        this.prepareOs(opSysRs.Rows[0], ref os_id);
                    }
                    
                }
        }


        private void processClient(string uaString, ref int os_id, ref int clientId, ref int classId)
        {
            string q = String.Format(UdgerSqlQuery.SQL_CRAWLER, uaString);
            DataTable userAgentRs = dt.selectQuery(q);
            if (userAgentRs != null && userAgentRs.Rows.Count > 0 )
            {

                this.prepareUa(userAgentRs.Rows[0],true, ref clientId, ref classId);
                classId = 99;
                clientId = -1;
            }
            else {
                int rowid = this.findIdFromList(uaString, clientWordDetector.findWords(uaString), clientRegstringList);
                if (rowid != -1)
                {
                    userAgentRs = dt.selectQuery(String.Format(UdgerSqlQuery.SQL_CLIENT, rowid));
                    this.prepareUa(userAgentRs.Rows[0],false, ref clientId, ref classId);
                    //patchVersions(ret);
                }
                else {
                    userAgent.UaClass = "Unrecognized";
                    userAgent.UaClassCode = "unrecognized";
                }
            }
        }

        private void processDevice(string uaString, ref int classId)
        {
            int rowid = this.findIdFromList(uaString, deviceWordDetector.findWords(uaString), deviceRegstringList);
            if (rowid != -1)
            {
                DataTable devRs = dt.selectQuery(String.Format(UdgerSqlQuery.SQL_DEVICE, rowid));
                this.prepareDevice(devRs.Rows[0], ref classId);
            }
            else {
                if ( classId != -1)
                {
                    DataTable devRs = dt.selectQuery(String.Format(UdgerSqlQuery.SQL_CLIENT_CLASS, classId.ToString()));
                    if (devRs != null && devRs.Rows.Count > 0)
                    {
                        this.prepareDevice(devRs.Rows[0], ref classId);
                    }
                }
            }
        }
        #endregion
        #region prepare data methods

        private void prepareUa(DataRow _row,Boolean crawler,ref int clientId, ref int classId)
        {
            //clientId = Convert.ToInt32(_row["client_id"]);
            //classId = Convert.ToInt32(_row["class_id"]);
            userAgent.CrawlerCategory = UdgerParser.ConvertToStr(_row["crawler_category"]);
            userAgent.CrawlerCategoryCode = UdgerParser.ConvertToStr(_row["crawler_category_code"]);
            userAgent.CrawlerLastSeen = UdgerParser.ConvertToStr(_row["crawler_last_seen"]);
            userAgent.CrawlerRespectRobotstxt = UdgerParser.ConvertToStr(_row["crawler_respect_robotstxt"]);
            userAgent.UaString = this.ua;
            userAgent.UaClass = UdgerParser.ConvertToStr(_row["ua_class"]);//ToString();
            userAgent.UaClassCode = UdgerParser.ConvertToStr(_row["ua_class_code"]);
            userAgent.Ua = UdgerParser.ConvertToStr(_row["ua"]);
            userAgent.UaVersion = UdgerParser.ConvertToStr(_row["ua_version"]);
            userAgent.UaVersionMajor = UdgerParser.ConvertToStr(_row["ua_version_major"]);
            userAgent.UaUptodateCurrentVersion = UdgerParser.ConvertToStr(_row["ua_uptodate_current_version"]);
            userAgent.UaFamily = UdgerParser.ConvertToStr(_row["ua_family"]);
            userAgent.UaFamilyCode = UdgerParser.ConvertToStr(_row["ua_family_code"]);
            userAgent.UaFamilyHompage = UdgerParser.ConvertToStr(_row["ua_family_homepage"]);
            userAgent.UaFamilyVendor = UdgerParser.ConvertToStr(_row["ua_family_vendor"]);
            userAgent.UaFamilyVendorCode = UdgerParser.ConvertToStr(_row["ua_family_vendor_code"]);
            userAgent.UaFamilyVendorHomepage = UdgerParser.ConvertToStr(_row["ua_family_vendor_homepage"]);
            userAgent.UaFamilyIcon = UdgerParser.ConvertToStr(_row["ua_family_icon"]);
            userAgent.UaFamilyIconBig = UdgerParser.ConvertToStr(_row["ua_family_icon_big"]);
            userAgent.UaFamilyIconUrl = UdgerParser.ConvertToStr(_row["ua_family_info_url"]);
            userAgent.UaEngine = UdgerParser.ConvertToStr(_row["ua_engine"]);

        }
        private void prepareOs(DataRow _row, ref int _osId)
        {
            //_osId = Convert.ToInt32(_row["os_id"]);
            userAgent.Os = UdgerParser.ConvertToStr(_row["os"]);
            userAgent.OsCode = UdgerParser.ConvertToStr(_row["os_code"]);
            userAgent.OsHomepage = UdgerParser.ConvertToStr(_row["os_home_page"]);
            userAgent.OsIcon = UdgerParser.ConvertToStr(_row["os_icon"]);
            userAgent.OsIconBig = UdgerParser.ConvertToStr(_row["os_icon_big"]);
            userAgent.OsInfoUrl = UdgerParser.ConvertToStr(_row["os_info_url"]);
            userAgent.OsFamily = UdgerParser.ConvertToStr(_row["os_family"]);
            userAgent.OsFamilyCode = UdgerParser.ConvertToStr(_row["os_family_code"]);
            userAgent.OsFamilyVendor = UdgerParser.ConvertToStr(_row["os_family_vendor"]);
            userAgent.OsFamilyVendorCode = UdgerParser.ConvertToStr(_row["os_family_vendor_code"]);
            userAgent.OsFamilyVendorHomepage = UdgerParser.ConvertToStr(_row["os_family_vedor_homepage"]);

        }

        private void prepareDevice(DataRow _row, ref int _deviceClassId)
        {

            //_deviceClassId = Convert.ToInt32(_row["device_class"]);
            userAgent.DeviceClass = UdgerParser.ConvertToStr(_row["device_class"]);
            userAgent.DeviceClassCode = UdgerParser.ConvertToStr(_row["device_class_code"]);
            userAgent.DeviceClassIcon = UdgerParser.ConvertToStr(_row["device_class_icon"]);
            userAgent.DeviceClassIconBig = UdgerParser.ConvertToStr(_row["device_class_icon_big"]);
            userAgent.DeviceClassInfoUrl =  UdgerParser.ConvertToStr(_row["device_class_info_url"]);
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

        private static int ConvertToInt(object value)
        {
            if (value == null)
                return 0;
            return Convert.ToInt32(value);
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void initStaticStructures(DataReader connection)
        {
            if (clientRegstringList == null) {

                    clientRegstringList = prepareRegexpStruct(connection, "udger_client_regex");
                    osRegstringList = prepareRegexpStruct(connection, "udger_os_regex");
                    deviceRegstringList = prepareRegexpStruct(connection, "udger_deviceclass_regex");

                    clientWordDetector = createWordDetector(connection, "udger_client_regex", "udger_client_regex_words");
                    deviceWordDetector = createWordDetector(connection, "udger_deviceclass_regex", "udger_deviceclass_regex_words");
                    osWordDetector = createWordDetector(connection, "udger_os_regex", "udger_os_regex_words");
                }
        }

        private static WordDetector createWordDetector(DataReader connection, String regexTableName, String wordTableName)
        {

            HashSet<int> usedWords = new HashSet<int>();

            addUsedWords(usedWords, connection, regexTableName, "word_id");
            addUsedWords(usedWords, connection, regexTableName, "word2_id");

            WordDetector result = new WordDetector();

            DataTable dt = connection.selectQuery("SELECT * FROM " + wordTableName);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int id = UdgerParser.ConvertToInt(row["id"]);
                    if (usedWords.Contains(id))
                    {
                        String word = UdgerParser.ConvertToStr(row["word"]).ToLower();
                        result.addWord(id, word);
                    }
                }
            }
            return result;
        }

    private static void addUsedWords(HashSet<int> usedWords, DataReader connection, String regexTableName, String wordIdColumn) 
    {
            DataTable rs = connection.selectQuery("SELECT " + wordIdColumn + " FROM " + regexTableName);
            if (rs != null)
            {
                foreach (DataRow row in rs.Rows)
                {
                    usedWords.Add(UdgerParser.ConvertToInt(row[wordIdColumn]));
                }
            }
    }

    private int findIdFromList(String uaString, HashSet<int> foundClientWords, List<IdRegString> list)
    {
        System.Text.RegularExpressions.Regex searchTerm;
        PerlRegExpConverter regConv;

         foreach (IdRegString irs in list)
        {
            if ((irs.wordId1 == 0 || foundClientWords.Contains(irs.wordId1)) &&
                (irs.wordId2 == 0 || foundClientWords.Contains(irs.wordId2)))
            {
                regConv = new PerlRegExpConverter(irs.pattern, "", Encoding.UTF8);
                searchTerm = regConv.Regex;
                if (searchTerm.IsMatch(uaString))
                {
                    //lastPatternMatcher = irs.pattern;
                    return irs.id;
                }
            }
        }
        return -1;
    }
 
    private static List<IdRegString> prepareRegexpStruct(DataReader connection, String regexpTableName) 
    {
        List<IdRegString> ret = new List<IdRegString>();
        DataTable rs = connection.selectQuery("SELECT rowid, regstring, word_id, word2_id FROM " + regexpTableName + " ORDER BY sequence");

        if (rs != null) {
        foreach (DataRow row in rs.Rows)
        {
            IdRegString irs = new IdRegString();
            irs.id = UdgerParser.ConvertToInt(row["rowid"]);
            irs.wordId1 = UdgerParser.ConvertToInt(row["word_id"]);
            irs.wordId2 = UdgerParser.ConvertToInt(row["word2_id"]);
            String regex = UdgerParser.ConvertToStr(row["regstring"]);
           // regConv = new PerlRegExpConverter(, "", Encoding.Unicode);
            Regex reg = new Regex(@"^/?(.*?)/si$");
            if (reg.IsMatch(regex))
            {
                regex = reg.Match(regex).Groups[0].ToString();
            }
            irs.pattern = regex;//Pattern.compile(regex, Pattern.CASE_INSENSITIVE | Pattern.DOTALL);
            ret.Add(irs);
        }
    }
        return ret;
    }

        #endregion
    }
}
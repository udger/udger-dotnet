/*
  UdgerParser - Local parser lib
  
  UdgerParser class parses useragent strings based on a database downloaded from udger.com
 
 
  author     Prokop Říha, Jaroslav Mallat (http://udger.com/)
  copyright  Copyright (c) 2014-2015 udger.com
  version    1.0.1.2
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
  
  Third Party lib:
  Json.NET - http://james.newtonking.com/json - MIT License
  ADO.NET Data Provider for SQLite - http://www.sqlite.org/ - Public domain
  RegExpPerl.cs - https://github.com/DEVSENSE/Phalanger/blob/master/Source/ClassLibrary/RegExpPerl.cs - Apache License Version 2.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace Udger.Parser
{
    public class UdgerParser
    {
        private DataReader dt;
        private bool debug;


        /// <summary>
        /// Constructor 
        /// </summary> 
        /// <param name="writeDebugInfo">write debug info output to console</param>
        public UdgerParser(bool writeDebugInfo = false)
        {
            dt = new DataReader();
            debug = writeDebugInfo;
            if (debug)
            {
                TextWriterTraceListener consoleListener = new TextWriterTraceListener(Console.Out);
                Debug.Listeners.Clear();
                Debug.Listeners.Add(consoleListener);
            }
        }
        #region setParser method
        /// <summary>
        /// Set the udger.com You Acceskey
        /// </summary> 
        /// <param name="accessKey">You Acceskey</param>        
        public void SetAccessKey(string accessKey) 
        {
            this.WriteDebug("Setting AccessKey to " + accessKey);
            dt.access_key = accessKey;
        }

        /// <summary>
        /// Set the data directory
        /// </summary> 
        /// <param name="dataDir">string path cache directory</param>
        public void SetDataDir(string dataDir)
        {
            this.WriteDebug("Setting cache dir to " + dataDir);
            dt.data_dir = dataDir;
            dt.DataSourcePath = dataDir + @"\udgerdb.dat";
        }

        /// <summary>
        /// Set to parse fragments
        /// </summary>
        /// <para>parse fragments Yes/No</para>
        public void SetParseFragments(bool parseFragments)
        {
            dt.parse_fragments = parseFragments;
        }
        #endregion

        #region public method
        /// <summary>
        /// Check if useragent string and/or IP address is bot 
        /// </summary>         
        /// <param name="_useragent">user agent string</param>
        /// <param name="_ip">IP address v4 or v6</param>
        /// <returns>Dictionary</returns> 
        public Dictionary<string, object> isBot(string _useragent = "", string _ip = "")
        {
            this.WriteDebug("isBot: start");           
            Dictionary<string, object> ret = new Dictionary<string, object>(); ;
            
            if (_useragent == "" && _ip == "")
            {
                this.WriteDebug("isBot: Missing mandatory parameter");
                ret.Add("flag", 1);
                ret.Add("errortext", "missing mandatory parameter");
                return ret;
            }

            if (_ip != "" && !this.IsValidIp(_ip))
            {
                this.WriteDebug("isBot: IP address is not valid");
                ret.Add("flag", 2);
                ret.Add("errortext", "ip address is not valid");
                return ret;
            }

            dt.connect(this);

            if (!dt.Connected)
            {
                this.WriteDebug("Data file not found, download the data manually");
                ret.Add("flag", 3);
                ret.Add("errortext", "data file not found");
                return ret;
            }   
         
            bool botInfo = false;
            bool botInfoUA = false;
            bool botInfoIP = false;
            bool harmony = false;
            string botName = "";
            string family = "";
            string botURL = "";           

            if (_useragent != "")
            {
                this.WriteDebug("isBot: test useragent");
                DataTable table1 = dt.selectQuery("SELECT name,family FROM c_robots where md5='" + this.CreateMD5(_useragent) + "'");
                if (table1.Rows.Count > 0)
                {
                    DataRow rowusr = table1.Rows[0];
                    botInfo = true;
                    botInfoUA = true;
                    botName = rowusr["name"].ToString();
                    family = rowusr["family"].ToString();
                    botURL = "http://udger.com/resources/ua-list/bot-detail?bot=" + family;
                }
            }

            if (_ip != "")
            {
                this.WriteDebug("isBot: test IP address");
                DataTable table2 = dt.selectQuery("SELECT name,family from c_robots AS C JOIN bot_ip as B ON C.id=B.robot and B.md5='" + this.CreateMD5(_ip) + "' ");
                if (table2.Rows.Count > 0)
                {
                    DataRow row = table2.Rows[0];
                    botInfo = true;
                    botInfoIP = true;
                    if (family == row["family"].ToString())
                    {
                        harmony = true;
                    }
                    botName = row["name"].ToString();
                    botURL = "http://udger.com/resources/ua-list/bot-detail?bot=" + row["family"].ToString();
                }
            }
            this.WriteDebug("isBot: completed");
            
            ret.Add("flag", 0);
            ret.Add("is_bot", botInfo);
            ret.Add("bot_by_ua", botInfoUA);
            ret.Add("bot_by_ip", botInfoIP);
            ret.Add("harmony_ua_ip", harmony);
            ret.Add("bot_name", botName);
            ret.Add("bot_udger_url", botURL);

            return ret;
        }

        /// <summary>
        /// Parse the useragent string 
        /// /// </summary> 
        /// <param name="_useragent">user agent string</param>
        /// <returns>Dictionary</returns>         
        public Dictionary<string, object> parse(string _useragent)
        {
            this.WriteDebug("parse: start");
            Dictionary<string, object> ret = new Dictionary<string, object>();
            
            if (String.IsNullOrEmpty(_useragent))
            {
                this.WriteDebug("parse: Missing mandatory parameter");
                ret.Add("flag", 1);
                ret.Add("errortext", "missing mandatory parameter");
                return ret;
            }
            
            dt.connect(this);
            
            if (!dt.Connected)
            {
                this.WriteDebug("Data file not found, download the data manually");
                ret.Add("flag", 3);
                ret.Add("errortext", "data file not found");
                return ret;
            }   
                        
            Dictionary<string, object> fragments = new Dictionary<string, object>();
            Dictionary<string, object> info = new Dictionary<string, object>();
            Dictionary<string, object> uptodate = new Dictionary<string, object>();
            string browser_id = "";
            string os_id = "";
            string device_id = "";


            #region set empty buffer
            info.Add("type", "unknown");
            info.Add("ua_name", "unknown");
            info.Add("ua_ver", "");
            info.Add("ua_family", "unknown");
            info.Add("ua_url", "unknown");
            info.Add("ua_company", "unknown");
            info.Add("ua_company_url", "unknown");
            info.Add("ua_icon", "unknown.png");
            info.Add("ua_engine", "n/a");
            info.Add("ua_udger_url", "");
            info.Add("os_name", "unknown");
            info.Add("os_family", "unknown");
            info.Add("os_url", "unknown");
            info.Add("os_company", "unknown");
            info.Add("os_company_url", "unknown");
            info.Add("os_icon", "unknown.png");
            info.Add("os_udger_url", "");
            info.Add("device_name", "Personal computer");
            info.Add("device_icon", "desktop.png");
            info.Add("device_udger_url", "http://udger.com/resources/ua-list/device-detail?device,Personal%20computer");

            //set empty uptodate buffer
            uptodate.Add("controlled", false);
            uptodate.Add("is", false);
            uptodate.Add("ver", "");
            uptodate.Add("url", "");
            #endregion


            DataTable table2 = dt.selectQuery("SELECT name,family,url,company,url_company,icon FROM c_robots where md5='" + this.CreateMD5(_useragent) + "'");
            if (table2.Rows.Count > 0)
            {
                DataRow row = table2.Rows[0];
                info["type"] = "Robot";
                info["ua_name"] = row["name"];
                info["ua_family"] = row["family"];
                info["ua_url"] = row["url"];
                info["ua_company"] = row["company"];
                info["ua_company_url"] = row["url_company"];
                info["ua_icon"] = row["icon"];
                info["ua_udger_url"] = "http://udger.com/resources/ua-list/bot-detail?bot=" + row["family"];
                info["device_name"] = "Other";
                info["device_icon"] = "other.png";
                info["device_udger_url"] = "http://udger.com/resources/ua-list/device-detail?device=Other";

                ret.Add("flag", 1);
                ret.Add("info", info);
                ret.Add("fragments", fragments);
                ret.Add("uptodate", uptodate);

                return ret;

            }

            DataTable table = dt.selectQuery("SELECT browser, regstring FROM reg_browser ORDER by sequence ASC");
            System.Text.RegularExpressions.Regex searchTerm;
            foreach (DataRow dr in table.Rows)
            {

                PerlRegExpConverter regConv = new PerlRegExpConverter(dr["regstring"].ToString(), "", Encoding.UTF8);
                searchTerm = regConv.Regex;

                if (searchTerm.IsMatch(_useragent))
                {
                    string matchRes = "";
                    browser_id = dr["browser"].ToString();
                    DataTable c_browser = dt.selectQuery("SELECT type,name,engine,url,company,company_url,icon FROM c_browser WHERE id=" + browser_id + " ");
                    System.Text.RegularExpressions.Match m = searchTerm.Match(_useragent);


                    if (m.Success)
                    {
                        if (m.Groups.Count > 1)
                            matchRes = m.Groups[1].Value;
                    }

                    if (c_browser.Rows.Count > 0)
                    {
                        DataRow c_BwrRow = c_browser.Rows[0];
                        DataTable c_browser_type = dt.selectQuery("SELECT name FROM c_browser_type WHERE type=" + c_BwrRow["type"] + " ");
                        if (c_browser_type.Rows.Count > 0)
                        {
                            DataRow c_BwrRowType = c_browser_type.Rows[0];
                            info["type"] = c_BwrRowType["name"];
                            info["ua_name"] = c_BwrRow["name"].ToString() + " " + matchRes;
                            info["ua_ver"] = matchRes;
                            info["ua_family"] = c_BwrRow["name"];
                            info["ua_url"] = c_BwrRow["url"];
                            info["ua_company"] = c_BwrRow["company"];
                            info["ua_company_url"] = c_BwrRow["company_url"];
                            info["ua_icon"] = c_BwrRow["icon"];
                            info["ua_engine"] = c_BwrRow["engine"];
                            info["ua_udger_url"] = "http://udger.com/resources/ua-list/browser-detail?browser=" + c_BwrRow["name"];

                            break;
                        }
                    }
                }
            }

            if (browser_id != "")
            {
                DataTable c_browser_os = dt.selectQuery("SELECT os FROM c_browser_os where browser=" + browser_id + "");
                if (c_browser_os.Rows.Count > 0)
                {
                    DataRow c_brwOsType = c_browser_os.Rows[0];
                    os_id = c_brwOsType["os"].ToString();
                }
            }

            if (os_id == "")
            {
                DataTable reg_os = dt.selectQuery("SELECT os, regstring FROM reg_os ORDER by sequence ASC");
                if (reg_os.Rows.Count > 0)
                {
                    foreach (DataRow reg_osRow in reg_os.Rows)
                    {
                        PerlRegExpConverter regOsConv = new PerlRegExpConverter(reg_osRow["regstring"].ToString(), "", Encoding.UTF8);
                        searchTerm = regOsConv.Regex;
                        if (searchTerm.IsMatch(_useragent))
                        {
                            os_id = reg_osRow["os"].ToString();
                            break;
                        }
                    }
                }
            }

            if (os_id != "")
            {
                DataTable c_os = dt.selectQuery("SELECT name, family, url, company, company_url, icon FROM c_os where id=" + os_id + "");
                if (c_os.Rows.Count > 0)
                {
                    DataRow c_osRow = c_os.Rows[0];
                    info["os_name"] = c_osRow["name"];
                    info["os_family"] = c_osRow["family"];
                    info["os_url"] = c_osRow["url"];
                    info["os_company"] = c_osRow["company"];
                    info["os_company_url"] = c_osRow["company_url"];
                    info["os_icon"] = c_osRow["icon"];
                    info["os_udger_url"] = @"http://udger.com/resources/ua-list/os-detail?os=" + c_osRow["name"];
                }
            }

            DataTable reg_device = dt.selectQuery("SELECT device, regstring FROM reg_device ORDER by sequence ASC");
            if (reg_device.Rows.Count > 0)
            {
                foreach (DataRow reg_deviceRow in reg_device.Rows)
                {
                    PerlRegExpConverter regOsConv = new PerlRegExpConverter(reg_deviceRow["regstring"].ToString(), "", Encoding.UTF8);
                    searchTerm = regOsConv.Regex;
                    if (searchTerm.IsMatch(_useragent))
                    {

                        device_id = reg_deviceRow["device"].ToString();
                        break;
                    }
                }
            }

            if (device_id != "")
            {
                DataTable c_device = dt.selectQuery("SELECT name,icon FROM c_device WHERE id=" + device_id + " ");
                if (c_device.Rows.Count > 0)
                {
                    DataRow c_deviceRow = c_device.Rows[0];
                    info["device_name"] = c_deviceRow["name"];
                    info["device_icon"] = c_deviceRow["icon"];
                    info["device_udger_url"] = "http://udger.com/resources/ua-list/device-detail?device=" + c_deviceRow["name"];

                }
            }
            else if (info["type"].ToString() == "Mobile Browser")
            {
                info["device_name"] = "Smartphone";
                info["device_icon"] = "phone.png";
                info["device_udger_url"] = "http://udger.com/resources/ua-list/device-detail?device=Smartphone";
            }

            if (browser_id != "")
            {
                char delimiterChars = '.';
                int testVer;
                string[] ver_major = info["ua_ver"].ToString().Split(delimiterChars);
                if(!String.IsNullOrEmpty(ver_major[0])) 
                {

                    if (int.TryParse(ver_major[0], out testVer))
                    {
                        DataTable c_browser_uptodate = dt.selectQuery("SELECT ver, url FROM c_browser_uptodate WHERE browser_id='" + browser_id + "' AND (os_independent = 1 OR os_family = '" + info["os_family"] + "')");
                        if (c_browser_uptodate.Rows.Count > 0)
                        {
                            DataRow c_browser_uptodateRow = c_browser_uptodate.Rows[0];
                            uptodate["controlled"] = true;
                            if (Convert.ToInt32(ver_major[0]) >= Convert.ToInt32(c_browser_uptodateRow["ver"]))
                            {
                                uptodate["is"] = true;
                            }
                            uptodate["ver"] = c_browser_uptodateRow["ver"];
                            uptodate["url"] = c_browser_uptodateRow["url"];
                        }
                    }
                }
            }

            //// TODO : Parse Fragmet

            ret.Add("flag", 1);
            ret.Add("info", info);
            ret.Add("fragments", fragments);
            ret.Add("uptodate", uptodate);

            return ret;


        }

        /// <summary>check your subscription</summary>
        /// <returns>Dictionary</returns> 
        public Dictionary<string, object> account()
        {
            string result = "";
            if (!string.IsNullOrEmpty(dt.access_key))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.udger.com/account");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string postData = "accesskey=" + dt.access_key;
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = bytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                result = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Dictionary<string,object>>(result);

        }
        
        #endregion


        #region private method
        public void WriteDebug(string _msg)
        {
            if (debug)
            {
                Debug.WriteLine( string.Format("{0} - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), _msg) );
            }

        }

        private bool IsValidIp(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}

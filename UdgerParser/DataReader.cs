
using System;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Net;
using System.IO;
using System.Security.Cryptography;


namespace Udger.Parser
{
    class DataReader
    {

        public string DataSourcePath { get; set; }
        private bool connected;
        public bool Connected { get { return this.connected; } }
        private SQLiteConnection sqlite;
        public int updateInteval { get; set; }
        public int timeout { get; set; }
        public string baseUrl { get; set; }
        public string apiUrl { get; set; }
        public string ver_filename { get; set; }
        public string data_filenam { get; set; }
        public string access_key { get; set; }
        public string data_dir { get; set; }
        public bool parse_fragments { get; set; }
        UdgerParser udger;

        public DataReader()
        {
            updateInteval = 86400; // in seconds - 86400 is 1 day
            parse_fragments = false;
            baseUrl = @"http://data.udger.com/";
        }

        public void connect(UdgerParser _udger)
        {
            udger = _udger;
            try
            {
                this.validate();

                if (!this.Connected)
                {                    
                    if (!string.IsNullOrEmpty(this.access_key))                    
                    {
                        if (File.Exists(DataSourcePath))
                        {
                            sqlite = new SQLiteConnection(@"Data Source=" + DataSourcePath);
                            this.connected = true;
                            if (sqlite != null)
                            {
                                DataTable _info_ = this.selectQuery("SELECT lastupdate,version FROM udger_db_info where key=1");
                                if (_info_.Rows.Count > 0)
                                {                                    
                                    DataRow row = _info_.Rows[0];
                                    long time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                                    long lastUpdate = Convert.ToInt32(row["lastupdate"].ToString());
                                
                                    if (lastUpdate + this.updateInteval < time)
                                    {                                        
                                        this.downloadFile(row["version"].ToString());
                                    }                                                                   
                                }
                                else
                                {
                                    this.downloadFile();
                                }

                            }
                        }
                        else {
                            this.downloadFile();
                        }
                    }
                    if (File.Exists(DataSourcePath))
                    {
                        sqlite = new SQLiteConnection(@"Data Source=" + DataSourcePath);
                        this.connected = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable selectQuery(string query)
        {
            if (connected)
            {
                SQLiteDataAdapter ad;
                DataTable dt = new DataTable();

                try
                {
                    SQLiteCommand cmd;
                    sqlite.Open();  //Initiate connection to the db
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = query;  //set the passed query
                    ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt); //fill the datasource
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
                sqlite.Close();
                return dt;
            }
            return new DataTable();
        }
               
        private void downloadFile(string version = "")        
        {
            try
            {
                if (!string.IsNullOrEmpty(version))
                {
                    if (this.serverVersion() == version)
                    {                        
                        long time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                        this.selectQuery("UPDATE udger_db_info SET lastupdate=" + time.ToString() + " WHERE key=1");
                        return;
                    }
                }


                using (WebClient myWebClient = new WebClient())
                { 
                    if (sqlite != null)
                    {
                        
                        sqlite.Close();
                        sqlite.Dispose();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        this.connected = false;
                        
                    }

                    myWebClient.DownloadFile(new Uri(baseUrl + access_key + @"/udgerdb_v3.dat"), DataSourcePath);
                    myWebClient.Dispose();

                        
                    if (this.GetMD5HashFromFile(DataSourcePath) != this.serverHash())
                    {
                        File.Delete(DataSourcePath);
                    }
                
               }
               if (File.Exists(DataSourcePath))
               {
                    sqlite = new SQLiteConnection(@"Data Source=" + DataSourcePath);
                    this.connected = true;
               
                    long time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                    this.selectQuery("UPDATE udger_db_info SET lastupdate=" + time.ToString() + " WHERE key=1");
               }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string serverVersion()
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl + access_key + @"/version");
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream stream = myHttpWebResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            myHttpWebResponse.Close();

            return result;
        }
        private string serverHash()
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl + access_key + @"/udgerdb_v3_dat.md5");
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream stream = myHttpWebResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            myHttpWebResponse.Close();

            return result;
        }
 
        private bool compareHash()
        {
            if (File.Exists(this.DataSourcePath))
            {
                string fileHash = this.GetMD5HashFromFile(DataSourcePath);
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl + access_key + @"/udgerdb_v3_dat.md5");
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream stream = myHttpWebResponse.GetResponseStream();                 
                StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();
                myHttpWebResponse.Close();
                if (fileHash == result)
                    return true;
                else
                    return false;
            }
            return true;
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

        private string GetMD5HashFromFile(string ff)
        {
          using (var md5 = MD5.Create())
          {
            using (var stream = File.Open(ff, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
              return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-",string.Empty).ToLower();
            }
          }
        }

        private void validate()
        {
            bool writeAllow =false;
            //bool writeDeny = false;
            if (!Directory.Exists(data_dir))
                throw new Exception("Data dir not found");

            if (Directory.Exists(data_dir))
            {
                System.Security.AccessControl.AuthorizationRuleCollection rules;
                if (File.Exists(this.DataSourcePath))
                {
                    rules = File.GetAccessControl(this.DataSourcePath).GetAccessRules(true, true,
                                typeof(System.Security.Principal.SecurityIdentifier));

                }
                else
                {
                    if (this.access_key == "")
                        throw new Exception("Empty acess key");

                    rules = Directory.GetAccessControl(this.data_dir).GetAccessRules(true, true,
                          typeof(System.Security.Principal.SecurityIdentifier));
                }

                foreach (System.Security.AccessControl.FileSystemAccessRule rule in rules)
                {
                    if ((System.Security.AccessControl.FileSystemRights.Write & rule.FileSystemRights) != System.Security.AccessControl.FileSystemRights.Write)
                        continue;

                    if (rule.AccessControlType == System.Security.AccessControl.AccessControlType.Allow)
                        writeAllow = true;

                }
                if (!writeAllow)
                    throw new Exception("Error. Cannot write to data dir.");
            }
        }
    }
}
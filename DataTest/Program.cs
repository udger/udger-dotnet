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
            //Stream stream;
            //StreamReader reader;

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

            #region test UA
            jsonString = File.ReadAllText(tempPath + "test_ua.json");
            jsonResult = JsonConvert.DeserializeObject(jsonString);

            foreach (dynamic x in jsonResult)
            {
                Console.WriteLine("test UA: " + x.test.teststring);
                parser.ua = x.test.teststring;
                parser.parse();
                a = parser.userAgent;
                
            }
            #endregion

        }
    }
}

/*
  UdgerParser - Test - Local parser lib 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
 
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
*/


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Udger.Parser;


namespace ConsoleTest
{
    class Program
    {

        static void Main(string[] args)
        {            

            Udger.Parser.UserAgent a;
            Udger.Parser.IPAddress i;
            List<Udger.Parser.UserAgent> retsua = new List<UserAgent>();
            List<Udger.Parser.IPAddress> retsip = new List<IPAddress>();
            // Create a new UdgerParser object
            UdgerParser parser = new UdgerParser();

            // Set data dir (in this directory is stored data file: udgerdb_v3.dat)
            // Data file can be downloaded manually from http://data.udger.com/, but we recommend use udger-updater
            parser.SetDataDir(@"C:\tmp");
            //parser.SetDataDir(@"C:\udger", "udgerdb_v3-noip.dat ");

            // Set user agent and /or IP address
            //parser.ua =@"Mozilla/5.0 (Linux; Android 4.4.4; eZee' Tab10010-S Build/KTU84Q) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/33.0.0.0 Safari/537.36";//@"Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Mobile/13E238";
            //parser.ip = "108.61.199.93";
          
            Console.WriteLine("start ua" + DateTime.Now.ToString("h:mm:ss.ffffff tt"));
            using (StreamReader r = new StreamReader("test_ua.json"))
            {
                string json = r.ReadToEnd();
                JArray stuff = JsonConvert.DeserializeObject<JArray>(json);
                var test = stuff.SelectToken("test");
                foreach (JToken jt in stuff)
                {
                    var s = jt["test"]["teststring"];
                    parser.ua = s.ToString();
                    parser.parse();
                    retsua.Add(parser.userAgent);
                }
                //while(stuff.nec)

            }
            Console.WriteLine("end ua" + DateTime.Now.ToString("h:mm:ss.ffffff tt"));

            Console.WriteLine("start ip" + DateTime.Now.ToString("h:mm:ss.ffffff tt"));
            using (StreamReader r = new StreamReader("test_ip.json"))
            {
                string json = r.ReadToEnd();
                JArray stuff = JsonConvert.DeserializeObject<JArray>(json);
                var test = stuff.SelectToken("test");
                foreach (JToken jt in stuff)
                {
                    var s = jt["test"]["teststring"];
                    parser.ip = s.ToString();
                    parser.parse();
                    retsip.Add(parser.ipAddress);
                }
                //while(stuff.nec)

            }
            Console.WriteLine("end ip" + DateTime.Now.ToString("h:mm:ss.ffffff tt"));
            Console.ReadKey();

        }

        
    }
}

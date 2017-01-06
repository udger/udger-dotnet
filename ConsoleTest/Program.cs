/*
  UdgerParser - Test - Local parser lib 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
 
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
*/


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

            // Create a new UdgerParser object
            UdgerParser parser = new UdgerParser();
            // orCreate and set LRU Cache capacity
            //UdgerParser parser = new UdgerParser(5000);

            // Set data dir (in this directory is stored data file: udgerdb_v3.dat)
            // Test data file is available on:  https://github.com/udger/test-data/tree/master/data_v3
            // Full data file can be downloaded manually from http://data.udger.com/, but we recommend use udger-updater
            parser.SetDataDir(@"C:\udger");
            // or set data dir and DB filename
            //parser.SetDataDir(@"C:\udger", "udgerdb_v3-noip.dat ");

            // Set user agent and /or IP address
            parser.ua = @"Mozilla/5.0 (compatible; SeznamBot/3.2; +http://fulltext.sblog.cz/)";
            parser.ip = "77.75.74.35";

            // Parse
            parser.parse();
            
            // Get information 
            a = parser.userAgent;
            i = parser.ipAddress;

            // Set user agent and /or IP address
            parser.ua = @"Mozilla/5.0 (Linux; U; Android 4.0.4; sk-sk; Luna TAB474 Build/LunaTAB474) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30";
            parser.parse();
            a = parser.userAgent;


            parser.ip = "2a02:598:111::9";
            parser.parse();
            i = parser.ipAddress;

        }


    }
}

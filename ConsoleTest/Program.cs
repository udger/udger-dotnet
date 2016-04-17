/*
  UdgerParser - Test - Local parser lib 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
 
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
*/

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using Udger.Parser;
using System.Collections.Generic;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {            

            Udger.Parser.UserAgent a;
            Udger.Parser.IPAddress i;

            // create a new UdgerParser object
            UdgerParser parser = new UdgerParser(); 

            // set data dir (this program must right write to cache dir)
            parser.SetDataDir(@"C:\tmp");
            // set You Accesskey (see https://udger.com/account/main) 
            //parser.SetAccessKey("XXXXXXX");
            // or download the datafile manually from https://data.udger.com/

            // set user agent and /or IP address
            parser.ua = @"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:40.0) Gecko/20100101 Firefox/40.0";
            parser.ip = "108.61.199.93";
            
            // parse
            parser.parse();
            // get information 
            a = parser.userAgent;
            i = parser.ipAddress;


            // set user agent and /or IP address
            parser.ua = @"/5.0 (compatible; SeznamBot/3.2; +http://fulltext.sblog.cz/)";
            parser.ip = "2001:41d0:8:d950:0:0:0:1";
            parser.parse();
            a = parser.userAgent;            
            i = parser.ipAddress;

            // set user agent and /or IP address
            parser.ua = @"Mozilla/5.0 (iPad; CPU OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53\";
            parser.ip = "2001:41d0:8:d950::1";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;

            // set user agent and /or IP address
            parser.ip = "66.249.64.73";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;

            // set user agent and /or IP address
            parser.ua = @"Mozilla/5.0 (Playstation Vita 1.61) AppleWebKit/531.22.8 (KHTML, like Gecko) Silk/3.2";
            parser.ip = "90.177.52.111";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;
            
        }

        
    }
}

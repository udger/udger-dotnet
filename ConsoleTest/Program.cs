/*
  UdgerParser - Test - Local parser lib 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
 
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Udger.Parser;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a new UdgerParser object
            UdgerParser parser = new UdgerParser(true);  // Development/Debug - debug info output to console
            //UdgerParser parser = new UdgerParser(); // Production

            // set data dir (this program must right write to cache dir)
            parser.SetDataDir(@"C:\tmp");

            // set You Acceskey (see http://udger.com/account/main) 
            //parser.SetAccessKey("XXXXXX");
            // or download the datafile manually from http://udger.com/resources/download
            
            //If you want information about fragments
            parser.SetParseFragments(false); //for future use, now has no meaning


            //method "parse"
            var useragent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; de) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/8888 Chrome anonymized by Abelssoft 1085275131";
            // Gets information about the user agent
            Dictionary<string, object> aa = parser.parse(useragent);

            //method "isBot"
            useragent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
            //var ip = "100.43.81.130";
            Dictionary<string, object> bb = parser.isBot(useragent);
            useragent = "Mozilla/4.0 (compatible; MSIE ; Windows NT 6.0)";
            bb = parser.isBot(useragent);

            //method "account"
            Dictionary<string, object> cc = parser.account();
            

        }
    }
}

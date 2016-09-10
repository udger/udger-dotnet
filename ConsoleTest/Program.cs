/*
  UdgerParser - Test - Local parser lib 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
 
  license    GNU Lesser General Public License
  link       http://udger.com/products/local_parser
*/


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

            // Set data dir (in this directory is stored data file: udgerdb_v3.dat)
            // Data file can be downloaded manually from http://data.udger.com/, but we recommend use udger-updater
            parser.SetDataDir(@"C:\udger");
            //parser.SetDataDir(@"C:\udger", "udgerdb_v3-noip.dat ");

            // Set user agent and /or IP address
            parser.ua = @"Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Mobile/13E238";
            parser.ip = "108.61.199.93";
            
            // Parse
            parser.parse();
            // Get information 
            a = parser.userAgent;
            i = parser.ipAddress;

            
            parser.ua = @"Mozilla/5.0 (compatible; SeznamBot/3.2; +http://fulltext.sblog.cz/)";
            parser.ip = "2001:41d0:8:d950:0:0:0:1";
            parser.parse();
            a = parser.userAgent;            
            i = parser.ipAddress;

                        
            parser.ua = @"'&lorem>>'adasdad asd ";
            parser.ip = "lorem'bla?&";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;

            parser.ua = @"Mozilla/5.0 (iPad; CPU OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53";
            parser.ip = "2001:41d0:8:d950::1";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;

            
            parser.ip = "66.249.64.73";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;

           
            parser.ua = @"Mozilla/5.0 (Playstation Vita 1.61) AppleWebKit/531.22.8 (KHTML, like Gecko) Silk/3.2";
            parser.ip = "90.177.52.111";
            parser.parse();
            a = parser.userAgent;
            i = parser.ipAddress;
            
        }

        
    }
}

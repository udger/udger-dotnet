# Udger client for .NET (data ver. 3)
Local parser is very fast and accurate useragent string detection solution. Enables developers to locally install and integrate a highly-scalable product.
We provide the detection of the devices (personal computer, tablet, Smart TV, Game console etc.), operating system and client SW type (browser, e-mail client etc.).
It also provides information about IP addresses (Public proxies, VPN services, Tor exit nodes, Fake crawlers, Web scrapers .. etc.)

### Requirements
- .NET Framework 4 or later.
- ADO.NET Data Provider for SQLite (included)

###Features
- Fast
- Written in C#
- Auto updated datafile and cache from remote server with version checking and checksum datafile
- Released under the GNU (LGPL v.3)


### Usage
You should review the included example (`ConsoleTest\Program.cs`)
Here's a quick example:

```csharp
UdgerParser parser = new UdgerParser();
parser.SetDataDir(@"C:\tmp");
//parser.SetAccessKey("XXXXXX");
// set user agent and /or IP address
parser.ua = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
parser.ip = "2600:3c01::f03c:91ff:fe70:9208";
// parse
parser.parse();
Udger.Parser.UserAgent a = parser.userAgent;
Udger.Parser.IPAddress i = parser.ipAddress;
```

### Author
The Udger.com Team (info@udger.com)

### old v2 format
If you still use the previous format of the db (v2), please see the branch old_format_v2
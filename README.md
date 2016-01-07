# Udger client for .NET
Local parser is very fast and accurate useragent string detection solution. Enables developers to locally install and integrate a highly-scalable product.
We provide the detection of the devices (personal computer, tablet, Smart TV, Game console etc.), operating system and client SW type (browser, e-mail client etc.).

### Requirements
.NET Framework 4 or later.
ADO.NET Data Provider for SQLite (included)
Json.NET (included)

###Features
Fast
Written in C#
Auto updated datafile and cache from remote server with version checking and checksum datafile
Released under the GNU (LGPL v.3)


### Usage
You should review the included example (`ConsoleTest\Program.cs`)
Here's a quick example:

```csharp
UdgerParser parser = new UdgerParser(true);  // Development/Debug - debug info output to console
parser.SetDataDir(@"C:\tmp");
//parser.SetAccessKey("XXXXXX");
var useragent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/601.3.9 (KHTML, like Gecko) Version/9.0.2 Safari/601.3.9";
Dictionary<string, object> res = parser.parse(useragent);
```

### Author
The Udger.com Team (info@udger.com)
// <copyright file="UdgerParser.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// UdgerParser - Java agent string parser based on Udger https://udger.com/products/local_parser
    /// </summary>
    public class UdgerParser : IDisposable
    {
        private const string DBFILENAME = "udgerdb_v3.dat";
        private const string UDGERUADEVBRANDLISTURL = "https://udger.com/resources/ua-list/devices-brand-detail?brand=";
        private const string IDCRAWLER = "crawler";
        private const string PATUNPERLIZE = "^/?(.*?)/si$";

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

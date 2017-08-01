// <copyright file="CacheEntry.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Udger.Parser.V3.Models;

    /// <summary>
    /// Implementation of the cache entry.
    /// </summary>
    public class CacheEntry
    {
        private const long SerialVersionUID = -2815264316130381309L;

        /// <summary>
        /// Gets or sets previous cache entry.
        /// </summary>
        public CacheEntry Previous { get; set; }

        /// <summary>
        /// Gets or sets next cache entry.
        /// </summary>
        public CacheEntry Next { get; set; }

        /// <summary>
        /// Gets or sets user agent string.
        /// </summary>
        public string UaString { get; set; }

        /// <summary>
        /// Gets or sets user agent result. <see cref="Udger.Parser.V3.Models.UserAgentResult"/>.
        /// </summary>
        public UserAgentResult UaResult { get; set; }
    }
}

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
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public class CacheEntry<TKey, TValue>
    {
        private const long SerialVersionUID = -2815264316130381309L;

        /// <summary>
        /// Gets or sets previous cache entry.
        /// </summary>
        public CacheEntry<TKey, TValue> Previous { get; set; }

        /// <summary>
        /// Gets or sets next cache entry.
        /// </summary>
        public CacheEntry<TKey, TValue> Next { get; set; }

        /// <summary>
        /// Gets or sets user agent string.
        /// </summary>
        public TKey UaString { get; set; }

        /// <summary>
        /// Gets or sets user agent result. <see cref="Udger.Parser.V3.Models.UserAgentResult"/>.
        /// </summary>
        public TValue UaResult { get; set; }
    }
}

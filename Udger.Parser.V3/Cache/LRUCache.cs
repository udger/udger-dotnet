// <copyright file="LRUCache.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Implementation of LRU cache with generic key and value types.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TValue">Type of value.</typeparam>
    public class LRUCache<TKey, TValue> : ICache<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        /// <summary>
        /// Constant for minimum viable cache size.
        /// </summary>
        public const int MinimumCacheSize = 1024;

        /// <summary>
        /// Field for internal cache dictionary.
        /// </summary>
        private Dictionary<TKey, TValue> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="LRUCache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">Capacity of the cache.</param>
        public LRUCache(int capacity)
        {
            if (capacity < MinimumCacheSize)
            {
                capacity = MinimumCacheSize;
            }

            this.items = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Gets the value from the cache regarding the passed key.
        /// </summary>
        /// <param name="key">The key to look for in the cache.</param>
        /// <returns>Returns instance of the item if exists, otherwise returns null.</returns>
        public TValue Pull(TKey key)
        {
            if (this.items.ContainsKey(key))
            {
                return this.items[key];
            }

            return null;
        }

        /// <summary>
        /// Pushes the value to the cache under specific key.
        /// </summary>
        /// <param name="key">The key to use for the identifier in the cache.</param>
        /// <param name="value">The value to store in the cache.</param>
        /// <remarks>
        /// If the key in the cache exists it's value is replaced.
        /// If the key does not exist the new entry is created.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="key"/> or <paramref name="value"/> is null.</exception>
        public void Push(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}

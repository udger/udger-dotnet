// <copyright file="LRUCache.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Cache
{
    using System;
    using System.Collections.Generic;

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

        private CacheEntry<TKey, TValue> head;
        private CacheEntry<TKey, TValue> tail;
        private int capacity;

        /// <summary>
        /// Field for internal cache dictionary.
        /// </summary>
        private Dictionary<TKey, CacheEntry<TKey, TValue>> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="LRUCache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">Capacity of the cache.</param>
        public LRUCache(int capacity)
        {
            this.capacity = (capacity < MinimumCacheSize) ? MinimumCacheSize : capacity;
            this.items = new Dictionary<TKey, CacheEntry<TKey, TValue>>(this.capacity);
        }

        /// <summary>
        /// Gets the value from the cache regarding the passed key.
        /// </summary>
        /// <param name="key">The key to look for in the cache.</param>
        /// <returns>Returns instance of the item if exists, otherwise returns null.</returns>
        public TValue Get(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var node = this.items[key];
            if (node == null)
            {
                return null;
            }

            if (this.head != node)
            {
                if (node.Next != null)
                {
                    node.Next.Previous = node.Previous;
                }
                else
                {
                    this.tail = node.Previous;
                }

                node.Previous.Next = node.Next;
                this.head.Previous = node;
                node.Next = this.head;
                node.Previous = null;
                this.head = node;
            }

            return node.UaResult;
        }

        /// <summary>
        /// Removes all items from cache.
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
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
        public void Put(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var node = this.items[key];
            if (node == null)
            {
                node = new CacheEntry<TKey, TValue>();
                node.UaString = key;
                node.UaResult = value;
                node.Next = this.head;
                node.Previous = null;
                if (this.head != null)
                {
                    this.head.Previous = node;
                }

                if (this.tail == null)
                {
                    this.tail = this.head;
                }

                this.head = node;
                this.items.Add(key, node);
                if ((this.items.Count > this.capacity) && (this.tail != null))
                {
                    this.items.Remove(this.tail.UaString);
                    this.tail.Previous = this.tail.Previous;
                    this.tail.Next = null;
                }
            }

            node.UaResult = value;
        }
    }
}

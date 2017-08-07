// <copyright file="ICache.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Cache
{
    /// <summary>
    /// Interface which must be implemented by every caching implementation.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TValue">Type of value.</typeparam>
    public interface ICache<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        /// <summary>
        /// Gets the value from the cache regarding the passed key.
        /// </summary>
        /// <param name="key">The key to look for in the cache.</param>
        /// <returns>Returns instance of the item if exists, otherwise returns null.</returns>
        TValue Get(TKey key);

        /// <summary>
        /// Pushes the value to the cache under specific key.
        /// </summary>
        /// <param name="key">The key to use for the identifier in the cache.</param>
        /// <param name="value">The value to store in the cache.</param>
        /// <remarks>
        /// If the key in the cache exists it's value is replaced.
        /// If the key does not exist the new entry is created.
        /// </remarks>
        void Put(TKey key, TValue value);

        /// <summary>
        /// Removes all items from cache.
        /// </summary>
        void Clear();
    }
}

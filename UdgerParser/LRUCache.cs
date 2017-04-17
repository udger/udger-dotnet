/*
  UdgerParser - Local parser lib
  
  UdgerParser class parses useragent strings based on a database downloaded from udger.com
 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
  license    GNU Lesser General Public License
  link       https://udger.com/products/local_parser
 */

using System;
using System.Collections.Generic;

namespace Udger.Parser
{
    /// <summary>
    /// Class implementing cache of the <see cref="Node{TKey, TValue}"/> for parsing.
    /// </summary>
    /// <typeparam name="TKey">Type of key in dictionary.</typeparam>
    /// <typeparam name="TValue">Type of the value store under the key.</typeparam>
    class LRUCache<TKey,TValue> where TKey: class
        where TValue: class 
    {
        /// <summary>
        /// Minimum capacity of the cache. This value is set to 1024 entries in the cache. 
        /// </summary>
        private const uint MinimumCacheCapacity = 1024;

        private readonly Dictionary<TKey, Node<TKey, TValue>> entries;
        private readonly int capacity;
        private Node<TKey, TValue> head;
        private Node<TKey, TValue> tail;

        #region Constructor
        public LRUCache(uint capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(
                    "capacity",
                    "Capacity should be greater than zero");
            this.capacity = capacity;
            entries = new Dictionary<TKey, Node<>>();
            head = null;
        }
        #endregion

        #region public method    
        public void Set(TKey key, TValue value)
        {
            Node entry;
            if (!entries.TryGetValue(key, out entry))
            {
                entry = new Node { Key = key, Value = value };
                if (entries.Count == capacity)
                {
                    entries.Remove(tail.Key);
                    tail = tail.Previous;
                    if (tail != null)
                        tail.Next = null;
                }
                entries.Add(key, entry);
            }

            entry.Value = value;
            MoveToHead(entry);
            if (tail == null)
                tail = head;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            Node entry;
            if (!entries.TryGetValue(key, out entry))
                return false;

            MoveToHead(entry);
            value = entry.Value;

            return true;
        }
        #endregion

        #region private method
        private void MoveToHead(Node entry)
        {
            if (entry == head || entry == null)
                return;

            var next = entry.Next;
            var previous = entry.Previous;

            if (next != null)
                next.Previous = entry.Previous;

            if (previous != null)
                previous.Next = entry.Next;

            entry.Previous = null;
            entry.Next = head;

            if (head != null)
                head.Previous = entry;

            head = entry;

            if (tail == entry)
                tail = previous;
        }
        #endregion
    }
}

namespace Udger.Parser
{
    /// <summary>
    /// Impelmentation of the cache node item
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal class Node<TKey, TValue> where TKey: class 
        where TValue: class
    {
        /// <summary>
        /// Gets or sets next node in the cache.
        /// </summary>
        public Node<TKey, TValue> Next { get; set; }

        /// <summary>
        /// Gets or sets previous node in the cache.
        /// </summary>
        public Node<TKey, TValue> Previous { get; set; }

        /// <summary>
        /// Gets or sets node key (identifier).
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets node value.
        /// </summary>
        public TValue Value { get; set; }
    }
}

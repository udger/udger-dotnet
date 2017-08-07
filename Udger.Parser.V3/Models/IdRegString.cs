// <copyright file="IdRegString.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Models
{
    /// <summary>
    /// Class implementing id registration string.
    /// </summary>
    internal class IdRegString
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets word if first.
        /// </summary>
        public int WordId1 { get; set; }

        /// <summary>
        /// Gets or sets word id second.
        /// </summary>
        public int WordId2 { get; set; }

        /// <summary>
        /// Gets or sets match pattern.
        /// </summary>
        public string Pattern { get; set; }
    }
}

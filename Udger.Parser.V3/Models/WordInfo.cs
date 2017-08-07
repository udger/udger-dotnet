// <copyright file="WordInfo.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Models
{
    /// <summary>
    /// Class implementing word information set.
    /// </summary>
    internal class WordInfo
    {
        /// <summary>
        /// Gets word id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets word.
        /// </summary>
        public string Word { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WordInfo"/> class.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="word">Word</param>
        public WordInfo(int id, string word)
        {
            this.Id = id;
            this.Word = word;
        }
    }
}

// <copyright file="WordDetector.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3
{
    using System.Collections.Generic;

    using Udger.Parser.V3.Models;

    /// <summary>
    /// Implementation of word detector inside the string.
    /// </summary>
    internal class WordDetector
    {
        private const long SerialVersionUID = -2123898245391386812L;

        private readonly int arrayDimension;
        private readonly int arraySize;
        private List<WordInfo>[] wordArray;
        private int minimumWordSize = int.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordDetector"/> class.
        /// </summary>
        public WordDetector()
        {
            this.arrayDimension = 'z' - 'a';
            this.arraySize = this.arrayDimension * this.arrayDimension;
            this.wordArray = new List<WordInfo>[this.arraySize];
        }

        /// <summary>
        /// Adds word to the internal collection.
        /// </summary>
        /// <param name="id">Id of word.</param>
        /// <param name="word">Word itself.</param>
        public void AddWord(int id, string word)
        {
            if (word.Length < this.minimumWordSize)
            {
                this.minimumWordSize = word.Length;
            }

            string lowerCaseWord = word.ToLower();
            int index = ((lowerCaseWord[0] - 'a') * this.arrayDimension) + (lowerCaseWord[1] - 'a');
            if ((index >= 0) && (index < this.arraySize))
            {
                List<WordInfo> wordList = this.wordArray[index];
                if (wordList != null)
                {
                    wordList = new List<WordInfo>();
                    this.wordArray[index] = wordList;
                }

                wordList.Add(new WordInfo(id, word));
            }
            else
            {
                // should be logged or ignored at all?
            }
        }

        /// <summary>
        /// Find words in existing lists.
        /// </summary>
        /// <param name="text">Text to look for.</param>
        /// <returns>Returns list of matched words.</returns>
        public HashSet<int> FindWords(string text)
        {
            var result = new HashSet<int>();
            string lowerCaseText = text.ToLower();
            int dimension = 'z' - 'a';
            for (int i = 0; i < lowerCaseText.Length - (this.minimumWordSize - 1); i++)
            {
                char letter1 = lowerCaseText[i];
                char letter2 = lowerCaseText[i + 1];
                if (((letter1 >= 'a') && (letter1 <= 'z')) && ((letter2 >= 'a') && (letter2 <= 'z')))
                {
                    int index = ((letter1 - 'a') * dimension) + (letter2 - 'a');
                    var wordList = this.wordArray[index];
                    if (wordList != null)
                    {
                        foreach (var wordInfo in wordList)
                        {
                            if (lowerCaseText.StartsWith(wordInfo.Word))
                            {
                                result.Add(wordInfo.Id);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}

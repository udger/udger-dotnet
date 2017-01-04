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
    class WordDetector
    {   

        struct WordInfo
        {
            public int id { get; }
            public String word { get; }

            public WordInfo(int id, String word)
            {
                this.id = id;
                this.word = word;
            }
        }

        private static readonly int ARRAY_DIMENSION = 'z' - 'a';
        private static readonly int ARRAY_SIZE = (ARRAY_DIMENSION + 1) * (ARRAY_DIMENSION + 1);

        private List<WordInfo>[] wordArray;
        private int minWordSize = Int32.MaxValue;

        public WordDetector()
        {
            wordArray = new List<WordInfo>[ARRAY_SIZE];
        }

        public void addWord(int id, String word)
        {

            if (word.Length < minWordSize)
            {
                minWordSize = word.Length;
            }

            String s = word.ToLower();
            int index = (s[0] - 'a') * ARRAY_DIMENSION + s[1] - 'a';
            if (index >= 0 && index < ARRAY_SIZE)
            {
                List<WordInfo> wList = wordArray[index];
                if (wList == null)
                {
                    wList = new List<WordInfo>();
                    wordArray[index] = wList;
                }
                wList.Add(new WordInfo(id, s));
            }
        }

        public HashSet<int> findWords(String text)
        {

            HashSet<int> ret = new HashSet<int>();

            String s = text.ToLower();
            int dimension = 'z' - 'a';
            for (int i = 0; i < s.Length - (minWordSize - 1); i++)
            {
                char c1 = s[i];
                char c2 = s[i + 1];
                if (c1 >= 'a' && c1 <= 'z' && c2 >= 'a' && c2 <= 'z')
                {
                    int index = (c1 - 'a') * dimension + c2 - 'a';
                    List<WordInfo> l = wordArray[index];
                    if (l != null)
                    {
                        foreach (WordInfo wi in l)
                        {
                            if (s.Substring(i).StartsWith(wi.word))
                            {
                                ret.Add(wi.id);
                            }
                        }
                    }
                }
            }
            return ret;
        }

    }
}


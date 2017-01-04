/*
  UdgerParser - Local parser lib
  
  UdgerParser class parses useragent strings based on a database downloaded from udger.com
 
 
  author     The Udger.com Team (info@udger.com)
  copyright  Copyright (c) Udger s.r.o.
  license    GNU Lesser General Public License
  link       https://udger.com/products/local_parser
 */

using System;

namespace Udger.Parser
{

    [AttributeUsage(AttributeTargets.Property)]
    public class NamePositionAttribute : Attribute
    {
        public readonly int Position;
        public string Name;

        public NamePositionAttribute(int position)
        {
            Position = position;
        }
    }
    
}

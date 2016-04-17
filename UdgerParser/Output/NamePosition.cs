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

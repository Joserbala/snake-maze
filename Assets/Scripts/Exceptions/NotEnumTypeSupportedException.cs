using System;

namespace SnakeMaze.Exceptions
{
    public class NotEnumTypeSupportedException : Exception
    {
        public NotEnumTypeSupportedException() : base(String.Format("Enum value not supported")) { }
    }
}
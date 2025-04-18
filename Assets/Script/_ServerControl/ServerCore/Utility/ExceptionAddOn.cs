using System;

namespace ServerCore.Utility
{
    [Serializable]
    public class WriteBytesException : Exception
    {
        public WriteBytesException() : base() { }
        public WriteBytesException(string message) : base(message) { }
        public WriteBytesException(string message, Exception inner) : base(message, inner) { }
    }
}
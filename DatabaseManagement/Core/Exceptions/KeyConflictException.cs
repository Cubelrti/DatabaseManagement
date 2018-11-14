using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    public class KeyConflictException : Exception
    {
        public KeyConflictException()
        {
        }

        public KeyConflictException(string message) : base(message)
        {
        }

        public KeyConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
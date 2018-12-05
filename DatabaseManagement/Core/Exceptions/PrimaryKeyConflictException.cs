using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    public class PrimaryKeyConflictException : Exception
    {
        public PrimaryKeyConflictException()
        {
        }

        public PrimaryKeyConflictException(string message) : base(message)
        {
        }

        public PrimaryKeyConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PrimaryKeyConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
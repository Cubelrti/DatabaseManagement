using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    public class DatabaseConflictException : Exception
    {
        public DatabaseConflictException()
        {
        }

        public DatabaseConflictException(string message) : base(message)
        {
        }

        public DatabaseConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
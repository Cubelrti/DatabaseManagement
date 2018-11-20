using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    internal class TableConflictException : Exception
    {
        public TableConflictException()
        {
        }

        public TableConflictException(string message) : base(message)
        {
        }

        public TableConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TableConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
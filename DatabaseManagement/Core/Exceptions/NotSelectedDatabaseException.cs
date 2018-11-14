using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    internal class NotSelectedDatabaseException : Exception
    {
        public NotSelectedDatabaseException()
        {
        }

        public NotSelectedDatabaseException(string message) : base(message)
        {
        }

        public NotSelectedDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSelectedDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
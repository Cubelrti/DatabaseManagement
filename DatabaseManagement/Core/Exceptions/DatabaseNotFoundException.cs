using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    internal class DatabaseNotFoundException : Exception
    {
        public DatabaseNotFoundException()
        {
        }

        public DatabaseNotFoundException(string message) : base(message)
        {
        }

        public DatabaseNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
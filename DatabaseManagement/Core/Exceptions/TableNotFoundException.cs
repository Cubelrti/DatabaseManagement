using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    public class TableNotFoundException : Exception
    {
        public TableNotFoundException()
        {
        }

        public TableNotFoundException(string message) : base(message)
        {
        }

        public TableNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TableNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
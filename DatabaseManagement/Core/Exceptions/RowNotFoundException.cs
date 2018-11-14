using System;
using System.Runtime.Serialization;

namespace DatabaseManagement.Core
{
    [Serializable]
    public class RowNotFoundException : Exception
    {
        public RowNotFoundException()
        {
        }

        public RowNotFoundException(string message) : base(message)
        {
        }

        public RowNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RowNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
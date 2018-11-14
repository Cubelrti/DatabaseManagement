using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Entities
{
    public enum Types
    {
        VARCHAR,
        INTEGER,
        DOUBLE,
        DATE,
        CHAR
    }
    public class Row<T>
    {


        public Types Type { get; set; } // maybe not needed

        public string name;

        public T value;

    }
}

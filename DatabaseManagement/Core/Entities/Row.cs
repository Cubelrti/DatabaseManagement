using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Entities
{
    class Row
    {
        Dictionary<String, String> columns = new Dictionary<string, string>();
        public string toString()
        {
            string builder = "";
            foreach (var item in columns)            {
                builder += (item.Value + ' ');
            }
            return builder;
        }
    }
}

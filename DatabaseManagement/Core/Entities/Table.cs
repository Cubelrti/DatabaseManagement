using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Entities
{
    [Serializable]
    public class Table
    {
        public string name;
        public Dictionary<string, Types> ColumnDefinitions;
        public List<Row> rows = new List<Row>();
    }
}

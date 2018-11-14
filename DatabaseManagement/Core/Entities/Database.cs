using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Entities
{
    [Serializable]
    public class Database
    {
        public string name;
        public List<Table> tables = new List<Table>();
        public Database()
        {

        }
    }
}

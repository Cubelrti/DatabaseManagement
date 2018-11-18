using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    class Adapter : Intefaces.Database
    {
        public void CreateDatabase(Executor expr)
        {
            throw new NotImplementedException();
        }

        public void CreateTable(Executor expr)
        {
            throw new NotImplementedException();
        }

        public void Delete(Executor expr)
        {
            throw new NotImplementedException();
        }

        public void DropDatabases(Executor expr)
        {
            throw new NotImplementedException();
        }

        public void DropTable(Executor expr)
        {
            throw new NotImplementedException();
        }

        public void Insert(Executor expr)
        {
            throw new NotImplementedException();
        }

        public List<string> Select(Executor expr)
        {
            throw new NotImplementedException();
        }

        public List<string> ShowDatabases(Executor expr)
        {
            throw new NotImplementedException();
        }

        public List<string> ShowTables(Executor expr)
        {
            throw new NotImplementedException();
        }

        public void Update(Executor expr)
        {
            throw new NotImplementedException();
        }
    }
}

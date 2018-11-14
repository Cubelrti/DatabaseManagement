using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    class Adapter : Intefaces.Database
    {
        public void CreateDatabase(Expr expr)
        {
            throw new NotImplementedException();
        }

        public void CreateTable(Expr expr)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expr expr)
        {
            throw new NotImplementedException();
        }

        public void DropDatabases(Expr expr)
        {
            throw new NotImplementedException();
        }

        public void DropTable(Expr expr)
        {
            throw new NotImplementedException();
        }

        public void Insert(Expr expr)
        {
            throw new NotImplementedException();
        }

        public List<string> Select(Expr expr)
        {
            throw new NotImplementedException();
        }

        public List<string> ShowDatabases(Expr expr)
        {
            throw new NotImplementedException();
        }

        public List<string> ShowTables(Expr expr)
        {
            throw new NotImplementedException();
        }

        public void Update(Expr expr)
        {
            throw new NotImplementedException();
        }
    }
}

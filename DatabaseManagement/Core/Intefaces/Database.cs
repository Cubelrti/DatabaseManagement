using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Intefaces
{
    public interface Database
    {
        void CreateDatabase(Expr expr);
        List<string> ShowDatabases(Expr expr);
        void DropDatabases(Expr expr);
        void CreateTable(Expr expr);
        List<string> ShowTables(Expr expr);
        void Insert(Expr expr);
        List<string> Select(Expr expr);
        void DropTable(Expr expr);
        void Delete(Expr expr);
        void Update(Expr expr);
    }
}

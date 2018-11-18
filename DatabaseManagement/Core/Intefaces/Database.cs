using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Intefaces
{
    public interface Database
    {
        void CreateDatabase(Executor expr);
        List<string> ShowDatabases(Executor expr);
        void DropDatabases(Executor expr);
        void CreateTable(Executor expr);
        List<string> ShowTables(Executor expr);
        void Insert(Executor expr);
        List<string> Select(Executor expr);
        void DropTable(Executor expr);
        void Delete(Executor expr);
        void Update(Executor expr);
    }
}

using DatabaseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    class Main
    {
        List<Database> databases;
        Database _current;
        Database CreateDatabase(string name)
        {
            return new Database { name = name };
        }
        void DropDatabase(string name)
        {
            var _remove = databases.Find(db => db.name == name);
            if (_remove == null)
            {
                throw new DatabaseNotFoundException();
            }
            databases.Remove(_remove);
        }
        void CreateTable(string name, List<object> rows)
        {
            if (_current == null)
            {
                throw new NotSelectedDatabaseException();
            }
            _current.tables.Add(new Table { name = name, rows = rows });
        }
        void insertTable(string into, string key, string value)
        {
            if (_current == null)
            {
                throw new NotSelectedDatabaseException();
            }
            var _table = _current.tables.Find(t => t.name == into);
            if (_table == null)
            {
                throw new TableNotFoundException();
            }
            
            switch (key)
            {
                case "VARCHAR":
                    _table.rows.Add(new Row<string> { name = key, value = value });
                    break;
                case "INTEGER":
                    _table.rows.Add(new Row<int> { name = key, value = Int32.Parse(value) });
                    break;
                case "DATE":
                    _table.rows.Add(new Row<DateTime> { name = key, value = DateTime.Parse(value)});
                    break;
                case "DOUBLE":
                    _table.rows.Add(new Row<double> { name = key, value = Double.Parse(value) });
                    break;
                default:
                    throw new UnsupportedTypeException();
            }
        }
        List<object> selectRow(string tableName, List<string> predicates)
        {
            var table = _current.tables.Find(tb => tb.name == tableName);
            if (table == null)
                throw new TableNotFoundException();



            throw new NotImplementedException();
        }
        void selectDatabase(string name)
        {
            _current = databases.Find(db => db.name == name);
        }
    }
}

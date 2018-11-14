using DatabaseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    public class Main
    {
        public List<Database> databases = new List<Database>();
        public Database _current;
        public void CreateDatabase(string name)
        {
            databases.Add( new Database { name = name });
        }
        public void DropDatabase(string name)
        {
            var _remove = databases.Find(db => db.name == name);
            if (_remove == null)
            {
                throw new DatabaseNotFoundException();
            }
            databases.Remove(_remove);
        }
        public void CreateTable(string name, Dictionary<string, Types> constraints)
        {
            if (_current == null)
            {
                throw new NotSelectedDatabaseException();
            }
            _current.tables.Add(new Table { name = name, constraints = constraints });
            
        }
        public void insertTable(string into, string key, string value)
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
            
            if (!_table.constraints.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            switch (_table.constraints[key])
            {
                case Types.VARCHAR:
                    _table.rows.Add(new Row<string> { name = key, value = value });
                    break;
                case Types.INTEGER:
                    _table.rows.Add(new Row<int> { name = key, value = Int32.Parse(value) });
                    break;
                case Types.DATE:
                    _table.rows.Add(new Row<DateTime> { name = key, value = DateTime.Parse(value)});
                    break;
                case Types.DOUBLE:
                    _table.rows.Add(new Row<double> { name = key, value = Double.Parse(value) });
                    break;
                default:
                    throw new UnsupportedTypeException();
            }
        }

        public List<object> selectRow(string tableName, List<string> predicates)
        {
            var table = _current.tables.Find(tb => tb.name == tableName);
            if (table == null)
                throw new TableNotFoundException();

            throw new NotImplementedException();
        }
        public void selectDatabase(string name)
        {
            _current = databases.Find(db => db.name == name);
        }
    }
}

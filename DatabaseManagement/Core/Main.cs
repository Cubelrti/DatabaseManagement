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
            _current.tables.Add(new Table { name = name, ColumnDefinitions = constraints });
            
        }
        public void InsertTable(string into, Dictionary<string, string> defs)
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
            var _row = new Row();
            foreach (var item in defs)
            {
                var key = item.Key;
                if (!_table.ColumnDefinitions.ContainsKey(key))
                {
                    throw new KeyNotFoundException();
                }
                // pattern matching foreach-switch
                switch (_table.ColumnDefinitions[key])
                {
                    case Types.VARCHAR:
                        _row.keyValuePairs.Add(key, item.Value);
                        break;
                    case Types.INTEGER:
                        _row.keyValuePairs.Add(key, Int32.Parse(item.Value));
                        break;
                    case Types.DATE:
                        _row.keyValuePairs.Add(key, DateTime.Parse(item.Value));
                        break;
                    case Types.DOUBLE:
                        _row.keyValuePairs.Add(key, Double.Parse(item.Value));
                        break;
                    default:
                        throw new UnsupportedTypeException();
                }
            }
            _table.rows.Add(_row);
            
        }

        public List<Row> SelectRow(string tableName)
        {
            if (_current == null)
            {
                throw new NotSelectedDatabaseException();
            }
            var table = _current.tables.Find(tb => tb.name == tableName);
            if (table == null)
                throw new TableNotFoundException();
            return table.rows;
        }

        public void SetInnerRowsDirectly(string tableName, List<Row> rows)
        {
            // this method is gay
            if (_current == null)
            {
                throw new NotSelectedDatabaseException();
            }
            var table = _current.tables.Find(tb => tb.name == tableName);
            if (table == null)
                throw new TableNotFoundException();
            table.rows = rows;
        }

        public void SelectDatabase(string name)
        {
            _current = databases.Find(db => db.name == name);
        }
    }
}

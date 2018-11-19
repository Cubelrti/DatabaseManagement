using DatabaseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    public class Executor
    {
        public enum ExprType
        {
            CREATETABLE,
            CREATEDATABASE,
            USEDATABASE,
            SHOWDATABASE,
            SHOWTABLE,
            DROPTABLE,
            DROPDATABASE,
            UPDATE,
            SELECT,
            INSERT,
            DELETE,
        }
        public ExprType type;
        private List<string> tokens = new List<string>();
        private Main instance;
        public static Types ToType(string type)
        {
            switch (type)
            {
                case "varchar":
                    return Types.VARCHAR;
                case "varchar2":
                    return Types.VARCHAR;
                case "integer":
                    return Types.INTEGER;
                case "double":
                    return Types.DOUBLE;
                case "date":
                    return Types.DATE;
                case "char":
                    return Types.CHAR;
                default:
                    throw new UnsupportedTypeException();
            }
        }
        
        private Func<Row, bool> filterWhere(bool reverse = false)
        {
            var whereStartBy = tokens.FindIndex(s => s == "where");
            if (whereStartBy == -1)
            {
                return (row) => reverse ^ true;
            }
            var predicates = tokens.Skip(whereStartBy + 1).ToList();
            var lhs = predicates[0];
            return (row) =>
            {
                if (predicates[1] == "=")
                {
                    if (row.keyValuePairs[predicates[0]].ToString() == predicates[2])
                    {
                        return reverse ^ true;
                    }
                }
                return reverse ^ false;
            };
        }

        private string execute()
        {
            if (type == ExprType.USEDATABASE)
            {
                instance.SelectDatabase(tokens[1]);
            }
            if (type == ExprType.CREATEDATABASE)
            {
                instance.CreateDatabase(tokens[2]);
            }
            if (type == ExprType.CREATETABLE)
            {
                var name = tokens[2];
                var constraints = tokens.Skip(4)
                    .Split(s => s == ",")
                    .ToDictionary(expr => expr[0],  expr => ToType(expr[1]));
                instance.CreateTable(name, constraints);
            }
            if (type == ExprType.SHOWDATABASE)
            {
                return instance.databases.Select(db => db.name).Aggregate((a, b) => a + ", " + b);
            }
            if (type == ExprType.DROPDATABASE)
            {
                instance.DropDatabase(tokens[2]);
            }
            if (type == ExprType.DROPTABLE)
            {
                instance.DropTable(tokens[2]);
            }
            if (type == ExprType.SHOWTABLE)
            {
                return instance._current.tables.Select(db => db.name).Aggregate((a, b) => a + ", " + b);
            }
            if (type == ExprType.INSERT)
            {
                var into = tokens[2];
                var values = tokens.Skip(3)
                    .Where(v => v != "(" && v != ")" && v != "'")
                    .Split(s => s == "values")
                    .Select(li => li.Split(s => s == ","))
                    .ToList();
                var dict = values[0].Zip(values[1], (k, v) => new { k, v })
                    .ToDictionary(x => x.k[0], x => x.v[0]);
                instance.InsertTable(into, dict);
            }
            if (type == ExprType.SELECT)
            {
                if (tokens[1] == "*")
                {
                    // select any
                    var rows = instance.SelectAny(tokens[3])
                        .Where(filterWhere())
                        .Select(r => r.keyValuePairs);
                    var values = rows.Select(r => r.Values.Select(obj => obj.ToString()).ToList())
                        .Select(row => row.Aggregate((a, b) => a + ", " + b))
                        .Aggregate((a, b) => a + "\n" + b);
                    return values;
                } else
                {
                    // select row
                    var from = tokens.FindIndex(s => s == "from");
                    var columns = tokens.Take(from)
                        .Skip(1)
                        .Where(v => v != "(" && v != ")")
                        .Split(s => s == ",")
                        .Select(l => l[0]);
                    var rows = instance.SelectAny(tokens[from + 1])
                        .Where(filterWhere())
                        .Select(r => r.keyValuePairs);
                    var values = rows.Select(r => r.Where(kv => columns.Contains(kv.Key)).Select(obj => obj.Value.ToString()).ToList())
                        .Select(row => row.Aggregate((a, b) => a + ", " + b))
                        .Aggregate((a, b) => a + "\n" + b);
                    return values;
                }
            }
            if (type == ExprType.DELETE)
            {
                var from = tokens[2];
                var rows = instance.SelectAny(from)
                    .Where(filterWhere(true));
                instance.SetInnerRowsDirectly(from, rows.ToList());
            }
            return "ok";
        }

        private static List<string> FormatRaw(string rawStmt)
        {
            string formatted = rawStmt.ToLower();
            // replace newline, tab, enter with one space
            formatted = Regex.Replace(formatted, @"[\r\n\t]", " ");
            // remove ; and chars after ;
            formatted = Regex.Replace(formatted, @";.*$", "");
            // remove leading spaces and trailing spaces
            formatted = Regex.Replace(formatted, @"(^ +)|( +$)", "");
            // remove duplicate spaces
            formatted = Regex.Replace(formatted, @" +", " ");
            // insert space before or after ( ) , = <> < >
            formatted = Regex.Replace(formatted, @" ?(\(|\)|,|=|(<>)|<|>) ?", " $1 ");
            // remove space between comparison operators
            formatted = Regex.Replace(formatted, @"< +>", "<>");
            formatted = Regex.Replace(formatted, @"< +=", "<=");
            formatted = Regex.Replace(formatted, @"> +=", ">=");

            return formatted.Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToList();
        }
        public Executor(Main instance)
        {
            this.instance = instance;
        }
        public string Run(string statememt)
        {
            var expr = FormatRaw(statememt);
            tokens = expr;
            for (int i = 0; i < expr.Count; i++)
            {
                switch (expr[0])
                {
                    case "use":
                        type = ExprType.USEDATABASE;
                        break;
                    case "create":
                        if (expr[1] == "database")
                            type = ExprType.CREATEDATABASE;
                        if (expr[1] == "table")
                            type = ExprType.CREATETABLE;
                        break;
                    case "select":
                        type = ExprType.SELECT;
                        break;
                    case "show":
                        if (expr[1] == "database")
                            type = ExprType.SHOWDATABASE;
                        if (expr[1] == "table")
                            type = ExprType.SHOWTABLE;
                        break;
                    case "drop":
                        if (expr[1] == "database")
                            type = ExprType.DROPDATABASE;
                        if (expr[1] == "table")
                            type = ExprType.DROPTABLE;
                        break;
                    case "update":
                        type = ExprType.UPDATE;
                        break;
                    case "insert":
                        type = ExprType.INSERT;
                        break;
                    case "delete":
                        type = ExprType.DELETE;
                        break;
                    default:
                        throw new UnsupportedTypeException();
                }
            }
            return execute();
        }
        
    }
}

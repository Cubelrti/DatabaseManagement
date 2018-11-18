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
                    .ToList()
                    .ToDictionary(expr => expr[0],  expr => ToType(expr[1]));
                instance.CreateTable(name, constraints);
            }
            if (type == ExprType.SHOWDATABASE)
            {
                return instance.databases.Select(db => db.name).Aggregate((a, b) => a + ", " + b);
            }
            if (type == ExprType.SHOWTABLE)
            {
                return instance._current.tables.Select(db => db.name).Aggregate((a, b) => a + ", " + b);
            }
            return "ok";
            // TODO excute this expression
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

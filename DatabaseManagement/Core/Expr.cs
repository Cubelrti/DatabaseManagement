using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    public class Expr
    {
        public enum ExprType
        {
            CREATETABLE,
            CREATEDATABASE,
            SHOWDATABASE,
            SHOWTABLES,
            DROPTABLE,
            DROPDATABASE,
            UPDATE,
            SELECT,
            INSERT,
            DELETE,
        }
        public ExprType type;
        private List<string> tokens = new List<string>();
        
        public void compile(ExprType type, List<string> tokens)
        {
            Expr expr = new Expr();
            expr.tokens = tokens;
            expr.type = type;
        }
        private void excute()
        {
            // TODO excute this expression
        }
        private Expr()
        {
            // TODO for shen: Intepreter/Parser
            // parser write in here -- or invoke inported methods in Lexer.cs
        }

    }
}

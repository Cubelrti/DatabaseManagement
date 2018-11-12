using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    class Expr
    {
        public enum ExprType
        {
            CREATE,
            DROP,
            UPDATE,
            SELECT,
            INSERT,
            DELETE,
        }
        public ExprType type;
        private string lhs;
        private string rhs;
        
        public void compile(ExprType type, string lhs, string rhs)
        {
            Expr expr = new Expr();
            expr.lhs = lhs;
            expr.rhs = rhs;
            expr.type = type;
        }
        private void excute()
        {
            // TODO excute this expression
        }
        private Expr()
        {
        }

    }
}

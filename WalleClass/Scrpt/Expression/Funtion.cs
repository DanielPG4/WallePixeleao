using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Function : ExpressionArithmetic
    {
        protected List<Expression> parameters;
        public Function(Type exprType, (int, int) location, List<Expression> parameters) : base(exprType, location)
        {
            this.parameters = parameters;

        }

        
    }
}
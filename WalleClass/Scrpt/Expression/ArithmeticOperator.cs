using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class ArithmeticOperator: ExpressionArithmetic
    {
        protected ArithmeticOperator(Type exprType, (int, int) location, ExpressionArithmetic leftExpression, ExpressionArithmetic rightExpression) : base(exprType, location)
        {
            leftExpression = LeftExpression;
            rightExpression = RightExpression;
        }

        public ExpressionArithmetic LeftExpression { get; private set ; }
        public ExpressionArithmetic RightExpression { get; private set; }
    }
}
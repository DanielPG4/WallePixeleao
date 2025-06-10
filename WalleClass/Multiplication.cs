using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Multiplication : ArithmeticOperator
    {
        public Multiplication(Type exprType, (int, int) location, ExpressionArithmetic leftExpression, ExpressionArithmetic rightExpression) : base(exprType, location, leftExpression, rightExpression)
        {
        }

        public override object Evaluate(ProgramMemory program)
        {
            return LeftExpression.ArithmeticEvaluate(program) * RightExpression.ArithmeticEvaluate(program);
        }
    }
}

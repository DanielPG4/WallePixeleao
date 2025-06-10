using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class ExpressionArithmetic : Expression
    {
        protected ExpressionArithmetic(Type exprType, (int, int) location) : base(exprType, location)
        {
        }
        public int ArithmeticEvaluate(ProgramMemory program)
        {
            return (int)Evaluate(program);
        }
    }
}
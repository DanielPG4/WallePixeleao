using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class ExpressionArithmetic : Expression
    {
        public ExpressionArithmetic((int, int) location) : base( location)
        {
        }
        public int ArithmeticEvaluate(ProgramMemory program)
        {
            return (int)Evaluate(program);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Number : ExpressionArithmetic
    {
        public Number(Type exprType, (int, int) location) : base(location)
        {
        }

        public override object Evaluate(ProgramMemory program)
        {
            throw new NotImplementedException();
        }
    }
}
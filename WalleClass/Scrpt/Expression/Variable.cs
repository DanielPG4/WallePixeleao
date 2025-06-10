using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Variable : ExpressionArithmetic
    {
        public Variable(Type exprType, (int, int) location) : base(exprType, location)
        {
        }

        public override object Evaluate(ProgramMemory program)
        {
            throw new NotImplementedException();
        }
    }
}
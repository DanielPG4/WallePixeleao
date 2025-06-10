using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Expression
    {
        public Type ExprType { get; private set; }
        
        public (int, int) location;

        public Expression(Type exprType, (int, int) location )
        {
            ExprType = exprType;
            this.location = location;
        }

        public abstract object Evaluate(ProgramMemory program);

    }
}
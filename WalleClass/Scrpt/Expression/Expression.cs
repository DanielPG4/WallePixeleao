using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Expression
    {
        public Type ExprType;
        public (int, int) location;

        public Expression((int, int) location )
        {
            this.location = location;
        }

        public abstract object Evaluate(ProgramMemory program);

    }
}
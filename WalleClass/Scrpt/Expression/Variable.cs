using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Variable : ExpressionArithmetic
    {
        public string Name { get; set; }
        public Variable((int, int) location, string name) : base(location)
        {
            Name = name;
        }

        public override object Evaluate(ProgramMemory program)
        {
            throw new NotImplementedException();
        }
    }
}
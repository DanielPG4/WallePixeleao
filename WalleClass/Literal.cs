using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Literal : Expression
    {
        public int Val { get; private set ; }
        public Literal((int, int) location, int val) : base(location)
        {
            Val = val;
        }

        public override object Evaluate(ProgramMemory program)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Assign : Node
    {
        public Assign((int, int) location) : base(location)
        {
        }

        public override void Execute(ProgramMemory programMemory)
        {
            throw new NotImplementedException();
        }
    }
}
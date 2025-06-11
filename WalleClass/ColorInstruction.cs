using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public class ColorInstruction : Intruction
    {
        public ColorInstruction((int, int) location, List<Expression> parameter) : base(location, parameter)
        {
        }

        public override void Execute(ProgramMemory programMemory)
        {
            throw new NotImplementedException();
        }
    }
}

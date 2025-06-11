using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Spawn : Intruction
    {
        public Spawn((int, int) location, List<Expression> parameter) : base(location, parameter)
        {
        }

        public override void Execute(ProgramMemory programMemory)//////////////////////////////////////////////////////////////////////////////////////
        {
            programMemory.wallePosition = ((int)parameter[0].Evaluate(programMemory), (int)parameter[0].Evaluate(programMemory));
        }
    }
}

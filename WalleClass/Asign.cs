using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Asign : Node
    {
        public string Name { get; private set ; }

        Expression expression;
        public Asign((int, int) location, string name, Expression expression) : base(location)
        {
            Name = name;
            this.expression = expression;
        }

        public override void Execute(ProgramMemory programMemory)
        {
            throw new NotImplementedException();
        }
    }
}

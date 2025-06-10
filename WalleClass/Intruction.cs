using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Intruction : Node
    {
        protected Intruction((int, int) location) : base(location)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class DrawLineIntruction : Intruction
    {
        protected DrawLineIntruction((int, int) location) : base(location)
        {
        }
    }
}

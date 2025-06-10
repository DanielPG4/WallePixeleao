using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class DrawCircle : Intruction
    {
        protected DrawCircle((int, int) location) : base(location)
        {
        }
    }
}

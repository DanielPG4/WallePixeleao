using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Fill : Intruction
    {
        protected Fill((int, int) location) : base(location)
        {
        }
    }
}

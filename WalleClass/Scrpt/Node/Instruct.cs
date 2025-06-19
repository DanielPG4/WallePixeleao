using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Instruct : Node
    {
        public Instruct((int, int) location) : base(location)
        {
        }
    }
}
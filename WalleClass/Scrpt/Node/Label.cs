using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Label : Node
    {
        public Label((int, int) location) : base(location)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public class Spawn : Intruction
    {
        public Spawn((int, int) location) : base(location)
        {
        }
    }
}

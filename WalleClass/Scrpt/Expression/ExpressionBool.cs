using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class ExpressionBool : Expression
    {
        public ExpressionBool((int, int) location) : base(location)
        {
        }
    }
}
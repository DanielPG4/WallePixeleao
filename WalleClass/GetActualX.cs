using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class GetActualX : Function
    {
        protected GetActualX(Type exprType, (int, int) location, List<Expression> parameters) : base(exprType, location, parameters)
        {
        }
    }
}

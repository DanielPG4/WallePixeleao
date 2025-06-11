using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Node
    {
        public (int, int) location;


        protected Node((int, int) location)
        {
            this.location = location;
        }
        public abstract void Execute(ProgramMemory programMemory);
        
    }
}
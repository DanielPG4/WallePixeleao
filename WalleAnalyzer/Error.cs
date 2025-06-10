using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleAnalyzer
{
    class Error
    {
        public Error((int, int) location, string text)
        {
            Location = location;
            Text = text;
        }

        public (int, int) Location { get; private set; } // (Row,Col)

        public string Text { get; private set;}
            
        
    }
}

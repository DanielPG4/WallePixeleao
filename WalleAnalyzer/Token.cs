using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalleAnalyzer
{
    public class Token
    {
        public (int,int) location;
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token((int,int) location, TokenType type, string value)
        {
            this.location = location;
            Type = type;
            Value = value;
        }
    }
    public enum TokenType
    {
        Number,
        Variable,
        KeyWord,
        Symbol,
        Operator,
        EndLine,
        Error



    }
    public class TokenValues
    {
        protected TokenValues() { }

        public const string Add = "Addition"; // +
        public const string Sub = "Subtract"; // -
        public const string Mul = "Multiplication"; // *
        public const string Div = "Division"; // /
        public const string Pot = "Potencia"; // ** 
        public const string Module = "Module"; // %

        public const string Assign = "Assign"; // <-
        public const string ValueSeparator = "ValueSeparator"; // ,
        public const string StatementSeparator = "StatementSeparator"; // ;
        public const string EndLine = "EndLine"; // \n
        
        public const string And = "And"; // &&
        public const string Or = "Or"; // ||

        public const string Equals = "Equals"; // == 
        public const string Different = "Different"; // !=
        public const string GreaterThan = "GreaterThan"; // >
        public const string LessThan = "LessThan"; // <
        public const string GreaterEqual = "GreaterEqual"; // >=
        public const string LessEqual = "LessEqual"; // <=
        
        public const string OpenBracket = "OpenBracket"; // (
        public const string ClosedBracket = "ClosedBracket"; // )
        public const string OpenSquareBraces = "OpenSquareBraces"; // [
        public const string ClosedSquareBraces = "ClosedSquareBraces"; // ]

        // KeyWord
        public const string Spawn = "Spawn"; 
        public const string Color = "Color"; 
        public const string Size = "Size";
        public const string DrawLine = "DrawLine";
        public const string DrawCircle = "DrawCircle";
        public const string DrawRectangle = "DrawRectangle";
        public const string Fill = "Fill";

        public const string GetActualX = "GetActualX";
        public const string GetActualY = "GetActualY";
        public const string GetCanvasSize = "GetCanvasSize";
        public const string GetColorCount = "GetColorCount";
        public const string IsBrushColor = "IsBrushColor";
        public const string IsBrushSize = "IsBrushSize";
        public const string IsCanvasColor = "IsCanvasColor";
        public const string IsColor = "IsColor";
        public const string GoTo = "GoTo";

    }

}

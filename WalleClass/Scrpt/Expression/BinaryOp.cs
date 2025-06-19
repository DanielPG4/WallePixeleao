using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalleClass
{
    public class BinaryOp: ExpressionArithmetic
    {
        public Expression Left, Right;
        public string Operator;
        public BinaryOp((int, int) loc, Expression left, string op, Expression right) :base(loc)
        {
            location = loc;
            Left = left;
            Right = right;
            Operator = op;
            ExprType = typeof(int); // Resultado final también se asume entero
        }

        public ExpressionArithmetic LeftExpression { get; private set ; }
        public ExpressionArithmetic RightExpression { get; private set; }

        public override object Evaluate(ProgramMemory program)
        {
            throw new NotImplementedException();
        }
    }
}
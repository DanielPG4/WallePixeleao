using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WalleClass
{
    public abstract class Intruction : Node
    {
        protected List<Expression> parameter;
        protected Intruction((int, int) location, List<Expression> parameter) : base(location)
        {
            this.parameter = parameter;
        }

        public Intruction GetIntruction((int,int) localization, string name, List<Expression> parameter)
        {
            switch(name)
            {
                case "Spawn":
                    return new Spawn(localization, parameter);
                case "Color":
                    return new ColorInstruction(localization, parameter);
                case "Size":
                    return new SizeIntruction(localization, parameter);
                case "DrawCircle":
                    return new DrawCircle(localization, parameter);
                case "DrawLine":
                    return new DrawLineIntruction(localization, parameter);
                case "DrawRectangle":
                    return new DrawRectangleIntruction(localization, parameter);
                case "Fill":
                    return new Fill(localization, parameter);

                default:
                    throw new Exception("Intrucción invalida");


            }
        
    }
}

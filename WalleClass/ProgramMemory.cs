using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WalleClass
{
    public class ProgramMemory
    {
        public int index;
        public Color[,] canvas;
        public int brushSize;
        public Color brushColor;
        public (int, int) wallePosition;
        public Dictionary<string, int> labels;
        public Dictionary<string, int> variables;

        public ProgramMemory(int index, Color[,] canvas, int brushSize, Color brushColor, (int, int) wallePosition)
        {
            this.index = index;
            this.canvas = canvas;
            this.brushSize = brushSize;
            this.brushColor = brushColor;
            this.wallePosition = wallePosition;
            labels = new Dictionary<string, int>();
            variables = new Dictionary<string, int>();
        }
    }
}

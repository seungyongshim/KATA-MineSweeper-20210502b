using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class MineField
    {
        public MineField(int width, int height)
        {
            Width = width;
            Height = height;

        }

        public IList<Cell> Cells { get; }
        public int Width { get; }
        public int Height { get; }
    }
}

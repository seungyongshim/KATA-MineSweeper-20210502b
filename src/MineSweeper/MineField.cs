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

            Cells = new Cell[Width * height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[y * Width + x] = new Cell();
                }
            }
        }

        public IList<Cell> Cells { get; }
        public int Width { get; }
        public int Height { get; }
    }
}

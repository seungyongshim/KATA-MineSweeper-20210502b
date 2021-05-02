using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class MineField
    {
        public MineField(int width, int height)
        {
            Width = width;
            Height = height;

            Cells = (from x in Enumerable.Range(0, width)
                     from y in Enumerable.Range(0, height)
                     select new Cell()).ToList();
        }

        public IList<Cell> Cells { get; }
        public int Width { get; }
        public int Height { get; }

        public void SetBombs(int bombsCount)
        {
            
        }
    }
}

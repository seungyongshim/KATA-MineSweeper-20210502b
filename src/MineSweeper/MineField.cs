using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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

        public IEnumerable<int> RandomIndexGenerator()
        {
            while (true)
            {
                yield return RandomNumberGenerator.GetInt32(Width * Height);
            }
        }
            

        public void SetBombs(int bombsCount)
        {
            foreach (var cell in RandomIndexGenerator().Distinct()
                                                       .Select(x => Cells[x])
                                                       .Take(bombsCount))
            {
                cell.SetBomb();
            }
        }
    }
}

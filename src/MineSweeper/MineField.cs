using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static MineSweeper.MineFieldHelper;

[assembly: InternalsVisibleTo("MineSweeper.Tests")]

namespace MineSweeper
{
    public class MineField
    {
        public MineField(int width, int height, int bombsCount = 0) :
            this(width, height, RandomIndexGenerator(width, height).Distinct()
                                                                   .Take(bombsCount))
        { }
        public MineField(int width, int height, IEnumerable<(int X, int Y)> bombPosGenerator)
        {
            Width = width;
            Height = height;

            Cells = (from x in Enumerable.Range(0, width)
                     from y in Enumerable.Range(0, height)
                     select new Cell(NearCellGenerator(x, y, GetCell)))
                    .ToList();

            bombPosGenerator.ForEach(x => GetCell(x)?.SetBomb());
        }

        internal IList<Cell> Cells { get; }
        public int Width { get; }
        public int Height { get; }

        public Cell GetCell((int X, int Y) pos) => pos switch
        {
            (var x, _) when x < 0 || x >= Width => null,
            (_, var y) when y < 0 || y >= Height => null,
            _ => Cells[(pos.Y * Width) + pos.X]
        };

        public void Click(int x, int y) =>
            GetCell((x, y))?.Click();
    }
}

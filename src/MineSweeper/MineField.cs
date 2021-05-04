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
        public MineField(int width, int height)
        {
            Width = width;
            Height = height;

            Cells = (from x in Enumerable.Range(0, width)
                     from y in Enumerable.Range(0, height)
                     select new Cell(x, y, NearCellGenerator(x, y, GetCell)))
                    .ToList();
        }

        internal IList<Cell> Cells { get; }
        public int Width { get; }
        public int Height { get; }

        public void SetBombs(int bombsCount)
        {
            foreach (var cell in RandomIndexGenerator(Width, Height).Distinct()
                                                                    .Select(x => Cells[x])
                                                                    .Take(bombsCount))
            {
                cell.SetBomb();
            }
        }

        public void SetNearBombsCounts()
        {
            foreach (var cell in from bombCell in Cells.Where(x => x.IsBomb)
                                 from nearCell in NearCellGenerator(bombCell.X, bombCell.Y, GetCell)
                                 select nearCell)
            {
                cell.NearBombsCount++;
            }
        }

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

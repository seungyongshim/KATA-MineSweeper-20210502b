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
                     select new Cell(x, y)).ToList();
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

        public void SetNearBombsCounts()
        {
            // 폭탄 위치 루프
            foreach (var cell in Cells.Where(x => x.IsBomb))
            {
                foreach(var item in NearCellGenerator(cell.X, cell.Y))
                {
                    // 폭탄 주변 블록에 +1
                    item.NearBombsCount++;
                }
            }
        }

        public IEnumerable<Cell> NearCellGenerator(int x, int y)
        {
            return NearCellPosGenerator(x, y).Select(GetCell)
                                             .Where(IsNotNull);
        }
        public bool IsNotNull<T>(T arg) => arg is not null;

        public Cell GetCell((int X, int Y) pos) => pos switch
        {
            (var x, _) when x < 0 || x >= Width => null,
            (_, var y) when y < 0 || y >= Height => null,
            var p => Cells[(pos.Y * Width) + pos.X]
        };

        public IEnumerable<(int X, int Y)> NearCellPosGenerator(int x, int y)
        {
            yield return (x - 1, y - 1);
            yield return (x, y - 1);
            yield return (x + 1, y - 1);
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x - 1, y + 1);
            yield return (x, y + 1);
            yield return (x + 1, y + 1);
        }
    }
}

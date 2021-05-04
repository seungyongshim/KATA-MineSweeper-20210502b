using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class Cell
    {
        public Cell() : this(0, 0)
        {
        }

        public Cell(int x, int y) : this(x, y, Enumerable.Empty<Cell>())
        {
        }

        public Cell(int x, int y, IEnumerable<Cell> nearCellGenerator)
        {
            X = x;
            Y = y;
            NearCellGenerator = nearCellGenerator;
        }

        public bool IsBomb { get; private set; }
        public int NearBombsCount { get; set; }
        public bool IsCovered { get; private set; } = true;
        public int X { get; }
        public int Y { get; }
        public IEnumerable<Cell> NearCellGenerator { get; }

        public void SetBomb()
        {
            IsBomb = true;
            NearCellGenerator.ForEach(x => x.NearBombsCount++);
        }

        public void Click()
        {
            if (IsCovered is false) return;
            else IsCovered = false;

            if (NearBombsCount is not 0) return;

            NearCellGenerator.ForEach(x => x.Click());
        }

        public override string ToString() => this switch
        {
            { IsCovered: true } => ".",
            { IsBomb: true } => "*",
            _ => NearBombsCount.ToString(),
        };
    }
}

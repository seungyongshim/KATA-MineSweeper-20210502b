using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static FpMineSweeper.Prelude;

namespace FpMineSweeper
{
    public record MineField(int Width, int Height, IEnumerable<Cell> Cells);

    public record MineFieldInit : MineField
    {
        public MineFieldInit(int width, int height) : base(width, height, Enumerable.Empty<Cell>())
        {
        }

        public MineFieldReady SetBombs(IEnumerable<(int X, int Y)> bombsPosGenerator) =>
           new(this with
           {
               Cells = bombsPosGenerator.Select(x => x.X + x.Y * Height)
                                        .Select(x => from idx in Enumerable.Range(0, Width * Height)
                                                     select idx == x ? true : false)
                                        .Aggregate((x, y) => x.Zip(y, (a, b) => a || b))
                                        .Select(x => cell(x))
           });
    }

    public record MineFieldReady : MineField
    {
        public MineFieldReady(MineFieldInit mineFieldInit) :
            base(mineFieldInit.Width, mineFieldInit.Height, mineFieldInit.Cells)
        {
        }
    }
}

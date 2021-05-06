using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static FpMineSweeper.Prelude;

namespace FpMineSweeper
{
    public record MineField(int Width, int Height, IEnumerable<Cell> Cells);

    public record MineFieldInit : MineField
    {
        public MineFieldInit(int width, int height) : base(width, height, List.empty<Cell>())
        {
        }

        public MineFieldReady SetBombs(IEnumerable<(int X, int Y)> bombsPosGenerator) =>
           new(this with
           {
               Cells = from idx in Enumerable.Range(0, Width * Height)
                       let c = cell(false)
                       select 

                       let bombPos = bomb.X + bomb.Y * Height
                       select (idx == bombPos ? cell(true) : cell(false)
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

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
               Cells = bombsPosGenerator.Select(ConvertPosToIndex)
                                        .Select(GenerateBooleanMap)
                                        .Aggregate(CombineBooleanMap)
                                        .Zip((from bombPos in bombsPosGenerator
                                              from nearBombPos in NearCells(bombPos)
                                              select nearBombPos)
                                             .Select(ConvertPosToIndex)
                                             .Select(x => from idx in Enumerable.Range(0, Width * Height)
                                                          select idx == x ? 1 : 0)
                                             .Aggregate((x, y) => x.Zip(y, (a, b) => a + b)),
                                             (a, b) => cell(a, b))
           });

        public int ConvertPosToIndex((int X, int Y) x) => x.X + x.Y * Width;

        public IEnumerable<bool> CombineBooleanMap(IEnumerable<bool> x, IEnumerable<bool> y) =>
            x.Zip(y, (a, b) => a || b);

        public IEnumerable<bool> GenerateBooleanMap (int index) =>
             from idx in Enumerable.Range(0, Width * Height)
             select idx == index ? true : false;
             

        public IEnumerable<(int X, int Y)> NearCells((int X, int Y) pos) 
        {
            IEnumerable<(int X, int Y)> NearCellsInner()
            {
                yield return (pos.X - 1, pos.Y - 1);
                yield return (pos.X, pos.Y - 1);
                yield return (pos.X + 1, pos.Y - 1);
                yield return (pos.X - 1, pos.Y);
                yield return (pos.X + 1, pos.Y);
                yield return (pos.X - 1, pos.Y + 1);
                yield return (pos.X, pos.Y + 1);
                yield return (pos.X + 1, pos.Y + 1);
            }

            bool CheckBoundary((int X, int Y) pos) => pos switch
            {
                (var x, var y) when x <0 || y <0 || x >= Width || y >= Height => false,
                _ => true
            };

            return NearCellsInner().Where(CheckBoundary);
        }
    }

    public record MineFieldReady : MineField
    {
        public MineFieldReady(MineFieldInit mineFieldInit) :
            base(mineFieldInit.Width, mineFieldInit.Height, mineFieldInit.Cells)
        {
        }
    }
}

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

        public MineFieldReady SetBombs(IEnumerable<(int X, int Y)> bombPosGenerator) =>
           new(this with
           {
               Cells = from Y in Enumerable.Range(0, Height)
                       from X in Enumerable.Range(0, Width)
                       let pos = (X, Y)
                       join bombPos in from bombPosInner in bombPosGenerator
                                       select new { Pos = bombPosInner }
                       on pos equals bombPos.Pos into bombPosGroup
                       from bombPosGroupItem in bombPosGroup.DefaultIfEmpty(null)
                       join nearBombCount in from bombPosInner in bombPosGenerator
                                             from nearBombPos in NearCells(bombPosInner)
                                             group nearBombPos by nearBombPos into nearBombPosGroup
                                             select new
                                             {
                                                 Pos = nearBombPosGroup.Key,
                                                 Count = nearBombPosGroup.Count()
                                             }
                       on pos equals nearBombCount.Pos into nearBombCountGroup
                       from nearBombCountGroupItem in nearBombCountGroup.DefaultIfEmpty()
                       select cell(pos,
                                   bombPosGroupItem is not null,
                                   nearBombCountGroupItem.Count)
           });

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
                (var x, var y) when x < 0 || y < 0 || x >= Width || y >= Height => false,
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

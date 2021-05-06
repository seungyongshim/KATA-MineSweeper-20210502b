using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static FpMineSweeper.Prelude;

namespace FpMineSweeper
{
    public record MineField(int Width, int Height, IEnumerable<Cell> Cells)
    {
        protected IEnumerable<(int X, int Y)> NearCellGenerator((int X, int Y) pos)
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

    public record MineFieldInit : MineField
    {
        public MineFieldInit(int width, int height)
            : base(width, height, Enumerable.Empty<Cell>())
        {
        }

        public MineFieldPlay SetBombs(IEnumerable<(int X, int Y)> bombPosGenerator) =>
           new(this with
           {
               Cells = from y in Enumerable.Range(0, Height)
                       from x in Enumerable.Range(0, Width)
                       select (x, y) into pos
                       join bombPos in from bombPosInner in bombPosGenerator
                                       select new { Pos = bombPosInner }
                       on pos equals bombPos.Pos into bombPosGroup
                       from bombPosGroupItem in bombPosGroup.DefaultIfEmpty(null)
                       join nearBombCount in from bombPosInner in bombPosGenerator
                                             from nearBombPos in NearCellGenerator(bombPosInner)
                                             group nearBombPos by nearBombPos into nearBombPosGroup
                                             select new
                                             {
                                                 Pos = nearBombPosGroup.Key,
                                                 Count = nearBombPosGroup.Count()
                                             }
                       on pos equals nearBombCount.Pos into nearBombCountGroup
                       from nearBombCountGroupItem in nearBombCountGroup.DefaultIfEmpty(new
                       {
                           Pos = (-1,-1),
                           Count = 0
                       })
                       select cell(pos,
                                   bombPosGroupItem is not null,
                                   nearBombCountGroupItem.Count)
           });
    }

    public record MineFieldPlay : MineField
    {
        public MineFieldPlay(MineFieldInit mineFieldInit) :
            base(mineFieldInit.Width, mineFieldInit.Height, mineFieldInit.Cells)
        {
        }

        public MineFieldPlay Click((int X, int Y) pos) =>
            new(this with
            {
                Cells = from cell in Cells
                        join uncovered in ClickInner(pos, Cells)
                        on cell.Pos equals uncovered.Pos into cellGroup
                        from cellGroupItem in cellGroup.DefaultIfEmpty(null)
                        select cellGroupItem is null ? cell : cellGroupItem
            });

        private IEnumerable<Cell> ClickInner((int X, int Y) pos, IEnumerable<Cell> cells)
        {
            var stage1 = from cell in cells
                         where cell.Pos == pos
                         where cell.IsCovered == true
                         select cell.Click();

            foreach(var item in stage1)
            {
                yield return item;
            }
            

            //var stage2 = 

            //var stage3 = from cell in stage1
            //             where cell.NearBombsCount == 0
            //             from nearcell in NearCellGenerator(cell.Pos)
            //             from x in ClickInner(nearcell, stage2)
            //             select x;

            //var stage4 = from cell in stage2
            //             join x in from x in stage3
            //                       group x by x into xGroup
            //                       select xGroup.Key
            //             on cell.Pos equals x.Pos into cellGroup
            //             from cellGroupItem in cellGroup.DefaultIfEmpty(null)
            //             select cellGroupItem is null ? cell : cellGroupItem;
        }





    }
}

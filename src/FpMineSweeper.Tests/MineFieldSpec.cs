using System.Linq;
using FluentAssertions;
using Xunit;
using static FpMineSweeper.Prelude;

namespace FpMineSweeper.Tests
{
    public class MineFieldSpec
    {
        [Fact]
        public void SetBombs()
        {
            mineField(3, 3).SetBombs(new[] { (0, 0), (1, 1), (0, 2) })
                           .Cells
                           .Select(x => x.IsBomb)
                           .Should()
                           .BeEquivalentTo(new[] {true, false, false,
                                                  false, true, false,
                                                  true, false, false});
        }

        [Fact]
        public void NearCountBombs()
        {
            mineField(3, 3).SetBombs(new[] { (0, 0), (1, 1), (0, 2) })
                           .Cells
                           .Select(x => x.NearBombsCount)
                           .Should()
                           .BeEquivalentTo(new[] { 1, 2, 1,
                                                   3, 2, 1,
                                                   1, 2, 1});
        }

        [Fact]
        public void NearCells()
        {
            mineField(3, 3).NearCells((0, 0))
                           .Should()
                           .BeEquivalentTo(new[] { (1, 0), (0, 1), (1, 1) });
        }
    }
}

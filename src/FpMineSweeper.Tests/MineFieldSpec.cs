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
        public void ConvertPosToIndex()
        {
            mineField(3, 3).ConvertPosToIndex((0, 0)).Should().Be(0);
            mineField(3, 3).ConvertPosToIndex((1, 1)).Should().Be(4);
            mineField(3, 3).ConvertPosToIndex((0, 2)).Should().Be(6);
        }

        [Fact]
        public void GenerateBooleanMap()
        {
            mineField(3, 3).GenerateBooleanMap(2)
                           .Should()
                           .BeEquivalentTo(new[] { false, false, true,
                                                   false, false, false,
                                                   false, false, false });
        }

        [Fact]
        public void CombineBooleanMap()
        {
            mineField(3, 3).CombineBooleanMap(new[] { true, false, false }, new[] { false, true, false })
                           .Should()
                           .BeEquivalentTo(new[] { true, true, false });
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

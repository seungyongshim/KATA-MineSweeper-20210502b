using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace MineSweeper.Tests
{
    public class MineFieldHelperSpec
    {
        [Fact]
        public void NearPosGenerator()
        {
            MineFieldHelper.NearPosGenerator(1, 1)
                           .Should()
                           .BeEquivalentTo((0, 0), (1, 0), (2, 0),
                                           (0, 1), (2, 1),
                                           (0, 2), (1, 2), (2, 2));
        }

        [Fact]
        public void IsNotNull()
        {
            Cell cell = null;
            MineFieldHelper.IsNotNull(cell).Should().BeFalse();
            MineFieldHelper.IsNotNull(1).Should().BeTrue();
        }

        [Fact]
        public void NearCellGenerator()
        {
            // Arrange
            MineFieldHelper.NearCellGenerator(1, 1, x => new Cell(x.Item1, x.Item2))
                           .Select(x => (x.X, x.Y))
                           .Should()
                           .BeEquivalentTo((0, 0), (1, 0), (2, 0),
                                           (0, 1), (2, 1),
                                           (0, 2), (1, 2), (2, 2));
        }
    }
}

using System.Linq;
using FluentAssertions;
using Xunit;

namespace MineSweeper.Tests
{
    public class MineFieldSpec
    {
        [Fact]
        public void Init()
        {
            // Arrange
            var sut = new MineField(3, 3);

            // Act

            // Assert
            sut.Cells.Count().Should().Be(9);
            sut.Cells.Where(x => x.IsCovered).Count().Should().Be(9);
        }

        [Fact]
        public void SetBombs()
        {
            // Arrange
            var sut = new MineField(3, 3, 3);

            // Act

            // Assert
            sut.Cells.Where(x => x.IsBomb).Count().Should().Be(3);
        }

        [Fact]
        public void SetNearBombsCounts()
        {
            // Arrange
            var sut = new MineField(3, 3, new[] { (0, 0) });

            // Act

            // Assert
            sut.Cells.Select(x => x.NearBombsCount)
                     .Should()
                     .BeEquivalentTo(new[] { 0, 1, 0,
                                             1, 1, 0,
                                             0, 0, 0 });
        }

        [Fact]
        public void Click()
        {
            // Arrange
            var sut = new MineField(3, 3, new[] { (0, 0) });

            // Act
            sut.Click(2, 2);

            // Assert
            sut.Cells.Select(x => x.ToString())
                     .Should()
                     .BeEquivalentTo(".", "1", "0",
                                     "1", "1", "0",
                                     "0", "0", "0");
        }
    }
}

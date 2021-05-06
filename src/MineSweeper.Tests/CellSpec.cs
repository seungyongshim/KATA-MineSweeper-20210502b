using FluentAssertions;
using Xunit;

namespace MineSweeper.Tests
{
    public class CellSpec
    {
        [Fact]
        public void Bomb()
        {
            // Arrange
            var sut = new Cell();

            // Act
            sut.SetBomb();
            sut.Click();

            // Assert
            sut.ToString().Should().Be("*");
        }

        [Fact]
        public void NearBombsCount()
        {
            // Arrange
            var sut = new Cell
            {

                // Act
                NearBombsCount = 3
            };
            sut.Click();

            // Assert
            sut.ToString().Should().Be("3");
        }

        [Fact]
        public void Covered()
        {
            // Arrange
            var sut = new Cell();

            // Act

            // Assert
            sut.ToString().Should().Be(".");
        }
    }
}

using System;
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

            // Assert
            sut.ToString().Should().Be("*");
        }

        [Fact]
        public void NearBombsCount()
        {
            // Arrange
            var sut = new Cell();

            // Act
            sut.NearBombsCount = 3;     

            // Assert
            sut.ToString().Should().Be("3");
        }
    }
}

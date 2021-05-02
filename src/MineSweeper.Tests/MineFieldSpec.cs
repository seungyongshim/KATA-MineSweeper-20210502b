using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var sut = new MineField(3, 3);

            // Act
            sut.SetBombs(3);

            // Assert
            sut.Cells.Where(x => x.IsBomb).Count().Should().Be(3);
        }
    }
}

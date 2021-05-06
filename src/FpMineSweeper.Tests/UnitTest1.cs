using System;
using FluentAssertions;
using Xunit;
using static FpMineSweeper.Prelude;

namespace FpMineSweeper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IsBomb()
        {
            new Cell(true).Should().BeEquivalentTo(new { IsBomb = true });
        }

        [Fact]
        public void IsCovered()
        {
            var sut = new Cell(true);

            sut.Should().BeEquivalentTo(new { IsCovered = true });

            sut.Click().Should().BeEquivalentTo(new { IsCovered = false });
        }

        [Fact]
        public void Str()
        {
            str(new Cell(true)).Should().Be(".");
            str(new Cell(true, 0, false)).Should().Be("*");
            str(new Cell(false, 3, false)).Should().Be("3");
        }
    }
}

using System;
using FluentAssertions;
using Xunit;
using static FpMineSweeper.Prelude;

namespace FpMineSweeper.Tests
{
    public class CellSpec
    {
        [Fact]
        public void IsBomb()
        {
            cell(true).Should().BeEquivalentTo(new { IsBomb = true });
        }

        [Fact]
        public void IsCovered()
        {
            cell(true).Should().BeEquivalentTo(new { IsCovered = true });
            cell(true).Click().Should().BeEquivalentTo(new { IsCovered = false });
        }

        [Fact]
        public void Str()
        {
            str(cell(true)).Should().Be(".");
            str(cell(true, 0, false)).Should().Be("*");
            str(cell(false, 3, false)).Should().Be("3");
        }
    }
}

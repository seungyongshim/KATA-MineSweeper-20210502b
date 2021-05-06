using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                           .BeEquivalentTo(new[] {true, false, true,
                                                   false, true, false,
                                                   false, false, false});
        }
    }
}

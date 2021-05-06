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
    }
}

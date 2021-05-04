using System.Collections.Generic;
using System.Security.Cryptography;

namespace MineSweeper
{
    public static class MineFieldHelper
    {
        public static IEnumerable<int> RandomIndexGenerator(int width, int height)
        {
            while (true)
            {
                yield return RandomNumberGenerator.GetInt32(width * height);
            }
        }

        public static IEnumerable<(int X, int Y)> NearPosGenerator(int x, int y)
        {
            yield return (x - 1, y - 1);
            yield return (x, y - 1);
            yield return (x + 1, y - 1);
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x - 1, y + 1);
            yield return (x, y + 1);
            yield return (x + 1, y + 1);
        }

        public static bool IsNotNull<T>(T arg) => arg is not null;
    }
}

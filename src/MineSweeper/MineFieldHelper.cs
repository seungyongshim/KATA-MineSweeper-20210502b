using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MineSweeper
{
    public static class MineFieldHelper
    {
        public static IEnumerable<(int X, int Y)> RandomIndexGenerator(int width, int height)
        {
            while (true)
            {
                yield return (RandomNumberGenerator.GetInt32(width),
                              RandomNumberGenerator.GetInt32(height));
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

        public static IEnumerable<Cell> NearCellGenerator(int x, int y, Func<(int, int), Cell> getCellFunc) =>
            from nearPos in NearPosGenerator(x, y)
            let cell = getCellFunc(nearPos)
            where cell is not null
            select cell;

    }
}

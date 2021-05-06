using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FpMineSweeper
{
    public static class CellHelper
    {
        public static Cell Click(this Cell cell) => cell with { IsCovered = false };
    }
}

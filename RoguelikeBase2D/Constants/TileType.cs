using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Constants
{
    /*  Wall Border
     *   1-2-3
     *   | | |
     *   4-5-6
     *   | | |
     *   7-8-9
     */

    public enum TileType
    {
        None,
        Floor,
        Wall,
        WallBorder5,
        WallBorder25,
        WallBorder258,
        WallBorder58,
        WallBorder56 = 91,
        WallBorder456,
        WallBorder45,
        WallBorder256,
        WallBorder2456,
        WallBorder245,
        WallBorder2568,
        WallBorder24568,
        WallBorder2458,
        WallBorder568,
        WallBorder4568,
        WallBorder458,
        WallBorder124568,
        WallBorder25689,
        WallBorder23568,
        WallBorder245678,
        WallBorder45689,
        WallBorder23456789,
        WallBorder12345689,
        WallBorder23456,
        WallBorder45678,
        WallBorder12456789,
        WallBorder12345678,
        WallBorder12456,
        WallBorder234568,
        WallBorder24578,
        WallBorder12458,
        WallBorder245689,
        WallBorder5689,
        WallBorder235689,
        WallBorder2345689,
        WallBorder2356,
        WallBorder2456789,
        WallBorder2345678,
        WallBorder123456789,
        WallBorder123456,
        WallBorder456789,
        WallBorder1245689,
        WallBorder1234568,
        WallBorder4578,
        WallBorder1245678,
        WallBorder124578,
        WallBorder1245,
    }

    public static class TileTypeExtensions
    {
        public static bool IsPassable(this TileType tileType)
        {
            return tileType == TileType.Floor;
        }
    }
}

﻿using System;
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
     *
     *  //Conversion Code
     *  public int EnumToValue(string enumName)
     *  {
     *      string tileType = enumName.Replace("WallBorder", "");
     *      var numbers = tileType.ToCharArray();
     *      return numbers.Sum(a => (int)Math.Pow(2, int.Parse(a.ToString())));
     *  }
     *
     *
     */

    public enum TileType
    {
        None = 0,
        Floor = 1,
        Wall = 2,
        WallBorder5 = 32,
        WallBorder25 = 36,
        
        // Vertical double sided wall
        WallBorder258 = 292,
        WallBorder1258 = 294,
        WallBorder12358 = 302,
        WallBorder12578 = 422,
        WallBorder12589 = 806,
        WallBorder123578 = 430,
        WallBorder123589 = 814,
        WallBorder1235789 = 942,
        WallBorder2358 = 300,
        WallBorder23578 = 428,
        WallBorder23589 = 812,
        WallBorder235789 = 940,
        WallBorder2578 = 420,
        WallBorder2589 = 804,
        WallBorder25789 = 932,


        WallBorder58 = 288,
        WallBorder56 = 96,
        WallBorder456 = 112,
        WallBorder45 = 48,
        WallBorder256 = 100,
        WallBorder2456 = 116,
        WallBorder245 = 52,
        WallBorder2568 = 356,
        WallBorder24568 = 372,
        WallBorder2458 = 308,
        WallBorder568 = 352,
        WallBorder4568 = 368,
        WallBorder458 = 304,
        WallBorder124568 = 374,
        WallBorder25689 = 868,
        WallBorder23568 = 364,
        WallBorder245678 = 500,
        WallBorder45689 = 880,
        WallBorder23456789 = 1020,
        WallBorder12345689 = 894,
        WallBorder23456 = 124,
        WallBorder45678 = 496,
        WallBorder12456789 = 1014,
        WallBorder12345678 = 510,
        WallBorder12456 = 118,
        WallBorder234568 = 380,
        WallBorder24578 = 436,
        WallBorder12458 = 310,
        WallBorder245689 = 884,
        WallBorder5689 = 864,
        WallBorder235689 = 876,
        WallBorder2345689 = 892,
        WallBorder2356 = 108,
        WallBorder2456789 = 1012,
        WallBorder2345678 = 508,
        WallBorder123456789 = 1022,
        WallBorder123456 = 126,
        WallBorder456789 = 1008,
        WallBorder1245689 = 886,
        WallBorder1234568 = 382,
        WallBorder4578 = 432,
        WallBorder1245678 = 502,
        WallBorder124578 = 438,
        WallBorder1245 = 54,
    }

    public static class TileTypeExtensions
    {
        public static bool IsPassable(this TileType tileType)
        {
            return tileType == TileType.Floor;
        }

        public static bool IsWallOrBorder(this TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Wall:
                case TileType.WallBorder5:
                case TileType.WallBorder25:

                case TileType.WallBorder258:
                case TileType.WallBorder1258:
                case TileType.WallBorder12358:
                case TileType.WallBorder12578:
                case TileType.WallBorder12589:
                case TileType.WallBorder123578:
                case TileType.WallBorder123589:
                case TileType.WallBorder1235789:
                case TileType.WallBorder2358:
                case TileType.WallBorder23578:
                case TileType.WallBorder23589:
                case TileType.WallBorder235789:
                case TileType.WallBorder2578:
                case TileType.WallBorder2589:
                case TileType.WallBorder25789:

                case TileType.WallBorder58:
                case TileType.WallBorder56:
                case TileType.WallBorder456:
                case TileType.WallBorder45:
                case TileType.WallBorder256:
                case TileType.WallBorder2456:
                case TileType.WallBorder245:
                case TileType.WallBorder2568:
                case TileType.WallBorder24568:
                case TileType.WallBorder2458:
                case TileType.WallBorder568:
                case TileType.WallBorder4568:
                case TileType.WallBorder458:
                case TileType.WallBorder124568:
                case TileType.WallBorder25689:
                case TileType.WallBorder23568:
                case TileType.WallBorder245678:
                case TileType.WallBorder45689:
                case TileType.WallBorder23456789:
                case TileType.WallBorder12345689:
                case TileType.WallBorder23456:
                case TileType.WallBorder45678:
                case TileType.WallBorder12456789:
                case TileType.WallBorder12345678:
                case TileType.WallBorder12456:
                case TileType.WallBorder234568:
                case TileType.WallBorder24578:
                case TileType.WallBorder12458:
                case TileType.WallBorder245689:
                case TileType.WallBorder5689:
                case TileType.WallBorder235689:
                case TileType.WallBorder2345689:
                case TileType.WallBorder2356:
                case TileType.WallBorder2456789:
                case TileType.WallBorder2345678:
                case TileType.WallBorder123456789:
                case TileType.WallBorder123456:
                case TileType.WallBorder456789:
                case TileType.WallBorder1245689:
                case TileType.WallBorder1234568:
                case TileType.WallBorder4578:
                case TileType.WallBorder1245678:
                case TileType.WallBorder124578:
                case TileType.WallBorder1245:
                    return true;
            }

            return false;
        }
    }
}

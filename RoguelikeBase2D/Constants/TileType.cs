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

        WallTopLeft = 3,
        WallTopRight = 4,
        WallBottomLeft = 5,
        WallBottomRight = 6,
        WallTopMiddle = 7,
        WallBottomMiddle = 8,
        WallTopSingle = 9,
        WallBottomSingle = 10,
        Exit = 11,

        WallBorder5 = 32,

        WallBorder25 = 36,
        WallBorder125 = 38,
        WallBorder235 = 44,
        WallBorder1235 = 46,

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
        WallBorder578 = 416,
        WallBorder589 = 800,
        WallBorder5789 = 928,

        WallBorder56 = 96,
        WallBorder356 = 104,
        WallBorder569 = 608,
        WallBorder3569 = 616,

        // Horizontal double sided wall
        WallBorder456 = 112,
        WallBorder1456 = 114,
        WallBorder13456 = 122,
        WallBorder14567 = 242,
        WallBorder14569 = 626,
        WallBorder134567 = 250,
        WallBorder134569 = 634,
        WallBorder1345679 = 762,
        WallBorder3456 = 120,
        WallBorder34567 = 248,
        WallBorder34569 = 632,
        WallBorder345679 = 760,
        WallBorder4567 = 240,
        WallBorder4569 = 624,
        WallBorder45679 = 752,

        WallBorder45 = 48,
        WallBorder145 = 50,
        WallBorder457 = 176,
        WallBorder1457 = 178,

        WallBorder256 = 100,
        WallBorder1256 = 102,
        WallBorder2569 = 612,
        WallBorder12569 = 614,

        WallBorder2456 = 116,
        
        WallBorder245 = 52,
        WallBorder2345 = 60,
        WallBorder2457 = 180,
        WallBorder23457 = 188,

        WallBorder2568 = 356,
        WallBorder24568 = 372,
        WallBorder2458 = 308,

        WallBorder568 = 352,
        WallBorder3568 = 360,
        WallBorder5678 = 480,
        WallBorder35678 = 488,

        WallBorder4568 = 368,

        WallBorder458 = 304,
        WallBorder1458 = 306,
        WallBorder4589 = 816,
        WallBorder14589 = 818,

        WallBorder124568 = 374,
        
        WallBorder25689 = 868,
        WallBorder256789 = 996,
        WallBorder125689 = 870,
        WallBorder1256789 = 998,

        WallBorder23568 = 364,
        WallBorder235678 = 492,
        WallBorder123568 = 366,
        WallBorder1235678 = 494,

        WallBorder245678 = 500,

        WallBorder45689 = 880,
        WallBorder145689 = 882,
        WallBorder345689 = 888,
        WallBorder1345689 = 890,

        WallBorder23456789 = 1020,
        WallBorder12345689 = 894,

        WallBorder23456 = 124,
        WallBorder234569 = 636,
        WallBorder234567 = 252,
        WallBorder2345679 = 764,

        WallBorder45678 = 496,
        WallBorder145678 = 498,
        WallBorder345678 = 504,
        WallBorder1345678 = 506,

        WallBorder12456789 = 1014,
        WallBorder12345678 = 510,
        
        WallBorder12456 = 118,
        WallBorder124569 = 630,
        WallBorder124567 = 246,
        WallBorder1245679 = 758,

        WallBorder234568 = 380,
        
        WallBorder24578 = 436,
        WallBorder234578 = 444,
        WallBorder245789 = 948,
        WallBorder2345789 = 956,

        WallBorder12458 = 310,
        WallBorder123458 = 318,
        WallBorder124589 = 822,
        WallBorder1234589 = 830,

        WallBorder245689 = 884,

        WallBorder5689 = 864,
        WallBorder35689 = 872,
        WallBorder56789 = 992,
        WallBorder356789 = 1000,

        WallBorder235689 = 876,
        WallBorder1235689 = 878,
        WallBorder2356789 = 1004,
        WallBorder12356789 = 1006,

        WallBorder2345689 = 892,

        WallBorder2356 = 108,
        WallBorder12356 = 110,
        WallBorder23569 = 620,
        WallBorder123569 = 622,

        WallBorder2456789 = 1012,
        WallBorder2345678 = 508,
        WallBorder123456789 = 1022,
        
        // Down facing solid wall
        WallBorder123456 = 126,
        WallBorder1234567 = 254,
        WallBorder1234569 = 638,
        WallBorder12345679 = 766,

        WallBorder456789 = 1008,
        WallBorder1456789 = 1010,
        WallBorder3456789 = 1016,
        WallBorder13456789 = 1018,

        WallBorder1245689 = 886,
        WallBorder1234568 = 382,

        WallBorder4578 = 432,
        WallBorder14578 = 434,
        WallBorder45789 = 944,
        WallBorder145789 = 946,

        WallBorder1245678 = 502,
        
        WallBorder124578 = 438,
        WallBorder1234578 = 446,
        WallBorder1245789 = 950,
        WallBorder12345789 = 958,

        WallBorder1245 = 54,
        WallBorder12345 = 62,
        WallBorder123457 = 190,
        WallBorder12457 = 182,
    }

    public static class TileTypeExtensions
    {
        public static bool IsPassable(this TileType tileType)
        {
            return !IsWallOrBorder(tileType);
        }

        public static bool IsBorder(this TileType tileType)
        {
            switch (tileType)
            {
                case TileType.WallBorder5:

                case TileType.WallBorder25:
                case TileType.WallBorder125:
                case TileType.WallBorder235:
                case TileType.WallBorder1235:

                // Vertical double sided wall
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
                case TileType.WallBorder578:
                case TileType.WallBorder589:
                case TileType.WallBorder5789:

                case TileType.WallBorder56:
                case TileType.WallBorder356:
                case TileType.WallBorder569:
                case TileType.WallBorder3569:

                // Horizontal double sided wall
                case TileType.WallBorder456:
                case TileType.WallBorder1456:
                case TileType.WallBorder13456:
                case TileType.WallBorder14567:
                case TileType.WallBorder14569:
                case TileType.WallBorder134567:
                case TileType.WallBorder134569:
                case TileType.WallBorder1345679:
                case TileType.WallBorder3456:
                case TileType.WallBorder34567:
                case TileType.WallBorder34569:
                case TileType.WallBorder345679:
                case TileType.WallBorder4567:
                case TileType.WallBorder4569:
                case TileType.WallBorder45679:

                case TileType.WallBorder45:
                case TileType.WallBorder145:
                case TileType.WallBorder457:
                case TileType.WallBorder1457:

                case TileType.WallBorder256:
                case TileType.WallBorder1256:
                case TileType.WallBorder2569:
                case TileType.WallBorder12569:

                case TileType.WallBorder2456:

                case TileType.WallBorder245:
                case TileType.WallBorder2345:
                case TileType.WallBorder2457:
                case TileType.WallBorder23457:

                case TileType.WallBorder2568:
                case TileType.WallBorder24568:
                case TileType.WallBorder2458:

                case TileType.WallBorder568:
                case TileType.WallBorder3568:
                case TileType.WallBorder5678:
                case TileType.WallBorder35678:

                case TileType.WallBorder4568:

                case TileType.WallBorder458:
                case TileType.WallBorder1458:
                case TileType.WallBorder4589:
                case TileType.WallBorder14589:

                case TileType.WallBorder124568:

                case TileType.WallBorder25689:
                case TileType.WallBorder256789:
                case TileType.WallBorder125689:
                case TileType.WallBorder1256789:

                case TileType.WallBorder23568:
                case TileType.WallBorder235678:
                case TileType.WallBorder123568:
                case TileType.WallBorder1235678:

                case TileType.WallBorder245678:

                case TileType.WallBorder45689:
                case TileType.WallBorder145689:
                case TileType.WallBorder345689:
                case TileType.WallBorder1345689:

                case TileType.WallBorder23456789:
                case TileType.WallBorder12345689:

                case TileType.WallBorder23456:
                case TileType.WallBorder234569:
                case TileType.WallBorder234567:
                case TileType.WallBorder2345679:

                case TileType.WallBorder45678:
                case TileType.WallBorder145678:
                case TileType.WallBorder345678:
                case TileType.WallBorder1345678:

                case TileType.WallBorder12456789:
                case TileType.WallBorder12345678:

                case TileType.WallBorder12456:
                case TileType.WallBorder124569:
                case TileType.WallBorder124567:
                case TileType.WallBorder1245679:

                case TileType.WallBorder234568:

                case TileType.WallBorder24578:
                case TileType.WallBorder234578:
                case TileType.WallBorder245789:
                case TileType.WallBorder2345789:

                case TileType.WallBorder12458:
                case TileType.WallBorder123458:
                case TileType.WallBorder124589:
                case TileType.WallBorder1234589:

                case TileType.WallBorder245689:

                case TileType.WallBorder5689:
                case TileType.WallBorder35689:
                case TileType.WallBorder56789:
                case TileType.WallBorder356789:

                case TileType.WallBorder235689:
                case TileType.WallBorder1235689:
                case TileType.WallBorder2356789:
                case TileType.WallBorder12356789:

                case TileType.WallBorder2345689:

                case TileType.WallBorder2356:
                case TileType.WallBorder12356:
                case TileType.WallBorder23569:
                case TileType.WallBorder123569:

                case TileType.WallBorder2456789:
                case TileType.WallBorder2345678:
                case TileType.WallBorder123456789:

                // Down facing solid wall
                case TileType.WallBorder123456:
                case TileType.WallBorder1234567:
                case TileType.WallBorder1234569:
                case TileType.WallBorder12345679:

                case TileType.WallBorder456789:
                case TileType.WallBorder1456789:
                case TileType.WallBorder3456789:
                case TileType.WallBorder13456789:

                case TileType.WallBorder1245689:
                case TileType.WallBorder1234568:

                case TileType.WallBorder4578:
                case TileType.WallBorder14578:
                case TileType.WallBorder45789:
                case TileType.WallBorder145789:

                case TileType.WallBorder1245678:

                case TileType.WallBorder124578:
                case TileType.WallBorder1234578:
                case TileType.WallBorder1245789:
                case TileType.WallBorder12345789:

                case TileType.WallBorder1245:
                case TileType.WallBorder12345:
                case TileType.WallBorder123457:
                case TileType.WallBorder12457:
                    return true;
            }

            return false;
        }

        public static bool IsWallOrBorder(this TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Wall:

                case TileType.WallTopLeft:
                case TileType.WallTopRight:
                case TileType.WallBottomLeft:
                case TileType.WallBottomRight:
                case TileType.WallTopMiddle:
                case TileType.WallBottomMiddle:
                case TileType.WallTopSingle:
                case TileType.WallBottomSingle:

                case TileType.WallBorder5:

                case TileType.WallBorder25:
                case TileType.WallBorder125:
                case TileType.WallBorder235:
                case TileType.WallBorder1235:

                // Vertical double sided wall
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
                case TileType.WallBorder578:
                case TileType.WallBorder589:
                case TileType.WallBorder5789:

                case TileType.WallBorder56:
                case TileType.WallBorder356:
                case TileType.WallBorder569:
                case TileType.WallBorder3569:

                // Horizontal double sided wall
                case TileType.WallBorder456:
                case TileType.WallBorder1456:
                case TileType.WallBorder13456:
                case TileType.WallBorder14567:
                case TileType.WallBorder14569:
                case TileType.WallBorder134567:
                case TileType.WallBorder134569:
                case TileType.WallBorder1345679:
                case TileType.WallBorder3456:
                case TileType.WallBorder34567:
                case TileType.WallBorder34569:
                case TileType.WallBorder345679:
                case TileType.WallBorder4567:
                case TileType.WallBorder4569:
                case TileType.WallBorder45679:

                case TileType.WallBorder45:
                case TileType.WallBorder145:
                case TileType.WallBorder457:
                case TileType.WallBorder1457:

                case TileType.WallBorder256:
                case TileType.WallBorder1256:
                case TileType.WallBorder2569:
                case TileType.WallBorder12569:

                case TileType.WallBorder2456:

                case TileType.WallBorder245:
                case TileType.WallBorder2345:
                case TileType.WallBorder2457:
                case TileType.WallBorder23457:

                case TileType.WallBorder2568:
                case TileType.WallBorder24568:
                case TileType.WallBorder2458:

                case TileType.WallBorder568:
                case TileType.WallBorder3568:
                case TileType.WallBorder5678:
                case TileType.WallBorder35678:

                case TileType.WallBorder4568:

                case TileType.WallBorder458:
                case TileType.WallBorder1458:
                case TileType.WallBorder4589:
                case TileType.WallBorder14589:

                case TileType.WallBorder124568:

                case TileType.WallBorder25689:
                case TileType.WallBorder256789:
                case TileType.WallBorder125689:
                case TileType.WallBorder1256789:

                case TileType.WallBorder23568:
                case TileType.WallBorder235678:
                case TileType.WallBorder123568:
                case TileType.WallBorder1235678:

                case TileType.WallBorder245678:

                case TileType.WallBorder45689:
                case TileType.WallBorder145689:
                case TileType.WallBorder345689:
                case TileType.WallBorder1345689:

                case TileType.WallBorder23456789:
                case TileType.WallBorder12345689:

                case TileType.WallBorder23456:
                case TileType.WallBorder234569:
                case TileType.WallBorder234567:
                case TileType.WallBorder2345679:

                case TileType.WallBorder45678:
                case TileType.WallBorder145678:
                case TileType.WallBorder345678:
                case TileType.WallBorder1345678:

                case TileType.WallBorder12456789:
                case TileType.WallBorder12345678:

                case TileType.WallBorder12456:
                case TileType.WallBorder124569:
                case TileType.WallBorder124567:
                case TileType.WallBorder1245679:

                case TileType.WallBorder234568:

                case TileType.WallBorder24578:
                case TileType.WallBorder234578:
                case TileType.WallBorder245789:
                case TileType.WallBorder2345789:

                case TileType.WallBorder12458:
                case TileType.WallBorder123458:
                case TileType.WallBorder124589:
                case TileType.WallBorder1234589:

                case TileType.WallBorder245689:

                case TileType.WallBorder5689:
                case TileType.WallBorder35689:
                case TileType.WallBorder56789:
                case TileType.WallBorder356789:

                case TileType.WallBorder235689:
                case TileType.WallBorder1235689:
                case TileType.WallBorder2356789:
                case TileType.WallBorder12356789:

                case TileType.WallBorder2345689:

                case TileType.WallBorder2356:
                case TileType.WallBorder12356:
                case TileType.WallBorder23569:
                case TileType.WallBorder123569:

                case TileType.WallBorder2456789:
                case TileType.WallBorder2345678:
                case TileType.WallBorder123456789:

                // Down facing solid wall
                case TileType.WallBorder123456:
                case TileType.WallBorder1234567:
                case TileType.WallBorder1234569:
                case TileType.WallBorder12345679:

                case TileType.WallBorder456789:
                case TileType.WallBorder1456789:
                case TileType.WallBorder3456789:
                case TileType.WallBorder13456789:

                case TileType.WallBorder1245689:
                case TileType.WallBorder1234568:

                case TileType.WallBorder4578:
                case TileType.WallBorder14578:
                case TileType.WallBorder45789:
                case TileType.WallBorder145789:

                case TileType.WallBorder1245678:

                case TileType.WallBorder124578:
                case TileType.WallBorder1234578:
                case TileType.WallBorder1245789:
                case TileType.WallBorder12345789:

                case TileType.WallBorder1245:
                case TileType.WallBorder12345:
                case TileType.WallBorder123457:
                case TileType.WallBorder12457:
                    return true;
            }

            return false;
        }
    }
}

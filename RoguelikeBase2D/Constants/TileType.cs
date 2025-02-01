using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Constants
{
    public enum TileType
    {
        None,
        Floor,
        Wall,
    }

    public static class TileTypeExtensions
    {
        public static bool IsPassable(this TileType tileType)
        {
            return tileType == TileType.Floor;
        }
    }
}

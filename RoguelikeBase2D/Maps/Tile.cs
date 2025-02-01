using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public struct Tile
    {
        public string TilesetName { get; set; }
        public int TilesetTileId { get; set; }
        public TileType TileType { get; set; }
        public bool IsExplored { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public struct Tile
    {
        public int TilesetTileId { get; set; }
        public TilesetTile TilesetTile { get; set; }
        public bool IsExplored { get; set; }
    }
}

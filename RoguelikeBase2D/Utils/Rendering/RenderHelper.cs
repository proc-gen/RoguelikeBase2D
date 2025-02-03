using Microsoft.Xna.Framework;
using RoguelikeBase2D.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Utils.Rendering
{
    public static class RenderHelper
    {
        public static Rectangle GetRectangleForTilesetTile(this Tileset tileset, int id)
        {
            return new Rectangle(
                (id % tileset.NumTilesX) * tileset.TileWidth, 
                ((int)(id / (float)tileset.NumTilesX)) * tileset.TileHeight, 
                tileset.TileWidth, 
                tileset.TileHeight
            );
        }
    }
}

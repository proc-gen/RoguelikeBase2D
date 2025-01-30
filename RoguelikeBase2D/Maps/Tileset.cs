using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public class Tileset
    {
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public TilesetTile[] TilesetTiles { get; private set; }

        public Tileset(string name, int width, int height, int tileWidth, int tileHeight)
        {
            Name = name;
            Width = width;
            Height = height;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesetTiles = new TilesetTile[(width/tileWidth) * (height/tileHeight)];
        }

        public TilesetTile GetTilesetTile(int id)
        {
            return TilesetTiles[id];
        }

        public void SetTilesetTile(int id, TilesetTile tilesetTile)
        {
            TilesetTiles[id] = tilesetTile;
        }
    }
}

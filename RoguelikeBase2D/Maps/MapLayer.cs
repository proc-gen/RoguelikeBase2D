using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public class MapLayer
    {
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        [JsonProperty]
        protected Tile[] Grid { get; private set; }
        public MapLayer(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
            Grid = new Tile[Width * Height];
            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i] = new Tile()
                {
                    TileType = Constants.TileType.None,
                    IsExplored = false,
                };
            }
        }

        public Tile GetTile(int x, int y)
        {
            return Grid[y * Width + x];
        }

        public Tile GetTile(Point point)
        {
            return GetTile(point.X, point.Y);
        }

        public void SetTile(int x, int y, Tile tile)
        {
            Grid[y * Width + x] = tile;
        }

        public void SetTile(Point point, Tile tile)
        {
            SetTile(point.X, point.Y, tile);
        }
    }
}

using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Generators
{
    public class TestGenerator : Generator
    {
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);

            PreProcessMap(map);
            ProcessWallBorders(map);
            ProcessWalls(map);

            return map;
        }

        private void PreProcessMap(Map map)
        {
            CreateOuterWalls(map);
            CreateDividerWalls(map);
        }

        private void CreateOuterWalls(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    if (i == 0 || i == (map.Width - 1) || j == 0 || j == (map.Height - 1))
                    {
                        var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                        tile.TileType = TileType.Wall;
                        map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
                    }
                    else
                    {
                        var tile = map.GetTileFromLayer(MapLayerType.Floor, i, j);
                        tile.TileType = TileType.Floor;
                        map.SetTileInLayer(MapLayerType.Floor, i, j, tile);
                    }
                }
            }
        }

        private void CreateDividerWalls(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                if (i != map.Width / 4 && i != map.Width * 3 / 4)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, map.Height / 2);
                    tile.TileType = TileType.Wall;
                    map.SetTileInLayer(MapLayerType.Wall, i, map.Height / 2, tile);
                }
            }

            for (int j = 0; j < map.Height; j++)
            {
                if (j < map.Height / 4 - 1 || (j > map.Height / 4 + 1 && j < map.Height * 3 / 4 - 1) || j > map.Height * 3 / 4 + 1)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, map.Width / 2, j);
                    tile.TileType = TileType.Wall;
                    map.SetTileInLayer(MapLayerType.Wall, map.Width / 2, j, tile);
                }
            }
        }

        public override Point GetPlayerStartingPosition(Map map)
        {
            return new Point(map.Width / 4, map.Height / 4);
        }
    }
}

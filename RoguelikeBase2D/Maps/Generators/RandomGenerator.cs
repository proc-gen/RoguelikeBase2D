using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Generators
{
    public class RandomGenerator : Generator
    {
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);

            PreProcessMap(map);
            ProcessWallBorders(map);
            ProcessWalls(map);
            RemoveHiddenFloors(map);

            return map;
        }

        private void PreProcessMap(Map map)
        {
            CreateOuterWalls(map);
            CreateRandomPillars(map);
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

        private void CreateRandomPillars(Map map)
        {
            Random random = new Random();

            for (int i = 0; i < (map.Width * map.Height / 4); i++)
            {
                int x = random.Next(1, map.Width - 1);
                int y = random.Next(3, map.Height - 3);

                if(x != map.Width / 2 &&  
                    y != map.Height / 2 &&
                    CanPlaceWall(map, x, y))
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, x, y);
                    tile.TileType = TileType.Wall;
                    map.SetTileInLayer(MapLayerType.Wall, x, y, tile);
                }
            }
        }

        private bool CanPlaceWall(Map map, int x, int y)
        {
            for (int i = x - 2; i < x + 3; i++)
            {
                for(int j = y - 2; j < y + 3; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    if(tile.TileType == TileType.Wall)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override Point GetPlayerStartingPosition(Map map)
        {
            return new Point(map.Width / 2, map.Height / 2);
        }

        public override HashSet<Point> GetEnemySpawnPoints(Map map)
        {
            HashSet<Point> points = new HashSet<Point>();

            var playerStart = GetPlayerStartingPosition(map);

            return points;
        }

        public override Point GetMapExit(Map map) => throw new NotImplementedException();
        public override HashSet<Point> GetItemSpawnPoints(Map map) => throw new NotImplementedException();
    }
}

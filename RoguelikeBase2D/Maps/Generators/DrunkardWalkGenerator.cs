using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Generators
{
    public class DrunkardWalkGenerator : Generator
    {
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);
            SeededRandom = new SeededRandom(Random.Shared.Next());
            map.Seed = SeededRandom.Seed;
            
            PreProcessMap(map);
            ProcessWallBorders(map);
            ProcessWalls(map);

            return map;
        }

        private void PreProcessMap(Map map)
        {
            FillMap(map);
            CreateRooms(map);
            FixGaps(map);
        }

        private void FillMap(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    tile.TileType = TileType.Wall;
                    map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
                    
                    tile = map.GetTileFromLayer(MapLayerType.Floor, i, j);
                    tile.TileType = TileType.Floor;
                    map.SetTileInLayer(MapLayerType.Floor, i, j, tile);
                }
            }
        }

        private void CreateRooms(Map map)
        {
            Point start = new Point(map.Width / 2, map.Height / 2);
            SetFloor(map, start.X, start.Y);

            int totalTiles = map.Width * map.Height,
                desiredTiles = map.Width * map.Height / 2,
                floorTiles = 1;

            while (floorTiles < desiredTiles)
            {
                int drunkX = start.X,
                    drunkY = start.Y,
                    drunkLife = SeededRandom.Next(200, 400);

                while (drunkLife > 0)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, drunkX, drunkY);
                    if (tile.TileType == TileType.Wall)
                    {
                        floorTiles++;
                    }
                    SetFloor(map, drunkX, drunkY);

                    int direction = SeededRandom.Next(4);
                    switch (direction)
                    {
                        case 0:
                            if (drunkX > 2)
                            {
                                drunkX--;
                            }
                            break;
                        case 1:
                            if (drunkX < map.Width - 2)
                            {
                                drunkX++;
                            }
                            break;
                        case 2:
                            if (drunkY > 2)
                            {
                                drunkY--;
                            }
                            break;
                        case 3:
                            if (drunkY < map.Height - 2)
                            {
                                drunkY++;
                            }
                            break;
                    }

                    drunkLife--;
                }
            }
        }

        private void SetFloor(Map map, int i, int j)
        {
            var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
            tile.TileType = TileType.None;
            map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
        }

        private void FixGaps(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    if(tile.TileType == TileType.None)
                    {
                        var tile2 = map.GetTileFromLayer(MapLayerType.Wall, i, j - 1);
                        var tile8 = map.GetTileFromLayer(MapLayerType.Wall, i, j + 1);

                        if(tile2.TileType == TileType.Wall && tile8.TileType == TileType.Wall)
                        {
                            tile.TileType = TileType.Wall;
                            map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
                        }
                    }
                }
            }
        }

        public override Point GetPlayerStartingPosition(Map map)
        {
            return new Point(map.Width / 4, map.Height / 4);
        }
    }
}

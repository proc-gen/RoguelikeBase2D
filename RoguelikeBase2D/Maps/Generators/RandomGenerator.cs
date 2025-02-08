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

        private void ProcessWallBorders(Map map)
        {
            bool[] isWallOrBorder = new bool[9];
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);

                    if (tile.TileType == TileType.Wall)
                    {
                        isWallOrBorder[0] = IsWallOrBorder(map, i - 1, j - 1);
                        isWallOrBorder[1] = IsWallOrBorder(map, i, j - 1);
                        isWallOrBorder[2] = IsWallOrBorder(map, i + 1, j - 1);
                        isWallOrBorder[3] = IsWallOrBorder(map, i - 1, j);
                        isWallOrBorder[4] = IsWallOrBorder(map, i, j);
                        isWallOrBorder[5] = IsWallOrBorder(map, i + 1, j);
                        isWallOrBorder[6] = IsWallOrBorder(map, i - 1, j + 1);
                        isWallOrBorder[7] = IsWallOrBorder(map, i, j + 1);
                        isWallOrBorder[8] = IsWallOrBorder(map, i + 1, j + 1);

                        int sum = 0;
                        for (int k = 0; k < 9; k++)
                        {
                            if (isWallOrBorder[k])
                            {
                                sum += (int)Math.Pow(2, k + 1);
                            }
                        }

                        tile.TileType = (TileType)sum;
                        map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
                    }
                }
            }
        }

        private void ProcessWalls(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    if (tile.TileType.IsBorder() && j < (map.Height - 2))
                    {
                        var tileBelow1 = map.GetTileFromLayer(MapLayerType.Wall, i, j + 1);
                        var tileBelow2 = map.GetTileFromLayer(MapLayerType.Wall, i, j + 2);

                        if (tileBelow1.TileType == TileType.None && tileBelow2.TileType == TileType.None)
                        {
                            var tile4 = IsBorder(map, i - 1, j);
                            var tile6 = IsBorder(map, i + 1, j);
                            var tile7 = IsBorder(map, i - 1, j + 1);
                            var tile9 = IsBorder(map, i + 1, j + 1);

                            if ((tile7 && tile9) ||
                                (!tile4 && !tile6 && !tile7 && !tile9) ||
                                (!tile4 && !tile7 && tile9) ||
                                (!tile6 && tile7 && !tile9))
                            {
                                tileBelow1.TileType = TileType.WallTopSingle;
                                tileBelow2.TileType = TileType.WallBottomSingle;
                            }
                            else if ((tile6 && tile7 && !tile9) ||
                                        (!tile4 && tile6 && !tile7))
                            {
                                tileBelow1.TileType = TileType.WallTopLeft;
                                tileBelow2.TileType = TileType.WallBottomLeft;
                            }
                            else if ((tile4 && !tile7 && tile9) ||
                                        (tile4 && !tile6 && !tile9))
                            {
                                tileBelow1.TileType = TileType.WallTopRight;
                                tileBelow2.TileType = TileType.WallBottomRight;
                            }
                            else
                            {
                                tileBelow1.TileType = TileType.WallTopMiddle;
                                tileBelow2.TileType = TileType.WallBottomMiddle;
                            }

                            map.SetTileInLayer(MapLayerType.Wall, i, j + 1, tileBelow1);
                            map.SetTileInLayer(MapLayerType.Wall, i, j + 2, tileBelow2);
                        }
                    }
                }
            }
        }

        private bool IsWallOrBorder(Map map, int i, int j)
        {
            if (i < 0 || j < 0 || i >= map.Width || j >= map.Height)
            {
                return true;
            }

            return map.GetTileFromLayer(MapLayerType.Wall, i, j).TileType.IsWallOrBorder();
        }

        private bool IsBorder(Map map, int i, int j)
        {
            if (i < 0 || j < 0 || i >= map.Width || j >= map.Height)
            {
                return true;
            }

            return map.GetTileFromLayer(MapLayerType.Wall, i, j).TileType.IsBorder();
        }


        public override Point GetPlayerStartingPosition(Map map)
        {
            return new Point(map.Width / 4, map.Height / 4);
        }
    }
}

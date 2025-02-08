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

            return map;
        }

        private void PreProcessMap(Map map)
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

        private bool IsWallOrBorder(Map map, int i, int j)
        {
            if(i < 0 || j < 0 || i >= map.Width || j >= map.Height)
            {
                return false;
            }

            return map.GetTileFromLayer(MapLayerType.Wall, i, j).TileType.IsWallOrBorder();
        }

        public override Point GetPlayerStartingPosition(Map map)
        {
            return new Point(map.Width / 2, map.Height / 2);
        }
    }
}

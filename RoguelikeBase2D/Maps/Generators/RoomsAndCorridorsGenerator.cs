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
    public class RoomsAndCorridorsGenerator : Generator
    {
        List<Rectangle> Rooms;
        SeededRandom random;
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);
            Rooms = new List<Rectangle>();
            // random = new SeededRandom(506787069);
            random = new SeededRandom(Random.Shared.Next());
            map.Seed = random.Seed;
            
            PreProcessMap(map);
            ProcessWallBorders(map);
            //ProcessWalls(map);

            return map;
        }

        private void PreProcessMap(Map map)
        {
            FillMap(map);
            CreateRooms(map);
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
            int maxRooms = 30, minSize = 6, maxSize = 8;

            for (int i = 0; i < maxRooms; i++)
            {
                int roomWidth = random.Next(minSize, maxSize);
                int roomHeight = random.Next(minSize, maxSize);
                int x = random.Next(1, map.Width - roomWidth - 1) - 1;
                int y = random.Next(1, map.Height - roomHeight - 1) - 1;

                Rectangle room = new Rectangle(x, y, roomWidth, roomHeight);
                Rectangle compareRoom = new Rectangle(x, y, roomWidth, roomHeight);
                compareRoom.Inflate(1, 1);

                bool canAdd = true;
                
                if (Rooms.Any() && Rooms.Exists(a => a.Intersects(compareRoom)))
                {
                    canAdd = false;
                }

                if (canAdd)
                {
                    ApplyRoomToMap(map, room);
                    if (Rooms.Any())
                    {
                        ApplyCorridorToMap(map, room, random);
                    }
                    Rooms.Add(room);
                }
            }

            ApplyCorridorToMap(map, Rooms.First(), random);
        }

        private void ApplyRoomToMap(Map map, Rectangle room)
        {
            for (int i = room.X + 1; i <= room.X + room.Width; i++)
            {
                for (int j = room.Y + 1; j <= room.Y + room.Height; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    tile.TileType = TileType.None;
                    map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
                }
            }
        }

        private void ApplyCorridorToMap(Map map, Rectangle room, Random random)
        {
            
                Point newCenter = room.Center;
                Point oldCenter = Rooms.Last().Center;

                if (random.Next(0, 2) == 1)
                {
                    ApplyHorizontalTunnel(map, oldCenter.X, newCenter.X, oldCenter.Y);
                    ApplyVerticalTunnel(map, oldCenter.Y, newCenter.Y, newCenter.X);
                }
                else
                {
                    ApplyVerticalTunnel(map, oldCenter.Y, newCenter.Y, oldCenter.X);
                    ApplyHorizontalTunnel(map, oldCenter.X, newCenter.X, newCenter.Y);
                }
            
        }

        private void ApplyHorizontalTunnel(Map map, int x1, int x2, int y)
        {
            for (int i = Math.Min(x1, x2); i <= Math.Max(x1, x2); i++)
            {
                var tile = map.GetTileFromLayer(MapLayerType.Wall, i, y);
                var tile2 = map.GetTileFromLayer(MapLayerType.Wall, i, y - 1);
                var tile8 = map.GetTileFromLayer(MapLayerType.Wall, i, y + 1);
                
                tile.TileType = TileType.None;
                tile2.TileType = TileType.None;
                tile8.TileType = TileType.None;

                map.SetTileInLayer(MapLayerType.Wall, i, y, tile);
                map.SetTileInLayer(MapLayerType.Wall, i, y - 1, tile2);
                map.SetTileInLayer(MapLayerType.Wall, i, y + 1, tile8);
            }
        }

        private void ApplyVerticalTunnel(Map map, int y1, int y2, int x)
        {
            for (int j = Math.Min(y1, y2); j <= Math.Max(y1, y2); j++)
            {
                var tile = map.GetTileFromLayer(MapLayerType.Wall, x, j);
                tile.TileType = TileType.None;
                map.SetTileInLayer(MapLayerType.Wall, x, j, tile);
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

                        if (i == 11 && j == 8)
                        {
                            Console.WriteLine("Debug");
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
            return Rooms.First().Center;
        }
    }
}

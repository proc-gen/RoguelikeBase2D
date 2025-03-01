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
    public abstract class Generator
    {
        protected SeededRandom SeededRandom;

        public Generator() { }

        public abstract Map GenerateMap(int width, int height);
        public abstract Point GetPlayerStartingPosition(Map map);
        public abstract HashSet<Point> GetEnemySpawnPoints(Map map);
        public abstract HashSet<Point> GetItemSpawnPoints(Map map);
        public abstract Point GetMapExit(Map map);

        protected void ProcessWallBorders(Map map)
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

        protected void ProcessWalls(Map map)
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

        protected void RemoveHiddenFloors(Map map)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var wall = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    if (wall.TileType.IsBlocked())
                    {
                        var floor = map.GetTileFromLayer(MapLayerType.Floor, i, j);
                        floor.TileType = TileType.None;
                        map.SetTileInLayer(MapLayerType.Floor, i, j, floor);
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

            return map.GetTileFromLayer(MapLayerType.Wall, i, j).TileType.IsBlocked();
        }

        private bool IsBorder(Map map, int i, int j)
        {
            if (i < 0 || j < 0 || i >= map.Width || j >= map.Height)
            {
                return true;
            }

            return map.GetTileFromLayer(MapLayerType.Wall, i, j).TileType.IsBorder();
        }

        protected void ApplyRoomToMap(Map map, Rectangle room)
        {
            for (int i = room.X + 1; i < room.X + room.Width; i++)
            {
                for (int j = room.Y + 1; j < room.Y + room.Height; j++)
                {
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, i, j);
                    tile.TileType = TileType.None;
                    map.SetTileInLayer(MapLayerType.Wall, i, j, tile);
                }
            }
        }

        protected void ApplyRandomCorridorToMap(Map map, Point newCenter, Point oldCenter)
        {
            if (SeededRandom.Next(0, 2) == 1)
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

        protected void ApplyEfficienctCorridorToMap(Map map, Point newCenter, Point oldCenter)
        {
            if (Math.Abs(newCenter.X - oldCenter.X) > Math.Abs(newCenter.Y - oldCenter.Y))
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

        protected void ApplyHorizontalTunnel(Map map, int x1, int x2, int y)
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

        protected void ApplyVerticalTunnel(Map map, int y1, int y2, int x)
        {
            for (int j = Math.Min(y1, y2); j <= Math.Max(y1, y2); j++)
            {
                var tile = map.GetTileFromLayer(MapLayerType.Wall, x, j);
                tile.TileType = TileType.None;
                map.SetTileInLayer(MapLayerType.Wall, x, j, tile);
            }
        }

        protected void CreateVerticalDoor(Map map, Point point)
        {
            if (map.PointWithinBounds(point) && map.PointWithinBounds(point + PointConstants.Left) && map.PointWithinBounds(point + PointConstants.Right))
            {
                var wallTile = map.GetTileFromLayer(MapLayerType.Wall, point);
                var wallLeft = map.GetTileFromLayer(MapLayerType.Wall, point + PointConstants.Left);
                var wallRight = map.GetTileFromLayer(MapLayerType.Wall, point + PointConstants.Right);
                if (wallTile.TileType.IsPassable() &&
                            wallLeft.TileType.IsBlocked() &&
                            wallRight.TileType.IsBlocked())
                {
                    var bottomDoor = map.GetTileFromLayer(MapLayerType.Door, point + PointConstants.Down);
                    bottomDoor.TileType = TileType.DoorVerticalClosedBottom;
                    map.SetTileInLayer(MapLayerType.Door, point + PointConstants.Down, bottomDoor);

                    var topDoor = map.GetTileFromLayer(MapLayerType.Door, point);
                    topDoor.TileType = TileType.DoorVerticalClosedTop;
                    map.SetTileInLayer(MapLayerType.Door, point, topDoor);
                }
            }
        }

        protected void CreateHorizontalDoor(Map map, Point point)
        {
            if (map.PointWithinBounds(point) && map.PointWithinBounds(point + PointConstants.Down) && map.PointWithinBounds(point + PointConstants.Up + PointConstants.Up + PointConstants.Up))
            {
                var wallTile = map.GetTileFromLayer(MapLayerType.Wall, point);
                var wallDown = map.GetTileFromLayer(MapLayerType.Wall, point + PointConstants.Down);
                if (wallTile.TileType.IsPassable() &&
                            wallDown.TileType.IsBlocked() &&
                            point.Y - 3 < map.Height)
                {
                    var wallUp = map.GetTileFromLayer(MapLayerType.Wall, point + PointConstants.Up + PointConstants.Up + PointConstants.Up);

                    if (wallUp.TileType.IsBlocked())
                    {
                        var bottomDoor = map.GetTileFromLayer(MapLayerType.Door, point);
                        bottomDoor.TileType = TileType.DoorHorizontalClosedBottom;
                        map.SetTileInLayer(MapLayerType.Door, point, bottomDoor);

                        var topDoor = map.GetTileFromLayer(MapLayerType.Door, point + PointConstants.Up);
                        topDoor.TileType = TileType.DoorHorizontalClosedTop;
                        map.SetTileInLayer(MapLayerType.Door, point + PointConstants.Up, topDoor);

                        var toptopDoor = map.GetTileFromLayer(MapLayerType.Door, point + PointConstants.Up + PointConstants.Up);
                        toptopDoor.TileType = TileType.DoorHorizontalClosedTopTop;
                        map.SetTileInLayer(MapLayerType.Door, point + PointConstants.Up + PointConstants.Up, toptopDoor);
                    }
                }
            }
        }
    }
}

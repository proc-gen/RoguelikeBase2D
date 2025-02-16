using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public static class FieldOfView
    {
        public static void CalculatePlayerFOV(GameWorld world)
        {
            world.PlayerFov = CalculateFOV(world, world.PlayerRef);

            foreach (var point in world.PlayerFov)
            {
                foreach (var layer in MapLayerTypeExtensions.AllLayers)
                {
                    var tile = world.Map.GetTileFromLayer(layer, point);
                    tile.IsExplored = true;
                    world.Map.SetTileInLayer(layer, point, tile);
                }
            }
        }

        public static HashSet<Point> CalculateFOV(GameWorld world, EntityReference entity)
        {
            var entityPosition = entity.Entity.Get<Position>();
            var entityViewDistance = entity.Entity.Get<ViewDistance>();

            return CalculateFOV(world, entityPosition.Point, entityViewDistance.Distance);
        }

        public static HashSet<Point> CalculateFOV(GameWorld world, Point originPoint, int viewDistance)
        {
            HashSet<Point> fov = new HashSet<Point>(viewDistance * viewDistance * 4);

            HashSet<Point> borderPoints = GetBorderPointsInSquare(world.Map, originPoint, viewDistance);
            HashSet<Point> pointsToCheck = new HashSet<Point>();
            foreach (var borderPoint in borderPoints)
            {
                HashSet<Point> pointsToBorder = GetPointsInLine(originPoint, borderPoint);
                bool hitWall = false;
                foreach (var point in pointsToBorder)
                {
                    if (Vector2.Distance(originPoint.ToVector2(), point.ToVector2()) >= viewDistance)
                    {
                        break;
                    }
                    if (!hitWall)
                    {
                        fov.Add(point);

                        if (world.Map.GetTileFromLayer(MapLayerType.Wall, point).TileType.IsBorder())
                        {
                            hitWall = true;
                        }
                    }
                    else
                    {
                        pointsToCheck.Add(point);
                    }
                }
            }

            fov = PostProcessPoints(world.Map, pointsToCheck, fov, originPoint);

            return fov;
        }

        private static HashSet<Point> GetBorderPointsInSquare(Map map, Point entityPosition, int range)
        {
            HashSet<Point> borderCells = new HashSet<Point>(range * 8);
            for (int i = Math.Max(entityPosition.X - range, 0);
                i < Math.Min(entityPosition.X + range + 1, map.Width);
                i++)
            {
                for (int j = Math.Max(entityPosition.Y - range, 0);
                    j < Math.Min(entityPosition.Y + range + 1, map.Height);
                    j++)
                {
                    if (i == Math.Max(entityPosition.X - range, 0) ||
                        j == Math.Max(entityPosition.Y - range, 0) ||
                        i == Math.Min(entityPosition.X + range, map.Width - 1) ||
                        j == Math.Min(entityPosition.Y + range, map.Height - 1))
                    {
                        borderCells.Add(new Point(i, j));
                    }
                }
            }

            return borderCells;
        }

        private static HashSet<Point> GetPointsInLine(Point origin, Point destination)
        {
            HashSet<Point> linePoints = new HashSet<Point>() { origin };

            int dx = Math.Abs(destination.X - origin.X);
            int dy = Math.Abs(destination.Y - origin.Y);

            Point sx = new Point(origin.X < destination.X ? 1 : -1, 0);
            Point sy = new Point(0, origin.Y < destination.Y ? 1 : -1);
            int err = dx - dy;

            Point current = origin;

            do
            {
                int errorCheck = 2 * err;

                if (errorCheck > -dy)
                {
                    err -= dy;
                    current += sx;
                }
                if (errorCheck < dx)
                {
                    err += dx;
                    current += sy;
                }

                linePoints.Add(current);
            } while (current != destination);

            return linePoints;
        }

        private static HashSet<Point> PostProcessPoints(Map map, HashSet<Point> pointsToCheck, HashSet<Point> fov, Point origin)
        {
            foreach (Point point in pointsToCheck)
            {
                if (!fov.Contains(point)
                    && map.GetTileFromLayer(MapLayerType.Wall, point).TileType.IsBorder())
                {
                    int x1 = point.X;
                    int y1 = point.Y;
                    int x2 = point.X;
                    int y2 = point.Y;

                    if (point.Y > origin.Y)
                    {
                        y1 = point.Y - 1;
                    }
                    else if (point.Y < origin.Y)
                    {
                        y1 = point.Y + 1;
                    }

                    if (point.X > origin.X)
                    {
                        x2 = point.X - 1;
                    }
                    else if (point.X < origin.X)
                    {
                        x2 = point.X + 1;
                    }

                    Point point1 = new Point(x1, y1);
                    Point point2 = new Point(x2, y2);
                    Point point3 = new Point(x2, y1);
                    Point point4 = new Point(x1, y2);

                    if ((map.GetTileFromLayer(MapLayerType.Wall, point1).TileType.IsBorder() && fov.Contains(point1))
                        || (map.GetTileFromLayer(MapLayerType.Wall, point2).TileType.IsBorder() && fov.Contains(point2))
                        || (map.GetTileFromLayer(MapLayerType.Wall, point3).TileType.IsBorder() && fov.Contains(point3))
                        || (map.GetTileFromLayer(MapLayerType.Wall, point4).TileType.IsBorder() && fov.Contains(point4)))
                    {
                        fov.Add(point);
                    }
                }
            }
            return fov;
        }

    }
}

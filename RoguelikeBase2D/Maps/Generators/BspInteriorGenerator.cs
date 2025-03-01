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
    public class BspInteriorGenerator : Generator
    {
        const int MinRoomWidth = 6;
        const int MinRoomHeight = 12;
        List<Rectangle> Rooms;
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);
            Rooms = new List<Rectangle>();
            SeededRandom = SeededRandom.New();
            map.Seed = SeededRandom.Seed;

            PreProcessMap(map);
            ProcessWallBorders(map);
            ProcessWalls(map);
            RemoveHiddenFloors(map);
            SetMapExit(map);

            return map;
        }

        private void PreProcessMap(Map map)
        {
            FillMap(map);
            CreateRooms(map);
            AddDoors(map);
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
            var first = new Rectangle(2, 2, map.Width - 3, map.Height - 3);
            Rooms.Add(first);
            AddSubrectangles(first);

            for (int i = 0; i < Rooms.Count - 1; i++)
            {
                ApplyRoomToMap(map, Rooms[i]);
                ApplyEfficienctCorridorToMap(map, Rooms[i].Center, Rooms[i + 1].Center);
            }
            ApplyRoomToMap(map, Rooms.Last());
        }

        private void AddDoors(Map map)
        {
            foreach (var room in Rooms)
            {
                for (int i = room.X; i < room.X + room.Width; i++)
                {
                    var point = new Point(i, room.Y);
                    CreateVerticalDoor(map, point);

                    point = new Point(i, room.Y + room.Height - 1);
                    CreateVerticalDoor(map, point);
                }

                for (int j = room.Y; j < room.Y + room.Height; j++)
                {
                    var point = new Point(room.X, j);
                    CreateHorizontalDoor(map, point);

                    point = new Point(room.X + room.Width, j);
                    CreateHorizontalDoor(map, point);
                }
            }
        }

        private void AddSubrectangles(Rectangle parentRectangle)
        {
            if (Rooms.Any())
            {
                Rooms.RemoveAt(Rooms.Count - 1);
            }

            int width = parentRectangle.Width,
                height = parentRectangle.Height,
                halfWidth = parentRectangle.Width / 2,
                halfHeight = parentRectangle.Height / 2,
                split = SeededRandom.Next(100);

            if (split < 50)
            {
                var h1 = new Rectangle(parentRectangle.X, parentRectangle.Y, halfWidth - 1, height);
                var h2 = new Rectangle(parentRectangle.X + halfWidth, parentRectangle.Y, halfWidth, height);

                Rooms.Add(h1);
                if (halfWidth > MinRoomWidth && height > MinRoomHeight)
                {
                    AddSubrectangles(h1);
                }

                Rooms.Add(h2);
                if (halfWidth > MinRoomWidth && height > MinRoomHeight)
                {
                    AddSubrectangles(h2);
                }
            }
            else
            {
                var v1 = new Rectangle(parentRectangle.X, parentRectangle.Y, width, halfHeight - 1);
                var v2 = new Rectangle(parentRectangle.X, parentRectangle.Y + halfHeight, width, halfHeight);

                Rooms.Add(v1);
                if (halfHeight > MinRoomHeight && width > MinRoomWidth)
                {
                    AddSubrectangles(v1);
                }

                Rooms.Add(v2);
                if (halfHeight > MinRoomHeight && width > MinRoomWidth)
                {
                    AddSubrectangles(v2);
                }
            }
        }


        public override Point GetPlayerStartingPosition(Map map)
        {
            Point pos = Rooms.First().Center;
            Point down = new Point(0, 1);

            var tile = map.GetTileFromLayer(MapLayerType.Wall, pos);
            while (tile.TileType.IsBlocked())
            {
                pos += down;
                tile = map.GetTileFromLayer(MapLayerType.Wall, pos);
            }

            return pos;
        }

        public override HashSet<Point> GetEnemySpawnPoints(Map map)
        {
            HashSet<Point> points = new HashSet<Point>();

            var playerStart = GetPlayerStartingPosition(map);

            foreach (var room in Rooms)
            {
                int numSpawns = SeededRandom.Next(0, 4);
                int tries = 0;
                HashSet<Point> spawnsForRoom = new HashSet<Point>();
                while (spawnsForRoom.Count < numSpawns && tries < 30)
                {
                    var spawn = new Point(room.X + SeededRandom.Next(1, room.Width), room.Y + SeededRandom.Next(1, room.Height));
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, spawn);
                    if (spawn != playerStart && !tile.TileType.IsBlocked())
                    {
                        spawnsForRoom.Add(spawn);
                    }
                }

                foreach (var spawn in spawnsForRoom)
                {
                    points.Add(spawn);
                }
            }

            return points;
        }

        public override HashSet<Point> GetItemSpawnPoints(Map map)
        {
            HashSet<Point> points = new HashSet<Point>();

            var playerStart = GetPlayerStartingPosition(map);

            foreach (var room in Rooms)
            {
                int numSpawns = Math.Max(SeededRandom.Next(0, 5) - 2, 0);
                int tries = 0;
                HashSet<Point> spawnsForRoom = new HashSet<Point>();
                while (spawnsForRoom.Count < numSpawns && tries < 30)
                {
                    var spawn = new Point(room.X + SeededRandom.Next(1, room.Width), room.Y + SeededRandom.Next(1, room.Height));
                    var tile = map.GetTileFromLayer(MapLayerType.Wall, spawn);
                    if (spawn != playerStart && !tile.TileType.IsBlocked())
                    {
                        spawnsForRoom.Add(spawn);
                    }
                }

                foreach (var spawn in spawnsForRoom)
                {
                    points.Add(spawn);
                }
            }

            return points;
        }

        public override Point GetMapExit(Map map)
        {
            var lastRoom = Rooms.Last();
            var point = lastRoom.Center;
            var tile = map.GetTileFromLayer(MapLayerType.Wall, point);
            while (tile.TileType.IsBlocked())
            {
                point = new Point(
                    SeededRandom.Next(lastRoom.X + 1, lastRoom.X + lastRoom.Width - 1),
                    SeededRandom.Next(lastRoom.Y + 1, lastRoom.Y + lastRoom.Height - 1)
                    );
                tile = map.GetTileFromLayer(MapLayerType.Wall, point);
            }

            return point;
        }

        private void SetMapExit(Map map)
        {
            var exitPoint = GetMapExit(map);

            var tile = map.GetTileFromLayer(MapLayerType.FloorDecorations, exitPoint);
            tile.TileType = TileType.Exit;
            map.SetTileInLayer(MapLayerType.FloorDecorations, exitPoint, tile);
        }
    }
}

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
                int roomWidth = SeededRandom.Next(minSize, maxSize);
                int roomHeight = SeededRandom.Next(minSize, maxSize);
                int x = SeededRandom.Next(1, map.Width - roomWidth - 1) - 1;
                int y = SeededRandom.Next(1, map.Height - roomHeight - 1) - 1;

                Rectangle room = new Rectangle(x, y, roomWidth, roomHeight);
                Rectangle compareRoom = new Rectangle(x, y, roomWidth, roomHeight);
                compareRoom.Inflate(1, 1);

                if (!Rooms.Any() || !Rooms.Exists(a => a.Intersects(compareRoom)))
                {
                    ApplyRoomToMap(map, room);
                    if (Rooms.Any())
                    {
                        ApplyRandomCorridorToMap(map, room.Center, Rooms.Last().Center);
                    }
                    Rooms.Add(room);
                }
            }

            ApplyRandomCorridorToMap(map, Rooms.First().Center, Rooms.Last().Center);
        }

        public override Point GetPlayerStartingPosition(Map map)
        {
            Point pos = Rooms.First().Center;
            Point down = new Point(0, 1);

            var tile = map.GetTileFromLayer(MapLayerType.Wall, pos);
            while (tile.TileType.IsWallOrBorder())
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
                    if (spawn != playerStart && !tile.TileType.IsWallOrBorder())
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
                    if (spawn != playerStart && !tile.TileType.IsWallOrBorder())
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
            while (tile.TileType.IsWallOrBorder())
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

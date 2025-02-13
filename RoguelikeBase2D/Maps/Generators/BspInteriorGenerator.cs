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
            var first = new Rectangle(0, 1, map.Width - 2, map.Height - 2);
            Rooms.Add(first);
            AddSubrectangles(first);

            for (int i = 0; i < Rooms.Count - 1; i++)
            {
                ApplyRoomToMap(map, Rooms[i]);
                ApplyEfficienctCorridorToMap(map, Rooms[i].Center, Rooms[i + 1].Center);
            }
            ApplyRoomToMap(map, Rooms.Last());
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
            while (tile.TileType.IsWallOrBorder())
            {
                pos += down;
                tile = map.GetTileFromLayer(MapLayerType.Wall, pos);
            }

            return pos;
        }
    }
}

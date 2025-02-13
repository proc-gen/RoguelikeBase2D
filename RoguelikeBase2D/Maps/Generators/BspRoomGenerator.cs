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
    public class BspRoomGenerator : Generator
    {
        List<Rectangle> Rooms;
        List<Rectangle> Subsections;
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);
            Rooms = new List<Rectangle>();
            Subsections = new List<Rectangle>();
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
            AddSubSections(new Rectangle(2, 2, map.Width - 5, map.Height - 5));
            int roomCreateTries = 0;
            while (roomCreateTries < 240)
            {
                var subSection = GetRandomRectangle();
                var candidate = GetRandomSubRectangle(subSection);

                if (IsPossible(map, candidate))
                {
                    ApplyRoomToMap(map, candidate);
                    Rooms.Add(candidate);
                    AddSubSections(subSection);
                }

                roomCreateTries++;
            }

            for (int i = 0; i < Rooms.Count - 1; i++)
            {
                ApplyEfficienctCorridorToMap(map, Rooms[i].Center, Rooms[i + 1].Center);
            }
        }

        private void AddSubSections(Rectangle parentSection)
        {
            int width = parentSection.Width;
            int height = parentSection.Height;
            int halfWidth = width / 2 + 1;
            int halfHeight = height / 2 + 1;

            Subsections.Add(new Rectangle(parentSection.X, parentSection.Y, halfWidth, halfHeight));
            Subsections.Add(new Rectangle(parentSection.X, parentSection.Y + halfHeight, halfWidth, halfHeight));
            Subsections.Add(new Rectangle(parentSection.X + halfWidth, parentSection.Y, halfWidth, halfHeight));
            Subsections.Add(new Rectangle(parentSection.X + halfWidth, parentSection.Y + halfHeight, halfWidth, halfHeight));
        }

        private Rectangle GetRandomRectangle()
        {
            if (Subsections.Count == 1)
            {
                return Subsections[0];
            }

            return Subsections[SeededRandom.Next(Subsections.Count)];
        }

        private Rectangle GetRandomSubRectangle(Rectangle parentSection)
        {
            int width = parentSection.Width;
            int height = parentSection.Height;

            int newWidth = Math.Max(5, SeededRandom.Next(Math.Min(15, width) - 1) + 1);
            int newHeight = Math.Max(5, SeededRandom.Next(Math.Min(15, height) - 1) + 1);

            var subRectangle = new Rectangle(parentSection.X + SeededRandom.Next(1, 6) - 1, parentSection.Y + SeededRandom.Next(1, 6) - 1, newWidth, newHeight);

            return subRectangle;
        }

        public bool IsPossible(Map map, Rectangle newRoom)
        {
            bool retVal = true;

            var testRoom = new Rectangle(newRoom.Location, newRoom.Size); 
            testRoom.Inflate(2, 2);

            if (Rooms.Where(a => a.Intersects(testRoom)).Any())
            {
                retVal = false;
            }
            else if (testRoom.X < 1
                    || testRoom.Y < 1
                    || (testRoom.X + testRoom.Width) > map.Width - 2
                    || (testRoom.Y + testRoom.Height) > map.Height - 2)
            {
                retVal = false;
            }

            return retVal;
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

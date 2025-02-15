using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RoguelikeBase2D.Pathfinding
{
    public class SquareGrid : IWeightedGraph<Location>
    {
        public static readonly Location[] AdjacentLocations = new[]
        {
            new Location(new Point(0, -1)),
            new Location(new Point(0, 1)),
            new Location(new Point(-1, 0)),
            new Location(new Point(1, 0))
        };

        public GameWorld World { get; set; }
        
        public SquareGrid(GameWorld world)
        {
            World = world;
        }

        public bool InBounds(Location id)
        {
            return 0 <= id.Point.X && id.Point.X < World.Map.Width
                && 0 <= id.Point.Y && id.Point.Y < World.Map.Height;
        }

        public bool Passable(Location id)
        {
            var tile = World.Map.GetTileFromLayer(MapLayerType.Wall, id.Point);
            if(tile.TileType.IsWallOrBorder())
            {
                return false;
            }

            var entitiesAtLocation = World.PhysicsWorld.GetEntitiesAtLocation(id.Point);
            return entitiesAtLocation == null || !entitiesAtLocation.Any(a => a.Entity.Has<Blocker>());
        }

        public float Cost(Location a, Location b)
        {
            return 1;
        }

        public float Cost(Point a, Location b)
        {
            return Cost(new Location(a), b);
        }

        public IEnumerable<Location> GetNeighbors(Location id, Location end)
        {
            foreach (var direction in AdjacentLocations)
            {
                Location next = new Location(id.Point + direction.Point);
                if (InBounds(next) && (Passable(next) || next.Point == end.Point))
                {
                    yield return next;
                }
            }
        }

        public IEnumerable<Location> GetNeighbors(Point id, Location end)
        {
            return GetNeighbors(new Location(id), end);
        }
    }
}

using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Pathfinding
{
    public class PhysicsWorld
    {
        public Dictionary<Point, HashSet<EntityReference>> EntityLocations { get; private set; }

        public PhysicsWorld()
        {
            EntityLocations = new Dictionary<Point, HashSet<EntityReference>>();
        }

        public void Clear()
        {
            EntityLocations.Clear();
        }

        public void Populate(World world)
        {
            Clear();
            QueryDescription query = new QueryDescription().WithAll<Position>();
            world.Query(in query, (Entity entity, ref Position pos) =>
            {
                var reference = entity.Reference();
                AddEntity(reference, pos.Point);
            });
        }

        public void AddEntity(EntityReference entity, Point point)
        {
            if (!EntityLocations.ContainsKey(point)) 
            {
                EntityLocations[point] = new HashSet<EntityReference>() { entity };
            }
            else
            {
                EntityLocations[point].Add(entity);
            }
        }

        public void MoveEntity(EntityReference entity, Point oldPoint, Point newPoint)
        {
            RemoveEntity(entity, oldPoint);
            AddEntity(entity, newPoint);
        }

        public void RemoveEntity(EntityReference entity, Point point) 
        {
            EntityLocations[point].Remove(entity);
        }

        public HashSet<EntityReference> GetEntitiesAtLocation(Point point) 
        {
            return EntityLocations.TryGetValue(point, out HashSet<EntityReference> value) ? value : null;
        }
    }
}

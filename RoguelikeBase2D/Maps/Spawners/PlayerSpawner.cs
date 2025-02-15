using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Spawners
{
    public class PlayerSpawner : ISpawner
    {
        public PlayerSpawner() { }

        public void SpawnEntitiesForPoints(GameWorld world, HashSet<Point> points)
        {
            
        }

        public void SpawnEntityForPoint(GameWorld world, Point point)
        {
            if (world.PlayerRef == EntityReference.Null)
            {
                world.PlayerRef = world.World.Create(
                    new Player(),
                    new Position() { Point = point }
                ).Reference();
            }
            else
            {
                var position = world.PlayerRef.Entity.Get<Position>();
                position.Point = point;
                world.PlayerRef.Entity.Set(position);
            }
        }
    }
}

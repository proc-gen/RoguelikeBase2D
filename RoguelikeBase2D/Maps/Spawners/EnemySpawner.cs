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
    public class EnemySpawner : ISpawner
    {
        public void SpawnEntitiesForPoints(GameWorld world, HashSet<Point> points)
        {
            foreach (var point in points)
            {
                SpawnEntityForPoint(world, point);
            }
        }

        public void SpawnEntityForPoint(GameWorld world, Point point)
        {
            world.World.Create(
                    new Blocker(),
                    new Enemy(),
                    new Position() { Point = point },
                    new ViewDistance() { Distance = 5 },
                    new Input(),
                    new SpriteInfo() { Sprite = "goblin", Width = 48, Height = 48 }
                ).Reference();
        }
    }
}

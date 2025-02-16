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
                    new Blocker(),
                    new Position() { Point = point },
                    new ViewDistance() { Distance = 7 },
                    new Input(),
                    new SpriteInfo() { Sprite = "player", Width = 48, Height = 48 },
                    new CombatStats()
                    {
                        MaxHealth = 30,
                        CurrentHealth = 30,
                        BaseStrength = 14,
                        CurrentStrength = 14,
                        BaseArmor = 0,
                        CurrentArmor = 0,
                    },
                    new CombatEquipment()
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

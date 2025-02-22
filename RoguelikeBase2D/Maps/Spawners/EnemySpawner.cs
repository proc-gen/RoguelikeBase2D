using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.Datasets;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Utils;
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
            var enemyContainers = EnemyDataset.GetEnemyContainersForDepth(world.Depth);
            SeededRandom random = SeededRandom.New();

            foreach (var point in points)
            {
                SpawnEntityForPoint(world, random, enemyContainers, point);
            }
        }

        public void SpawnEntityForPoint(GameWorld world, SeededRandom random, List<EnemyContainer> enemyContainers, Point point)
        {
            EnemyContainer enemyContainer = enemyContainers.FirstOrDefault();
            if(enemyContainers.Count > 1)
            {
                enemyContainer = enemyContainers[random.Next(0, enemyContainers.Count)];
            }

            world.World.Create(
                    new Blocker(),
                    new Enemy(),
                    new Identity() { Name = enemyContainer.Name },
                    new Position() { Point = point },
                    new ViewDistance() { Distance = enemyContainer.ViewDistance },
                    new Input(),
                    new SpriteInfo() { Sprite = enemyContainer.Sprite, Width = 48, Height = 48 },
                    new CombatStats()
                    {
                        MaxHealth = enemyContainer.Health,
                        CurrentHealth = enemyContainer.Health,
                        BaseStrength = enemyContainer.Strength,
                        CurrentStrength = enemyContainer.Strength,
                        BaseArmor = enemyContainer.Armor,
                        CurrentArmor = enemyContainer.Armor,
                    },
                    new CombatEquipment()
                ).Reference();
        }
    }
}

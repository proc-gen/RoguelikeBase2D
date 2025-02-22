using RoguelikeBase2D.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Datasets
{
    public static class EnemyDataset
    {
        static List<EnemyContainer> enemyContainers = [
            new EnemyContainer(){
                Name = "Goblin",
                ViewDistance = 5,
                Sprite = "goblin",
                Health = 5,
                Strength = 12,
                Armor = 0,
                MinDepthSpawn = 0,
                MaxDepthSpawn = 5,
            },
            new EnemyContainer(){
                Name = "Ogre",
                ViewDistance = 5,
                Sprite = "ogre",
                Health = 15,
                Strength = 20,
                Armor = 0,
                MinDepthSpawn = 3,
                MaxDepthSpawn = 10,
            }
        ];

        public static List<EnemyContainer> GetEnemyContainersForDepth(int depth)
        {
            return enemyContainers.Where(a => a.MinDepthSpawn <= depth && a.MaxDepthSpawn >= depth).ToList();
        }
    }
}

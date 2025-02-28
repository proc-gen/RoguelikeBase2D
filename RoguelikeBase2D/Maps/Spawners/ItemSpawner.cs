using Microsoft.Xna.Framework;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.Datasets;
using RoguelikeBase2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Spawners
{
    public class ItemSpawner : ISpawner
    {
        public void SpawnEntitiesForPoints(GameWorld world, HashSet<Point> points)
        {
            var itemContainers = ItemDataset.GetItemContainersForDepth(world.Depth);
            SeededRandom random = SeededRandom.New();

            foreach (var point in points)
            {
                SpawnEntityForPoint(world, random, itemContainers, point);
            }
        }

        public void SpawnEntityForPoint(GameWorld world, SeededRandom random, List<ItemContainer> itemContainers, Point point)
        {
            ItemContainer itemContainer = itemContainers.FirstOrDefault();
            if (itemContainers.Count > 1)
            {
                itemContainer = itemContainers[random.Next(0, itemContainers.Count)];
            }

            ItemDataset.SpawnEntityAtPoint(world, itemContainer.Name, point);
        }
    }
}

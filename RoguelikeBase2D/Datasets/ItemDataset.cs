using Arch.Core;
using Arch.Core.Extensions;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Datasets
{
    public static class ItemDataset
    {
        static List<ItemContainer> itemContainers = [
            new ItemContainer(){
                Name = "Health Potion",
                Sprite = "health-potion",
                MinDepthSpawn = 0,
                MaxDepthSpawn = 999,
            }
        ];

        public static List<ItemContainer> GetItemContainersForDepth(int depth)
        {
            return itemContainers.Where(a => a.MinDepthSpawn <= depth && a.MaxDepthSpawn >= depth).ToList();
        }

        public static EntityReference SpawnEntityForOwner(GameWorld world, string name, EntityReference owner)
        {
            var item = itemContainers.Where(a => a.Name == name).FirstOrDefault();
            if (item != null) 
            {
                return world.World.Create(
                    new Item(),
                    new Identity() { Name = name },
                    new Owner() { OwnerReference = owner },
                    new SpriteInfo() { Height = 48, Width = 48, Sprite = item.Sprite }
                ).Reference();
            }

            return EntityReference.Null;
        }
    }
}

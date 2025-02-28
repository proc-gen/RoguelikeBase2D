using Arch.Core;
using Arch.Core.Extensions;
using CommunityToolkit.HighPerformance;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Utils;
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
                Consumable = true,
                ConsumableType = ConsumableType.Health,
                EffectAmount = 5,
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
                List<object> components = [
                    new Item(),
                    new Identity() { Name = name },
                    new Owner() { OwnerReference = owner },
                    new SpriteInfo() { Height = 48, Width = 48, Sprite = item.Sprite }
                ];

                if (item.Consumable)
                {
                    components.Add(new Consumable() { ConsumableType = item.ConsumableType, Amount = item.EffectAmount });
                }
                
                return world.World.CreateFromArray(components.ToArray()).Reference();
            }

            return EntityReference.Null;
        }

        public static EntityReference SpawnEntityAtPoint(GameWorld world, string name, Point point)
        {
            var item = itemContainers.Where(a => a.Name == name).FirstOrDefault();
            if (item != null)
            {
                List<object> components = [
                    new Item(),
                    new Identity() { Name = name },
                    new Position() { Point = point },
                    new SpriteInfo() { Height = 48, Width = 48, Sprite = item.Sprite }
                ];

                if (item.Consumable)
                {
                    components.Add(new Consumable() { ConsumableType = item.ConsumableType, Amount = item.EffectAmount });
                }

                return world.World.CreateFromArray(components.ToArray()).Reference();
            }

            return EntityReference.Null;
        }
    }
}

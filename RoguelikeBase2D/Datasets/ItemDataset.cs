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
using System.Xml.Linq;

namespace RoguelikeBase2D.Datasets
{
    public static class ItemDataset
    {
        static List<ItemContainer> itemContainers = [
            new ConsumableItemContainer(){
                Name = "Health Potion",
                Sprite = "health-potion",
                Consumable = true,
                ConsumableType = ConsumableType.Health,
                EffectAmount = 5,
                MinDepthSpawn = 0,
                MaxDepthSpawn = 999,
            },
            new ArmorItemContainer(){
                Name = "Cloth Armor",
                Sprite = "cloth-armor",
                MinDepthSpawn = 0,
                MaxDepthSpawn = 2,
                Armor = 1,
            },
            new ArmorItemContainer(){
                Name = "Leather Armor",
                Sprite = "leather-armor",
                MinDepthSpawn = 2,
                MaxDepthSpawn = 5,
                Armor = 3,
            },
            new WeaponItemContainer(){
                Name = "Dagger",
                Sprite = "dagger",
                MinDepthSpawn = 0,
                MaxDepthSpawn = 2,
                MinDamage = 1,
                MaxDamage = 3,
                WeaponType = WeaponType.Melee,
            },
            new WeaponItemContainer(){
                Name = "Sword",
                Sprite = "sword",
                MinDepthSpawn = 2,
                MaxDepthSpawn = 5,
                MinDamage = 3,
                MaxDamage = 6,
                WeaponType = WeaponType.Melee,
            },
            new WeaponItemContainer(){
                Name = "Bow",
                Sprite = "bow",
                MinDepthSpawn = 1,
                MaxDepthSpawn = 4,
                MinDamage = 1,
                MaxDamage = 4,
                Range = 5,
                WeaponType = WeaponType.Ranged,
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
                List<object> components = GetItemGenericComponents(item);
                components.Add(new Owner() { OwnerReference = owner });
                components.AddRange(GetItemTypeSpecificComponents(item));

                return world.World.CreateFromArray(components.ToArray()).Reference();
            }

            return EntityReference.Null;
        }

        public static EntityReference SpawnEntityAtPoint(GameWorld world, string name, Point point)
        {
            var item = itemContainers.Where(a => a.Name == name).FirstOrDefault();
            if (item != null)
            {
                List<object> components = GetItemGenericComponents(item);
                components.Add(new Position() { Point = point });
                components.AddRange(GetItemTypeSpecificComponents(item));

                return world.World.CreateFromArray(components.ToArray()).Reference();
            }

            return EntityReference.Null;
        }

        private static List<object> GetItemGenericComponents(ItemContainer item)
        {
            return [
                new Item(),
                new Identity() { Name = item.Name },
                new SpriteInfo() { Height = 48, Width = 48, Sprite = item.Sprite }
            ];
        }

        private static List<object> GetItemTypeSpecificComponents(ItemContainer item)
        {
            var components = new List<object>();

            if (item is ConsumableItemContainer)
            {
                var consumableItem = (ConsumableItemContainer)item;
                components.Add(new Consumable() { ConsumableType = consumableItem.ConsumableType, Amount = consumableItem.EffectAmount });
            }
            else if (item is ArmorItemContainer)
            {
                var armorItem = (ArmorItemContainer)item;
                components.Add(new Armor() { Amount = armorItem.Armor });
            }
            else if (item is WeaponItemContainer)
            {
                var weaponItem = (WeaponItemContainer)item;
                components.Add(new Weapon() { Range = weaponItem.Range, MinDamage = weaponItem.MinDamage, MaxDamage = weaponItem.MaxDamage, WeaponType = weaponItem.WeaponType });
            }

            return components;
        }
    }
}

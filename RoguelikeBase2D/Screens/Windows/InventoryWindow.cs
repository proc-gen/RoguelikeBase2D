using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameWindow = RoguelikeBase2D.Screens.Generated.GameWindow;

namespace RoguelikeBase2D.Screens.Windows
{
    public class GroupInventoryItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public EntityReference Entity { get; set; }
    }

    public class InventoryWindow : Window
    {
        
        List<GroupInventoryItem> GroupedInventoryItems;
        int selectedItem = 0;
        QueryDescription ownedItemsQuery = new QueryDescription().WithAll<Item, Owner>();

        public InventoryWindow(GameScreen gameScreen, GameWorld world)
            : base(gameScreen, world)
        {
            GroupedInventoryItems = new List<GroupInventoryItem>();
        }

        public override void OpenWindow()
        {
            IsOpen = true;
            UpdateInventoryItems();
            UpdatePlayerEquipment();
        }

        public override void CloseWindow()
        {
            IsOpen = false;
        }

        public override void HandleKeyboard(KeyboardState kState) 
        {
            if (kState.IsKeyDown(Keys.Escape) || kState.IsKeyDown(Keys.I))
            {
                CloseInventory();
            }
            else if (kState.IsKeyDown(Keys.Up))
            {
                selectedItem = Math.Max(selectedItem - 1, 0);
                SetSelectedItem();
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                selectedItem = Math.Min(selectedItem + 1, GroupedInventoryItems.Count - 1);
                SetSelectedItem();
            }
            else if (GroupedInventoryItems.Count > 0)
            {
                if (kState.IsKeyDown(Keys.U))
                {
                    UseItem();
                }
                else if (kState.IsKeyDown(Keys.D))
                {
                    DropItem();
                }
            }
        }

        private void UseItem()
        {
            var item = GroupedInventoryItems[selectedItem].Entity;
            
            if (item.Entity.Has<Consumable>())
            {
                item.Entity.Add(new WantToUseItem());
            }
            else
            {
                var combatEquipment = World.PlayerRef.Entity.Get<CombatEquipment>();
                var old = item.Entity.Has<Armor>() ? combatEquipment.Armor : combatEquipment.Weapon;
                if (old != EntityReference.Null)
                {
                    old.Entity.Remove<Equipped>();
                }
                item.Entity.Add(new Equipped());
                if (item.Entity.Has<Armor>())
                {
                    combatEquipment.Armor = item;
                }
                else
                {
                    combatEquipment.Weapon = item;
                }
                World.PlayerRef.Entity.Set(combatEquipment);

                var ownerName = item.Entity.Get<Owner>().OwnerReference.Entity.Get<Identity>();
                var itemName = item.Entity.Get<Identity>();
                World.GameLog.Add(string.Format("{0} equipped {1}", ownerName.Name, itemName.Name));
            }

            GameScreen.MovePlayer(Point.Zero);
            CloseInventory();
        }

        private void DropItem()
        {
            var item = GroupedInventoryItems[selectedItem].Entity;

            var ownerPosition = item.Entity.Get<Owner>().OwnerReference.Entity.Get<Position>();
            var ownerName = item.Entity.Get<Owner>().OwnerReference.Entity.Get<Identity>();
            var itemName = item.Entity.Get<Identity>();

            Point targetPosition = Point.Zero;
            int fovDistanceForDrop = 0;
            do
            {
                var pointsToCheck = FieldOfView.CalculateFOV(World, ownerPosition.Point, fovDistanceForDrop);
                foreach (var point in pointsToCheck)
                {
                    var tile = World.Map.GetTileFromLayer(MapLayerType.Wall, point);
                    if (targetPosition == Point.Zero
                        && tile.TileType.IsPassable())
                    {
                        var entitiesAtLocation = World.PhysicsWorld.GetEntitiesAtLocation(point);
                        if (entitiesAtLocation == null || !entitiesAtLocation.Any(a => a.Entity.Has<Item>()))
                        {
                            targetPosition = point;
                        }
                    }
                }
                fovDistanceForDrop++;
            } while (targetPosition == Point.Zero);

            item.Entity.Remove<Owner>();
            item.Entity.Add(new Position() { Point = targetPosition });
            World.PhysicsWorld.AddEntity(item, targetPosition);

            World.LogEntry(string.Format("{0} dropped {1}", ownerName.Name, itemName.Name));
            GameScreen.MovePlayer(Point.Zero);
            CloseInventory();
        }

        private void CloseInventory()
        {
            GameScreen.CloseInventory();
        }

        private void SetSelectedItem()
        {
            var window = (GameWindow)GameScreen.MyraWindow;
            window.BackpackList.Items[selectedItem].IsSelected = true;
            GameScreen.InputDelayHelper.Reset();
        }

        private void UpdateInventoryItems()
        {
            var inventoryItems = new List<EntityReference>();
            GroupedInventoryItems.Clear();
            var window = (GameWindow)GameScreen.MyraWindow;
            window.BackpackList.Items.Clear();

            World.World.Query(in ownedItemsQuery, (Entity entity, ref Owner owner) =>
            {
                if (owner.OwnerReference == World.PlayerRef && !entity.Has<Equipped>())
                {
                    inventoryItems.Add(entity.Reference());
                }
            });

            if (inventoryItems.Count > 0)
            {
                foreach (var item in inventoryItems)
                {
                    if (item.Entity.Has<Consumable>())
                    {
                        var groupedItem = GroupedInventoryItems.Where(a => a.Name == item.Entity.Get<Identity>().Name).FirstOrDefault();
                        if (groupedItem != null)
                        {
                            groupedItem.Count++;
                        }
                        else
                        {
                            GroupedInventoryItems.Add(new GroupInventoryItem() { Name = item.Entity.Get<Identity>().Name, Count = 1, Entity = item });
                        }
                    }
                    else
                    {
                        GroupedInventoryItems.Add(new GroupInventoryItem() { Name = item.Entity.Get<Identity>().Name, Count = 1, Entity = item });
                    }
                }

                for (int i = 0; i < GroupedInventoryItems.Count; i++)
                {
                    var item = GroupedInventoryItems[i];
                    string text = item.Count == 1 ? item.Name : string.Format("{0} ({1})", item.Name, item.Count);
                    window.BackpackList.Items.Add(new Myra.Graphics2D.UI.ListItem(text, null, item));
                }
                window.BackpackList.Items.First().IsSelected = true;
                selectedItem = 0;
            }
        }

        private void UpdatePlayerEquipment()
        {
            var window = (GameWindow)GameScreen.MyraWindow;
            var playerEquipment = World.PlayerRef.Entity.Get<CombatEquipment>();

            StringBuilder sb = new StringBuilder();

            sb.Append("Weapon: ");
            if(playerEquipment.Weapon != EntityReference.Null)
            {
                sb.AppendLine(playerEquipment.Weapon.Entity.Get<Identity>().Name);
            }
            else
            {
                sb.AppendLine("Unarmed");
            }

            sb.AppendLine();

            sb.Append("Armor: ");
            if (playerEquipment.Armor != EntityReference.Null)
            {
                sb.AppendLine(playerEquipment.Armor.Entity.Get<Identity>().Name);
            }
            else
            {
                sb.AppendLine("None");
            }

            window.EquipmentTextbox.Text = sb.ToString();
        }
    }
}

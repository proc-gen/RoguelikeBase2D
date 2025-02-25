using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Screens.Windows
{
    public class InventoryWindow : Window
    {
        GameScreen GameScreen;
        GameWorld World;
        List<EntityReference> InventoryItems;
        int selectedItem = 0;
        QueryDescription ownedItemsQuery = new QueryDescription().WithAll<Item, Owner>();

        public InventoryWindow(GameScreen gameScreen, GameWorld world) 
        {
            GameScreen = gameScreen;
            World = world;
            InventoryItems = new List<EntityReference>();
        }

        public override void OpenWindow()
        {
            IsOpen = true;
            UpdateInventoryItems();
        }

        public override void CloseWindow()
        {
            IsOpen = false;
        }

        public void HandleKeyboard(KeyboardState kState) 
        {
            if (kState.IsKeyDown(Keys.Escape) || kState.IsKeyDown(Keys.I))
            {
                GameScreen.CloseInventory();
            }
            else if (kState.IsKeyDown(Keys.Up))
            {
                selectedItem = Math.Max(selectedItem - 1, 0);
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                selectedItem = Math.Min(selectedItem + 1, 11);
            }
        }

        private void UpdateInventoryItems()
        {
            InventoryItems.Clear();
            World.World.Query(in ownedItemsQuery, (Entity entity, ref Owner owner) =>
            {
                if (owner.OwnerReference == World.PlayerRef && !entity.Has<Equipped>())
                {
                    InventoryItems.Add(entity.Reference());
                }
            });
        }
    }
}

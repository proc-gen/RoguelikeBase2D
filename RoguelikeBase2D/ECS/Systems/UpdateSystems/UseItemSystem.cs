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

namespace RoguelikeBase2D.ECS.Systems.UpdateSystems
{
    public class UseItemSystem : ArchSystem, IUpdateSystem
    {
        QueryDescription usedItemsToRemoveQuery = new QueryDescription().WithAll<WantToUseItem, Remove>();
        QueryDescription consumablesToUseQuery = new QueryDescription().WithAll<WantToUseItem, Consumable>().WithNone<Remove>();


        public UseItemSystem(GameWorld world)
            : base(world)
        {

        }

        public void Update(GameTime gameTime)
        {
            World.World.Query(in consumablesToUseQuery, (Entity entity, ref Identity identity, ref Owner owner, ref Consumable consumable) =>
            {
                var ownerIdentity = owner.OwnerReference.Entity.Get<Identity>();
                var ownerStats = owner.OwnerReference.Entity.Get<CombatStats>();
                bool consumed = false;
                switch (consumable.ConsumableType)
                {
                    case Constants.ConsumableType.Health:
                        if (ownerStats.CurrentHealth < ownerStats.MaxHealth)
                        {
                            int healAmount = Math.Min(consumable.Amount, ownerStats.MaxHealth - ownerStats.CurrentHealth);
                            ownerStats.CurrentHealth += healAmount;
                            consumed = true;

                            World.LogEntry(string.Concat(ownerIdentity.Name, " drank a ", identity.Name, " and healed for ", healAmount, "hp"));
                        }
                        else
                        {
                            World.LogEntry(string.Concat(ownerIdentity.Name, " is already at full health!"));
                        }
                        break;
                }

                if (consumed)
                {
                    owner.OwnerReference.Entity.Set(ownerStats);
                    entity.Add(new Remove());
                }
                else
                {
                    entity.Remove<WantToUseItem>();
                }
            });

            World.World.Destroy(in usedItemsToRemoveQuery);
        }
    }
}

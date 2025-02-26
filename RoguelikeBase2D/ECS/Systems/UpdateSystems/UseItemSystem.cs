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
        QueryDescription itemsToUseQuery = new QueryDescription().WithAll<WantToUseItem>();

        public UseItemSystem(GameWorld world)
            : base(world)
        {

        }

        public void Update(GameTime gameTime)
        {
            World.World.Query(in itemsToUseQuery, (Entity entity, ref Identity identity, ref Owner owner) =>
            {
                var ownerIdentity = owner.OwnerReference.Entity.Get<Identity>();

                World.LogEntry(string.Format("{0} used {1}", ownerIdentity.Name, identity.Name));
            });

            World.World.Destroy(in itemsToUseQuery);
        }
    }
}

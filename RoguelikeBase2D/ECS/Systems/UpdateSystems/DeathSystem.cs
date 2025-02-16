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
    public class DeathSystem : ArchSystem, IUpdateSystem
    {
        QueryDescription deathQuery = new QueryDescription().WithAll<Dead>();
        public DeathSystem(GameWorld world)
            : base(world)
        {
        }

        public void Update(GameTime gameTime)
        {
            World.World.Query(in deathQuery, (Entity entity, ref Position position) =>
            {
                World.PhysicsWorld.RemoveEntity(entity.Reference(), position.Point);
            });

            World.World.Destroy(in deathQuery);
        }
    }
}

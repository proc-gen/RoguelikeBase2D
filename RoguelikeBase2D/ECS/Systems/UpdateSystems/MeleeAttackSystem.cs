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
    public class MeleeAttackSystem : ArchSystem, IUpdateSystem
    {
        QueryDescription meleeAttacksQuery = new QueryDescription().WithAll<MeleeAttack>();

        public MeleeAttackSystem(GameWorld world)
            : base(world)
        {
        }

        public void Update(GameTime gameTime)
        {
            World.World.Query(in meleeAttacksQuery, (ref MeleeAttack meleeAttack) =>
            {
                if (!meleeAttack.Source.Entity.Has<Dead>())
                {
                    if (meleeAttack.Source.Entity.Has<Player>())
                    {
                        meleeAttack.Target.Entity.Add(new Dead());
                    }
                    else if (meleeAttack.Target.Entity.Has<Player>())
                    {
                        World.CurrentState = Constants.GameState.PlayerDeath;
                    }
                }

                World.World.Destroy(in meleeAttacksQuery);
            });
        }
    }
}

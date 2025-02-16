using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RoguelikeBase2D.ECS.Systems.UpdateSystems
{
    public class MeleeAttackSystem : ArchSystem, IUpdateSystem
    {
        QueryDescription meleeAttacksQuery = new QueryDescription().WithAll<MeleeAttack>();
        SeededRandom SeededRandom;
        public MeleeAttackSystem(GameWorld world)
            : base(world)
        {
            SeededRandom = new SeededRandom(Random.Shared.Next());
        }

        public void Update(GameTime gameTime)
        {
            World.World.Query(in meleeAttacksQuery, (ref MeleeAttack meleeAttack) =>
            {
                if (!meleeAttack.Source.Entity.Has<Dead>())
                {
                    var sourceStats = meleeAttack.Source.Entity.Get<CombatStats>();
                    var sourceEquipment = meleeAttack.Source.Entity.Get<CombatEquipment>();
                    var targetStats = meleeAttack.Target.Entity.Get<CombatStats>();
                    var targetEquipment = meleeAttack.Target.Entity.Get<CombatEquipment>();

                    int damage = CalculateDamage(sourceStats, targetStats, sourceEquipment, targetEquipment);
                    if (damage > 0)
                    {
                        targetStats.CurrentHealth = Math.Max(0, targetStats.CurrentHealth - damage);
                        if (targetStats.CurrentHealth == 0)
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
                        meleeAttack.Target.Entity.Set(targetStats);
                    }
                }
            });

            World.World.Destroy(in meleeAttacksQuery);
        }

        private int CalculateDamage(CombatStats sourceStats, CombatStats targetStats, CombatEquipment sourceEquipment, CombatEquipment targetEquipment)
        {
            int damageStat = (int)((sourceStats.CurrentStrength - 10f) / 2f);
            int damage = SeededRandom.Next(damageStat, damageStat * 3 / 2);
            int damageReduction = targetStats.CurrentArmor;

            return damage - damageReduction;
        }
    }
}

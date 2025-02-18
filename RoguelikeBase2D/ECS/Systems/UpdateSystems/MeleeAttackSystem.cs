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
                    var source = meleeAttack.Source.Entity.Get<CombatStats, CombatEquipment, Identity>();

                    var target = meleeAttack.Target.Entity.Get<CombatStats, CombatEquipment, Identity>();

                    int damage = CalculateDamage(source.t0, target.t0, source.t1, target.t1);
                    if (damage > 0)
                    {
                        target.t0.CurrentHealth = Math.Max(0, target.t0.CurrentHealth - damage);
                        if (target.t0.CurrentHealth == 0)
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
                        meleeAttack.Target.Entity.Set(target.t0);
                    }

                    LogAttack(damage, target.t0, source.t2, target.t2);
                }
            });

            World.World.Destroy(in meleeAttacksQuery);
        }

        private void LogAttack(int damage, CombatStats target, Identity sourceIdentity, Identity targetIdentity)
        {
            if (damage > 0)
            {
                World.LogEntry(string.Format("{0} hit {1} for {2} damage", sourceIdentity.Name, targetIdentity.Name, damage));
                if (target.CurrentHealth == 0)
                {
                    World.LogEntry(string.Format("{0} killed {1}", sourceIdentity.Name, targetIdentity.Name));
                }
            }
            else
            {
                World.LogEntry(string.Format("{0} failed to hurt {1}", sourceIdentity.Name, targetIdentity.Name));
            }
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

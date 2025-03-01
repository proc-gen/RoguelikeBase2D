using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Screens;
using RoguelikeBase2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Systems.UpdateSystems
{
    public class RangedAttackSystem : ArchSystem, IUpdateSystem
    {
        QueryDescription rangedAttacksQuery = new QueryDescription().WithAll<RangedAttack>();
        SeededRandom SeededRandom;
        public RangedAttackSystem(GameWorld world)
            : base(world)
        {
            SeededRandom = SeededRandom.New();
        }

        public void Update(GameTime gameTime)
        {
            World.World.Query(in rangedAttacksQuery, (ref RangedAttack rangedAttack) =>
            {
                if (!rangedAttack.Source.Entity.Has<Dead>())
                {
                    var source = rangedAttack.Source.Entity.Get<CombatStats, CombatEquipment, Identity>();

                    var target = rangedAttack.Target.Entity.Get<CombatStats, CombatEquipment, Identity>();

                    int damage = CalculateDamage(source.t0, target.t0, source.t1, target.t1);
                    if (damage > 0)
                    {
                        target.t0.CurrentHealth = Math.Max(0, target.t0.CurrentHealth - damage);
                        if (target.t0.CurrentHealth == 0)
                        {
                            if (rangedAttack.Source.Entity.Has<Player>())
                            {
                                rangedAttack.Target.Entity.Add(new Dead());
                            }
                            else if (rangedAttack.Target.Entity.Has<Player>())
                            {
                                World.CurrentState = GameState.PlayerDeath;
                            }
                        }
                        rangedAttack.Target.Entity.Set(target.t0);
                    }

                    LogAttack(damage, target.t0, source.t2, target.t2);

                    if (source.t1.Weapon.Entity.Get<Weapon>().WeaponType == WeaponType.Melee)
                    {
                        World.LogEntry(string.Format("{0} is now unarmed after throwing {1}", source.t2.Name, source.t1.Weapon.Entity.Get<Identity>().Name));
                        ThrowItem(source.t1.Weapon, rangedAttack.Target.Entity.Get<Position>().Point);
                    }
                }
            });

            World.World.Destroy(in rangedAttacksQuery);
        }

        private void LogAttack(int damage, CombatStats target, Identity sourceIdentity, Identity targetIdentity)
        {
            if (damage > 0)
            {
                World.LogEntry(string.Format("{0} hit {1} for {2} damage from far away", sourceIdentity.Name, targetIdentity.Name, damage));
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
            int damage = 0;

            if (sourceEquipment.Weapon != EntityReference.Null)
            {
                var weapon = sourceEquipment.Weapon.Entity.Get<Weapon>();
                if (weapon.WeaponType == WeaponType.Melee)
                {
                    damage += weapon.MinDamage;
                }
                else
                {
                    damage += SeededRandom.Next(weapon.MinDamage, weapon.MaxDamage + 1);
                }
            }

            int damageReduction = targetStats.CurrentArmor;
            if (targetEquipment.Armor != EntityReference.Null)
            {
                var armor = targetEquipment.Armor.Entity.Get<Armor>();
                damageReduction += armor.Amount;
            }


            return damage - damageReduction;
        }

        private void ThrowItem(EntityReference item, Point thrownPosition)
        { 
            var ownerName = item.Entity.Get<Owner>().OwnerReference.Entity.Get<Identity>();
            var itemName = item.Entity.Get<Identity>();

            Point targetPosition = Point.Zero;
            int fovDistanceForDrop = 0;
            do
            {
                var pointsToCheck = FieldOfView.CalculateFOV(World, thrownPosition, fovDistanceForDrop);
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

            item.Entity.Remove<Owner, Equipped>();
            item.Entity.Add(new Position() { Point = targetPosition });
            World.PhysicsWorld.AddEntity(item, targetPosition);
        }
    }
}

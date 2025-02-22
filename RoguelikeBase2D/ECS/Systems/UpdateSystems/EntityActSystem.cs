using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Systems.UpdateSystems
{
    internal class EntityActSystem : ArchSystem, IUpdateSystem
    {
        QueryDescription nonPlayerQuery = new QueryDescription().WithAll<Position, Input>().WithNone<Player>();

        public EntityActSystem(GameWorld world)
            : base(world)
        {
        }

        public void Update(GameTime gameTime)
        {
            switch (World.CurrentState)
            {
                case GameState.PlayerTurn:
                    HandlePlayerTurn();
                    break;
                case GameState.ComputerTurn:
                    HandleComputerTurn();
                    break;
            }
        }

        private void HandlePlayerTurn()
        {
            var playerRefs = World.PlayerRef.Entity.Get<Position, Input>();
            TryAct(World.PlayerRef, ref playerRefs.t0, ref playerRefs.t1);
            World.PlayerRef.Entity.Set(playerRefs.t0, playerRefs.t1);
            FieldOfView.CalculatePlayerFOV(World);
        }

        private void HandleComputerTurn()
        {
            World.World.Query(in nonPlayerQuery, (Entity entity, ref Position position, ref Input input) =>
            {
                TryAct(entity.Reference(), ref position, ref input);
            });
        }

        private void TryAct(EntityReference entity, ref Position position, ref Input input)
        {
            var newPosition = position.Point + input.Direction;
            if (newPosition != position.Point)
            {
                var tile = World.Map.GetTileFromLayer(MapLayerType.Wall, newPosition);
                if (!tile.TileType.IsWallOrBorder())
                {
                    var entitiesAtPosition = World.PhysicsWorld.GetEntitiesAtLocation(newPosition);

                    if (entitiesAtPosition == null || !entitiesAtPosition.Any(a => a.Entity.Has<Blocker>()))
                    {
                        World.PhysicsWorld.MoveEntity(entity, position.Point, newPosition);
                        position.Point = newPosition;
                    }
                    else
                    {
                        var target = entitiesAtPosition.Where(a => a.Entity.Has<Blocker>()).First();
                        if (entity.Entity.Has<Player>() || target.Entity.Has<Player>())
                        {
                            World.World.Create(new MeleeAttack() { Source = entity, Target = target });
                        }
                    }
                }
            }
        }
    }
}

using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
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
                    break;
            }
        }

        private void HandlePlayerTurn()
        {
            var playerRefs = World.PlayerRef.Entity.Get<Position, Input>();
            TryAct(World.PlayerRef, ref playerRefs.t0, ref playerRefs.t1);
            World.PlayerRef.Entity.Set(playerRefs.t0, playerRefs.t1);
        }

        private void TryAct(EntityReference entity, ref Position position, ref Input input)
        {
            var newPosition = position.Point + input.Direction;
            var tile = World.Map.GetTileFromLayer(MapLayerType.Wall, newPosition);
            if (!tile.TileType.IsWallOrBorder())
            {
                World.PhysicsWorld.MoveEntity(entity, position.Point, newPosition);
                position.Point = newPosition;
            }
        }
    }
}

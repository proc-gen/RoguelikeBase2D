using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.VisualBasic;
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
        Dictionary<string, Tileset> Tilesets;

        public EntityActSystem(GameWorld world, Dictionary<string, Tileset> tilesets)
            : base(world)
        {
            Tilesets = tilesets;
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
                if (!tile.TileType.IsBlocked())
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
                        if (target.Entity.Has<IsDoor>())
                        {
                            OpenDoor(target);
                        }
                        else if (entity.Entity.Has<Player>() || target.Entity.Has<Player>())
                        {
                            World.World.Create(new MeleeAttack() { Source = entity, Target = target });
                        }
                    }
                }
            }
        }

        private void OpenDoor(EntityReference doorPart)
        {
            var parent = doorPart.Entity.Get<IsDoor>().Parent.Entity.Get<Door>();
            
            OpenDoorPart(parent.TopTop);
            OpenDoorPart(parent.Top);
            OpenDoorPart(parent.Bottom);

            FieldOfView.CalculatePlayerFOV(World);
        }

        private void OpenDoorPart(EntityReference doorPart)
        {
            if (doorPart != EntityReference.Null)
            {
                doorPart.Entity.Remove<Blocker>();
                var point = doorPart.Entity.Get<Position>().Point;
                var doorPartTile = World.Map.GetTileFromLayer(MapLayerType.Door, point);

                doorPartTile.TileType = OpenDoorTile(doorPartTile.TileType);
                var tileset = Tilesets[doorPartTile.TilesetName];
                var tilesetTile = tileset.TilesetTiles.Where(a => a != null && a.TileTypes.Contains(doorPartTile.TileType)).FirstOrDefault();
                doorPartTile.TilesetTileId = tilesetTile.Id;
                
                World.Map.SetTileInLayer(MapLayerType.Door, point, doorPartTile);
            }
        }

        private TileType OpenDoorTile(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.DoorHorizontalClosedTop:
                    return TileType.DoorHorizontalOpenTop;
                    break;
                case TileType.DoorVerticalClosedTop:
                    return TileType.DoorVerticalOpenTop;
                    break;
                case TileType.DoorHorizontalClosedBottom:
                    return TileType.DoorHorizontalOpenBottom;
                    break;
                case TileType.DoorVerticalClosedBottom:
                    return TileType.DoorVerticalOpenBottom;
                    break;
                case TileType.DoorHorizontalClosedTopTop:
                    return TileType.DoorHorizontalOpenTopTop;
                    break;
            }

            return TileType.None;
        }
    }
}

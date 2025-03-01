using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public class MapPostCreationProcessor
    {
        public MapPostCreationProcessor() { }

        public void PostProcess(GameWorld world, Map map) 
        {
            for (int i = 0; i < map.Width; i++)
            {
                for(int j = 0; j < map.Height; j++)
                {
                    var door = map.GetTileFromLayer(MapLayerType.Door, i, j);
                    if (door.TileType != TileType.None)
                    {
                        if (door.TileType == TileType.DoorHorizontalClosedBottom)
                        {
                            var doorComponent = new Door()
                            {
                                Bottom = CreateDoorForPoint(world, new Point(i, j)),
                                Top = CreateDoorForPoint(world, new Point(i, j) + PointConstants.Up),
                                TopTop = CreateDoorForPoint(world, new Point(i, j) + PointConstants.Up + PointConstants.Up)
                            };

                            var reference = world.World.Create(doorComponent).Reference();
                            SetDoorParent(doorComponent.Bottom, reference);
                            SetDoorParent(doorComponent.Top, reference);
                            SetDoorParent(doorComponent.TopTop, reference);
                        }
                        else if (door.TileType == TileType.DoorVerticalClosedBottom)
                        {
                            var doorComponent = new Door()
                            {
                                Bottom = CreateDoorForPoint(world, new Point(i, j)),
                                Top = CreateDoorForPoint(world, new Point(i, j) + PointConstants.Up),
                                TopTop = EntityReference.Null,
                            };

                            var reference = world.World.Create(doorComponent).Reference();
                            SetDoorParent(doorComponent.Bottom, reference);
                            SetDoorParent(doorComponent.Top, reference);
                        }
                    }
                }
            }
        }

        private EntityReference CreateDoorForPoint(GameWorld world, Point point)
        {
            return world.World.Create(
                new Position() { Point = point },
                new Blocker(),
                new IsDoor()
                ).Reference();
        }

        private void SetDoorParent(EntityReference part, EntityReference parent)
        {
            var isDoor = part.Entity.Get<IsDoor>();
            isDoor.Parent = parent;
            part.Entity.Set(isDoor);
        }
    }
}

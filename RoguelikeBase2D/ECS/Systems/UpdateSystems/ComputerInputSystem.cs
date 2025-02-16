using Arch.Core.Extensions;
using Arch.Core;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;

namespace RoguelikeBase2D.ECS.Systems.UpdateSystems
{
    public class ComputerInputSystem : ArchSystem, IUpdateSystem
    {
        SquareGrid SquareGrid { get; set; }
        AStarSearch<Location> AStarSearch { get; set; }
        QueryDescription nonPlayerQuery = new QueryDescription().WithAll<Position, Input>().WithNone<Player>();
        public ComputerInputSystem(GameWorld world)
            : base(world)
        {
            SquareGrid = new SquareGrid(world);
            AStarSearch = new AStarSearch<Location>(SquareGrid);
        }

        public void Update(GameTime gameTime)
        {
            if (World.CurrentState == GameState.ComputerTurn)
            {
                var playerPosition = World.PlayerRef.Entity.Get<Position>();
                World.World.Query(in nonPlayerQuery, (Entity entity, ref Position position, ref Input input, ref ViewDistance viewDistance) =>
                {
                    var fov = FieldOfView.CalculateFOV(World, entity.Reference());

                    if (fov.Contains(playerPosition.Point))
                    {
                        var path = AStarSearch.RunSearch(new Location(position.Point), new Location(playerPosition.Point));
                        input.Direction = path - position.Point;
                        input.SkipTurn = path == position.Point;
                    }
                    else
                    {
                        input.SkipTurn = true;
                        input.Direction = Point.Zero;
                    }
                    input.Processed = false;
                });
            }
        }
    }
}

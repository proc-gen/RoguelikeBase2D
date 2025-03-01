using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Screens.Generated;
using RoguelikeBase2D.Utils.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWindow = RoguelikeBase2D.Screens.Generated.GameWindow;

namespace RoguelikeBase2D.Screens.Windows
{
    public class TargetingWindow : Window
    {
        Point Start, End;
        EntityReference Target;
        public TargetingWindow(GameScreen screen, GameWorld world)
            : base(screen, world)
        {

        }

        public override void OpenWindow()
        {
            IsOpen = true;
            Start = World.PlayerRef.Entity.Get<Position>().Point;
            End = Start + new Point(1, 0);
            UpdateTarget(Point.Zero);

            var gameWindow = ((GameWindow)GameScreen.MyraWindow);
            gameWindow.TargetingWeaponLabel.Text = string.Format("Weapon: {0}", World.PlayerRef.Entity.Get<CombatEquipment>().Weapon.Entity.Get<Identity>().Name);
        }

        public override void CloseWindow()
        {
            IsOpen = false;
        }

        public override void HandleKeyboard(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Escape))
            {
                CloseTargeting();
            }
            else if (kState.IsKeyDown(Keys.Up))
            {
                UpdateTarget(PointConstants.Up);
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                UpdateTarget(PointConstants.Down);
            }
            else if (kState.IsKeyDown(Keys.Left))
            {
                UpdateTarget(PointConstants.Left);
            }
            else if (kState.IsKeyDown(Keys.Right))
            {
                UpdateTarget(PointConstants.Right);
            }
            else if (kState.IsKeyDown(Keys.Enter))
            {
                if (Target != EntityReference.Null)
                {
                    World.World.Create(new RangedAttack() { Source = World.PlayerRef, Target = Target });
                    GameScreen.MovePlayer(Point.Zero);
                    CloseTargeting();
                }
            }
        }

        public override void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var position = World.PlayerRef.Entity.Get<Position>();
            var calcPlayerPosition = position.Point.ToVector2() * 48;
            var tileset = GameScreen.Tilesets["test-tileset"];
            var sourceRect = tileset.GetRectangleForTilesetTile(0);
            var tileX = End.X * tileset.TileWidth;
            var tileY = End.Y * tileset.TileHeight;

            spriteBatch.Draw(GameScreen.textures["test-tileset"], new Vector2(tileX, tileY) + RogueGame.CenterOffset, sourceRect, TrajectoryColor(), 0f, calcPlayerPosition, 1f, SpriteEffects.None, 1f);
        }

        private Color TrajectoryColor()
        {
            if (Start == End)
            {
                return Color.Red;
            }

            return Target == EntityReference.Null ? Color.Yellow : Color.Green;
        }

        private void CloseTargeting()
        {
            ClearTargetingData();
            GameScreen.CloseTargeting();
        }

        private void ClearTargetingData()
        {
            Target = EntityReference.Null;
            Start = End = Point.Zero;
        }

        private void UpdateTarget(Point direction)
        {
            if (World.PlayerFov.Contains(End + direction))
            {
                End += direction;
                var gameWindow = ((GameWindow)GameScreen.MyraWindow);
                var entitiesAtLocation = World.PhysicsWorld.GetEntitiesAtLocation(End);
                if (entitiesAtLocation == null || !entitiesAtLocation.Where(a => a.Entity.Has<Blocker>()).Any())
                {
                    Target = EntityReference.Null;
                    gameWindow.TargetingEntityLabel.Text = string.Format("Targeting: None");
                }
                else
                {
                    Target = entitiesAtLocation.Where(a => a.Entity.Has<Blocker>()).First();
                    gameWindow.TargetingEntityLabel.Text = string.Format("Targeting: {0}", Target.Entity.Get<Identity>().Name);
                }
            }

            GameScreen.InputDelayHelper.Reset();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoguelikeBase2D.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Screens.Windows
{
    public class TargetingWindow : Window
    {
        public TargetingWindow(GameScreen screen, GameWorld world)
            : base(screen, world)
        {

        }

        public override void OpenWindow()
        {
            IsOpen = true;
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
        }

        public override void Render(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        private void CloseTargeting()
        {
            GameScreen.CloseTargeting();
        }
    }
}

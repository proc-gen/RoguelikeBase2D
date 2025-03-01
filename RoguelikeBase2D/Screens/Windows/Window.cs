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
    public abstract class Window
    {
        protected GameScreen GameScreen;
        protected GameWorld World;
        public bool IsOpen { get; set; }

        public Window(GameScreen gameScreen, GameWorld world)
        {
            GameScreen = gameScreen;
            World = world;
        }

        public abstract void OpenWindow();

        public abstract void CloseWindow();

        public abstract void HandleKeyboard(KeyboardState kState);
        public virtual void Render(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}

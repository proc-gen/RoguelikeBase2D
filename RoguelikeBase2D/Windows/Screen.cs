using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using RoguelikeBase2D.Utils;
using RoguelikeBase2D.Utils.Myra;
using RoguelikeBase2D.Windows.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Windows
{
    public abstract class Screen
    {
        public Widget MyraWindow { get; set; }
        public RogueGame Game { get; set; }
        protected InputDelayHelper InputDelayHelper;

        public Screen(RogueGame game) 
        { 
            Game = game;
            InputDelayHelper = new InputDelayHelper();
        }

        public abstract void SetActive();
        public virtual void Update(GameTime gameTime)
        {
            InputDelayHelper.Update(gameTime);
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        protected MenuInputStatus GetMenuInputStatus(KeyboardState kState)
        {
            return new MenuInputStatus()
            {
                Left = kState.IsKeyDown(Keys.Left),
                Right = kState.IsKeyDown(Keys.Right),
                Down = kState.IsKeyDown(Keys.Down),
                Up = kState.IsKeyDown(Keys.Up),
                Enter = kState.IsKeyDown(Keys.Enter),
            };
        }

        protected void HandleUIInput(Panel panel, List<ImageTextButton> Buttons, bool next, bool previous, bool enter)
        {
            if (Game.IsActive)
            {
                if (InputDelayHelper.ReadyForInput)
                {
                    if (next || previous)
                    {
                        var currentButton = Buttons.Where(a => a.IsKeyboardFocused).FirstOrDefault();
                        if (currentButton != null)
                        {
                            var index = Buttons.IndexOf(currentButton);

                            if (previous)
                            {
                                Buttons[Math.Max(0, index - 1)].SetKeyboardFocus();
                            }
                            else if (next)
                            {
                                Buttons[Math.Min(Buttons.Count - 1, index + 1)].SetKeyboardFocus();
                            }

                            if (Buttons.IndexOf(Buttons.Where(a => a.IsKeyboardFocused).FirstOrDefault()) != index)
                            {
                                // Play hover sound
                            }
                        }
                        else
                        {
                            Buttons.First().SetKeyboardFocus();
                        }
                    }

                    if (enter)
                    {
                        panel.FindWidgetToClick();
                    }

                    if (next || previous || enter)
                    {
                        InputDelayHelper.Reset();
                    }
                }
            }
        }
    }
}

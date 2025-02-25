using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using RoguelikeBase2D.Utils;
using RoguelikeBase2D.Utils.Myra;
using RoguelikeBase2D.Screens.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Screens
{
    public class MainMenuScreen : Screen
    {
        private List<ImageTextButton> Buttons { get; set; }

        public MainMenuScreen(RogueGame game) 
            : base(game) 
        {
            MyraWindow = new MainMenuWindow();
            ((MainMenuWindow)MyraWindow).NewButton.Click += NewGameButtonClick;
            ((MainMenuWindow)MyraWindow).ContinueButton.Click += ContinueGameButtonClick;
            ((MainMenuWindow)MyraWindow).ExitButton.Click += ExitButtonClick;

            Buttons = MyraWindow.FindAllWidgetsOfType<ImageTextButton>();
        }

        public override void SetActive()
        {
            Buttons.First().SetKeyboardFocus();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputDelayHelper.ReadyForInput)
            {
                var kState = Keyboard.GetState();
                var menuInputStatus = GetMenuInputStatus(kState);
                HandleUIInput(MyraWindow as MainMenuWindow, Buttons, menuInputStatus.Down, menuInputStatus.Up, menuInputStatus.Enter);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {

        }

        protected void NewGameButtonClick(object e, EventArgs eventArgs)
        {
            Game.SetScreen(new GameScreen(Game, false));
        }

        protected void ContinueGameButtonClick(object e, EventArgs eventArgs)
        {
            Game.SetScreen(new GameScreen(Game, true));
        }

        protected void ExitButtonClick(object e, EventArgs eventArgs)
        {
            Game.Exit();
        }
    }
}

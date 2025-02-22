using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using RoguelikeBase2D.Utils;
using RoguelikeBase2D.Utils.Myra;
using RoguelikeBase2D.Windows.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Windows
{
    public class MainMenuScreen : Screen
    {
        private List<ImageTextButton> Buttons { get; set; }

        public MainMenuScreen(RogueGame game) 
            : base(game) 
        {
            MyraWindow = new MainMenuWindow();    
            
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
    }
}

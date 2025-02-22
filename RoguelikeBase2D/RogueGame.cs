using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using RoguelikeBase2D.Windows;
using System;


namespace RoguelikeBase2D
{
    public class RogueGame : Game
    {
        public static Vector2 CenterOffset = new Vector2(960, 540);

        private GraphicsDeviceManager _graphics;
        RenderTarget2D renderTarget;
        private SpriteBatch _spriteBatch;
        Desktop desktop;

        Screen currentScreen;

        public RogueGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
            renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080);

            base.Initialize();
        }

        protected void OnResize(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MyraEnvironment.Game = this;
            desktop = new Desktop();
            currentScreen = new GameScreen(this, false);
            desktop.Root = currentScreen.MyraWindow;
        }

        protected override void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Matrix.Identity);

            currentScreen.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                                    SamplerState.LinearClamp, DepthStencilState.Default,
                                    RasterizerState.CullNone);

            _spriteBatch.Draw(renderTarget, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth,_graphics.PreferredBackBufferHeight), Color.White);

            _spriteBatch.End();

            desktop.Render();

            base.Draw(gameTime);
        }
    }
}

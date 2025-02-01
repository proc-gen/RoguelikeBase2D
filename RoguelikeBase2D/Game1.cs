using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Maps.Generators;
using RoguelikeBase2D.Maps.Painters;
using System.Collections.Generic;
using System.IO;

namespace RoguelikeBase2D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Dictionary<string, Texture2D> textures;
        Map map;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadTextures();
            GenerateMap();
        }

        private void LoadTextures()
        {
            textures = new Dictionary<string, Texture2D>()
            {
                { "test-tileset", Content.Load<Texture2D>(Path.Combine("Tilesets", "test-tileset")) },
            };
        }

        private void GenerateMap()
        {
            Tileset tileset = new Tileset("test-tileset", 480, 480, 48, 48);
            tileset.SetTilesetTile(31, new TilesetTile()
            {
                Id = 31,
                TileType = Constants.TileType.Wall,
            });
            tileset.SetTilesetTile(40, new TilesetTile()
            {
                Id = 40,
                TileType = Constants.TileType.Floor,
            });
            tileset.SetTilesetTile(41, new TilesetTile()
            {
                Id = 41,
                TileType = Constants.TileType.Floor,
            });

            TestGenerator generator = new TestGenerator();
            TestPainter painter = new TestPainter();
            map = generator.GenerateMap(10, 10);
            map = painter.PaintMap(map, tileset);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

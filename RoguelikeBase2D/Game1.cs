using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Maps.Generators;
using RoguelikeBase2D.Maps.Painters;
using RoguelikeBase2D.Utils.Rendering;
using System.Collections.Generic;
using System.IO;

namespace RoguelikeBase2D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Dictionary<string, Texture2D> textures;
        Dictionary<string, Tileset> tilesets;
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
            var tileset = new Tileset("test-tileset", 480, 480, 48, 48);
            tileset.SetTilesetTile(31, new TilesetTile()
            {
                Id = 31,
                TileType = TileType.Wall,
            });
            tileset.SetTilesetTile(40, new TilesetTile()
            {
                Id = 40,
                TileType = TileType.Floor,
            });
            tileset.SetTilesetTile(41, new TilesetTile()
            {
                Id = 41,
                TileType = TileType.Floor,
            });

            tilesets = new Dictionary<string, Tileset>()
            {
                {tileset.Name, tileset},
            };

            TestGenerator generator = new TestGenerator();
            TestPainter painter = new TestPainter();
            map = generator.GenerateMap(10, 10);
            map = painter.PaintMap(map, tilesets["test-tileset"]);

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

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: Matrix.Identity);
            RenderLayer(MapLayerType.Floor);
            RenderLayer(MapLayerType.Wall);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RenderLayer(MapLayerType layerType)
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    var tile = map.GetTileFromLayer(layerType, i, j);
                    if (tile.TileType != TileType.None) 
                    {
                        var tileset = tilesets[tile.TilesetName];
                        var sourceRect = tileset.GetRectangleForTilesetTile(tile.TilesetTileId);
                        var tileX = i * tileset.TileWidth;
                        var tileY = j * tileset.TileHeight;
                        _spriteBatch.Draw(textures[tile.TilesetName], new Vector2(tileX, tileY), sourceRect, Color.White);
                    } 
                }
            }
        }
    }
}

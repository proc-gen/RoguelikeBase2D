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
            tileset.SetTilesetTile(31, new TilesetTile(31, TileType.Wall));
            tileset.SetTilesetTile(40, new TilesetTile(40, TileType.Floor));
            tileset.SetTilesetTile(41, new TilesetTile(41, TileType.Floor));

            tileset.SetTilesetTile(90, new TilesetTile(90, TileType.WallBorder5));
            tileset.SetTilesetTile(80, new TilesetTile(80, TileType.WallBorder25));
            tileset.SetTilesetTile(70, new TilesetTile(70, 
                TileType.WallBorder258, 
                TileType.WallBorder1258,
                TileType.WallBorder12358,
                TileType.WallBorder12578,
                TileType.WallBorder12589,
                TileType.WallBorder123578,
                TileType.WallBorder123589,
                TileType.WallBorder1235789,
                TileType.WallBorder2358,
                TileType.WallBorder23578,
                TileType.WallBorder23589,
                TileType.WallBorder235789,
                TileType.WallBorder2578,
                TileType.WallBorder2589,
                TileType.WallBorder25789));
            tileset.SetTilesetTile(60, new TilesetTile(60, TileType.WallBorder58));
            tileset.SetTilesetTile(91, new TilesetTile(91, TileType.WallBorder56));
            tileset.SetTilesetTile(92, new TilesetTile(92, TileType.WallBorder456));
            tileset.SetTilesetTile(93, new TilesetTile(93, TileType.WallBorder45));
            tileset.SetTilesetTile(81, new TilesetTile(81, TileType.WallBorder256));
            tileset.SetTilesetTile(82, new TilesetTile(82, TileType.WallBorder2456));
            tileset.SetTilesetTile(83, new TilesetTile(83, TileType.WallBorder245));
            tileset.SetTilesetTile(71, new TilesetTile(71, TileType.WallBorder2568));
            tileset.SetTilesetTile(72, new TilesetTile(72, TileType.WallBorder24568));
            tileset.SetTilesetTile(73, new TilesetTile(73, TileType.WallBorder2458));
            tileset.SetTilesetTile(61, new TilesetTile(61, TileType.WallBorder568));
            tileset.SetTilesetTile(62, new TilesetTile(62, TileType.WallBorder4568));
            tileset.SetTilesetTile(63, new TilesetTile(63, TileType.WallBorder458));
            tileset.SetTilesetTile(66, new TilesetTile(66, TileType.WallBorder124568));
            tileset.SetTilesetTile(76, new TilesetTile(76, TileType.WallBorder25689));
            tileset.SetTilesetTile(86, new TilesetTile(86, TileType.WallBorder23568));
            tileset.SetTilesetTile(96, new TilesetTile(96, TileType.WallBorder245678));
            tileset.SetTilesetTile(67, new TilesetTile(67, TileType.WallBorder45689));
            tileset.SetTilesetTile(77, new TilesetTile(77, TileType.WallBorder23456789));
            tileset.SetTilesetTile(87, new TilesetTile(87, TileType.WallBorder12345689));
            tileset.SetTilesetTile(97, new TilesetTile(97, TileType.WallBorder23456));
            tileset.SetTilesetTile(68, new TilesetTile(68, TileType.WallBorder45678));
            tileset.SetTilesetTile(78, new TilesetTile(78, TileType.WallBorder12456789));
            tileset.SetTilesetTile(88, new TilesetTile(88, TileType.WallBorder12345678));
            tileset.SetTilesetTile(98, new TilesetTile(98, TileType.WallBorder12456));
            tileset.SetTilesetTile(69, new TilesetTile(69, TileType.WallBorder234568));
            tileset.SetTilesetTile(79, new TilesetTile(79, TileType.WallBorder24578));
            tileset.SetTilesetTile(89, new TilesetTile(89, TileType.WallBorder12458));
            tileset.SetTilesetTile(99, new TilesetTile(99, TileType.WallBorder245689));
            tileset.SetTilesetTile(6, new TilesetTile(6, TileType.WallBorder5689));
            tileset.SetTilesetTile(16, new TilesetTile(16, TileType.WallBorder235689));
            tileset.SetTilesetTile(26, new TilesetTile(26, TileType.WallBorder2345689));
            tileset.SetTilesetTile(36, new TilesetTile(36, TileType.WallBorder2356));
            tileset.SetTilesetTile(7, new TilesetTile(7, TileType.WallBorder2456789));
            tileset.SetTilesetTile(17, new TilesetTile(17, TileType.WallBorder2345678));
            tileset.SetTilesetTile(27, new TilesetTile(27, TileType.WallBorder123456789));
            tileset.SetTilesetTile(37, new TilesetTile(37, TileType.WallBorder123456));
            tileset.SetTilesetTile(8, new TilesetTile(8, TileType.WallBorder456789));
            tileset.SetTilesetTile(28, new TilesetTile(28, TileType.WallBorder1245689));
            tileset.SetTilesetTile(38, new TilesetTile(38, TileType.WallBorder1234568));
            tileset.SetTilesetTile(9, new TilesetTile(9, TileType.WallBorder4578));
            tileset.SetTilesetTile(19, new TilesetTile(19, TileType.WallBorder1245678));
            tileset.SetTilesetTile(29, new TilesetTile(29, TileType.WallBorder124578));
            tileset.SetTilesetTile(39, new TilesetTile(39, TileType.WallBorder1245));



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

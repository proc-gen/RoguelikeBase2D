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

            tileset.SetTilesetTile(90, new TilesetTile()
            {
                Id = 90,
                TileType = TileType.WallBorder5,
            });
            tileset.SetTilesetTile(80, new TilesetTile()
            {
                Id = 80,
                TileType = TileType.WallBorder25,
            });
            tileset.SetTilesetTile(70, new TilesetTile()
            {
                Id = 70,
                TileType = TileType.WallBorder258,
            });
            tileset.SetTilesetTile(60, new TilesetTile()
            {
                Id = 60,
                TileType = TileType.WallBorder58,
            });
            tileset.SetTilesetTile(91, new TilesetTile()
            {
                Id = 91,
                TileType = TileType.WallBorder56,
            });
            tileset.SetTilesetTile(92, new TilesetTile()
            {
                Id = 92,
                TileType = TileType.WallBorder456,
            });
            tileset.SetTilesetTile(93, new TilesetTile()
            {
                Id = 93,
                TileType = TileType.WallBorder45,
            });
            tileset.SetTilesetTile(81, new TilesetTile()
            {
                Id = 81,
                TileType = TileType.WallBorder256,
            });
            tileset.SetTilesetTile(82, new TilesetTile()
            {
                Id = 82,
                TileType = TileType.WallBorder2456,
            });
            tileset.SetTilesetTile(83, new TilesetTile()
            {
                Id = 83,
                TileType = TileType.WallBorder245,
            });
            tileset.SetTilesetTile(71, new TilesetTile()
            {
                Id = 71,
                TileType = TileType.WallBorder2568,
            });
            tileset.SetTilesetTile(72, new TilesetTile()
            {
                Id = 72,
                TileType = TileType.WallBorder24568,
            });
            tileset.SetTilesetTile(73, new TilesetTile()
            {
                Id = 73,
                TileType = TileType.WallBorder2458,
            });
            tileset.SetTilesetTile(61, new TilesetTile()
            {
                Id = 61,
                TileType = TileType.WallBorder568,
            });
            tileset.SetTilesetTile(62, new TilesetTile()
            {
                Id = 72,
                TileType = TileType.WallBorder4568,
            });
            tileset.SetTilesetTile(63, new TilesetTile()
            {
                Id = 63,
                TileType = TileType.WallBorder458,
            });
            tileset.SetTilesetTile(66, new TilesetTile()
            {
                Id = 66,
                TileType = TileType.WallBorder124568,
            });
            tileset.SetTilesetTile(76, new TilesetTile()
            {
                Id = 76,
                TileType = TileType.WallBorder25689,
            });
            tileset.SetTilesetTile(86, new TilesetTile()
            {
                Id = 86,
                TileType = TileType.WallBorder23568,
            });
            tileset.SetTilesetTile(96, new TilesetTile()
            {
                Id = 96,
                TileType = TileType.WallBorder245678,
            });
            tileset.SetTilesetTile(67, new TilesetTile()
            {
                Id = 67,
                TileType = TileType.WallBorder45689,
            });
            tileset.SetTilesetTile(77, new TilesetTile()
            {
                Id = 77,
                TileType = TileType.WallBorder23456789,
            });
            tileset.SetTilesetTile(87, new TilesetTile()
            {
                Id = 87,
                TileType = TileType.WallBorder12345689,
            });
            tileset.SetTilesetTile(97, new TilesetTile()
            {
                Id = 97,
                TileType = TileType.WallBorder23456,
            });
            tileset.SetTilesetTile(68, new TilesetTile()
            {
                Id = 68,
                TileType = TileType.WallBorder45678,
            });
            tileset.SetTilesetTile(78, new TilesetTile()
            {
                Id = 78,
                TileType = TileType.WallBorder12456789,
            });
            tileset.SetTilesetTile(88, new TilesetTile()
            {
                Id = 88,
                TileType = TileType.WallBorder12345678,
            });
            tileset.SetTilesetTile(98, new TilesetTile()
            {
                Id = 98,
                TileType = TileType.WallBorder12456,
            });
            tileset.SetTilesetTile(69, new TilesetTile()
            {
                Id = 69,
                TileType = TileType.WallBorder234568,
            });
            tileset.SetTilesetTile(79, new TilesetTile()
            {
                Id = 79,
                TileType = TileType.WallBorder24578,
            });
            tileset.SetTilesetTile(89, new TilesetTile()
            {
                Id = 89,
                TileType = TileType.WallBorder12458,
            });
            tileset.SetTilesetTile(99, new TilesetTile()
            {
                Id = 99,
                TileType = TileType.WallBorder245689,
            });
            tileset.SetTilesetTile(6, new TilesetTile()
            {
                Id = 6,
                TileType = TileType.WallBorder5689,
            });
            tileset.SetTilesetTile(16, new TilesetTile()
            {
                Id = 16,
                TileType = TileType.WallBorder235689,
            });
            tileset.SetTilesetTile(26, new TilesetTile()
            {
                Id = 26,
                TileType = TileType.WallBorder2345689,
            });
            tileset.SetTilesetTile(36, new TilesetTile()
            {
                Id = 36,
                TileType = TileType.WallBorder2356,
            });
            tileset.SetTilesetTile(7, new TilesetTile()
            {
                Id = 7,
                TileType = TileType.WallBorder2456789,
            });
            tileset.SetTilesetTile(17, new TilesetTile()
            {
                Id = 17,
                TileType = TileType.WallBorder2345678,
            });
            tileset.SetTilesetTile(27, new TilesetTile()
            {
                Id = 27,
                TileType = TileType.WallBorder123456789,
            });
            tileset.SetTilesetTile(37, new TilesetTile()
            {
                Id = 37,
                TileType = TileType.WallBorder123456,
            });
            tileset.SetTilesetTile(8, new TilesetTile()
            {
                Id = 8,
                TileType = TileType.WallBorder456789,
            });
            tileset.SetTilesetTile(28, new TilesetTile()
            {
                Id = 28,
                TileType = TileType.WallBorder1245689,
            });
            tileset.SetTilesetTile(38, new TilesetTile()
            {
                Id = 38,
                TileType = TileType.WallBorder1234568,
            });
            tileset.SetTilesetTile(9, new TilesetTile()
            {
                Id = 9,
                TileType = TileType.WallBorder4578,
            });
            tileset.SetTilesetTile(19, new TilesetTile()
            {
                Id = 19,
                TileType = TileType.WallBorder1245678,
            });
            tileset.SetTilesetTile(29, new TilesetTile()
            {
                Id = 29,
                TileType = TileType.WallBorder124578,
            });
            tileset.SetTilesetTile(39, new TilesetTile()
            {
                Id = 39,
                TileType = TileType.WallBorder1245,
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

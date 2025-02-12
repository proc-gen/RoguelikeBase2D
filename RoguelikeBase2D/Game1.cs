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
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadTextures();
            LoadTilesets();
            GenerateMap();
        }

        private void LoadTextures()
        {
            textures = new Dictionary<string, Texture2D>()
            {
                { "test-tileset", Content.Load<Texture2D>(Path.Combine("Tilesets", "test-tileset")) },
            };
        }

        private void LoadTilesets()
        {
            var tileset = new Tileset("test-tileset", 480, 480, 48, 48);
            tileset.SetTilesetTile(30, new TilesetTile(30, TileType.WallBottomLeft, TileType.WallTopLeft));
            tileset.SetTilesetTile(31, new TilesetTile(31, TileType.WallBottomMiddle, TileType.WallTopMiddle));
            tileset.SetTilesetTile(32, new TilesetTile(32, TileType.WallBottomRight, TileType.WallTopRight));
            tileset.SetTilesetTile(33, new TilesetTile(33, TileType.WallBottomSingle, TileType.WallTopSingle));

            tileset.SetTilesetTile(40, new TilesetTile(40, TileType.Floor));
            tileset.SetTilesetTile(41, new TilesetTile(41, TileType.Floor));

            tileset.SetTilesetTile(90, new TilesetTile(90, TileType.WallBorder5));
            tileset.SetTilesetTile(80, new TilesetTile(80, 
                TileType.WallBorder25,
                TileType.WallBorder125,
                TileType.WallBorder235,
                TileType.WallBorder1235));
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
            tileset.SetTilesetTile(60, new TilesetTile(60, 
                TileType.WallBorder58,
                TileType.WallBorder578,
                TileType.WallBorder589,
                TileType.WallBorder5789));
            tileset.SetTilesetTile(91, new TilesetTile(91, 
                TileType.WallBorder56,
                TileType.WallBorder356,
                TileType.WallBorder569,
                TileType.WallBorder3569));
            tileset.SetTilesetTile(92, new TilesetTile(92,
                TileType.WallBorder456,
                TileType.WallBorder1456,
                TileType.WallBorder13456,
                TileType.WallBorder14567,
                TileType.WallBorder14569,
                TileType.WallBorder134567,
                TileType.WallBorder134569,
                TileType.WallBorder1345679,
                TileType.WallBorder3456,
                TileType.WallBorder34567,
                TileType.WallBorder34569,
                TileType.WallBorder345679,
                TileType.WallBorder4567,
                TileType.WallBorder4569,
                TileType.WallBorder45679));
            tileset.SetTilesetTile(93, new TilesetTile(93, 
                TileType.WallBorder45,
                TileType.WallBorder145,
                TileType.WallBorder457,
                TileType.WallBorder1457));
            tileset.SetTilesetTile(81, new TilesetTile(81, 
                TileType.WallBorder256,
                TileType.WallBorder1256,
                TileType.WallBorder2569,
                TileType.WallBorder12569));
            tileset.SetTilesetTile(82, new TilesetTile(82, TileType.WallBorder2456));
            tileset.SetTilesetTile(83, new TilesetTile(83, 
                TileType.WallBorder245,
                TileType.WallBorder2345,
                TileType.WallBorder2457,
                TileType.WallBorder23457));
            tileset.SetTilesetTile(71, new TilesetTile(71, TileType.WallBorder2568));
            tileset.SetTilesetTile(72, new TilesetTile(72, TileType.WallBorder24568));
            tileset.SetTilesetTile(73, new TilesetTile(73, TileType.WallBorder2458));
            tileset.SetTilesetTile(61, new TilesetTile(61, 
                TileType.WallBorder568,
                TileType.WallBorder3568,
                TileType.WallBorder5678,
                TileType.WallBorder35678));
            tileset.SetTilesetTile(62, new TilesetTile(62, TileType.WallBorder4568));
            tileset.SetTilesetTile(63, new TilesetTile(63, 
                TileType.WallBorder458,
                TileType.WallBorder1458,
                TileType.WallBorder4589,
                TileType.WallBorder14589));
            tileset.SetTilesetTile(66, new TilesetTile(66, TileType.WallBorder124568));
            tileset.SetTilesetTile(76, new TilesetTile(76, 
                TileType.WallBorder25689,
                TileType.WallBorder256789,
                TileType.WallBorder125689,
                TileType.WallBorder1256789));
            tileset.SetTilesetTile(86, new TilesetTile(86, 
                TileType.WallBorder23568,
                TileType.WallBorder235678,
                TileType.WallBorder123568,
                TileType.WallBorder1235678));
            tileset.SetTilesetTile(96, new TilesetTile(96, TileType.WallBorder245678));
            tileset.SetTilesetTile(67, new TilesetTile(67, 
                TileType.WallBorder45689,
                TileType.WallBorder145689,
                TileType.WallBorder345689,
                TileType.WallBorder1345689));
            tileset.SetTilesetTile(77, new TilesetTile(77, TileType.WallBorder23456789));
            tileset.SetTilesetTile(87, new TilesetTile(87, TileType.WallBorder12345689));
            tileset.SetTilesetTile(97, new TilesetTile(97, 
                TileType.WallBorder23456,
                TileType.WallBorder234569,
                TileType.WallBorder234567,
                TileType.WallBorder2345679));
            tileset.SetTilesetTile(68, new TilesetTile(68, 
                TileType.WallBorder45678,
                TileType.WallBorder145678,
                TileType.WallBorder345678,
                TileType.WallBorder1345678));
            tileset.SetTilesetTile(78, new TilesetTile(78, TileType.WallBorder12456789));
            tileset.SetTilesetTile(88, new TilesetTile(88, TileType.WallBorder12345678));
            tileset.SetTilesetTile(98, new TilesetTile(98, 
                TileType.WallBorder12456,
                TileType.WallBorder124569,
                TileType.WallBorder124567,
                TileType.WallBorder1245679));
            tileset.SetTilesetTile(69, new TilesetTile(69, TileType.WallBorder234568));
            tileset.SetTilesetTile(79, new TilesetTile(79, 
                TileType.WallBorder24578,
                TileType.WallBorder234578,
                TileType.WallBorder245789,
                TileType.WallBorder2345789));
            tileset.SetTilesetTile(89, new TilesetTile(89, 
                TileType.WallBorder12458,
                TileType.WallBorder123458,
                TileType.WallBorder124589,
                TileType.WallBorder1234589));
            tileset.SetTilesetTile(99, new TilesetTile(99, TileType.WallBorder245689));
            tileset.SetTilesetTile(6, new TilesetTile(6, 
                TileType.WallBorder5689,
                TileType.WallBorder35689,
                TileType.WallBorder56789,
                TileType.WallBorder356789));
            tileset.SetTilesetTile(16, new TilesetTile(16, 
                TileType.WallBorder235689,
                TileType.WallBorder1235689,
                TileType.WallBorder2356789,
                TileType.WallBorder12356789));
            tileset.SetTilesetTile(26, new TilesetTile(26, TileType.WallBorder2345689));
            tileset.SetTilesetTile(36, new TilesetTile(36, 
                TileType.WallBorder2356,
                TileType.WallBorder12356,
                TileType.WallBorder23569,
                TileType.WallBorder123569));
            tileset.SetTilesetTile(7, new TilesetTile(7, TileType.WallBorder2456789));
            tileset.SetTilesetTile(17, new TilesetTile(17, TileType.WallBorder2345678));
            tileset.SetTilesetTile(27, new TilesetTile(27, TileType.WallBorder123456789));
            tileset.SetTilesetTile(37, new TilesetTile(37, 
                TileType.WallBorder123456,
                TileType.WallBorder1234567,
                TileType.WallBorder1234569,
                TileType.WallBorder12345679));
            tileset.SetTilesetTile(8, new TilesetTile(8, 
                TileType.WallBorder456789,
                TileType.WallBorder1456789,
                TileType.WallBorder3456789,
                TileType.WallBorder13456789));
            tileset.SetTilesetTile(28, new TilesetTile(28, TileType.WallBorder1245689));
            tileset.SetTilesetTile(38, new TilesetTile(38, TileType.WallBorder1234568));
            tileset.SetTilesetTile(9, new TilesetTile(9, 
                TileType.WallBorder4578,
                TileType.WallBorder14578,
                TileType.WallBorder45789,
                TileType.WallBorder145789));
            tileset.SetTilesetTile(19, new TilesetTile(19, TileType.WallBorder1245678));
            tileset.SetTilesetTile(29, new TilesetTile(29, 
                TileType.WallBorder124578,
                TileType.WallBorder1234578,
                TileType.WallBorder1245789,
                TileType.WallBorder12345789));
            tileset.SetTilesetTile(39, new TilesetTile(39, 
                TileType.WallBorder1245,
                TileType.WallBorder12345,
                TileType.WallBorder123457,
                TileType.WallBorder12457));

            tilesets = new Dictionary<string, Tileset>()
            {
                {tileset.Name, tileset},
            };
        }

        private void GenerateMap()
        {
            Generator generator = new DrunkardWalkGenerator();
            TestPainter painter = new TestPainter();
            map = generator.GenerateMap(40, 22);
            map = painter.PaintMap(map, tilesets["test-tileset"]);

            Window.Title = string.Format("RoguelikeBase2D - Seed: {0}", map.Seed);
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            else if(kState.IsKeyDown(Keys.R))
            {
                GenerateMap();
            }

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

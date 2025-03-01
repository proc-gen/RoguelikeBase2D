using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.ECS.Systems.RenderSystems;
using RoguelikeBase2D.ECS.Systems.UpdateSystems;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Maps.Generators;
using RoguelikeBase2D.Maps.Painters;
using RoguelikeBase2D.Maps.Spawners;
using RoguelikeBase2D.Screens.Windows;
using RoguelikeBase2D.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameWindow = RoguelikeBase2D.Screens.Generated.GameWindow;

namespace RoguelikeBase2D.Screens
{
    public class GameScreen : Screen
    {
        public Dictionary<string, Texture2D> textures;
        public Dictionary<string, Tileset> Tilesets;
        GameWorld world;
        List<IUpdateSystem> updateSystems;
        List<IRenderSystem> renderSystems;
        SeededRandom random;
        InventoryWindow inventoryWindow;
        TargetingWindow targetingWindow;
        public GameScreen(RogueGame game, bool continueGame)
            : base(game)
        {
            MyraWindow = new GameWindow();
            random = SeededRandom.New();

            if (continueGame)
            {
                world = SaveGameManager.LoadGame();
            }
            else
            {
                world = SaveGameManager.NewGame();
            }

            LoadTextures();
            LoadTilesets();
            InitSystems();

            if (!continueGame)
            {
                GenerateMap();
            }
        }


        private void LoadTextures()
        {
            textures = new Dictionary<string, Texture2D>()
            {
                { "test-tileset", Game.Content.Load<Texture2D>(Path.Combine("Tilesets", "test-tileset")) },

                { "player", Game.Content.Load<Texture2D>(Path.Combine("Sprites", "Player")) },
                { "goblin", Game.Content.Load<Texture2D>(Path.Combine("Sprites", "Goblin")) },
                { "ogre", Game.Content.Load<Texture2D>(Path.Combine("Sprites", "Ogre")) },

                { "crate", Game.Content.Load<Texture2D>(Path.Combine("Objects", "crate")) },
                { "bow", Game.Content.Load<Texture2D>(Path.Combine("Objects", "bow")) },
                { "cloth-armor", Game.Content.Load<Texture2D>(Path.Combine("Objects", "cloth-armor")) },
                { "leather-armor", Game.Content.Load<Texture2D>(Path.Combine("Objects", "leather-armor")) },
                { "health-potion", Game.Content.Load<Texture2D>(Path.Combine("Objects", "health-potion")) },
                { "dagger", Game.Content.Load<Texture2D>(Path.Combine("Objects", "dagger")) },
                { "sword", Game.Content.Load<Texture2D>(Path.Combine("Objects", "sword")) },
            };
        }

        private void LoadTilesets()
        {
            var tileset = new Tileset("test-tileset", 480, 480, 48, 48);
            tileset.SetTilesetTile(44, new TilesetTile(44, TileType.Exit));
            tileset.SetTilesetTile(30, new TilesetTile(30, TileType.WallBottomLeft, TileType.WallTopLeft));
            tileset.SetTilesetTile(31, new TilesetTile(31, TileType.WallBottomMiddle, TileType.WallTopMiddle));
            tileset.SetTilesetTile(32, new TilesetTile(32, TileType.WallBottomRight, TileType.WallTopRight));
            tileset.SetTilesetTile(33, new TilesetTile(33, TileType.WallBottomSingle, TileType.WallTopSingle));

            tileset.SetTilesetTile(40, new TilesetTile(40, TileType.Floor));
            tileset.SetTilesetTile(41, new TilesetTile(41, TileType.Floor));

            tileset.SetTilesetTile(24, new TilesetTile(24, TileType.DoorVerticalClosedTop));
            tileset.SetTilesetTile(34, new TilesetTile(34, TileType.DoorVerticalClosedBottom));
            tileset.SetTilesetTile(25, new TilesetTile(25, TileType.DoorVerticalOpenTop));
            tileset.SetTilesetTile(35, new TilesetTile(35, TileType.DoorVerticalOpenBottom));
            tileset.SetTilesetTile(46, new TilesetTile(46, TileType.DoorHorizontalClosedTop));
            tileset.SetTilesetTile(56, new TilesetTile(56, TileType.DoorHorizontalClosedBottom));
            tileset.SetTilesetTile(47, new TilesetTile(47, TileType.DoorHorizontalOpenTop));
            tileset.SetTilesetTile(57, new TilesetTile(57, TileType.DoorHorizontalOpenBottom));
            tileset.SetTilesetTile(2, new TilesetTile(2, TileType.DoorHorizontalClosedTopTop, TileType.DoorHorizontalOpenTopTop));

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

            Tilesets = new Dictionary<string, Tileset>()
            {
                {tileset.Name, tileset},
            };
        }

        private void InitSystems()
        {
            updateSystems = new List<IUpdateSystem>()
            {
                new ComputerInputSystem(world),
                new UseItemSystem(world),
                new EntityActSystem(world, Tilesets),
                new MeleeAttackSystem(world),
                new RangedAttackSystem(world),
                new DeathSystem(world),
            };

            renderSystems = new List<IRenderSystem>()
            {
                new RenderMapSystem(world, Tilesets, textures),
                new RenderEntitySystem(world, textures),
                new RenderHudSystem(world, MyraWindow as GameWindow),
            };

            inventoryWindow = new InventoryWindow(this, world);
            targetingWindow = new TargetingWindow(this, world);
        }

        private void GenerateMap(bool nextLevel = false)
        {
            world.RemoveAllNonPlayerOwnedEntities();
            Generator generator = PickGenerator();
            TestPainter painter = new TestPainter();
            int width = random.Next(30, 30 + 5 * world.Depth);
            int height = random.Next(30, 30 + 5 * world.Depth);

            var map = generator.GenerateMap(width, height);
            map = painter.PaintMap(map, Tilesets["test-tileset"]);

            PlayerSpawner playerSpawner = new PlayerSpawner();
            playerSpawner.SpawnEntityForPoint(world, generator.GetPlayerStartingPosition(map));
            EnemySpawner enemySpawner = new EnemySpawner();
            enemySpawner.SpawnEntitiesForPoints(world, generator.GetEnemySpawnPoints(map));
            ItemSpawner itemSpawner = new ItemSpawner();
            itemSpawner.SpawnEntitiesForPoints(world, generator.GetItemSpawnPoints(map));
            MapPostCreationProcessor processor = new MapPostCreationProcessor();
            processor.PostProcess(world, map);

            if (nextLevel)
            {
                world.LogEntry("You descend to the next level...");
            }

            world.SetMap(map);
            FieldOfView.CalculatePlayerFOV(world);

            Game.Window.Title = string.Format("RoguelikeBase2D - Depth: {0} - Seed: {1}", world.Depth, map.Seed);
            world.CurrentState = GameState.AwaitingPlayerInput;
        }

        private Generator PickGenerator()
        {
            switch (random.Next(0, 3))
            {
                case 0:
                    return new BspInteriorGenerator();
                case 1:
                    return new BspRoomGenerator();
                default:
                    return new RoomsAndCorridorsGenerator();
            }
        }

        public override void SetActive()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (world.CurrentState == GameState.AwaitingPlayerInput)
            {
                HandleKeyboard();
            }
            else if (world.CurrentState == GameState.PlayerTurn
                    || world.CurrentState == GameState.ComputerTurn)
            {
                foreach (var system in updateSystems)
                {
                    system.Update(gameTime);
                }

                switch (world.CurrentState)
                {
                    case GameState.PlayerTurn:
                        world.CurrentState = GameState.ComputerTurn;
                        break;
                    case GameState.ComputerTurn:
                        world.CurrentState = GameState.AwaitingPlayerInput;
                        break;
                    case GameState.PlayerDeath:
                        SaveGameManager.DeleteSaveData();
                        Game.SetScreen(new MainMenuScreen(Game));
                        break;

                }
            }

            base.Update(gameTime);
        }

        private void HandleKeyboard()
        {
            if (InputDelayHelper.ReadyForInput)
            {
                var kState = Keyboard.GetState();
                if (inventoryWindow.IsOpen)
                {
                    inventoryWindow.HandleKeyboard(kState);
                }
                else if (targetingWindow.IsOpen)
                {
                    targetingWindow.HandleKeyboard(kState);
                }
                else
                {
                    HandleInGameKeyboard(kState);
                }
            }
        }

        private void HandleInGameKeyboard(KeyboardState kState)
        {
            if (kState.IsKeyDown(Keys.Escape))
            {
                SaveGameManager.SaveGame(world);
                Game.SetScreen(new MainMenuScreen(Game));
            }
            else if (kState.IsKeyDown(Keys.Up))
            {
                MovePlayer(PointConstants.Up);
            }
            else if (kState.IsKeyDown(Keys.Down))
            {
                MovePlayer(PointConstants.Down);
            }
            else if (kState.IsKeyDown(Keys.Left))
            {
                MovePlayer(PointConstants.Left);
            }
            else if (kState.IsKeyDown(Keys.Right))
            {
                MovePlayer(PointConstants.Right);
            }
            else if (kState.IsKeyDown(Keys.Enter))
            {
                MovePlayer(Point.Zero);
            }
            else if (kState.IsKeyDown(Keys.E))
            {
                CheckForExit();
            }
            else if (kState.IsKeyDown(Keys.I))
            {
                ((GameWindow)MyraWindow).InventoryPanel.Visible = true;
                inventoryWindow.OpenWindow();
                InputDelayHelper.Reset();
            }
            else if (kState.IsKeyDown(Keys.G))
            {
                TryPickUpItem();
            }
            else if (kState.IsKeyDown(Keys.T))
            {
                if (world.PlayerRef.Entity.Get<CombatEquipment>().Weapon != EntityReference.Null)
                {
                    ((GameWindow)MyraWindow).TargetingPanel.Visible = true;
                    targetingWindow.OpenWindow();
                    InputDelayHelper.Reset();
                }
            }
        }

        public void CloseInventory()
        {
            ((GameWindow)MyraWindow).InventoryPanel.Visible = false;
            inventoryWindow.CloseWindow();
            InputDelayHelper.Reset();
        }

        public void CloseTargeting()
        {
            ((GameWindow)MyraWindow).TargetingPanel.Visible = false;
            targetingWindow.CloseWindow();
            InputDelayHelper .Reset();
        }

        public void MovePlayer(Point direction)
        {
            var input = world.PlayerRef.Entity.Get<Input>();
            input.Direction = direction;
            world.PlayerRef.Entity.Set(input);
            world.CurrentState = GameState.PlayerTurn;
            InputDelayHelper.Reset();
        }

        private void CheckForExit()
        {
            var position = world.PlayerRef.Entity.Get<Position>();
            var exitTile = world.Map.GetTileFromLayer(MapLayerType.FloorDecorations, position.Point);
            if (exitTile.TileType == TileType.Exit)
            {
                GenerateMap(true);
            }
            else
            {
                world.LogEntry("The exit isn't here");
            }
            InputDelayHelper.Reset();
        }


        private void TryPickUpItem()
        {
            var name = world.PlayerRef.Entity.Get<Identity>();
            var position = world.PlayerRef.Entity.Get<Position>();
            var entitiesAtLocation = world.PhysicsWorld.GetEntitiesAtLocation(position.Point);
            if (entitiesAtLocation != null && entitiesAtLocation.Any(a => a.Entity.Has<Item>()))
            {
                var item = entitiesAtLocation.Where(a => a.Entity.Has<Item>()).FirstOrDefault();
                string itemName = item.Entity.Get<Identity>().Name;
                item.Entity.Add(new Owner() { OwnerReference = world.PlayerRef });
                item.Entity.Remove<Position>();
                world.PhysicsWorld.RemoveEntity(item, position.Point);

                world.LogEntry(string.Format("{0} picked up {1}", name.Name, itemName));
            }
            else
            {
                world.LogEntry("There's nothing here");
            }

            MovePlayer(Point.Zero);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            foreach (var layer in MapLayerTypeExtensions.AllLayers)
            {
                foreach (var system in renderSystems)
                {
                    system.Render(gameTime, spriteBatch, layer);
                }
            }

            if (targetingWindow.IsOpen)
            {
                targetingWindow.Render(gameTime, spriteBatch);
            }
        }
    }
}

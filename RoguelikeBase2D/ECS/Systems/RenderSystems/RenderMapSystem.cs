using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using RoguelikeBase2D.Maps;
using RoguelikeBase2D.Utils.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Systems.RenderSystems
{
    public class RenderMapSystem : ArchSystem, IRenderSystem
    {
        Dictionary<string, Texture2D> Textures;
        Dictionary<string, Tileset> Tilesets;

        public RenderMapSystem(GameWorld world, Dictionary<string, Tileset> tilesets, Dictionary<string, Texture2D> textures) 
            : base(world) 
        {
            Textures = textures;
            Tilesets = tilesets;
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, MapLayerType layerType)
        {
            var position = World.PlayerRef.Entity.Get<Position>();
            var calcPlayerPosition = position.Point.ToVector2() * 48;

            for (int i = 0; i < World.Map.Width; i++)
            {
                for (int j = 0; j < World.Map.Height; j++)
                {
                    var tile = World.Map.GetTileFromLayer(layerType, i, j);
                    if (tile.TileType != TileType.None)
                    {
                        var tileset = Tilesets[tile.TilesetName];
                        var sourceRect = tileset.GetRectangleForTilesetTile(tile.TilesetTileId);
                        var tileX = i * tileset.TileWidth;
                        var tileY = j * tileset.TileHeight;

                        float shading = 0f;

                        if(World.PlayerFov.Contains(new Point(i, j)))
                        {
                            shading = 1f;
                        }
                        else if (tile.IsExplored)
                        {
                            shading = 0.5f;
                        }

                        spriteBatch.Draw(Textures[tile.TilesetName], new Vector2(tileX, tileY) + RogueGame.CenterOffset, sourceRect, Color.White * shading, 0f, calcPlayerPosition, 1f, SpriteEffects.None, 1f);
                    }
                }
            }
        }
    }
}

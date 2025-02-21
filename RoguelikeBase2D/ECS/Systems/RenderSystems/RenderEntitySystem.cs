using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using RoguelikeBase2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Systems.RenderSystems
{
    public class RenderEntitySystem : ArchSystem, IRenderSystem
    {
        Dictionary<string, Texture2D> Textures;
        QueryDescription EntityQuery = new QueryDescription().WithAll<Position, SpriteInfo>();
        public RenderEntitySystem(GameWorld world, Dictionary<string, Texture2D> textures)
            : base(world)
        {
            Textures = textures;
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, MapLayerType layerType)
        {
            if(layerType == MapLayerType.FloorDecorations)
            {
                var playerPosition = World.PlayerRef.Entity.Get<Position>();

                World.World.Query(in EntityQuery, (ref Position position, ref SpriteInfo spriteInfo) =>
                {
                    if (World.PlayerFov.Contains(position.Point))
                    {
                        var renderPosition = (position.Point - playerPosition.Point).ToVector2() * 48 + RogueGame.CenterOffset;
                        spriteBatch.Draw(Textures[spriteInfo.Sprite], renderPosition, new Rectangle(0, 0, spriteInfo.Width, spriteInfo.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                });
            }
        }
    }
}

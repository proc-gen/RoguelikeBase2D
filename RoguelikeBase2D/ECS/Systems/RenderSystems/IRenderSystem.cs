using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Systems.RenderSystems
{
    internal interface IRenderSystem
    {
        void Render(GameTime gameTime, SpriteBatch spriteBatch, MapLayerType layerType);
    }
}

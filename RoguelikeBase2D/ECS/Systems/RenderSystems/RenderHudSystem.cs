using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using RoguelikeBase2D.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguelikeBase2D.ECS.Components;
using Arch.Core.Extensions;
using RoguelikeBase2D.Windows.Generated;
using GameWindow = RoguelikeBase2D.Windows.Generated.GameWindow;

namespace RoguelikeBase2D.ECS.Systems.RenderSystems
{
    public class RenderHudSystem : ArchSystem, IRenderSystem
    {
        GameWindow GameWindow { get; set; }
        public RenderHudSystem(GameWorld world, GameWindow gameWindow) : base(world) 
        {
            GameWindow = gameWindow;
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, MapLayerType layerType)
        {
            var playerStats = World.PlayerRef.Entity.Get<CombatStats>();
            GameWindow.PlayerHealthLabel.Text = string.Format("{0} / {1}", playerStats.CurrentHealth, playerStats.MaxHealth);
            GameWindow.PlayerHealthBar.Width = (int)(((float)playerStats.CurrentHealth / playerStats.MaxHealth) * 952);
        }
    }
}

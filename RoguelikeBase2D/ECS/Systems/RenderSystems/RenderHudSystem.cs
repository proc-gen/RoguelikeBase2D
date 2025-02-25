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
using RoguelikeBase2D.Screens.Generated;
using GameWindow = RoguelikeBase2D.Screens.Generated.GameWindow;

namespace RoguelikeBase2D.ECS.Systems.RenderSystems
{
    public class RenderHudSystem : ArchSystem, IRenderSystem
    {
        GameWindow GameWindow { get; set; }
        StringBuilder Log { get; set; }
        int LogEntries = 0;
        public RenderHudSystem(GameWorld world, GameWindow gameWindow) : base(world) 
        {
            GameWindow = gameWindow;
            Log = new StringBuilder();
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, MapLayerType layerType)
        {
            if (LogEntries != World.GameLog.Count)
            {
                RenderPlayerHealth();
                RenderLog();
                LogEntries = World.GameLog.Count;
            }
        }

        private void RenderPlayerHealth()
        {
            var playerStats = World.PlayerRef.Entity.Get<CombatStats>();
            GameWindow.PlayerHealthLabel.Text = string.Format("{0} / {1}", playerStats.CurrentHealth, playerStats.MaxHealth);
            GameWindow.PlayerHealthBar.Width = (int)(((float)playerStats.CurrentHealth / playerStats.MaxHealth) * 952);
        }

        private void RenderLog()
        {
            Log.Clear();
            int i = 0;

            while (i < World.GameLog.Count && i < 6)
            {
                i++;
                Log.AppendLine(World.GameLog[World.GameLog.Count - i]);
            }

            GameWindow.Log.Text = Log.ToString();
        }
    }
}

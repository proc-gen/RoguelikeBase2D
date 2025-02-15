using RoguelikeBase2D.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Systems
{
    public abstract class ArchSystem
    {
        public GameWorld World { get; set; }

        public ArchSystem(GameWorld world)
        {
            World = world;
        }
    }
}

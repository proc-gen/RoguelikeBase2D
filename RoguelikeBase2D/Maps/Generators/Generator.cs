using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Generators
{
    public abstract class Generator
    {
        public Generator() { }

        public abstract Map GenerateMap(int width, int height);
        public abstract Point GetPlayerStartingPosition(Map map);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Painters
{
    public abstract class Painter
    {
        public Painter() { }
        public abstract Map PaintMap(Map map, Tileset tileset);
    }
}

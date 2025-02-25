using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Containers
{
    public abstract class Container
    {
        public string Name { get; set; }
        public string Sprite { get; set; }
        public int MinDepthSpawn { get; set; }
        public int MaxDepthSpawn { get; set; }
    }
}

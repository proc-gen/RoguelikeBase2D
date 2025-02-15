using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct SpriteInfo
    {
        public string Sprite {  get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

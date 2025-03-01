using Arch.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct Door
    {
        public EntityReference TopTop { get; set; }
        public EntityReference Top { get; set; }
        public EntityReference Bottom { get; set; }
    }
}

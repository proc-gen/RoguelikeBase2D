using Arch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct MeleeAttack : IAttack
    {
        public EntityReference Source { get; set; }
        public EntityReference Target { get; set; }
    }
}

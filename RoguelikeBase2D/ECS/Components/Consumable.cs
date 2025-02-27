using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct Consumable
    {
        public ConsumableType ConsumableType { get; set; }
        public int Amount { get; set; }
    }
}

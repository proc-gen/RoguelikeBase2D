using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Containers
{
    public class ItemContainer : Container
    {
        public bool Consumable { get; set; }
        public string ConsumableType { get; set; }
        public string Effect { get; set; }
        public int EffectAmount { get; set; }
    }
}

using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct Weapon
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Range { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}

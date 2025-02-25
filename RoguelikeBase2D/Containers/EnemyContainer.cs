using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Containers
{
    public class EnemyContainer : Container
    {
        public int ViewDistance { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public string WeaponItem { get; set; }
        public string ArmorItem { get; set; }
    }
}

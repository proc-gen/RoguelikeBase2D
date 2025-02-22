using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Containers
{
    public struct EnemyContainer
    {
        public string Name { get; set; }
        public int ViewDistance { get; set; }
        public string Sprite { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public string WeaponItem { get; set; }
        public string ArmorItem { get; set; }
        public int MinDepthSpawn { get; set; }
        public int MaxDepthSpawn { get; set; }
    }
}

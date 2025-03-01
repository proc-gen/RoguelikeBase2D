using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Containers
{
    public class ItemContainer : Container
    {
        
    }

    public class ConsumableItemContainer : ItemContainer
    {
        public bool Consumable { get; set; }
        public ConsumableType ConsumableType { get; set; }
        public string Effect { get; set; }
        public int EffectAmount { get; set; }
    }

    public class WeaponItemContainer : ItemContainer
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Range { get; set; }
        public WeaponType WeaponType { get; set; }
    }

    public class ArmorItemContainer : ItemContainer
    {
        public int Armor { get; set; }
    }
}

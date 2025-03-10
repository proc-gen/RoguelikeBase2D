﻿using Arch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct CombatEquipment
    {
        public EntityReference Weapon { get; set; }
        public EntityReference Armor { get; set; }
        public CombatEquipment()
        {
            Weapon = EntityReference.Null;
            Armor = EntityReference.Null;
        }
    }
}

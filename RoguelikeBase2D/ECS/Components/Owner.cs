﻿using Arch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct Owner
    {
        public EntityReference OwnerReference { get; set; }
    }
}

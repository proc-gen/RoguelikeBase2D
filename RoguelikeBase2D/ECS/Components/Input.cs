﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.ECS.Components
{
    public struct Input
    {
        public bool SkipTurn { get; set; }
        public Point Direction { get; set; }
        public bool Processed { get; set; }
        public bool CanAct { get; set; }
    }
}

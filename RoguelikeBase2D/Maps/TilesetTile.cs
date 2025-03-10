﻿using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public class TilesetTile
    {
        public int Id { get; set; }
        public TileType[] TileTypes { get; set; }

        public TilesetTile() { }
        public TilesetTile(int id, params TileType[] tileTypes)
        {
            Id = id;
            TileTypes = tileTypes;
        }
    }
}

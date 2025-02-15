using Microsoft.Xna.Framework;
using RoguelikeBase2D.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Spawners
{
    internal interface ISpawner
    {
        void SpawnEntitiesForPoints(GameWorld world, HashSet<Point> points);
        void SpawnEntityForPoint(GameWorld world, Point point);
    }
}

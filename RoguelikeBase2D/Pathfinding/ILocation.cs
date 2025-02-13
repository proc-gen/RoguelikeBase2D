using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace RoguelikeBase2D.Pathfinding
{
    public interface ILocation<T> : IEquatable<ILocation<T>>
    {
        Point Point { get; }
        List<T> AdjacentLocations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Constants
{
    public enum MapLayerType
    {
        Floor,
        Wall,
    }

    public static class MapLayerTypeExtensions
    {
        public static MapLayerType[] AllLayers { get; private set; } = [MapLayerType.Floor, MapLayerType.Wall];
    }
}

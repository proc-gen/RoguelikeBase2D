using Microsoft.Xna.Framework;
using RoguelikeBase2D.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps
{
    public class Map
    {
        public Dictionary<MapLayerType, MapLayer> MapLayers { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Seed { get; set; }

        public Map(int width, int height) 
        {
            MapLayers = new Dictionary<MapLayerType, MapLayer>()
            {
                { MapLayerType.Wall, new MapLayer(MapLayerType.Wall.ToString(), width, height) },
                { MapLayerType.Floor, new MapLayer(MapLayerType.Floor.ToString(), width, height) },
            };
            Width = width;
            Height = height;
        }

        public Tile GetTileFromLayer(MapLayerType layer, int x, int y)
        {
            return MapLayers[layer].GetTile(x, y);
        }

        public Tile GetTileFromLayer(MapLayerType layer, Point point)
        {
            return MapLayers[layer].GetTile(point);
        }

        public void SetTileInLayer(MapLayerType layer, int x, int y, Tile tile)
        {
            MapLayers[layer].SetTile(x, y, tile);
        }

        public void SetTileInLayer(MapLayerType layer, Point point, Tile tile)
        {
            MapLayers[layer].SetTile(point, tile);
        }

        public void SetIsExplored(Point point, bool isExplored)
        {
            foreach(var layer in MapLayers.Values)
            {
                var tile = layer.GetTile(point);
                tile.IsExplored = isExplored;
                layer.SetTile(point, tile);
            }
        }

        public void SetIsExplored(int x, int y, bool isExplored)
        {
            foreach (var layer in MapLayers.Values)
            {
                var tile = layer.GetTile(x, y);
                tile.IsExplored = isExplored;
                layer.SetTile(x, y, tile);
            }
        }
    }
}

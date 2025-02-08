using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Painters
{
    public class TestPainter : Painter
    {
        public override Map PaintMap(Map map, Tileset tileset) 
        {
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {

                    var floorTile = map.GetTileFromLayer(Constants.MapLayerType.Floor, i, j);
                    if (floorTile.TileType != Constants.TileType.None)
                    {
                        floorTile.TilesetName = tileset.Name;
                        floorTile.TilesetTileId = (i % 2 == j % 2) ? 40 : 41;
                        map.SetTileInLayer(Constants.MapLayerType.Floor, i, j, floorTile);
                    }

                    var wallTile = map.GetTileFromLayer(Constants.MapLayerType.Wall, i, j);
                    if (wallTile.TileType != Constants.TileType.None)
                    {
                        wallTile.TilesetName = tileset.Name;
                        var tilesetTile = tileset.TilesetTiles.Where(a => a != null && a.TileTypes.Contains(wallTile.TileType)).FirstOrDefault();
                        wallTile.TilesetTileId = tilesetTile?.Id ?? 31;
                        map.SetTileInLayer(Constants.MapLayerType.Wall, i, j, wallTile);
                    }
                }
            }

            return map;
        }
    }
}

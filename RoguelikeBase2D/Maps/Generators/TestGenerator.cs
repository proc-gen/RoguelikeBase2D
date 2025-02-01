using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Maps.Generators
{
    public class TestGenerator : Generator
    {
        public override Map GenerateMap(int width, int height)
        {
            Map map = new Map(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(i == 0 || i == (width - 1) || j == 0 || j == (height - 1))
                    {
                        var tile = map.GetTileFromLayer(Constants.MapLayerType.Wall, i, j);
                        tile.TileType = Constants.TileType.Wall;
                        map.SetTileInLayer(Constants.MapLayerType.Wall, i, j, tile);
                    }
                    else
                    {
                        var tile = map.GetTileFromLayer(Constants.MapLayerType.Floor, i, j);
                        tile.TileType = Constants.TileType.Floor;
                        map.SetTileInLayer(Constants.MapLayerType.Floor, i, j, tile);
                    }
                }
            }

            return map;
        }

        public override Point GetPlayerStartingPosition(Map map)
        {
            return new Point(map.Width / 2, map.Height / 2);
        }
    }
}

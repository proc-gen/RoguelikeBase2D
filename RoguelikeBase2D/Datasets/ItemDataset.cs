using RoguelikeBase2D.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Datasets
{
    public static class ItemDataset
    {
        static List<ItemContainer> itemContainers = [
            new ItemContainer(){
                Name = "Health Potion",
                Sprite = "health-potion",
                MinDepthSpawn = 0,
                MaxDepthSpawn = 999,
            }
        ];

        public static List<ItemContainer> GetItemContainersForDepth(int depth)
        {
            return itemContainers.Where(a => a.MinDepthSpawn <= depth && a.MaxDepthSpawn >= depth).ToList();
        }
    }
}

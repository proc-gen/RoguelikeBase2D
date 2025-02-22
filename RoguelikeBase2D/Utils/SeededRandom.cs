using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Utils
{
    public class SeededRandom : Random
    {
        public int Seed { get; private set; }
 
        public static SeededRandom New()
        {
            return new SeededRandom(Shared.Next());
        }

        public SeededRandom(int seed) : base(seed) 
        {
            Seed = seed;
        }
    }
}

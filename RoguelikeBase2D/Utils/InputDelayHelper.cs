using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Utils
{
    public class InputDelayHelper
    {
        float ResponseTime = .15f;
        public bool ReadyForInput { get; private set; }
        float counter = 0f;

        public InputDelayHelper(float delay = .15f)
        {
            ReadyForInput = false;
            ResponseTime = delay;
        }

        public void Update(GameTime gameTime)
        {
            if (!ReadyForInput)
            {
                counter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (counter > ResponseTime)
                {
                    counter = 0f;
                    ReadyForInput = true;
                }
            }
        }

        public void Reset()
        {
            ReadyForInput = false;
        }
    }
}

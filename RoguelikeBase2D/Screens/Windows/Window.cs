using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Screens.Windows
{
    public abstract class Window
    {
        public bool IsOpen { get; set; }

        public abstract void OpenWindow();

        public abstract void CloseWindow();
    }
}

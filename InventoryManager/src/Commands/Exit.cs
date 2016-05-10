using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;
using System.Windows.Forms;

namespace MyGame
{
    class Exit : Command
    { 
          public override void Do(int x, int y)
        {
            Environment.Exit(0);
        }
    }
}

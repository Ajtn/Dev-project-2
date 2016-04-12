using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public class ScrollUp : Command
    {
        public override void Do(int i, int j)
        {
            GameMain.pTable.pBuffer--;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class GetReport : Command
    {
        public override void Do(int x, int y)
        {
            GetDateForm lForm = new GetDateForm();
            lForm.Run();
        }
    }
}
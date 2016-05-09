using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
	class DeleteItem : Command
	{
		public override void Do(int x, int y)
		{
			string[] row;

			row = GameMain.pTable.GetRow (x, y);

			if (GameMain.pTable.pTableName == )
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    class DisplayOrdersTable : Command
    {
        public override void Do(int aX, int ay)
        {
            GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, GameMain.inventoryDB.runOrdersQuery());
            GameMain.pTable.pHeader = new string[] { "Order ID", "Date Ordered", "Date Expected", "Processed", "Value of Order" };
            GameMain.pUIElements.Add(GameMain.pTable);
        }
    }
}

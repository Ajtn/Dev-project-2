using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    public class DisplayCurrentStockTable : Command
    {
        public override void Do(int x, int y)
        {
            GameMain.pTable = new Table(10, 10, GameMain.TABLE_WIDTH, GameMain.TABLE_HEIGHT, Color.Black, GameMain.inventoryDB.runDatabaseQuery(new string[] { "StockID", "ProductName", "NumberInStock" }, "CurrentStock"), "CurrentStock");
            GameMain.pTable.pScrollBar.Initialise();
            GameMain.pTable.pHeader = new string[] {"Stock ID", "Number Sold", "Sale ID"};
        }
    }
}

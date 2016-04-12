using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    class DisplayItemTable : Command
    {
        public override void Do(int x, int y)
        {
            GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, GameMain.inventoryDB.runDatabaseQuery(new string[] { "StockID", "ProductName", "SalePrice", "OrderPrice", "AverageSold" }, "StockItem"));
            GameMain.pTable.pHeader = new string[] { "Stock ID", "Product Name", "Sale Price", "Order Price", "Average Sold" };
            GameMain.pUIElements.Add(GameMain.pTable);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    class DisplaySaleTable : Command
    {

        public override void Do(int x, int y)
        {
            //GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, GameMain.inventoryDB.runSaleQuery());
            //GameMain.pTable.pHeader = new string[] { "StockID", "Number Sold", "SaleID" };
            //GameMain.pUIElements.Add(GameMain.pTable);

            GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, GameMain.inventoryDB.runDatabaseQuery( new string[] { "StockID", "NumberSold", "SaleID" }, "Sale"));
            GameMain.pTable.pHeader = new string[] { "StockID", "Number Sold", "SaleID" };
            GameMain.pUIElements.Add(GameMain.pTable);
        }
    }
}

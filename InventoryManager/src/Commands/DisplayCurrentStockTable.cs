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
            /*GameMain.pUIElements.Remove(GameMain.pTable);
            GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, TestData.GenerateData());
            GameMain.pTable.pHeader = new string[] { "ID", "Numbers", "Data" };
            GameMain.pUIElements.Add(GameMain.pTable);*/

            GameMain.pUIElements.Remove(GameMain.pTable);
            GameMain.pTable = new Table(10, 10, GameMain.TABLE_WIDTH, GameMain.TABLE_HEIGHT, Color.Black, GameMain.inventoryDB.runDatabaseQuery(new string[] { "StockID", "ProductName", "NumberInStock" }, "CurrentStock"));
            GameMain.pTable.pScrollBar.Initialise();
            GameMain.pTable.pHeader = new string[] {"Stock ID", "Number Sold", "Sale ID"};
            GameMain.pUIElements.Add(GameMain.pTable);
        }
    }
}

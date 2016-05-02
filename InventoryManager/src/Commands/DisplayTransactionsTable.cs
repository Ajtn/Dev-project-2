using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    class DisplayTransactionsTable : Command
    {

        public override void Do(int x, int y)
        {
            GameMain.pTable = new Table(10, 10, GameMain.TABLE_WIDTH, GameMain.TABLE_HEIGHT, Color.Black, GameMain.inventoryDB.runDatabaseQuery("TransactionItems", "Transaction", "Item", 
                                                                                                                                                new string[] {"Quantity"}, new string[] {"Date"}, 
                                                                                                                                                new string[] {"Name", "SalePrice","CostPrice"}, 
                                                                                                                                                "TransID", "ItemID" ), "Sale");
            GameMain.pTable.pScrollBar.Initialise();
            GameMain.pTable.pHeader = new string[] { "Quantity", "Date", "Item Name", "Sale Price", "Cost Price" };
        }
    }
}

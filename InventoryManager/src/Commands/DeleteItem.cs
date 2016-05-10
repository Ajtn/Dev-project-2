using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwinGameSDK;


namespace MyGame
{
    class DeleteItem : Command
    {
        public override void Do(int x, int y)
        {
            DialogResult confirmationOne = MessageBox.Show("Are you sure you want to delete this record?", "DELETE RECORD", MessageBoxButtons.YesNo);

            if (confirmationOne == DialogResult.Yes)
            {
                string[] row;

                row = GameMain.pTable.GetRow(x, y);
                DialogResult confirmationTwo = MessageBox.Show("Deleting this record will remove existing transaction and Order information. Do you wish to proceed?", "WARNING", MessageBoxButtons.YesNo);
                bool cancelDelete = false;

                if (confirmationTwo == DialogResult.Yes)
                {

                if (GameMain.pTable.pTableName == "Item")
                {
                    if (GameMain.inventoryDB.checkRecordExists("AvailableItems", "ItemID", row[0]))
                    {
                        
                        if (GameMain.inventoryDB.checkRecordExists("TransactionItems", "ItemID", row[0]))
                        {
                            
                                string[,] itemsInTransaction;


                                itemsInTransaction = GameMain.inventoryDB.runDatabaseQueryWithWhere(new string[] { "TransID", "ItemID" }, "ItemID", row[0], "TransactionItems");

                                List<string> transactionIDs = new List<string>();

                                for (int i = 0; i < itemsInTransaction.Length / 2; i++)
                                {
                                    if (!transactionIDs.Contains(itemsInTransaction[i, 0]))
                                        transactionIDs.Add(itemsInTransaction[i, 0]);
                                }

                                int[] totalTransactions = new int[transactionIDs.Count];

                                for (int i = 0; i < transactionIDs.Count; i++)
                                {
                                    totalTransactions[i] = GameMain.inventoryDB.retrieveAmountOfRows("TransactionItems", "TransID", transactionIDs[i]);
                                }

                                for (int i = 0; i < totalTransactions.Length; i++)
                                {
                                    string[,] temp;
                                    temp = GameMain.inventoryDB.runDatabaseQueryWithWhere(new string[] { "TransID", "ItemID" }, "ItemID", "TransID", row[0], transactionIDs[i], "TransactionItems");

                                    if (temp.Length / 2 == totalTransactions[i])
                                    {
                                        GameMain.inventoryDB.deleteDatabaseRow("ItemID", "TransID", row[0], transactionIDs[i], "TransactionItems");
                                        GameMain.inventoryDB.deleteDatabaseRow("TransID", transactionIDs[i], "Transaction");
                                    }
                                    else GameMain.inventoryDB.deleteDatabaseRow("ItemID", "TransID", row[0], transactionIDs[i], "TransactionItems"); 
                                }
                            }

                    }
                    else cancelDelete = true;

                    }

                    if (GameMain.inventoryDB.checkRecordExists("OrderItems", "ItemID", row[0]))
                    {
                        string[,] itemsInOrder = GameMain.inventoryDB.runDatabaseQueryWithWhere(new string[] { "OrderID", "ItemID" }, "ItemID", row[0], "OrderItems");

                        List<string> orderIDs = new List<string>();

                        for (int i = 0; i < itemsInOrder.Length / 2; i++)
                        {
                            if (!orderIDs.Contains(itemsInOrder[i, 0]))
                                orderIDs.Add(itemsInOrder[i, 0]);
                        }

                        int[] totalOrders = new int[orderIDs.Count];

                        for (int i = 0; i < orderIDs.Count; i++)
                        {
                            totalOrders[i] = GameMain.inventoryDB.retrieveAmountOfRows("OrderItems", "OrderID", orderIDs[i]);
                        }

                        for (int i = 0; i < totalOrders.Length; i++)
                        {
                            string[,] temp;
                            temp = GameMain.inventoryDB.runDatabaseQueryWithWhere(new string[] { "OrderID", "ItemID" }, "ItemID", "OrderID", row[0], orderIDs[i], "OrderItems");

                            if (temp.Length / 2 == totalOrders[i])
                            {
                                GameMain.inventoryDB.deleteDatabaseRow("ItemID", "OrderID", row[0], orderIDs[i], "OrderItems");
                                GameMain.inventoryDB.deleteDatabaseRow("OrderID", orderIDs[i], "StockOrder");
                            }
                            else GameMain.inventoryDB.deleteDatabaseRow("ItemID", "OrderID", row[0], orderIDs[i], "OrderItems");
                        }
                    }
                }
                    if (!cancelDelete)
                    {
                        GameMain.inventoryDB.deleteDatabaseRow("ItemID", row[0], "AvailableItems");
                        GameMain.inventoryDB.deleteDatabaseRow("ItemID", row[0], "Item");
                    }

                    
                

                MessageBox.Show("Item: " + row[1] + " successfully deleted.", "Success!", MessageBoxButtons.OK);

                GameMain.pCurrentTable.OnClick(SwinGame.MousePosition());
            }
        }
    }
}

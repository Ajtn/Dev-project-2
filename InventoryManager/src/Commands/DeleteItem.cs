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

                if (GameMain.pTable.pTableName == "Item")
                {
                    if (GameMain.inventoryDB.checkRecordExists("AvailableItems", "ItemID", row[0]))
                    {
                        bool cancelDelete = false;
                        if(GameMain.inventoryDB.checkRecordExists("TransactionItems", "ItemID",row[0]))
                        {
                            DialogResult confirmationTwo = MessageBox.Show("Deleting this record will remove existing transaction information. Do you wish to proceed?", "WARNING", MessageBoxButtons.YesNo);

                            if(confirmationTwo == DialogResult.Yes)
                            {
                                
                                string transactionID = GameMain.inventoryDB.findPrimaryKey("ItemID",row[0],"TransID","TransactionItems");
                                int amountOfItemRows = GameMain.inventoryDB.retrieveAmountOfRows("TransactionItems","ItemID", row[0]);
                                int amountOfTransRows = GameMain.inventoryDB.retrieveAmountOfRows("TransactionItems","TransID", transactionID);

                                if (amountOfItemRows == amountOfTransRows)
                                {
                                    GameMain.inventoryDB.deleteDatabaseRow("TransID", transactionID, "Transaction");
                                }
                                GameMain.inventoryDB.deleteDatabaseRow("ItemID", row[0], "TransactionItems");
                            }
                            else cancelDelete = true;
                        }

                        if (!cancelDelete)
                        {                        
                            GameMain.inventoryDB.deleteDatabaseRow("ItemID", row[0], "AvailableItems");
                            GameMain.inventoryDB.deleteDatabaseRow("ItemID", row[0], "Item");
                        }

        
                    } else GameMain.inventoryDB.deleteDatabaseRow("ItemID", row[0], "Item");
                }

                MessageBox.Show("Item: " + row[1] + " successfully deleted.", "Success!", MessageBoxButtons.OK);

                GameMain.pCurrentTable.OnClick(SwinGame.MousePosition());
            }
        }
    }
}

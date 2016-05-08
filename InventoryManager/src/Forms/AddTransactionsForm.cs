using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class AddTransactionsForm
    {
        public Form fForm;
        private TextBox[] fTextBoxes;
        private Label[] fLabels;
        private Label fHeader;
        private System.Windows.Forms.Button fButton;

        public AddTransactionsForm()
        {
            fForm = new Form();
            fTextBoxes = new TextBox[6];
            fLabels = new Label[6];

            fTextBoxes[0] = new TextBox();
            fTextBoxes[0].Left = 120;
            fTextBoxes[0].Top = 60;
            fForm.Controls.Add(fTextBoxes[0]);

            fLabels[0] = new Label();
            fLabels[0].Text = "Quantity";
            fLabels[0].Left = 20;
            fLabels[0].Top = 60;
            fForm.Controls.Add(fLabels[0]);

            fTextBoxes[1] = new TextBox();
            fTextBoxes[1].Left = 120;
            fTextBoxes[1].Top = 90;
            fForm.Controls.Add(fTextBoxes[1]);

            fLabels[1] = new Label();
            fLabels[1].Text = "Date";
            fLabels[1].Left = 20;
            fLabels[1].Top = 90;
            fForm.Controls.Add(fLabels[1]);

            fTextBoxes[2] = new TextBox();
            fTextBoxes[2].Left = 120;
            fTextBoxes[2].Top = 120;
            fForm.Controls.Add(fTextBoxes[2]);

            fLabels[2] = new Label();
            fLabels[2].Text = "Item Name";
            fLabels[2].Left = 20;
            fLabels[2].Top = 120;
            fForm.Controls.Add(fLabels[2]);

            fHeader = new Label();
            fHeader.Text = GameMain.pCurrentTable.pText;
            fHeader.Left = 50;
            fHeader.Top = 30;
            fForm.Controls.Add(fHeader);

            fButton = new System.Windows.Forms.Button();
            fButton.Text = "Add";
            fButton.Left = 150;
            fButton.Top = 200;
            fButton.Click += AddButtonClick;
            fForm.Controls.Add(fButton);

        }

        public void AddButtonClick(object sender, EventArgs args)
        {
            string[] transactionItemsArguments = new string[3];
            string[] transactionArguments = new string[1];
            string[] itemArguments = new string[1];
            string itemID;

            transactionItemsArguments[0] = fTextBoxes[0].Text;
            transactionArguments[0] = fTextBoxes[1].Text;
            itemArguments[0] = fTextBoxes[2].Text;

            

            if (GameMain.inventoryDB.checkRecordExists("Item", "Name", itemArguments[0]))
            {
                if (!GameMain.inventoryDB.checkRecordExists("Transaction","TransID", transactionArguments[0]))
                    GameMain.inventoryDB.addDatabaseRow(transactionArguments, new string[] { "Date" }, "Transaction");

                transactionItemsArguments[2] = GameMain.inventoryDB.findPrimaryKey("Name", itemArguments[0], "ItemID", "Item");

                
                transactionItemsArguments[1] = GameMain.inventoryDB.findPrimaryKey("Date", transactionArguments[0],"TransID","Transaction");

                GameMain.inventoryDB.addDatabaseRow(transactionItemsArguments, new string[] { "Quantity", "TransID", "ItemID" }, "TransactionItems");
            }

            //GameMain.inventoryDB.addDatabaseRow(dbArguments, GameMain.pTable.pTableName);
            fForm.Close();
        }

        public void Run()
        {
            fForm.ShowDialog();
        }
    }
}

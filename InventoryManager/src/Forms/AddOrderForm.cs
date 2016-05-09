using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class AddOrderForm
    {
        public Form fForm;
        private TextBox[] fTextBoxes;
        private Label[] fLabels;
        private Label fHeader;
        private System.Windows.Forms.Button fButton;

        public AddOrderForm()
        {
            fForm = new Form();
            fTextBoxes = new TextBox[4];
            fLabels = new Label[4];

            fTextBoxes[0] = new TextBox();
            fTextBoxes[0].Left = 120;
            fTextBoxes[0].Top = 60;
            fForm.Controls.Add(fTextBoxes[0]);

            fLabels[0] = new Label();
            fLabels[0].Text = "Item Name";
            fLabels[0].Left = 20;
            fLabels[0].Top = 60;
            fForm.Controls.Add(fLabels[0]);

            fTextBoxes[1] = new TextBox();
            fTextBoxes[1].Left = 120;
            fTextBoxes[1].Top = 90;
            fForm.Controls.Add(fTextBoxes[1]);

            fLabels[1] = new Label();
            fLabels[1].Text = "Quantity Ordered";
            fLabels[1].Left = 20;
            fLabels[1].Top = 90;
            fForm.Controls.Add(fLabels[1]);

            fTextBoxes[2] = new TextBox();
            fTextBoxes[2].Left = 120;
            fTextBoxes[2].Top = 120;
            fForm.Controls.Add(fTextBoxes[2]);

            fLabels[2] = new Label();
            fLabels[2].Text = "Date";
            fLabels[2].Left = 20;
            fLabels[2].Top = 120;
            fForm.Controls.Add(fLabels[2]);

            fTextBoxes[3] = new TextBox();
            fTextBoxes[3].Left = 120;
            fTextBoxes[3].Top = 150;
            fForm.Controls.Add(fTextBoxes[3]);

            fLabels[3] = new Label();
            fLabels[3].Text = "Employee Name";
            fLabels[3].Left = 20;
            fLabels[3].Top = 150;
            fForm.Controls.Add(fLabels[3]);

            fHeader = new Label();
            fHeader.Text = GameMain.pCurrentTable.pText;
            fHeader.Left = 50;
            fHeader.Top = 30;
            fForm.Controls.Add(fHeader);

            fButton = new System.Windows.Forms.Button();
            fButton.Text = "Add";
            fButton.Left = 150;
            fButton.Top = 170;
            fButton.Click += AddButtonClick;
            fForm.Controls.Add(fButton);

        }

        public void AddButtonClick(object sender, EventArgs args)
        {
            //Inserting an order requires that we work with three different tables
            //Currently, it is a requirement that stock being ordered already exists in the database
            string[] stockOrderArguments = new string[2]; 
            string[] orderItemsArguments = new string[3];
            string[] itemArguments = new string[1];

            stockOrderArguments[0] = fTextBoxes[2].Text; //Assign user inputted Date
            stockOrderArguments[1] = fTextBoxes[3].Text; //Assign user inputted Name

            orderItemsArguments[0] = fTextBoxes[1].Text; //Assign user input quantity ordered

            itemArguments[0] = fTextBoxes[0].Text; //Assign item name

            //Check if we're ordering an item the store already stocks, items ordered by employees must already exist in the database
            if (GameMain.inventoryDB.checkRecordExists("Item", "Name", itemArguments[0]))
            {
                //Check if an employee has already put through a stockorder today, if they haven't we need to create a new row in the stockOrder table.
                if (!GameMain.inventoryDB.checkRecordExists("StockOrder", "Date", "EmployeeName", stockOrderArguments[0], stockOrderArguments[1]))
                {
                    GameMain.inventoryDB.addDatabaseRow(stockOrderArguments, new string[] { "Date", "EmployeeName" }, "StockOrder");
                    //Find the primary key of our newly created stock order
                    orderItemsArguments[1] = GameMain.inventoryDB.findPrimaryKey("Date", "EmployeeName", stockOrderArguments[0], stockOrderArguments[1], "OrderID", "StockOrder");
                }
                else  orderItemsArguments[1] = GameMain.inventoryDB.findPrimaryKey("Date","EmployeeName", stockOrderArguments[0], stockOrderArguments[1], "OrderID", "StockOrder"); //Find the primary key of our existing stock order.

        
                    //Find the primary key of our item
                    orderItemsArguments[2] = GameMain.inventoryDB.findPrimaryKey("Name", itemArguments[0], "ItemID", "Item"); 

                    //Insert a new record into orderItems
                    GameMain.inventoryDB.addDatabaseRow(orderItemsArguments, new string[] { "Quantity", "OrderID", "ItemID" }, "OrderItems");

                    fForm.Close();
                    return;
            }
            else
            {
                MessageBox.Show("You can't order items that are not already stocked!");
                fForm.Close();
                return;
            }
            

        }

        public void Run()
        {
            fForm.ShowDialog();
        }
    }
}

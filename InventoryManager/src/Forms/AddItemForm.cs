﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class AddItemForm
    {
        public Form fForm;
        private TextBox[] fTextBoxes;
        private Label[] fLabels;
        private Label fHeader;
        private System.Windows.Forms.Button fButton;

        public AddItemForm()
        {
            fForm = new Form();
            fTextBoxes = new TextBox[4];
            fLabels = new Label[4];

            fTextBoxes[0] = new TextBox();
            fTextBoxes[0].Left = 120;
            fTextBoxes[0].Top = 60;
            fForm.Controls.Add(fTextBoxes[0]);

            fLabels[0] = new Label();
            fLabels[0].Text = "Name";
            fLabels[0].Left = 20;
            fLabels[0].Top = 60;
            fForm.Controls.Add(fLabels[0]);

            fTextBoxes[1] = new TextBox();
            fTextBoxes[1].Left = 120;
            fTextBoxes[1].Top = 90;
            fForm.Controls.Add(fTextBoxes[1]);

            fLabels[1] = new Label();
            fLabels[1].Text = "Sale Price";
            fLabels[1].Left = 20;
            fLabels[1].Top = 90;
            fForm.Controls.Add(fLabels[1]);

            fTextBoxes[2] = new TextBox();
            fTextBoxes[2].Left = 120;
            fTextBoxes[2].Top = 120;
            fForm.Controls.Add(fTextBoxes[2]);

            fLabels[2] = new Label();
            fLabels[2].Text = "Cost Price";
            fLabels[2].Left = 20;
            fLabels[2].Top = 120;
            fForm.Controls.Add(fLabels[2]);

            fTextBoxes[3] = new TextBox();
            fTextBoxes[3].Left = 120;
            fTextBoxes[3].Top = 150;
            fForm.Controls.Add(fTextBoxes[3]);

            fLabels[3] = new Label();
            fLabels[3].Text = "Quantity";
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
            fButton.Top = 200;
            fButton.Click += AddButtonClick;
            fForm.Controls.Add(fButton);
        }

        public void AddButtonClick(object sender, EventArgs args)
        {
            //When adding an item, we need to work with two tables.
            //Firstly an Item must be inserted.
            //The primary key of the item must be found and a record must be made in the availableItems table
            string[] itemArguments = new string[3];
            string[] availableitemsArguments = new string[2];

            int temp = 0;

            //Fill our argument arrays with the user specified values.
            //NULL check here is not really needed, as the form will not process unless all fields are filled
            for (int i = 0; i < 3; i++)
            {
                if (fTextBoxes[i] == null)
                {
                    itemArguments[i] = null;
                }
                else itemArguments[i] = fTextBoxes[i].Text;

                temp = i;
            }
            temp++;
            for (int i = 0; i < 1; i++)
            {
                if (fTextBoxes[temp] == null)
                {
                    availableitemsArguments[i] = null;
                }
                else availableitemsArguments[i] = fTextBoxes[temp].Text;
            }

           
            //Add our new item to the Item table.
            GameMain.inventoryDB.addDatabaseRow(itemArguments, new string[] {"Name", "SalePrice", "CostPrice"}, "Item");

            //Find the primary key of our newly created item and store it as an argument to availableItems.
            availableitemsArguments[1] = GameMain.inventoryDB.findPrimaryKey("Name", fTextBoxes[0].Text, "ItemID", "Item");

            //add our new available item to the database.
            GameMain.inventoryDB.addDatabaseRow(availableitemsArguments, new string[] {"Quantity", "ItemID"  }, "AvailableItems");
            
            fForm.Close();
        }

        public void Run()
        {
            fForm.ShowDialog();
        }
    }
}

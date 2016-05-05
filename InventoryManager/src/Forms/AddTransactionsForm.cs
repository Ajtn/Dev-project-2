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
            fLabels[3].Text = "Stock Quantity";
            fLabels[3].Left = 20;
            fLabels[3].Top = 150;
            fForm.Controls.Add(fLabels[3]);

            fTextBoxes[4] = new TextBox();
            fTextBoxes[4].Left = 120;
            fTextBoxes[4].Top = 180;
            fForm.Controls.Add(fTextBoxes[4]);

            fLabels[4] = new Label();
            fLabels[4].Text = "Date";
            fLabels[4].Left = 20;
            fLabels[4].Top = 180;
            fForm.Controls.Add(fLabels[4]);

            fTextBoxes[5] = new TextBox();
            fTextBoxes[5].Left = 120;
            fTextBoxes[5].Top = 210;
            fForm.Controls.Add(fTextBoxes[5]);

            fLabels[5] = new Label();
            fLabels[5].Text = "Transaction Qty";
            fLabels[5].Left = 20;
            fLabels[5].Top = 210;
            fForm.Controls.Add(fLabels[5]);

            fHeader = new Label();
            fHeader.Text = GameMain.pCurrentTable.pText;
            fHeader.Left = 50;
            fHeader.Top = 30;
            fForm.Controls.Add(fHeader);

            fButton = new System.Windows.Forms.Button();
            fButton.Text = "Add";
            fButton.Left = 150;
            fButton.Top = 270;
            fButton.Click += AddButtonClick;
            fForm.Controls.Add(fButton);

        }

        public void AddButtonClick(object sender, EventArgs args)
        {
            string[] dbArguments = new string[fTextBoxes.Length];
            for (int i = 0; i < fTextBoxes.Length; i++)
            {
                if (fTextBoxes[i] == null)
                {
                    dbArguments[i] = null;
                }
                else dbArguments[i] = fTextBoxes[i].Text;
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

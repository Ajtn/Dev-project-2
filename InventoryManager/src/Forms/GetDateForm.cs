using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class GetDateForm
    {
        public Form fForm;
        private TextBox[] fTextBoxes;
        private Label[] fLabels;
        private Label fHeader;
        private System.Windows.Forms.Button fButton;

        public GetDateForm()
        {
            fForm = new Form();
            fTextBoxes = new TextBox[2];
            fLabels = new Label[2];

            fTextBoxes[0] = new TextBox();
            fTextBoxes[0].Left = 120;
            fTextBoxes[0].Top = 60;
            fForm.Controls.Add(fTextBoxes[0]);

            fLabels[0] = new Label();
            fLabels[0].Text = "Start Date";
            fLabels[0].Left = 20;
            fLabels[0].Top = 60;
            fForm.Controls.Add(fLabels[0]);

            fTextBoxes[1] = new TextBox();
            fTextBoxes[1].Left = 120;
            fTextBoxes[1].Top = 90;
            fForm.Controls.Add(fTextBoxes[1]);

            fLabels[1] = new Label();
            fLabels[1].Text = "End Date";
            fLabels[1].Left = 20;
            fLabels[1].Top = 90;
            fForm.Controls.Add(fLabels[1]);

            fHeader = new Label();
            fHeader.Text = "Generate Report";
            fHeader.Left = 50;
            fHeader.Top = 30;
            fForm.Controls.Add(fHeader);

            fButton = new System.Windows.Forms.Button();
            fButton.Text = "Generate";
            fButton.Left = 100;
            fButton.Top = 120;
            fButton.Click += AddButtonClick;
            fForm.Controls.Add(fButton);
        }
        public void AddButtonClick(object sender, EventArgs args)
        {
            GameMain.inventoryDB.ExportAsCSV("TransactionItems", "Transaction", "Item", new string[] { "Quantity" }, new string[] { "Date" }, new string[] { "Name", "SalePrice", "CostPrice" }, "TransID", "ItemID", fTextBoxes[0].Text, fTextBoxes[1].Text);
            fForm.Close();
        }

        public void Run()
        {
            fForm.ShowDialog();
        }
    }
}

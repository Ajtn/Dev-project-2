using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class AddForm
    {
        public Form fForm;
        private TextBox[] fTextBoxes;
        private Label[] fLabels;
        private Label fHeader;
        private System.Windows.Forms.Button fButton;

        public AddForm()
        {
            fForm = new Form();
            fTextBoxes = new TextBox[GameMain.pTable.pHeader.Length];
            fLabels = new Label[GameMain.pTable.pHeader.Length];

            for (int i = 1; i < fTextBoxes.Length; i++)
            {
                fTextBoxes[i] = new TextBox();
                fTextBoxes[i].Left = 120;
                fTextBoxes[i].Top = 30 * i + 30;
                fForm.Controls.Add(fTextBoxes[i]);

                fLabels[i] = new Label();
                fLabels[i].Text = GameMain.pTable.pHeader[i];
                fLabels[i].Left = 20;
                fLabels[i].Top = 30 * i + 30;
                fForm.Controls.Add(fLabels[i]);

            }

            fHeader = new Label();
            fHeader.Text = GameMain.pCurrentTable.pText;
            fHeader.Left = 50;
            fHeader.Top = 20;
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

        }

        public void Run()
        {
            fForm.ShowDialog();
        }
    }
}

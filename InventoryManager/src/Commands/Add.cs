using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwinGameSDK;

namespace MyGame
{
    public class Add : Command
    {
        public Add() {}

        public override void Do(int x, int y)
        {
            if (GameMain.pTable != null)
            {
                switch (GameMain.pCurrentTable.pText)
                {
                    case ("Current Stock"):
                        AddItemForm lForm = new AddItemForm();
                        lForm.Run();
                        break;
                    case ("Entire Catalogue"):
                        AddItemForm aForm = new AddItemForm();
                        aForm.Run();
                        break;
                    case ("Stock Orders"):
                        AddOrderForm fForm = new AddOrderForm();
                        fForm.Run();
                        break;
                    case ("Transactions"):
                        AddTransactionsForm dForm = new AddTransactionsForm();
                        dForm.Run();
                        break;
                }
                GameMain.pCurrentTable.OnClick(SwinGame.MousePosition());
            }
        }
    }
}

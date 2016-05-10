using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    class EditItem : Command
    {
        public override void Do(int x, int y)
        {

            if (GameMain.pTable != null)
            {
                if (GameMain.pCurrentTable.pText == "Entire Catalogue")
                {

                    string[] myRow;
                    myRow = GameMain.pTable.GetRow(x, y);
                    SetItemForm myForm = new SetItemForm(myRow);
                    myForm.Run();

                    

                    GameMain.pCurrentTable.OnClick(SwinGame.MousePosition());


                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwinGameSDK;

namespace MyGame
{
    public class DeleteID : Command
    {
        public override void Do(int x, int y)
        {
           // string[] lSelectedID = {"3"};

            DialogResult lTemp = MessageBox.Show("Are you sure you want to delete this record?", "DELETE RECORD", MessageBoxButtons.YesNo);

            if (lTemp == DialogResult.Yes)
            {
                //lSelectedID = GameMain.pTable.GetID(x, y);
                //GameMain.inventoryDB.deleteDatabaseRow(lSelectedID,  GameMain.pTable.pTableName);
                //MessageBox.Show("ID: " + lSelectedID + " successfully deleted.", "Success!", MessageBoxButtons.OK);
                GameMain.pCurrentTable.OnClick(SwinGame.MousePosition());
            }
        }

    }
}

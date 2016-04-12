﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class DeleteID : Command
    {  
        public override void Do(int x, int y)
        {
            int lSelectedID;

            DialogResult lTemp = MessageBox.Show("Are you sure you want to delete this record?", "DELETE RECORD", MessageBoxButtons.YesNo);

            if (lTemp == DialogResult.Yes)
            {
                for (int i = 0; i < GameMain.pTable.pTotalDisplayed; i++)
                {
                    if (y > GameMain.pTable.pY + Table.CELL_HEIGHT * (i + 1) && y < GameMain.pTable.pY + Table.CELL_HEIGHT * (i + 2))
                    {
                        lSelectedID = i + GameMain.pTable.pBuffer + 1;
                        MessageBox.Show("ID " + lSelectedID + " could not be deleted.", "COULD NOT DELETE", MessageBoxButtons.OK);
                    }
                }
            }
        }
    }
}
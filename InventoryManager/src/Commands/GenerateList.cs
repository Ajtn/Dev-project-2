using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    public class GenerateList : Command
    {
        public override void Do(int i, int j)
        {
            /*GameMain.pUIElements.Remove(GameMain.pTable);
            GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, TestData.GenerateData());
            GameMain.pTable.pHeader = new string[] { "ID", "Numbers", "Data" };
            GameMain.pUIElements.Add(GameMain.pTable);*/

            GameMain.pTable = new Table(10, 10, 490, 500, Color.Black, GameMain.inventoryDB.runQuery());
            GameMain.pTable.pHeader = new string[] {"BLAH", "blah", "blah"};
            GameMain.pUIElements.Add(GameMain.pTable);
        }
    }
}

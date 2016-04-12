using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SwinGameSDK;

namespace MyGame
{
    public static class GameMain
    {
        public static DatabaseManager inventoryDB = new DatabaseManager("sql6.freemysqlhosting.net", "sql6114576",
                                                                "sql6114576", "BzSIN9yCih");
        private static List<UIElement> fUIElements;
        private static Table fTable;

        public static Table pTable { get { return fTable; } set { fTable = value; } }
        public static List<UIElement> pUIElements { get { return fUIElements; } set { fUIElements = value; } }

        public static void Main()
        {
            SwinGame.OpenGraphicsWindow("Test", 650, 520);
            SwinGame.LoadFontNamed("courier", "cour.ttf", 14);

            inventoryDB.OpenDBConnection();

            PopulateElements();

            do
            {
                Draw();
                HandleInput();
            } while (!SwinGame.WindowCloseRequested());

        }

        public static void HandleInput()
        {
            do
            {
                SwinGame.ProcessEvents();

                if (SwinGame.MouseClicked(MouseButton.WheelUpButton))
                {
                    if (fTable != null && fTable.pBuffer > 0)
                    {
                        fTable.pBuffer--;
                        return;
                    }
                }

                if (SwinGame.MouseClicked(MouseButton.WheelDownButton))
                {
                    if (fTable != null && fTable.pBuffer < fTable.pData.GetLength(0) - fTable.pTotalDisplayed)
                    {
                        fTable.pBuffer++;
                        return;
                    }
                }

                if (SwinGame.MouseClicked(MouseButton.LeftButton))
                {
                    foreach (ClickableElement e in fUIElements)
                    {
                        if (SwinGame.PointInRect(SwinGame.MousePosition(), e.pClickbox))
                        {
                            e.OnClick(SwinGame.MousePosition());
                            break;
                        }
                    }
                }
            } while (!(SwinGame.MouseClicked(MouseButton.LeftButton)));
        }

        public static void Draw()
        {
            SwinGame.ClearScreen(Color.White);

            foreach (UIElement e in fUIElements)
            {
                if (e != null)
                    e.Draw();
            }

            if (fTable == null)
            {
                SwinGame.FillRectangle(Color.LightBlue, 11, 11, 490, 500);
                SwinGame.DrawRectangle(Color.Black, 10, 10, 490, 500);
            }

            SwinGame.RefreshScreen();
        }

        public static void PopulateElements()
        {
            fUIElements = new List<UIElement>();
            fTable = null;

            fUIElements.Add(new Button(520, 10, 120, 40, Color.DodgerBlue, "Generate List", new GenerateList()));
            fUIElements.Add(new Button(520, 70, 120, 40, Color.DodgerBlue, "Display Sales", new DisplaySaleTable()));
           // fUIElements.Add(fTable); 
            fUIElements.Add(new Button(520, 130, 120, 40, Color.DodgerBlue, "Display Orders", new DisplayOrdersTable()));
            fUIElements.Add(new Button(520, 190, 120, 40, Color.DodgerBlue, "Display Items", new DisplayItemTable()));
            fUIElements.Add(new Button(520, 250, 120, 40, Color.DodgerBlue, "Empty", new Empty()));
            fUIElements.Add(new Button(520, 310, 120, 40, Color.DodgerBlue, "Empty", new Empty()));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SwinGameSDK;
using System.Windows.Forms;

namespace MyGame
{
    public static class GameMain
    {
        public const int TABLE_HEIGHT = 500;
        public const int TABLE_WIDTH = 1050;

        public static DatabaseManager inventoryDB = new DatabaseManager("sql6.freemysqlhosting.net", "sql6114576",
                                                                "sql6114576", "BzSIN9yCih");

        private static Table fTable;
        private static Button fCurrentTable;
        private static List<Button> fTabButtons;
        private static List<Button> fFunctionButtons;

        public static Button pCurrentTable { get { return fCurrentTable; } }
        public static Table pTable { get { return fTable; } set { fTable = value; } }
        public static List<Button> pTabButtons { get { return fTabButtons; } }

		public static bool sucess = false;

        public static void Main()
		{
			Login_Form start = new Login_Form ();
			Application.Run (start);
         

			if (sucess)
			{

				SwinGame.OpenGraphicsWindow ("Peoples Health Pharmacy", 1220, 670);
				SwinGame.ShowSwinGameSplashScreen ();
				SwinGame.LoadFontNamed ("courier", "cour.ttf", 14);

				inventoryDB.openDBConnection ();
				PopulateElements ();

				do
				{
					Draw ();
					HandleInput ();
				} while (!SwinGame.WindowCloseRequested ());
			}

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
                    if (fTable != null && SwinGame.PointInRect(SwinGame.MousePosition(), fTable.pClickbox))
                    {
                        fTable.OnClick(SwinGame.MousePosition());
                        break;
                    }

                    foreach (Button b in fTabButtons)
                    {
                        if (b != null && SwinGame.PointInRect(SwinGame.MousePosition(), b.pClickbox))
                        {
                            b.OnClick(SwinGame.MousePosition());
                            fCurrentTable = b;
                            break;
                        }
                    }

                    foreach (Button b in fFunctionButtons)
                    {
                        if (b != null && SwinGame.PointInRect(SwinGame.MousePosition(), b.pClickbox))
                            b.OnClick(SwinGame.MousePosition());
                    }
                }

            } while (!(SwinGame.MouseClicked(MouseButton.LeftButton)) && !SwinGame.WindowCloseRequested());
        }

        public static void Draw()
        {
            SwinGame.ClearScreen(Color.White);

            foreach (Button b in fTabButtons)
            {
                if (fCurrentTable == b)
                    b.DrawSelected();
                else
                    b.Draw();
            }

            foreach (Button b in fFunctionButtons)
                b.Draw();

            if (fTable == null)
            {
                SwinGame.FillRectangle(Color.LightBlue, 10, 10, GameMain.TABLE_WIDTH, GameMain.TABLE_HEIGHT);
                SwinGame.DrawRectangle(Color.Black, 10, 10, GameMain.TABLE_WIDTH, GameMain.TABLE_HEIGHT);
            }
            else
                fTable.Draw();

            SwinGame.RefreshScreen();
        }

        public static void PopulateElements()
        {
            fTabButtons = new List<Button>();
            fFunctionButtons = new List<Button>();
            fTable = null;

            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 10, 150, 40, Color.DodgerBlue, "Current Stock", new DisplayCurrentStockTable()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 49, 150, 40, Color.DodgerBlue, "Entire Catalogue", new DisplayItemTable()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 88, 150, 40, Color.DodgerBlue, "Stock Orders", new DisplayOrdersTable()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 127, 150, 40, Color.DodgerBlue, "Transactions", new DisplayTransactionsTable()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 166, 150, 40, Color.DodgerBlue, "Empty", new Empty()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 205, 150, 40, Color.DodgerBlue, "Empty", new Empty()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 244, 150, 40, Color.DodgerBlue, "Empty", new Empty()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 283, 150, 40, Color.DodgerBlue, "Empty", new Empty()));
            fTabButtons.Add(new Button(9 + TABLE_WIDTH, 322, 150, 40, Color.DodgerBlue, "Empty", new Empty()));

            fFunctionButtons.Add(new Button(20 + TABLE_WIDTH, 373, 90, 40, Color.Aqua, "Add", new Add()));
        }


        public static void Login(string aUsername, string aPassword)
        {

            Login tryLogin = new Login();
            tryLogin.Try(aUsername,aPassword);

            if (tryLogin.Authenticated)
            {
				sucess = true;
                MessageBox.Show("Login Successful.\nWelcome to People's Health Pharmacy","Welcome",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Application.Exit();

            }
            else
            {
				MessageBox.Show("Invalid username and password.\nPlease try again","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);

                
            }
        }
    }
}

   
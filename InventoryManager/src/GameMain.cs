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

        public static DatabaseManager inventoryDB = new DatabaseManager();

        private static Table fTable;
        private static Button fCurrentTable;
        private static List<Button> fTabButtons;
        private static List<Button> fFunctionButtons;

        public static Button pCurrentTable { get { return fCurrentTable; } }
        public static Table pTable { get { return fTable; } set { fTable = value; } }
        public static List<Button> pTabButtons { get { return fTabButtons; } }

        private static Bitmap _Background;

        private static bool sucess = false;

        public static void Main()
        {
            //Load the default login form
            Login_Form start = new Login_Form();
            Application.Run(start);

            //Swingame file only runs if logged in.
            if (sucess)
            {
                //Start Swingame application
                SwinGame.OpenGraphicsWindow("lLAMA: Peoples Health Pharmacy", 1220, 510);

                //load resources
                SwinGame.LoadFontNamed("courier", "cour.ttf", 14);
                _Background = SwinGame.LoadBitmap(SwinGame.PathToResource("test.bmp", ResourceKind.BitmapResource));

                //Draw loading screen
                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawBitmap(_Background, 0, 0);
                SwinGame.RefreshScreen();

                //open database connection
                inventoryDB.openDBConnection();
                PopulateElements();

                //Show splash screen when database connection is open
                SwinGame.ShowSwinGameSplashScreen();

                do
                {
                    Draw();
                    HandleInput();
                } while (!SwinGame.WindowCloseRequested());
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

            fFunctionButtons.Add(new Button(20 + TABLE_WIDTH, 370, 135, 40, Color.Aqua, "Add", new Add()));
            fFunctionButtons.Add(new Button(20 + TABLE_WIDTH, 420, 135, 40, Color.Aqua, "Export as CSV", new GetReport()));
            fFunctionButtons.Add(new Button(20 + TABLE_WIDTH, 470, 135, 40, Color.Aqua, "Exit", new Exit()));
        }

        public static void Login(string aUsername, string aPassword)
        {
            //try new login
            Login tryLogin = new Login();
            tryLogin.Try(aUsername, aPassword);

            if (tryLogin.Authenticated)
            {
                //display welcome message, quit windows form and open swingame
                sucess = true;
                MessageBox.Show("Login Successful.\nWelcome to People's Health Pharmacy", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Exit();
            }
            else
            {
                //display error message and try again
                MessageBox.Show("Invalid username and password.\nPlease try again", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
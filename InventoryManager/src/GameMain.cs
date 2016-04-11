using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            DatabaseManager inventoryDB = new DatabaseManager("sql6.freemysqlhosting.net", "sql6114576",
                                                               "sql6114576", "BzSIN9yCih");
            if (inventoryDB.OpenDBConnection())
            {
                //Open the game window
                SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
                //SwinGame.ShowSwinGameSplashScreen();
                SwinGame.DrawText(inventoryDB.runQuery("Current Stock", "*"), Color.Black, 25, 25);

                //Run the game loop
                while (false == SwinGame.WindowCloseRequested())
                {
                    //Fetch the next batch of UI interaction
                    SwinGame.ProcessEvents();

                    //Clear the screen and draw the framerate
                    SwinGame.ClearScreen(Color.White);
                    SwinGame.DrawFramerate(0, 0);

                    //Draw onto the screen
                    SwinGame.RefreshScreen(60);
                }
            }
            else { Console.WriteLine("Error Opening DB Connection"); }


        }
    }
}
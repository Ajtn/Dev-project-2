using System;
using System.Collections.Generic;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    static class TestData
    {
        public static string[,] fData = new string[70, 4];

        public static string[,] GenerateData()
        {
            int lCounter = 0;

            for (int j = 0; j < fData.GetLength(0); j++)
            {
                lCounter++;
                fData[j, 0] = lCounter.ToString();
                fData[j, 1] = SwinGame.Rnd(100).ToString();
                fData[j, 2] = SwinGame.Rnd(100).ToString();
            }

            return fData;
        }
    }
}

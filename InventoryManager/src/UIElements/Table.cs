using System;
using System.Collections.Generic;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    public class Table : ClickableElement
    {
        public const int TEXT_BORDER = 9;
        public const int SCROLL_WIDTH = 20;
        public const int CELL_HEIGHT = 25;
        public const int SET_WIDTH = 50;
        public const int DELETE_WIDTH = 70;

        protected string[,] fData;
        protected string[] fHeader;
        protected int fCellWidth;
        protected int fBuffer = 0;
        protected int fTotalDisplayed;
        protected Button[] fSetButtons;
        protected Button[] fScroll;
        protected Button[] fDeleteButtons;
        protected int fListWidth;

        public int pTotalDisplayed { get { return fTotalDisplayed; } }
        public string[,] pData { get { return fData; } }
        public int pBuffer { get { return fBuffer; } set { fBuffer = value; } }
        public string[] pHeader { get { return fHeader; } set { fHeader = value; } }

        public Table(int aX, int aY, int aWidth, int aHeight, Color aColor, string[,] aData)
            : base(aX, aY, aWidth, aHeight, aColor)
        {
            if (aData.GetLength(0) < 19)
                fTotalDisplayed = aData.GetLength(0);
            else
                fTotalDisplayed = 19;
            fData = aData;
            fListWidth = aWidth - SCROLL_WIDTH - SET_WIDTH - DELETE_WIDTH; 
            fCellWidth = fListWidth / aData.GetLength(1);
            fSetButtons = new Button[fTotalDisplayed];
            fDeleteButtons = new Button[fTotalDisplayed];


            for (int i = 0; i < fSetButtons.Length; i++)
            {
                fSetButtons[i] = new Button(aX + fListWidth, aY + CELL_HEIGHT * i + CELL_HEIGHT, SET_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Set", new SetField());
                fDeleteButtons[i] = new Button(aX + fListWidth + SET_WIDTH, aY + CELL_HEIGHT * i + CELL_HEIGHT, DELETE_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Delete", new DeleteID());
            } 

            fScroll = new Button[2];
            fScroll[0] = new Button(aX + fWidth - SCROLL_WIDTH, aY, 20, 20, Color.DodgerBlue, null, new ScrollUp());
            fScroll[1] = new Button(aX + fWidth - SCROLL_WIDTH, aY + aHeight - 20, 20, 20, Color.DodgerBlue, null, new ScrollDown());
        }

        public override void Draw()
        {
            // Draw filler
            SwinGame.FillRectangle(Color.LightCyan, fX, fY, fWidth, fHeight);
            SwinGame.DrawRectangle(Color.Black, fX, fY, fWidth, fHeight);
            
            // Header background and outline
            SwinGame.FillRectangle(Color.DodgerBlue, fX, fY, fListWidth + SET_WIDTH + DELETE_WIDTH, CELL_HEIGHT);
            SwinGame.DrawRectangle(Color.Black, fX, fY, fListWidth + SET_WIDTH + DELETE_WIDTH, CELL_HEIGHT);
            
            // Header text
            for (int i = 0; i < fHeader.Length; i++)
            {
                SwinGame.DrawText(fHeader[i], Color.Black, (float)(fCellWidth * i + TEXT_BORDER + fX), fY + TEXT_BORDER);
            }

            // Table rows and columns
            for (int j = 0 + fBuffer; j < fBuffer + fTotalDisplayed; j++)
            {
                for (int i = 0; i < fData.GetLength(1); i++)
                {
                    if (j % 2 != 0)
                        SwinGame.FillRectangle(Color.PowderBlue, (fCellWidth * i) + fX, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT, fCellWidth, CELL_HEIGHT);
                    else
                        SwinGame.FillRectangle(Color.White, (fCellWidth * i) + fX, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT, fCellWidth, CELL_HEIGHT);
                    SwinGame.DrawRectangle(fColor, (fCellWidth * i) + fX, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT, fCellWidth, CELL_HEIGHT);
                    SwinGame.DrawText(fData[j, i], Color.Black, (float)(fCellWidth * i + TEXT_BORDER + fX), (float)(CELL_HEIGHT * (j - fBuffer) + TEXT_BORDER + fY + CELL_HEIGHT));
                }
            }

            // Set and delete buttons
            for (int i = 0; i < fSetButtons.Length; i++)
            {
                fSetButtons[i].Draw();
                fDeleteButtons[i].Draw();
            }

            // Scroll buttons
            fScroll[0].Draw();
            fScroll[1].Draw();

            // Scroll bar and scaling logic
            float lSize = fHeight - 40;
            float lScale = lSize / (fData.GetLength(0) - fTotalDisplayed);
            float lBarSize = lScale * 5;
            float lIncrementSub = lBarSize / (fData.GetLength(0) - fTotalDisplayed);

            SwinGame.FillRectangle(Color.LightSkyBlue, fX + fWidth - SCROLL_WIDTH, fY + SCROLL_WIDTH, SCROLL_WIDTH, fHeight - SCROLL_WIDTH * 2);
            SwinGame.FillRectangle(Color.DodgerBlue, fX + (fWidth - SCROLL_WIDTH), (int)(fY + SCROLL_WIDTH + (lScale * fBuffer) - (lIncrementSub * fBuffer)), SCROLL_WIDTH, (int)lBarSize);
            SwinGame.DrawRectangle(Color.Black, fX + fWidth - SCROLL_WIDTH, fY, SCROLL_WIDTH, fHeight);
            SwinGame.DrawRectangle(fColor, fX + (fWidth - SCROLL_WIDTH), (int)(fY + SCROLL_WIDTH + (lScale * fBuffer) - (lIncrementSub * fBuffer)), SCROLL_WIDTH, (int)lBarSize);
        }

        public override void OnClick(Point2D aPoint)
        {
            if (SwinGame.PointInRect(aPoint, fScroll[0].pClickbox))
            {
                if (fBuffer > 0)
                    fScroll[0].OnClick(SwinGame.MousePosition());
            }
            
            if (SwinGame.PointInRect(aPoint, fScroll[1].pClickbox))
            {
                if (fBuffer < fData.GetLength(0) - fTotalDisplayed)
                    fScroll[1].OnClick(SwinGame.MousePosition());
            }

            for (int i = 0; i < fSetButtons.Length; i++)
            {
                if (SwinGame.PointInRect(aPoint, fSetButtons[i].pClickbox))
                    fSetButtons[i].OnClick(SwinGame.MousePosition());
                if (SwinGame.PointInRect(aPoint, fDeleteButtons[i].pClickbox))
                    fDeleteButtons[i].OnClick(SwinGame.MousePosition());
            }
        }
    }
}

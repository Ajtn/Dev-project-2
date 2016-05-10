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
        public const int ID_WIDTH = 90;
        public const int SET_WIDTH = 50;
        public const int DELETE_WIDTH = 70;

        protected string[,] fData;
        protected string[] fHeader;
        protected string fTableName;
        protected int fCellWidth;
        protected int fBuffer = 0;
        protected int fTotalDisplayed;
        protected Button[] fSetButtons;
        protected Button[] fDeleteButtons;
        protected Scrollbar fScrollBar;
        protected int fListWidth;

        public Scrollbar pScrollBar { get { return fScrollBar; } }
        public int pTotalDisplayed { get { return fTotalDisplayed; } }
        public string[,] pData { get { return fData; } }
        public int pBuffer { get { return fBuffer; } set { fBuffer = value; } }
        public string[] pHeader { get { return fHeader; } set { fHeader = value; } }
        public string pTableName { get { return fTableName; } set { fTableName = value; } }

        public Table(int aX, int aY, int aWidth, int aHeight, Color aColor, string[,] aData, string aTableName)
            : base(aX, aY, aWidth, aHeight, aColor)
        {
            if (aData.GetLength(0) < 19)
                fTotalDisplayed = aData.GetLength(0);
            else
                fTotalDisplayed = 19;

            fTableName = aTableName;
            fData = aData;
            fListWidth = aWidth - SCROLL_WIDTH - SET_WIDTH - DELETE_WIDTH;
            fCellWidth = (fListWidth - ID_WIDTH) / (aData.GetLength(1) - 1);
            fSetButtons = new Button[fTotalDisplayed];
            fDeleteButtons = new Button[fTotalDisplayed];

            fScrollBar = new Scrollbar(fX + fWidth - SCROLL_WIDTH, fY, SCROLL_WIDTH, fHeight, Color.DarkCyan);

            for (int i = 0; i < fSetButtons.Length; i++)
            {
                if (fTableName == "Item")
                {
                    fSetButtons[i] = new Button(aX + fListWidth, aY + CELL_HEIGHT * i + CELL_HEIGHT, SET_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Set", new EditItem());
                    fDeleteButtons[i] = new Button(aX + fListWidth + SET_WIDTH, aY + CELL_HEIGHT * i + CELL_HEIGHT, DELETE_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Delete", new DeleteItem());
                }
                else
                {
                    fSetButtons[i] = new Button(aX + fListWidth, aY + CELL_HEIGHT * i + CELL_HEIGHT, SET_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Set", new EditItem());
                    fDeleteButtons[i] = new Button(aX + fListWidth + SET_WIDTH, aY + CELL_HEIGHT * i + CELL_HEIGHT, DELETE_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Delete", new DeleteID());
                }
            }
        }

        public override void Draw()
        {
            // draw table outline and background
            SwinGame.FillRectangle(Color.LightCyan, fX, fY, fWidth, fHeight);
            SwinGame.DrawRectangle(Color.Black, fX, fY, fWidth, fHeight);

            // draw header background and outline
            SwinGame.FillRectangle(Color.DodgerBlue, fX, fY, fListWidth + SET_WIDTH + DELETE_WIDTH, CELL_HEIGHT);
            SwinGame.DrawRectangle(Color.Black, fX, fY, fListWidth + SET_WIDTH + DELETE_WIDTH, CELL_HEIGHT);

            // draw header text
            for (int i = 0; i < fHeader.Length; i++)
            {
                if (i == 0)
                    SwinGame.DrawText(fHeader[i], Color.Black, (float)fX + TEXT_BORDER, fY + TEXT_BORDER);
                else
                    SwinGame.DrawText(fHeader[i], Color.Black, (float)fX + fCellWidth * (i - 1) + TEXT_BORDER + ID_WIDTH, fY + TEXT_BORDER);
            }

            DrawData();
            DrawButtons();
            fScrollBar.Draw();
        }

        public void DrawData()
        {
            // print background
            for (int i = 0; i < pTotalDisplayed; i++)
            {
                // print a darker blue if mod 2 is = 0
                if (i % 2 != 0)
                    SwinGame.FillRectangle(Color.PowderBlue, fX, fY + CELL_HEIGHT * (i + 1), fListWidth, CELL_HEIGHT);
                else
                    SwinGame.FillRectangle(Color.White, fX, fY + CELL_HEIGHT * (i + 1), fListWidth, CELL_HEIGHT);
            }

            // loop through array
            for (int j = 0 + fBuffer; j < fBuffer + fTotalDisplayed; j++)
            {
                for (int i = 0; i < fData.GetLength(1); i++)
                {
                    // print text
                    if (i == 0)
                        SwinGame.DrawText(fData[j, 0], Color.Black, fX + TEXT_BORDER, (float)(CELL_HEIGHT * (j - fBuffer) + TEXT_BORDER + fY + CELL_HEIGHT));
                    else
                        SwinGame.DrawText(fData[j, i], Color.Black, (ID_WIDTH + (fCellWidth * (i - 1))) + fX + TEXT_BORDER, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT + TEXT_BORDER);
                }
            }

            for (int j = 0; j < fTotalDisplayed; j++)
            {
                for (int i = 0; i < fData.GetLength(1); i++)
                {
                    // print cell outline
                    if (i == 0)
                        SwinGame.DrawRectangle(Color.Black, fX, CELL_HEIGHT * j + fY + CELL_HEIGHT, ID_WIDTH, CELL_HEIGHT);
                    else
                        SwinGame.DrawRectangle(Color.Black, ID_WIDTH + (fCellWidth * (i - 1)) + fX, CELL_HEIGHT * j + fY + CELL_HEIGHT, fCellWidth, CELL_HEIGHT);
                }
            }
        }

        public void DrawButtons()
        {
            // Loop through set and delete buttons
            for (int i = 0; i < fSetButtons.Length; i++)
            {
                fSetButtons[i].Draw();
                fDeleteButtons[i].Draw();
            }
        }

        public int GetID(int x, int y)
        {
            for (int i = 0; i < GameMain.pTable.pTotalDisplayed; i++)
            {
                if (y > fY + CELL_HEIGHT * (i + 1) && y < fY + CELL_HEIGHT * (i + 2))
                    return Int32.Parse(pData[i + pBuffer, 0]);
            }
            return 0;
        }

        public string[] GetRow(int x, int y)
        {
            string[] lTemp = new string[fData.GetLength(1)];
            for (int i = 0; i < GameMain.pTable.pTotalDisplayed; i++)
            {
                if (y > fY + CELL_HEIGHT * (i + 1) && y < fY + CELL_HEIGHT * (i + 2))
                {
                    for (int j = 0; j < fData.GetLength(1); j++)
                    {
                        lTemp[j] = fData[i + pBuffer, j];
                    }
                }
            }
            return lTemp;
        }


public override void OnClick(Point2D aPoint)
        {
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

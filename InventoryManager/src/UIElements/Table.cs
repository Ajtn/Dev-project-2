﻿using System;
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
        public const int ID_WIDTH = 70;
        public const int SET_WIDTH = 50;
        public const int DELETE_WIDTH = 70;

        protected string[,] fData;
        protected string[] fHeader;
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

        public Table(int aX, int aY, int aWidth, int aHeight, Color aColor, string[,] aData)
            : base(aX, aY, aWidth, aHeight, aColor)
        {
            if (aData.GetLength(0) < 19)
                fTotalDisplayed = aData.GetLength(0);
            else
                fTotalDisplayed = 19;

            fData = aData;
            fListWidth = aWidth - SCROLL_WIDTH - SET_WIDTH - DELETE_WIDTH; 
            fCellWidth = fListWidth - ID_WIDTH / aData.GetLength(1);
            fSetButtons = new Button[fTotalDisplayed];
            fDeleteButtons = new Button[fTotalDisplayed];

            fScrollBar = new Scrollbar(fX + fWidth - SCROLL_WIDTH, fY, SCROLL_WIDTH, fHeight, Color.DarkCyan);

            for (int i = 0; i < fSetButtons.Length; i++)
            {
                fSetButtons[i] = new Button(aX + fListWidth, aY + CELL_HEIGHT * i + CELL_HEIGHT, SET_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Set", new SetField());
                fDeleteButtons[i] = new Button(aX + fListWidth + SET_WIDTH, aY + CELL_HEIGHT * i + CELL_HEIGHT, DELETE_WIDTH, CELL_HEIGHT, Color.PowderBlue, "Delete", new DeleteID());
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
            
            // draw eader text
            for (int i = 0; i < fHeader.Length; i++)
            {
                SwinGame.DrawText(fHeader[i], Color.Black, (float)(fCellWidth * i + TEXT_BORDER + fX), fY + TEXT_BORDER);
            }

            DrawData();
            DrawButtons();
            fScrollBar.Draw();
        }

        public void DrawData()
        {
            // Loop through array
            for (int j = 0 + fBuffer; j < fBuffer + fTotalDisplayed; j++)
            {
                // print a darker blue if mod 2 is = 0
                if (j % 2 != 0)
                    SwinGame.FillRectangle(Color.PowderBlue, fX, fY + CELL_HEIGHT, fListWidth, CELL_HEIGHT);
                else
                    SwinGame.FillRectangle(Color.White, fX, fY + CELL_HEIGHT, fListWidth, CELL_HEIGHT);

                for (int i = 0; i < fData.GetLength(1); i++)
                {

                    // print cell outline
                    if (i == 0)
                        SwinGame.DrawRectangle(fColor, (fCellWidth * i) + fX, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT, ID_WIDTH, CELL_HEIGHT);
                    else
                        SwinGame.DrawRectangle(fColor, (ID_WIDTH + (fCellWidth * (i - 1))) + fX, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT, fCellWidth, CELL_HEIGHT);

                    // print text
                    if (i == 0)
                        SwinGame.DrawText(fData[j, i], Color.Black, fX + TEXT_BORDER, (float)(CELL_HEIGHT * (j - fBuffer) + TEXT_BORDER + fY + CELL_HEIGHT));
                    else
                        SwinGame.DrawRectangle(fColor, (ID_WIDTH + (fCellWidth * (i - 1))) + fX + TEXT_BORDER, CELL_HEIGHT * (j - fBuffer) + fY + CELL_HEIGHT, fCellWidth, CELL_HEIGHT);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    public class Scrollbar : ClickableElement
    {
        protected float fScrollSize;
        protected float fScale;
        protected float fBarSize;
        protected float fIncrementSub;
        protected Button[] fScrollButtons;

        public Scrollbar(int aX, int aY, int aWidth, int aHeight, Color aColor) :
            base (aX, aY, aWidth, aHeight, aColor)
        {
        }

        public void Initialise()
        {
            fScrollSize = fHeight - Table.SCROLL_WIDTH * 2;
            fScale = fScrollSize / (GameMain.pTable.pData.GetLength(0) - GameMain.pTable.pTotalDisplayed);

            if (GameMain.pTable.pData.GetLength(0) - GameMain.pTable.pTotalDisplayed == 1)
                fBarSize = fScrollSize / 2;
            else
                fBarSize = fScale + 30;

            fIncrementSub = fBarSize / (GameMain.pTable.pData.GetLength(0) - GameMain.pTable.pTotalDisplayed);

            fScrollButtons = new Button[2];
            fScrollButtons[0] = new Button(fX, fY, 20, 20, Color.DodgerBlue, null, new ScrollUp());
            fScrollButtons[1] = new Button(fX, fY + fHeight - 20, 20, 20, Color.DodgerBlue, null, new ScrollDown());
        }

        public override void OnClick(Point2D aPoint)
        {
            fScrollButtons[0].OnClick(SwinGame.MousePosition());
            fScrollButtons[1].OnClick(SwinGame.MousePosition());
        }

        public override void Draw()
        {
            // Draw scroll bar
            SwinGame.FillRectangle(Color.LightSkyBlue, fX, fY + Table.SCROLL_WIDTH, Table.SCROLL_WIDTH, (int)fScrollSize);
            SwinGame.DrawRectangle(Color.Black, fX, fY + Table.SCROLL_WIDTH, Table.SCROLL_WIDTH, (int)fScrollSize);

            if (GameMain.pTable.pData.GetLength(0) > GameMain.pTable.pTotalDisplayed)
            {
                SwinGame.FillRectangle(Color.DodgerBlue, fX, (int)(fY + Table.SCROLL_WIDTH + (fScale * GameMain.pTable.pBuffer) - (fIncrementSub * GameMain.pTable.pBuffer)), Table.SCROLL_WIDTH, (int)fBarSize);
                SwinGame.DrawRectangle(Color.Black, fX + fWidth - Table.SCROLL_WIDTH, (int)(fY + Table.SCROLL_WIDTH + (fScale * GameMain.pTable.pBuffer) - (fIncrementSub * GameMain.pTable.pBuffer)), Table.SCROLL_WIDTH, (int)fBarSize);
            }   
            
            // Draw scroll buttons
            fScrollButtons[0].Draw();
            fScrollButtons[1].Draw();
        }
    }
}

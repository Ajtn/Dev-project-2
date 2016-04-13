using System;
using System.Collections.Generic;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    public class Button : ClickableElement
    {
        protected string fText;
        protected Command fCommand;

        public Button(int aX, int aY, int aWidth, int aHeight, Color aColor, string aText, Command aCmd)
            : base(aX, aY, aWidth, aHeight, aColor)
        {
            fText = aText;
            fCommand = aCmd;
        }

        public override void Draw()
        {
            SwinGame.FillRectangle(fColor, fX, fY, fWidth, fHeight);
            SwinGame.DrawRectangle(Color.Black, fX, fY, fWidth, fHeight);
            SwinGame.DrawText(fText, Color.Black, (float)(fX + (fWidth / 2) - (SwinGame.TextWidth(SwinGame.FontNamed("courier"), fText) / 2)),(float)(fY + (fHeight / 2) - (SwinGame.TextHeight(SwinGame.FontNamed("courier"), fText) / 2) + 4));
        }

        public void DrawSelected()
        {
            SwinGame.FillRectangle(Color.Aquamarine, fX, fY, fWidth, fHeight);
            SwinGame.DrawRectangle(Color.Black, fX, fY, fWidth, fHeight);
            SwinGame.DrawText(fText, Color.Black, (float)(fX + (fWidth / 2) - (SwinGame.TextWidth(SwinGame.FontNamed("courier"), fText) / 2)), (float)(fY + (fHeight / 2) - (SwinGame.TextHeight(SwinGame.FontNamed("courier"), fText) / 2) + 4));
        }

        public override void OnClick(Point2D aPoint)
        {
            if (fCommand != null)
                fCommand.Do((int)aPoint.X, (int)aPoint.Y);
        }
    }
}

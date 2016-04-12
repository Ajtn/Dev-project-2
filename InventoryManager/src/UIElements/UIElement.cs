using System;
using System.Collections.Generic;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    public abstract class UIElement
    {
        protected int fX;
        protected int fY;
        protected int fWidth;
        protected int fHeight;
        protected Bitmap fBmp;
        protected Color fColor;

        public int pX { get { return fX; } }
        public int pY { get { return fY; } }
        public int pWidth { get { return fWidth; } }
        public int pHeight { get { return fHeight; } }

        public UIElement(int aX, int aY, int aWidth, int aHeight, Color aColor)
        {
            fX = aX;
            fY = aY;
            fWidth = aWidth;
            fHeight = aHeight;
            fColor = aColor;
        }

        public abstract void Draw();
    }


}

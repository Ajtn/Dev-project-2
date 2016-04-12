using System;
using System.Collections.Generic;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
     public abstract class ClickableElement : UIElement
    {
        protected Rectangle fClickbox;

        public Rectangle pClickbox { get { return fClickbox; } }

        public ClickableElement(int aX, int aY, int aWidth, int aHeight, Color aColor)
            : base(aX, aY, aWidth, aHeight, aColor)
        {
            fClickbox = new Rectangle();
            fClickbox.X = aX;
            fClickbox.Y = aY;
            fClickbox.Width = aWidth;
            fClickbox.Height = aHeight;
        }

        public abstract void OnClick(Point2D aPoint);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGame
{
    public class Add : Command
    {
        public Add() {}

        public override void Do(int x, int y)
        {
            if (GameMain.pTable != null)
            {
                AddForm lForm = new AddForm();
                lForm.Run();
            }
        }
    }
}
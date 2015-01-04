﻿using ForestSim.Flora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Flora
{
    public class Sapling : Tree
    {
        public Sapling(int x, int y, int width, int height)
        {
            this.textureName = "tree1";
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Health = 100;
            this.growthModifier = 1;
        }

    }
}

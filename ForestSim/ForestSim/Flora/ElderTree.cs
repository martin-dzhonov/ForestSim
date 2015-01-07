using ForestSim.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Flora
{
    public class ElderTree : Tree
    {
        public ElderTree(AdultTree adultTree)
        {
            this.healthyTextureName = "elder";
            this.deadTextureName = "elderDead";
            this.Width = Constants.TREESIZE;
            this.Height = Constants.TREESIZE;
            this.X = adultTree.X;
            this.Y = adultTree.Y;
            this.Health = adultTree.Health;
            this.MaxHealth = 400;
            this.spawnRate = 25;
        }

        public ElderTree(int x, int y)
        {
            this.healthyTextureName = "elder";
            this.deadTextureName = "elderDead";
            this.X = x;
            this.Y = y;
            this.Width = Constants.TREESIZE;
            this.Height = Constants.TREESIZE;
            this.Health = 100;
            this.spawnRate = 25;
        }
    }
}

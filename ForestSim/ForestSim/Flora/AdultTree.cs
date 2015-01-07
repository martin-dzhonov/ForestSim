using ForestSim.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Flora
{
    public class AdultTree : Tree
    {

        public AdultTree(Tree sapling)
        {
            this.healthyTextureName = "adult";
            this.deadTextureName = "adultDead";
            this.Width = Constants.TREESIZE;
            this.Height = Constants.TREESIZE;
            this.X = sapling.X;
            this.Y = sapling.Y;
            this.Health = sapling.Health;
            this.MaxHealth = 300;
            this.spawnRate = 15;
        }

        public AdultTree(int x, int y)
        {
            this.healthyTextureName = "adult";
            this.deadTextureName = "adultDead";
            this.Width = Constants.TREESIZE;
            this.Height = Constants.TREESIZE;
            this.X =x;
            this.Y = y;
            this.Health = RandomGenerator.GetRandomInt(0, 200);
            this.MaxHealth = 300;
            this.spawnRate = 15;
        }
    }
}

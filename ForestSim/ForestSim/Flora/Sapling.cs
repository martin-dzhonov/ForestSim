using ForestSim.Flora;
using ForestSim.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Flora
{
    public class Sapling : Tree
    {
        public Sapling(int x, int y)
        {
            this.healthyTextureName = "sapling";
            this.deadTextureName = "saplingDead";
            this.Width = Constants.TREESIZE;
            this.Height = Constants.TREESIZE;
            this.X = x;
            this.Y = y;
            this.Health = RandomGenerator.GetRandomInt(-100, 75);
            this.MaxHealth = RandomGenerator.GetRandomInt(100, 200);
            this.spawnRate = 0;
            this.growthAge = Constants.YEARLENGHT * 3;
        }
    }
}

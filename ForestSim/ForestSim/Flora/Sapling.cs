using ForestSim.Flora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Flora
{
    public class Sapling : Tree
    {
        public Sapling(int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.textureName = "tree1";
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Fauna
{
    public class Lumberjack : Mammal
    {
        private float roamTimer = 1f;
        List<int> directionsRow = new List<int> { -1, -1, -1, 0, 0, 1, 1, 1 };
        List<int> directionsCol = new List<int> { -1, 0, 1, -1, 1, -1, 0, 1 };
        private int randomIndex;
        public Lumberjack(int x, int y, int width, int height)
        {
            this.textureName = "human";
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.roamTimer -= elapsed;
            if (this.roamTimer < 0)
            {
                this.randomIndex = RandomGenerator.GetRandomInt(0, directionsRow.Count - 1);
                this.roamTimer = 1f;             
            }
            this.X += directionsRow[randomIndex];
            this.Y += directionsCol[randomIndex];
        }
    }
}

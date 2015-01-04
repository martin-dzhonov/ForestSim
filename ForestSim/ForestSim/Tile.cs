using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public class Tile
    {
        protected Texture2D texture;

        public Rectangle Rectangle { get; set; }

        public static ContentManager Content { get; set; }
        public Tile(int i, Rectangle rectangle)
        {
            this.texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Rectangle, Color.White);
        }
    }
}

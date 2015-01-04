using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Flora
{
    public abstract class Tree
    {
        protected string textureName;

        protected Texture2D texture;

        public Tree(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Size { get; set; }

        public abstract void Update(GameTime gameTime);

        public void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(textureName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(this.X, this.Y, this.Width, this.Height), Color.White);
        }
    }
}

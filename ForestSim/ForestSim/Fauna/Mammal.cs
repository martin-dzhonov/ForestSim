using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim.Fauna
{
    public abstract class Mammal
    {
        protected Texture2D texture;
    
        protected string textureName;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(textureName);
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Rectangle(this.X, this.Y, this.Width, this.Height), Color.White);
        }
    }
}

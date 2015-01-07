using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public class Map
    {
        private Texture2D basicTileTexture;

        public int Width { get; set; }

        public int Height { get; set; }

        public int TileSize { get; set; }
        public Map(int width, int height, int tileSize)
        {
            this.Width = width;
            this.Height = height;
            this.TileSize = 50;
        }

        public void Load(ContentManager contentManager)
        {
            this.basicTileTexture = contentManager.Load<Texture2D>("Tile5");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int tilesRow = this.Width / this.TileSize;
            int tilesCol = this.Height / this.TileSize;
            for (int i = 0; i < tilesRow; i++)
            {
                for (int j = 0; j < tilesCol; j++)
                {
                    spriteBatch.Draw(basicTileTexture, new Rectangle(j*TileSize, i*TileSize, 50, 50), Color.White);
                }
            }
        }
    }
}

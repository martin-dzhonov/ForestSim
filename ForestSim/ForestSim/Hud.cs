using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public class Hud
    {
        SpriteFont font;
        Weather climate;
        Texture2D background;
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Hud()
        {
            this.Width = 200;
            this.X = (int)WindowSize.Width - this.Width;
            this.Y = 0;
        }

        public void Load(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("HudFont");
            background = contentManager.Load<Texture2D>("BlackRectangle");
        }

        public void Update(Weather climate)
        {
            this.climate = climate;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(this.X, this.Y, this.Width, (int)WindowSize.Height), Color.White);

            spriteBatch.DrawString(font, "Season: ", new Vector2(this.X + 10, 10), Color.Red);
            spriteBatch.DrawString(font, climate.Season.ToString(), new Vector2(this.X + 110, 10), Color.Red);

            spriteBatch.DrawString(font, "Weather: ", new Vector2(this.X + 10, 50), Color.Red);
            spriteBatch.DrawString(font, climate.CurrentWeather.ToString(), new Vector2(this.X + 110, 50), Color.Red);

            spriteBatch.DrawString(font, "Temp: ", new Vector2(this.X + 10, 90), Color.Red);
            spriteBatch.DrawString(font, climate.Tempertature.ToString() + " C", new Vector2(this.X + 110, 90), Color.Red);
        }
    }
}


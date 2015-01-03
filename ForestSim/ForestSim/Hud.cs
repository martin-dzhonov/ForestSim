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
        Climate climate;
      
        public Hud()
        {
        }

        public void Load(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("HudFont");
        }

        public void Update(Climate climate)
        {
            this.climate = climate;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, climate.Tempertature.ToString(), new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(font, climate.Weather.ToString(), new Vector2(10, 50), Color.Red);
            spriteBatch.DrawString(font, climate.Season.ToString(), new Vector2(10, 90), Color.Red);

        }
    }
}


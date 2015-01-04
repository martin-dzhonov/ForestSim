using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public static class Loger
    {
        private static List<string> log;
        private static SpriteFont font;
        private static string singleLog;
        static Loger()
        {
            log = new List<string>();
            singleLog = "";
        }

        public static void Load(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("HudFont");
        }

        public static void Add(string message)
        {
            log.Add(message);
        }

        public static void SingleLog(string mesage)
        {
            singleLog = mesage;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            int x = 1110;
            int y = 140;

            spriteBatch.DrawString(font, singleLog, new Vector2(x, y), Color.Red);

            for (int i = 0; i < log.Count; i++)
			{
                spriteBatch.DrawString(font, log[i], new Vector2(x, y + (i*20)), Color.Red);
			}
        }
    }
}

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

        protected int spawnRate;

        protected double growthRate;

        protected double health;

        private SpriteFont font;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Size { get; set; }

        public double TotalGrowth { get; set; }

        public int Counter { get; set; }
        public Tree(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.health = 50;
        }

        public void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(textureName);
            font = contentManager.Load<SpriteFont>("HudFont");
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(this.X, this.Y, this.Width, this.Height), Color.White);
        }

        public void Subscribe(Weather weather)
        {
            weather.Changed += new EventHandler(this.WeatherChanged);
        }

        private void WeatherChanged(object sender, EventArgs e)
        {
            Weather weather = sender as Weather;
            double weatherWeight = 0.0;
            switch (weather.CurrentWeather)
            {
                case WeatherType.Cloudy:
                    weatherWeight = 0.5;
                    break;
                case WeatherType.Clear:
                    weatherWeight = 0.75;
                    break;
                case WeatherType.Rain:
                    weatherWeight = 1;
                    break;
                case WeatherType.Snow:
                    weatherWeight = -1.5;
                    break;
                case WeatherType.Storm:
                    weatherWeight = -2;
                    break;
                default:
                    break;
            }
            double temperatureWeight = 0.0;
            if (weather.Tempertature < 0)
            {
                temperatureWeight = weather.Tempertature * 0.1;
            }
            if (weather.Tempertature >= 0 && weather.Tempertature < 37)
            {
                temperatureWeight = weather.Tempertature * 0.075;
            }
            else if (weather.Tempertature >= 37)
            {
                temperatureWeight = weather.Tempertature * (-0.05);
            }

            double seasonWeight = 0.0;
            switch (weather.Season)
            {
                case Season.Spring:
                    seasonWeight = 0;
                    break;
                case Season.Summer:
                    seasonWeight = 0;
                    break;
                case Season.Autumn:
                    seasonWeight = -2;
                    break;
                case Season.Winter:
                    seasonWeight = -0.75;
                    break;
                default:
                    break;
            }

            this.Counter++;

            this.growthRate = weatherWeight + temperatureWeight + seasonWeight;
            this.health += growthRate;
            this.TotalGrowth += growthRate;
            double avgGrowth = TotalGrowth / Counter;
            Loger.SingleLog(avgGrowth.ToString());
        }
    }
}

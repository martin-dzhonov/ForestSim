using ForestSim.Utils;
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
        public EventHandler Spawned;

        protected Texture2D currentTexture;

        protected Texture2D healthyTexture;

        protected Texture2D deadTexture;

        protected string healthyTextureName;

        protected string deadTextureName;

        protected int spawnRate;

        protected int growthAge;

        private SpriteFont font;

        private float dayTimer = Constants.DAYLENGHT;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Grow { get; set; }

        public int Age { get; set; }

        public double Size { get; set; }

        public double Health { get; set; }

        public double MaxHealth { get; set; }

        public bool IsDead { get; set; }

        public void Load(ContentManager contentManager)
        {
            this.healthyTexture = contentManager.Load<Texture2D>(healthyTextureName);
            this.deadTexture = contentManager.Load<Texture2D>(deadTextureName);
            this.currentTexture = this.healthyTexture;
            font = contentManager.Load<SpriteFont>("HudFont");
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.dayTimer -= elapsed;

            double growthRate = 0;
            if (this.Health > 100)
            {
                int ageGrowthModifier = (this.Age / 100) + 1;
                growthRate = (Health / 100) / ageGrowthModifier;
           }

            if (this.dayTimer < 0)
            {
                this.dayTimer = Constants.DAYLENGHT;
                this.Age += 1;
                if (this.Age % Constants.MONTHLENGHT == 0 && !this.IsDead)
                {
                    int rnd = RandomGenerator.GetRandomInt(1, 100);
                    if (rnd <= this.spawnRate)
                    {
                        this.OnSpawned(EventArgs.Empty);
                    }
                }
                if (!this.IsDead)
                {
                    this.Size += growthRate * 0.2;
                }
            }

            if (this.Age == this.growthAge)
            {
                this.Grow = true;
            }

            if (this.Health > this.MaxHealth)
            {
                this.Health = this.MaxHealth;
            }

            if (this.Health < -150)
            {
                this.IsDead = true;
                this.currentTexture = deadTexture;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentTexture, new Rectangle(this.X, this.Y, this.Width, this.Height), Color.White);
            // spriteBatch.DrawString(font, ((int)this.Size).ToString(), new Vector2(this.X + 20, this.Y), Color.Red);
            //spriteBatch.DrawString(font, ((int)this.Health).ToString(), new Vector2(this.X + 20, this.Y + 20), Color.Red);
        }

        private void OnSpawned(EventArgs e)
        {
            if (this.Spawned != null)
            {
                Spawned(this, e);
            }
        }

        public void Subscribe(Weather weather)
        {
            weather.Changed += new EventHandler(this.UpdateHealth);
        }

        private void UpdateHealth(object sender, EventArgs e)
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
            if (weather.Tempertature >= 0 && weather.Tempertature < 36)
            {
                temperatureWeight = weather.Tempertature * 0.07;
            }
            else if (weather.Tempertature >= 36)
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
                    seasonWeight = -0.5;
                    break;
                default:
                    break;
            }

            double healthModifier = weatherWeight + temperatureWeight + seasonWeight;
            this.Health += healthModifier;
        }
    }
}

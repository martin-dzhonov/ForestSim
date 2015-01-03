using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public class Climate
    {
        float dayTimer = 0.5f;
        const float DAYTIMER = 0.5f;
        private int daysPassed = 1;
        bool seasonChanged = false;
        public int Tempertature { get; set; }

        public WeatherType Weather { get; set; }

        public Season Season { get; set; }

        public Climate()
        {
            this.Tempertature = 10;
            this.Weather = WeatherType.Cloudy;
            this.Season = Season.Winter;
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            dayTimer -= elapsed;
            if (dayTimer < 0)
            {
                this.daysPassed += 1;
                this.Tempertature += 1;
                this.dayTimer = DAYTIMER;
                seasonChanged = false;
                this.UpdateTemperature();
            }
            if (daysPassed % 30 == 0 && seasonChanged == false)
            {
               // this.AdvanceSeason();
                seasonChanged = true;
            }
        }

        private void UpdateTemperature()
        {
            int seasonWeight = 0;
            int seasonMin = 0;
            int seasonMax = 0;
            switch (this.Season)
            {
                case Season.Spring:
                    seasonWeight = 20;
                    seasonMin = 2;
                    seasonMax = 24;
                    break;
                case Season.Summer:
                    seasonWeight = 20;
                    seasonMin = 11;
                    seasonMax = 40;
                    break;
                case Season.Autumn:
                    seasonWeight = 21;
                    seasonMin = 3;
                    seasonMax = 22;
                    break;
                case Season.Winter:
                    seasonWeight = 23;
                    seasonMin = -23;
                    seasonMax = 5;
                    break;
                default:
                    break;
            }
            this.Tempertature = this.Tempertature + 12 - RandomGenerator.GetRandomInt(1, seasonWeight);
            if (this.Tempertature < seasonMin)
            {
                this.Tempertature = seasonMin;
            }
            if (Tempertature > seasonMax)
            {
                this.Tempertature = seasonMax;
            }
        }

        private void AdvanceSeason()
        {
            switch (this.Season)
            {
                case Season.Spring:
                    this.Season =  Season.Summer;
                    break;
                case Season.Summer:
                    this.Season = Season.Autumn;
                    break;
                case Season.Autumn:
                    this.Season = Season.Winter;
                    break;
                case Season.Winter:
                    this.Season = Season.Spring;
                    break;
            }
        }
    }
}

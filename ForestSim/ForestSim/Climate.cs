using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public class Climate
    {
        float dayTimer = 1f;
        const float DAYTIMER = 1f;
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
                this.UpdateWeather();
            }
            if (daysPassed % 30 == 0 && seasonChanged == false)
            {
                this.AdvanceSeason();
                seasonChanged = true;
            }
        }

        private void UpdateTemperature()
        {
            int seasonWeight = 0;
            int weatherWeight = 0;
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
            switch (this.Weather)
            {
                case WeatherType.Cloudy:
                    weatherWeight = 1;
                    break;
                case WeatherType.Clear:
                    weatherWeight = -1;
                    break;
                case WeatherType.Rain:
                    weatherWeight = 1;
                    break;
                case WeatherType.Snow:
                    weatherWeight = 2;
                    break;
                case WeatherType.Storm:
                    weatherWeight = 0;
                    break;
                default:
                    break;
            }
            this.Tempertature = this.Tempertature + 12 - RandomGenerator.GetRandomInt(1, seasonWeight + weatherWeight);
            if (this.Tempertature < seasonMin)
            {
                this.Tempertature = seasonMin;
            }
            if (Tempertature > seasonMax)
            {
                this.Tempertature = seasonMax;
            }
        }

        private void UpdateWeather()
        {
            double cloudyWeight = 0;
            double clearWeight = 0;
            double rainWeight = 0;
            double snowWeight = 0;
            double stormWeight = 0;
            switch (this.Season)
            {
                case Season.Spring:
                    cloudyWeight = 0.15;
                    clearWeight = 0.30;
                    rainWeight = 0.35;
                    snowWeight = 0.5;
                    stormWeight = 0.15;
                    break;
                case Season.Summer:
                    cloudyWeight = 0.20;
                    clearWeight = 0.5;
                    rainWeight = 0.15;
                    snowWeight = 0.0;
                    stormWeight = 0.15;
                    break;
                case Season.Autumn:
                    cloudyWeight = 0.45;
                    clearWeight = 0.15;
                    rainWeight = 0.30;
                    snowWeight = 0.0;
                    stormWeight = 0.10;
                    break;
                case Season.Winter:
                    cloudyWeight = 0.30;
                    clearWeight = 0.10;
                    rainWeight = 0.10;
                    snowWeight = 0.45;
                    stormWeight = 0.05;
                    break;
                default:
                    break;
            }
            var list = new[]{
                ProportionValue.Create(cloudyWeight, WeatherType.Cloudy),
                ProportionValue.Create(clearWeight, WeatherType.Clear),
                ProportionValue.Create(rainWeight, WeatherType.Rain),
                ProportionValue.Create(snowWeight, WeatherType.Snow),
                ProportionValue.Create(stormWeight, WeatherType.Storm)
            };

            WeatherType weather = (WeatherType)list.ChooseByRandom();

            this.Weather = weather;
        }
        private void AdvanceSeason()
        {
            switch (this.Season)
            {
                case Season.Spring:
                    this.Season = Season.Summer;
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

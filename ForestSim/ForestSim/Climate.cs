using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public class Climate
    {
        float temperatureTimer = 5;
        const float TEMPERATURETIMER = 5;
        public int Tempertature { get; set; }

        public WeatherType Weather { get; set; }

        public Climate()
        {
            this.Tempertature = 20;
            this.Weather = WeatherType.Clear;
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            temperatureTimer -= elapsed;
            if (temperatureTimer < 0)
            {
                this.Tempertature += 1;
                temperatureTimer = TEMPERATURETIMER;
            }
        }
    }
}

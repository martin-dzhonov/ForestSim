using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForestSim
{
    public static class RandomGenerator
    {
        private static Random rnd;
        static RandomGenerator()
        {
            rnd = new Random();
        }

        public static int GetRandomInt(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}

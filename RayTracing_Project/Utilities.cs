using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public static class Utilities
    {
        private static double _infinity = double.MaxValue;

        public static double infinity
        {
            get => _infinity;
        }
        


        public static double clamp(double x, double min, double max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }

        public static double random_double()
        {
            var rand = new Random();
            return rand.NextDouble();
        }
        public static double random_double(double min, double max)
        {
            return min + (max-min)*random_double();
        }

        public static double degrees_to_radians(double degrees)
        {
            return (degrees * Math.PI) / 180;
        }
    }
}

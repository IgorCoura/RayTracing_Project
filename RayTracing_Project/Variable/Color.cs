using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Color : Vec3
    {
        public Color() { }


        public Color(double e0, double e1, double e2) : base(e0, e1, e2)
        {
        }

        public void write_color(StreamWriter sw, int samples_per_pixel)
        {
            var r = vector[0];
            var g = vector[1];
            var b = vector[2];

            var scale = 1.0 / samples_per_pixel;

            r = Math.Sqrt(scale * r);
            g = Math.Sqrt(scale * g);
            b = Math.Sqrt(scale * b);

            sw.WriteLine((int)Math.Ceiling(255 * Utilities.clamp(r, 0.0, 0.999)) + " " + (int)Math.Ceiling(255 * Utilities.clamp(g, 0.0, 0.999)) + " " + (int)Math.Ceiling(255 * Utilities.clamp(b, 0.0, 0.999)));
        }
        public static Color operator *(double t, Color v) => new Color(t * v.vector[0], t * v.vector[1], t * v.vector[2]);
        public static Color operator +(Vec3 u, Color v) => new Color(u.vector[0] + v.vector[0], u.vector[1] + v.vector[1], u.vector[2] + v.vector[2]);
        public static Color operator *(Color u, Vec3 v) => new Color(u.vector[0] * v.vector[0], u.vector[1] * v.vector[1], u.vector[2] * v.vector[2]);
        new public static Color random()
        {
            return new Color(Utilities.random_double(), Utilities.random_double(), Utilities.random_double());
        }
        new public static Color random(double min, double max)
        {
            return new Color(Utilities.random_double(min, max), Utilities.random_double(min, max), Utilities.random_double(min, max));
        }
    }
}

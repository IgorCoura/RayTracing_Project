using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Ray
    {

        public Point3 origin { get; }
        public Vec3 direction { get; }

        public Ray() { }
        public Ray(Point3 origin, Vec3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

       public Point3 at(double t)
        {
            return origin + t * direction;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Point3 : Vec3
    {
        public Point3() { }

        public Point3(double e0, double e1, double e2) : base(e0, e1, e2)
        {
        }

        public static Point3 operator +(Point3 u, Vec3 v) => new Point3(u.vector[0] + v.vector[0], u.vector[1] + v.vector[1], u.vector[2] + v.vector[2]);
        public static Point3 operator -(Point3 u, Vec3 v) => new Point3(u.vector[0] - v.vector[0], u.vector[1] - v.vector[1], u.vector[2] - v.vector[2]);

    }
}

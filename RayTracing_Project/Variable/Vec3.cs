using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Vec3
    {
        internal double[] vector = { 0, 0, 0 };
        public Vec3() { }
        public Vec3(double e0, double e1, double e2)
        {
            this.vector[0] = e0;
            this.vector[1] = e1;
            this.vector[2] = e2;
        }

        public double x() => vector[0];
        public double y() => vector[1];
        public double z() => vector[2];

        public double length_squared() => (vector[0] * vector[0] + vector[1] * vector[1] + vector[2] * vector[2]);
        public double length() => Math.Sqrt(length_squared());
        public static Vec3 operator -(Vec3 v) => new Vec3(-v.x(), -v.y(), -v.z());
        public static Vec3 operator -(Vec3 u, Vec3 v) => new Vec3(u.vector[0] - v.vector[0], u.vector[1] - v.vector[1], u.vector[2] - v.vector[2]);
        public static Vec3 operator +(Vec3 u, Vec3 v) => new Vec3(u.vector[0] + v.vector[0], u.vector[1] + v.vector[1], u.vector[2] + v.vector[2]);
        public static Vec3 operator *(Vec3 u, Vec3 v) => new Vec3(u.vector[0] * v.vector[0], u.vector[1] * v.vector[1], u.vector[2] * v.vector[2]);
        public static Vec3 operator *(double t, Vec3 v) => new Vec3(t * v.vector[0], t * v.vector[1], t * v.vector[2]);
        public static Vec3 operator *(Vec3 v, double t) => t * v;
        public static Vec3 operator /(Vec3 v, double t) => (1 / t) * v;
        public static Vec3 unit_vector(Vec3 v) => v / v.length();
        public static double dot(Vec3 u, Vec3 v) => u.vector[0] * v.vector[0] + u.vector[1] * v.vector[1] + u.vector[2] * v.vector[2];
        public static Vec3 random()
        {
            return new Vec3(Utilities.random_double(), Utilities.random_double(), Utilities.random_double());
        }
        public static Vec3 random(double min, double max)
        {
            return new Vec3(Utilities.random_double(min, max), Utilities.random_double(min, max), Utilities.random_double(min, max));
        }
        public static Vec3 random_in_unit_sphere()
        {
            while (true)
            {
                var p = random(-1, 1);
                if (p.length_squared() >= 1) continue;
                return p;
            }
        }

        public static Vec3 random_unit_vector()
        {
            return unit_vector(random_in_unit_sphere());
        }

        public static Vec3 random_in_hemisphre(Vec3 normal)
        {
            Vec3 in_unit_sphere = random_in_unit_sphere();
            if (dot(in_unit_sphere, normal) > 0.0)
            {
                return in_unit_sphere;
            }
            else
            {
                return -in_unit_sphere;
            }
        }

        public bool near_zero()
        {
            var s = 1e-8;
            return (Math.Abs(vector[0]) < s && Math.Abs(vector[1]) < s && Math.Abs(vector[2]) < s);
        }

        public static Vec3 reflect(Vec3 v, Vec3 n)
        {
            return v - 2 * dot(v, n) * n;
        }

        public static Vec3 refract(Vec3 uv, Vec3 n, double etai_over_etat)
        {
            var cos_theta = Math.Min(dot(-uv, n), 1.0);
            Vec3 r_out_perp = etai_over_etat * (uv + cos_theta * n);
            Vec3 r_out_parallel = -Math.Sqrt(Math.Abs(1.0 - r_out_perp.length_squared())) * n;
            return r_out_perp + r_out_parallel;
        }

        public static Vec3 cross(Vec3 u, Vec3 v)
        {
            return new Vec3(u.vector[1] * v.vector[2] - u.vector[2] * v.vector[1],
                            u.vector[2] * v.vector[0] - u.vector[0] * v.vector[2],
                            u.vector[0] * v.vector[1] - u.vector[1] * v.vector[0]
                            );
        }

        public static Vec3 random_in_unit_disk()
        {
            while (true)
            {
                var p = new Vec3(Utilities.random_double(-1, 1), Utilities.random_double(-1, 1), 0);
                if (p.length_squared() >= 1) continue;
                return p;
            }
        }




    }
}


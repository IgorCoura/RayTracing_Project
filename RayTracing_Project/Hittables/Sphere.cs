using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Sphere : Hittable
    {
        private Point3 center;
        private double radius;
        private Material mat;

        Sphere() { }
        public Sphere(Point3 center, double radius, Material mat)
        {
            this.center = center;
            this.radius = radius;
            this.mat = mat;

        }

        public override bool hit(Ray r, double t_min, double t_max, ref hit_record rec)
        {
            Vec3 oc = r.origin - center;
            var a = r.direction.length_squared();
            var half_b = Vec3.dot(oc, r.direction);
            var c = oc.length_squared() - radius * radius;

            var discriminant = half_b * half_b - a * c;
            if (discriminant < 0) return false;
            var sqrtd = Math.Sqrt(discriminant);

            var root = (-half_b - sqrtd) / a;
            if(root < t_min || t_max < root)
            {
                root = (-half_b + sqrtd) / a;
                if (root < t_min || t_max < root)
                    return false;
            }

            rec.t = root;
            rec.p = r.at(rec.t);
            Vec3 outward_normal = (rec.p - center) / radius;
            rec.set_face_normal(r, outward_normal);
            rec.mat_prt = this.mat;

            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public struct hit_record
    {
        public Point3 p;
        public Vec3 normal;
        public double t;
        public bool front_face;
        public Material mat_prt;

        public void set_face_normal(Ray r, Vec3 outward_normal)
        {
            front_face = Vec3.dot(r.direction, outward_normal) < 0;
            normal = front_face ? outward_normal : -outward_normal;
        }
    }
    public class Hittable
    {
        public virtual bool hit(Ray r, double t_min, double t_max, ref hit_record rec) => false;
    }
}


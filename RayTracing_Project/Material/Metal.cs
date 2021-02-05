using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Metal: Material
    {
        private Color albedo;
        private double fuzz;
        public Metal(Color color, double f)
        {
            this.albedo = color;
            this.fuzz = f;
        }

        public override bool scatter(Ray r_in, hit_record rec, ref Color attenuation, ref Ray scattered)
        {
            Vec3 reflected = Vec3.reflect(Vec3.unit_vector(r_in.direction), rec.normal);
            scattered = new Ray(rec.p, reflected + fuzz*Vec3.random_in_unit_sphere());
            attenuation = this.albedo;
            return (Vec3.dot(scattered.direction, rec.normal) > 0);
        }

    }
}

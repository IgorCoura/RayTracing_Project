using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Lambertian: Material
    {
        private Color albedo;
        public Lambertian(Color albedo)
        {
            this.albedo = albedo;
        }

        public override bool scatter(Ray r_in, hit_record rec, ref Color attenuation, ref Ray scattered)
        {
            var scatter_direction = rec.normal + Vec3.random_unit_vector();
            if (scatter_direction.near_zero())
            {
                scatter_direction = rec.normal;
            }
            scattered = new Ray(rec.p, scatter_direction);
            attenuation = albedo;
            return true;
        }
    }
}

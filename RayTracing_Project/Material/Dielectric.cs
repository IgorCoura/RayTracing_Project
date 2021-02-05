using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Dielectric: Material
    {
        private double ir;

        public Dielectric(double ir)
        {
            this.ir = ir;
        }

        public override bool scatter(Ray r_in, hit_record rec, ref Color attenuation, ref Ray scattered)
        {
            attenuation = new Color(1.0, 1.0, 1.0);
            double refraction_ration = rec.front_face ? (1.0 / ir) : ir;

            Vec3 unit_direction = Vec3.unit_vector(r_in.direction);
            double cos_theta = Math.Min(Vec3.dot(-unit_direction, rec.normal), 1.0);
            double sin_theta = Math.Sqrt(1.0 - cos_theta * cos_theta);

            bool cannot_refract = refraction_ration * sin_theta > 1.0;
            Vec3 direction = new Vec3();
            if (cannot_refract || reflectance(cos_theta, refraction_ration) > Utilities.random_double())
                direction = Vec3.reflect(unit_direction, rec.normal);
            else
                direction = Vec3.refract(unit_direction, rec.normal, refraction_ration);  
            scattered = new Ray(rec.p, direction);
            return true;            
        }

        private static double reflectance(double cosine, double ref_idx)
        {
            var r0 = (1 - ref_idx) / (1 + ref_idx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * Math.Pow((1 - cosine), 5);
        }
    }
}

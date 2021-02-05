using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Material
    {
        public virtual bool scatter(Ray r_in, hit_record rec, ref Color attenuation, ref Ray scattered) => false;

    }
}

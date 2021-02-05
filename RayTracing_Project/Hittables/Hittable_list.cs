using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Hittable_list
    {
        private List<Hittable> hittables = new List<Hittable>();
        public Hittable_list() { }
        public Hittable_list(List<Hittable> obj)
        {
            hittables = obj;
        }

        public void add(Hittable obj)
        {
            hittables.Add(obj);
        }

        void clear() => hittables.Clear();

        public virtual bool hit(Ray r, double t_min, double t_max, ref hit_record rec)
        {
            hit_record temp_rec = new hit_record();
            bool hit_anything = false;
            var closest_so_far = t_max;

            foreach(Hittable hittable in hittables)
            {
                if (hittable.hit(r, t_min, closest_so_far, ref temp_rec))
                {
                    hit_anything = true;
                    closest_so_far = temp_rec.t;
                    rec = temp_rec;
                }
            }
            return hit_anything;
        }

    }
}

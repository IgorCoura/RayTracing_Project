using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingInOneWeekend
{
    public class Camera
    {
        private Point3 origin;
        private Point3 lower_left_corner;
        private Vec3 horizontal;
        private Vec3 vertical;
        private Vec3 u, v, w;
        private double lens_radius;

        public Camera(Point3 lookFrom, Point3 lookat, Vec3 vup, double vfov, double aspect_ratio, double aperture, double focus_dist)
        {
            var theta = Utilities.degrees_to_radians(vfov);
            var h = Math.Tan(theta/2);
            double viewport_height = 2.0 * h;
            double viewport_width = aspect_ratio * viewport_height;

            w = Vec3.unit_vector(lookFrom - lookat);
            u = Vec3.unit_vector(Vec3.cross(vup, w));
            v = Vec3.cross(w, u);

            this.origin = lookFrom;
            this.horizontal = focus_dist * viewport_width * u;
            this.vertical = focus_dist *  viewport_height * v;
            this.lower_left_corner = origin - (horizontal / 2) - (vertical / 2) - focus_dist * w;
            lens_radius = aperture / 2;
        }

        public Ray get_ray(double s, double t)
        {
            Vec3 rd = lens_radius * Vec3.random_in_unit_disk();
            Vec3 offset = u * rd.x() + v * rd.y();
            return new Ray(origin + offset, lower_left_corner+ s*horizontal + t*vertical - origin - offset);
        }

    }
}

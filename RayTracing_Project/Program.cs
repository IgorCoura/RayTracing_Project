using RayTracingInOneWeekend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RayTracing_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            var inicioTempo = DateTime.Now;
            //Image
            const double aspect_ratio = 16.0 / 9.0;
            const int image_width = 400;
            const int image_height = (int)(image_width / aspect_ratio);
            const int samples_per_pixel = 10;
            const int max_depth = 10;

            //world
            var world = random_scene();

            //Camera
            Point3 lookfrom = new Point3(13, 2, 3);
            Point3 lookat = new Point3(0, 0, 0);
            Vec3 vup = new Vec3(0, 1, 0);
            var dist_to_focus = 10.0;
            var aperture = 0.1;
            Camera cam = new Camera(lookfrom, lookat, vup, 20, aspect_ratio, aperture, dist_to_focus);

            //render
            string path = @"D:\AreaDoCodigo\.Codigos\.NET\RayTracing_Project\First.ppm";
            using StreamWriter sw = File.CreateText(path);

            sw.WriteLine("P3");
            sw.WriteLine(image_width + " " + image_height);
            sw.WriteLine("255");

            Color[] allColors = new Color[image_height * image_width];
            List<Task> tasks = new List<Task>();

            int count = 0;
            for (int j = image_height - 1; j >= 0; j--)
            {
                for (int i = 0; i < image_width; i++)
                {
                    int position = count;
                    int _i = i;
                    int _j = j;
                    count++;
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        Color pixel_color = new Color(0, 0, 0);
                        for (int s = 0; s < samples_per_pixel; s++)
                        {
                            double u = (_i + Utilities.random_double()) / (image_width - 1);
                            double v = (_j + Utilities.random_double()) / (image_height - 1);
                            Ray r = cam.get_ray(u, v);
                            pixel_color = pixel_color + ray_color(r, ref world, max_depth);
                        }
                        allColors[position] = pixel_color;
                        Console.WriteLine("Scanlines remaining: " + _j);
                    }));
                }
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Processing image...");
            for (int i = 0; i < count; i++)
            {
                allColors[i].write_color(sw, samples_per_pixel);
            }

            Console.WriteLine("Done");
            Console.WriteLine("Process time: " + (DateTime.Now - inicioTempo));
        }
        public static Color ray_color(Ray r, ref Hittable_list world, int depth)
        {
            hit_record rec = new hit_record();
            if (depth <= 0)
            {
                return new Color(0, 0, 0);
            }
            if (world.hit(r, 0.001, Utilities.infinity, ref rec))
            {
                Ray scattered = new Ray();
                Color attenuation = new Color();
                if (rec.mat_prt.scatter(r, rec, ref attenuation, ref scattered))
                {
                    return attenuation * ray_color(scattered, ref world, depth - 1);
                }
                //Random Ray
                Point3 target = rec.p + rec.normal + Vec3.random_unit_vector();
                return 0.5 * ray_color(new Ray(rec.p, target - rec.p), ref world, depth - 1);
            }
            Vec3 unit_direction = Vec3.unit_vector(r.direction);
            var t = 0.5 * (unit_direction.y() + 1.0);
            return (1.0 - t) * new Color(1.0, 1.0, 1.0) + t * new Color(0.5, 0.7, 1.0);
        }

        public static Hittable_list random_scene()
        {
            Hittable_list world = new Hittable_list();

            var ground_material = new Lambertian(new Color(0.5, 0.5, 0.5));
            world.add(new Sphere(new Point3(0, -1000, 0), 1000, ground_material));

            for (int a = -11; a < 11; a++)
            {
                for (int b = -11; b < 11; b++)
                {
                    var choose_mat = Utilities.random_double();
                    Point3 center = new Point3(a + 0.9 * Utilities.random_double(), 0.2, b + 0.9 * Utilities.random_double());

                    if ((center - new Point3(4, 0.2, 0)).length() > 0.9)
                    {
                        Material sphere_material;

                        if (choose_mat < 0.8)
                        {
                            Color albedo = Color.random() * Color.random();
                            sphere_material = new Lambertian(albedo);
                            world.add(new Sphere(center, 0.2, sphere_material));
                        }
                        else if (choose_mat < 0.95)
                        {
                            Color albedo = Color.random(0.5, 1);
                            var fuzz = Utilities.random_double(0, 0.5);
                            sphere_material = new Metal(albedo, fuzz);
                            world.add(new Sphere(center, 0.2, sphere_material));
                        }
                        else
                        {
                            sphere_material = new Dielectric(1.5);
                            world.add(new Sphere(center, 0.2, sphere_material));
                        }
                    }
                }
            }

            var material1 = new Dielectric(1.5);
            world.add(new Sphere(new Point3(0, 1, 0), 1.0, material1));

            var material2 = new Lambertian(new Color(0.4, 0.2, 0.1));
            world.add(new Sphere(new Point3(-4, 1, 0), 1.0, material2));

            var material3 = new Metal(new Color(0.7, 0.6, 0.5), 0.0);
            world.add(new Sphere(new Point3(4, 1, 0), 1.0, material3));

            return world;
        }
    }
}

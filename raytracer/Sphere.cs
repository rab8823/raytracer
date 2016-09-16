using System;

namespace raytracer
{
    public class Sphere : IRenderObject
    {
        private readonly double RadiusSquared;
        private readonly Vector Center;

        public ISurface Surface { get; set; }

        public Sphere(Vector center, double radius, ISurface surface)
        {
            RadiusSquared = radius * radius;
            Center = center;
            Surface = surface;
        }

        public Vector Normal(Vector position)
        {
            return (position - Center).GetNormal();
        }

        public Intersection? Intersect(Ray ray)
        {
            var eo = Center - ray.Start;
            var v = eo.dot(ray.Direction);
            var dist = 0.0;
            if(v >= 0)
            {
                var disc = RadiusSquared - (eo.dot(eo) - v * v);
                if(disc >= 0)
                {
                    dist = v - Math.Sqrt(disc);
                }
            }
            if(dist == 0)
            {
                return null;
            }
            else
            {
                return new Intersection
                {
                    Distance = dist,
                    Thing = this,
                    Ray = ray
                };
            }
        }
    }
}

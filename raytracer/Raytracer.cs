using System;
using System.Linq;

namespace raytracer
{
    public class Raytracer
    {
        private readonly int _maxDepth;

        public Raytracer(int maxDepth)
        {
            _maxDepth = Math.Max(1, maxDepth);
        }

        public void Render(Scene scene, int height, int width, Action<int, int, FColor> pixelCallback)
        {
            if (scene == null) { throw new ArgumentNullException(nameof(scene)); }
            if (height < 1) { throw new ArgumentOutOfRangeException(nameof(height)); }
            if (width < 1) { throw new ArgumentOutOfRangeException(nameof(width)); }
            if (pixelCallback == null) { throw new ArgumentNullException(nameof(pixelCallback)); }

            var centerX = width / 2;
            var centerY = height / 2;

            Func<int, int, Camera, Vector> getPoint = (x, y, camera) =>
            {
                var recenterX = (x - centerX) / 2.0 / width;
                var recenterY = (y - centerY) / 2.0 / height;
                return (camera.Right.times(recenterX) + camera.Up.times(recenterY) + camera.Forward).GetNormal();
            };

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var r = new Ray(scene.Camera.Position, getPoint(x, y, scene.Camera));
                    pixelCallback(x, y, traceRay(r, scene, 0));
                }
            }
        }

        private FColor traceRay(Ray ray, Scene scene, int depth)
        {
            var intersection = intersections(ray, scene);
            if(intersection == null)
            {
                return scene.BackgroundColor;
            }
            else
            {
                return shade(intersection.Value, scene, depth);
            }
        }

        private FColor shade(Intersection intersection, Scene scene, int depth)
        {
            var dir = intersection.Ray.Direction;
            var pos = dir.times(intersection.Distance) + intersection.Ray.Start;
            var nor = intersection.Thing.Normal(pos);
            var reflectDir = dir - nor.times(nor.dot(dir)).times(2);
            var naturalColor = scene.BackgroundColor.Plus(getNaturalColor(intersection.Thing, pos, nor, reflectDir, scene));
            var reflectColor = (depth >= _maxDepth) ? FColors.Gray : getReflectionColor(intersection.Thing, pos, nor, reflectDir, scene, depth);
            return reflectColor.Plus(naturalColor);
        }

        private FColor getReflectionColor(IRenderObject thing, Vector pos, Vector normal, Vector rd, Scene scene, int depth)
        {
            var r = new Ray(pos, rd);
            var color = traceRay(r, scene, depth + 1);
            return color.Scale(thing.Surface.Reflect(pos));
        }

        private FColor getNaturalColor(IRenderObject thing, Vector pos, Vector norm, Vector rd, Scene scene)
        {
            return scene.Lights
                .Aggregate(scene.BackgroundColor, (color, light) =>
                {
                    var ldis = light.Position - pos;
                    var livec = ldis.GetNormal();
                    var neatIsect = testRay(new Ray(pos, livec), scene);
                    var isInShadow = double.IsNaN(neatIsect) ? false : (neatIsect <= ldis.Magnitude);
                    if (isInShadow)
                    {
                        return color;
                    }
                    else
                    {
                        var illum = livec.dot(norm);
                        var lcolor = (illum > 0) ? light.Color.Scale(illum) : scene.BackgroundColor;
                        var specular = livec.dot(rd.GetNormal());
                        var scolor = (specular > 0) ? light.Color.Scale(Math.Pow(specular, thing.Surface.Roughness)) : scene.BackgroundColor;

                        var diffuseColor = thing.Surface.Diffuse(pos).Times(lcolor);
                        var specularColor = thing.Surface.Specular(pos).Times(scolor);
                        return color.Plus(diffuseColor).Plus(specularColor);
                    }
                });
        }

        private double testRay(Ray ray, Scene scene)
        {
            var isect = intersections(ray, scene);
            if (isect.HasValue)
            {
                return isect.Value.Distance;
            }
            else {
                return double.NaN;
            }
        }

        private Intersection? intersections(Ray r, Scene s)
        {
            var closest = double.PositiveInfinity;
            Intersection? closestIntersection = null;
            foreach (var item in s.Things)
            {
                var intersection = item.Intersect(r);
                if(intersection != null && intersection.Value.Distance < closest)
                {
                    closestIntersection = intersection;
                    closest = intersection.Value.Distance;
                }
            }
            return closestIntersection;
        }
    }
}


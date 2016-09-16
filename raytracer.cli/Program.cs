using System.Collections.Generic;
using System.Diagnostics;
using Utils;

namespace raytracer.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var height = 512;
            var width  = 512;
            var sphere = new Sphere(new Vector(0, 1, -.25), 1, new ShinySurface());
            var plane = new Plane(new Checkerboard(), 1, new Vector(0, 1, 0));
            var camera = new Camera(new Vector(3, 2, 4), new Vector(-1, .5, 0));
            var scene = new Scene
            {
                Things = new List<IRenderObject> { sphere, plane },
                Camera = camera,
                Lights = new List<Light> {
                    new Light(new Vector(-2, 2.5, 0), new FColor(.49, .07, .07, 1)),
                    new Light(new Vector(1.5, 2.5, 1.5), new FColor(.07, .07, .49, 1)),
                    new Light(new Vector(1.5, 2.5, -1.5), new FColor(.07, .49, .071, 1)),
                    new Light(new Vector(1.5, 2.5, 1.5), new FColor(.07, .07, .49, 1))
                },
                BackgroundColor = new FColor(0, 0, 0, 1)
            };
            var rayTracer = new Raytracer(5);
            var writer = new TgaWriter("out.tga", height, width);
            rayTracer.Render(scene, height, width, (x, y, c) =>
            {
                writer.WritePixel(c.To8BitRGBA());
            });
            Process.Start("out.tga");
        }
    }
}

using System.Collections.Generic;

namespace raytracer
{
    public class Scene
    {
        public IList<IRenderObject> Things { get; set; }
        public IList<Light> Lights { get; set; }
        public Camera Camera { get; set; }
        public FColor BackgroundColor { get; set; }
    }
}

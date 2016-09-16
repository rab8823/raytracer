namespace raytracer
{
    public class Plane : IRenderObject
    {
        public ISurface Surface { get; set; }

        private readonly double _offset;
        private readonly Vector _normal;
        private readonly Ray _intersection;

        public Plane(ISurface surface, double offset, Vector normal)
        {
            Surface = surface;
            _normal = normal;
            _offset = offset;
        }

        public Intersection? Intersect(Ray ray)
        {
            var denom = _normal.dot(ray.Direction);
            if(denom > 0)
            {
                return null;
            }
            else
            {
                var dist = (_normal.dot(ray.Start) + _offset) / (-denom);
                return new Intersection
                {
                    Thing = this,
                    Ray = ray,
                    Distance = dist
                };
            }
        }

        public Vector Normal(Vector position)
        {
            return _normal;
        }
    }
}

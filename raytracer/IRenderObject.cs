namespace raytracer
{
    public interface IRenderObject
    {
        ISurface Surface { get; set; }
        Intersection? Intersect(Ray ray);
        Vector Normal(Vector position);
    }
}

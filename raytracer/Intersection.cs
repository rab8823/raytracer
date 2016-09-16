namespace raytracer
{
    public struct Intersection
    {
        public IRenderObject Thing { get; set; }
        public Ray Ray { get; set; }
        public double Distance { get; set; }
    }
}

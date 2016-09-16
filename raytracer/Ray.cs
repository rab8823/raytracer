namespace raytracer
{
    public struct Ray
    {
        public Vector Start { get; }
        public Vector Direction { get; }

        public Ray(Vector start, Vector direction)
        {
            Start = start;
            Direction = direction;
        }
    }
}

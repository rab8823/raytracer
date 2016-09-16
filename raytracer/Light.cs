namespace raytracer
{
    public struct Light
    {
        public Vector Position { get; }
        public FColor Color { get; }

        public Light(Vector position, FColor color)
        {
            Position = position;
            Color = color;
        }
    }
}

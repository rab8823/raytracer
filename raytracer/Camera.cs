namespace raytracer
{
    public class Camera
    {
        private readonly Vector Down = new Vector(0, -1, 0);

        public Vector Position { get; set; }
        public Vector Forward { get; set; }
        public Vector Right { get; set; }
        public Vector Up { get; set; }

        public Camera(Vector position, Vector lookAt)
        {
            Position = position;
            Forward = (lookAt - position).GetNormal();
            Right = Forward.Cross(Down).GetNormal().times(1.5);
            Up = Forward.Cross(Right).GetNormal().times(1.5);
        }
    }
}

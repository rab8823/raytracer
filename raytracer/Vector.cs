using System;

namespace raytracer
{
    public class Vector
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public double Magnitude { get; private set; }

        private Vector _normal = null;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Magnitude = Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector GetNormal()
        {
            if(_normal == null)
            {
                var k = (Magnitude == 0) ? double.PositiveInfinity : 1.0 / Magnitude;
                _normal = times(k);
            }
            return _normal;
        }

        public Vector times(double c)
        {
            return new Vector(X * c, Y * c, Z * c);
        }

        public double dot(Vector other)
        {
            return X * other.X +
                Y * other.Y +
                Z * other.Z;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public Vector Cross(Vector v)
        {
            return new Vector(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        }
    }
}

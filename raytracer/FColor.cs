using System;
using System.Drawing;

namespace raytracer
{
    public struct FColor
    {
        public double R;
        public double G;
        public double B;
        public double A;

        public FColor(double r, double g, double b, double a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color To8BitRGBA()
        {
            return Color.FromArgb(
                (int)Math.Min(255, 255 * A),
                (int)Math.Min(255, 255 * R),
                (int)Math.Min(255, 255 * G),
                (int)Math.Min(255, 255 * B)
                );
        }
    }

    public static class FColors
    {
        public static readonly FColor Black = new FColor(0, 0, 0, 1);
        public static readonly FColor White = new FColor(1, 1, 1, 1);
        public static readonly FColor Gray  = FromColor(Color.Gray);

        public static FColor FromColor(Color c)
        {
            return new FColor(
                c.R / 255,
                c.G / 255,
                c.B / 255,
                c.A / 255
                );
        }


        public static FColor Plus(this FColor c, FColor c2)
        {
            return new FColor(
                c.R + c2.R,
                c.G + c2.G,
                c.B + c2.B,
                c.A + c2.A
            );
        }

        public static FColor Times(this FColor c, FColor c2)
        {
            return new FColor(
                c.R * c2.R,
                c.G * c2.G,
                c.B * c2.B,
                c.A * c2.A
            );
        }

        public static FColor Scale(this FColor c, int k)
        {
            return new FColor(
                c.R * k,
                c.G * k,
                c.B * k,
                c.A * k
            );
        }

        public static FColor Scale(this FColor c, double k)
        {
            return new FColor(
                c.R * k,
                c.G * k,
                c.B * k,
                c.A * k
            );
        }
    }
}

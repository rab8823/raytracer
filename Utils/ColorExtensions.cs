using System;
using System.Drawing;

namespace Utils
{
    public static class ColorExtensions
    {
        public static Color Plus(this Color c, Color c2)
        {
            return Color.FromArgb(
                Math.Min(255, c.A + c2.A),
                Math.Min(255, c.R + c2.R),
                Math.Min(255, c.G + c2.G),
                Math.Min(255, c.B + c2.B)
            );
        }

        public static Color Times(this Color c, Color c2)
        {
            return Color.FromArgb(
                Math.Min(255, c.A * c2.A),
                Math.Min(255, c.R * c2.R),
                Math.Min(255, c.G * c2.G),
                Math.Min(255, c.B * c2.B) 
            );
        }

        public static Color Scale(this Color c, int k)
        {
            return Color.FromArgb(
                Math.Min(255, c.A * k),
                Math.Min(255, c.R * k),
                Math.Min(255, c.G * k),
                Math.Min(255, c.B * k)
            );
        }

        public static Color Scale(this Color c, double k)
        {
            return Color.FromArgb(
                (int)(Math.Floor(c.A * k)),
                (int)(Math.Floor(c.R * k)),
                (int)(Math.Floor(c.G * k)),
                (int)(Math.Floor(c.B * k))
            );
        }


    }
}

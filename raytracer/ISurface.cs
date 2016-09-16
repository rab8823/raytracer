using System;

namespace raytracer
{
    public interface ISurface
    {
        double Roughness { get; set; }

        FColor Diffuse(Vector position);
        FColor Specular(Vector position);
        double Reflect(Vector position);
    }

    public class ShinySurface : ISurface
    {
        public double Roughness { get; set; } = 250;

        public FColor Diffuse(Vector position)
        {
            return FColors.White;
        }

        public double Reflect(Vector position)
        {
            return 0.7;
        }

        public FColor Specular(Vector position)
        {
            return FColors.Gray;
        }
    }

    public class Checkerboard : ISurface
    {
        public double Roughness { get; set; } = 150;

        public FColor Diffuse(Vector position)
        {
            if(Math.Floor(position.Z) + Math.Floor(position.X) % 2 != 0)
            {
                return FColors.White;
            }
            else
            {
                return FColors.Black;
            }
        }

        public double Reflect(Vector position)
        {
            if (Math.Floor(position.Z) + Math.Floor(position.X) % 2 != 0)
            {
                return 0.1;
            }
            else
            {
                return 0.7;
            }
        }

        public FColor Specular(Vector position)
        {
            return FColors.White;
        }
    }
}

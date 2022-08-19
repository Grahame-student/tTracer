using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes
{
    public class Sphere : Shape
    {
        protected override IList<Intersection> LocalIntersects(TRay ray)
        {
            var result = new List<Intersection>();

            // Calculate the discriminant, which will tell us if the ray has hit the object
            TVector sphereToRay = ray.Origin - new TPoint(0, 0, 0);
            Single a = ray.Direction.Dot(ray.Direction);
            Single b = 2 * ray.Direction.Dot(sphereToRay);
            Single c = sphereToRay.Dot(sphereToRay) - 1;

            Single discriminant = b * b - 4 * a * c;
            if (discriminant < 0) return result;

            // Now that we know the ray has hit, determine where the hits occurred
            result.Add(new Intersection((-b - MathF.Sqrt(discriminant)) / (2 * a), this));
            result.Add(new Intersection((-b + MathF.Sqrt(discriminant)) / (2 * a), this));

            return result;
        }

        protected override TVector LocalNormal(TPoint point)
        {
            return point - new TPoint(0, 0, 0);
        }

        public static Sphere Glass()
        {
            return new Sphere
            {
                Material = new Material
                {
                    Transparency = 1.0f,
                    RefractiveIndex = 1.5f
                }
            };
        }
    }
}

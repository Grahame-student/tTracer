using System;
using System.Collections.Generic;

namespace libTracer
{
    public class Sphere : Shape
    {
        public TMatrix Transform { get; set; }

        public Sphere()
        {
            Transform = new TMatrix();
        }

        public IList<Intersection> Intersects(TRay ray)
        {
            var result = new List<Intersection>();

            // Rather than translate objects to world space we translate the ray instead
            TRay transformedRay = ray.Transform(Transform.Inverse());

            // Calculate the discriminant, which will tell us if the ray has hit the object
            TVector sphereToRay = transformedRay.Origin - new TPoint(0, 0, 0);
            Single a = transformedRay.Direction.Dot(transformedRay.Direction);
            Single b = 2 * transformedRay.Direction.Dot(sphereToRay);
            Single c = sphereToRay.Dot(sphereToRay) - 1;

            Single discriminant = b * b - 4 * a * c;
            if (discriminant < 0) return result;

            // Now that we know the ray has hit, determine where the hits occurred
            result.Add(new Intersection((-b - MathF.Sqrt(discriminant)) / (2 * a), this));
            result.Add(new Intersection((-b + MathF.Sqrt(discriminant)) / (2 * a), this));

            // We sort the list to make it easier to determine the order of the hits
            // This may need to move to a better place later to reduce the total number
            // of sorts that occur
            result.Sort();
            return result;
        }
    }
}

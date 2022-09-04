using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using Constants = libTracer.Common.Constants;

namespace libTracer.Shapes
{
    public class Cylinder : Shape
    {
        public Double Minimum { get; set; }
        public Double Maximum { get; set; }
        public Boolean Closed { get; set; }

        public Cylinder()
        {
            Minimum = Double.NegativeInfinity;
            Maximum = Double.PositiveInfinity;
            Closed = false;
        }

        protected override TVector LocalNormal(TPoint point)
        {
            Double dist = point.X * point.X + point.Z * point.Z;
            return dist switch
            {
                < 1 when point.Y >= (Maximum - Constants.EPSILON) => new TVector(0, 1, 0),
                < 1 when point.Y <= (Minimum + Constants.EPSILON) => new TVector(0, -1, 0),
                _ => new TVector(point.X, 0, point.Z)
            };
        }

        protected override IList<Intersection> LocalIntersects(TRay ray)
        {
            var result = new List<Intersection>();

            // Test if ray is parallel to Y axis
            Double a = Math.Pow(ray.Direction.X, 2) + Math.Pow(ray.Direction.Z, 2);
            if (Math.Abs(0 - a) < Constants.EPSILON)
            {
                IntersectsCap(ray, result);
            }
            else
            {
                Double b = 2 * ray.Origin.X * ray.Direction.X +
                           2 * ray.Origin.Z * ray.Direction.Z;
                Double c = Math.Pow(ray.Origin.X, 2) + Math.Pow(ray.Origin.Z, 2) - 1;

                // Test if ray intesects Cylinder
                Double discriminant = b * b - 4 * a * c;
                if (discriminant < 0) return result;

                Double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                Double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

                Double y0 = ray.Origin.Y + t0 * ray.Direction.Y;
                if (Minimum < y0 && y0 < Maximum) result.Add(new Intersection(t0, this));

                Double y1 = ray.Origin.Y + t1 * ray.Direction.Y;
                if (Minimum < y1 && y1 < Maximum) result.Add(new Intersection(t1, this));

                IntersectsCap(ray, result);
            }

            return result;
        }

        private void IntersectsCap(TRay ray, IList<Intersection> intersections)
        {
            if (!Closed || Math.Abs(0 - ray.Direction.Y) < Constants.EPSILON) return;

            Double time = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, time)) intersections.Add(new Intersection(time, this));

            time = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, time)) intersections.Add(new Intersection(time, this));
        }

        private static Boolean CheckCap(TRay ray, Double time)
        {
            Double x = ray.Origin.X + time * ray.Direction.X;
            Double z = ray.Origin.Z + time * ray.Direction.Z;

            return (x * x + z * z) <= 1;
        }
    }
}

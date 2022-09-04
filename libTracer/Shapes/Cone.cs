using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes
{
    public class Cone : Shape
    {
        public Double Minimum { get; set; }
        public Double Maximum { get; set; }
        public Boolean Closed { get; set; }

        public Cone()
        {
            Minimum = Double.NegativeInfinity;
            Maximum = Double.PositiveInfinity;
            Closed = false;
        }

        protected override TVector LocalNormal(TPoint point)
        {
            Double dist = point.X * point.X + point.Z * point.Z;
            if (dist < 1 && point.Y >= (Maximum - Constants.EPSILON)) return new TVector(0, 1, 0);
            if (dist < 1 && point.Y <= (Minimum + Constants.EPSILON)) return new TVector(0, -1, 0);
            Double y = Math.Sqrt(dist);
            if (point.Y > 0) y = -y;
            return new TVector(point.X, y, point.Z);
        }

        protected override IList<Intersection> LocalIntersects(TRay ray)
        {
            var result = new List<Intersection>();

            Double a = Math.Pow(ray.Direction.X, 2) - Math.Pow(ray.Direction.Y, 2) + Math.Pow(ray.Direction.Z, 2);
            Double b = 2 * ray.Origin.X * ray.Direction.X -
                       2 * ray.Origin.Y * ray.Direction.Y +
                       2 * ray.Origin.Z * ray.Direction.Z;
            Double c = Math.Pow(ray.Origin.X, 2) - Math.Pow(ray.Origin.Y, 2) + Math.Pow(ray.Origin.Z, 2);

            if (Math.Abs(a) < Constants.EPSILON)
            {
                if (!(Math.Abs(b) > Constants.EPSILON)) return result;
                IntersectsCap(ray, result);
                // Ray parallel to half of cone
                Double t = -c / (2 * b);
                result.Add(new Intersection(t, this));
            }
            else
            {
                // Test if ray intersects Cone
                Double discriminant = b * b - 4 * a * c;
                if (discriminant < 0) return result;

                Double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                Double y0 = ray.Origin.Y + t0 * ray.Direction.Y;
                if (Minimum < y0 && y0 < Maximum) result.Add(new Intersection(t0, this));

                Double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                Double y1 = ray.Origin.Y + t1 * ray.Direction.Y;
                if (Minimum < y1 && y1 < Maximum) result.Add(new Intersection(t1, this));

                IntersectsCap(ray, result);
            }

            return result;
        }

        private void IntersectsCap(TRay ray, ICollection<Intersection> intersections)
        {
            if (!Closed || Math.Abs(ray.Direction.Y) < Constants.EPSILON) return;

            Double time = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, time, Minimum)) intersections.Add(new Intersection(time, this));

            time = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, time, Maximum)) intersections.Add(new Intersection(time, this));
        }

        private static Boolean CheckCap(TRay ray, Double time, Double y)
        {
            Double x = ray.Origin.X + time * ray.Direction.X;
            Double z = ray.Origin.Z + time * ray.Direction.Z;

            return (x * x + z * z) <= Math.Abs(y);
        }
    }
}

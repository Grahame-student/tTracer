using System;
using System.Collections.Generic;
using System.Linq;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes
{
    public class Cube : Shape
    {
        protected override TVector LocalNormal(TPoint point)
        {
            Double maxc = new[] { Math.Abs(point.X), Math.Abs(point.Y), Math.Abs(point.Z) }.Max();

            if (Math.Abs(maxc - Math.Abs(point.X)) < Constants.EPSILON)
            {
                return new TVector(point.X, 0, 0);
            }

            if (Math.Abs(maxc - Math.Abs(point.Y)) < Constants.EPSILON)
            {
                return new TVector(0, point.Y, 0);
            }

            return new TVector(0, 0, point.Z);
        }

        protected override IList<Intersection> LocalIntersects(TRay ray)
        {
            (Double tmin, Double tmax) xt = CheckAxis(ray.Origin.X, ray.Direction.X);
            (Double tmin, Double tmax) yt = CheckAxis(ray.Origin.Y, ray.Direction.Y);
            (Double tmin, Double tmax) zt = CheckAxis(ray.Origin.Z, ray.Direction.Z);

            Double tmin = new[] { xt.tmin, yt.tmin, zt.tmin }.Max();
            Double tmax = new[] { xt.tmax, yt.tmax, zt.tmax }.Min();

            var result = new List<Intersection>();
            if (tmin > tmax)
            {
                return result;
            }

            result.Add(new Intersection(tmin, this));
            result.Add(new Intersection(tmax, this));
            return result;
        }

        private static (Double tmin, Double tmax) CheckAxis(Double origin, Double direction)
        {
            Double tminNumerator = (-1 - origin);
            Double tmaxNumerator = (1 - origin);

            Double tmin;
            Double tmax;
            if (Math.Abs(direction) >= Constants.EPSILON)
            {
                tmin = tminNumerator / direction;
                tmax = tmaxNumerator / direction;
            }
            else
            {
                tmin = tminNumerator * Double.PositiveInfinity;
                tmax = tmaxNumerator * Double.PositiveInfinity;
            }

            if (tmin > tmax)
            {
                (tmax, tmin) = (tmin, tmax);
            }

            return (tmin, tmax);
        }
    }
}

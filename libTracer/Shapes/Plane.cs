using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes
{
    public class Plane : Shape
    {
        protected override TVector LocalNormal(TPoint point)
        {
            return new TVector(0, 1, 0);
        }

        protected override IList<Intersection> LocalIntersects(TRay ray)
        {
            var result = new List<Intersection>();
            if (MathF.Abs(ray.Direction.Y) < EPSILON) return result;
            Single t = -ray.Origin.Y / ray.Direction.Y;
            result.Add(new Intersection(t, this));

            return result;
        }
    }
}

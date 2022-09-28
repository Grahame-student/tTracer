using System;
using System.Collections.Generic;
using System.Linq;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes;

public class Cube : Shape
{
    protected override Bounds LocalBounds()
    {
        return new Bounds(new TPoint(-1, -1, -1), new TPoint(1, 1, 1));
    }

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
        // TODO: Duplicate of code in Bounds class
        (Double tmin, Double tmax) xt = Bounds.CheckAxis(ray.Origin.X, ray.Direction.X, -1, 1);
        (Double tmin, Double tmax) yt = Bounds.CheckAxis(ray.Origin.Y, ray.Direction.Y, -1, 1);
        (Double tmin, Double tmax) zt = Bounds.CheckAxis(ray.Origin.Z, ray.Direction.Z, -1, 1);

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
}

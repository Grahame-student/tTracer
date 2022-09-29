using System;
using System.Collections.Generic;
using System.Linq;

using libTracer.Common;

namespace libTracer.Shapes;

public class Bounds
{
    public TPoint Minimum { get; private set; }
    public TPoint Maximum { get; private set; }

    public Bounds() : this(
        new TPoint(Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity),
        new TPoint(Double.NegativeInfinity, Double.NegativeInfinity, Double.NegativeInfinity))
    {
    }

    public Bounds(TPoint minimum, TPoint maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    public void Merge(IList<TPoint> corners)
    {
        Double minX = Minimum.X;
        Double minY = Minimum.Y;
        Double minZ = Minimum.Z;
        Double maxX = Maximum.X;
        Double maxY = Maximum.Y;
        Double maxZ = Maximum.Z;

        foreach (TPoint point in corners)
        {
            if (point.X <= minX) minX = point.X;
            if (point.X >= maxX) maxX = point.X;
            if (point.Y <= minY) minY = point.Y;
            if (point.Y >= maxY) maxY = point.Y;
            if (point.Z <= minZ) minZ = point.Z;
            if (point.Z >= maxZ) maxZ = point.Z;
        }

        Minimum = new TPoint(minX, minY, minZ);
        Maximum = new TPoint(maxX, maxY, maxZ);
    }

    public Boolean Intersected(TRay ray)
    {
        // TODO: Duplicate of code in Cube class
        (Double tmin, Double tmax) xt = CheckAxis(ray.Origin.X, ray.Direction.X, Minimum.X, Maximum.X);
        (Double tmin, Double tmax) yt = CheckAxis(ray.Origin.Y, ray.Direction.Y, Minimum.Y, Maximum.Y);
        (Double tmin, Double tmax) zt = CheckAxis(ray.Origin.Z, ray.Direction.Z, Minimum.Z, Maximum.Z);

        Double tmin = new[] { xt.tmin, yt.tmin, zt.tmin }.Max();
        Double tmax = new[] { xt.tmax, yt.tmax, zt.tmax }.Min();

        return tmin < tmax;
    }

    public static (Double tmin, Double tmax) CheckAxis(Double origin, Double direction, Double min, Double max)
    {
        Double tminNumerator = (min - origin);
        Double tmaxNumerator = (max - origin);

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

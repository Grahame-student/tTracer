using System;
using System.Collections.Generic;
using System.Linq;

using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes;

public class Group : Shape
{
    private Bounds _localBounds;

    public IList<Shape> Shapes { get; }

    public Group()
    {
        _localBounds = new Bounds(
            new TPoint(Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity),
            new TPoint(Double.NegativeInfinity, Double.NegativeInfinity, Double.NegativeInfinity));
        Shapes = new List<Shape>();
    }

    protected override Bounds LocalBounds()
    {
        return _localBounds;
    }

    protected override TVector LocalNormal(TPoint point)
    {
        // Groups have no normals, the contained shapes do
        // If this method is called, that's a defect that needs to be resolved
        throw new System.NotImplementedException();
    }

    protected override IList<Intersection> LocalIntersects(TRay ray)
    {
        var result = new List<Intersection>();
        if (!BoundsIntersected(ray)) return result;

        foreach (Shape shape in Shapes)
        {
            result.AddRange(shape.Intersects(ray));
        }

        result.Sort();

        return result;
    }

    private Boolean BoundsIntersected(TRay ray)
    {
        (Double tmin, Double tmax) xt = CheckAxis(ray.Origin.X, ray.Direction.X, _localBounds.Minimum.X, _localBounds.Maximum.X);
        (Double tmin, Double tmax) yt = CheckAxis(ray.Origin.Y, ray.Direction.Y, _localBounds.Minimum.Y, _localBounds.Maximum.Z);
        (Double tmin, Double tmax) zt = CheckAxis(ray.Origin.Z, ray.Direction.Z, _localBounds.Minimum.Z, _localBounds.Maximum.Z);

        Double tmin = new[] { xt.tmin, yt.tmin, zt.tmin }.Max();
        Double tmax = new[] { xt.tmax, yt.tmax, zt.tmax }.Min();

        return tmin < tmax;
    }

    private static (Double tmin, Double tmax) CheckAxis(Double origin, Double direction, Double min, Double max)
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

    public void Add(Shape shape)
    {
        Shapes.Add(shape);
        shape.Parent = this;
        SetBounds(shape);
    }

    private void SetBounds(Shape shape)
    {
        // 8 corners bound the shape
        // min x, min y, min z
        // min x, min y, max z
        // min x, max y, min z
        // min x, max y, max z
        // max x, min y, min z
        // max x, min y, max z
        // max x, max y, min z
        // max x, max y, max z

        // Convert shape to group space
        // corner * shape.transformation

        var corners = new List<TPoint>
        {
            GroupPoint(shape, new TPoint(_localBounds.Minimum.X, _localBounds.Minimum.Y, _localBounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Minimum.X, _localBounds.Minimum.Y, _localBounds.Maximum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Minimum.X, _localBounds.Maximum.Y, _localBounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Minimum.X, _localBounds.Maximum.Y, _localBounds.Maximum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Maximum.X, _localBounds.Minimum.Y, _localBounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Maximum.X, _localBounds.Minimum.Y, _localBounds.Maximum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Maximum.X, _localBounds.Maximum.Y, _localBounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(_localBounds.Maximum.X, _localBounds.Maximum.Y, _localBounds.Maximum.Z))
        };

        _localBounds = UpdatedBounds(corners);
    }

    private static TPoint GroupPoint(Shape shape, TPoint point)
    {
        return shape.Transform * point;
    }

    private Bounds UpdatedBounds(IEnumerable<TPoint> corners)
    {
        Double minX = _localBounds.Minimum.X;
        Double minY = _localBounds.Minimum.Y;
        Double minZ = _localBounds.Minimum.Z;
        Double maxX = _localBounds.Maximum.X;
        Double maxY = _localBounds.Maximum.Y;
        Double maxZ = _localBounds.Maximum.Z;

        foreach (TPoint point in corners)
        {
            if (point.X <= minX) minX = point.X;
            if (point.X >= maxX) maxX = point.X;
            if (point.Y <= minY) minY = point.Y;
            if (point.Y >= maxY) maxY = point.Y;
            if (point.Z <= minZ) minZ = point.Z;
            if (point.Z >= maxZ) maxZ = point.Z;
        }

        return new Bounds(
            new TPoint(minX, minY, minZ),
            new TPoint(maxX, maxY, maxZ));
    }
}

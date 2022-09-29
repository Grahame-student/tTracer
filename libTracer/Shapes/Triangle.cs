using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes;

public class Triangle : Shape
{
    public IList<TPoint> Points;
    public TPoint Point1 => Points[0];
    public TPoint Point2 => Points[1];
    public TPoint Point3 => Points[2];

    public TVector Edge1 { get; }
    public TVector Edge2 { get; }

    public Triangle(TPoint point1, TPoint point2, TPoint point3)
    {
        Points = new List<TPoint>
        {
            point1,
            point2,
            point3
        };

        Edge1 = point2 - point1;
        Edge2 = point3 - point1;
        Bounds.Merge(Points);
    }

    protected override Bounds LocalBounds()
    {
        return new Bounds();
    }

    protected override TVector LocalNormal(TPoint point)
    {
        return Edge2.Cross(Edge1).Normalise();
    }

    protected override IList<Intersection> LocalIntersects(TRay ray)
    {
        var result = new List<Intersection>();
        TVector dirCrossE2 = ray.Direction.Cross(Edge2);
        Double determinant = Edge1.Dot(dirCrossE2);
        if (Math.Abs(determinant) < Constants.EPSILON)
        {
            return result;
        }

        Double f = 1 / determinant;
        TVector p1Origin = ray.Origin - Point1;
        Double u = f * p1Origin.Dot(dirCrossE2);
        if (u is < 0 or > 1)
        {
            return result;
        }

        TVector originCrossE1 = p1Origin.Cross(Edge1);
        Double v = f * ray.Direction.Dot(originCrossE1);
        if (v < 0 || u + v > 1)
        {
            return result;
        }

        Double t = f * Edge2.Dot(originCrossE1);
        result.Add(new Intersection(t, this));
        return result;
    }
}

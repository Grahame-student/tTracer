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
        // TODO: Investigate where the problems with bounding boxes are
        if (!_localBounds.Intersected(ray)) return result;

        foreach (Shape shape in Shapes)
        {
            result.AddRange(shape.Intersects(ray));
        }

        result.Sort();

        return result;
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
            GroupPoint(shape, new TPoint(shape.Bounds.Minimum.X, shape.Bounds.Minimum.Y, shape.Bounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Minimum.X, shape.Bounds.Minimum.Y, shape.Bounds.Maximum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Minimum.X, shape.Bounds.Maximum.Y, shape.Bounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Minimum.X, shape.Bounds.Maximum.Y, shape.Bounds.Maximum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Maximum.X, shape.Bounds.Minimum.Y, shape.Bounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Maximum.X, shape.Bounds.Minimum.Y, shape.Bounds.Maximum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Maximum.X, shape.Bounds.Maximum.Y, shape.Bounds.Minimum.Z)),
            GroupPoint(shape, new TPoint(shape.Bounds.Maximum.X, shape.Bounds.Maximum.Y, shape.Bounds.Maximum.Z)),
        };

        _localBounds.Merge(corners);
    }

    private static TPoint GroupPoint(Shape shape, TPoint point)
    {
        return shape.Transform * point;
    }
}

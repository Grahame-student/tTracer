using libTracer.Common;
using libTracer.Shapes;

using System;

namespace libTracer.Scene.Patterns;

public abstract class Pattern
{
    private readonly Lazy<TMatrix> _inverse;

    public TMatrix Transform { get; set; }
    public TMatrix Inverse => _inverse.Value;

    protected Pattern()
    {
        Transform = new TMatrix();
        _inverse = new Lazy<TMatrix>(() => Transform.Inverse());
    }

    public TColour ColourAt(Shape shape, TPoint worldPoint)
    {
        TPoint objectPoint = shape.WorldToObject(worldPoint);
        TPoint patternpoint = Inverse * objectPoint;
        return LocalColourAt(shape, patternpoint);
    }

    protected abstract TColour LocalColourAt(Shape shape, TPoint point);
}
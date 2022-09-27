using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes;

public class Group : Shape
{
    public IList<Shape> Shapes { get; }

    public Group()
    {
        Shapes = new List<Shape>();
    }

    protected override TVector LocalNormal(TPoint point)
    {
        throw new System.NotImplementedException();
    }

    protected override IList<Intersection> LocalIntersects(TRay ray)
    {
        var result = new List<Intersection>();
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
    }
}
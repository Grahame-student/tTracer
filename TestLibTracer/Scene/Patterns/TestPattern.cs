using libTracer.Common;
using libTracer.Scene.Patterns;
using libTracer.Shapes;

namespace TestLibTracer.Scene.Patterns;

internal class TestPattern : Pattern
{
    protected override TColour LocalColourAt(Shape shape, TPoint point)
    {
        return new TColour(point.X, point.Y, point.Z);
    }
}
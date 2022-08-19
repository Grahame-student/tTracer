using System;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene.Patterns
{
    public class Gradient : Pattern
    {
        public Pattern A { get; }
        public Pattern B { get; }

        public Gradient(TColour colour1, TColour colour2)
        {
            A = new Solid(colour1);
            B = new Solid(colour2);
        }

        protected override TColour LocalColourAt(Shape shape, TPoint point)
        {
            TColour distance = B.ColourAt(shape, point) - A.ColourAt(shape, point);
            Double fraction = point.X - Math.Floor(point.X);
            return A.ColourAt(shape, point) + distance * fraction;
        }
    }
}

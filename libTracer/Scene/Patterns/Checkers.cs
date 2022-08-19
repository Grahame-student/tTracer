using System;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene.Patterns
{
    public class Checkers : Pattern
    {
        public Pattern A { get; set; }
        public Pattern B { get; set; }

        public Checkers(TColour colour1, TColour colour2)
        {
            A = new Solid(colour1);
            B = new Solid(colour2);
        }

        protected override TColour LocalColourAt(Shape shape, TPoint point)
        {
            Single position = MathF.Floor(point.X) + MathF.Floor(point.Y) + MathF.Floor(point.Z);
            return position % 2 == 0 ? A.ColourAt(shape, point) : B.ColourAt(shape, point);
        }
    }
}

using System;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene.Patterns
{
    public class Rings : Pattern
    {
        public Pattern A { get; set; }
        public Pattern B { get; set; }

        public Rings(TColour colour1, TColour colour2)
        {
            A = new Solid(colour1);
            B = new Solid(colour2);
        }

        protected override TColour LocalColourAt(Shape shape, TPoint point)
        {
            Single position = MathF.Sqrt((point.X * point.X) + (point.Z * point.Z));
            return MathF.Floor(position) % 2 == 0 ? A.ColourAt(shape, point) : B.ColourAt(shape, point);
        }
    }
}

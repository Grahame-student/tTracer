using System;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene.Patterns
{
    public class Stripes : Pattern
    {
        public Pattern A { get; }
        public Pattern B { get; }

        public Stripes(TColour colour1, TColour colour2)
        {
            A = new Solid(colour1);
            B = new Solid(colour2);
        }

        public Stripes(Pattern pattern1, Pattern pattern2)
        {
            A = pattern1;
            B = pattern2;
        }

        protected override TColour LocalColourAt(Shape shape, TPoint point)
        {
            return (Int32)Math.Floor(point.X) % 2 == 0 ? A.ColourAt(shape, point) : B.ColourAt(shape, point);
        }
    }
}

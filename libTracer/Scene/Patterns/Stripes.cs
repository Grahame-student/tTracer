using System;
using libTracer.Common;

namespace libTracer.Scene.Patterns
{
    public class Stripes : Pattern
    {
        public TColour A { get; }
        public TColour B { get; }

        public Stripes(TColour colour1, TColour colour2)
        {
            A = colour1;
            B = colour2;
        }

        protected override TColour LocalColourAt(TPoint point)
        {
            return (Int32)Math.Floor(point.X) % 2 == 0 ? A : B;
        }
    }
}

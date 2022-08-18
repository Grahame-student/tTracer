using System;
using libTracer.Common;
using libTracer.Scene.Patterns;

namespace libTracer.Scene.Patterns
{
    public class Gradient : Pattern
    {
        public TColour A { get; }
        public TColour B { get; }

        public Gradient(TColour colour1, TColour colour2)
        {
            A = colour1;
            B = colour2;
        }

        protected override TColour LocalColourAt(TPoint point)
        {
            TColour distance = B - A;
            Single fraction = point.X - MathF.Floor(point.X);
            return A + distance * fraction;
        }
    }
}

using System;
using libTracer.Common;

namespace libTracer.Scene.Patterns
{
    public class Checkers : Pattern
    {
        public TColour A { get; set; }
        public TColour B { get; set; }

        public Checkers(TColour colour1, TColour colour2)
        {
            A = colour1;
            B = colour2;
        }

        protected override TColour LocalColourAt(TPoint point)
        {
            Single position = MathF.Floor(point.X) + MathF.Floor(point.Y) + MathF.Floor(point.Z);
            return position % 2 == 0 ? A : B;
        }
    }
}

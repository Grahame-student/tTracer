using System;
using libTracer.Common;

namespace libTracer.Scene.Patterns
{
    public class Rings : Pattern
    {
        public TColour A { get; set; }
        public TColour B { get; set; }

        public Rings(TColour colour1, TColour colour2)
        {
            A = colour1;
            B = colour2;
        }

        protected override TColour LocalColourAt(TPoint point)
        {
            Single position = MathF.Sqrt((point.X * point.X) + (point.Z * point.Z));
            return MathF.Floor(position) % 2 == 0 ? A : B;
        }
    }
}

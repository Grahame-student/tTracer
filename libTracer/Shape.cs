using System;

namespace libTracer
{
    public abstract class Shape
    {
        public TMatrix Transform { get; set; }
        public Material Material { get; set; }

        public abstract TVector Normal(TPoint point);
    }
}

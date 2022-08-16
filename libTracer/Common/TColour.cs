using System;

namespace libTracer.Common
{
    public class TColour : IEquatable<TColour>
    {
        private const float EPSILON = 0.001f;

        public float Red { get; }
        public float Green { get; }
        public float Blue { get; }

        public TColour(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static TColour operator +(TColour c1, TColour c2) => new(
            c1.Red + c2.Red,
            c1.Green + c2.Green,
            c1.Blue + c2.Blue);

        public static TColour operator -(TColour c1, TColour c2) => new(
            c1.Red - c2.Red,
            c1.Green - c2.Green,
            c1.Blue - c2.Blue);

        public static TColour operator *(TColour c, float s) => new(
            c.Red * s,
            c.Green * s,
            c.Blue * s);

        public static TColour operator *(TColour c1, TColour c2) => new(
            c1.Red * c2.Red,
            c1.Green * c2.Green,
            c1.Blue * c2.Blue);

        public bool Equals(TColour other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MathF.Abs(Red - other.Red) < EPSILON &&
                   MathF.Abs(Green - other.Green) < EPSILON &&
                   MathF.Abs(Blue - other.Blue) < EPSILON;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((TColour)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue);
        }
    }
}

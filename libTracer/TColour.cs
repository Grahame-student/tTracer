using System;

namespace libTracer
{
    public class TColour : IEquatable<TColour>
    {
        private const Single EPSILON = 0.001f;

        public Single Red { get; }
        public Single Green { get; }
        public Single Blue { get; }

        public TColour(Single red, Single green, Single blue)
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

        public static TColour operator *(TColour c, Single s) => new(
            c.Red * s,
            c.Green * s,
            c.Blue * s);

        public static TColour operator *(TColour c1, TColour c2) => new(
            c1.Red * c2.Red,
            c1.Green * c2.Green,
            c1.Blue * c2.Blue);

        public Boolean Equals(TColour other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MathF.Abs(Red - other.Red) < EPSILON &&
                   MathF.Abs(Green - other.Green) < EPSILON &&
                   MathF.Abs(Blue - other.Blue) < EPSILON;
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((TColour)obj);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue);
        }
    }
}

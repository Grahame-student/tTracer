using System;

namespace libTracer.Common
{
    // ReSharper disable once InconsistentNaming
    public class TPoint : IEquatable<TPoint>
    {
        private const float EPSILON = 0.001f;

        public TPoint(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float W => 1;

        public static TPoint operator +(TPoint p1, TVector v2) => new(p1.X + v2.X, p1.Y + v2.Y, p1.Z + v2.Z);
        public static TVector operator -(TPoint p1, TPoint p2) => new(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static TPoint operator -(TPoint p1, TVector v2) => new(p1.X - v2.X, p1.Y - v2.Y, p1.Z - v2.Z);

        public bool Equals(TPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MathF.Abs(X - other.X) < EPSILON &&
                   MathF.Abs(Y - other.Y) < EPSILON &&
                   MathF.Abs(Z - other.Z) < EPSILON;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((TPoint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}

using System;

namespace libTracer.Common
{
    // ReSharper disable once InconsistentNaming
    public class TPoint : IEquatable<TPoint>
    {
        private const Single EPSILON = 0.001f;

        public TPoint(Single x, Single y, Single z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Single X { get; }
        public Single Y { get; }
        public Single Z { get; }
        public Single W => 1;

        public static TPoint operator +(TPoint p1, TVector v2) => new(p1.X + v2.X, p1.Y + v2.Y, p1.Z + v2.Z);
        public static TVector operator -(TPoint p1, TPoint p2) => new(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static TPoint operator -(TPoint p1, TVector v2) => new(p1.X - v2.X, p1.Y - v2.Y, p1.Z - v2.Z);

        public Boolean Equals(TPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MathF.Abs(X - other.X) < EPSILON &&
                   MathF.Abs(Y - other.Y) < EPSILON &&
                   MathF.Abs(Z - other.Z) < EPSILON;
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((TPoint)obj);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}

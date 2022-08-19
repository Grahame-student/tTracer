using System;

namespace libTracer.Common
{
    // ReSharper disable once InconsistentNaming
    public class TPoint : IEquatable<TPoint>
    {
        public TPoint(Double x, Double y, Double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Double X { get; }
        public Double Y { get; }
        public Double Z { get; }
        public Double W => 1;

        public static TPoint operator +(TPoint p1, TVector v2) => new(p1.X + v2.X, p1.Y + v2.Y, p1.Z + v2.Z);
        public static TVector operator -(TPoint p1, TPoint p2) => new(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static TPoint operator -(TPoint p1, TVector v2) => new(p1.X - v2.X, p1.Y - v2.Y, p1.Z - v2.Z);

        public Boolean Equals(TPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(X - other.X) < Constants.EPSILON &&
                   Math.Abs(Y - other.Y) < Constants.EPSILON &&
                   Math.Abs(Z - other.Z) < Constants.EPSILON;
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

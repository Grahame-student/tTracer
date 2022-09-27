using System;

namespace libTracer.Common;

// ReSharper disable once InconsistentNaming
public class TVector : IEquatable<TVector>
{
    public TVector(Double x, Double y, Double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Double X { get; }
    public Double Y { get; }
    public Double Z { get; }
    public Double W => 0;
    public Double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z);

    public static TVector operator +(TVector v1, TVector v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    public static TVector operator +(TVector v1, TPoint p2) => new(v1.X + p2.X, v1.Y + p2.Y, v1.Z + p2.Z);
    public static TVector operator -(TVector v1, TVector v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    public static TVector operator -(TVector v1) => new(-v1.X, -v1.Y, -v1.Z);
    public static TVector operator *(TVector v, Double s) => new(v.X * s, v.Y * s, v.Z * s);
    public static TVector operator /(TVector v, Double s) => new(v.X / s, v.Y / s, v.Z / s);

    public TVector Normalise()
    {
        return Magnitude == 0 ? 
            new TVector(0, 0, 0) : 
            new TVector(X / Magnitude, Y / Magnitude, Z / Magnitude);
    }
    public Double Dot(TVector v) => X * v.X + Y * v.Y + Z * v.Z + W * v.W;

    public TVector Cross(TVector vector2) => new(
        Y * vector2.Z - Z * vector2.Y,
        Z * vector2.X - X * vector2.Z,
        X * vector2.Y - Y * vector2.X);

    public override Boolean Equals(Object other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals((TVector)other);
    }

    public Boolean Equals(TVector other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Math.Abs(X - other.X) < Constants.EPSILON &&
               Math.Abs(Y - other.Y) < Constants.EPSILON &&
               Math.Abs(Z - other.Z) < Constants.EPSILON;
    }

    public override Int32 GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public TVector Reflect(TVector normal)
    {
        return this - normal * 2 * Dot(normal);
    }
}
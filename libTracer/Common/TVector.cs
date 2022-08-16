using System;

namespace libTracer.Common
{
    // ReSharper disable once InconsistentNaming
    public class TVector : IEquatable<TVector>
    {
        private const float EPSILON = 0.001f;

        public TVector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float W => 0;
        public float Magnitude => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public static TVector operator +(TVector v1, TVector v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static TVector operator +(TVector v1, TPoint p2) => new(v1.X + p2.X, v1.Y + p2.Y, v1.Z + p2.Z);
        public static TVector operator -(TVector v1, TVector v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static TVector operator -(TVector v1) => new(-v1.X, -v1.Y, -v1.Z);
        public static TVector operator *(TVector v, float s) => new(v.X * s, v.Y * s, v.Z * s);
        public static TVector operator /(TVector v, float s) => new(v.X / s, v.Y / s, v.Z / s);

        public TVector Normalise() => new(X / Magnitude, Y / Magnitude, Z / Magnitude);
        public float Dot(TVector v) => X * v.X + Y * v.Y + Z * v.Z + W * W;

        public TVector Cross(TVector vector2) => new(
            Y * vector2.Z - Z * vector2.Y,
            Z * vector2.X - X * vector2.Z,
            X * vector2.Y - Y * vector2.X);

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals((TVector)other);
        }

        public bool Equals(TVector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MathF.Abs(X - other.X) < EPSILON &&
                   MathF.Abs(Y - other.Y) < EPSILON &&
                   MathF.Abs(Z - other.Z) < EPSILON;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public TVector Reflect(TVector normal)
        {
            return this - normal * 2 * Dot(normal);
        }
    }
}

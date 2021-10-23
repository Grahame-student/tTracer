using System;

namespace libTracer
{
    // ReSharper disable once InconsistentNaming
    public class TPoint
    {
        public TPoint(Single x, Single y, Single z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Single X { get; }
        public Single Y { get; }
        public Single Z { get; }
        public static Single W => 1;

        public static TPoint operator +(TPoint p1, TVector v2) => new(p1.X + v2.X, p1.Y + v2.Y, p1.Z + v2.Z);
        public static TVector operator -(TPoint p1, TPoint p2) => new(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static TPoint operator -(TPoint p1, TVector v2) => new(p1.X - v2.X, p1.Y - v2.Y, p1.Z - v2.Z);
    }
}

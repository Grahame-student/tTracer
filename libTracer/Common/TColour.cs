using System;

namespace libTracer.Common;

public class TColour : IEquatable<TColour>
{
    public Double Red { get; }
    public Double Green { get; }
    public Double Blue { get; }

    public TColour(Double red, Double green, Double blue)
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

    public static TColour operator *(TColour c, Double s) => new(
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
        return Math.Abs(Red - other.Red) < Constants.EPSILON &&
               Math.Abs(Green - other.Green) < Constants.EPSILON &&
               Math.Abs(Blue - other.Blue) < Constants.EPSILON;
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
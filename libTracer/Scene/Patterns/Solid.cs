using System;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene.Patterns;

public class Solid : Pattern, IEquatable<Solid>
{
    private TColour Colour { get; set; }

    public Solid(TColour colour)
    {
        Colour = colour;
    }

    protected override TColour LocalColourAt(Shape shape, TPoint point)
    {
        return Colour;
    }

    public Boolean Equals(Solid other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Colour, other.Colour);
    }

    public override Boolean Equals(Object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Solid)obj);
    }

    public override Int32 GetHashCode()
    {
        return (Colour != null ? Colour.GetHashCode() : 0);
    }
}
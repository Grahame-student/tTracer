using System;
using System.Collections.Generic;
using System.Linq;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene;

public class Intersection : IComparable<Intersection>, IEquatable<Intersection>
{
    // Smaller value results in scene acne
    private const Double EPSILON = 0.0001;

    public Double Time { get; }
    public Shape Shape { get; }

    public Intersection(Double time, Shape shape)
    {
        Time = time;
        Shape = shape;
    }

    public Int32 CompareTo(Intersection other)
    {
        if (Time < other.Time) return -1;
        if (Time > other.Time) return 1;
        return 0;
    }

    /// <summary>
    /// Return the intersection with lowest non-negative time.
    /// </summary>
    /// <param name="list"></param>
    /// <returns>The first intersection with lowest non-negative time. Returns null if no intersections meet the criteria</returns>
    public static Intersection Hit(IList<Intersection> list)
    {
        return list.FirstOrDefault(intersection => !(intersection.Time < 0));
    }

    public Computations PrepareComputations(TRay ray, IList<Intersection> intersections)
    {
        var result = new Computations
        {
            Time = Time,
            Object = Shape,
            Inside = false
        };
        result.Point = ray.Position(result.Time);
        result.EyeV = -ray.Direction;
        result.NormalV = result.Object.Normal(result.Point);
        if (result.NormalV.Dot(result.EyeV) < 0)
        {
            result.Inside = true;
            result.NormalV = -result.NormalV;
        }
        result.ReflectV = ray.Direction.Reflect(result.NormalV);
        result.OverPoint = result.Point + result.NormalV * EPSILON;
        result.UnderPoint = result.Point - result.NormalV * EPSILON;

        SetTransparencyTransitions(intersections, result);

        return result;
    }

    private void SetTransparencyTransitions(IList<Intersection> intersections, Computations result)
    {
        var containers = new List<Shape>();
        Intersection hit = Hit(intersections);
        foreach (Intersection intersection in intersections)
        {
            if (intersection.Equals(this))
            {
                result.N1 = containers.Count == 0 ? 1.0 : containers.Last().Material.RefractiveIndex;
            }

            if (containers.Contains(intersection.Shape))
            {
                containers.Remove(intersection.Shape);
            }
            else
            {
                containers.Add(intersection.Shape);
            }

            if (intersection.Equals(this))
            {
                result.N2 = containers.Count == 0 ? 1.0 : containers.Last().Material.RefractiveIndex;
                break;
            }
        }
    }

    public Boolean Equals(Intersection other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Time.Equals(other.Time) && Equals(Shape, other.Shape);
    }

    public override Boolean Equals(Object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Intersection)obj);
    }

    public override Int32 GetHashCode()
    {
        return HashCode.Combine(Time, Shape);
    }

    public static Double Schlick(Computations comps)
    {
        Double cos = comps.EyeV.Dot(comps.NormalV);
        if (comps.N1 > comps.N2)
        {
            Double n = comps.N1 / comps.N2;
            Double sin2T = Math.Pow(n, 2) * (1.0 - Math.Pow(cos, 2));
            if (sin2T > 1.0) return 1.0;

            Double cosT = Math.Sqrt(1.0 - sin2T);
            cos = cosT;
        }

        Double r0 = Math.Pow((comps.N1 - comps.N2) / (comps.N1 + comps.N2), 2);
        return r0 + (1 - r0) * Math.Pow(1 - cos, 5);
    }
}
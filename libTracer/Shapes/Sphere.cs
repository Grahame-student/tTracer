using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes;

public class Sphere : Shape
{
    protected override IList<Intersection> LocalIntersects(TRay ray)
    {
        var result = new List<Intersection>();

        // Calculate the discriminant, which will tell us if the ray has hit the object
        TVector sphereToRay = ray.Origin - new TPoint(0, 0, 0);
        Double a = ray.Direction.Dot(ray.Direction);
        Double b = 2 * ray.Direction.Dot(sphereToRay);
        Double c = sphereToRay.Dot(sphereToRay) - 1;

        Double discriminant = b * b - 4 * a * c;
        if (discriminant < 0) return result;

        // Now that we know the ray has hit, determine where the hits occurred
        result.Add(new Intersection((-b - Math.Sqrt(discriminant)) / (2 * a), this));
        result.Add(new Intersection((-b + Math.Sqrt(discriminant)) / (2 * a), this));

        return result;
    }

    protected override Bounds LocalBounds()
    {
        return new Bounds(new TPoint(-1, -1, -1), new TPoint(1, 1, 1));
    }

    protected override TVector LocalNormal(TPoint point)
    {
        return point - new TPoint(0, 0, 0);
    }

    public static Sphere Glass()
    {
        return new Sphere
        {
            Material = new Material
            {
                Transparency = 1.0,
                RefractiveIndex = 1.5
            }
        };
    }
}
using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene;

public class World
{
    public Light Light { get; set; }
    public IList<Shape> Objects { get; }

    public World()
    {
        Objects = new List<Shape>();
    }

    /// <summary>
    /// Creates a default world object
    /// </summary>
    /// <returns></returns>
    public static World CreateWorld()
    {
        var world = new World
        {
            Light = new Light(new TPoint(-10, 10, -10), new TColour(1, 1, 1)),
        };

        world.Objects.Add(new Sphere()
        {
            Material = new Material()
            {
                Colour = new TColour(0.8, 1.0, 0.6),
                Diffuse = 0.7,
                Specular = 0.2
            }
        });
        world.Objects.Add(new Sphere()
        {
            Transform = new TMatrix().Scaling(0.5, 0.5, 0.5)
        });

        return world;
    }

    public IList<Intersection> Intersects(TRay ray)
    {
        var result = new List<Intersection>();
        foreach (Shape shape in Objects)
        {
            result.AddRange(shape.Intersects(ray));
        }
        // We sort the list to make it easier to determine the order of the hits
        // This may need to move to a better place later to reduce the total number
        // of sorts that occur
        result.Sort();
        return result;
    }

    public TColour ShadeHit(Computations computations, Int32 remaining)
    {
        TColour surface = computations.Object.Material.Lighting(computations.Object, Light, computations.OverPoint,
            computations.EyeV, computations.NormalV, IsShadowed(computations.OverPoint));
        TColour reflected = ReflectedColour(computations, remaining);
        TColour refracted = RefractedColour(computations, remaining);

        if (computations.Object.Material.Reflective > 0 &&
            computations.Object.Material.Transparency > 0)
        {
            var reflectance = Intersection.Schlick(computations);
            return surface +  reflected * reflectance + 
                   refracted * (1 - reflectance);
        }
        else
        {
            return surface + reflected + refracted;
        }
    }

    public TColour ColourAt(TRay ray, Int32 remaining)
    {
        IList<Intersection> intersections = Intersects(ray);
        Intersection hit = Intersection.Hit(intersections);
        if (hit == null) return new TColour(0, 0, 0);

        Computations comp = hit.PrepareComputations(ray, intersections);
        return ShadeHit(comp, remaining);
    }

    public Boolean IsShadowed(TPoint point)
    {
        TVector v = Light.Position - point;
        Double distance = v.Magnitude;
        TVector direction = v.Normalise();

        var r = new TRay(point, direction);
        IList<Intersection> intersections = Intersects(r);

        Intersection h = Intersection.Hit(intersections);
        return h != null && h.Time < distance;
    }

    public TColour ReflectedColour(Computations comps, Int32 remaining)
    {
        if (remaining <= 0 || comps.Object.Material.Reflective == 0.0)
        {
            return ColourFactory.Black();
        }

        var reflectRay = new TRay(comps.OverPoint, comps.ReflectV);
        TColour colour = ColourAt(reflectRay, remaining - 1);
        return colour * comps.Object.Material.Reflective;
    }

    public TColour RefractedColour(Computations comps, Int32 remaining)
    {
        if (remaining <=0 || comps.Object.Material.Transparency == 0.0)
        {
            return ColourFactory.Black();
        }
        Double ratio = comps.N1 / comps.N2;
        Double cosI = comps.EyeV.Dot(comps.NormalV);
        Double sin2T = Math.Pow(ratio, 2) * (1 - Math.Pow(cosI, 2));
        if (sin2T > 1)
        {
            return ColourFactory.Black();
        }

        Double cosT = Math.Sqrt(1.0 - sin2T);
        TVector direction = comps.NormalV * (ratio * cosI - cosT) - comps.EyeV * ratio;
        var refractedRay = new TRay(comps.UnderPoint, direction);

        return ColourAt(refractedRay, remaining - 1) * comps.Object.Material.Transparency;
    }
}
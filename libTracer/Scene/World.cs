using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene
{
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
                    Colour = new TColour(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f
                }
            });
            world.Objects.Add(new Sphere()
            {
                Transform = new TMatrix().Scaling(0.5f, 0.5f, 0.5f)
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

        public TColour ShadeHit(Computations computations)
        {
            return computations.Object.Material.Lighting(computations.Object, Light, computations.OverPoint, computations.EyeV,
                computations.NormalV, IsShadowed(computations.OverPoint));
        }

        public TColour ColourAt(TRay ray)
        {
            IList<Intersection> intersections = Intersects(ray);
            Intersection hit = Intersection.Hit(intersections);
            if (hit == null) return new TColour(0, 0, 0);

            Computations comp = hit.PrepareComputations(ray);
            return ShadeHit(comp);
        }

        public Boolean IsShadowed(TPoint point)
        {
            TVector v = Light.Position - point;
            Single distance = v.Magnitude;
            TVector direction = v.Normalise();

            var r = new TRay(point, direction);
            IList<Intersection> intersections = Intersects(r);

            Intersection h = Intersection.Hit(intersections);
            return h != null && h.Time < distance;
        }
    }
}

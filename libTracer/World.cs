using System.Collections.Generic;

namespace libTracer
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
            result.Sort();

            return result;
        }

        public TColour ShadeHit(Computations computations)
        {
            return computations.Object.Material.Lighting(Light, computations.Point, computations.EyeV,
                computations.NormalV);
        }

        public TColour ColourAt(TRay ray)
        {
            IList<Intersection> intersections = Intersects(ray);
            Intersection hit = Intersection.Hit(intersections);
            if (hit == null) return new TColour(0, 0, 0);

            Computations comp = hit.PrepareComputations(ray);
            return ShadeHit(comp);
        }
    }
}

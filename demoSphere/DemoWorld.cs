using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using System;

namespace demoSphere
{
    public class DemoWorld
    {
        public TPoint RayOrigin { get; }
        public Double WallZ { get; }
        public Double WallSize { get; }
        public Double HalfWall { get; }
        public Sphere Sphere { get; }
        public Light Light { get; }

        public DemoWorld(TPoint origin, Double wallZ, Double wallSize)
        {
            RayOrigin = origin;
            WallZ = wallZ;
            WallSize = wallSize;
            HalfWall = wallSize / 2;
            Sphere = new Sphere
            {
                Material = new Material
                {
                    Colour = new TColour(1, 0.2, 1)
                }
            };
            Light = new Light(new TPoint(-10, 10, -10), new TColour(1, 1, 1));
        }
    }
}

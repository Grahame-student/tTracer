using libTracer;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using System;

namespace demoSphere
{
    public class DemoWorld
    {
        public TPoint RayOrigin { get; }
        public Single WallZ { get; }
        public Single WallSize { get; }
        public Single HalfWall { get; }
        public Sphere Sphere { get; }
        public Light Light { get; }

        public DemoWorld(TPoint origin, Single wallZ, Single wallSize)
        {
            RayOrigin = origin;
            WallZ = wallZ;
            WallSize = wallSize;
            HalfWall = wallSize / 2;
            Sphere = new Sphere
            {
                Material = new Material
                {
                    Colour = new TColour(1, 0.2f, 1)
                }
            };
            Light = new Light(new TPoint(-10, 10, -10), new TColour(1, 1, 1));
        }
    }
}

using libTracer;

using System;
using System.Collections.Generic;

namespace demoShadow
{
    internal class Program
    {
        static void Main(String[] args)
        {
            var rayOrigin = new TPoint(0, 0, -5);
            Single wallZ = 10;
            Single wallSize = 7;

            var size = 500;
            var canvas = new Canvas(size, size);
            Single pixelSize = wallSize / canvas.Width;
            Single halfWall = wallSize / 2;

            var sphereColour= new TColour(1, 0, 0);
            var sphere = new Sphere();

            foreach (Pixel pixel in canvas.Pixels)
            {
                Single worldY = -halfWall + pixelSize * pixel.Y;
                Single worldX = -halfWall + pixelSize * pixel.X;

                var position = new TPoint(worldX, worldY, wallZ);

                var ray = new TRay(rayOrigin, (position - rayOrigin).Normalise());
                IList<Intersection> intersection = sphere.Intersects(ray);
                if (Intersection.Hit(intersection) == null) continue;

                canvas.SetPixel(pixel.X, pixel.Y, sphereColour);
            }

            var writer = new BitmapWriter();
            writer.SaveToBitmap(canvas, @"sphere.png");
        }
    }
}

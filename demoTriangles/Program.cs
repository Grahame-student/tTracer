using System.Diagnostics;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

namespace demoTriangles;

internal class Program
{
    static void Main(string[] args)
    {
        var world = new World
        {
            Light = new Light(
                new TPoint(1, 1, -5),
                new TColour(0.9, 0.9, 0.9))
        };

        world.Objects.Add(new Triangle(
            new TPoint(-1, 0,  1),
            new TPoint(-1, 0, -1), 
            new TPoint(0, 1, 0)));
        world.Objects.Add(new Triangle(
            new TPoint(1, 0, 1),
            new TPoint(1, 0, -1),
            new TPoint(0, 1, 0)));
        world.Objects.Add(new Triangle(
            new TPoint(-1, 0, 1),
            new TPoint( 1, 0, 1),
            new TPoint( 0, 1, 0)));

        var camera = new Camera(500, 500, 0.45) // benchmark
            // var camera = new Camera(8000, 8000, 0.45)
            {
                Transformation = TMatrix.ViewTransformation(new TPoint(0, 0, -8),
                    new TPoint(0, 0, 0),
                    new TVector(0, 1, 0))
            };

        var timer = Stopwatch.StartNew();
        Canvas? image = camera.Render(world, 5);
        timer.Stop();
        Console.WriteLine($"Rendered in {timer.ElapsedMilliseconds} ms");

        var write = new BitmapWriter();
        write.SaveToBitmap(image, @"result.png");
    }
}

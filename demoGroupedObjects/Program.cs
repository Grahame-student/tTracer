using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

using System.Diagnostics;

namespace demoGroupedObjects;

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
        Shape hex = Hexagon();
        // hex.Transform = new TMatrix().RotationX(Math.PI / 2);
        world.Objects.Add(hex);

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

    private static Shape Hexagon()
    {
        var hexagon = new Group();

        for (UInt32 i = 0; i < 6; ++i)
        {
            Shape side = HexSide();
            side.Transform = new TMatrix().RotationY(i * Math.PI / 3);
            hexagon.Add(side);
        }

        return hexagon;
    }

    private static Shape HexSide()
    {
        var side = new Group();
        side.Add(HexCorner());
        side.Add(HexEdge());

        return side;
    }

    private static Shape HexCorner()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .Scaling(0.25, 0.25, 0.25)
                .Translation(0, 0, -1)
        };
    }

    private static Shape HexEdge()
    {
        return new Cylinder
        {
            Minimum = 0,
            Maximum = 1,
            Transform = new TMatrix()
                .Scaling(0.25, 1, 0.25)
                .RotationZ(-Math.PI / 2)
                .RotationY(-Math.PI / 6)
                .Translation(0, 0, -1)
        };
    }
}

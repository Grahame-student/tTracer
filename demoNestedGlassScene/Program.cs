using System.Diagnostics;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;

namespace demoNestedGlassScene;

internal class Program
{
    private static void Main(String[] args)
    {
        var world = new World
        {
            Light = new Light(
                new TPoint(2, 10, -5),
                new TColour(0.9, 0.9, 0.9))
        };

        world.Objects.Add(CreateWall());
        world.Objects.Add(CreateBall());
        world.Objects.Add(CreateHollow());

        // var camera = new Camera(500, 500, 0.45) // benchmark
        var camera = new Camera(8000, 8000, 0.45)
        {
            Transformation = TMatrix.ViewTransformation(new TPoint(0, 0, -5),
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

    private static Shape CreateWall()
    {
        var shape = new Plane
        {
            Transform = new TMatrix()
                .RotationX(1.5708)
                .Translation(0, 0, 10),
            Material =
            {
                Pattern = new Checkers(new TColour(0.15, 0.15, 0.15), new TColour(0.85, 0.85, 0.85)),
                Ambient = 0.8,
                Diffuse = 0.2,
                Specular = 0
            }
        };
        return shape;
    }

    private static Shape CreateBall()
    {
        var shape = new Sphere
        {
            Material =
            {
                Colour = new TColour(1, 1, 1),
                Ambient = 0,
                Diffuse = 0,
                Specular = 0.9,
                Shininess = 300,
                Reflective = 0.9,
                Transparency = 0.9,
                RefractiveIndex = 1.5
            }
        };

        return shape;
    }

    private static Shape CreateHollow()
    {
        var shape = new Sphere
        {
            Transform = new TMatrix().Scaling(0.5, 0.5, 0.5),
            Material =
            {
                Colour = new TColour(1, 1, 1),
                Ambient = 0,
                Diffuse = 0,
                Specular = 0.9,
                Shininess = 300,
                Reflective = 0.9,
                Transparency = 0.9,
                RefractiveIndex = 1.0000034
            }
        };

        return shape;
    }
}
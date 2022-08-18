using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;

namespace demoScene;

internal class Program
{
    private static void Main(String[] args)
    {
        var world = new World
        {
            Light = new Light(new TPoint(-10, 10, -10), new TColour(1, 1, 1))
        };

        world.Objects.Add(CreateFloor());
        world.Objects.Add(CreateLeftWall());
        world.Objects.Add(CreateRightWall());
        world.Objects.Add(CreateBigGreenSphere());
        world.Objects.Add(CreateMediumRedSphere());
        world.Objects.Add(CreateSmallYellowSphere());

        var camera = new Camera(4000, 2000, MathF.PI / 3)
        {
            Transformation = TMatrix.ViewTransformation(new TPoint(0, 1.5f, -5), 
                new TPoint(0, 1, 0), 
                new TVector(0, 1, 0))
        };
        Canvas? image = camera.Render(world);

        var write = new BitmapWriter();
        write.SaveToBitmap(image, @"result.png");
    }

    private static Plane CreateFloor()
    {
        return new Plane
        {
            Material = new Material
            {
                Colour = new TColour(1, 0.9f, 0.9f),
                Specular = 0
            }
        };
    }
    private static Plane CreateLeftWall()
    {
        return new Plane
        {
            Transform = new TMatrix()
                .RotationX(MathF.PI / 2)
                .RotationY(-MathF.PI / 4)
                .Translation(0, 0, 5),
            Material = new Material
            {
                Colour = new TColour(1, 0.9f, 0.9f),
                Specular = 0
            }
        };
    }

    private static Shape CreateRightWall()
    {
        return new Plane
        {
            Transform = new TMatrix()
                .RotationX(MathF.PI / 2)
                .RotationY(MathF.PI / 4)
                .Translation(0, 0, 5),
            Material = new Material
            {
                Colour = new TColour(1, 0.9f, 0.9f),
                Specular = 0
            }
        };
    }

    private static Shape CreateBigGreenSphere()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .RotationY(MathF.PI/4)
                .Translation(-0.5f, 1, 0.5f),
            Material = new Material
            {
                Pattern = new Checkers(ColourFactory.White(), ColourFactory.Black())
                {
                    Transform = new TMatrix().Scaling(0.5f, 0.5f, 0.5f)
                },
                Colour = new TColour(0.1f, 1, 0.5f),
                Diffuse = 0.7f,
                Specular = 0.3f
            }
        };
    }

    private static Shape CreateMediumRedSphere()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .RotationZ(MathF.PI / 8)
                .Scaling(0.5f, 0.5f, 0.5f)
                .Translation(1.5f, 0.5f, -0.5f),
            Material = new Material
            {
                Pattern = new Gradient(new TColour(0.9f, 0.1f, 0.1f), new TColour(0.1f, 0.1f, 0.9f)),
                Diffuse = 0.7f,
                Specular = 0.3f
            }
        };
    }

    private static Shape CreateSmallYellowSphere()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .Scaling(0.33f, 0.33f, 0.33f)
                .Translation(-1.5f, 0.33f, -0.75f),
            Material = new Material
            {
                Colour = new TColour(1, 0.8f, 0.1f),
                Diffuse = 0.7f,
                Specular = 0.3f
            }
        };
    }
}

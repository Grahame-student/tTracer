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

        var camera = new Camera(400, 200, Math.PI / 3)
        {
            Transformation = TMatrix.ViewTransformation(new TPoint(0, 1.5, -5), 
                new TPoint(0, 1, 0), 
                new TVector(0, 1, 0))
        };
        Canvas? image = camera.Render(world, 5);

        var write = new BitmapWriter();
        write.SaveToBitmap(image, @"result.png");
    }

    private static Plane CreateFloor()
    {
        return new Plane
        {
            Material = new Material
            {
                Pattern = new Stripes(
                    new Gradient(new TColour(1, 0.9, 0.9), new TColour(0.9, 0.9, 1)),
                    new Solid(new TColour(0.5, 0.5, 0.5))
                    ),
                Specular = 0,
                Reflective = 0.5
            }
        };
    }
    private static Plane CreateLeftWall()
    {
        return new Plane
        {
            Transform = new TMatrix()
                .RotationX(Math.PI / 2)
                .RotationY(-Math.PI / 4)
                .Translation(0, 0, 5),
            Material = new Material
            {
                Colour = new TColour(1, 0.9, 0.9),
                Specular = 0
            }
        };
    }

    private static Shape CreateRightWall()
    {
        return new Plane
        {
            Transform = new TMatrix()
                .RotationX(Math.PI / 2)
                .RotationY(Math.PI / 4)
                .Translation(0, 0, 5),
            Material = new Material
            {
                Colour = new TColour(1, 0.9, 0.9),
                Specular = 0
            }
        };
    }

    private static Shape CreateBigGreenSphere()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .RotationY(Math.PI/4)
                .Translation(-0.5, 1, 0.5),
            Material = new Material
            {
                Pattern = new Checkers(ColourFactory.White(), ColourFactory.Black())
                {
                    Transform = new TMatrix().Scaling(0.5, 0.5, 0.5)
                },
                Colour = new TColour(0.1, 1, 0.5),
                Diffuse = 0.7,
                Specular = 0.3,
                Reflective = 0.5
            }
        };
    }

    private static Shape CreateMediumRedSphere()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .RotationZ(Math.PI / 8)
                .Scaling(0.5, 0.5, 0.5)
                .Translation(1.5, 0.5, -0.5),
            Material = new Material
            {
                Pattern = new Gradient(new TColour(0.9, 0.1, 0.1), new TColour(0.1, 0.1, 0.9)),
                Diffuse = 0.7,
                Specular = 0.3
            }
        };
    }

    private static Shape CreateSmallYellowSphere()
    {
        return new Sphere
        {
            Transform = new TMatrix()
                .Scaling(0.33, 0.33, 0.33)
                .Translation(-1.5, 0.33, -0.75),
            Material = new Material
            {
                Colour = new TColour(1, 0.8, 0.1),
                Diffuse = 0.7,
                Specular = 0.3,
                Transparency = 0.75,
                RefractiveIndex = 1.5,
                Reflective = 0.6
            }
        };
    }
}

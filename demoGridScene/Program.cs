using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;

namespace demoGridScene
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var world = new World
            {
                Light = new Light(
                    new TPoint(2, 10, -5),
                    new TColour(0.9, 0.9, 0.9))
            };

            world.Objects.Add(CreateWall());

            world.Objects.Add(CreateBall(-1, -1, 0));
            world.Objects.Add(CreateBall(0, 0, 0));
            world.Objects.Add(CreateBall(1, 1, 0));
            world.Objects.Add(CreateBall(1, -1, 0));
            world.Objects.Add(CreateBall(-1, 1, 0));

            world.Objects.Add(CreateCube(1, 0, 0));
            world.Objects.Add(CreateCube(0, 1, 0));
            world.Objects.Add(CreateCube(-1, 0, 0));
            world.Objects.Add(CreateCube(0, -1, 0));

            var camera = new Camera(1000, 1000, 0.45)
            {
                Transformation = TMatrix.ViewTransformation(new TPoint(0, 8, -7),
                    new TPoint(0, 0, 0),
                    new TVector(0, 1, 0))
            };
            Canvas? image = camera.Render(world, 5);

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

        private static Shape CreateBall(Double x, Double y, Double z)
        {
            var shape = new Sphere
            {
                Transform = new TMatrix()
                    .Scaling(0.5, 0.5, 0.5)
                    .Translation(x, y, z),
                Material =
                {
                    Colour = new TColour(0.2, 1, 0.2),
                    Ambient = .1,
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

        private static Shape CreateCube(Double x, Double y, Double z)
        {
            var shape = new Cube()
            {
                Transform = new TMatrix()
                    .Scaling(0.5, 0.5, 0.5)
                    .Translation(x, y, z),
                Material =
                {
                    Colour = new TColour(1, 0.2, 0.2),
                    Ambient = .1,
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
    }
}
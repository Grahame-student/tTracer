using libTracer;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

namespace demoScene
{
    internal class Program
    {
        private static void Main(String[] args)
        {
            var world = new World
            {
                Light = new Light(new TPoint(-10, 10, -10), new TColour(1, 1, 1))
            };

            var floor = new Plane()
            {
                Material = new Material
                {
                    Colour = new TColour(1, 0.9f, 0.9f),
                    Specular = 0
                }
            };

            var leftWall = new Sphere
            {
                Transform = new TMatrix()
                    .Scaling(10, 0.01f, 10)
                    .RotationX(MathF.PI / 2)
                    .RotationY(-MathF.PI / 4)
                    .Translation(0, 0, 5),
                Material = floor.Material
            };

            var rightWall = new Sphere
            {
                Transform = new TMatrix()
                    .Scaling(10, 0.01f, 10)
                    .RotationX(MathF.PI / 2)
                    .RotationY(MathF.PI / 4)
                    .Translation(0, 0, 5),
                Material = floor.Material
            };

            var middle = new Sphere
            {
                Transform = new TMatrix().Translation(-0.5f, 1, 0.5f),
                Material = new Material
                {
                    Colour = new TColour(0.1f, 1, 0.5f),
                    Diffuse = 0.7f,
                    Specular = 0.3f
                }
            };

            var right = new Sphere
            {
                Transform = new TMatrix()
                    .Scaling(0.5f, 0.5f, 0.5f)
                    .Translation(1.5f, 0.5f, -0.5f),
                Material = new Material
                {
                    Colour = new TColour(0.5f, 1, 0.1f),
                    Diffuse = 0.7f,
                    Specular = 0.3f
                }
            };

            var left = new Sphere
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

            world.Objects.Add(floor);
            world.Objects.Add(leftWall);
            world.Objects.Add(rightWall);
            world.Objects.Add(middle);
            world.Objects.Add(right);
            world.Objects.Add(left);

            var camera = new Camera(800, 400, MathF.PI / 3)
            {
                Transformation = TMatrix.ViewTransformation(new TPoint(0, 1.5f, -5), 
                    new TPoint(0, 1, 0), 
                    new TVector(0, 1, 0))
            };
            Canvas? image = camera.Render(world);

            var write = new BitmapWriter();
            write.SaveToBitmap(image, @"result.png");
        }
    }
}

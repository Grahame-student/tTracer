using System;
using System.Threading.Tasks;
using libTracer.Common;

namespace libTracer.Scene
{
    public class Camera
    {
        private long _processedPixels;

        private float _halfWidth;
        private float _halfHeight;

        public Canvas Canvas { get; }
        public float FieldOfView { get; set; }
        public TMatrix Transformation { get; set; }
        public float PixelSize { get; private set; }

        public Camera(int width, int height, float fieldOfView)
        {
            Canvas = new Canvas(width, height);
            FieldOfView = fieldOfView;
            Transformation = new TMatrix();

            SetPixelSize();
        }

        private void SetPixelSize()
        {
            float halfView = MathF.Tan(FieldOfView / 2);
            float aspect = Canvas.Width / (float)Canvas.Height;

            if (aspect >= 1)
            {
                _halfWidth = halfView;
                _halfHeight = halfView / aspect;
            }
            else
            {
                _halfWidth = halfView * aspect;
                _halfHeight = halfView;
            }

            PixelSize = _halfWidth * 2 / Canvas.Width;
        }

        public TRay PixelRay(int pixelX, int pixelY)
        {
            float xOffset = (pixelX + 0.5f) * PixelSize;
            float yOffset = (pixelY + 0.5f) * PixelSize;

            float worldX = _halfWidth - xOffset;
            float worldY = _halfHeight - yOffset;

            TMatrix inverse = Transformation.Inverse();
            TPoint pixel = inverse * new TPoint(worldX, worldY, -1);
            TPoint origin = inverse * new TPoint(0, 0, 0);
            TVector direction = (pixel - origin).Normalise();
            return new TRay(origin, direction);
        }

        public Canvas Render(World world)
        {
            _processedPixels = 0;
            Parallel.ForEach(Canvas.Pixels, pixel => RenderPixel(world, pixel, Canvas));
            return Canvas;
        }

        private void RenderPixel(World world, Pixel pixel, Canvas canvas)
        {
            TRay ray = PixelRay(pixel.X, pixel.Y);
            TColour colour = world.ColourAt(ray);
            canvas.SetPixel(pixel.X, pixel.Y, colour);
            _processedPixels++;
            if (_processedPixels % 100000 == 0)
            {
                Console.WriteLine($"Processing pixel {_processedPixels} of {Canvas.PixelCount} {_processedPixels / (float)Canvas.PixelCount * 100:F2}%");
            };
        }
    }
}

using System;
using System.Threading.Tasks;
using libTracer.Common;

namespace libTracer.Scene
{
    public class Camera
    {
        private Int64 _processedPixels;

        private Double _halfWidth;
        private Double _halfHeight;

        public Canvas Canvas { get; }
        public Double FieldOfView { get; set; }
        public TMatrix Transformation { get; set; }
        public Double PixelSize { get; private set; }

        public Camera(Int32 width, Int32 height, Double fieldOfView)
        {
            Canvas = new Canvas(width, height);
            FieldOfView = fieldOfView;
            Transformation = new TMatrix();

            SetPixelSize();
        }

        private void SetPixelSize()
        {
            Double halfView = Math.Tan(FieldOfView / 2);
            Double aspect = Canvas.Width / (Double)Canvas.Height;

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

        public TRay PixelRay(Int32 pixelX, Int32 pixelY)
        {
            Double xOffset = (pixelX + 0.5) * PixelSize;
            Double yOffset = (pixelY + 0.5) * PixelSize;

            Double worldX = _halfWidth - xOffset;
            Double worldY = _halfHeight - yOffset;

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
            TColour colour = world.ColourAt(ray, 5);
            canvas.SetPixel(pixel.X, pixel.Y, colour);
            _processedPixels++;
            if (_processedPixels % 100000 == 0)
            {
                Console.WriteLine(
                    $"Processing pixel {_processedPixels} of {Canvas.PixelCount} {_processedPixels / (Double)Canvas.PixelCount * 100:F2}%");
            };
        }
    }
}

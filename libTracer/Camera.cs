using System;
using System.Threading.Tasks;

namespace libTracer
{
    public class Camera
    {
        private Single _halfWidth;
        private Single _halfHeight;

        public Canvas Canvas { get; }
        public Single FieldOfView { get; set; }
        public TMatrix Transformation { get; set; }
        public Single PixelSize { get; private set; }

        public Camera(Int32 width, Int32 height, Single fieldOfView)
        {
            Canvas = new Canvas(width, height);
            FieldOfView = fieldOfView;
            Transformation = new TMatrix();

            SetPixelSize();
        }

        private void SetPixelSize()
        {
            Single halfView = MathF.Tan(FieldOfView / 2);
            Single aspect = Canvas.Width / (Single)Canvas.Height;

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

            PixelSize = (_halfWidth * 2) / Canvas.Width;
        }

        public TRay PixelRay(Int32 pixelX, Int32 pixelY)
        {
            Single xOffset = (pixelX + 0.5f) * PixelSize;
            Single yOffset = (pixelY + 0.5f) * PixelSize;

            Single worldX = _halfWidth - xOffset;
            Single worldY = _halfHeight - yOffset;

            TMatrix inverse = Transformation.Inverse();
            TPoint pixel = inverse * new TPoint(worldX, worldY, -1);
            TPoint origin = inverse * new TPoint(0, 0, 0);
            TVector direction = (pixel - origin).Normalise();
            return new TRay(origin, direction);
        }

        public Canvas Render(World world)
        {
            Parallel.ForEach(Canvas.Pixels, pixel => RenderPixel(world, pixel, Canvas));
            return Canvas;
        }

        private void RenderPixel(World world, Pixel pixel, Canvas canvas)
        {
            TRay ray = PixelRay(pixel.X, pixel.Y);
            TColour colour = world.ColourAt(ray);
            canvas.SetPixel(pixel.X, pixel.Y, colour);
        }
    }
}

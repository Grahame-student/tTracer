using System;
using System.Threading.Tasks;
using libTracer.Common;

namespace libTracer.Scene;

public class Camera
{
    private const String TIMESTAMP_FORMAT = "dd/MM/yyyy,HH:mm:ss.ffff";

    private Int64 _processedPixels;

    private Double _halfWidth;
    private Double _halfHeight;

    public Canvas Canvas { get; }
    public Double FieldOfView { get; set; }
    public TMatrix Transformation { get; set; }
    public TMatrix Inverse { get; set; }
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

        Inverse ??= Transformation.Inverse();
        // TMatrix inverse = Transformation.Inverse();
        TPoint pixel = Inverse * new TPoint(worldX, worldY, -1);
        TPoint origin = Inverse * new TPoint(0, 0, 0);
        TVector direction = (pixel - origin).Normalise();
        return new TRay(origin, direction);
    }

    public Canvas Render(World world, Int32 remaining)
    {
        _processedPixels = 0;
        Parallel.ForEach(Canvas.Pixels, pixel => RenderPixel(world, pixel, Canvas, remaining));
        return Canvas;
    }

    private void RenderPixel(World world, Pixel pixel, Canvas canvas, Int32 remaining)
    {
        TRay ray = PixelRay(pixel.X, pixel.Y);
        TColour colour = world.ColourAt(ray, remaining);
        canvas.SetPixel(pixel.X, pixel.Y, colour);
        _processedPixels++;
        if (_processedPixels % 100000 == 0)
        {
            Console.WriteLine($"{DateTime.Now.ToString(TIMESTAMP_FORMAT)} - " +
                              $"Processing pixel {_processedPixels} of {Canvas.PixelCount} {_processedPixels / (Double)Canvas.PixelCount * 100:F2}%");
        };
    }
}
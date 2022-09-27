using libTracer.Common;

namespace libTracer.Scene;

public static class ColourFactory
{
    public static TColour Black() => new(0, 0, 0);
    public static TColour White() => new(1, 1, 1);
}
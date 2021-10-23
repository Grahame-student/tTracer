using System;

namespace libTracer
{
    public class Pixel
    {
        public Int32 X { get; }
        public Int32 Y { get; }
        public TColour Colour { get; private set; }

        public Pixel(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
            Colour = new TColour(0, 0, 0);
        }

        public void SetColour(Single red, Single green, Single blue)
        {
            Colour = new TColour(red, green, blue);
        }
    }
}

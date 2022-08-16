using System;
using libTracer.Common;

namespace libTracer.Scene
{
    public class Pixel
    {
        public int X { get; }
        public int Y { get; }
        public TColour Colour { get; private set; }

        public Pixel(int x, int y)
        {
            X = x;
            Y = y;
            Colour = new TColour(0, 0, 0);
        }

        public void SetColour(float red, float green, float blue)
        {
            Colour = new TColour(red, green, blue);
        }
    }
}

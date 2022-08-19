using System;
using System.Drawing;
using System.Drawing.Imaging;
using libTracer.Common;

namespace libTracer.Scene
{
    public class BitmapWriter
    {
        private const Int32 MAX_VALUE = 255;

        public void SaveToBitmap(Canvas canvas, String path)
        {
            using var bm = new Bitmap(canvas.Width, canvas.Height);
            foreach (Pixel pixel in canvas.Pixels)
            {
                Color colour = GetScaledColour(pixel.Colour);
                bm.SetPixel(pixel.X, pixel.Y, colour);
            }
            bm.Save(path, ImageFormat.Png);
        }

        private Color GetScaledColour(TColour colour)
        {
            return Color.FromArgb(MAX_VALUE,
                GetScaledChannel(colour.Red),
                GetScaledChannel(colour.Green),
                GetScaledChannel(colour.Blue));
        }

        private Int32 GetScaledChannel(Double channel)
        {
            var value = (Int32)(channel * MAX_VALUE);
            if (value < 0) value = 0;
            if (value > MAX_VALUE) value = MAX_VALUE;
            return value;
        }

    }
}

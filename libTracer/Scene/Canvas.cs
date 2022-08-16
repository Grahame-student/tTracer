using System;
using System.Collections.Generic;
using System.Linq;
using libTracer.Common;

namespace libTracer.Scene
{
    public class Canvas
    {
        private readonly IList<IList<Pixel>> _pixels;

        public int Width => _pixels[0].Count;
        public int Height => _pixels.Count;
        public long PixelCount { get; }

        public Canvas(int width, int height)
        {
            _pixels = new List<IList<Pixel>>();
            PixelCount = width * height;

            for (var row = 0; row < height; row++)
            {
                var newRow = new List<Pixel>();
                for (var col = 0; col < width; col++)
                {
                    newRow.Add(new Pixel(col, row));
                }
                _pixels.Add(newRow);
            }
        }

        public IEnumerable<Pixel> Pixels => _pixels.SelectMany(row => row);

        public void SetPixel(int x, int y, TColour colour)
        {
            _pixels[y][x].SetColour(colour.Red, colour.Green, colour.Blue);
        }

        public TColour GetPixel(int x, int y)
        {
            return _pixels[y][x].Colour;
        }
    }
}

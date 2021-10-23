using System;
using System.Collections.Generic;
using System.Linq;

namespace libTracer
{
    public class Canvas
    {
        private readonly IList<IList<Pixel>> _pixels;

        public Int32 Width => _pixels[0].Count;
        public Int32 Height => _pixels.Count;

        public Canvas(Int32 width, Int32 height)
        {
            _pixels = new List<IList<Pixel>>();
            Int32 maxHeight = height - 1;
            for (var row = 0; row < height; row++)
            {
                var newRow = new List<Pixel>();
                for (var col = 0; col < width; col++)
                {
                    // Invert the y-axis so that when the canvas is used it is the right way up
                    newRow.Add(new Pixel(col, maxHeight - row));
                }
                _pixels.Add(newRow);
            }
        }

        public IEnumerable<Pixel> Pixels => _pixels.SelectMany(row => row);

        public void SetPixel(Int32 x, Int32 y, TColour colour)
        {
            _pixels[y][x].SetColour(colour.Red, colour.Green, colour.Blue);
        }
    }
}

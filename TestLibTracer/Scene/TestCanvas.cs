using System;
using System.Collections.Generic;
using System.Linq;
using libTracer.Common;
using libTracer.Scene;
using NUnit.Framework;

namespace TestLibTracer.Scene
{
    internal class TestCanvas
    {
        private const Int32 SOME_WIDTH = 10;
        private const Int32 SOME_HEIGHT = 20;

        private const Single SOME_RED = 0.1f;
        private const Single SOME_GREEN = 0.2f;
        private const Single SOME_BLUE = 0.3f;

        private Canvas _canvas;

        [Test]
        public void Constructor_SetsWidth_ToPassedInValue()
        {
            _canvas = new Canvas(SOME_WIDTH, SOME_HEIGHT);

            Assert.That(_canvas.Width, Is.EqualTo(SOME_WIDTH));
        }

        [Test]
        public void Constructor_SetsHeight_ToPassedInValue()
        {
            _canvas = new Canvas(SOME_WIDTH, SOME_HEIGHT);

            Assert.That(_canvas.Height, Is.EqualTo(SOME_HEIGHT));
        }

        [Test]
        public void Pixels_Returns_AllPixelsInCanvas()
        {
            _canvas = new Canvas(SOME_WIDTH, SOME_HEIGHT);

            List<Pixel> pixelList = _canvas.Pixels.ToList();

            Assert.That(pixelList.Count, Is.EqualTo(SOME_WIDTH * SOME_HEIGHT));
        }


        [Test]
        public void Pixels_SanityCheck()
        {
            _canvas = new Canvas(SOME_WIDTH, SOME_HEIGHT);

            List<Pixel> pixelList = _canvas.Pixels.ToList();

        }

        [Test]
        public void SetPixel_SetsPixel_ToPassedInColour()
        {
            _canvas = new Canvas(SOME_WIDTH, SOME_HEIGHT);
            var colour = new TColour(SOME_RED, SOME_GREEN, SOME_BLUE);

            _canvas.SetPixel(5, 3, colour);

            List<Pixel> pixelList = _canvas.Pixels.ToList();
            Assert.That(pixelList[3 * SOME_WIDTH + 5].Colour, Is.EqualTo(colour));
        }
    }
}

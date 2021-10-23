using System;
using libTracer;
using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestPixel
    {
        private const Int32 SOME_X = 123;
        private const Int32 SOME_Y = 321;

        private const Single SOME_RED = 0.1f;
        private const Single SOME_GREEN = 0.2f;
        private const Single SOME_BLUE = 0.3f;

        private Pixel _pixel;

        [Test]
        public void Constructor_SetsX_ToPassedinValue()
        {
            _pixel = new Pixel(SOME_X, SOME_Y);

            Assert.That(_pixel.X, Is.EqualTo(SOME_X));
        }

        [Test]
        public void Constructor_SetsY_ToPassedinValue()
        {
            _pixel = new Pixel(SOME_X, SOME_Y);

            Assert.That(_pixel.Y, Is.EqualTo(SOME_Y));
        }

        [Test]
        public void Constructor_SetsY_ToBlack()
        {
            _pixel = new Pixel(SOME_X, SOME_Y);

            var expected = new TColour(0, 0, 0);

            Assert.That(_pixel.Colour, Is.EqualTo(expected));
        }

        [Test]
        public void SetColour_SetsRed_ToPassedInValue()
        {
            _pixel = new Pixel(SOME_X, SOME_Y);

            _pixel.SetColour(SOME_RED, 0, 0);

            Assert.That(_pixel.Colour.Red, Is.EqualTo(SOME_RED));
        }

        [Test]
        public void SetColour_SetsGreen_ToPassedInValue()
        {
            _pixel = new Pixel(SOME_X, SOME_Y);

            _pixel.SetColour(0, SOME_GREEN, 0);

            Assert.That(_pixel.Colour.Green, Is.EqualTo(SOME_GREEN));
        }

        [Test]
        public void SetColour_SetsBlue_ToPassedInValue()
        {
            _pixel = new Pixel(SOME_X, SOME_Y);

            _pixel.SetColour(0, 0, SOME_BLUE);

            Assert.That(_pixel.Colour.Blue, Is.EqualTo(SOME_BLUE));
        }
    }
}

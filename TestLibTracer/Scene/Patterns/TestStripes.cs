using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Scene.Patterns
{
    internal class TestStripes
    {
        private Stripes _pattern;

        [Test]
        public void Constructor_SetsA_ToFirstColour()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.A, Is.EqualTo(new Solid(ColourFactory.White())));
        }

        [Test]
        public void Constructor_SetsB_ToSecondColour()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.B, Is.EqualTo(new Solid(ColourFactory.Black())));
        }

        [Test]
        public void ColourAt_IsConstant_InY()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            TColour colour1 = _pattern.ColourAt(new Sphere(), new TPoint(0, 0, 0));
            TColour colour2 = _pattern.ColourAt(new Sphere(), new TPoint(0, 2, 0));

            Assert.That(colour1, Is.EqualTo(colour2));
        }

        [Test]
        public void ColourAt_IsConstant_InZ()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            TColour colour1 = _pattern.ColourAt(new Sphere(), new TPoint(0, 0, 0));
            TColour colour2 = _pattern.ColourAt(new Sphere(), new TPoint(0, 0, 2));

            Assert.That(colour1, Is.EqualTo(colour2));
        }

        [Test]
        public void ColourAt_StartsWithColour1_InX()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0, 0, 0)), Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_StartsWithColour1_WhenXModuloOneIsZero()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(1 - 0.001f, 0, 0)), Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_ReturnsColour2_WhenXModuloOneIsOne()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            TColour result = _pattern.ColourAt(new Sphere(), new TPoint(1, 0, 0));

            Assert.That(result, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void ColourAt_ReturnsColour2_WhenMinusXModuloOneIsOne()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            TColour result = _pattern.ColourAt(new Sphere(), new TPoint(-1, 0, 0));

            Assert.That(result, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void ColourAt_ReturnsColour1_WhenMinusXModuloOneIsZero()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());

            TColour result = _pattern.ColourAt(new Sphere(), new TPoint(-1.001f, 0, 0));

            Assert.That(result, Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_ReturnsWhite_WhenObjectTransformed()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());
            var shape = new Sphere
            {
                Transform = new TMatrix().Scaling(2, 2, 2)
            };

            TColour colour = _pattern.ColourAt(shape, new TPoint(1.5f, 0, 0));

            Assert.That(colour, Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_ReturnsWhite_WhenPatternTransformed()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black());
            _pattern.Transform = new TMatrix().Scaling(2, 2, 2);
            var shape = new Sphere();

            TColour colour = _pattern.ColourAt(shape, new TPoint(1.5f, 0, 0));

            Assert.That(colour, Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_ReturnsWhite_WhenObjectAndPatternTransformed()
        {
            _pattern = new Stripes(ColourFactory.White(), ColourFactory.Black())
            {
                Transform = new TMatrix().Translation(0.5f, 0, 0)
            };
            var shape = new Sphere
            {
                Transform = new TMatrix().Scaling(2, 2, 2)
            };

            TColour colour = _pattern.ColourAt(shape, new TPoint(2.5f, 0, 0));

            Assert.That(colour, Is.EqualTo(ColourFactory.White()));
        }
    }
}

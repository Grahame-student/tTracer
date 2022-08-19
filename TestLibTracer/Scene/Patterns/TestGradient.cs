using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Scene.Patterns
{
    public class TestGradient
    {
        private Pattern _pattern;

        [Test]
        public void ColourAt_Returns100PercentOfFirstColour_WhenXIsZero()
        {
            _pattern = new Gradient(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0, 0, 0)), Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_Returns75PercentOfFirstColour_WhenXIs25()
        {
            _pattern = new Gradient(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0.25, 0, 0)),
                Is.EqualTo(new TColour(0.75, 0.75, 0.75)));
        }

        [Test]
        public void ColourAt_Returns50PercentOfFirstColour_WhenXIs50()
        {
            _pattern = new Gradient(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0.5, 0, 0)),
                Is.EqualTo(new TColour(0.5, 0.5, 0.5)));
        }

        [Test]
        public void ColourAt_Returns25PercentOfFirstColour_WhenXIs75()
        {
            _pattern = new Gradient(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0.75, 0, 0)),
                Is.EqualTo(new TColour(0.25, 0.25, 0.25)));
        }

        [Test]
        public void ColourAt_Returns0PercentOfFirstColour_WhenXIs100()
        {
            _pattern = new Gradient(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(1 - Constants.EPSILON, 0, 0)),
                Is.EqualTo(ColourFactory.Black()));
        }
    }
}

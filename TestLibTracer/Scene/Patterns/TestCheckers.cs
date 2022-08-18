using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Scene.Patterns
{
    internal class TestCheckers
    {
        private Pattern _pattern;

        [Test]
        public void ColourAt_StartsWithWhite_FromTheCenter()
        {
            _pattern = new Checkers(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0, 0, 0)), Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void ColourAt_Alternates_InX()
        {
            _pattern = new Checkers(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(1, 0, 0)), Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void ColourAt_Alternates_InY()
        {
            _pattern = new Checkers(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0, 1, 0)), Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void ColourAt_Alternates_InZ()
        {
            _pattern = new Checkers(ColourFactory.White(), ColourFactory.Black());

            Assert.That(_pattern.ColourAt(new Sphere(), new TPoint(0, 0, 1)), Is.EqualTo(ColourFactory.Black()));
        }
    }
}

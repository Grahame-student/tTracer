using libTracer.Common;
using libTracer.Scene;
using libTracer.Scene.Patterns;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Scene.Patterns;

internal class TestRings
{
    private Pattern _pattern;

    [Test]
    public void ColourAt_Center_StartsWithColour1()
    {
        _pattern = new Rings(ColourFactory.White(), ColourFactory.Black());

        TColour colour1 = _pattern.ColourAt(new Sphere(), new TPoint(0, 0, 0));

        Assert.That(colour1, Is.EqualTo(ColourFactory.White()));
    }

    [Test]
    public void ColourAt_After1_ReturnsWithColour2()
    {
        _pattern = new Rings(ColourFactory.White(), ColourFactory.Black());

        TColour colour1 = _pattern.ColourAt(new Sphere(), new TPoint(1, 0, 0));

        Assert.That(colour1, Is.EqualTo(ColourFactory.Black()));
    }

    [Test]
    public void ColourAt_ExtendsIn_XAndZ()
    {
        _pattern = new Rings(ColourFactory.White(), ColourFactory.Black());

        TColour colour1 = _pattern.ColourAt(new Sphere(), new TPoint(1, 0, 0));
        TColour colour2 = _pattern.ColourAt(new Sphere(), new TPoint(0, 0, 1));

        Assert.That(colour1, Is.EqualTo(colour2));
    }

    [Test]
    public void ColourAt_ExtendsIn_XAndZSimultaneously()
    {
        _pattern = new Rings(ColourFactory.White(), ColourFactory.Black());

        TColour colour1 = _pattern.ColourAt(new Sphere(), new TPoint(0.708, 0, 0.708));

        Assert.That(colour1, Is.EqualTo(ColourFactory.Black()));
    }
}
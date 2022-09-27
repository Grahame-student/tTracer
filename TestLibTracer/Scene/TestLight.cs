using libTracer.Common;
using libTracer.Scene;
using NUnit.Framework;

namespace TestLibTracer.Scene;

internal class TestLight
{
    private Light _light;

    [Test]
    public void Constructor_SetsIntensity_ToPassedInValue()
    {
        var intensity = new TColour(1, 1, 1);
        _light = new Light(new TPoint(0, 0, 0), intensity);

        Assert.That(_light.Intensity, Is.EqualTo(intensity));
    }

    [Test]
    public void Constructor_SetsPosition_ToPassedInValue()
    {
        var position = new TPoint(1, 2, 3);
        _light = new Light(position, new TColour(1, 1, 1));

        Assert.That(_light.Position, Is.EqualTo(position));
    }
}
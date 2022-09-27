using libTracer.Common;
using libTracer.Scene;
using NUnit.Framework;

namespace TestLibTracer.Scene;

internal class TestColourFactory
{
    [Test]
    public void Black_Returns_BlackColour()
    {
        Assert.That(ColourFactory.Black(), Is.EqualTo(new TColour(0, 0, 0)));
    }

    [Test]
    public void White_Returns_WhiteColour()
    {
        Assert.That(ColourFactory.White(), Is.EqualTo(new TColour(1, 1, 1)));
    }
}
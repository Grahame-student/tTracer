using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestShape
{
    private Shape _shape;


    [Test]
    public void Constructor_SetsTransform_ToIdentityMatrix()
    {
        _shape = new ExampleShape();

        Assert.That(_shape.Transform, Is.EqualTo(new TMatrix()));
    }

    [Test]
    public void Constructor_SetsMaterial_ToDefaultMaterial()
    {
        _shape = new ExampleShape();

        Assert.That(_shape.Material, Is.EqualTo(new Material()));
    }

    [Test]
    public void Constructor_SetsParent_ToNull()
    {
        _shape = new ExampleShape();

        Assert.That(_shape.Parent, Is.EqualTo(null));
    }
}

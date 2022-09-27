using System;
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

    [Test]
    public void WorldToObject_ReturnsInverseOfTransformationTimesPoint_WhenParentNull()
    {
        _shape = new ExampleShape();
        _shape.Transform = new TMatrix().Translation(1, 2, 3);
        TMatrix transform = _shape.Transform;
        TMatrix inverse = transform.Inverse();
        var point = new TPoint(2, 3, 4);

        TPoint result = _shape.WorldToObject(point);
        TPoint expectedResult = inverse * point;

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void WorldToObject_AppliesParentTransformation_WhenParentNotNull()
    {
        var group1 = new Group
        {
            Transform = new TMatrix().RotationY(Math.PI / 2)
        };
        var group2 = new Group
        {
            Transform = new TMatrix().Scaling(2, 2, 2)
        };
        group1.Add(group2);
        _shape = new Sphere();
        _shape.Transform = new TMatrix().Translation(5, 0, 0);
        group2.Add(_shape);

        TPoint result = _shape.WorldToObject(new TPoint(-2, 0, -10));

        Assert.That(result, Is.EqualTo(new TPoint(0, 0, -1)));
    }

    [Test]
    public void NormalToWorld_ReturnsNoramlWithParentTransformationApplied_WhenParentNotNull()
    {
        var group1 = new Group
        {
            Transform = new TMatrix().RotationY(Math.PI / 2)
        };
        var group2 = new Group
        {
            Transform = new TMatrix().Scaling(1, 2, 3)
        };
        group1.Add(group2);
        _shape = new Sphere();
        _shape.Transform = new TMatrix().Translation(5, 0, 0);
        group2.Add(_shape);

        TVector result = _shape.NormalToWorld(new TVector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));

        Assert.That(result, Is.EqualTo(new TVector(0.2857, 0.4286, -0.8571)));
    }

    [Test]
    public void Normal_ReturnsNormalVector_OfObjectInGroup()
    {
        var group1 = new Group
        {
            Transform = new TMatrix().RotationY(Math.PI / 2)
        };
        var group2 = new Group
        {
            Transform = new TMatrix().Scaling(1, 2, 3)
        };
        group1.Add(group2);
        _shape = new Sphere();
        _shape.Transform = new TMatrix().Translation(5, 0, 0);
        group2.Add(_shape);

        TVector result = _shape.Normal(new TPoint(1.7321, 1.1547, -5.5774));

        Assert.That(result, Is.EqualTo(new TVector(0.2857, 0.4286, -0.8571)));
    }
}

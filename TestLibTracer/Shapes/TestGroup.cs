using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestGroup
{
    private Group _shape;

    [Test]
    public void Constructor_SetShapes_ToEmptyList()
    {
        _shape = new Group();

        Assert.That(_shape.Shapes.Count, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_SetsTransform_ToIdentityMatrix()
    {
        _shape = new Group();

        Assert.That(_shape.Transform, Is.EqualTo(new TMatrix()));
    }

    [Test]
    public void Add_SetsParent_ToPassedInShape()
    {
        _shape = new Group();
        var child = new Group();

        _shape.Add(child);

        Assert.That(child.Parent, Is.EqualTo(_shape));
    }

    [Test]
    public void Add_AddsPassedInShape_ToShapes()
    {
        _shape = new Group();
        var child = new Group();

        _shape.Add(child);

        Assert.That(_shape.Shapes.Contains(child), Is.True);
    }

    [Test]
    public void Intersects_ReturnsEmptyList_WhenGroupEmpty()
    {
        _shape = new Group();
        var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void Intersect_ReturnsFourHits_WhenTwoOfThreeShperesHit()
    {
        _shape = new Group();
        var sphere1 = new Sphere();
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Translation(0, 0, -3)
        };
        var sphere3 = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        _shape.Add(sphere1);
        _shape.Add(sphere2);
        _shape.Add(sphere3);
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result.Count, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetsFirstElementToSphere2_WhenTwoOfThreeShperesHit()
    {
        _shape = new Group();
        var sphere1 = new Sphere();
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Translation(0, 0, -3)
        };
        var sphere3 = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        _shape.Add(sphere1);
        _shape.Add(sphere2);
        _shape.Add(sphere3);
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result[0].Shape, Is.EqualTo(sphere2));
    }

    [Test]
    public void Intersect_SetsSecondElementToSphere2_WhenTwoOfThreeShperesHit()
    {
        _shape = new Group();
        var sphere1 = new Sphere();
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Translation(0, 0, -3)
        };
        var sphere3 = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        _shape.Add(sphere1);
        _shape.Add(sphere2);
        _shape.Add(sphere3);
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result[1].Shape, Is.EqualTo(sphere2));
    }

    [Test]
    public void Intersect_SetsThirdElementToSphere1_WhenTwoOfThreeShperesHit()
    {
        _shape = new Group();
        var sphere1 = new Sphere();
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Translation(0, 0, -3)
        };
        var sphere3 = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        _shape.Add(sphere1);
        _shape.Add(sphere2);
        _shape.Add(sphere3);
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result[2].Shape, Is.EqualTo(sphere1));
    }

    [Test]
    public void Intersect_SetsFourthElementToSphere1_WhenTwoOfThreeShperesHit()
    {
        _shape = new Group();
        var sphere1 = new Sphere();
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Translation(0, 0, -3)
        };
        var sphere3 = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        _shape.Add(sphere2);
        _shape.Add(sphere3);
        _shape.Add(sphere1);
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result[3].Shape, Is.EqualTo(sphere1));
    }

    [Test]
    public void Intersect_AppliesGroupAndChildTransformation()
    {
        _shape = new Group
        {
            Transform = new TMatrix().Scaling(2, 2, 2)
        };
        var sphere1 = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        _shape.Add(sphere1);
        var ray = new TRay(new TPoint(10, 0, -10), new TVector(0, 0, 1));

        IList<Intersection> result = _shape.Intersects(ray);

        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void BoundsMinimum_ReturnsPositiveInfinity_WhenGroupEmpty()
    {
        _shape = new Group();

        var expectedResult = new TPoint(Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity);

        Assert.That(_shape.Bounds.Minimum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMaximum_ReturnsNegativeInfinity_WhenGroupEmpty()
    {
        _shape = new Group();

        var expectedResult = new TPoint(Double.NegativeInfinity, Double.NegativeInfinity, Double.NegativeInfinity);

        Assert.That(_shape.Bounds.Maximum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMinimum_ReturnsMinimum_OfSingleUntransformedObject()
    {
        _shape = new Group();
        _shape.Add(new Sphere());

        var expectedResult = new TPoint(-1, -1, -1);

        Assert.That(_shape.Bounds.Minimum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMaximum_ReturnsMaximum_OfSingleUntransformedObject()
    {
        _shape = new Group();
        _shape.Add(new Sphere());

        var expectedResult = new TPoint(1, 1, 1);

        Assert.That(_shape.Bounds.Maximum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMinimum_ReturnsMinimum_OfSingleTransformedObject()
    {
        _shape = new Group();
        var sphere = new Sphere
        {
            Transform = new TMatrix().Scaling(2, 3, 4)
        };
        _shape.Add(sphere);

        var expectedResult = new TPoint(-2, -3, -4);

        Assert.That(_shape.Bounds.Minimum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMaximum_ReturnsMaximum_OfSingleTransformedObject()
    {
        _shape = new Group();
        var sphere = new Sphere
        {
            Transform = new TMatrix().Scaling(2, 3, 4)
        };
        _shape.Add(sphere);

        var expectedResult = new TPoint(2, 3, 4);

        Assert.That(_shape.Bounds.Maximum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMinimum_ReturnsMinimum_OfMultipleObjects()
    {
        _shape = new Group();
        var sphere = new Sphere
        {
            Transform = new TMatrix().Scaling(2, 3, 4)
        };
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Scaling(4, 2, 5)
        };
        _shape.Add(sphere);
        _shape.Add(sphere2);

        var expectedResult = new TPoint(-4, -3, -5);

        Assert.That(_shape.Bounds.Minimum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BoundsMaximum_ReturnsMaximum_OfMultipleObjects()
    {
        _shape = new Group();
        var sphere = new Sphere
        {
            Transform = new TMatrix().Scaling(2, 3, 4)
        };
        var sphere2 = new Sphere
        {
            Transform = new TMatrix().Scaling(4, 2, 5)
        };
        _shape.Add(sphere);
        _shape.Add(sphere2);

        var expectedResult = new TPoint(4, 3, 5);

        Assert.That(_shape.Bounds.Maximum, Is.EqualTo(expectedResult));
    }
}

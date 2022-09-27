using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestSphere
{
    private Sphere _sphere;

    [Test]
    public void Intersects_ReturnsTwoElements_WhenRayPassesThroughSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_Returns_4InFirstElement()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersects_Returns_ShapeInFirstElement()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Shape, Is.EqualTo(_sphere));
    }

    [Test]
    public void Intersects_Returns_6InSecondElement()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersects_Returns_ShapeInSecondElement()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Shape, Is.EqualTo(_sphere));
    }

    [Test]
    public void Intersects_ReturnsTwoElements_WhenRayAtTangentToSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 1, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_Returns5InFirstElement_WhenRayAtTagentToSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 1, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Time, Is.EqualTo(5));
    }

    [Test]
    public void Intersects_Returns5InSecondElement_WhenRayAtTagentToSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 1, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[1].Time, Is.EqualTo(5));
    }

    [Test]
    public void Intersects_ReturnsNoElements_WhenRayMissesSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 2, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection.Count, Is.EqualTo(0));
    }

    [Test]
    public void Intersects_ReturnsTwoElements_WhenRayStartsWithinSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_ReturnsMinus1InFirstElement_WhenRayStartsWithinSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Time, Is.EqualTo(-1));
    }

    [Test]
    public void Intersects_Returns1InSecondElement_WhenRayStartsWithinSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[1].Time, Is.EqualTo(1));
    }

    [Test]
    public void Intersects_ReturnsTwoElements_WhenRayStartsBeyondSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_ReturnsMinus6InFirstElement_WhenRayStartsBeyondSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Time, Is.EqualTo(-6));
    }

    [Test]
    public void Intersects_ReturnsMinus4InSecondElement_WhenRayStartsBeyondSphere()
    {
        _sphere = new Sphere();
        var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[1].Time, Is.EqualTo(-4));
    }

    [Test]
    public void Intersect_ScalesRay_WhenCalculatingResult()
    {
        _sphere = new Sphere
        {
            Transform = new TMatrix().Scaling(2, 2, 2)
        };
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection[0].Time, Is.EqualTo(3));
        Assert.That(intersection[1].Time, Is.EqualTo(7));
    }

    [Test]
    public void Intersect_TranslatesRay_WhenCalculatingResult()
    {
        _sphere = new Sphere
        {
            Transform = new TMatrix().Translation(5, 0, 0)
        };
        var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersection = _sphere.Intersects(ray);

        Assert.That(intersection.Count, Is.EqualTo(0));
    }

    [Test]
    public void Normal_Returns_InstanceOfVector()
    {
        _sphere = new Sphere();

        Assert.That(_sphere.Normal(new TPoint(0, 0, 0)), Is.InstanceOf<TVector>());
    }

    [Test]
    public void Normal_Returns_PointOnXAxis()
    {
        _sphere = new Sphere();

        TVector result = _sphere.Normal(new TPoint(1, 0, 0));

        var expectedResult = new TVector(1, 0, 0);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Normal_Returns_PointOnYAxis()
    {
        _sphere = new Sphere();

        TVector result = _sphere.Normal(new TPoint(0, 1, 0));

        var expectedResult = new TVector(0, 1, 0);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Normal_Returns_PointOnZAxis()
    {
        _sphere = new Sphere();

        TVector result = _sphere.Normal(new TPoint(0, 0, 1));

        var expectedResult = new TVector(0, 0, 1);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Normal_Returns_PointOnSphereAtNonAxialPosition()
    {
        _sphere = new Sphere();

        TVector result = _sphere.Normal(new TPoint(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));

        var expectedResult = new TVector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Normal_Returns_NormalisedVector()
    {
        _sphere = new Sphere();

        TVector result = _sphere.Normal(new TPoint(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));

        Assert.That(result.Normalise(), Is.EqualTo(result));
    }

    [Test]
    public void Normal_ReturnsNormal_WhenSphereTranslated()
    {
        _sphere = new Sphere
        {
            Transform = new TMatrix().Translation(0, 1, 0)
        };

        TVector result = _sphere.Normal(new TPoint(0, 1.70711, -0.70711));

        var expectedResult = new TVector(0, 0.70711, -0.70711);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Normal_ReturnsNormal_WhenSphereTransformed()
    {
        _sphere = new Sphere
        {
            Transform = new TMatrix().RotationZ(Math.PI / 5).Scaling(1, 0.5, 1)
        };

        TVector result = _sphere.Normal(new TPoint(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));

        var expectedResult = new TVector(0, 0.97014, -0.24254);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Equals_ReturnsTrue_WhenMaterialEqualToOtherMaterial()
    {
        Shape sphere1 = new Sphere();
        Shape sphere2 = new Sphere();

        sphere1.Material.Ambient = 12;
        sphere2.Material.Ambient = 12;

        Assert.That(sphere1, Is.EqualTo(sphere2));
    }

    [Test]
    public void Equals_ReturnsTrue_WhenTransformEqualToOtherTransform()
    {
        Shape sphere1 = new Sphere();
        Shape sphere2 = new Sphere();

        sphere1.Transform = new TMatrix().RotationX(123);
        sphere2.Transform = new TMatrix().RotationX(123);

        Assert.That(sphere1, Is.EqualTo(sphere2));
    }

    [Test]
    public void Equals_ReturnsFalse_WhenOtherIsObjectAndNull()
    {
        Shape sphere1 = new Sphere();
        Object sphere2 = null;

        Assert.That(sphere1.Equals(sphere2), Is.False);
    }

    [Test]
    public void Equals_ReturnsTrue_WhenOtherIsObject()
    {
        Shape sphere1 = new Sphere();
        Object sphere2 = new Sphere();

        Assert.That(sphere1.Equals(sphere2), Is.True);
    }

    [Test]
    public void Equals_ReturnsTrue_WhenOtherIsObjectAndSameReference()
    {
        Shape sphere1 = new Sphere();
        Object sphere2 = sphere1;

        Assert.That(sphere1.Equals(sphere2), Is.True);
    }

    [Test]
    public void Equals_ReturnsFalse_WhenOtherIsShapeAndNull()
    {
        Shape sphere1 = new Sphere();
        Shape sphere2 = null;

        Assert.That(sphere1.Equals(sphere2), Is.False);
    }

    [Test]
    public void Equals_ReturnsTrue_WhenOtherIsShapeAndSameReference()
    {
        Shape sphere1 = new Sphere();
        Shape sphere2 = sphere1;

        Assert.That(sphere1.Equals(sphere2), Is.True);
    }

    [Test]
    public void Equals_ReturnsFalse_WhenOtherIsDifferentType()
    {
        Shape sphere1 = new Sphere();
        var sphere2 = new TVector(1, 1, 1);

        Assert.That(sphere1.Equals(sphere2), Is.False);
    }

    [Test]
    public void Hashcode_ReturnsSameValue_WhenSetToSameValues()
    {
        Shape sphere1 = new Sphere();
        Shape sphere2 = new Sphere();

        Assert.That(sphere1.GetHashCode(), Is.EqualTo(sphere2.GetHashCode()));
    }
}
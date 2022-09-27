using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestPlane
{
    private Plane _plane;

    [Test]
    public void Normal_ReturnsConstantValue_AtAllPoints()
    {
        _plane = new Plane();

        TVector normal2 = _plane.Normal(new TPoint(10, 0, -10));

        Assert.That(normal2, Is.EqualTo(new TVector(0, 1, 0)));
    }

    [Test]
    public void Normal_ReturnsSameValue_AtAllPoints()
    {
        _plane = new Plane();

        TVector normal1 = _plane.Normal(new TPoint(0, 0, 0));
        TVector normal2 = _plane.Normal(new TPoint(10, 0, -10));

        Assert.That(normal1, Is.EqualTo(normal2));
    }

    [Test]
    public void Intersect_ReturnsEmptyList_WhenRayParallelToPlane()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, 10, 0), new TVector(0, 0, 1));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(0));
    }

    [Test]
    public void Intersect_ReturnsEmptyList_WhenRayCoplanarToPlane()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(0));
    }

    [Test]
    public void Intersect_ReturnsDoublePoint_WhenRayIntersectsFromAbove()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, 1, 0), new TVector(0, -1, 0));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(1));
    }

    [Test]
    public void Intersect_ReturnsIntersectTimeOfOne_WhenRayIntersectsFromAbove()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, 1, 0), new TVector(0, -1, 0));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(1));
    }

    [Test]
    public void Intersect_ReturnsPlane_WhenRayIntersectsFromAbove()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, 1, 0), new TVector(0, -1, 0));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects[0].Shape, Is.EqualTo(_plane));
    }

    [Test]
    public void Intersect_ReturnsDoublePoint_WhenRayIntersectsFromBelow()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, -1, 0), new TVector(0, 1, 0));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(1));
    }

    [Test]
    public void Intersect_ReturnsIntersectTimeOfOne_WhenRayIntersectsFromBelow()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, -1, 0), new TVector(0, 1, 0));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(1));
    }

    [Test]
    public void Intersect_ReturnsPlane_WhenRayIntersectsFromBelow()
    {
        _plane = new Plane();
        var ray = new TRay(new TPoint(0, -1, 0), new TVector(0, 1, 0));

        IList<Intersection> intersects = _plane.Intersects(ray);

        Assert.That(intersects[0].Shape, Is.EqualTo(_plane));
    }
}
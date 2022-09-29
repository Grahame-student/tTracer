using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestTriangle
{
    private Triangle _triangle;

    [Test]
    public void Constructor_SetsPoint1_ToPassedInValue()
    {
        var point1 = new TPoint( 0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint( 1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);

        Assert.That(_triangle.Point1, Is.EqualTo(point1));
    }

    [Test]
    public void Constructor_SetsPoint2_ToPassedInValue()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);

        Assert.That(_triangle.Point2, Is.EqualTo(point2));
    }

    [Test]
    public void Constructor_SetsPoint3_ToPassedInValue()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);

        Assert.That(_triangle.Point3, Is.EqualTo(point3));
    }

    [Test]
    public void Constructor_SetsEdge1_ToPoint2MinusPoint1()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);

        var expectedResult = new TVector(-1, -1, 0);

        Assert.That(_triangle.Edge1, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Constructor_SetsEdge2_ToPoint3MinusPoint1()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);

        var expectedResult = new TVector(1, -1, 0);

        Assert.That(_triangle.Edge2, Is.EqualTo(expectedResult));
    }

    [TestCase(-0.5, 0.75, 0)]
    [TestCase(-0.5, 0.75, 0)]
    [TestCase( 0,   0.5,  0)]
    public void Normal_Returns_NormalisedCrossProductOfEdge2AndEdge1(Double x, Double y, Double z)
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);

        var expectedResult = new TVector(0, 0, -1);

        Assert.That(_triangle.Normal(new TPoint(x, y, z)), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Intersect_ReturnsNoHits_WhenRayParallelToTriangle()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);
        var ray = new TRay(new TPoint(0, -1, -2), new TVector(0, 1, 0));

        IList<Intersection> result = _triangle.Intersects(ray);

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [TestCase( 1,  1, -2)] // P1->P3
    [TestCase(-1,  1, -2)] // P1->P2
    [TestCase( 0, -1, -2)] // P2->P3
    public void Intersect_ReturnsNoHits_WhenRayPassesOverEdge(Double ox, Double oy, Double oz)
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);
        var ray = new TRay(new TPoint(ox, oy, oz), new TVector(0, 0, 1));

        IList<Intersection> result = _triangle.Intersects(ray);

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void Intersect_ReturnsOneHit_WhenRayHitsTriangle()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);
        var ray = new TRay(new TPoint(0, 0.5, -2), new TVector(0, 0, 1));

        IList<Intersection> result = _triangle.Intersects(ray);

        Assert.That(result.Count, Is.EqualTo(1));
    }

    [Test]
    public void Intersect_SetsHitOneTimeToTwo_WhenRayHitsTriangle()
    {
        var point1 = new TPoint(0, 1, 0);
        var point2 = new TPoint(-1, 0, 0);
        var point3 = new TPoint(1, 0, 0);
        _triangle = new Triangle(point1, point2, point3);
        var ray = new TRay(new TPoint(0, 0.5, -2), new TVector(0, 0, 1));

        IList<Intersection> result = _triangle.Intersects(ray);

        Assert.That(result[0].Time, Is.EqualTo(2));
    }
}

using libTracer.Common;
using libTracer.Shapes;

using NUnit.Framework;

using System;

namespace TestLibTracer.Shapes;

internal class TestBounds
{
    private Bounds _bounds;

    [Test]
    public void Intersected_ReturnsTrue_WhenRayIntersectsPositiveXFace()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(5, 0.5, 0), new TVector(-1, 0, 0));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [Test]
    public void Intersected_ReturnsTrue_WhenRayIntersectsNegativeXFace()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(-5, 0.5, 0), new TVector(1, 0, 0));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [Test]
    public void Intersected_ReturnsTrue_WhenRayIntersectsPositiveYFace()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(0.5, 5, 0), new TVector(0, -1, 0));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [Test]
    public void Intersected_ReturnsTrue_WhenRayIntersectsNegativeYFace()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(0.5, -5, 0), new TVector(0, 1, 0));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [Test]
    public void Intersected_ReturnsTrue_WhenRayIntersectsPositiveZFace()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(0.5, 0, 5), new TVector(0, 0, -1));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [Test]
    public void Intersected_ReturnsTrue_WhenRayIntersectsNegativeZFace()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(0.5, 0, -5), new TVector(0, 0, 1));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [Test]
    public void Intersected_ReturnsTrue_WhenRayInsideBounds()
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(0, 0.5, 0), new TVector(0, 0, 1));

        Assert.That(_bounds.Intersected(ray), Is.True);
    }

    [TestCase(-2, 0, 0, 0.2673, 0.5345, 0.8018)]
    [TestCase(0, -2, 0, 0.8018, 0.2673, 0.5345)]
    [TestCase(0, 0, -2, 0.5345, 0.8018, 0.2673)]
    [TestCase(2, 0, 2, 0, 0, -1)]
    [TestCase(0, 2, 2, 0, -1, 0)]
    [TestCase(2, 2, 0, -1, 0, 0)]
    public void Intersected_ReturnsTrue_WhenRayMisses(Double ox, Double oy, Double oz, Double dx, Double dy, Double dz)
    {
        _bounds = new Bounds(
            new TPoint(-1, -1, -1),
            new TPoint(1, 1, 1));
        var ray = new TRay(new TPoint(ox, oy, oz), new TVector(dx, dy, dz));

        Assert.That(_bounds.Intersected(ray), Is.False);
    }

}
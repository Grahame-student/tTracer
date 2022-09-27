using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestCube
{
    private Cube _cube;

    [Test]
    public void Intersect_Returns2Hits_WhenRayIntersectsPositiveXFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(5, 0.5, 0), new TVector(-1, 0, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayIntersectsPositiveXFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(5, 0.5, 0), new TVector(-1, 0, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayIntersectsNegativeXFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(5, 0.5, 0), new TVector(-1, 0, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersect_Returns2Hits_WhenRayIntersectsNegativeXFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(-5, 0.5, 0), new TVector(1, 0, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayIntersectsNegativeXFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(-5, 0.5, 0), new TVector(1, 0, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayIntersectsPositiveXFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(-5, 0.5, 0), new TVector(1, 0, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersect_Returns2Hits_WhenRayIntersectsPositiveYFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 5, 0), new TVector(0, -1, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayIntersectsPositiveYFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 5, 0), new TVector(0, -1, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayIntersectsNegativeYFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 5, 0), new TVector(0, -1, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersect_Returns2Hits_WhenRayIntersectsNegativeYFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, -5, 0), new TVector(0, 1, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayIntersectsNegativeYFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, -5, 0), new TVector(0, 1, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayIntersectsPositiveYFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, -5, 0), new TVector(0, 1, 0));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersect_Returns2Hits_WhenRayIntersectsPositiveZFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 0, 5), new TVector(0, 0, -1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayIntersectsPositiveZFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 0, 5), new TVector(0, 0, -1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayIntersectsNegativeZFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 0, 5), new TVector(0, 0, -1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersect_Returns2Hits_WhenRayIntersectsNegativeZFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayIntersectsNegativeZFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayIntersectsPositiveZFace()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0.5, 0, -5), new TVector(0, 0, 1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(6));
    }

    [Test]
    public void Intersect_Returns2Hits_WhenRayInsideCube()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0, 0.5, 0), new TVector(0, 0, 1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersect_SetFirstHitTimeTo4_WhenRayInsideCube()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0, 0.5, 0), new TVector(0, 0, 1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(-1));
    }

    [Test]
    public void Intersect_SetSecondHitTimeTo6_WhenRayInsideCube()
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(0, 0.5, 0), new TVector(0, 0, 1));

        IList<Intersection> intersects = _cube.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(1));
    }

    [TestCase(-2, 0, 0, 0.2673, 0.5345, 0.8018)]
    [TestCase(0, -2, 0, 0.8018, 0.2673, 0.5345)]
    [TestCase(0, 0, -2, 0.5345, 0.8018, 0.2673)]
    [TestCase(2, 0, 2,  0, 0, -1)]
    [TestCase(0, 2, 2,  0, -1, 0)]
    [TestCase(2, 2, 0,  -1, 0, 0)]
    public void Intersect_Returns0Hits_WhenRayMisses(Double ox, Double oy, Double oz, Double dx, Double dy, Double dz)
    {
        _cube = new Cube();
        var ray = new TRay(new TPoint(ox, oy, oz), new TVector(dx, dy, dz));

        IList<Intersection> interesctions = _cube.Intersects(ray);

        Assert.That(interesctions.Count, Is.EqualTo(0));
    }

    [TestCase( 1.0,  0.5, -0.8,  1, 0,  0)]
    [TestCase(-1.0, -0.2,  0.9, -1, 0,  0)]
    [TestCase( 0.4,  1.0, -0.1,  0, 1,  0)]
    [TestCase( 0.3, -1.0, -0.7,  0,-1,  0)]
    [TestCase(-0.6,  0.3,  1.0,  0, 0,  1)]
    [TestCase( 0.4,  0.4, -1.0,  0, 0, -1)]
    [TestCase( 1.0,  1.0,  1.0,  1, 0,  0)]
    [TestCase(-1.0, -1.0, -1.0, -1, 0,  0)]
    public void NormalAt_ReturnsNormalVector_AtSpecifiedPoint(Double px, Double py, Double pz, Double nx, Double ny, Double nz)
    {
        _cube = new Cube();

        TVector normal = _cube.Normal(new TPoint(px, py, pz));

        Assert.That(normal, Is.EqualTo(new TVector(nx, ny, nz)));
    }
}
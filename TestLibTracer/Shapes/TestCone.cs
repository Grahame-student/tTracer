using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Shapes;

internal class TestCone
{
    private Cone _shape;

    [Test]
    public void Constructor_SetMinimum_ToMinusInfinity()
    {
        _shape = new Cone();

        Assert.That(_shape.Minimum, Is.EqualTo(Double.NegativeInfinity));
    }

    [Test]
    public void Constructor_SetMaximum_ToPositiveInfinity()
    {
        _shape = new Cone();

        Assert.That(_shape.Maximum, Is.EqualTo(Double.PositiveInfinity));
    }

    [Test]
    public void Constructor_SetsCapped_ToFalse()
    {
        _shape = new Cone();

        Assert.That(_shape.Closed, Is.EqualTo(false));
    }

    [Test]
    public void Intersects_ReturnsTwoHits_WhenRayHitsConeTip()
    {
        _shape = new Cone();
        TVector direction = new TVector(0, 0, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_SetsHit0Time_WhenRayHitsConeTip()
    {
        _shape = new Cone();
        TVector direction = new TVector(0, 0, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(5).Within(Constants.EPSILON));
    }

    [Test]
    public void Intersects_SetsHit1Time_WhenRayHitsConeTip()
    {
        _shape = new Cone();
        TVector direction = new TVector(0, 0, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(5).Within(Constants.EPSILON));
    }

    [Test]
    public void Intersects_ReturnsTwoHits_WhenRayHitsConeTipAtAngle()
    {
        _shape = new Cone();
        TVector direction = new TVector(1, 1, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_SetsHit0Time_WhenRayHitsConeTipAtAngle()
    {
        _shape = new Cone();
        TVector direction = new TVector(1, 1, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(8.66025).Within(Constants.EPSILON));
    }

    [Test]
    public void Intersects_SetsHit1Time_WhenRayHitsConeTipAtAngle()
    {
        _shape = new Cone();
        TVector direction = new TVector(1, 1, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(8.66025).Within(Constants.EPSILON));
    }

    [Test]
    public void Intersects_ReturnsTwoHits_WhenRayHitsConeAtAngle()
    {
        _shape = new Cone();
        TVector direction = new TVector(-0.5, -1, 1).Normalise();
        var ray = new TRay(new TPoint(1, 1, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    [Test]
    public void Intersects_SetsHit0Time_WhenRayHitsConeAtAngle()
    {
        _shape = new Cone();
        TVector direction = new TVector(-0.5, -1, 1).Normalise();
        var ray = new TRay(new TPoint(1, 1, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(4.55006).Within(Constants.EPSILON));
    }

    [Test]
    public void Intersects_SetsHit1Time_WhenRayHitsConeAtAngle()
    {
        _shape = new Cone();
        TVector direction = new TVector(-0.5, -1, 1).Normalise();
        var ray = new TRay(new TPoint(1, 1, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[1].Time, Is.EqualTo(49.44994).Within(Constants.EPSILON));
    }

    [Test]
    public void Intersects_ReturnsOneHit_WhenRayParallelToHalfOfCone()
    {
        _shape = new Cone();
        TVector direction = new TVector(0, 1, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -1), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(1));
    }

    [Test]
    public void Intersects_SetsHit0Time_WhenRayParrallelToHalfOfCone()
    {
        _shape = new Cone();
        TVector direction = new TVector(0, 1, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -1), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects[0].Time, Is.EqualTo(0.35355).Within(Constants.EPSILON));
    }

    /// <summary>
    ///      ^
    /// ---- |
    /// \  / |
    ///  \/  |
    ///  /\  |
    /// /  \ |
    /// ---- |
    ///      |
    /// </summary>
    [Test]
    public void Intersect_ReturnsZeroHits_WhenRayParallelToConeWithEndCap()
    {
        _shape = new Cone
        {
            Closed = true,
            Minimum = -0.5,
            Maximum = 0.5
        };
        TVector direction = new TVector(0, 1, 0).Normalise();
        var ray = new TRay(new TPoint(0, 0, -5), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(0));
    }

    /// <summary>
    ///        ^
    ///       /
    ///   ---/- 
    ///   \ / / 
    ///    / /  
    ///   / /\  
    ///  / /  \ 
    /// /  ---- 
    ///      
    /// </summary>
    [Test]
    public void Intersect_ReturnsTwoHits_WhenRayHitsConeWithEndcap()
    {
        _shape = new Cone
        {
            Closed = true,
            Minimum = -0.5,
            Maximum = 0.5
        };
        TVector direction = new TVector(0, 1, 1).Normalise();
        var ray = new TRay(new TPoint(0, 0, -0.25), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(2));
    }

    /// <summary>
    ///   ^
    /// --|--
    /// \ | /
    ///  \|/
    ///  /|\
    /// / | \
    /// --|--
    ///   |
    /// </summary>
    [Test]
    public void Intersect_ReturnsFourHits_WhenRayPassesVertivallyThroughCenterOfCone()
    {
        _shape = new Cone
        {
            Closed = true,
            Minimum = -0.5,
            Maximum = 0.5
        };
        TVector direction = new TVector(0, 1, 0).Normalise();
        var ray = new TRay(new TPoint(0, 0, -0.25), direction);

        IList<Intersection> intersects = _shape.Intersects(ray);

        Assert.That(intersects.Count, Is.EqualTo(4));
    }

    [Test]
    public void Normal_ReturnsZeroVector_AtConeTip()
    {
        _shape = new Cone();

        TVector normal = _shape.Normal(new TPoint(0, 0, 0));

        Assert.That(normal, Is.EqualTo(new TVector(0, 0, 0)));
    }

    [Test]
    public void Normal_ReturnsNormal_WhenConeHitFromAbove()
    {
        _shape = new Cone();

        TVector normal = _shape.Normal(new TPoint(1, 1, 1));

        Assert.That(normal, Is.EqualTo(new TVector(1, -Math.Sqrt(2), 1).Normalise()));
    }

    [Test]
    public void Normal_ReturnsNormal_WhenConeHitFromBelowInXY()
    {
        _shape = new Cone();

        TVector normal = _shape.Normal(new TPoint(-1, -1, 0));

        Assert.That(normal, Is.EqualTo(new TVector(-1, 1, 0).Normalise()));
    }
}
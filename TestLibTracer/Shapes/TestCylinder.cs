using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Shapes
{
    internal class TestCylinder
    {
        private Cylinder _cylinder;

        [Test]
        public void Constructor_SetMinimum_ToMinusInfinity()
        {
            _cylinder = new Cylinder();

            Assert.That(_cylinder.Minimum, Is.EqualTo(Double.NegativeInfinity));
        }

        [Test]
        public void Constructor_SetMaximum_ToPositiveInfinity()
        {
            _cylinder = new Cylinder();

            Assert.That(_cylinder.Maximum, Is.EqualTo(Double.PositiveInfinity));
        }

        [Test]
        public void Constructor_SetsCapped_ToFalse()
        {
            _cylinder = new Cylinder();

            Assert.That(_cylinder.Closed, Is.EqualTo(false));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayIsParallelToSurfaceOfCylinderInPositiveYAxis()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 1, 0).Normalise();
            var ray = new TRay(new TPoint(1, 0, 0), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayIsParallelToInsideSurfaceOfCylinderInPositiveYAxis()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 1, 0).Normalise();
            var ray = new TRay(new TPoint(0, 0, 0), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayIsOutsideCylinderAndOrientedAwayFroMFromAllAxes()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(1, 1, 1).Normalise();
            var ray = new TRay(new TPoint(0, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayAtTangentToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(1, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_SetsHit0TimeTo5_WhenRayAtTangentToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(1, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections[0].Time, Is.EqualTo(5));
        }

        [Test]
        public void Intersect_SetsHit1TimeTo5_WhenRayAtTangentToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(1, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections[1].Time, Is.EqualTo(5));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayStrikePerpendicularToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_SetsHit0TimeTo4_WhenRayStrikePerpendicularToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections[0].Time, Is.EqualTo(4));
        }

        [Test]
        public void Intersect_SetsHit1TimeTo6_WhenRayStrikePerpendicularToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections[1].Time, Is.EqualTo(6));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayStrikeAtAngleToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0.1, 1, 1).Normalise();
            var ray = new TRay(new TPoint(0.5, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_SetsHit0TimeTo4_WhenRayStrikeAtAngleToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0.1, 1, 1).Normalise();
            var ray = new TRay(new TPoint(0.5, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(Math.Abs(intersections[0].Time - 6.80798) , Is.LessThan(Constants.EPSILON));
        }

        [Test]
        public void Intersect_SetsHit1TimeTo6_WhenRayStrikeAtAngleToCylinder()
        {
            _cylinder = new Cylinder();
            TVector direction = new TVector(0.1, 1, 1).Normalise();
            var ray = new TRay(new TPoint(0.5, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(Math.Abs(intersections[1].Time - 7.08872), Is.LessThan(Constants.EPSILON));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayEscapesCylinderWithoutHittingEdge()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2
            };
            TVector direction = new TVector(0.1, 1, 0).Normalise();
            var ray = new TRay(new TPoint(0, 1.5, 0), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayPassesAboveCylinder()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2
            };
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 3, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayPassesBelowCylinder()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2
            };
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 0, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayAtMaximumOfCylinder()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2
            };
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 2, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayAtMinimumOfCylinder()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2
            };
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 1, -5), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersect_ReturnsZeroHits_WhenRayBetweenMinimumAndMaximumOfCylinder()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2
            };
            TVector direction = new TVector(0, 0, 1).Normalise();
            var ray = new TRay(new TPoint(0, 1.5, -2), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayShinesDownThroughCenterOfClosedCylinder()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };
            TVector direction = new TVector(0, -1, 0).Normalise();
            var ray = new TRay(new TPoint(0, 3, 0), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayShinesDownThrougClosedCylinderAtDiagonal()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };
            TVector direction = new TVector(0, -1, 2).Normalise();
            var ray = new TRay(new TPoint(0, 3, -2), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayShinesUpThrougClosedCylinderAtDiagonal()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };
            TVector direction = new TVector(0, 1, 2).Normalise();
            var ray = new TRay(new TPoint(0, 0, -2), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayShinesDownThrougClosedCylinderHittingJoinOfCapAndWall()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };
            TVector direction = new TVector(0, -1, 1).Normalise();
            var ray = new TRay(new TPoint(0, 4, -2), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersect_ReturnsTwoHits_WhenRayShinesUpThrougClosedCylinderHittingJoinOfCapAndWall()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };
            TVector direction = new TVector(0, 1, 1).Normalise();
            var ray = new TRay(new TPoint(0, -1, -2), direction);

            IList<Intersection> intersections = _cylinder.Intersects(ray);

            Assert.That(intersections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Normal_ReturnsOutwardFacingVector_AtPositiveXSurface()
        {
            _cylinder = new Cylinder();
            
            Assert.That(_cylinder.Normal(new TPoint(1, 0, 0)), Is.EqualTo(new TVector(1, 0, 0)));
        }

        [Test]
        public void Normal_ReturnsOutwardFacingVector_AtNegativeZSurface()
        {
            _cylinder = new Cylinder();

            Assert.That(_cylinder.Normal(new TPoint(0, 5, -1)), Is.EqualTo(new TVector(0, 0, -1)));
        }

        [Test]
        public void Normal_ReturnsOutwardFacingVector_AtPositiveZSurface()
        {
            _cylinder = new Cylinder();

            Assert.That(_cylinder.Normal(new TPoint(0, -2, 1)), Is.EqualTo(new TVector(0, 0, 1)));
        }

        [Test]
        public void Normal_ReturnsOutwardFacingVector_AtNegativeXSurface()
        {
            _cylinder = new Cylinder();

            Assert.That(_cylinder.Normal(new TPoint(-1, 1, 0)), Is.EqualTo(new TVector(-1, 0, 0)));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenCenterOfBottomEndCapHit()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };

            TVector normal = _cylinder.Normal(new TPoint(0, 1, 0));

            Assert.That(normal, Is.EqualTo(new TVector(0, -1, 0)));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenXEdgeOfBottomEndCapHit()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };

            TVector normal = _cylinder.Normal(new TPoint(0.5, 1, 0));

            Assert.That(normal, Is.EqualTo(new TVector(0, -1, 0)));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenZEdgeOfBottomEndCapHit()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };

            TVector normal = _cylinder.Normal(new TPoint(0, 1, 0.5));

            Assert.That(normal, Is.EqualTo(new TVector(0, -1, 0)));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenCenterOfTopEndCapHit()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };

            TVector normal = _cylinder.Normal(new TPoint(0, 2, 0));

            Assert.That(normal, Is.EqualTo(new TVector(0, 1, 0)));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenXEdgeOfTopEndCapHit()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };

            TVector normal = _cylinder.Normal(new TPoint(0.5, 2, 0));

            Assert.That(normal, Is.EqualTo(new TVector(0, 1, 0)));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenZEdgeOfTopEndCapHit()
        {
            _cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };

            TVector normal = _cylinder.Normal(new TPoint(0, 2, 0.5));

            Assert.That(normal, Is.EqualTo(new TVector(0, 1, 0)));
        }
    }
}

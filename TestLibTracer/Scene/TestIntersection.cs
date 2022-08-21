using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Scene
{
    internal class TestIntersection
    {
        private const Double EARLIER_TIME = 120;
        private const Double SOME_TIME = 123;
        private const Double LATER_TIME = 130;

        private Intersection _intersection;

        [Test]
        public void Constructor_SetsTime_ToPassedInValue()
        {
            _intersection = new Intersection(SOME_TIME, new Sphere());

            Assert.That(_intersection.Time, Is.EqualTo(SOME_TIME));
        }

        [Test]
        public void Constructor_SetsShape_ToPassedInValue()
        {
            Shape shape = new Sphere();
            _intersection = new Intersection(SOME_TIME, shape);

            Assert.That(_intersection.Shape, Is.EqualTo(shape));
        }

        [Test]
        public void CompareTo_ReturnsLessThanZero_WhenOtherValueComesLater()
        {
            Shape shape = new Sphere();
            _intersection = new Intersection(SOME_TIME, shape);
            var other = new Intersection(LATER_TIME, shape);

            Assert.That(_intersection.CompareTo(other), Is.EqualTo(-1));
        }

        [Test]
        public void CompareTo_ReturnsGreateThanZero_WhenOtherValueComesBefore()
        {
            Shape shape = new Sphere();
            _intersection = new Intersection(SOME_TIME, shape);
            var other = new Intersection(EARLIER_TIME, shape);

            Assert.That(_intersection.CompareTo(other), Is.EqualTo(1));
        }

        [Test]
        public void CompareTo_ReturnsZero_WhenOtherValueSame()
        {
            Shape shape = new Sphere();
            _intersection = new Intersection(SOME_TIME, shape);
            var other = new Intersection(SOME_TIME, shape);

            Assert.That(_intersection.CompareTo(other), Is.EqualTo(0));
        }

        [Test]
        public void Hit_ReturnsFirstIntersectionWithLowestTime_WhenAllTimesPositive()
        {
            Shape shape = new Sphere();
            IList<Intersection> list = new List<Intersection>();
            list.Add(new Intersection(1, shape));
            list.Add(new Intersection(2, shape));
            list.Add(new Intersection(3, shape));

            Intersection result = Intersection.Hit(list);

            Assert.That(result.Time, Is.EqualTo(1));
        }

        [Test]
        public void Hit_ReturnsFirstIntersectionWithLowestTime_WhenSomeTimesNegative()
        {
            Shape shape = new Sphere();
            IList<Intersection> list = new List<Intersection>();
            list.Add(new Intersection(-1, shape));
            list.Add(new Intersection(2, shape));
            list.Add(new Intersection(3, shape));

            Intersection result = Intersection.Hit(list);

            Assert.That(result.Time, Is.EqualTo(2));
        }

        [Test]
        public void Hit_ReturnsNull_WhenAllTimesNegative()
        {
            Shape shape = new Sphere();
            IList<Intersection> list = new List<Intersection>();
            list.Add(new Intersection(-3, shape));
            list.Add(new Intersection(-2, shape));
            list.Add(new Intersection(-1, shape));

            Intersection result = Intersection.Hit(list);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void PrepareComputations_Returns_InstanceOfComputations()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result, Is.InstanceOf<Computations>());
        }

        [Test]
        public void PrepareComputations_SetsTime_ToIntersectionTime()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.Time, Is.EqualTo(_intersection.Time));
        }

        [Test]
        public void PrepareComputations_SetsObject_ToShape()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.Object, Is.EqualTo(shape));
        }

        [Test]
        public void PrepareComputations_SetsPoint_ToHitPosition()
        {
            var origin = new TPoint(0, 0, -5);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.Point, Is.EqualTo(new TPoint(0, 0, -1)));
        }

        [Test]
        public void PrepareComputations_SetsEyeV_ToOppositeOfRayDirection()
        {
            var origin = new TPoint(0, 0, -5);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.EyeV, Is.EqualTo(-direction));
        }

        [Test]
        public void PrepareComputations_SetsNormalV_ToNormalOfIntersection()
        {
            var origin = new TPoint(0, 0, -5);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.NormalV, Is.EqualTo(new TVector(0, 0, -1)));
        }

        [Test]
        public void PrepareComputations_SetsInsideToFalse_WhenRayOriginatesOutsideShape()
        {
            var origin = new TPoint(0, 0, -5);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.Inside, Is.False);
        }

        [Test]
        public void PrepareComputations_SetsInsideToTrue_WhenRayOriginatesInsideShape()
        {
            var origin = new TPoint(0, 0, 0);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere();
            _intersection = new Intersection(1, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.Inside, Is.True);
        }

        [Test]
        public void PrepareComputations_NegatesNormalV_WhenRayOriginatesInsideShape()
        {
            var origin = new TPoint(0, 0, 0);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere();
            _intersection = new Intersection(1, shape);

            Computations result = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(result.NormalV, Is.EqualTo(new TVector(0, 0, -1)));
        }

        [Test]
        public void PrepareComputations_SetsOverPoint_ToPointInsideShape()
        {
            var origin = new TPoint(0, 0, -5);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere
            {
                Transform = new TMatrix().Translation(0, 0, 1)
            };
            _intersection = new Intersection(1, shape);

            Computations comps = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(comps.OverPoint.Z, Is.LessThan(-Constants.EPSILON / 2));
        }

        [Test]
        public void PrepareComputations_SetsPointZ_ToGreaterThanOverPointZ()
        {
            var origin = new TPoint(0, 0, -5);
            var direction = new TVector(0, 0, 1);
            var ray = new TRay(origin, direction);
            Shape shape = new Sphere
            {
                Transform = new TMatrix().Translation(0, 0, 1)
            };
            _intersection = new Intersection(1, shape);

            Computations comps = _intersection.PrepareComputations(ray, new List<Intersection>{_intersection});

            Assert.That(comps.Point.Z, Is.GreaterThan(comps.OverPoint.Z));
        }

        [Test]
        public void PrepareComputations_SetsReflectVTo_DirectionOfReflection()
        {
            var shape = new Plane();
            var ray = new TRay(new TPoint(0, 1, -1), new TVector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            _intersection = new Intersection(Math.Sqrt(2), shape);

            Computations comps = _intersection.PrepareComputations(ray, new List<Intersection> { _intersection });

            Assert.That(comps.ReflectV, Is.EqualTo(new TVector(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2)));
        }

        [TestCase(0, 1.0, 1.5)]
        [TestCase(1, 1.5, 2.0)]
        [TestCase(2, 2.0, 2.5)]
        [TestCase(3, 2.5, 2.5)]
        [TestCase(4, 2.5, 1.5)]
        [TestCase(5, 1.5, 1.0)]
        public void PrepareComputation_Calculates_AllHitsInScene(Int32 index, Double n1, Double n2)
        {
            Sphere shape1 = Sphere.Glass();
            shape1.Transform = new TMatrix().Scaling(2, 2, 2);
            shape1.Material.RefractiveIndex = 1.5;
            Sphere shape2 = Sphere.Glass();
            shape2.Transform = new TMatrix().Translation(0, 0, -0.25);
            shape2.Material.RefractiveIndex = 2.0f;
            Sphere shape3 = Sphere.Glass();
            shape3.Transform = new TMatrix().Translation(0, 0, 0.25);
            shape3.Material.RefractiveIndex = 2.5;
            var ray = new TRay(new TPoint(0, 0, -4), new TVector(0, 0, 1));
            var intersections = new List<Intersection>
            {
                new(2.00, shape1),
                new(2.75, shape2),
                new(3.25, shape3),
                new(4.75, shape2),
                new(5.25, shape3),
                new(6.00, shape1)
            };

            Computations comps = intersections[index].PrepareComputations(ray, intersections);

            Assert.That(comps.N1, Is.EqualTo(n1));
            Assert.That(comps.N2, Is.EqualTo(n2));
        }

        [Test]
        public void PrepareComputation_SetsUnderPointZ_ToGreaterThanHalfEpsilon()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Sphere shape = Sphere.Glass();
            shape.Transform = new TMatrix().Translation(0, 0, 1);
            var intersection = new Intersection(5, shape);
            var intersections = new List<Intersection> { intersection };

            Computations comps = intersection.PrepareComputations(ray, intersections);

            Assert.That(comps.UnderPoint.Z, Is.GreaterThan(Constants.EPSILON / 2));
        }

        [Test]
        public void PrepareComputation_SetsUnderPointZ_ToJustBelowPointZ()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Sphere shape = Sphere.Glass();
            shape.Transform = new TMatrix().Translation(0, 0, 1);
            var intersection = new Intersection(5, shape);
            var intersections = new List<Intersection> { intersection };

            Computations comps = intersection.PrepareComputations(ray, intersections);

            Assert.That(comps.Point.Z, Is.LessThan(comps.UnderPoint.Z));
        }

        [Test]
        public void Schlick_SetReflectanceToOne_WhenTotalInternalReflectionOccurs()
        {
            var shape = Sphere.Glass();
            var ray = new TRay(new TPoint(0, 0, Math.Sqrt(2) / 2), new TVector(0, 1, 0));
            var intersections = new List<Intersection>
            {
                new(-Math.Sqrt(2) / 2, shape),
                new(Math.Sqrt(2) / 2, shape)
            };

            Computations comps = intersections[1].PrepareComputations(ray, intersections);
            Double reflectance = Intersection.Schlick(comps);

            Assert.That(reflectance, Is.EqualTo(1.0));
        }

        [Test]
        public void Schlick_ReturnsLittleReflectance_WhenRayPerpendicular()
        {
            var shape = Sphere.Glass();
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 1, 0));
            var intersections = new List<Intersection>
            {
                new(-1.0, shape),
                new(1.0, shape)
            };

            Computations comps = intersections[1].PrepareComputations(ray, intersections);
            Double reflectance = Intersection.Schlick(comps);

            Assert.That(Math.Abs(reflectance - 0.04), Is.LessThan(Constants.EPSILON));
        }

        [Test]
        public void Schlick_ReturnsHalfReflectance_WhenN2GreaterThanN1()
        {
            var shape = Sphere.Glass();
            var ray = new TRay(new TPoint(0, 0.99, -2), new TVector(0, 0, 1));
            var intersections = new List<Intersection>
            {
                new(1.8589, shape)
            };

            Computations comps = intersections[0].PrepareComputations(ray, intersections);
            Double reflectance = Intersection.Schlick(comps);

            Assert.That(Math.Abs(reflectance - 0.48873), Is.LessThan(Constants.EPSILON));
        }
    }
}

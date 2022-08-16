using System;
using System.Collections.Generic;
using libTracer;
using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestIntersection
    {
        private const Single EARLIER_TIME = 120;
        private const Single SOME_TIME    = 123;
        private const Single LATER_TIME   = 130;

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

            Computations result = _intersection.PrepareComputations(ray);

            Assert.That(result, Is.InstanceOf<Computations>());
        }

        [Test]
        public void PrepareComputations_SetsTime_ToIntersectionTime()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray);

            Assert.That(result.Time, Is.EqualTo(_intersection.Time));
        }

        [Test]
        public void PrepareComputations_SetsObject_ToShape()
        {
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = new Sphere();
            _intersection = new Intersection(4, shape);

            Computations result = _intersection.PrepareComputations(ray);

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

            Computations result = _intersection.PrepareComputations(ray);

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

            Computations result = _intersection.PrepareComputations(ray);

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

            Computations result = _intersection.PrepareComputations(ray);

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

            Computations result = _intersection.PrepareComputations(ray);

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

            Computations result = _intersection.PrepareComputations(ray);

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

            Computations result = _intersection.PrepareComputations(ray);

            Assert.That(result.NormalV, Is.EqualTo(new TVector(0, 0, -1)));
        }
    }
}

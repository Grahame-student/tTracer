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
    }
}

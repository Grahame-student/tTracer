using System;
using libTracer;
using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestTPoint
    {
        private const Single SOME_X = 1.2f;
        private const Single SOME_Y = 3.4f;
        private const Single SOME_Z = 5.6f;

        private TPoint _point;

        [Test]
        public void Constructor_SetsX_ToPassedInValue()
        {
            _point = new TPoint(SOME_X, 0, 0);

            Assert.That(_point.X, Is.EqualTo(SOME_X));
        }

        [Test]
        public void Constructor_SetsY_ToPassedInValue()
        {
            _point = new TPoint(0, SOME_Y, 0);

            Assert.That(_point.Y, Is.EqualTo(SOME_Y));
        }

        [Test]
        public void Constructor_SetsZ_ToPassedInValue()
        {
            _point = new TPoint(0, 0, SOME_Z);

            Assert.That(_point.Z, Is.EqualTo(SOME_Z));
        }

        [Test]
        public void Constructor_SetsW_To1()
        {
            _point = new TPoint(0, 0, SOME_Z);

            Assert.That(TPoint.W, Is.EqualTo(1f));
        }

        [Test]
        public void AddingVectorToPoint_Returns_Point()
        {
            var point1 = new TPoint(0, 0, 0);
            var vector2 = new TVector(0, 0, 0);

            Assert.That(point1 + vector2, Is.InstanceOf<TPoint>());
        }

        [Test]
        public void AddingVectorToPoint_Adds_XComponents()
        {
            var point1 = new TPoint(1, 0, 0);
            var vector2 = new TVector(2, 0, 0);

            TPoint point3 = point1 + vector2;

            Assert.That(point3.X, Is.EqualTo(3));
        }

        [Test]
        public void AddingVectorToPoint_Adds_YComponents()
        {
            var point1 = new TPoint(0, 2, 0);
            var vector2 = new TVector(0, 4, 0);

            TPoint point3 = point1 + vector2;

            Assert.That(point3.Y, Is.EqualTo(6));
        }

        [Test]
        public void AddingVectorToPoint_Adds_ZComponents()
        {
            var point1 = new TPoint(0, 0, 4);
            var vector2 = new TVector(0, 0, 8);

            TPoint point3 = point1 + vector2;

            Assert.That(point3.Z, Is.EqualTo(12));
        }

        [Test]
        public void SubtractingPointFromPoint_Returns_Vector()
        {
            var point1 = new TPoint(0, 0, 0);
            var point2 = new TPoint(0, 0, 0);

            Assert.That(point1 - point2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void SubtractingPointFromPoint_Subtracts_XComponents()
        {
            var point1 = new TPoint(3, 0, 0);
            var point2 = new TPoint(1, 0, 0);

            TVector point3 = point1 - point2;

            Assert.That(point3.X, Is.EqualTo(2));
        }

        [Test]
        public void SubtractingPointFromPoint_Subtracts_YComponents()
        {
            var point1 = new TPoint(0, 5, 0);
            var point2 = new TPoint(0, 2, 0);

            TVector point3 = point1 - point2;

            Assert.That(point3.Y, Is.EqualTo(3));
        }

        [Test]
        public void SubtractingPointFromPoint_Subtracts_ZComponents()
        {
            var point1 = new TPoint(0, 0, 9);
            var point2 = new TPoint(0, 0, 1);

            TVector point3 = point1 - point2;

            Assert.That(point3.Z, Is.EqualTo(8));
        }

        [Test]
        public void SubtractingVectorFromPoint_Returns_Point()
        {
            var point1 = new TPoint(0, 0, 0);
            var vector2 = new TVector(0, 0, 0);

            Assert.That(point1 - vector2, Is.InstanceOf<TPoint>());
        }

        [Test]
        public void SubtractingVectorFromPoint_Subtracts_XComponents()
        {
            var point1 = new TPoint(3, 0, 0);
            var vector2 = new TVector(1, 0, 0);

            TPoint point3 = point1 - vector2;

            Assert.That(point3.X, Is.EqualTo(2));
        }

        [Test]
        public void SubtractingVectorFromPoint_Subtracts_YComponents()
        {
            var point1 = new TPoint(0, 5, 0);
            var vector2 = new TVector(0, 2, 0);

            TPoint point3 = point1 - vector2;

            Assert.That(point3.Y, Is.EqualTo(3));
        }

        [Test]
        public void SubtractingVectorFromPoint_Subtracts_ZComponents()
        {
            var point1 = new TPoint(0, 0, 9);
            var vector2 = new TVector(0, 0, 1);

            TPoint point3 = point1 - vector2;

            Assert.That(point3.Z, Is.EqualTo(8));
        }
    }
}

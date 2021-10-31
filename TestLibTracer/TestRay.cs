using libTracer;
using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestRay
    {
        private TRay _ray;

        [Test]
        public void Constructor_SetOrigin_ToPassedInPoint()
        {
            var point = new TPoint(1, 2, 3);
            var direction = new TVector(3, 2, 1);

            _ray = new TRay(point, direction);

            Assert.That(_ray.Origin, Is.EqualTo(point));
        }

        [Test]
        public void Constructor_SetDirection_ToPassedInVector()
        {
            var point = new TPoint(1, 2, 3);
            var direction = new TVector(3, 2, 1);

            _ray = new TRay(point, direction);

            Assert.That(_ray.Direction, Is.EqualTo(direction));
        }

        [Test]
        public void Position_ReturnsPositionOfRay_AfterTSeconds()
        {
            var point = new TPoint(2, 3, 4);
            var direction = new TVector(1, 0, 0);

            _ray = new TRay(point, direction);

            var result = new TPoint(4.5f, 3, 4);
            Assert.That(_ray.Position(2.5f), Is.EqualTo(result));
        }

        [Test]
        public void Position_ReturnsPositionOfRay_AfterTSecondsWhenTIsNegative()
        {
            var point = new TPoint(2, 3, 4);
            var direction = new TVector(1, 0, 0);

            _ray = new TRay(point, direction);

            var result = new TPoint(1f, 3, 4);
            Assert.That(_ray.Position(-1f), Is.EqualTo(result));
        }

        [Test]
        public void Transform_Returns_InstanceOfRay()
        {
            _ray = new TRay(new TPoint(1, 2, 3), new TVector(0, 1, 0));
            TMatrix translation = new TMatrix().Translation(3, 4, 5);

            Assert.That(_ray.Transform(translation), Is.InstanceOf<TRay>());
        }

        [Test]
        public void Transform_Translates_Origin()
        {
            _ray = new TRay(new TPoint(1, 2, 3), new TVector(0, 1, 0));
            TMatrix translation = new TMatrix().Translation(3, 4, 5);

            TRay result = _ray.Transform(translation);

            var expectedResult = new TPoint(4, 6, 8);
            Assert.That(result.Origin, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Transform_DoesNotTranslate_Direction()
        {
            _ray = new TRay(new TPoint(1, 2, 3), new TVector(0, 1, 0));
            TMatrix translation = new TMatrix().Translation(3, 4, 5);

            TRay result = _ray.Transform(translation);

            var expectedResult = new TVector(0, 1, 0);
            Assert.That(result.Direction, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Transform_Scales_Origin()
        {
            _ray = new TRay(new TPoint(1, 2, 3), new TVector(0, 1, 0));
            TMatrix scaling = new TMatrix().Scaling(2, 3, 4);

            TRay result = _ray.Transform(scaling);

            var expectedResult = new TPoint(2, 6, 12);
            Assert.That(result.Origin, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Transform_Scales_Direction()
        {
            _ray = new TRay(new TPoint(1, 2, 3), new TVector(0, 1, 0));
            TMatrix scaling = new TMatrix().Scaling(2, 3, 4);

            TRay result = _ray.Transform(scaling);

            var expectedResult = new TVector(0, 3, 0);
            Assert.That(result.Direction, Is.EqualTo(expectedResult));
        }
    }
}

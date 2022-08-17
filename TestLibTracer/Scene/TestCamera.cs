using System;
using libTracer.Common;
using libTracer.Scene;
using NUnit.Framework;

namespace TestLibTracer.Scene
{
    internal class TestCamera
    {
        private const Int32 SOME_WIDTH = 160;
        private const Int32 SOME_HEIGHT = 120;
        private const Single SOME_FIELD_OF_VIEW = MathF.PI / 2;
        private const Int32 SOME_X = 100;
        private const Int32 SOME_Y = 25;

        private Camera _camera;

        [Test]
        public void Constructor_SetsCanvas_ToPassedInWidth()
        {
            _camera = new Camera(SOME_WIDTH, SOME_HEIGHT, SOME_FIELD_OF_VIEW);

            Assert.That(_camera.Canvas.Width, Is.EqualTo(SOME_WIDTH));
        }

        [Test]
        public void Constructor_SetsCanvas_ToPassedInHeight()
        {
            _camera = new Camera(SOME_WIDTH, SOME_HEIGHT, SOME_FIELD_OF_VIEW);

            Assert.That(_camera.Canvas.Height, Is.EqualTo(SOME_HEIGHT));
        }

        [Test]
        public void Constructor_SetsFieldOfView_ToPassedInValue()
        {
            _camera = new Camera(SOME_WIDTH, SOME_HEIGHT, SOME_FIELD_OF_VIEW);

            Assert.That(MathF.Abs(_camera.FieldOfView - SOME_FIELD_OF_VIEW), Is.LessThan(0.0001f));
        }

        [Test]
        public void Constructor_SetsTransformation_ToIdentiyMatrix()
        {
            _camera = new Camera(SOME_WIDTH, SOME_HEIGHT, SOME_FIELD_OF_VIEW);

            Assert.That(_camera.Transformation, Is.EqualTo(new TMatrix()));
        }

        [Test]
        public void Constructor_SetsPixelSize_ForLandscape()
        {
            _camera = new Camera(200, 125, MathF.PI / 2);

            Assert.That(_camera.PixelSize, Is.EqualTo(0.01f));
        }

        [Test]
        public void Constructor_SetsPixelSize_ForPortrait()
        {
            _camera = new Camera(125, 200, MathF.PI / 2);

            Assert.That(_camera.PixelSize, Is.EqualTo(0.01f));
        }

        [Test]
        public void PixelRay_Returns_InstanceOfRay()
        {
            _camera = new Camera(SOME_WIDTH, SOME_HEIGHT, SOME_FIELD_OF_VIEW);

            Assert.That(_camera.PixelRay(SOME_X, SOME_Y), Is.InstanceOf<TRay>());
        }

        [Test]
        public void PixelRay_ReturnsRay_FromCameraOriginThroughCentrePixel()
        {
            _camera = new Camera(201, 101, MathF.PI / 2);

            Assert.That(_camera.PixelRay(100, 50).Origin, Is.EqualTo(new TPoint(0, 0, 0)));
        }

        [Test]
        public void PixelRay_ReturnsRay_FromCameraInDirectionOfCentrePixel()
        {
            _camera = new Camera(201, 101, MathF.PI / 2);

            TRay result = _camera.PixelRay(100, 50);
            Assert.That(result.Direction, Is.EqualTo(new TVector(0, 0, -1)));
        }

        [Test]
        public void PixelRay_ReturnsRay_FromCameraOriginThroughCornerPixel()
        {
            _camera = new Camera(201, 101, MathF.PI / 2);

            Assert.That(_camera.PixelRay(0, 0).Origin, Is.EqualTo(new TPoint(0, 0, 0)));
        }

        [Test]
        public void PixelRay_ReturnsRay_FromCameraInDirectionOfCornerPixel()
        {
            _camera = new Camera(201, 101, MathF.PI / 2);

            TVector result = _camera.PixelRay(0, 0).Direction;
            var expectedResult = new TVector(0.66519f, 0.33259f, -0.66851f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void PixelRay_ReturnsRay_FromCameraOrginWhenTransformed()
        {
            _camera = new Camera(201, 101, MathF.PI / 2)
            {
                Transformation = new TMatrix().Translation(0, -2, 5).RotationY(MathF.PI / 4)
            };

            Assert.That(_camera.PixelRay(100, 50).Origin, Is.EqualTo(new TPoint(0, 2, -5)));
        }

        [Test]
        public void PixelRay_ReturnsRay_FromInCameraDirectionWhenTransformed()
        {
            _camera = new Camera(201, 101, MathF.PI / 2)
            {
                Transformation = new TMatrix().Translation(0, -2, 5).RotationY(MathF.PI / 4)
            };

            TVector result = _camera.PixelRay(100, 50).Direction;
            var expectedResult = new TVector(MathF.Sqrt(2) / 2, 0, -MathF.Sqrt(2) / 2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Render_SetsCenterPixelToExpectedColour_WhenUsingDefaultWorld()
        {
            World world = World.CreateWorld();
            _camera = new Camera(11, 11, MathF.PI / 2);
            var from = new TPoint(0, 0, -5);
            var to = new TPoint(0, 0, 0);
            var up = new TVector(0, 1, 0);
            _camera.Transformation = TMatrix.ViewTransformation(from, to, up);

            Canvas image = _camera.Render(world);

            Assert.That(image.GetPixel(5, 5), Is.EqualTo(new TColour(0.38066f, 0.47583f, 0.2855f)));
        }
    }
}

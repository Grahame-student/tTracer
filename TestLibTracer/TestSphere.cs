using System;
using System.Collections.Generic;
using libTracer;
using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestSphere
    {
        private Sphere _sphere;

        [Test]
        public void Constructor_SetsTransform_ToIdentityMatrix()
        {
            _sphere = new Sphere();

            Assert.That(_sphere.Transform, Is.EqualTo(new TMatrix()));
        }

        [Test]
        public void Constructor_SetsMaterial_ToDefaultMaterial()
        {
            _sphere = new Sphere();

            Assert.That(_sphere.Material, Is.EqualTo(new Material()));
        }

        [Test]
        public void Intersects_ReturnsTwoElements_WhenRayPassesThroughSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersects_Returns_4InFirstElement()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Time, Is.EqualTo(4));
        }

        [Test]
        public void Intersects_Returns_ShapeInFirstElement()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Shape, Is.EqualTo(_sphere));
        }

        [Test]
        public void Intersects_Returns_6InSecondElement()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[1].Time, Is.EqualTo(6));
        }

        [Test]
        public void Intersects_Returns_ShapeInSecondElement()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Shape, Is.EqualTo(_sphere));
        }

        [Test]
        public void Intersects_ReturnsTwoElements_WhenRayAtTangentToSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 1, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersects_Returns5InFirstElement_WhenRayAtTagentToSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 1, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Time, Is.EqualTo(5));
        }

        [Test]
        public void Intersects_Returns5InSecondElement_WhenRayAtTagentToSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 1, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[1].Time, Is.EqualTo(5));
        }

        [Test]
        public void Intersects_ReturnsNoElements_WhenRayMissesSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 2, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Intersects_ReturnsTwoElements_WhenRayStartsWithinSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersects_ReturnsMinus1InFirstElement_WhenRayStartsWithinSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Time, Is.EqualTo(-1));
        }

        [Test]
        public void Intersects_Returns1InSecondElement_WhenRayStartsWithinSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[1].Time, Is.EqualTo(1));
        }

        [Test]
        public void Intersects_ReturnsTwoElements_WhenRayStartsBeyondSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection.Count, Is.EqualTo(2));
        }

        [Test]
        public void Intersects_ReturnsMinus6InFirstElement_WhenRayStartsBeyondSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Time, Is.EqualTo(-6));
        }

        [Test]
        public void Intersects_ReturnsMinus4InSecondElement_WhenRayStartsBeyondSphere()
        {
            _sphere = new Sphere();
            var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[1].Time, Is.EqualTo(-4));
        }

        [Test]
        public void Intersect_ScalesRay_WhenCalculatingResult()
        {
            _sphere = new Sphere
            {
                Transform = new TMatrix().Scaling(2, 2, 2)
            };
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection[0].Time, Is.EqualTo(3));
            Assert.That(intersection[1].Time, Is.EqualTo(7));
        }

        [Test]
        public void Intersect_TranslatesRay_WhenCalculatingResult()
        {
            _sphere = new Sphere
            {
                Transform = new TMatrix().Translation(5, 0, 0)
            };
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> intersection = _sphere.Intersects(ray);

            Assert.That(intersection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Normal_Returns_InstanceOfVector()
        {
            _sphere = new Sphere();

            Assert.That(_sphere.Normal(new TPoint(0, 0, 0)), Is.InstanceOf<TVector>());
        }

        [Test]
        public void Normal_Returns_PointOnXAxis()
        {
            _sphere = new Sphere();

            TVector result = _sphere.Normal(new TPoint(1, 0, 0));

            var expectedResult = new TVector(1, 0, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Normal_Returns_PointOnYAxis()
        {
            _sphere = new Sphere();

            TVector result = _sphere.Normal(new TPoint(0, 1, 0));

            var expectedResult = new TVector(0, 1, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Normal_Returns_PointOnZAxis()
        {
            _sphere = new Sphere();

            TVector result = _sphere.Normal(new TPoint(0, 0, 1));

            var expectedResult = new TVector(0, 0, 1);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Normal_Returns_PointOnSphereAtNonAxialPosition()
        {
            _sphere = new Sphere();

            TVector result = _sphere.Normal(new TPoint(MathF.Sqrt(3)/3, MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3));

            var expectedResult = new TVector(MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Normal_Returns_NormalisedVector()
        {
            _sphere = new Sphere();

            TVector result = _sphere.Normal(new TPoint(MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3));

            Assert.That(result.Normalise(), Is.EqualTo(result));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenSphereTranslated()
        {
            _sphere = new Sphere
            {
                Transform = new TMatrix().Translation(0, 1, 0)
            };

            TVector result = _sphere.Normal(new TPoint(0, 1.70711f, -0.70711f));

            var expectedResult = new TVector(0, 0.70711f, -0.70711f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Normal_ReturnsNormal_WhenSphereTransformed()
        {
            _sphere = new Sphere
            {
                Transform = new TMatrix().RotationZ(MathF.PI / 5).Scaling(1, 0.5f, 1)
            };

            TVector result = _sphere.Normal(new TPoint(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2));

            var expectedResult = new TVector(0, 0.97014f, -0.24254f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}

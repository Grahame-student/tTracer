using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

using NUnit.Framework;

namespace TestLibTracer.Scene
{
    internal class TestWorld
    {
        private World _world;

        [Test]
        public void Constructor_SetsLight_ToNull()
        {
            _world = new World();

            Assert.That(_world.Light, Is.Null);
        }

        [Test]
        public void Constructor_SetsObjects_ToEmptyList()
        {
            _world = new World();

            Assert.That(_world.Objects.Count, Is.EqualTo(0));
        }

        [Test]
        public void CreateWorld_Returns_InstanceOfWorld()
        {
            Assert.That(World.CreateWorld(), Is.InstanceOf<World>());
        }

        [Test]
        public void CreateWorld_SetsLightColour_ToDefaultLightColour()
        {
            _world = World.CreateWorld();

            var expectedIntensity = new TColour(1, 1, 1);
            Assert.That(_world.Light.Intensity, Is.EqualTo(expectedIntensity));
        }

        [Test]
        public void CreateWorld_SetsLightPosition_ToDefaultLightPosition()
        {
            _world = World.CreateWorld();

            var expectedPosition = new TPoint(-10, 10, -10);
            Assert.That(_world.Light.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void CreateWorld_Adds_OuterSphere()
        {
            _world = World.CreateWorld();

            var outerSpehere = new Sphere()
            {
                Material = new Material()
                {
                    Colour = new TColour(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f
                }
            };
            Assert.That(_world.Objects.Contains(outerSpehere), Is.True);
        }

        [Test]
        public void CreateWorld_Adds_InnerSphere()
        {
            _world = World.CreateWorld();

            var innerSphere = new Sphere()
            {
                Transform = new TMatrix().Scaling(0.5f, 0.5f, 0.5f)
            };
            Assert.That(_world.Objects.Contains(innerSphere), Is.True);
        }

        [Test]
        public void Intersect_Returns_AllRayIntersections()
        {
            // Ray will pass through both of the default spheres in the world
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> result = _world.Intersects(ray);

            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void Intersect_ReturnsIntersection_AsOuterSphereEntered()
        {
            // Ray will pass through both of the default spheres in the world
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> result = _world.Intersects(ray);

            Assert.That(result[0].Time, Is.EqualTo(4));
        }

        [Test]
        public void Intersect_ReturnsIntersection_AsInnerSphereEntered()
        {
            // Ray will pass through both of the default spheres in the world
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> result = _world.Intersects(ray);

            Assert.That(result[1].Time, Is.EqualTo(4.5));
        }

        [Test]
        public void Intersect_ReturnsIntersection_AsInnerSphereExited()
        {
            // Ray will pass through both of the default spheres in the world
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> result = _world.Intersects(ray);

            Assert.That(result[2].Time, Is.EqualTo(5.5));
        }

        [Test]
        public void Intersect_ReturnsIntersection_AsOuterSphereExited()
        {
            // Ray will pass through both of the default spheres in the world
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            IList<Intersection> result = _world.Intersects(ray);

            Assert.That(result[3].Time, Is.EqualTo(6));
        }

        [Test]
        public void ShadeHit_ReturnsInstanceOfColour()
        {
            _world = World.CreateWorld();

            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));
            Shape shape = _world.Objects[1];
            var intersection = new Intersection(0.5f, shape);
            Computations comps = intersection.PrepareComputations(ray);
            Assert.That(_world.ShadeHit(comps), Is.InstanceOf<TColour>());
        }

        [Test]
        public void ShadeHit_ShadesIntersection_fromOutside()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = _world.Objects[0];
            var intersection = new Intersection(4, shape);
            Computations comps = intersection.PrepareComputations(ray);

            TColour result = _world.ShadeHit(comps);

            var expectedResult = new TColour(0.38066f, 0.47583f, 0.2855f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShadeHit_ShadesIntersection_fromInside()
        {
            _world = World.CreateWorld();
            _world.Light = new Light(new TPoint(0, 0.25f, 0), new TColour(1, 1, 1));
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));
            Shape shape = _world.Objects[1];
            var intersection = new Intersection(0.5f, shape);
            Computations comps = intersection.PrepareComputations(ray);

            TColour result = _world.ShadeHit(comps);

            var expectedResult = new TColour(0.90498f, 0.90498f, 0.90498f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShadeHit_IdentifyIntersection_InShadow()
        {
            _world = new World
            {
                Light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1))
            };
            var shape1 = new Sphere();
            _world.Objects.Add(shape1);
            var shape2 = new Sphere
            {
                Transform = new TMatrix().Translation(0, 0, 10)
            };
            _world.Objects.Add(shape2);
            var ray = new TRay(new TPoint(0, 0, 5), new TVector(0, 0, 1));
            var intersection = new Intersection(4, shape2);
            Computations comps = intersection.PrepareComputations(ray);

            TColour result = _world.ShadeHit(comps);

            var expectedResult = new TColour(0.1f, 0.1f, 0.1f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ColourAt_Returns_InstanceOfColour()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            Assert.That(_world.ColourAt(ray), Is.InstanceOf<TColour>());
        }

        [Test]
        public void ColourAt_ReturnsBlack_WhenRayMissesAllObjects()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 1, 0));

            Assert.That(_world.ColourAt(ray), Is.EqualTo(new TColour(0, 0, 0)));
        }

        [Test]
        public void ColourAt_ReturnsColour_WhenRayHits()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            Assert.That(_world.ColourAt(ray), Is.EqualTo(new TColour(0.38066f, 0.47583f, 0.2855f)));
        }

        [Test]
        public void ColourAt_ReturnsMaterialColour_WhenRayIntersectsBehindRay()
        {
            _world = World.CreateWorld();
            Shape outer = _world.Objects[0];
            outer.Material.Ambient = 1;
            Shape inner = _world.Objects[1];
            inner.Material.Ambient = 1;
            var ray = new TRay(new TPoint(0, 0, 0.75f), new TVector(0, 0, -1));

            Assert.That(_world.ColourAt(ray), Is.EqualTo(inner.Material.Colour));
        }

        [Test]
        public void IsShadow_ReturnsFalse_WhenNothingCollinearWithPointAndLight()
        {
            _world = World.CreateWorld();

            var p = new TPoint(0, 10, 0);

            Assert.That(_world.IsShadowed(p), Is.EqualTo(false));
        }

        [Test]
        public void IsShadow_ReturnsTrue_WhenObjectBetweenPointAndLight()
        {
            _world = World.CreateWorld();

            var p = new TPoint(10, -10, 10);

            Assert.That(_world.IsShadowed(p), Is.EqualTo(true));
        }

        [Test]
        public void IsShadow_ReturnsFalse_WhenPointBehindLight()
        {
            _world = World.CreateWorld();

            var p = new TPoint(-20, 20, -20);

            Assert.That(_world.IsShadowed(p), Is.EqualTo(false));
        }

        [Test]
        public void IsShadow_ReturnsFalse_WhenObjectBehindPoint()
        {
            _world = World.CreateWorld();

            var p = new TPoint(-2, 2, -2);

            Assert.That(_world.IsShadowed(p), Is.EqualTo(false));
        }
    }
}

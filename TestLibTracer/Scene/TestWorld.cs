using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

using NUnit.Framework;
using TestLibTracer.Scene.Patterns;

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
            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });
            Assert.That(_world.ShadeHit(comps, 0), Is.InstanceOf<TColour>());
        }

        [Test]
        public void ShadeHit_ShadesIntersection_fromOutside()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            Shape shape = _world.Objects[0];
            var intersection = new Intersection(4, shape);
            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });

            TColour result = _world.ShadeHit(comps, 0);

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
            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });

            TColour result = _world.ShadeHit(comps, 0);

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
            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });

            TColour result = _world.ShadeHit(comps, 0);

            var expectedResult = new TColour(0.1f, 0.1f, 0.1f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShadeHit_Includes_ReflectiveColour()
        {
            _world = World.CreateWorld();
            var shape = new Plane();
            shape.Material.Reflective = 0.5f;
            shape.Transform = new TMatrix().Translation(0, -1, 0);
            _world.Objects.Add(shape);
            var ray = new TRay(new TPoint(0, 0, -3), new TVector(0, -MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2));
            var intersection = new Intersection(MathF.Sqrt(2), shape);

            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });
            TColour colour = _world.ShadeHit(comps, 5);

            Assert.That(colour, Is.EqualTo(new TColour(0.87677f, 0.92436f, 0.82918f)));
        }

        [Test]
        public void ShadeHit_IncludeRefraction()
        {
            _world = World.CreateWorld();
            var floor = new Plane();
            floor.Transform = new TMatrix().Translation(0, -1, 0);
            floor.Material.Transparency = 0.5f;
            floor.Material.RefractiveIndex = 1.5f;
            _world.Objects.Add(floor);
            var ball = new Sphere();
            ball.Material.Colour = new TColour(1, 0, 0);
            ball.Material.Ambient = 0.5f;
            ball.Transform = new TMatrix().Translation(0, -3.5f, -0.5f);
            _world.Objects.Add(ball);
            var ray = new TRay(new TPoint(0, 0, -3), new TVector(0, -MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2));
            var intersections = new List<Intersection>
            {
                new(MathF.Sqrt(2), floor)
            };

            var comps = intersections[0].PrepareComputations(ray, intersections);
            var colour = _world.ShadeHit(comps, 5);

            Assert.That(colour, Is.EqualTo(new TColour(0.93642f, 0.68642f, 0.68642f)));
        }

        [Test]
        public void ColourAt_Returns_InstanceOfColour()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            Assert.That(_world.ColourAt(ray, 0), Is.InstanceOf<TColour>());
        }

        [Test]
        public void ColourAt_ReturnsBlack_WhenRayMissesAllObjects()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 1, 0));

            Assert.That(_world.ColourAt(ray, 0), Is.EqualTo(new TColour(0, 0, 0)));
        }

        [Test]
        public void ColourAt_ReturnsColour_WhenRayHits()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));

            Assert.That(_world.ColourAt(ray, 0), Is.EqualTo(new TColour(0.38066f, 0.47583f, 0.2855f)));
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

            Assert.That(_world.ColourAt(ray, 0), Is.EqualTo(inner.Material.Colour));
        }

        [Test]
        public void ColourAt_ReturnsNonNull_WhenRayCanReflectForever()
        {
            _world = new World();
            _world.Light = new Light(new TPoint(0, 0, 0), ColourFactory.White());
            var lower = new Plane();
            lower.Material.Reflective = 1;
            lower.Transform = new TMatrix().Translation(0, -1, 0);
            _world.Objects.Add(lower);
            var upper = new Plane();
            upper.Material.Reflective = 1;
            upper.Transform = new TMatrix().Translation(0, 1, 0);
            _world.Objects.Add(upper);
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 1, 0));

            Assert.That(_world.ColourAt(ray, 0), Is.Not.Null);
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

        [Test]
        public void ReflectedColour_ReturnsBlack_WhenMaterialNonReflective()
        {
            _world = World.CreateWorld();
            var ray = new TRay(new TPoint(0, 0, 0), new TVector(0, 0, 1));
            Shape shape = _world.Objects[1];
            shape.Material.Ambient = 1;
            var intersection = new Intersection(1, shape);

            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });
            TColour colour = _world.ReflectedColour(comps, 0);

            Assert.That(colour, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void ReflectedColour_ReturnsShapeColour_WhenShapeIsReflective()
        {
            _world = World.CreateWorld();
            var shape = new Plane
            {
                Material =
                {
                    Reflective = 0.5f
                },
                Transform = new TMatrix().Translation(0, -1, 0)
            };
            _world.Objects.Add(shape);
            var ray = new TRay(new TPoint(0, 0, -3), new TVector(0, -MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2));
            var intersection = new Intersection(MathF.Sqrt(2), shape);

            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });
            TColour colour = _world.ReflectedColour(comps, 5);

            Assert.That(colour, Is.EqualTo(new TColour(0.19032f, 0.2379f, 0.14274f)));
        }

        [Test]
        public void ReflectedColour_ReturnsBlack_WhenRecursionLimitReached()
        {
            _world = World.CreateWorld();
            var shape = new Plane();
            shape.Material.Reflective = 0.5f;
            shape.Transform = new TMatrix().Translation(0, -1, 0);
            _world.Objects.Add(shape);
            var ray = new TRay(new TPoint(0, 0, -3), new TVector(0, -MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2));
            var intersection = new Intersection(MathF.Sqrt(2), shape);

            Computations comps = intersection.PrepareComputations(ray, new List<Intersection> { intersection });
            TColour colour = _world.ReflectedColour(comps, 0);

            Assert.That(colour, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void RefractedColour_ReturnsBlack_WhenMaterialOpaque()
        {
            _world = World.CreateWorld();
            Shape shape = _world.Objects[0];
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            var intersections = new List<Intersection>
            {
                new(4, shape),
                new(6, shape)
            };

            Computations comps = intersections[0].PrepareComputations(ray, intersections);
            TColour colour = _world.RefractedColour(comps, 5);

            Assert.That(colour, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void RefractedColour_ReturnsBlack_WhenAtMaximumRecursion()
        {
            _world = World.CreateWorld();
            Shape shape = _world.Objects[0];
            shape.Material.Transparency = 1.0f;
            shape.Material.RefractiveIndex = 1.5f;
            var ray = new TRay(new TPoint(0, 0, -5), new TVector(0, 0, 1));
            var intersections = new List<Intersection>
            {
                new(4, shape),
                new(6, shape)
            };

            Computations comps = intersections[0].PrepareComputations(ray, intersections);
            TColour colour = _world.RefractedColour(comps, 0);

            Assert.That(colour, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void RefractedColour_ReturnsBlack_UnderTotalInternalReflection()
        {
            _world = World.CreateWorld();
            Shape shape = _world.Objects[0];
            shape.Material.Transparency = 1.0f;
            shape.Material.RefractiveIndex = 1.5f;
            var ray = new TRay(new TPoint(0, 0, MathF.Sqrt(2) / 2), new TVector(0, 1, 0));
            var intersections = new List<Intersection>
            {
                new(-MathF.Sqrt(2) / 2, shape),
                new(MathF.Sqrt(2) / 2, shape)
            };

            Computations comps = intersections[1].PrepareComputations(ray, intersections);
            TColour colour = _world.RefractedColour(comps, 5);

            Assert.That(colour, Is.EqualTo(ColourFactory.Black()));
        }

        [Test]
        public void RefractedColour_ReturnsPointOfIntersection_WhenUsingTestPattern()
        {
            _world = World.CreateWorld();
            Shape shape1 = _world.Objects[0];
            shape1.Material.Ambient = 1.0f;
            shape1.Material.Pattern = new TestPattern();
            Shape shape2 = _world.Objects[1];
            shape2.Material.Transparency = 1.0f;
            shape2.Material.RefractiveIndex = 1.5f;
            var ray = new TRay(new TPoint(0, 0, 0.1f), new TVector(0, 1, 0));
            var intersections = new List<Intersection>
            {
                new(-0.9899f, shape1),
                new(-0.4899f, shape2),
                new(0.4899f, shape2),
                new(0.9899f, shape1)
            };

            Computations comps = intersections[2].PrepareComputations(ray, intersections);
            TColour colour = _world.RefractedColour(comps, 5);

            Assert.That(colour, Is.EqualTo(new TColour(0, 0.99888f, 0.04725f)));
        }
    }
}

using libTracer.Common;
using libTracer.Scene;
using NUnit.Framework;

using System;
using libTracer.Scene.Patterns;
using libTracer.Shapes;

namespace TestLibTracer.Scene
{
    internal class TestMaterial
    {
        private Material _material;

        [Test]
        public void Constructor_SetsColour_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Colour, Is.EqualTo(new TColour(1, 1, 1)));
        }

        [Test]
        public void Constructor_SetsAmbient_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Ambient, Is.EqualTo(0.1f));
        }

        [Test]
        public void Constructor_SetsDiffuse_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Diffuse, Is.EqualTo(0.9f));
        }

        [Test]
        public void Constructor_SetsSpecular_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Specular, Is.EqualTo(0.9f));
        }

        [Test]
        public void Constructor_SetsShininess_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Shininess, Is.EqualTo(200f));
        }

        [Test]
        public void Constructor_SetsReflective_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Reflective, Is.EqualTo(0.0f));
        }

        [Test]
        public void Constructor_SetsTransparency_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.Transparency, Is.EqualTo(0.0f));
        }

        [Test]
        public void Constructor_SetsRefractiveIndex_ToDefault()
        {
            _material = new Material();

            Assert.That(_material.RefractiveIndex, Is.EqualTo(1.0f));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenAmbientEqualToOtherAmbient()
        {
            var material1 = new Material();
            var material2 = new Material();

            material1.Ambient = 5;
            material2.Ambient = 5;

            Assert.That(material1, Is.EqualTo(material2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenDiffuseEqualToOtherDiffuse()
        {
            var material1 = new Material();
            var material2 = new Material();

            material1.Diffuse = 5;
            material2.Diffuse = 5;

            Assert.That(material1, Is.EqualTo(material2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenShininessEqualToOtherShininess()
        {
            var material1 = new Material();
            var material2 = new Material();

            material1.Shininess = 5;
            material2.Shininess = 5;

            Assert.That(material1, Is.EqualTo(material2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenSpecularEqualToOtherSpecular()
        {
            var material1 = new Material();
            var material2 = new Material();

            material1.Specular = 5;
            material2.Specular = 5;

            Assert.That(material1, Is.EqualTo(material2));
        }

        [Test]
        public void Equals_ReturnsFalse_WhenOtherIsObjectAndNull()
        {
            var material1 = new Material();
            Object material2 = null;

            Assert.That(material1.Equals(material2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsObject()
        {
            var material1 = new Material();
            Object material2 = new Material();

            Assert.That(material1.Equals(material2), Is.True);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsObjectAndSameReference()
        {
            var material1 = new Material();
            Object material2 = material1;

            Assert.That(material1.Equals(material2), Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenOtherIsVectorAndNull()
        {
            var material1 = new Material();
            Material material2 = null;

            Assert.That(material1.Equals(material2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsVectorAndSameReference()
        {
            var material1 = new Material();
            Material material2 = material1;

            Assert.That(material1.Equals(material2), Is.True);
        }

        [Test]
        public void Hashcode_ReturnsSameValue_WhenSetToSameValues()
        {
            var material1 = new Material();
            var material2 = new Material();

            Assert.That(material1.GetHashCode(), Is.EqualTo(material2.GetHashCode()));
        }

        [Test]
        public void Lighting_Returns_InstanceOfColour()
        {
            _material = new Material();
            var light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1));
            var position = new TPoint(0, 0, 0);
            TVector eye = new TVector(0, 0, -1);
            TVector normal = new TVector(0, 0, -1);

            Assert.That(_material.Lighting(new Sphere(), light, position, eye, normal, false), Is.InstanceOf<TColour>());
        }

        [Test]
        public void Lighting_ReturnsFullIntensity_WhenEyeBetweenLightAndSurface()
        {
            _material = new Material();
            var light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1));
            var position = new TPoint(0, 0, 0);
            TVector eye = new TVector(0, 0, -1);
            TVector normal = new TVector(0, 0, -1);

            var expectedResult = new TColour(1.9f, 1.9f, 1.9f);
            Assert.That(_material.Lighting(new Sphere(), light, position, eye, normal, false), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Lighting_ReturnsUnchangedIntensity_WhenLightSurfaceAndEyeOffsetBy45Degrees()
        {
            _material = new Material();
            var light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1));
            var position = new TPoint(0, 0, 0);
            TVector eye = new TVector(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
            TVector normal = new TVector(0, 0, -1);

            var expectedResult = new TColour(1.0f, 1.0f, 1.0f);
            Assert.That(_material.Lighting(new Sphere(), light, position, eye, normal, false), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Lighting_ReturnsReducedIntensity_WhenEyeOppositeSurfaceLightAt45Degrees()
        {
            _material = new Material();
            var light = new Light(new TPoint(0, 10, -10), new TColour(1, 1, 1));
            var position = new TPoint(0, 0, 0);
            var eye = new TVector(0, 0, -1);
            var normal = new TVector(0, 0, -1);

            TColour result = _material.Lighting(new Sphere(), light, position, eye, normal, false);

            var expectedResult = new TColour(0.7364f, 0.7364f, 0.7364f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Lighting_ReturnsReducedIntensity_WhenEyeInPathOfReflectedLight()
        {
            _material = new Material();
            var light = new Light(new TPoint(0, 10, -10), new TColour(1, 1, 1));
            var position = new TPoint(0, 0, 0);
            var eye = new TVector(0, -MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
            var normal = new TVector(0, 0, -1);

            var expectedResult = new TColour(1.6364f, 1.6364f, 1.6364f);
            Assert.That(_material.Lighting(new Sphere(), light, position, eye, normal, false), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Lighting_ReturnsLittleColour_WhenLightBehindSurface()
        {
            _material = new Material();
            var light = new Light(new TPoint(0, 0, 10), new TColour(1, 1, 1));
            var position = new TPoint(0, 0, 0);
            var eye = new TVector(0, 0, -1);
            var normal = new TVector(0, 0, -1);

            var expectedResult = new TColour(0.1f, 0.1f, 0.1f);
            Assert.That(_material.Lighting(new Sphere(), light, position, eye, normal, false), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Lighting_ReturnsLittleColour_WhenPointInShadow()
        {
            _material = new Material();
            var position = new TPoint(0, 0, 0);
            var eye = new TVector(0, 0, -1);
            var normal = new TVector(0, 0, -1);
            var light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1));
            var inShadow = true;

            var result = _material.Lighting(new Sphere(), light, position, eye, normal, inShadow);

            var expectedResult = new TColour(0.1f, 0.1f, 0.1f);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Lighting_ReturnsWhite_WhenStripePatternAppliedToMaterialAndRayHitsWhiteStripe()
        {
            _material = new Material
            {
                Pattern = new Stripes(ColourFactory.White(), ColourFactory.Black()),
                Ambient = 1,
                Diffuse = 0,
                Specular = 0
            };
            var eyev = new TVector(0, 0, -1);
            var normalv = new TVector(0, 0, -1);
            var light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1));

            Assert.That(_material.Lighting(new Sphere(), light, new TPoint(0.9f, 0, 0), eyev, normalv, false),
                Is.EqualTo(ColourFactory.White()));
        }

        [Test]
        public void Lighting_ReturnsBlack_WhenStripePatternAppliedToMaterialAndRayHitsBlackStripe()
        {
            _material = new Material
            {
                Pattern = new Stripes(ColourFactory.White(), ColourFactory.Black()),
                Ambient = 1,
                Diffuse = 0,
                Specular = 0
            };
            var eyev = new TVector(0, 0, -1);
            var normalv = new TVector(0, 0, -1);
            var light = new Light(new TPoint(0, 0, -10), new TColour(1, 1, 1));

            Assert.That(_material.Lighting(new Sphere(), light, new TPoint(1.1f, 0, 0), eyev, normalv, false),
                Is.EqualTo(ColourFactory.Black()));
        }
    }
}

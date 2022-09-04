using System;
using libTracer.Common;
using NUnit.Framework;

namespace TestLibTracer.Common
{
    internal class TestTColour
    {
        private const Double SOME_RED = 0.1;
        private const Double SOME_GREEN = 0.2;
        private const Double SOME_BLUE = 0.3;

        private TColour _colour;

        [Test]
        public void Constructor_SetsRed_ToPassedInValue()
        {
            _colour = new TColour(SOME_RED, 0, 0);

            Assert.That(_colour.Red, Is.EqualTo(SOME_RED));
        }

        [Test]
        public void Constructor_SetsGreen_ToPassedInValue()
        {
            _colour = new TColour(0, SOME_GREEN, 0);

            Assert.That(_colour.Green, Is.EqualTo(SOME_GREEN));
        }

        [Test]
        public void Constructor_SetsBlue_ToPassedInValue()
        {
            _colour = new TColour(0, 0, SOME_BLUE);

            Assert.That(_colour.Blue, Is.EqualTo(SOME_BLUE));
        }

        [Test]
        public void Add_Returns_TColourInstance()
        {
            var colour1 = new TColour(0, 0, 0);
            var colour2 = new TColour(0, 0, 0);

            Assert.That(colour1 + colour2, Is.InstanceOf<TColour>());
        }

        [Test]
        public void Add_Adds_RedComponents()
        {
            var colour1 = new TColour(1, 0, 0);
            var colour2 = new TColour(2, 0, 0);

            TColour result = colour1 + colour2;

            Assert.That(result.Red, Is.EqualTo(3));
        }

        [Test]
        public void Add_Adds_GreenComponents()
        {
            var colour1 = new TColour(0, 2, 0);
            var colour2 = new TColour(0, 4, 0);

            TColour result = colour1 + colour2;

            Assert.That(result.Green, Is.EqualTo(6));
        }

        [Test]
        public void Add_Adds_BlueComponents()
        {
            var colour1 = new TColour(0, 0, 4);
            var colour2 = new TColour(0, 0, 8);

            TColour result = colour1 + colour2;

            Assert.That(result.Blue, Is.EqualTo(12));
        }

        [Test]
        public void Subtract_Returns_TColourInstance()
        {
            var colour1 = new TColour(0, 0, 0);
            var colour2 = new TColour(0, 0, 0);

            Assert.That(colour1 - colour2, Is.InstanceOf<TColour>());
        }

        [Test]
        public void Subtract_Subtracts_RedComponents()
        {
            var colour1 = new TColour(1, 0, 0);
            var colour2 = new TColour(2, 0, 0);

            TColour result = colour1 - colour2;

            Assert.That(result.Red, Is.EqualTo(-1));
        }

        [Test]
        public void Subtract_Subtracts_GreenComponents()
        {
            var colour1 = new TColour(0, 2, 0);
            var colour2 = new TColour(0, 4, 0);

            TColour result = colour1 - colour2;

            Assert.That(result.Green, Is.EqualTo(-2));
        }

        [Test]
        public void Subtract_Subtracts_BlueComponents()
        {
            var colour1 = new TColour(0, 0, 4);
            var colour2 = new TColour(0, 0, 8);

            TColour result = colour1 - colour2;

            Assert.That(result.Blue, Is.EqualTo(-4));
        }

        [Test]
        public void Multiple_ReturnsTColourInstance_WhenMultiplyingByScalar()
        {
            var colour1 = new TColour(0, 0, 0);

            Assert.That(colour1 * 0.1, Is.InstanceOf<TColour>());
        }

        [Test]
        public void Multiply_Multiplies_RedComponentByScalar()
        {
            var colour = new TColour(0.1, 0, 0);

            TColour result = colour * 0.2;

            Assert.That(Math.Abs(result.Red - 0.02), Is.LessThan(0.0001));
        }

        [Test]
        public void Multiply_Multiplies_GreenComponentByScalar()
        {
            var colour = new TColour(0, 0.2, 0);

            TColour result = colour * 0.3;

            Assert.That(Math.Abs(result.Green - 0.06), Is.LessThan(0.0001));
        }

        [Test]
        public void Multiply_Multiplies_BlueComponentByScalar()
        {
            var colour = new TColour(0, 0, 0.3);

            TColour result = colour * 0.4;

            Assert.That(Math.Abs(result.Blue - 0.12), Is.LessThan(0.0001));
        }

        [Test]
        public void Multiple_ReturnsTColourInstance_WhenMultiplyingByTColour()
        {
            var colour1 = new TColour(0, 0, 0);
            var colour2 = new TColour(0, 0, 0);

            Assert.That(colour1 * colour2, Is.InstanceOf<TColour>());
        }

        [Test]
        public void Multiply_Multiplies_RedComponents()
        {
            var colour1 = new TColour(2, 0, 0);
            var colour2 = new TColour(4, 0, 0);

            TColour result = colour1 * colour2;

            Assert.That(result.Red, Is.EqualTo(8));
        }

        [Test]
        public void Multiply_Multiplies_GreenComponents()
        {
            var colour1 = new TColour(0, 4, 0);
            var colour2 = new TColour(0, 8, 0);

            TColour result = colour1 * colour2;

            Assert.That(result.Green, Is.EqualTo(32));
        }

        [Test]
        public void Multiply_Multiplies_BlueComponents()
        {
            var colour1 = new TColour(0, 0, 8);
            var colour2 = new TColour(0, 0, 16);

            TColour result = colour1 * colour2;

            Assert.That(result.Blue, Is.EqualTo(128));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenRedEqualToOtherRed()
        {
            var colour1 = new TColour(1, 0, 0);
            var colour2 = new TColour(1, 0, 0);

            Assert.That(colour1, Is.EqualTo(colour2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenYEqualToOtherY()
        {
            var colour1 = new TColour(0, 2, 0);
            var colour2 = new TColour(0, 2, 0);

            Assert.That(colour1, Is.EqualTo(colour2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenZEqualToOtherZ()
        {
            var colour1 = new TColour(0, 0, 3);
            var colour2 = new TColour(0, 0, 3);

            Assert.That(colour1, Is.EqualTo(colour2));
        }

        [Test]
        public void Equals_ReturnsFalse_WhenOtherIsObjectAndNull()
        {
            var colour1 = new TColour(0, 0, 0);
            Object colour2 = null;

            Assert.That(colour1.Equals(colour2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsObject()
        {
            var colour1 = new TColour(0, 0, 0);
            Object colour2 = new TColour(0, 0, 0);

            Assert.That(colour1.Equals(colour2), Is.True);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsObjectAndSameReference()
        {
            var colour1 = new TColour(0, 0, 0);
            Object colour2 = colour1;

            Assert.That(colour1.Equals(colour2), Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenOtherIsVectorAndNull()
        {
            var colour1 = new TColour(0, 0, 0);
            TColour colour2 = null;

            Assert.That(colour1.Equals(colour2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsVectorAndSameReference()
        {
            var colour1 = new TColour(0, 0, 0);
            TColour colour2 = colour1;

            Assert.That(colour1.Equals(colour2), Is.True);
        }

        [Test]
        public void Hashcode_ReturnsSameValue_WhenSetToSameValues()
        {
            var colour1 = new TColour(2, 3, 4);
            var colour2 = new TColour(2, 3, 4);

            Assert.That(colour1.GetHashCode(), Is.EqualTo(colour2.GetHashCode()));
        }
    }
}

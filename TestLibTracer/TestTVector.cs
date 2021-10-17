using System;
using libTracer;
using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestTVector
    {
        private const Single SOME_X = 1.2f;
        private const Single SOME_Y = 3.4f;
        private const Single SOME_Z = 5.6f;

        private TVector _vector;

        [Test]
        public void Constructor_SetsX_ToPassedInValue()
        {
            _vector = new TVector(SOME_X, 0, 0);

            Assert.That(_vector.X, Is.EqualTo(SOME_X));
        }

        [Test]
        public void Constructor_SetsY_ToPassedInValue()
        {
            _vector = new TVector(0, SOME_Y, 0);

            Assert.That(_vector.Y, Is.EqualTo(SOME_Y));
        }

        [Test]
        public void Constructor_SetsZ_ToPassedInValue()
        {
            _vector = new TVector(0, 0, SOME_Z);

            Assert.That(_vector.Z, Is.EqualTo(SOME_Z));
        }

        [Test]
        public void Constructor_SetsW_To0()
        {
            _vector = new TVector(0, 0, SOME_Z);

            Assert.That(TVector.W, Is.EqualTo(0f));
        }


        [Test]
        public void AddingVectors_Returns_Vector()
        {
            var vector1 = new TVector(0, 0, 0);
            var vector2 = new TVector(0, 0, 0);

            Assert.That(vector1 + vector2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void AddingVectors_Adds_XComponents()
        {
            var vector1 = new TVector(1, 0, 0);
            var vector2 = new TVector(2, 0, 0);

            TVector vector3 = vector1 + vector2;

            Assert.That(vector3.X, Is.EqualTo(3));
        }

        [Test]
        public void AddingVectors_Adds_YComponents()
        {
            var vector1 = new TVector(0, 2, 0);
            var vector2 = new TVector(0, 4, 0);

            TVector vector3 = vector1 + vector2;

            Assert.That(vector3.Y, Is.EqualTo(6));
        }

        [Test]
        public void AddingVectors_Adds_ZComponents()
        {
            var vector1 = new TVector(0, 0, 4);
            var vector2 = new TVector(0, 0, 8);

            TVector vector3 = vector1 + vector2;

            Assert.That(vector3.Z, Is.EqualTo(12));
        }

        [Test]
        public void AddingPointToVector_Returns_Vector()
        {
            var vector1 = new TVector(0, 0, 0);
            var point2 = new TPoint(0, 0, 0);

            Assert.That(vector1 + point2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void AddingVectorToPoint_Adds_XComponents()
        {
            var vector1 = new TVector(1, 0, 0);
            var point2 = new TPoint(2, 0, 0);

            TVector vector3 = vector1 + point2;

            Assert.That(vector3.X, Is.EqualTo(3));
        }

        [Test]
        public void AddingVectorToPoint_Adds_YComponents()
        {
            var vector1 = new TVector(0, 2, 0);
            var point2 = new TPoint(0, 4, 0);

            TVector vector3 = vector1 + point2;

            Assert.That(vector3.Y, Is.EqualTo(6));
        }

        [Test]
        public void AddingVectorToPoint_Adds_ZComponents()
        {
            var vector1 = new TVector(0, 0, 4);
            var point2 = new TPoint(0, 0, 8);

            TVector vector3 = vector1 + point2;

            Assert.That(vector3.Z, Is.EqualTo(12));
        }

        [Test]
        public void SubtractingVectorFromVector_Returns_Vector()
        {
            var vector1 = new TVector(0, 0, 0);
            var vector2 = new TVector(0, 0, 0);

            Assert.That(vector1 - vector2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void SubtractingVectorFromVector_Subtracts_XComponents()
        {
            var vector1 = new TVector(3, 0, 0);
            var vector2 = new TVector(1, 0, 0);

            TVector vector3 = vector1 - vector2;

            Assert.That(vector3.X, Is.EqualTo(2));
        }

        [Test]
        public void SubtractingVectorFromVector_Subtracts_YComponents()
        {
            var vector1 = new TVector(0, 5, 0);
            var vector2 = new TVector(0, 2, 0);

            TVector vector3 = vector1 - vector2;

            Assert.That(vector3.Y, Is.EqualTo(3));
        }

        [Test]
        public void SubtractingVectorFromVector_Subtracts_ZComponents()
        {
            var vector1 = new TVector(0, 0, 9);
            var vector2 = new TVector(0, 0, 1);

            TVector vector3 = vector1 - vector2;

            Assert.That(vector3.Z, Is.EqualTo(8));
        }

        [Test]
        public void Negation_ReturnsVector()
        {
            _vector = new TVector(SOME_X, SOME_Y, SOME_Z);

            TVector negativeVector = -_vector;

            Assert.That(negativeVector, Is.InstanceOf<TVector>());
        }

        [Test]
        public void Negation_Negates_XComponent()
        {
            _vector = new TVector(SOME_X, SOME_Y, SOME_Z);

            TVector negativeVector = -_vector;

            Assert.That(negativeVector.X, Is.EqualTo(-SOME_X));
        }

        [Test]
        public void Negation_Negates_YComponent()
        {
            _vector = new TVector(SOME_X, SOME_Y, SOME_Z);

            TVector negativeVector = -_vector;

            Assert.That(negativeVector.Y, Is.EqualTo(-SOME_Y));
        }

        [Test]
        public void Negation_Negates_ZComponent()
        {
            _vector = new TVector(SOME_X, SOME_Y, SOME_Z);

            TVector negativeVector = -_vector;

            Assert.That(negativeVector.Z, Is.EqualTo(-SOME_Z));
        }

        [Test]
        public void Multiply_Returns_Vector()
        {
            _vector = new TVector(0, 0, 0);

            Assert.That(_vector * 2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void Multiply_Multiplies_XComponent()
        {
            _vector = new TVector(3, 0, 0);

            TVector result = _vector * 2;

            Assert.That(result.X, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_Multiplies_YComponent()
        {
            _vector = new TVector(0, 4, 0);

            TVector result = _vector * 2;

            Assert.That(result.Y, Is.EqualTo(8));
        }

        [Test]
        public void Multiply_Multiplies_ZComponent()
        {
            _vector = new TVector(0, 0, 5);

            TVector result = _vector * 3;

            Assert.That(result.Z, Is.EqualTo(15));
        }

        [Test]
        public void Divide_Returns_Vector()
        {
            _vector = new TVector(0, 0, 0);

            Assert.That(_vector / 2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void Divide_Divides_XComponent()
        {
            _vector = new TVector(6, 0, 0);

            TVector result = _vector / 2;

            Assert.That(result.X, Is.EqualTo(3));
        }

        [Test]
        public void Divide_Divides_YComponent()
        {
            _vector = new TVector(0, 12, 0);

            TVector result = _vector / 3;

            Assert.That(result.Y, Is.EqualTo(4));
        }

        [Test]
        public void Divide_Divides_ZComponent()
        {
            _vector = new TVector(0, 0, 20);

            TVector result = _vector / 4;

            Assert.That(result.Z, Is.EqualTo(5));
        }

        [Test]
        public void Magnitude_Returns_X()
        {
            _vector = new TVector(SOME_X, 0, 0);

            Assert.That(_vector.Magnitude, Is.EqualTo(SOME_X));
        }

        [Test]
        public void Magnitude_Returns_Y()
        {
            _vector = new TVector(0, SOME_Y, 0);

            Assert.That(_vector.Magnitude, Is.EqualTo(SOME_Y));
        }

        [Test]
        public void Magnitude_Returns_Z()
        {
            _vector = new TVector(0, 0, SOME_Z);

            Assert.That(_vector.Magnitude, Is.EqualTo(SOME_Z));
        }

        [Test]
        public void Magnitude_Returns_SquareRootOfXSquaredAndYSquaredAndZSquared()
        {
            _vector = new TVector(SOME_X, SOME_Y, SOME_Z);

            Single expected = MathF.Sqrt((SOME_X * SOME_X) + (SOME_Y * SOME_Y) + (SOME_Z * SOME_Z));

            Assert.That(_vector.Magnitude, Is.EqualTo(expected));
        }

        [Test]
        public void Normalise_Returns_Vector()
        {
            _vector = new TVector(0, 0, 0);

            Assert.That(_vector.Normalise(), Is.InstanceOf<TVector>());
        }

        [Test]
        public void Normalise_SetsXComponent_To1()
        {
            _vector = new TVector(SOME_X, 0, 0);

            TVector result = _vector.Normalise();

            Assert.That(result.X, Is.EqualTo(1));
        }

        [Test]
        public void Normalise_SetsYComponent_To1()
        {
            _vector = new TVector(0, SOME_Y, 0);

            TVector result = _vector.Normalise();

            Assert.That(result.Y, Is.EqualTo(1));
        }

        [Test]
        public void Normalise_SetsZComponent_To1()
        {
            _vector = new TVector(0, 0, SOME_Z);

            TVector result = _vector.Normalise();

            Assert.That(result.Z, Is.EqualTo(1));
        }

        [Test]
        public void Normalise_SetsMagnitude_To1()
        {
            _vector = new TVector(SOME_X, SOME_Y, SOME_Z);

            TVector result = _vector.Normalise();

            Assert.That(result.Magnitude, Is.EqualTo(1));
        }

        [Test]
        public void Dot_Returns_Vector1xMultipliedWithVector2x()
        {
            var vector1 = new TVector(2, 0, 0);
            var vector2 = new TVector(3, 0, 0);

            Assert.That(vector1.Dot(vector2), Is.EqualTo(6));
        }

        [Test]
        public void Dot_Returns_Vector1yMultipliedWithVector2y()
        {
            var vector1 = new TVector(0, 3, 0);
            var vector2 = new TVector(0, 4, 0);

            Assert.That(vector1.Dot(vector2), Is.EqualTo(12));
        }

        [Test]
        public void Dot_Returns_Vector1zMultipliedWithVector2z()
        {
            var vector1 = new TVector(0, 0, 4);
            var vector2 = new TVector(0, 0, 5);

            Assert.That(vector1.Dot(vector2), Is.EqualTo(20));
        }

        [Test]
        public void Dot_Returns_Vector1wMultipliedWithVector2w()
        {
            var vector1 = new TVector(0, 0, 0);
            var vector2 = new TVector(0, 0, 0);

            Assert.That(vector1.Dot(vector2), Is.EqualTo(0));
        }

        [Test]
        public void Dot_Returns_SumOfXYZWComponentsMultipliedTogether()
        {
            var vector1 = new TVector(2, 3, 4);
            var vector2 = new TVector(3, 4, 5);

            Single expected = (2 * 3) + (3 * 4) + (4 * 5) + (0 * 0);

            Assert.That(vector1.Dot(vector2), Is.EqualTo(expected));
        }

        [Test]
        public void Cross_Returns_Vector()
        {
            var vector1 = new TVector(0, 0, 0);
            var vector2 = new TVector(0, 0, 0);

            Assert.That(vector1.Cross(vector2), Is.InstanceOf<TVector>());
        }

        [Test]
        public void Cross_ReturnsV1yMultipliedByV2z_InXComponent()
        {
            var vector1 = new TVector(0, 2, 0);
            var vector2 = new TVector(0, 0, 3);

            TVector result = vector1.Cross(vector2);

            Assert.That(result.X, Is.EqualTo(6));
        }

        [Test]
        public void Cross_ReturnsNegativeV1zMultipliedByV2y_InXComponent()
        {
            var vector1 = new TVector(0, 0, 2);
            var vector2 = new TVector(0, 3, 0);

            TVector result = vector1.Cross(vector2);

            Assert.That(result.X, Is.EqualTo(-6));
        }

        [Test]
        public void Cross_ReturnsV1zMultipliedByV2x_InYComponent()
        {
            var vector1 = new TVector(0, 0, 2);
            var vector2 = new TVector(3, 0, 0);

            TVector result = vector1.Cross(vector2);

            Assert.That(result.Y, Is.EqualTo(6));
        }

        [Test]
        public void Cross_ReturnsNegativeV1xMultipliedByV2z_InYComponent()
        {
            var vector1 = new TVector(2, 0, 0);
            var vector2 = new TVector(0, 0, 3);

            TVector result = vector1.Cross(vector2);

            Assert.That(result.Y, Is.EqualTo(-6));
        }

        [Test]
        public void Cross_ReturnsV1xMultipliedByV2y_InZComponent()
        {
            var vector1 = new TVector(2, 0, 0);
            var vector2 = new TVector(0, 3, 0);

            TVector result = vector1.Cross(vector2);

            Assert.That(result.Z, Is.EqualTo(6));
        }

        [Test]
        public void Cross_ReturnsNegativeV1xMultipliedByV2z_InZComponent()
        {
            var vector1 = new TVector(0, 2, 0);
            var vector2 = new TVector(3, 0, 0);

            TVector result = vector1.Cross(vector2);

            Assert.That(result.Z, Is.EqualTo(-6));
        }

        [Test]
        public void Cross_Returns_CrossProduct()
        {
            var vector1 = new TVector(1, 2, 3);
            var vector2 = new TVector(2, 3, 4);

            TVector result = vector1.Cross(vector2);

            var expected = new TVector(-1, 2, -1);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenXEqualToOtherX()
        {
            var vector1 = new TVector(1, 0, 0);
            var vector2 = new TVector(1, 0, 0);

            Assert.That(vector1, Is.EqualTo(vector2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenYEqualToOtherY()
        {
            var vector1 = new TVector(0, 2, 0);
            var vector2 = new TVector(0, 2, 0);

            Assert.That(vector1, Is.EqualTo(vector2));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenZEqualToOtherZ()
        {
            var vector1 = new TVector(0, 0, 3);
            var vector2 = new TVector(0, 0, 3);

            Assert.That(vector1, Is.EqualTo(vector2));
        }

        [Test]
        public void Equals_ReturnsFalse_WhenOtherIsObjectAndNull()
        {
            var vector1 = new TVector(0, 0, 0);
            Object vector2 = null;

            Assert.That(vector1.Equals(vector2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsObject()
        {
            var vector1 = new TVector(0, 0, 0);
            Object vector2 = new TVector(0, 0, 0);

            Assert.That(vector1.Equals(vector2), Is.True);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsObjectAndSameReference()
        {
            var vector1 = new TVector(0, 0, 0);
            Object vector2 = vector1;

            Assert.That(vector1.Equals(vector2), Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenOtherIsVectorAndNull()
        {
            var vector1 = new TVector(0, 0, 0);
            TVector vector2 = null;

            Assert.That(vector1.Equals(vector2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenOtherIsVectorAndSameReference()
        {
            var vector1 = new TVector(0, 0, 0);
            TVector vector2 = vector1;

            Assert.That(vector1.Equals(vector2), Is.True);
        }

        [Test]
        public void Hashcode_ReturnsSameValue_WhenSetToSameValues()
        {
            var vector1 = new TVector(2, 3, 4);
            var vector2 = new TVector(2, 3, 4);

            Assert.That(vector1.GetHashCode(), Is.EqualTo(vector2.GetHashCode()));
        }
    }
}

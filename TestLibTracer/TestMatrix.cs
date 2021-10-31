using System;
using System.Runtime.InteropServices.ComTypes;
using libTracer;

using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestMatrix
    {
        private const Int32 ROW_1 = 1;
        private const Int32 COL_1 = 1;

        private const Single SOME_X = 1.25f;
        private const Single SOME_Y = 2.33f;
        private const Single SOME_Z = 3.66f;

        private const Single EIGHTH_ROTATION_CLOCKWISE  = MathF.PI / 4;
        private const Single QUARTER_ROTATION_CLOCKWISE = MathF.PI / 2;

        private TMatrix _matrix;

        [Test]
        public void Constructor_ReturnsIdentityMatrix_WhenNoArgumentsPassed()
        {
            _matrix = new TMatrix();

            var expectedResult = new TMatrix(new Single[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });

            Assert.That(_matrix, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Columns_Returns_NumberOfColumns()
        {
            _matrix = new TMatrix(
                new[,] { 
                    { 1f, 2f, 3f }, 
                    { 3f, 4f, 5f } });
            Assert.That(_matrix.Columns, Is.EqualTo(3));
        }

        [Test]
        public void Columns_Returns_NumberOfRows()
        {
            _matrix = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            Assert.That(_matrix.Rows, Is.EqualTo(2));
        }

        [Test]
        public void Indexer_ReturnsValue_FromSpecifiedRowAndCol()
        {
            _matrix = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });


            Assert.That(_matrix[COL_1, ROW_1], Is.EqualTo(4f));
        }

        [Test]
        public void Equals_ReturnsTrue_WhenMatrixContainsSamesValuesInSamePositions()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            var matrix2 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });

            Assert.That(matrix1.Equals(matrix2), Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenMatrixDoesNotContainSamesValuesInSamePositions()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            var matrix2 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 6f } });

            Assert.That(matrix1.Equals(matrix2), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenComparedAgainstNull()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            TMatrix matrix2 = null;

            Assert.That(matrix1.Equals(matrix2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenComparedAgainstReferenceToSelf()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            TMatrix matrix2 = matrix1;

            Assert.That(matrix1.Equals(matrix2), Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenMarticesHaveDifferentNumberOfDimensions()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            var matrix2 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f },
                    { 6f, 7f, 8f }
                });

            Assert.That(matrix1.Equals(matrix2), Is.False);
        }

        [Test]
        public void Equals_ReturnsFalse_WhenComparedAgainstNullObject()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            Object matrix2 = null;

            Assert.That(matrix1.Equals(matrix2), Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenComparedAgainstObjectReferenceToSelf()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            Object matrix2 = matrix1;

            Assert.That(matrix1.Equals(matrix2), Is.True);
        }

        [Test]
        public void Equals_ReturnsTrue_WhenObjectContainsSamesValuesInSamePositions()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            Object matrix2 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });

            Assert.That(matrix1.Equals(matrix2), Is.True);
        }

        [Test]
        public void GetHashCode_ReturnsTrue_WhenMatricesAreSame()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });
            var matrix2 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f },
                    { 3f, 4f, 5f } });


            Assert.That(matrix1.GetHashCode(), Is.EqualTo(matrix2.GetHashCode()));
        }

        [Test]
        public void Multiply_ReturnsInstanceOfMatrix_WhenMultipliedByMatrix()
        {
            var matrix1 = new TMatrix(new Single[4, 4]);
            var matrix2 = new TMatrix(new Single[4, 4]);


            Assert.That(matrix1 * matrix2, Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void Multiply_ReturnsMatrix_With4Rows()
        {
            var matrix1 = new TMatrix(new Single[4, 4]);
            var matrix2 = new TMatrix(new Single[4, 4]);

            TMatrix result = matrix1 * matrix2;

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void Multiply_ReturnsMatrix_With4Columns()
        {
            var matrix1 = new TMatrix(new Single[4, 4]);
            var matrix2 = new TMatrix(new Single[4, 4]);

            TMatrix result = matrix1 * matrix2;

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void Multiply_Returns_DotProductForEachCell()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f, 4f },
                    { 5f, 6f, 7f, 8f },
                    { 9f, 8f, 7f, 6f },
                    { 5f, 4f, 3f, 2f }
                });
            var matrix2 = new TMatrix(
                new[,] {
                    { -2f, 1f, 2f,  3f },
                    {  3f, 2f, 1f, -1f },
                    {  4f, 3f, 6f,  5f },
                    {  1f, 2f, 7f,  8f }
                });

            TMatrix result = matrix1 * matrix2;

            var expectedResult = new TMatrix(
                new[,] {
                    { 20f, 22f,  50f,  48f },
                    { 44f, 54f, 114f, 108f },
                    { 40f, 58f, 110f, 102f },
                    { 16f, 26f,  46f,  42f }
                });

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Multiply_ReturnsOriginalValue_WhenMultiplyingMatrixByIdentityMatrix()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f, 4f },
                    { 5f, 6f, 7f, 8f },
                    { 9f, 8f, 7f, 6f },
                    { 5f, 4f, 3f, 2f }
                });
            var matrix2 = new TMatrix();

            TMatrix result = matrix1 * matrix2;

            Assert.That(result, Is.EqualTo(matrix1));
        }

        [Test]
        public void Multiply_ReturnsInstanceOfVector_WhenMultiplyingByVector()
        {
            var matrix1 = new TMatrix(new Single[4, 4]);
            var vector2 = new TVector(1, 2, 3);


            Assert.That(matrix1 * vector2, Is.InstanceOf<TVector>());
        }

        [Test]
        public void Multiply_ReturnsDotProductForEachCell_WhenMultipliedByVector()
        {
            var matrix1 = new TMatrix(
                new[,] {
                    { 1f, 2f, 3f, 4f },
                    { 2f, 4f, 4f, 2f },
                    { 8f, 6f, 4f, 1f },
                    { 0f, 0f, 0f, 1f }
                });
            var vector2 = new TVector(1, 2, 3);

            TVector result = matrix1 * vector2;

            var expectedResult = new TVector(14, 22, 32);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Multiply_ReturnsOriginalValue_WhenMultiplingIdentityMatrixByVector()
        {
            var matrix1 = new TMatrix();
            var vector2 = new TVector(1, 2, 3);

            TVector result = matrix1 * vector2;

            Assert.That(result, Is.EqualTo(vector2));
        }

        [Test]
        public void Multiply_ReturnsInstanceOfPoint_WhenMultiplyingByPoint()
        {
            var matrix1 = new TMatrix();
            var point2 = new TPoint(1, 2, 3);

            Assert.That(matrix1 * point2, Is.InstanceOf<TPoint>());
        }

        [Test]
        public void Multiply_ReturnsDotProductForEachCell_WhenMultipliedByPoint()
        {
            TMatrix matrix1 = new TMatrix().Translation(5, -3, 2);
            var point2 = new TPoint(-3, 4, 5);

            TPoint result = matrix1 * point2;

            var expectedResult = new TPoint(2, 1, 7);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Transpose_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix(
                new[,]
                {
                    { 1f, 2f, 3f, 4f },
                    { 2f, 4f, 4f, 2f },
                    { 8f, 6f, 4f, 1f },
                    { 0f, 0f, 0f, 1f }
                });

            Assert.That(_matrix.Transpose(), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void Transpose_SwapsValues_FromColumnsAndRows()
        {
            _matrix = new TMatrix(
                new[,]
                {
                    { 1f, 2f, 3f, 4f },
                    { 2f, 4f, 4f, 2f },
                    { 8f, 6f, 4f, 1f },
                    { 0f, 0f, 0f, 1f }
                });

            var expectedResult = new TMatrix(
                new[,]
                {
                    { 1f, 2f, 8f, 0f },
                    { 2f, 4f, 6f, 0f },
                    { 3f, 4f, 4f, 0f },
                    { 4f, 2f, 1f, 1f }
                });
            Assert.That(_matrix.Transpose(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Transpose_ReturnsIdentityMatrix_WhenIdentityMatrixTransposed()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.Transpose(), Is.EqualTo(_matrix));
        }

        [Test]
        public void Determinant_Returns_R0C0TimesR1C1MinusR0C1TimesR1C0()
        {
            _matrix = new TMatrix(new Single[,]
            {
                {  1, 5 },
                { -3, 2 }
            });

            Assert.That(_matrix.Determinant(), Is.EqualTo(17));
        }

        [Test]
        public void SubMatrix_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.SubMatrix(0, 0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void SubMatrix_ReturnsMatrix_With1LessRow()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.SubMatrix(0, 0);

            Assert.That(result.Rows, Is.EqualTo(3));
        }

        [Test]
        public void SubMatrix_ReturnsMatrix_With1LessColumn()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.SubMatrix(0, 0);

            Assert.That(result.Columns, Is.EqualTo(3));
        }

        [Test]
        public void SubMatrix_ReturnsMatrix_WithRowRemoved()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { 2, 2, 2, 2 },
                { 2, 2, 2, 2 },
                { 1, 1, 1, 1 },
                { 2, 2, 2, 2 },
            });

            TMatrix result = _matrix.SubMatrix(2, 0);

            var expectedResult = new TMatrix(new Single[,]
            {
                { 2, 2, 2 },
                { 2, 2, 2 },
                { 2, 2, 2 },
            });
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SubMatrix_ReturnsMatrix_WithColRemoved()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { 2, 2, 1, 2 },
                { 2, 2, 1, 2 },
                { 2, 2, 1, 2 },
                { 2, 2, 1, 2 },
            });

            TMatrix result = _matrix.SubMatrix(0, 2);

            var expectedResult = new TMatrix(new Single[,]
            {
                { 2, 2, 2 },
                { 2, 2, 2 },
                { 2, 2, 2 },
            });
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Minor_ReturnsTheDeterminant_OfTheSubmatrixAtTheSpecifiedLocation()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { 3,  5,  0 },
                { 2, -1, -7 },
                { 6, -1, 5 }
            });

            Assert.That(_matrix.Minor(1, 0), Is.EqualTo(25));
        }

        [Test]
        public void Cofactor_ReturnsMinorAtLocation_WhenRowPlusColumnIsEven()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { 3,  5,  0 },
                { 2, -1, -7 },
                { 6, -1, 5 }
            });

            Assert.That(_matrix.Cofactor(0, 0), Is.EqualTo(-12));
        }

        [Test]
        public void Cofactor_ReturnsNegativeMinorAtLocation_WhenRowPlusColumnIsOdd()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { 3,  5,  0 },
                { 2, -1, -7 },
                { 6, -1, 5 }
            });

            Assert.That(_matrix.Cofactor(1, 0), Is.EqualTo(-25));
        }

        [Test]
        public void Determinant_Returns_DeterminantOf3x3Matrix()
        {
            _matrix = new TMatrix(new Single[,]
            {
                {  1, 2,  6 },
                { -5, 8, -4 },
                {  2, 6,  4 }
            });

            Assert.That(_matrix.Determinant(), Is.EqualTo(-196));
        }

        [Test]
        public void Determinant_Returns_DeterminantOf4x4Matrix()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { -2, -8,  3,  5 },
                { -3,  1,  7,  3 },
                {  1,  2, -9,  6 },
                { -6,  7,  7, -9 }
            });

            Assert.That(_matrix.Determinant(), Is.EqualTo(-4071));
        }

        [Test]
        public void IsInvertable_ReturnsTrue_WhenMatrixCanBeInverted()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { 6,  4, 4,  4 },
                { 5,  5, 7,  6 },
                { 4, -9, 3, -7 },
                { 9,  1, 7, -6 }
            });

            Assert.That(_matrix.IsInvertable, Is.True);
        }

        [Test]
        public void IsInvertable_ReturnsFalse_WhenMatrixCanNotBeInverted()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { -4,  2, -2, -3 },
                {  9,  6,  2,  6 },
                {  0, -5,  1, -5 },
                {  0,  0,  0,  0 }
            });

            Assert.That(_matrix.IsInvertable, Is.False);
        }

        [Test]
        public void Inverse_Returns_Matrix()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { -5,  2,  6, -8 },
                {  1, -5,  1,  8 },
                {  7,  7, -6, -7 },
                {  1, -3,  7,  4 }
            });

            Assert.That(_matrix.Inverse(), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void Inverse_Returns_InvertedMatrix()
        {
            _matrix = new TMatrix(new Single[,]
            {
                { -5,  2,  6, -8 },
                {  1, -5,  1,  8 },
                {  7,  7, -6, -7 },
                {  1, -3,  7,  4 }
            });

            TMatrix result = _matrix.Inverse();

            var expectedResult = new TMatrix(new Single[,]
            {
                {  0.21805f,  0.45113f,  0.24060f, -0.04511f },
                { -0.80827f, -1.45677f, -0.44361f,  0.52068f },
                { -0.07895f, -0.22368f, -0.05263f,  0.19737f },
                { -0.52256f, -0.81391f, -0.30075f,  0.30639f }
            });
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [Test]
        public void Inverse_ReturnsOriginalMatrix_WhenCalledOnInvertedMatrix()
        {
            var original = new TMatrix(new Single[,]
            {
                { -5,  2,  6, -8 },
                {  1, -5,  1,  8 },
                {  7,  7, -6, -7 },
                {  1, -3,  7,  4 }
            });

            TMatrix result = _matrix.Inverse();

            Assert.That(result.Inverse(), Is.EqualTo(original));
        }

        [Test]
        public void Translation_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.Translation(0, 0, 0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void Translation_ReturnsMatrix_With4Rows()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Translation(0, 0, 0);

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void Translation_ReturnsMatrix_With4Columns()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Translation(0, 0, 0);

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void Translation_SetsR0C3_ToXComponent()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Translation(SOME_X, 0, 0);

            Assert.That(result[3, 0], Is.EqualTo(SOME_X));
        }

        [Test]
        public void Translation_SetsR1C3_ToYComponent()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Translation(0, SOME_Y, 0);

            Assert.That(result[3, 1], Is.EqualTo(SOME_Y));
        }

        [Test]
        public void Translation_SetsR2C3_ToZComponent()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Translation(0, 0, SOME_Z);

            Assert.That(result[3, 2], Is.EqualTo(SOME_Z));
        }

        [Test]
        public void Translation_MovesPointInOppositeDirection_WhenInverted()
        {
            _matrix = new TMatrix().Translation(5, -3, 2).Inverse();
            var point = new TPoint(-3, 4, 5);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-8, 7, 3);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Translation_ReturnsOriginalVector_WhenMultiplyingByVector()
        {
            TMatrix matrix = new TMatrix().Translation(SOME_X, SOME_Y, SOME_Z);
            var vector = new TVector(5, 6, 7);

            TVector result = matrix * vector;

            Assert.That(result, Is.EqualTo(vector));
        }

        [Test]
        public void Scaling_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.Scaling(0, 0, 0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void Scaling_ReturnsMatrix_With4Rows()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Scaling(0, 0, 0);

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void Scaling_ReturnsMatrix_With4Columns()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Scaling(0, 0, 0);

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void Scaling_SetsR0C0_ToXComponent()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Scaling(SOME_X, 0, 0);

            Assert.That(result[0, 0], Is.EqualTo(SOME_X));
        }

        [Test]
        public void Scaling_SetsR1C1_ToYComponent()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Scaling(0, SOME_Y, 0);

            Assert.That(result[1, 1], Is.EqualTo(SOME_Y));
        }

        [Test]
        public void Scaling_SetsR2C2_ToZComponent()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Scaling(0, 0, SOME_Z);

            Assert.That(result[2, 2], Is.EqualTo(SOME_Z));
        }

        [Test]
        public void Scaling_ScalesPoint_ByScalingMatrix()
        {
            _matrix = new TMatrix().Scaling(2, 3, 4);
            var point = new TPoint(-4, 6, 8);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-8, 18, 32);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Scaling_ScalesVector_ByScalingMatrix()
        {
            _matrix = new TMatrix().Scaling(2, 3, 4);
            var vector = new TVector(-4, 6, 8);

            TVector result = _matrix * vector;

            var expectedResult = new TVector(-8, 18, 32);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Scaling_ShrinksPointByScalingMatrix_WhenMatrixInverted()
        {
            _matrix = new TMatrix().Scaling(2, 3, 4).Inverse();
            var point = new TPoint(-4, 6, 8);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-2, 2, 2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Scaling_ReflectsPoint_WhenScalingFactorIsNegative()
        {
            _matrix = new TMatrix().Scaling(-1, 1, 1);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-2, 3, 4);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationX_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.RotationX(0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void RotationX_ReturnsMatrix_With4Rows()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationX(0);

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void RotationX_ReturnsMatrix_With4Columns()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationX(0);

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void RotationX_SetsR1C1_ToCosAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationX(MathF.PI);

            Assert.That(result[1, 1], Is.EqualTo(MathF.Cos(MathF.PI)));
        }

        [Test]
        public void RotationX_SetsR1C2_ToNegativeSinAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationX(MathF.PI);

            Assert.That(result[2, 1], Is.EqualTo(-MathF.Sin(MathF.PI)));
        }

        [Test]
        public void RotationX_SetsR2C1_ToSinAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationX(MathF.PI);

            Assert.That(result[1, 2], Is.EqualTo(MathF.Sin(MathF.PI)));
        }

        [Test]
        public void RotationX_SetsR2C2_ToCosAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationX(MathF.PI);

            Assert.That(result[2, 2], Is.EqualTo(MathF.Cos(MathF.PI)));
        }

        [Test]
        public void RotationX_RotatesPointAroundXAxis_ByEighthOfRotationClockwise()
        {
            _matrix = new TMatrix().RotationX(EIGHTH_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 1, 0);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(0, MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationX_RotatesPointAroundXAxis_ByQuarterOfRotationClockwise()
        {
            _matrix = new TMatrix().RotationX(QUARTER_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 1, 0);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(0, 0, 1);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationX_RotatesPointAroundXAxis_ByEighthOfRotationAntiClockwise()
        {
            _matrix = new TMatrix().RotationX(-EIGHTH_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 1, 0);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationY_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.RotationY(0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void RotationY_ReturnsMatrix_With4Rows()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationY(0);

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void RotationY_ReturnsMatrix_With4Columns()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationY(0);

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void RotationY_SetsR0C0_ToCosAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationY(MathF.PI);

            Assert.That(result[0, 0], Is.EqualTo(MathF.Cos(MathF.PI)));
        }

        [Test]
        public void RotationY_SetsR0C2_ToSinAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationY(MathF.PI);

            Assert.That(result[2, 0], Is.EqualTo(MathF.Sin(MathF.PI)));
        }

        [Test]
        public void RotationY_SetsR2C0_ToNegativeSinAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationY(MathF.PI);

            Assert.That(result[0, 2], Is.EqualTo(-MathF.Sin(MathF.PI)));
        }

        [Test]
        public void RotationY_SetsR2C2_ToCosAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationY(MathF.PI);

            Assert.That(result[2, 2], Is.EqualTo(MathF.Cos(MathF.PI)));
        }

        [Test]
        public void RotationY_RotatesPointAroundYAxis_ByEighthOfRotationClockwise()
        {
            _matrix = new TMatrix().RotationY(EIGHTH_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 0, 1);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(MathF.Sqrt(2) / 2, 0, MathF.Sqrt(2) / 2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationY_RotatesPointAroundYAxis_ByQuarterOfRotationClockwise()
        {
            _matrix = new TMatrix().RotationY(QUARTER_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 0, 1);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(1, 0, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationY_RotatesPointAroundYAxis_ByEighthOfRotationAntiClockwise()
        {
            _matrix = new TMatrix().RotationY(-EIGHTH_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 0, 1);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-MathF.Sqrt(2) / 2, 0, MathF.Sqrt(2) / 2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationZ_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.RotationZ(0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void RotationZ_ReturnsMatrix_With4Rows()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationZ(0);

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void RotationZ_ReturnsMatrix_With4Columns()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationZ(0);

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void RotationZ_SetsR0C0_ToCosAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationZ(MathF.PI);

            Assert.That(result[0, 0], Is.EqualTo(MathF.Cos(MathF.PI)));
        }

        [Test]
        public void RotationZ_SetsR0C1_ToNegativeSinAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationZ(MathF.PI);

            Assert.That(result[1, 0], Is.EqualTo(-MathF.Sin(MathF.PI)));
        }

        [Test]
        public void RotationZ_SetsR2C0_ToSinAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationZ(MathF.PI);

            Assert.That(result[0, 1], Is.EqualTo(MathF.Sin(MathF.PI)));
        }

        [Test]
        public void RotationZ_SetsR2C2_ToCosAngleInRadians()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.RotationZ(MathF.PI);

            Assert.That(result[1, 1], Is.EqualTo(MathF.Cos(MathF.PI)));
        }

        [Test]
        public void RotationZ_RotatesPointAroundZAxis_ByEighthOfRotationClockwise()
        {
            _matrix = new TMatrix().RotationZ(EIGHTH_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 1, 0);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationZ_RotatesPointAroundZAxis_ByQuarterOfRotationClockwise()
        {
            _matrix = new TMatrix().RotationZ(QUARTER_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 1, 0);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(-1, 0, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RotationZ_RotatesPointAroundZAxis_ByEighthOfRotationAntiClockwise()
        {
            _matrix = new TMatrix().RotationZ(-EIGHTH_ROTATION_CLOCKWISE);
            var point = new TPoint(0, 1, 0);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [Test]
        public void Shearing_Returns_InstanceOfMatrix()
        {
            _matrix = new TMatrix();

            Assert.That(_matrix.Shearing(0, 0, 0, 0, 0, 0), Is.InstanceOf<TMatrix>());
        }

        [Test]
        public void Shearing_ReturnsMatrix_With4Rows()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Shearing(0, 0, 0, 0, 0, 0);

            Assert.That(result.Rows, Is.EqualTo(4));
        }

        [Test]
        public void Shearing_ReturnsMatrix_With4Columns()
        {
            _matrix = new TMatrix();

            TMatrix result = _matrix.Shearing(0, 0, 0, 0, 0, 0);

            Assert.That(result.Columns, Is.EqualTo(4));
        }

        [Test]
        public void Shearing_SetsR0C1_ToXYValue()
        {
            _matrix = new TMatrix().Shearing(2, 0, 0, 0, 0, 0);

            Assert.That(_matrix[1, 0], Is.EqualTo(2));
        }

        [Test]
        public void Shearing_SetsR0C2_ToXZValue()
        {
            _matrix = new TMatrix().Shearing(0, 2, 0, 0, 0, 0);

            Assert.That(_matrix[2, 0], Is.EqualTo(2));
        }

        [Test]
        public void Shearing_SetsR1C0_ToYXValue()
        {
            _matrix = new TMatrix().Shearing(0, 0, 2, 0, 0, 0);

            Assert.That(_matrix[0, 1], Is.EqualTo(2));
        }

        [Test]
        public void Shearing_SetsR1C2_ToYZValue()
        {
            _matrix = new TMatrix().Shearing(0, 0, 0, 2, 0, 0);

            Assert.That(_matrix[2, 1], Is.EqualTo(2));
        }

        [Test]
        public void Shearing_SetsR1C2_ToZXValue()
        {
            _matrix = new TMatrix().Shearing(0, 0, 0, 0, 2, 0);

            Assert.That(_matrix[0, 2], Is.EqualTo(2));
        }

        [Test]
        public void Shearing_SetsR1C2_ToZYValue()
        {
            _matrix = new TMatrix().Shearing(0, 0, 0, 0, 0, 2);

            Assert.That(_matrix[1, 2], Is.EqualTo(2));
        }

        [Test]
        public void Shearing_MovesX_InProportionToY()
        {
            _matrix = new TMatrix().Shearing(1, 0, 0, 0, 0, 0);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(5, 3, 4);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Shearing_MovesX_InProportionToZ()
        {
            _matrix = new TMatrix().Shearing(0, 1, 0, 0, 0, 0);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(6, 3, 4);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Shearing_MovesY_InProportionToX()
        {
            _matrix = new TMatrix().Shearing(0, 0, 1, 0, 0, 0);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(2, 5, 4);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Shearing_MovesY_InProportionToZ()
        {
            _matrix = new TMatrix().Shearing(0, 0, 0, 1, 0, 0);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(2, 7, 4);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Shearing_MovesZ_InProportionToX()
        {
            _matrix = new TMatrix().Shearing(0, 0, 0, 0, 1, 0);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(2, 3, 6);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Shearing_MovesZ_InProportionToY()
        {
            _matrix = new TMatrix().Shearing(0, 0, 0, 0, 0, 1);
            var point = new TPoint(2, 3, 4);

            TPoint result = _matrix * point;

            var expectedResult = new TPoint(2, 3, 7);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ChainedTransformations_AreExecuted_InCorrectOrder()
        {
            // TODO: Make into a fluent API
            TMatrix rotate = new TMatrix().RotationX(MathF.PI / 2);
            TMatrix scaling = new TMatrix().Scaling(5, 5, 5);
            TMatrix translate = new TMatrix().Translation(10, 5, 7);
            TMatrix transform = translate * scaling * rotate;
            var point = new TPoint(1, 0, 1);

            TPoint result = transform * point;

            var expectedResult = new TPoint(15, 0, 7);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ChainedTransformations_AreExecutedInCorrectOrder_WhenCalledFluently2()
        {
            var point = new TPoint(1, 0, 1);
            TMatrix fluent = new TMatrix()
                .RotationX(MathF.PI / 2)
                .Scaling(5, 5, 5)
                .Translation(10, 5, 7);
            TPoint fluentResult = fluent * point;

            var expectedResult = new TPoint(15, 0, 7);
            Assert.That(fluentResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Rotation_MultipliesScaling_WhenCalledFluently()
        {
            TMatrix fluent = new TMatrix()
                .RotationX(MathF.PI / 2)
                .Scaling(5, 5, 5);

            TMatrix rotate = new TMatrix().RotationX(MathF.PI / 2);
            TMatrix scaling = new TMatrix().Scaling(5, 5, 5);
            TMatrix transform = scaling * rotate;

            Assert.That(fluent, Is.EqualTo(transform));
        }

        [Test]
        public void Rotation_MultipliesTranslation_WhenCalledFluently()
        {
            TMatrix fluent = new TMatrix()
                .RotationX(MathF.PI / 2)
                .Translation(10, 5, 7);

            TMatrix rotate = new TMatrix().RotationX(MathF.PI / 2);
            TMatrix translate = new TMatrix().Translation(10, 5, 7);
            TMatrix transform = translate * rotate;

            Assert.That(fluent, Is.EqualTo(transform));
        }

        [Test]
        public void Scaling_MultipliesRotation_WhenCalledFluently()
        {
            TMatrix fluent = new TMatrix()
                .Scaling(5, 5, 5)
                .RotationX(MathF.PI / 2);

            TMatrix rotate = new TMatrix().RotationX(MathF.PI / 2);
            TMatrix scaling = new TMatrix().Scaling(5, 5, 5);
            TMatrix transform = rotate * scaling;

            Assert.That(fluent, Is.EqualTo(transform));
        }

        [Test]
        public void Translation_MultipliesRotation_WhenCalledFluently()
        {
            TMatrix fluent = new TMatrix()
                .Translation(10, 5, 7)
                .RotationX(MathF.PI / 2);

            TMatrix rotate = new TMatrix().RotationX(MathF.PI / 2);
            TMatrix translate = new TMatrix().Translation(10, 5, 7);
            TMatrix transform = rotate * translate;

            Assert.That(fluent, Is.EqualTo(transform));
        }
    }
}

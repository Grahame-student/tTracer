using System;

using libTracer;

using NUnit.Framework;

namespace TestLibTracer
{
    internal class TestMatrix
    {
        private const Int32 ROW_1 = 1;
        private const Int32 COL_1 = 1;

        private TMatrix _matrix;

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
        public void Multiply_Returns_InstanceOfMatrix()
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
    }
}

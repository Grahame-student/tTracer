using System;
using System.ComponentModel;

namespace libTracer
{
    public class TMatrix : IEquatable<TMatrix>
    {
        private const Int32 DIMINSION_ROWS = 0;
        private const Int32 DIMINSION_COLS = 1;

        private readonly Single[,] _members;
        public Int32 Rows => _members.GetLength(DIMINSION_ROWS);

        public Int32 Columns => _members.GetLength(DIMINSION_COLS);

        public Single this[Int32 col, Int32 row] => _members[row, col];

        public TMatrix(Single[,] members)
        {
            _members = members;
        }

        public static TMatrix operator *(TMatrix m1, TMatrix m2)
        {
            // Hard coding for 4x4 matrix
            return new TMatrix(new[,]
            {
                {
                   //  C, R       C, R       C, R       C, R       C, R       C, R       C, R       C, R
                    m1[0, 0] * m2[0, 0] + m1[1, 0] * m2[0, 1] + m1[2, 0] * m2[0, 2] + m1[3, 0] * m2[0, 3], // R0 C0
                    m1[0, 0] * m2[1, 0] + m1[1, 0] * m2[1, 1] + m1[2, 0] * m2[1, 2] + m1[3, 0] * m2[1, 3], // R0 C1
                    m1[0, 0] * m2[2, 0] + m1[1, 0] * m2[2, 1] + m1[2, 0] * m2[2, 2] + m1[3, 0] * m2[2, 3], // R0 C2
                    m1[0, 0] * m2[3, 0] + m1[1, 0] * m2[3, 1] + m1[2, 0] * m2[3, 2] + m1[3, 0] * m2[3, 3]  // R0 C3
                },
                {
                    m1[0, 1] * m2[0, 0] + m1[1, 1] * m2[0, 1] + m1[2, 1] * m2[0, 2] + m1[3, 1] * m2[0, 3], // R1 C0
                    m1[0, 1] * m2[1, 0] + m1[1, 1] * m2[1, 1] + m1[2, 1] * m2[1, 2] + m1[3, 1] * m2[1, 3], // R1 C1
                    m1[0, 1] * m2[2, 0] + m1[1, 1] * m2[2, 1] + m1[2, 1] * m2[2, 2] + m1[3, 1] * m2[2, 3], // R1 C2
                    m1[0, 1] * m2[3, 0] + m1[1, 1] * m2[3, 1] + m1[2, 1] * m2[3, 2] + m1[3, 1] * m2[3, 3]  // R1 C3
                },
                {
                    m1[0, 2] * m2[0, 0] + m1[1, 2] * m2[0, 1] + m1[2, 2] * m2[0, 2] + m1[3, 2] * m2[0, 3], // R2 C0
                    m1[0, 2] * m2[1, 0] + m1[1, 2] * m2[1, 1] + m1[2, 2] * m2[1, 2] + m1[3, 2] * m2[1, 3], // R2 C1
                    m1[0, 2] * m2[2, 0] + m1[1, 2] * m2[2, 1] + m1[2, 2] * m2[2, 2] + m1[3, 2] * m2[2, 3], // R2 C2
                    m1[0, 2] * m2[3, 0] + m1[1, 2] * m2[3, 1] + m1[2, 2] * m2[3, 2] + m1[3, 2] * m2[3, 3]  // R2 C3
                },
                {
                    m1[0, 3] * m2[0, 0] + m1[1, 3] * m2[0, 1] + m1[2, 3] * m2[0, 2] + m1[3, 3] * m2[0, 3], // R3 C0
                    m1[0, 3] * m2[1, 0] + m1[1, 3] * m2[1, 1] + m1[2, 3] * m2[1, 2] + m1[3, 3] * m2[1, 3], // R3 C1
                    m1[0, 3] * m2[2, 0] + m1[1, 3] * m2[2, 1] + m1[2, 3] * m2[2, 2] + m1[3, 3] * m2[2, 3], // R3 C2
                    m1[0, 3] * m2[3, 0] + m1[1, 3] * m2[3, 1] + m1[2, 3] * m2[3, 2] + m1[3, 3] * m2[3, 3]  // R3 C3
                },
            });
        }

        public Boolean Equals(TMatrix other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if ((Rows != other.Rows) ||
                (Columns != other.Columns)) return false;
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    if (Math.Abs(_members[row, col] - other._members[row, col]) > 0.001) return false;
                }
            }

            return true;
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((TMatrix)obj);
        }

        public override Int32 GetHashCode()
        {
            Int32 result = HashCode.Combine(Rows, Columns);
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    result = HashCode.Combine(_members[row, col], result);
                }
            }

            return result;
        }
    }
}

using System;

namespace libTracer.Common
{
    public class TMatrix : IEquatable<TMatrix>
    {
        private const Int32 DIMINSION_ROWS = 0;
        private const Int32 DIMINSION_COLS = 1;

        private readonly Single[,] _members;
        public Int32 Rows => _members.GetLength(DIMINSION_ROWS);

        public Int32 Columns => _members.GetLength(DIMINSION_COLS);

        public Boolean IsInvertable => Determinant() != 0;

        public Single this[Int32 col, Int32 row]
        {
            get => _members[row, col];
            private init => _members[row, col] = value;
        }

        public TMatrix() : this(new Single[,]
        {
            { 1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 },
        })
        {
        }

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

        public static TVector operator *(TMatrix m, TVector v)
        {
            // Hard coding for 4x4 matrix
            return new TVector(m[0, 0] * v.X + m[1, 0] * v.Y + m[2, 0] * v.Z + m[3, 0] * v.W,
                               m[0, 1] * v.X + m[1, 1] * v.Y + m[2, 1] * v.Z + m[3, 1] * v.W,
                               m[0, 2] * v.X + m[1, 2] * v.Y + m[2, 2] * v.Z + m[3, 2] * v.W);
        }

        public static TPoint operator *(TMatrix m, TPoint p)
        {
            return new TPoint(m[0, 0] * p.X + m[1, 0] * p.Y + m[2, 0] * p.Z + m[3, 0] * p.W,
                              m[0, 1] * p.X + m[1, 1] * p.Y + m[2, 1] * p.Z + m[3, 1] * p.W,
                              m[0, 2] * p.X + m[1, 2] * p.Y + m[2, 2] * p.Z + m[3, 2] * p.W);
        }

        public Boolean Equals(TMatrix other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Rows != other.Rows ||
                Columns != other.Columns) return false;
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

        public TMatrix Transpose()
        {
            var result = new Single[Columns, Rows];
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    result[col, row] = _members[row, col];
                }
            }

            return new TMatrix(result);
        }

        public Single Determinant()
        {
            Single result = 0;
            if (Rows == 2 && Columns == 2)
            {
                result = _members[0, 0] * _members[1, 1] -
                         _members[0, 1] * _members[1, 0];
            }
            else
            {
                for (var col = 0; col < Columns; col++)
                {
                    result += Cofactor(0, col) * _members[0, col];
                }
            }

            return result;
        }

        public TMatrix SubMatrix(Int32 delRow, Int32 delCol)
        {
            var result = new Single[Rows - 1, Columns - 1];
            var newRow = 0;

            for (var curRow = 0; curRow < Rows; curRow++)
            {
                var newCol = 0;
                if (curRow == delRow) continue;
                for (var curCol = 0; curCol < Columns; curCol++)
                {
                    if (curCol == delCol) continue;
                    result[newRow, newCol] = _members[curRow, curCol];
                    newCol++;
                }

                newRow++;
            }

            return new TMatrix(result);
        }

        public Single Minor(Int32 row, Int32 col)
        {
            return SubMatrix(row, col).Determinant();
        }

        public Single Cofactor(Int32 row, Int32 col)
        {
            return (row + col & 1) == 0 ? Minor(row, col) : -Minor(row, col);
        }

        public TMatrix Inverse()
        {
            var result = new Single[Rows, Columns];

            Single determinant = Determinant();
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    result[col, row] = Cofactor(row, col) / determinant;
                }
            }

            return new TMatrix(result);
        }

        public TMatrix Translation(Single x, Single y, Single z)
        {
            return new TMatrix
            {
                [3, 0] = x,
                [3, 1] = y,
                [3, 2] = z
            } * this;
        }

        public TMatrix Scaling(Single x, Single y, Single z)
        {
            return new TMatrix
            {
                [0, 0] = x,
                [1, 1] = y,
                [2, 2] = z
            } * this;
        }

        public TMatrix RotationX(Single angleRads)
        {
            return new TMatrix
            {
                [1, 1] = MathF.Cos(angleRads),
                [2, 1] = -MathF.Sin(angleRads),
                [1, 2] = MathF.Sin(angleRads),
                [2, 2] = MathF.Cos(angleRads)
            } * this;
        }

        public TMatrix RotationY(Single angleRads)
        {
            return new TMatrix
            {
                [0, 0] = MathF.Cos(angleRads),
                [2, 0] = MathF.Sin(angleRads),
                [0, 2] = -MathF.Sin(angleRads),
                [2, 2] = MathF.Cos(angleRads)
            } * this;
        }

        public TMatrix RotationZ(Single angleRads)
        {
            return new TMatrix
            {
                [0, 0] = MathF.Cos(angleRads),
                [1, 0] = -MathF.Sin(angleRads),
                [0, 1] = MathF.Sin(angleRads),
                [1, 1] = MathF.Cos(angleRads)
            } * this;
        }

        public TMatrix Shearing(Single xy, Single xz, Single yx, Single yz, Single zx, Single zy)
        {
            return new TMatrix()
            {
                [1, 0] = xy,
                [2, 0] = xz,
                [0, 1] = yx,
                [2, 1] = yz,
                [0, 2] = zx,
                [1, 2] = zy
            } * this;
        }

        public static TMatrix ViewTransformation(TPoint from, TPoint to, TVector up)
        {
            TVector forward = (to - from).Normalise();
            TVector left = forward.Cross(up.Normalise());
            TVector trueUp = left.Cross(forward);

            var orientation = new TMatrix(new[,]
            {
                {  left.X,     left.Y,     left.Z,    0 },
                {  trueUp.X,   trueUp.Y,   trueUp.Z,  0 },
                { -forward.X, -forward.Y, -forward.Z, 0 },
                {  0,          0,          0,         1 }
            });

            return orientation * new TMatrix().Translation(-from.X, -from.Y, -from.Z);
        }
    }
}

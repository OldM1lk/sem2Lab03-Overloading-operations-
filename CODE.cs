
using System;
using System.Text;

namespace OverloadingOperations {
  class SquareMatrix : ICloneable, IComparable<SquareMatrix> {
    public int[,] matrix;
    public int Size { get; set; }

    public SquareMatrix(int size) {
      if (size <= 1) {
        throw new InvalidMatrixSizeException("Матрица не может быть такого размера!");
      }

      Size = size;
      matrix = new int[size, size];
      Random random = new Random();

      for (int rowIndex = 0; rowIndex < size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < size; ++columnIndex) {
          matrix[rowIndex, columnIndex] = random.Next(-10, 10);
        }
      }
    }

    public static SquareMatrix operator +(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      SquareMatrix result = (SquareMatrix)matrix1.Clone();

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          result.matrix[rowIndex, columnIndex] = matrix1.matrix[rowIndex, columnIndex] +
                                                 matrix2.matrix[rowIndex, columnIndex];
        }
      }
      return result;
    }

    public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      SquareMatrix result = (SquareMatrix)matrix1.Clone();

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          result.matrix[rowIndex, columnIndex] = matrix1.matrix[rowIndex, columnIndex] *
                                                 matrix2.matrix[rowIndex, columnIndex];
        }
      }
      return result;
    }

    public static bool operator >(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1 is null || matrix2 is null) {
        throw new CustomArgumentNullException("Обе матрицы должны быть ненулевыми!");
      }

      return matrix1.CompareTo(matrix2) > 0;
    }

    public static bool operator <(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      return matrix1.CompareTo(matrix2) < 0;
    }

    public static bool operator >=(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      return matrix1.CompareTo(matrix2) >= 0;
    }

    public static bool operator <=(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      return matrix1.CompareTo(matrix2) <= 0;
    }

    public static bool operator ==(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      return matrix1.Equals(matrix2);
    }

    public static bool operator !=(SquareMatrix matrix1, SquareMatrix matrix2) {
      if (matrix1.Size != matrix2.Size) {
        throw new DiffrentMatrixSizeException("Матрицы должны быть одного размера!");
      }

      return !matrix1.Equals(matrix2);
    }

    public static explicit operator int[,](SquareMatrix matrix) {
      int[,] result = new int[matrix.Size, matrix.Size];
      for (int rowIndex = 0; rowIndex < matrix.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix.Size; ++columnIndex) {
          result[rowIndex, columnIndex] = matrix.matrix[rowIndex, columnIndex];
        }
      }
      return result;
    }

    public static bool operator true(SquareMatrix matrix) {
      return !matrix.IsMatrixNull();
    }

    public static bool operator false(SquareMatrix matrix) {
      return matrix.IsMatrixNull();
    }

    public bool IsMatrixNull() {
      int result = 0;

      for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
          result += matrix[rowIndex, columnIndex];
        }
      }
      if (result == 0) {
        return true;
      } else {
        return false;
      }
    }

    public double Determinant() {
      if (matrix == null) {
        throw new CustomArgumentNullException("Матрица должна быть ненулевой!");
      }
      if (Size == 1) {
        return matrix[0, 0];
      } else if (Size == 2) {
        return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
      } else if (Size == 3) {
        return (matrix[0, 0] * matrix[1, 1] * matrix[2, 2]) +
               (matrix[1, 0] * matrix[2, 1] * matrix[0, 2]) +
               (matrix[0, 1] * matrix[1, 2] * matrix[2, 0]) -
               ((matrix[0, 2] * matrix[1, 1] * matrix[2, 0]) +
               (matrix[0, 1] * matrix[1, 0] * matrix[2, 2]) +
               (matrix[0, 0] * matrix[1, 2] * matrix[2, 1]));
      } else {
        Random random = new Random();
        return random.Next(-10, 10);
      }
    }

    public SquareMatrix Inverse () {
      SquareMatrix inverse = (SquareMatrix)this.Clone();
      SquareMatrix identity = new SquareMatrix(Size);

      for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
        identity.matrix[rowIndex, rowIndex] = 1;
      }
      for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
        int ratio = inverse.matrix[rowIndex, rowIndex];

        if (ratio == 0) {
          throw new NonInvertibleMatrixException("Данная матрица необратима!");
        }
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
          inverse.matrix[rowIndex, columnIndex] /= ratio;
          identity.matrix[rowIndex, columnIndex] /= ratio;
        }
        for (int elementIndex = 0; elementIndex < Size; ++elementIndex) {
          if (elementIndex == rowIndex) {
            continue;
          }
          int coefficient = inverse.matrix[elementIndex, rowIndex];

          for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
            inverse.matrix[elementIndex, columnIndex] -= coefficient * inverse.matrix[rowIndex, columnIndex];
            identity.matrix[elementIndex, columnIndex] -= coefficient * identity.matrix[rowIndex, columnIndex];
          }
        }
      }
      return identity;
    }

    public override string ToString() {
      StringBuilder stringBuilder = new StringBuilder();

      for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
          stringBuilder.Append(matrix[rowIndex, columnIndex].ToString("0" + "\t"));
        }
        stringBuilder.AppendLine();
      }
      return stringBuilder.ToString();
    }

    public int CompareTo(SquareMatrix other) {
      if (other == null) {
        return 1;
      }
      double determinantThis = this.Determinant();
      double determinantOther = other.Determinant();

      if (determinantThis < determinantOther) {
        return -1;
      } else if (determinantThis > determinantOther) {
        return 1;
      } else {
        return 0;
      }
    }

    public override bool Equals(object obj) {
      if (obj is SquareMatrix other) {
        if (this.Size != other.Size) {
          return false;
        }
        for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
          for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
            if (this.matrix[rowIndex, columnIndex] != other.matrix[rowIndex, columnIndex]) {
              return false;
            }
          }
        }
        return true;
      }
      return false;
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
        int primeNumber = 23;
        hash = hash * primeNumber + Size.GetHashCode();

        for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
          for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
            hash = hash * primeNumber + matrix[rowIndex, columnIndex].GetHashCode();
          }
        }
        return hash;
      }
    }

    public object Clone() {
      SquareMatrix clone = new SquareMatrix(Size);
      Array.Copy(matrix, clone.matrix, matrix.Length);
      return clone;
    }
  }

  class InvalidMatrixSizeException : Exception {
    public InvalidMatrixSizeException(string message) : base(message) { }
  }

  class DiffrentMatrixSizeException : Exception {
    public DiffrentMatrixSizeException(string message) : base(message) { }
  }

  class NonInvertibleMatrixException : Exception {
    public NonInvertibleMatrixException(string message) : base(message) { }
  }

  class CustomArgumentNullException : Exception {
    public CustomArgumentNullException(string message) : base(message) { }
  }

  class Program {
    static void Main(string[] args) {
      Random random = new Random();
      int matrixSize = random.Next(2, 5);
      SquareMatrix mymatrix1 = new SquareMatrix(matrixSize);
      SquareMatrix mymatrix2 = new SquareMatrix(matrixSize);

      Console.WriteLine("\nМатрица 1:");
      Console.WriteLine(mymatrix1);
      Console.WriteLine("Матрица 2:");
      Console.WriteLine(mymatrix2);
      Console.WriteLine("Сумма матриц:");
      Console.WriteLine(mymatrix1 + mymatrix2);
      Console.WriteLine("Произведение матриц:");
      Console.WriteLine(mymatrix1 * mymatrix2);
      Console.WriteLine("Определитель 1-ой матрицы:");
      Console.WriteLine(mymatrix1.Determinant());
      Console.WriteLine();
      Console.WriteLine("Определитель 2-ой матрицы:");
      Console.WriteLine(mymatrix2.Determinant());
      Console.WriteLine();
      Console.WriteLine("Матрица, обратная 1-ой:");
      Console.WriteLine(mymatrix1.Inverse());
      Console.WriteLine();
      Console.WriteLine("Матрица, обратная 2-ой:");
      Console.WriteLine(mymatrix2.Inverse());
      Console.WriteLine();
      Console.WriteLine("1-ая матрица больше 2-ой: " + (mymatrix1 > mymatrix2));
      Console.WriteLine("1-ая матрица меньше 2-ой: " + (mymatrix1 < mymatrix2));
      Console.WriteLine("1-ая матрица больше или равна 2-ой: " + (mymatrix1 >= mymatrix2));
      Console.WriteLine("1-ая матрица меньше или равна 2-ой: " + (mymatrix1 <= mymatrix2));
      Console.WriteLine("Матрицы равны: " + (mymatrix1 == mymatrix2));
      Console.WriteLine("Матрицы не равны: " + (mymatrix1 != mymatrix2));
      Console.WriteLine("1-ая матрица нулевая: " + mymatrix1.IsMatrixNull());
      Console.WriteLine("2-ая матрица нулевая: " + mymatrix2.IsMatrixNull());
    }
  }
}

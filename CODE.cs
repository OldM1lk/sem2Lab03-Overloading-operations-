
using System;
using System.Collections.Generic;
using System.Text;

namespace OverloadingOperations {
  class SquareMatrix {
    public int[,] matrix;
    public int Size { get; set; }

    public SquareMatrix(int size) {
      Size = size;
      matrix = new int[size, size];
      Random random = new Random();

      for (int rowIndex = 0; rowIndex < Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex) {
          matrix[rowIndex, columnIndex] = random.Next(-10, 10);
        }
      }
    }

    public static SquareMatrix operator +(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          result.matrix[rowIndex, columnIndex] = matrix1.matrix[rowIndex, columnIndex] +
                                                 matrix2.matrix[rowIndex, columnIndex];
        }
      }
      return result;
    }

    public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          result.matrix[rowIndex, columnIndex] = matrix1.matrix[rowIndex, columnIndex] *
                                                 matrix2.matrix[rowIndex, columnIndex];
        }
      }
      return result;
    }

    public static SquareMatrix operator >(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          if (matrix1.matrix[rowIndex, columnIndex] > matrix2.matrix[rowIndex, columnIndex]) {
            result.matrix[rowIndex, columnIndex] = 1;
          } else {
            result.matrix[rowIndex, columnIndex] = 0;
          }
        }
      }
      return result;
    }

    public static SquareMatrix operator <(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          if (matrix1.matrix[rowIndex, columnIndex] < matrix2.matrix[rowIndex, columnIndex]) {
            result.matrix[rowIndex, columnIndex] = 1;
          } else {
            result.matrix[rowIndex, columnIndex] = 0;
          }
        }
      }
      return result;
    }

    public static SquareMatrix operator >=(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          if (matrix1.matrix[rowIndex, columnIndex] >= matrix2.matrix[rowIndex, columnIndex]) {
            result.matrix[rowIndex, columnIndex] = 1;
          } else {
            result.matrix[rowIndex, columnIndex] = 0;
          }
        }
      }
      return result;
    }

    public static SquareMatrix operator <=(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          if (matrix1.matrix[rowIndex, columnIndex] <= matrix2.matrix[rowIndex, columnIndex]) {
            result.matrix[rowIndex, columnIndex] = 1;
          } else {
            result.matrix[rowIndex, columnIndex] = 0;
          }
        }
      }
      return result;
    }

    public static SquareMatrix operator ==(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          if (matrix1.matrix[rowIndex, columnIndex] == matrix2.matrix[rowIndex, columnIndex]) {
            result.matrix[rowIndex, columnIndex] = 1;
          } else {
            result.matrix[rowIndex, columnIndex] = 0;
          }
        }
      }
      return result;
    }

    public static SquareMatrix operator !=(SquareMatrix matrix1, SquareMatrix matrix2) {
      SquareMatrix result = new SquareMatrix(matrix1.Size);

      for (int rowIndex = 0; rowIndex < matrix1.Size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < matrix1.Size; ++columnIndex) {
          if (matrix1.matrix[rowIndex, columnIndex] != matrix2.matrix[rowIndex, columnIndex]) {
            result.matrix[rowIndex, columnIndex] = 1;
          } else {
            result.matrix[rowIndex, columnIndex] = 0;
          }
        }
      }
      return result;
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

    public double GetDeterminant() {
      return CalculateDeterminant(matrix);
    }

    private double CalculateDeterminant(int[,] Matrix) {
      
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

  }
  class Program {
    static void Main(string[] args) {
      SquareMatrix mymatrix = new SquareMatrix(3);
      Console.WriteLine(mymatrix.ToString());
      Console.ReadKey();
    }
  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    public static class LinearEquationsSystemMethods
    {
        public static class GaussianElimination
        {
            public static double[] Solve(double[,] coefficients, double[] constants)
            {
                int numberOfVariables = constants.Length;

                // Прямой ход
                for (int pivotIndex = 0; pivotIndex < numberOfVariables; pivotIndex++)
                {
                    for (int rowIndex = pivotIndex + 1; rowIndex < numberOfVariables; rowIndex++)
                    {
                        double factor = coefficients[rowIndex, pivotIndex] / coefficients[pivotIndex, pivotIndex];

                        for (int colIndex = pivotIndex; colIndex < numberOfVariables; colIndex++)
                        {
                            coefficients[rowIndex, colIndex] -= factor * coefficients[pivotIndex, colIndex];
                        }
                        constants[rowIndex] -= factor * constants[pivotIndex];
                    }
                }

                // Обратный ход
                double[] solution = new double[numberOfVariables];
                for (int pivotIndex = numberOfVariables - 1; pivotIndex >= 0; pivotIndex--)
                {
                    solution[pivotIndex] = constants[pivotIndex];
                    for (int colIndex = pivotIndex + 1; colIndex < numberOfVariables; colIndex++)
                    {
                        solution[pivotIndex] -= coefficients[pivotIndex, colIndex] * solution[colIndex];
                    }
                    solution[pivotIndex] /= coefficients[pivotIndex, pivotIndex];
                }

                return solution;
            }
        }
        public static class GaussJordanElimination
        {
            public static double[] Solve(double[,] coefficients, double[] constants)
            {
                int numberOfVariables = constants.Length;

                // Расширение матрицы
                for (int rowIndex = 0; rowIndex < numberOfVariables; rowIndex++)
                {
                    // Прямой ход для нормализации и исключения
                    for (int pivotIndex = 0; pivotIndex < numberOfVariables; pivotIndex++)
                    {
                        if (rowIndex != pivotIndex)
                        {
                            double factor = coefficients[pivotIndex, rowIndex] / coefficients[rowIndex, rowIndex];
                            for (int colIndex = 0; colIndex < numberOfVariables; colIndex++)
                            {
                                coefficients[pivotIndex, colIndex] -= factor * coefficients[rowIndex, colIndex];
                            }
                            constants[pivotIndex] -= factor * constants[rowIndex];
                        }
                    }
                }

                // Нормализация матрицы
                for (int rowIndex = 0; rowIndex < numberOfVariables; rowIndex++)
                {
                    double normalizingFactor = coefficients[rowIndex, rowIndex];
                    for (int colIndex = 0; colIndex < numberOfVariables; colIndex++)
                    {
                        coefficients[rowIndex, colIndex] /= normalizingFactor;
                    }
                    constants[rowIndex] /= normalizingFactor;
                }

                return constants;
            }
        }
        public static class Cramer
        {
            // Метод для вычисления определителя матрицы
            public static double CalculateDeterminant(double[,] matrix)
            {
                int size = matrix.GetLength(0); // Получаем размерность матрицы
                if (size == 1)
                {
                    return matrix[0, 0]; // Определитель 1x1
                }
                if (size == 2)
                {
                    // Определитель 2x2
                    return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
                }

                double determinant = 0;
                for (int column = 0; column < size; column++)
                {
                    // Создание минорной матрицы
                    double[,] minorMatrix = new double[size - 1, size - 1];
                    for (int row = 1; row < size; row++)
                    {
                        for (int currentColumn = 0; currentColumn < size; currentColumn++)
                        {
                            if (currentColumn < column)
                            {
                                minorMatrix[row - 1, currentColumn] = matrix[row, currentColumn];
                            }
                            else if (currentColumn > column)
                            {
                                minorMatrix[row - 1, currentColumn - 1] = matrix[row, currentColumn];
                            }
                        }
                    }

                    // Добавление знака и вычисление определителя
                    determinant += ((column % 2 == 0 ? 1 : -1) * matrix[0, column] * CalculateDeterminant(minorMatrix));
                }
                return determinant;
            }

            // Метод для решения системы линейных уравнений
            public static double[] Solve(double[,] coefficientMatrix, double[] constantTerms)
            {
                int size = coefficientMatrix.GetLength(0); // Получаем размерность матрицы
                double determinant = CalculateDeterminant(coefficientMatrix);
                double[] solution = new double[size];

                // Решение для каждой переменной
                for (int column = 0; column < size; column++)
                {
                    double[,] tempMatrix = (double[,])coefficientMatrix.Clone(); // Клонируем матрицу коэффициентов
                    for (int row = 0; row < size; row++)
                    {
                        tempMatrix[row, column] = constantTerms[row]; // Заменяем столбец на свободные члены
                    }
                    solution[column] = CalculateDeterminant(tempMatrix) / determinant; // Вычисляем значение переменной
                }

                return solution;
            }
        }
    }
}

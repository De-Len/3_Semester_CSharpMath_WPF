using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    internal static class LeastSquareMethod
    {
        public static class LinearRegression
        {
            public static double Slope { get; private set; } // Коэффициент наклона (угловой коэффициент)
            public static double Intercept { get; private set; } // Свободный член (пересечение с осью Y)

            public static void Fit(double[] independentVariable, double[] dependentVariable)
            {
                if (independentVariable.Length != dependentVariable.Length)
                    throw new ArgumentException("Длины массивов должны совпадать.");

                int numberOfPoints = independentVariable.Length;
                double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;

                for (int index = 0; index < numberOfPoints; index++)
                {
                    sumX += independentVariable[index];
                    sumY += dependentVariable[index];
                    sumXY += independentVariable[index] * dependentVariable[index];
                    sumX2 += independentVariable[index] * independentVariable[index];
                }

                Slope = (numberOfPoints * sumXY - sumX * sumY) / (numberOfPoints * sumX2 - sumX * sumX);
                Intercept = (sumY - Slope * sumX) / numberOfPoints;
            }

            public static double LinearFunction(double xValue)
            {
                return Slope * xValue + Intercept;
            }
        }

        public static class QuadraticRegression
        {
            public static double QuadraticCoefficient { get; private set; } // Коэффициент при x^2
            public static double LinearCoefficient { get; private set; } // Коэффициент при x
            public static double ConstantTerm { get; private set; } // Свободный член

            public static void Fit(double[] independentVariable, double[] dependentVariable)
            {
                int n = independentVariable.Length;

                double sumX = 0, sumY = 0, sumX2 = 0, sumX3 = 0, sumX4 = 0, sumXY = 0, sumX2Y = 0;

                for (int index = 0; index < n; ++index)
                {
                    sumX += independentVariable[index];
                    sumY += dependentVariable[index];
                    sumX2 += independentVariable[index] * independentVariable[index];
                    sumX3 += independentVariable[index] * independentVariable[index] * independentVariable[index];
                    sumX4 += independentVariable[index] * independentVariable[index] * independentVariable[index] * independentVariable[index];
                    sumXY += independentVariable[index] * dependentVariable[index];
                    sumX2Y += independentVariable[index] * independentVariable[index] * dependentVariable[index];
                }

                // Создаем матрицы для решения систем уравнений
                double[,] matrix = new double[3, 4]
                {
                    {sumX4, sumX3, sumX2, sumX2Y},
                    {sumX3, sumX2, sumX, sumXY},
                    {sumX2, sumX, n, sumY}
                };

                // Решаем систему уравнений методом Гаусса
                for (int i = 0; i < 3; i++)
                {
                    for (int j = i + 1; j < 3; j++)
                    {
                        double ratio = matrix[j, i] / matrix[i, i];
                        for (int k = 0; k < 4; k++)
                        {
                            matrix[j, k] -= ratio * matrix[i, k];
                        }
                    }
                }

                // Подсчитываем коэффициенты
                ConstantTerm = matrix[2, 3] / matrix[2, 2];
                LinearCoefficient = (matrix[1, 3] - matrix[1, 2] * ConstantTerm) / matrix[1, 1];
                QuadraticCoefficient = (matrix[0, 3] - matrix[0, 1] * LinearCoefficient - matrix[0, 2] * ConstantTerm) / matrix[0, 0];
            }

            public static double QuadraticFunction(double xValue)
            {
                return QuadraticCoefficient * xValue * xValue + LinearCoefficient * xValue + ConstantTerm;
            }
        }
    }
}

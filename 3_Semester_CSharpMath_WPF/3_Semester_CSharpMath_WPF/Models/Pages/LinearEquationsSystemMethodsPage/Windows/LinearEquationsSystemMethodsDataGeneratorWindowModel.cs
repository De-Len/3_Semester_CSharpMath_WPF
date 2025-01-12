using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.LinearEquationsSystemMethodsPage.Windows
{
    internal class LinearEquationsSystemMethodsDataGeneratorWindowModel
    {
        private Random _random;

        public LinearEquationsSystemMethodsDataGeneratorWindowModel()
        {
            _random = new Random();
        }

        public double[][] GenerateRandomDoubles(int rows, int columns, double minValue, double maxValue, int decimalPlaces)
        {
            if (rows <= 0)
            {
                throw new ArgumentException("Количество строк должно быть положительным.", nameof(rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentException("Количество столбцов должно быть положительным.", nameof(columns));
            }

            if (minValue >= maxValue)
            {
                throw new ArgumentException($"Минимальное значение ({minValue}) должно быть меньше максимального значения ({maxValue}).", nameof(minValue));
            }

            if (decimalPlaces < 0)
            {
                throw new ArgumentException("Количество знаков после запятой не может быть отрицательным.", nameof(decimalPlaces));
            }

            var randomDoubles = new double[rows][];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                randomDoubles[rowIndex] = new double[columns];

                for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    double randomValue = _random.NextDouble() * (maxValue - minValue) + minValue;
                    randomDoubles[rowIndex][columnIndex] = Math.Round(randomValue, decimalPlaces);
                }
            }

            return randomDoubles;
        }
    }
}

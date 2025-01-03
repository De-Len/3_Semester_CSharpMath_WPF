using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage.Windows;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.LeastSquareMethodPage.Windows
{
    internal class LeastSquareMethodDataGeneratorWindowModel
    {
        private Random _random;

        public LeastSquareMethodDataGeneratorWindowModel()
        {
            _random = new Random();
        }

        public double[] GenerateRandomDoubles(int length, double minValue, double maxValue, int decimalPlaces)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Длина массива должна быть положительной.", nameof(length));
            }

            if (minValue >= maxValue)
            {
                throw new ArgumentException("Минимальное значение должно быть меньше максимального значения.");
            }

            if (decimalPlaces < 0)
            {
                throw new ArgumentException("Количество знаков после запятой не может быть отрицательным.", nameof(decimalPlaces));
            }

            double[] randomDoubles = new double[length];

            for (int index = 0; index < length; index++)
            {
                //DataGeneratorAutomaticallyViewModel.Instance.ProgressBarValue = (index > 0) ? (int)(((double)index / length) * 100) : 0;

                double randomValue = _random.NextDouble() * (maxValue - minValue) + minValue;

                // Округление до указанного количества знаков после запятой
                randomDoubles[index] = Math.Round(randomValue, decimalPlaces);
            }
            LeastSquareMethodDataGeneratorWindowViewModel.GetInstance().ProgressBarValue += 50;

            return randomDoubles;
        }
    }
}

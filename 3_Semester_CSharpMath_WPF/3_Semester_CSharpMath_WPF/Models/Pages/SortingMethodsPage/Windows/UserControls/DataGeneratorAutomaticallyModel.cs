using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage.Windows.UserControls
{
    class DataGeneratorAutomaticallyModel
    {
        public static ObservableCollection<double> GenerateRandomDoubles(int count, double minValue, double maxValue, int decimalPlaces)
        {
            Random random = new();
            ObservableCollection<double> randomDoubles = new();

            for (int index = 0; index < count; ++index)
            {
                // Генерация случайного числа в диапазоне [minValue, maxValue)
                double randomDouble = minValue + (random.NextDouble() * (maxValue - minValue));
                // Округление числа до заданного количества знаков после запятой
                randomDouble = Math.Round(randomDouble, decimalPlaces);
                randomDoubles.Add(randomDouble);
            }

            return randomDoubles;
        }
    }
}

using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage.Windows.UserControls
{
    class DataGeneratorAutomaticallyModel
    {
        public static async Task GenerateRandomDoubles(int count, double minValue, double maxValue, int decimalPlaces)
        {
            Random random = new();
            for (int index = 0; index < count; ++index)
            {
                DataGeneratorAutomaticallyViewModel.Instance.ProgressBarValue = (index > 0) ? (int)(((double)index / count) * 100) : 0;       
                // Генерация случайного числа в диапазоне [minValue, maxValue)
                double randomDouble = minValue + (random.NextDouble() * (maxValue - minValue));
                // Округление числа до заданного количества знаков после запятой
                randomDouble = Math.Round(randomDouble, decimalPlaces);
                SortingMethodsDataGeneratorWindowViewModel.NumbersCollection.Add(randomDouble);
            }
            DataGeneratorAutomaticallyViewModel.Instance.ProgressBarValue = 100;
        }
    }
}

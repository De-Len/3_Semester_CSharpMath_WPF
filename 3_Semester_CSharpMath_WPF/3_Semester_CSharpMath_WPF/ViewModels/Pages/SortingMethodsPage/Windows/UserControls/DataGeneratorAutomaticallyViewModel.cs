using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage.Windows.UserControls;
using _3_Semester_CSharpMath_WPF.Models;
using System.Windows;
using System.Collections.ObjectModel;


namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls
{
    partial class DataGeneratorAutomaticallyViewModel : ObservableObject
    {
        public ICommand GenerateNumbersCommand;

        [ObservableProperty]
        private string _numbersCount;
        [ObservableProperty]
        private string _startLimit;
        [ObservableProperty]
        private string _endLimit;
        [ObservableProperty]
        private string _accuracyCountDigitsAfterPoint;

        public DataGeneratorAutomaticallyViewModel()
        {
            GenerateNumbersCommand = new RelayCommand(GenerateNumbers);
        }

        private void GenerateNumbers()
        {
            double startLimitDouble = double.NaN;
            double endLimitDouble = double.NaN;
            int accuracyCountDigitsAfterPointInt = 0;
            int numbersCountInt = 0;

            try
            {
                (startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt, numbersCountInt) = Helpers.CheckExceptionsSortingMethods(startLimit: StartLimit, endLimit: EndLimit,
                                                                                       accuracyCountDigitsAfterPoint: AccuracyCountDigitsAfterPoint, numbersCount: NumbersCount);
                SortingMethodsDataGeneratorWindowViewModel.NumbersCollection = DataGeneratorAutomaticallyModel.GenerateRandomDoubles(numbersCountInt, startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt);
                DataGeneratorManuallyViewModel. = "";
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка аргументов", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Неопределённая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

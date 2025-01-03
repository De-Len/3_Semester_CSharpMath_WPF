using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage.Windows.UserControls;
using _3_Semester_CSharpMath_WPF.Models;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage;
using CommunityToolkit.Mvvm.Input;
using Accord;
using _3_Semester_CSharpMath_WPF.Models.Pages.LeastSquareMethodPage.Windows;


namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage.Windows
{
    partial class LeastSquareMethodDataGeneratorWindowViewModel : ObservableObject
    {
        public ICommand GenerateNumbersCommand { get; }

        [ObservableProperty]
        private string _numbersCount;
        [ObservableProperty]
        private string _startLimitX;
        [ObservableProperty]
        private string _endLimitX;
        [ObservableProperty]
        private string _accuracyCountDigitsAfterPointX;
        [ObservableProperty]
        private string _startLimitY;
        [ObservableProperty]
        private string _endLimitY;
        [ObservableProperty]
        private string _accuracyCountDigitsAfterPointY;
        [ObservableProperty]
        private static int _progressBarValue;
        public static string FilePath { get; set; }
        private LeastSquareMethodDataGeneratorWindowModel _leastSquareMethodDataGeneratorWindowModel;
        private LeastSquareMethodPageViewModel _leastSquareMethodPageViewModel;

        // Статическое свойство для хранения единственного экземпляра класса
        private static LeastSquareMethodDataGeneratorWindowViewModel _instance;

        // Статический метод для доступа к экземпляру (синглтон)
        public static LeastSquareMethodDataGeneratorWindowViewModel GetInstance(LeastSquareMethodPageViewModel LeastSquareMethodPageViewModel = null)
        {
            if (_instance == null)
            {
                _instance = new LeastSquareMethodDataGeneratorWindowViewModel(LeastSquareMethodPageViewModel);
            }
            return _instance;
        }
        private LeastSquareMethodDataGeneratorWindowViewModel(LeastSquareMethodPageViewModel LeastSquareMethodPageViewModel)
        {
            _leastSquareMethodPageViewModel = LeastSquareMethodPageViewModel;
            _leastSquareMethodDataGeneratorWindowModel = new LeastSquareMethodDataGeneratorWindowModel();

            GenerateNumbersCommand = new RelayCommand(GenerateNumbers);
        }

        private async void GenerateNumbers()
        {
            ProgressBarValue = 0;
            int numbersCountInt = 0;

            double startLimitDoubleX = double.NaN;
            double endLimitDoubleX = double.NaN;
            int accuracyCountDigitsAfterPointIntX = 0;

            double startLimitDoubleY = double.NaN;
            double endLimitDoubleY = double.NaN;
            int accuracyCountDigitsAfterPointIntY = 0;


            double[] independentVariable;
            double[] dependentVariable;

            try
            {
                // Проверка и получение параметров
                (startLimitDoubleX, endLimitDoubleX, accuracyCountDigitsAfterPointIntX, numbersCountInt) =
                    Helpers.CheckExceptionsSortingMethods(
                        startLimit: StartLimitX,
                        endLimit: EndLimitX,
                        accuracyCountDigitsAfterPoint: AccuracyCountDigitsAfterPointX,
                        numbersCount: NumbersCount
                    );

                (startLimitDoubleY, endLimitDoubleY, accuracyCountDigitsAfterPointIntY, numbersCountInt) =
                   Helpers.CheckExceptionsSortingMethods(
                       startLimit: StartLimitY,
                       endLimit: EndLimitY,
                       accuracyCountDigitsAfterPoint: AccuracyCountDigitsAfterPointY,
                       numbersCount: NumbersCount
                   );

                await Task.Run(() => {
                    independentVariable = _leastSquareMethodDataGeneratorWindowModel.GenerateRandomDoubles(numbersCountInt, startLimitDoubleX, endLimitDoubleX, accuracyCountDigitsAfterPointIntX);
                    dependentVariable = _leastSquareMethodDataGeneratorWindowModel.GenerateRandomDoubles(numbersCountInt, startLimitDoubleY, endLimitDoubleY, accuracyCountDigitsAfterPointIntY);
                    _leastSquareMethodPageViewModel.UpdateTable(independentVariable, dependentVariable);
                });

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

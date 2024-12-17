using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage;
using SkiaSharp;
using System.Diagnostics.CodeAnalysis;
using AngouriMath.Extensions;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage
{
    partial class DichotomyMethodPageViewModel : ObservableObject
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public DrawMarginFrame Frame { get; set; }

        public DichotomyMethodPageViewModel()
        {

            SolveFunctionCommand = new RelayCommand(SolveFunction);
        }

        public ICommand SolveFunctionCommand { get; }

        private Visibility _chartVisibility = Visibility.Hidden;
        public Visibility ChartVisibility
        {
            get => _chartVisibility;
            set => SetProperty(ref _chartVisibility, value);
        }



        private string _userMathFormula = string.Empty;
        public string UserMathFormula
        {
            get => _userMathFormula;
            set
            {
                if (SetProperty(ref _userMathFormula, value))
                {
                    UserMathFunction = value;
                }
            }

        }

        public static string UserMathFunction { get; set; } = string.Empty;

        [ObservableProperty]
        private string _startLimit = string.Empty;
        [ObservableProperty]
        private string _endLimit = string.Empty;

        private string _accuracyCountDigitsAfterPoint = string.Empty;
        public string AccuracyCountDigitsAfterPoint
        {
            get => _accuracyCountDigitsAfterPoint;
            set
            {
                if (double.Parse(value) < 0)
                {
                    throw new FormatException("Неверный формат для точности вычисления. Пожалуйста, введите положительное число.");
                }
                if (SetProperty(ref _accuracyCountDigitsAfterPoint, value))
                {
                    _tolerance = Math.Pow(10, -double.Parse(value)).ToString();
                }
            }
        }

        [ObservableProperty]
        private string _answerCountDigitsAfterPoint = string.Empty;

        [ObservableProperty]
        private string _tolerance = string.Empty;

        [ObservableProperty]
        private string _foundRoot = string.Empty;

        private Visibility _foundRootVisibility = Visibility.Hidden;
        public Visibility FoundRootVisibility
        {
            get => _foundRootVisibility;
            set => SetProperty(ref _foundRootVisibility, value);
        }

        [ObservableProperty]
        private object _chartView;

        public void SolveFunction()
        {
            double startLimitDouble = double.NaN;
            double endLimitDouble = double.NaN;
            double toleranceDouble = double.NaN;

            try
            {
                if (UserMathFunction == "")
                {
                    throw new FormatException("Функция не задана.");
                }
                // Проверка и преобразование StartLimit
                if (!double.TryParse(StartLimit, out startLimitDouble))
                {
                    throw new FormatException("Неверный формат для начала интервала. Пожалуйста, введите число.");
                }

                // Проверка и преобразование EndLimit
                if (!double.TryParse(EndLimit, out endLimitDouble))
                {
                    throw new FormatException("Неверный формат для конца интервала. Пожалуйста, введите число.");
                }

                // Проверка и преобразование Tolerance (точности)
                if (!double.TryParse(Tolerance, out toleranceDouble))
                {
                    throw new FormatException("Неверный формат для точности вычисления. Пожалуйста, введите число.");
                }

                if (!double.TryParse(AnswerCountDigitsAfterPoint, out double answerCountDigitsAfterPoint))
                {
                    throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите число.");
                }

                // Проверка на соответствие границ
                if (startLimitDouble >= endLimitDouble)
                {
                    throw new ArgumentException("Начальная граница должна быть меньше конечной границы.");
                }

                MathMethodsGroup.UserMathFunction = UserMathFunction;

                // Вычисление корня методом Дихотомии
                FoundRoot = MathMethodsGroup.Bisection(startLimitDouble, endLimitDouble, toleranceDouble)
                    .ToString("F" + AnswerCountDigitsAfterPoint);

                ChartModel.StartLimit = float.Parse(StartLimit);
                ChartModel.EndLimit = float.Parse(EndLimit);
                ChartView = new ChartView();

                UpdateChartVisibility();
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

        private void UpdateChartVisibility()
        {
            // Например, если имеются данные для отображения
            if (Series != null && Series.Length > 0)
            {
                ChartVisibility = Visibility.Visible;
                FoundRootVisibility = Visibility.Visible;
            }
            else
            {
                ChartVisibility = Visibility.Collapsed;
                FoundRootVisibility = Visibility.Visible;
            }
        }
    }
}

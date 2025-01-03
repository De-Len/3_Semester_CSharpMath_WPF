using _3_Semester_CSharpMath_WPF.Models;
using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;


namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.CoordinateDescentMethodPage
{
    partial class CoordinateDescentMethodPageViewModel : ObservableObject
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public DrawMarginFrame Frame { get; set; }

        public CoordinateDescentMethodPageViewModel()
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

        [ObservableProperty]
        private bool _isToggleButtonOn;

        [ObservableProperty]
        private string _answerCountDigitsAfterPoint = string.Empty;

        [ObservableProperty]
        private string _countIterations = string.Empty;

        [ObservableProperty]
        private string _stepSize = string.Empty;

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
        [ObservableProperty]
        private object _chart3DView;

        public void SolveFunction()
        {
            double startLimitDouble = double.NaN;
            double endLimitDouble = double.NaN;
            double stepSize = double.NaN;
            int countIterationsInt = 0;
            try
            {
                (startLimitDouble, endLimitDouble, stepSize, countIterationsInt) = Helpers.CheckExceptionsCoordinateDescentMethod(userMathFunction: UserMathFunction, startLimit: StartLimit, endLimit: EndLimit, stepSize: StepSize,
                                                                                        answerCountDigitsAfterPoint: AnswerCountDigitsAfterPoint, countIterations: CountIterations, startLimitDouble: startLimitDouble, endLimitDouble: endLimitDouble,
                                                                                        countIterationsInt: countIterationsInt);

                MathMethodsGroup.UserMathFunction = UserMathFunction;

                // Вычисление корня методом Ньютона
                // FoundRoot = MathMethodsGroup.NewtonMethod(startLimitDouble, endLimitDouble, countIterationsInt, toleranceDouble)
                //    .ToString("F" + AnswerCountDigitsAfterPoint);
                if (IsToggleButtonOn)
                {
                    FoundRoot = MathMethodsGroup.CoordinateDescentMethod(startLimitDouble, endLimitDouble, stepSize, countIterationsInt, OptimizationType.Maximize)
                        .ToString("F" + AnswerCountDigitsAfterPoint);
                }
                else
                {
                    FoundRoot = MathMethodsGroup.CoordinateDescentMethod(startLimitDouble, endLimitDouble, stepSize, countIterationsInt, OptimizationType.Minimize)
                        .ToString("F" + AnswerCountDigitsAfterPoint);
                }

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

using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using _3_Semester_CSharpMath_WPF.Models;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.NewtonMethodPage
{
    partial class NewtonMethodPageViewModel : ObservableObject
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public DrawMarginFrame Frame { get; set; }

        public NewtonMethodPageViewModel()
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
        private bool _isToggleButtonOn;

        [ObservableProperty]
        private string _answerCountDigitsAfterPoint = string.Empty;

        [ObservableProperty]
        private string _countIterations = string.Empty;

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
            int countIterationsInt = 0;

            Helpers.TryAndCatchExceptions(UserMathFunction, StartLimit, EndLimit, Tolerance,
            AnswerCountDigitsAfterPoint, CountIterations,  out startLimitDouble, out endLimitDouble,
            out toleranceDouble, out countIterationsInt);

            List<object> list1 = [startLimitDouble, endLimitDouble,
                            toleranceDouble, countIterationsInt];


            MathMethodsGroup.UserMathFunction = UserMathFunction;

            // Вычисление корня методом Ньютона
            // FoundRoot = MathMethodsGroup.NewtonMethod(startLimitDouble, endLimitDouble, countIterationsInt, toleranceDouble)
            //    .ToString("F" + AnswerCountDigitsAfterPoint);
            if (IsToggleButtonOn)
            {
                FoundRoot = MathMethodsGroup.NewtonRaphson(startLimitDouble, endLimitDouble, countIterationsInt, toleranceDouble, OptimizationType.Maximize)
                    .ToString("F" + AnswerCountDigitsAfterPoint);
            }
            else
            {
                FoundRoot = MathMethodsGroup.NewtonRaphson(startLimitDouble, endLimitDouble, countIterationsInt, toleranceDouble, OptimizationType.Minimize)
                    .ToString("F" + AnswerCountDigitsAfterPoint);
            }
            ChartModel.StartLimit = float.Parse(StartLimit);
            ChartModel.EndLimit = float.Parse(EndLimit);
            ChartView = new ChartView();

            UpdateChartVisibility();

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

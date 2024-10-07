using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows;
using System.Windows.Input;


namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.GoldenSectionSearchPage
{
    partial class GoldenSectionSearchPageViewModel : ObservableObject
    {
        private DichotomyMethodPageModel _model;

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public DrawMarginFrame Frame { get; set; }

        public GoldenSectionSearchPageViewModel()
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
            double startLimitDouble = double.Parse(StartLimit);
            double endLimitDouble = double.Parse(EndLimit);
            double toleranceDouble = double.Parse(Tolerance);

            try
            {
                // FoundRoot = Math.Round(MathMethodsGroup.Bisection(startLimitInt, endLimitInt, toleranceInt), int.Parse(CountDigitsAfterPoint)).ToString();
                FoundRoot = MathMethodsGroup.GoldenSectionSeacthMinimum(startLimitDouble, endLimitDouble, toleranceDouble).ToString("G" + AnswerCountDigitsAfterPoint);
                ChartModel.StartLimit = float.Parse(StartLimit);
                ChartModel.EndLimit = float.Parse(EndLimit);
                ChartView = new ChartView();

                UpdateChartVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

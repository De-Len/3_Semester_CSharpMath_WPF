using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Windows;
using System.Windows.Input;



namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.EvaluatingDefiniteIntegralsPage
{
    partial class EvaluatingDefiniteIntegralsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _upperBound = string.Empty;
        [ObservableProperty]
        private string _lowerBound = string.Empty;
        [ObservableProperty]
        private string _subintervalsNumber = string.Empty;
        [ObservableProperty]
        private string _answerCountDigitsAfterPoint = string.Empty;
        [ObservableProperty]
        private object _chartView;
        [ObservableProperty]
        private string _foundRootRectangleMethod = string.Empty;
        [ObservableProperty]
        private string _foundRootTrapezoidalMethod = string.Empty;
        [ObservableProperty]
        private string _foundRootSimpsonMethod = string.Empty;
        [ObservableProperty]
        private bool _isSelectedRectangleMethod = true;
        [ObservableProperty]
        private bool _isSelectedTrapezoidalMethod = true;
        [ObservableProperty]
        private bool _isSelectedSimpsonMethod = true;
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

        private Visibility _chartVisibility = Visibility.Hidden;
        public Visibility ChartVisibility
        {
            get => _chartVisibility;
            set => SetProperty(ref _chartVisibility, value);
        }
        private Visibility _foundRootRectangleMethodVisibility = Visibility.Hidden;
        public Visibility FoundRootRectangleMethodVisibility
        {
            get => _foundRootRectangleMethodVisibility;
            set => SetProperty(ref _foundRootRectangleMethodVisibility, value);
        }
        private Visibility _foundRootTrapezoidalMethodVisibility = Visibility.Hidden;
        public Visibility FoundRootTrapezoidalMethodVisibility
        {
            get => _foundRootTrapezoidalMethodVisibility;
            set => SetProperty(ref _foundRootTrapezoidalMethodVisibility, value);
        }
        private Visibility _foundRootSimpsonMethodVisibility = Visibility.Hidden;
        public Visibility FoundRootSimpsonMethodVisibility
        {
            get => _foundRootSimpsonMethodVisibility;
            set => SetProperty(ref _foundRootSimpsonMethodVisibility, value);
        }

        public ICommand SolveFunctionCommand { get; }

        public EvaluatingDefiniteIntegralsPageViewModel()
        {
            SolveFunctionCommand = new RelayCommand(SolveFunction);
        }


        public void SolveFunction()
        {
            FoundRootRectangleMethodVisibility = Visibility.Collapsed;
            FoundRootTrapezoidalMethodVisibility = Visibility.Collapsed;
            FoundRootSimpsonMethodVisibility = Visibility.Collapsed;
            

            double upperBound = double.NaN;
            double lowerBound = double.NaN;
            int subintervalsNumber = 0;

            try
            {
                if (UserMathFunction == "")
                {
                    throw new FormatException("Функция не задана.");
                }
                // Проверка и преобразование StartLimit
                if (!double.TryParse(UpperBound, out upperBound))
                {
                    throw new FormatException("Неверный формат для начала интервала. Пожалуйста, введите число.");
                }

                // Проверка и преобразование EndLimit
                if (!double.TryParse(LowerBound, out lowerBound))
                {
                    throw new FormatException("Неверный формат для конца интервала. Пожалуйста, введите число.");
                }

                // Проверка и преобразование Tolerance (точности)
                if (!int.TryParse(SubintervalsNumber, out subintervalsNumber))
                {
                    throw new FormatException("Неверный формат для количества итераций. Пожалуйста, введите число.");
                }

                if (!double.TryParse(AnswerCountDigitsAfterPoint, out double answerCountDigitsAfterPoint))
                {
                    throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите число.");
                }

                // Проверка на соответствие границ
                if (lowerBound >= upperBound)
                {
                    throw new ArgumentException("Начальная граница должна быть меньше конечной границы.");
                }

                MathMethodsGroup.UserMathFunction = UserMathFunction;

                ChartModel.SubintervalsNumber = subintervalsNumber;
                if (IsSelectedRectangleMethod)
                {
                    ChartModel.IsSelectedRectangleMethod = true;
                    FoundRootRectangleMethod = EvaluatingDifiniteIntegralsModel.CalculateUsingRectangleMethod(lowerBound, upperBound, subintervalsNumber).ToString("F" + AnswerCountDigitsAfterPoint);
                    FoundRootRectangleMethodVisibility = Visibility.Visible;
                }
                if (IsSelectedTrapezoidalMethod)
                {
                    ChartModel.IsSelectedTrapezoidalMethod = true;  
                    FoundRootTrapezoidalMethod = EvaluatingDifiniteIntegralsModel.CalculateUsingTrapezoidalMethod(lowerBound, upperBound, subintervalsNumber).ToString("F" + AnswerCountDigitsAfterPoint);
                    FoundRootTrapezoidalMethodVisibility = Visibility.Visible;
                }
                if (IsSelectedSimpsonMethod)
                {
                    ChartModel.IsSelectedSimpsonMethod = true;
                    FoundRootSimpsonMethod = EvaluatingDifiniteIntegralsModel.CalculateUsingSimpsonMethod(lowerBound, upperBound, subintervalsNumber).ToString("F" + AnswerCountDigitsAfterPoint);
                    FoundRootSimpsonMethodVisibility = Visibility.Visible;
                }

                ChartModel.StartLimit = float.Parse(LowerBound);
                ChartModel.EndLimit = float.Parse(UpperBound);
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
            ChartModel.IsSelectedRectangleMethod = false;
            ChartModel.IsSelectedTrapezoidalMethod = false;
            ChartModel.IsSelectedSimpsonMethod = false;
        }

        public ISeries[] Series { get; set; }
        private void UpdateChartVisibility()
        {
            // Например, если имеются данные для отображения
            if (Series != null && Series.Length > 0)
            {
                ChartVisibility = Visibility.Visible;
                //FoundRootRectangleMethodVisibility = Visibility.Visible;
                //FoundRootTrapezoidalMethodVisibility = Visibility.Visible;
                //FoundRootSimpsonMethodVisibility = Visibility.Visible;
            }
            else
            {
                ChartVisibility = Visibility.Collapsed;
                //FoundRootRectangleMethodVisibility = Visibility.Visible;
                //FoundRootTrapezoidalMethodVisibility = Visibility.Visible;
                //FoundRootSimpsonMethodVisibility = Visibility.Visible;
            }
        }
       
    }
}

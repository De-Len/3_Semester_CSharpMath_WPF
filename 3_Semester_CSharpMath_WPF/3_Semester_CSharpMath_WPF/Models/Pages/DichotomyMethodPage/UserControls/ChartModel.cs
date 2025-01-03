using System;
using System.Collections.Generic;
using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using static _3_Semester_CSharpMath_WPF.Models.MathMethods.LeastSquareMethod;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls
{
    class ChartModel
    {
        #region Для определённых интегралов
        public static bool IsSelectedRectangleMethod = false;
        public static bool IsSelectedTrapezoidalMethod = false;
        public static bool IsSelectedSimpsonMethod = false;
        public static int SubintervalsNumberRectangleMethod;
        public static int SubintervalsNumberTrapezoidalMethod;
        public static int SubintervalsNumberNumberSimpsonMethod;
        #endregion

        #region Для метода наименьших квадратов
        public static bool IsLinearRegression = false;
        public static bool IsQuadraticRegression = false;
        #endregion

        public static int GraphsCount = 1;
        private static readonly SKColor s_dark = new(00, 00, 00);
        private static readonly SKColor s_gray1 = new(100, 100, 100);
        private static readonly SKColor s_gray2 = new(200, 200, 200);
        private static readonly SKColor s_white = new(255, 255, 255);

        public static float StartLimit { get; set; }
        public static float EndLimit { get; set; }

        public ISeries[] Series { get; set; } = CreateSeries();

        public Axis[] XAxes { get; set; } = CreateAxes("X ось");

        public Axis[] YAxes { get; set; } = CreateAxes("Y ось");

        public DrawMarginFrame Frame { get; set; } = new()
        {
            Fill = new SolidColorPaint(s_white),
            Stroke = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1
            }
        };

        private static ISeries[] CreateSeries()
        {
            var seriesList = new List<ISeries>();
            seriesList.Add(new LineSeries<ObservablePoint>
            {
                Values = Fetch(), 
                Stroke = new SolidColorPaint(SKColors.Blue, 4),
                Fill = null,
                GeometrySize = 0
            });

            if (IsSelectedRectangleMethod)
            {
                double lowerBound = StartLimit;
                double upperBound = EndLimit;

                double subintervalWidth = (upperBound - lowerBound) / SubintervalsNumberRectangleMethod; // Ширина подынтервалов

                for (int index = 0; index < SubintervalsNumberRectangleMethod; ++index)
                {
                    double x = lowerBound + index * subintervalWidth;
                    var y = MathMethodsGroup.SolveFunctionSingleArgument(x); 

                    seriesList.Add(new ScatterSeries<ObservablePoint>
                    {
                        Values = FetchRectangleMethodX(x, y, 0), // Вызываем Fetch с индексом
                        Fill = new SolidColorPaint(SKColors.Red, 4), // Цвет точки
                        GeometrySize = 5 // Размер точки
                    });
                    seriesList.Add(new LineSeries<ObservablePoint>
                    {
                        Values = FetchRectangleMethodX(x, y, subintervalWidth), // Вызываем Fetch с индексом
                        Stroke = new SolidColorPaint(SKColors.Red, 4),
                        Fill = null,
                        GeometrySize = 0
                    });
                    seriesList.Add(new LineSeries<ObservablePoint>
                    {
                        Values = FetchRectangleMethodY1(x, y, subintervalWidth), // Вызываем Fetch с индексом
                        Stroke = new SolidColorPaint(SKColors.Red, 4),
                        Fill = null,
                        GeometrySize = 0
                    });
                    seriesList.Add(new LineSeries<ObservablePoint>
                    {
                        Values = FetchRectangleMethodY2(x, y, subintervalWidth), // Вызываем Fetch с индексом
                        Stroke = new SolidColorPaint(SKColors.Red, 4),
                        Fill = null,
                        GeometrySize = 0
                    });
                }
            }

            if (IsSelectedTrapezoidalMethod)
            {
                double lowerBound = StartLimit;
                double upperBound = EndLimit;

                double subintervalWidth = (upperBound - lowerBound) / SubintervalsNumberTrapezoidalMethod; // Ширина подынтервалов

                // Рисую первый столбец
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchTrapezoidalMethodY(lowerBound, MathMethodsGroup.SolveFunctionSingleArgument(lowerBound), subintervalWidth),
                    Stroke = new SolidColorPaint(SKColors.Green, 4),
                    Fill = null,
                    GeometrySize = 0
                });

                for (int index = 0; index < SubintervalsNumberTrapezoidalMethod; ++index)
                {
                    double x = lowerBound + index * subintervalWidth;
                    var y = MathMethodsGroup.SolveFunctionSingleArgument(x); 

                    seriesList.Add(new LineSeries<ObservablePoint>
                    {
                        Values = FetchTrapezoidalMethodX(x, y, subintervalWidth), 
                        Stroke = new SolidColorPaint(SKColors.Green, 4),
                        Fill = null,
                        GeometrySize = 0
                    });
                    
                    // Дорисовываю последий столбик
                    seriesList.Add(new LineSeries<ObservablePoint>
                    {
                        Values = FetchTrapezoidalMethodY(x + subintervalWidth, MathMethodsGroup.SolveFunctionSingleArgument(x + subintervalWidth), subintervalWidth),
                        Stroke = new SolidColorPaint(SKColors.Green, 4),
                        Fill = null,
                        GeometrySize = 0
                    });
                }
                

            }

            if (IsSelectedSimpsonMethod)
            {
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchSimpsonMethod(), // Вызываем Fetch с индексом
                    Stroke = new SolidColorPaint(SKColors.Yellow),
                    Fill = null,
                    GeometrySize = 0
                });
            }

            if (IsLinearRegression)
            {
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchLinearRegression(), // Вызываем Fetch с индексом
                    Stroke = new SolidColorPaint(SKColors.Red),
                    Fill = null,
                    GeometrySize = 0
                });
            }

            if (IsQuadraticRegression)
            {
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchQuadraticRegression(), // Вызываем Fetch с индексом
                    Stroke = new SolidColorPaint(SKColors.Blue),
                    Fill = null,
                    GeometrySize = 0
                });
            }

            return seriesList.ToArray();
        }

        private static Axis[] CreateAxes(string axisName)
        {
            return new Axis[]
            {
                new Axis
                {
                    Name = axisName,
                    NamePaint = new SolidColorPaint(s_gray1),
                    TextSize = 18,
                    Padding = new Padding(5, 15, 5, 5),
                    LabelsPaint = new SolidColorPaint(s_dark),
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = s_dark,
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    },
                    SubseparatorsPaint = new SolidColorPaint
                    {
                        Color = s_gray2,
                        StrokeThickness = 0.5f
                    },
                    SubseparatorsCount = 9,
                    ZeroPaint = new SolidColorPaint
                    {
                        Color = s_gray1,
                        StrokeThickness = 2
                    },
                    TicksPaint = new SolidColorPaint
                    {
                        Color = s_dark,
                        StrokeThickness = 1.5f
                    },
                    SubticksPaint = new SolidColorPaint
                    {
                        Color = s_dark,
                        StrokeThickness = 1
                    }
                }
            };
        }

        private static List<ObservablePoint> Fetch()
        {
            var list = new List<ObservablePoint>();

            // Реализуйте вашу логику для выбора данных в зависимости от индекса серии
            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = MathMethodsGroup.SolveFunctionSingleArgument(x); // Меняем поведение на основе индекса
                list.Add(new ObservablePoint(x, y));
            }

            return list;
        }
        #region Отрисовка метода прямоугольников
        private static List<ObservablePoint> FetchRectangleMethodX(double x, double y, double subintervalWidth)
        {
            var list = new List<ObservablePoint>() 
            {
                new ObservablePoint(x, y),
                new ObservablePoint(x + subintervalWidth, y)
            };
            return list;
        }
        private static List<ObservablePoint> FetchRectangleMethodY1(double x, double y, double subintervalWidth)
        {
            var list = new List<ObservablePoint>()
            {
                new ObservablePoint(x, y),
                new ObservablePoint(x, 0)
            };
            return list;
        }
        private static List<ObservablePoint> FetchRectangleMethodY2(double x, double y, double subintervalWidth)
        {
            var list = new List<ObservablePoint>()
            {
                new ObservablePoint(x + subintervalWidth, y),
                new ObservablePoint(x + subintervalWidth, 0)
            };
            return list;
        }
        #endregion
        #region Отрисовка метода трапеций
        private static List<ObservablePoint> FetchTrapezoidalMethodX(double x, double y, double subintervalWidth)
        {
            var list = new List<ObservablePoint>()
            {
                new ObservablePoint(x, y),
                new ObservablePoint(x + subintervalWidth, MathMethodsGroup.SolveFunctionSingleArgument(x + subintervalWidth))
            };
            return list;
        }
        private static List<ObservablePoint> FetchTrapezoidalMethodY(double x, double y, double subintervalWidth)
        {
            var list = new List<ObservablePoint>()
            {
                new ObservablePoint(x, y),
                new ObservablePoint(x, 0)
            };
            return list;
        }
        #endregion

        #region Отрисовка метода Симсона
        private static List<ObservablePoint> FetchSimpsonMethod()
        {
            var list = new List<ObservablePoint>();

            double upperBound = EndLimit;
            double lowerBound = StartLimit;
            int subintervalsNumber = SubintervalsNumberNumberSimpsonMethod;

            if (subintervalsNumber % 2 != 0)
            {
                ++subintervalsNumber; // Делим количество подынтервалов на 2, если оно нечетное
            }

            double subintervalWidth = (upperBound - lowerBound) / subintervalsNumber;

            for (int index = 0; index <= subintervalsNumber; ++index)
            {
                double x = lowerBound + index * subintervalWidth;
                list.Add(new ObservablePoint(x, MathMethodsGroup.SolveFunctionSingleArgument(x)));

            }

            return list;
        }
        #endregion

        #region Отрисовка метода наименьших квадратов
        private static List<ObservablePoint> FetchLinearRegression()
        {
            var list = new List<ObservablePoint>();

            // Реализуйте вашу логику для выбора данных в зависимости от индекса серии
            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = LeastSquareMethod.LinearRegression.LinearFunction(x); // Меняем поведение на основе индекса
                list.Add(new ObservablePoint(x, y));
            }

            return list;
        }
        private static List<ObservablePoint> FetchQuadraticRegression()
        {
            var list = new List<ObservablePoint>();

            // Реализуйте вашу логику для выбора данных в зависимости от индекса серии
            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = LeastSquareMethod.QuadraticRegression.QuadraticFunction(x); // Меняем поведение на основе индекса
                list.Add(new ObservablePoint(x, y));
            }

            return list;
        }
        #endregion
    }
}
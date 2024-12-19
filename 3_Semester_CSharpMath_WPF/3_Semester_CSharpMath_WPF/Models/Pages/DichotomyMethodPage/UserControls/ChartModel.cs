using System.Collections.Generic;
using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls
{
    class ChartModel
    {
        #region Для определённых интегралов
        public static bool IsSelectedRectangleMethod { get; set; }
        public static bool IsSelectedTrapezoidalMethod { get; set; }
        public static bool IsSelectedSimpsonMethod { get; set; }
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
            var colors = new List<SKColor>
            {
                new SKColor(33, 150, 243), // Цвет первого графика
                new SKColor(239, 83, 80),  // Цвет второго графика
                new SKColor(76, 175, 80),   // Цвет третьего графика
                new SKColor(255, 193, 7)    // Цвет четвертого графика
            };

            seriesList.Add(new LineSeries<ObservablePoint>
            {
                Values = Fetch(), // Вызываем Fetch с индексом
                Stroke = new SolidColorPaint(colors[0], 4),
                Fill = null,
                GeometrySize = 0
            });

            if (IsSelectedRectangleMethod)
            {
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchRectangleMethod(), // Вызываем Fetch с индексом
                    Stroke = new SolidColorPaint(colors[1], 4),
                    Fill = null,
                    GeometrySize = 0
                });
            }

            if (IsSelectedTrapezoidalMethod)
            {
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchTrapezoidalMethod(), // Вызываем Fetch с индексом
                    Stroke = new SolidColorPaint(colors[2], 4),
                    Fill = null,
                    GeometrySize = 0
                });
            }

            if (IsSelectedSimpsonMethod)
            {
                seriesList.Add(new LineSeries<ObservablePoint>
                {
                    Values = FetchSimpsonMethod(), // Вызываем Fetch с индексом
                    Stroke = new SolidColorPaint(colors[3], 4),
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
        private static List<ObservablePoint> FetchRectangleMethod()
        {
            var list = new List<ObservablePoint>();

            // Реализуйте вашу логику для выбора данных в зависимости от индекса серии
            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = MathMethodsGroup.SolveFunctionSingleArgument(x+1); // Меняем поведение на основе индекса
                list.Add(new ObservablePoint(x, y));
            }

            return list;
        }
        private static List<ObservablePoint> FetchTrapezoidalMethod()
        {
            var list = new List<ObservablePoint>();

            // Реализуйте вашу логику для выбора данных в зависимости от индекса серии
            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = MathMethodsGroup.SolveFunctionSingleArgument(x+2); // Меняем поведение на основе индекса
                list.Add(new ObservablePoint(x, y));
            }

            return list;
        }
        private static List<ObservablePoint> FetchSimpsonMethod()
        {
            var list = new List<ObservablePoint>();

            // Реализуйте вашу логику для выбора данных в зависимости от индекса серии
            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = MathMethodsGroup.SolveFunctionSingleArgument(x+3); // Меняем поведение на основе индекса
                list.Add(new ObservablePoint(x, y));
            }

            return list;
        }
    }
}